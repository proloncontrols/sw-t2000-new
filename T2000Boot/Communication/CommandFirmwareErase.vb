
Imports T2000Boot.CCom

Public Class CCommandFirmwareErase
    Private Base As CCom

    Enum EPos
        '0 = Command
        Firmware = 1
        Progress3
        Progress2
        Progress1
        Progress0
        Size
    End Enum

    '----------------------------------------------------------------------------------------------
    Public Property Firmware As Byte
        Get
            Return Base.RxBuffer(Base.CommandPositionOffset + EPos.Firmware)
        End Get
        Set(value As Byte)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Firmware) = value
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Property Progress As UInt32
        Get
            Dim Res As UInt32 = Base.RxBuffer(Base.CommandPositionOffset + EPos.Progress0)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.Progress1)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.Progress2)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.Progress3)
            Return Res
        End Get
        Set(value As UInt32)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Progress0) = value >> 24 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Progress1) = value >> 16 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Progress2) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Progress3) = value And &HFF
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Sub New(ByRef NewBase As CCom)
        Base = NewBase
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function Execute() As Boolean
        Dim Res As Boolean = False

        Base.Execute(CMD_FIRMWARE_ERASE, EPos.Size)
        Base.AppendChecksum(CRC16(Base.TxBuffer, 0, Base.TxLength))
        Base.AttemptTimeout = ERASE_TIMEOUT
        Return Base.TxRx()
    End Function

End Class
