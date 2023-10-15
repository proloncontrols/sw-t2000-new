
Public Class CCommandFirmwareWrite
    Private Base As CCom

    Enum EPos
        '0 = Command
        Firmware = 1
        Address0
        Address1
        Address2
        Address3
        LengthLo
        LengthHi
        Size
        Data = Size
    End Enum

    '----------------------------------------------------------------------------------------------
    Public ReadOnly Property PayloadSize As Integer
        Get
            Return Base.PayloadSize
        End Get
    End Property

    '----------------------------------------------------------------------------------------------
    Public Property Firmware As Byte
        Get
            Return Base.RxBuffer(Base.CommandPositionOffset + EPos.Firmware)
        End Get
        Set(value As Byte)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Firmware) = value
        End Set
    End Property

    Public Property Address As UInt32
        Get
            Dim Res As UInt32 = Base.RxBuffer(Base.CommandPositionOffset + EPos.Address3)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.Address2)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.Address1)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.Address0)
            Return Res
        End Get
        Set(value As UInt32)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Address3) = value >> 24 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Address2) = value >> 16 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Address1) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Address0) = value And &HFF
        End Set
    End Property

    Public Property Length As UInt16
        Get
            Dim Res As UInt16 = Base.RxBuffer(Base.CommandPositionOffset + EPos.LengthHi)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.LengthLo)
            Return Res
        End Get
        Set(value As UInt16)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.LengthHi) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.LengthLo) = value And &HFF
        End Set
    End Property

    Public Property Data(Idx As Integer) As Byte
        Get
            Return Base.TxBuffer(Base.CommandPositionOffset + EPos.Data + Idx)
        End Get
        Set(value As Byte)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Data + Idx) = value
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Sub New(ByRef NewBase As CCom)
        Base = NewBase
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function Execute() As Boolean
        Dim Res As Boolean = False

        Base.Execute(CMD_FIRMWARE_WRITE, EPos.Size)
        Base.TxLength += Base.PayloadSize
        Base.AppendChecksum(CRC16(Base.TxBuffer, 0, Base.TxLength))
        Base.AttemptTimeout = TXRX_TIMEOUT
        Return Base.TxRx()
    End Function

End Class
