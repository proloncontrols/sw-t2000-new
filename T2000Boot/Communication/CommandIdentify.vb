
Public Class CCommandIdentify
    Private Base As CCom

    Enum EPos
        '0 = Command
        Firmware = 1
        Family
        Product
        HardwareVersionLo
        HardwareVersionHi
        BootloaderVersionLo
        BootloaderVersionHi
        ApplicationVersionLo
        ApplicationVersionHi
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

    Public Property Family As Byte
        Get
            Return Base.RxBuffer(Base.CommandPositionOffset + EPos.Family)
        End Get
        Set(value As Byte)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Family) = value
        End Set
    End Property

    Public Property Product As Byte
        Get
            Return Base.RxBuffer(Base.CommandPositionOffset + EPos.Product)
        End Get
        Set(value As Byte)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.Product) = value
        End Set
    End Property

    Public Property HardwareVersion As UInt16
        Get
            Dim Res As UInt16 = Base.RxBuffer(Base.CommandPositionOffset + EPos.HardwareVersionHi)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.HardwareVersionLo)
            Return Res
        End Get
        Set(value As UInt16)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.HardwareVersionHi) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.HardwareVersionLo) = value And &HFF
        End Set
    End Property

    Public Property BootloaderVersion As UInt16
        Get
            Dim Res As UInt16 = Base.RxBuffer(Base.CommandPositionOffset + EPos.BootloaderVersionHi)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.BootloaderVersionLo)
            Return Res
        End Get
        Set(value As UInt16)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.BootloaderVersionHi) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.BootloaderVersionLo) = value And &HFF
        End Set
    End Property

    Public Property ApplicationVersion As UInt16
        Get
            Dim Res As UInt16 = Base.RxBuffer(Base.CommandPositionOffset + EPos.ApplicationVersionHi)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.ApplicationVersionLo)
            Return Res
        End Get
        Set(value As UInt16)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.ApplicationVersionHi) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.ApplicationVersionLo) = value And &HFF
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Sub New(ByRef NewBase As CCom)
        Base = NewBase
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function Execute() As Boolean
        Dim Res As Boolean = False

        Base.Execute(CMD_IDENTIFY, EPos.Size)
        Base.AppendChecksum(CRC16(Base.TxBuffer, 0, Base.TxLength))
        Base.AttemptTimeout = TXRX_TIMEOUT
        Base.OutputMessage("Identifying product")
        Return Base.TxRx()
    End Function

End Class
