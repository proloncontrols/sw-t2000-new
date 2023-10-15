
Imports System.IO.Ports
Imports System.Threading

Public Class CCom
    Inherits CBase

    Enum EPosFramework
        Value3
        Value2
        Value1
        Value0
        Size
    End Enum

    Enum EPosModBus
        Address
        Fct
        SubFct
        Size
    End Enum

    '----------------------------------------------------------------------------------------------
    Public Port As SerialPort
    Public ComThread As Thread
    Public CurModBusProtocol As Boolean
    Public CurModBusAddress As Byte
    Public PayloadSize As Integer
    Public TxLength As Integer
    Public RxLength As Integer
    Public TxBuffer(PACKET_SIZE_MAX) As Byte
    Public RxBuffer(PACKET_SIZE_MAX) As Byte
    Public CommandPositionOffset As Integer
    Public AttemptTimeout As Integer
    Public TotalRetries As Integer
    Public CmdIdentify As CCommandIdentify = New CCommandIdentify(Me)
    Public CmdEnter As CCommandEnter = New CCommandEnter(Me)
    Public CmdFirmwareErase As CCommandFirmwareErase = New CCommandFirmwareErase(Me)
    Public CmdFirmwareWrite As CCommandFirmwareWrite = New CCommandFirmwareWrite(Me)
    Public CmdAssetErase As CCommandAssetErase = New CCommandAssetErase(Me)
    Public CmdAssetWrite As CCommandAssetWrite = New CCommandAssetWrite(Me)
    Public CmdDataWrite As CCommandDataWrite = New CCommandDataWrite(Me)
    Public CmdLeave As CCommandLeave = New CCommandLeave(Me)

    '----------------------------------------------------------------------------------------------
    Private m_Cancelled As Boolean

    '----------------------------------------------------------------------------------------------
    Public Property Header As UInt32
        Get
            Dim Res As UInt32 = RxBuffer(EPosFramework.Value3)
            Res <<= 8
            Res += RxBuffer(EPosFramework.Value2)
            Res <<= 8
            Res += RxBuffer(EPosFramework.Value1)
            Res <<= 8
            Res += RxBuffer(EPosFramework.Value0)
            Return Res
        End Get
        Set(value As UInt32)
            TxBuffer(EPosFramework.Value3) = value >> 24 And &HFF
            TxBuffer(EPosFramework.Value2) = value >> 16 And &HFF
            TxBuffer(EPosFramework.Value1) = value >> 8 And &HFF
            TxBuffer(EPosFramework.Value0) = value And &HFF
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Property ModBusAddress As Byte
        Get
            Return RxBuffer(EPosModBus.Address)
        End Get
        Set(value As Byte)
            TxBuffer(EPosModBus.Address) = value
        End Set
    End Property

    Public Property ModBusFct As Byte
        Get
            Return RxBuffer(EPosModBus.Fct)
        End Get
        Set(value As Byte)
            TxBuffer(EPosModBus.Fct) = value
        End Set
    End Property

    Public Property ModBusSubFct As Byte
        Get
            Return RxBuffer(EPosModBus.SubFct)
        End Get
        Set(value As Byte)
            TxBuffer(EPosModBus.SubFct) = value
        End Set
    End Property

    Public ReadOnly Property IsModBus As Boolean
        Get
            If Header = HEADER_FRAMEWORK_VALUE Then
                CommandPositionOffset = EPosFramework.Size
                Return False
            End If
            CommandPositionOffset = EPosModBus.Size
            Return True
        End Get
    End Property

    '----------------------------------------------------------------------------------------------
    Public Property Command As Byte
        Get
            Return RxBuffer(CommandPositionOffset)
        End Get
        Set(value As Byte)
            TxBuffer(CommandPositionOffset) = value
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Property Result As Byte
        Get
            Return RxBuffer(CommandPositionOffset + 1)
        End Get
        Set(value As Byte)
            TxBuffer(CommandPositionOffset + 1) = value
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Sub New(ByRef NewOwner As Form, ByRef NewPort As SerialPort)
        MyBase.New(NewOwner)
        Port = NewPort
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Sub Execute(Cmd As Byte, CmdLength As Integer)
        If CurModBusProtocol Then
            CommandPositionOffset = EPosModBus.Size
            PayloadSize = MODBUS_PAYLOAD_SIZE
            ModBusAddress = CurModBusAddress
            ModBusFct = MODBUS_MEIT_FUNCTION
            ModBusSubFct = MODBUS_MEIT_FUNCTION_UPGRADE
        Else
            CommandPositionOffset = EPosFramework.Size
            PayloadSize = PAYLOAD_SIZE
            Header = HEADER_FRAMEWORK_VALUE
        End If

        Command = Cmd
        TxLength = CommandPositionOffset + CmdLength
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Sub Cancel()
        m_Cancelled = True
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function TxRx() As Boolean
        Const RxTimeout As Integer = 10   '10msec without any activity on the RX line

        Dim Success As Boolean = False
        Dim Done As Boolean
        Dim Index As Integer
        Dim Attempts As Integer
        Dim RxTimeoutCounter As Integer
        Dim RxTimeoutCount As Integer = AttemptTimeout / RxTimeout

        Done = False
        Attempts = 0
        m_Cancelled = False
        Port.ReadTimeout = RxTimeout
        Index = 0
        RxTimeoutCounter = 0
        Port.DiscardInBuffer()
        Port.Write(TxBuffer, 0, TxLength)

        Do
            Try
                Do
                    Index += Port.Read(RxBuffer, Index, 1)
                    If Index >= PACKET_SIZE_MAX Then
                        Done = True
                    End If
                Loop While Not Done
            Catch ex As Exception
                If Index > 0 Then
                    Done = True
                Else
                    RxTimeoutCounter += 1
                    If RxTimeoutCounter >= RxTimeoutCount Then
                        Attempts += 1
                        If Attempts < TXRX_ATTEMPTS Then
                            TotalRetries += 1
                            PostMessage(WN_INT, INT_MSG_RETRIES, TotalRetries)
                            RxTimeoutCounter = 0
                            Port.DiscardInBuffer()
                            Port.Write(TxBuffer, 0, TxLength)
                        End If
                    End If
                End If
            End Try
        Loop Until m_Cancelled Or Attempts >= TXRX_ATTEMPTS Or Done

        If m_Cancelled Then
            PostMessage(WN_INT, INT_MSG_CANCELLED, 0)

        ElseIf Attempts >= TXRX_ATTEMPTS Then
            PostMessage(WN_INT, INT_MSG_NO_ANSWER, 0)

        ElseIf Not ValidateChecksum(Index) Then
            PostMessage(WN_INT, INT_MSG_CRC_MISMATCH, 0)

        Else
            If (Command And CMD_ERROR_FLAG) = 0 Then
                Success = True
            End If
            PostMessage(WM_COM, Command, Result)
        End If

        Return Success
    End Function

    '----------------------------------------------------------------------------------------------
    Public Sub AppendChecksum(Checksum As UInt16)
        TxBuffer(TxLength) = Checksum >> 8 And &HFF
        TxBuffer(TxLength + 1) = Checksum And &HFF
        TxLength += sizeofChecksum
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Function ValidateChecksum(TotalLength As Integer) As Boolean
        Dim ChecksumRange As Integer = TotalLength - sizeofChecksum
        Dim Checksum As UInt16 = RxBuffer(ChecksumRange + 1)
        Checksum <<= 8
        Checksum += RxBuffer(ChecksumRange)
        If Checksum = CRC16(RxBuffer, 0, ChecksumRange) Then
            Return True
        End If
        Return False
    End Function

End Class
