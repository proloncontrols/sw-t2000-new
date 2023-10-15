
Imports System.IO.Ports
Imports System.Threading

Public Module MProduct
    Public Const FAMILY_THERMOSTAT As Byte = &H1
End Module



Public MustInherit Class CProduct
    Inherits CCom

    Public FamilyID As Byte
    Public ProductID As Byte
    Public CurHexFile As String
    Public PacketCount As Integer
    Public CalculatedCRC As UInt16
    Public ProgressTotal As UInt32
    Public Sections As CSections = New CSections
    Public Segments As CSegments = New CSegments

    '----------------------------------------------------------------------------------------------
    Public Sub New(ByRef NewOwner As Form, ByRef NewPort As SerialPort, NewFamilyID As Byte, NewProductID As Byte)
        MyBase.New(NewOwner, NewPort)

        FamilyID = NewFamilyID
        ProductID = NewProductID
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Sub Identify(ModBusProtocol As Boolean, ModBusAddress As Byte)
        CurModBusProtocol = ModBusProtocol
        CurModBusAddress = ModBusAddress
        ComThread = New Thread(AddressOf IdentifyThread)
        ComThread.Start()
    End Sub

    Private Sub IdentifyThread()
        UpdateButtons(False)
        WaitCursor()
        CmdIdentify.Execute()
        DefaultCursor()
        UpdateButtons(True)
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Sub Program(ModBusProtocol As Boolean, ModBusAddress As Byte, HexFile As String)
        CurModBusProtocol = ModBusProtocol
        CurModBusAddress = ModBusAddress
        CurHexFile = HexFile
        ComThread = New Thread(AddressOf ProgramThread)
        ComThread.Start()
    End Sub

    Private Sub ProgramThread()
        UpdateButtons(False)
        WaitCursor()
        If Programming() Then
            OutputMessage("<<<<<<<< SUCCESS >>>>>>>>")
        Else
            OutputMessage("<<<<<<<< FAILURE >>>>>>>>")
        End If
        DefaultCursor()
        UpdateButtons(True)
        CloseHexFile()
    End Sub

    Private Function Programming() As Boolean
        TotalRetries = 0
        PacketCount = 0

        'Identifying product
        If Not CmdIdentify.Execute() Then
            Return False
        End If

        'Open Hex file And build its segment table
        OutputMessage("Loading HEX file")
        If Not LoadHexFile(CurHexFile) Then
            PostMessage(WN_INT, INT_MSG_HEX_FILE, 0)
            Return False
        Else
            'Build programming segments table
            Segments.Clear()
            For Each HexSegment In HexSegments
                Dim Section As CSection = Sections.GetSection(HexSegment.Address)
                If Section Is Nothing Then
                    Return False
                End If
                Select Case Section.Category
                    Case ESectionCategory.Firmware
                        Segments.Add(New CSegmentFirmware(HexSegment))
                    Case ESectionCategory.Asset
                        Segments.Add(New CSegmentAsset(HexSegment, (HexSegment.Address - Section.Origin) / Section.SectorSize))
                    Case ESectionCategory.Data
                        Segments.Add(New CSegmentData(HexSegment))
                End Select
            Next

            'Build transfer progress display
            ProgressTotal = 1     'Initialize with firmware earse command transfer
            For Each Segment In Segments

                Dim Offset As Integer
                Dim HexSegment As CIntelHexSegmentData = New CIntelHexSegmentData

                'All data (firmware/assets/data) command transfer
                LoadHexSegment(HexSegment, Segment.HexSegment.Address)

                Offset = 0
                Do
                    For i As Integer = 0 To PayloadSize - 1
                        If HexSegment.Data(Offset + i) <> &HFF Then
                            ProgressTotal += 1
                            Exit For
                        End If
                    Next
                    Offset += PayloadSize
                Loop Until Offset >= SEGMENT_SIZE

                'Erase asset sector command transfer
                If Segment.Category = ESegmentCategory.Asset Then
                    ProgressTotal += 1
                End If

            Next
            ProgressInit(ProgressTotal)

            'Enter programming
            OutputMessage("Entering programming mode")
            If Not CmdEnter.Execute() Then
                Return False
            End If

            'Reset transfer CRC
            CRCClear()

            'Erase firmware
            OutputMessage("Erasing firmware")
            CmdFirmwareErase.Firmware = 0
            CmdFirmwareErase.Progress = ProgressTotal
            If Not CmdFirmwareErase.Execute() Then
                Return False
            End If
            ProgressIncrement()

            'Erase assets
            For Each Segment In Segments
                If Segment.Category = ESegmentCategory.Asset Then
                    CmdAssetErase.SectorStart = Segment.Sector
                    CmdAssetErase.SectorCount = 1
                    If Not CmdAssetErase.Execute() Then
                        Return False
                    End If
                    ProgressIncrement()
                End If
            Next

            'Program
            OutputMessage("Writing firmware")
            For Each Segment In Segments
                'If Segment.Category = ESegmentCategory.Firmware Then
                If Not Segment.Program(Me) Then
                    Return False
                End If
                'End If
            Next

            'Exit programming
            OutputMessage("Leaving programming mode")
            CmdLeave.CalculatedCRC = CRCGet()
            If Not CmdLeave.Execute() Then
                Return False
            End If

            Return True
        End If
    End Function

    '----------------------------------------------------------------------------------------------
    Public Sub CRCClear()
        CalculatedCRC = &HFFFF
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Sub CRCCalculate(Data As Byte(), Length As UInt16)
        Dim x As UInt16
        Dim i As Integer = 0
        Dim Tmp As UInt16

        While Length > 0
            'x = FMK_CalculatedCRC >> 8 ^ *Data++;
            x = CalculatedCRC
            x >>= 8
            x = x And &HFF
            x = x Xor Data(i)
            i += 1

            'x ^= (x >> 4);
            x = x Xor (x >> 4 And &HF)

            'FMK_CalculatedCRC = (FMK_CalculatedCRC << 8) ^ ((uint16_t)(x << 12)) ^ ((uint16_t)(x << 5)) ^ ((uint16_t)x);
            CalculatedCRC <<= 8
            Tmp = x << 12
            CalculatedCRC = CalculatedCRC Xor Tmp
            Tmp = x << 5
            CalculatedCRC = CalculatedCRC Xor Tmp
            CalculatedCRC = CalculatedCRC Xor x

            Length -= 1
        End While
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function CRCGet() As UInt16
        Return CalculatedCRC
    End Function

    '----------------------------------------------------------------------------------------------
    Private Function GetTransferCount(ByRef Segment As CSegment, PayloadSize As Integer) As Integer
        Return 0
    End Function

End Class
