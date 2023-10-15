
Public Class CCommandAssetErase
    Private Base As CCom

    Enum EPos
        '0 = Command
        SectorStartLo = 1
        SectorStartHi
        SectorCountLo
        SectorCountHi
        Size
    End Enum

    '----------------------------------------------------------------------------------------------
    Public Property SectorStart As UInt16
        Get
            Dim Res As UInt16 = Base.RxBuffer(Base.CommandPositionOffset + EPos.SectorStartHi)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.SectorStartLo)
            Return Res
        End Get
        Set(value As UInt16)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.SectorStartHi) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.SectorStartLo) = value And &HFF
        End Set
    End Property

    Public Property SectorCount As UInt16
        Get
            Dim Res As UInt16 = Base.RxBuffer(Base.CommandPositionOffset + EPos.SectorCountHi)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.SectorCountLo)
            Return Res
        End Get
        Set(value As UInt16)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.SectorCountHi) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.SectorCountLo) = value And &HFF
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Sub New(ByRef NewBase As CCom)
        Base = NewBase
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function Execute() As Boolean
        Dim Res As Boolean = False

        Base.Execute(CMD_ASSET_ERASE, EPos.Size)
        Base.AppendChecksum(CRC16(Base.TxBuffer, 0, Base.TxLength))
        Base.AttemptTimeout = TXRX_TIMEOUT
        Return Base.TxRx()
    End Function

End Class
