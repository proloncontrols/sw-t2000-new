
Public Class CCommandLeave
    Private Base As CCom

    Enum EPos
        '0 = Command
        CalculatedCrcLo = 1
        CalculatedCrcHi
        Size
    End Enum

    '----------------------------------------------------------------------------------------------
    Public Property CalculatedCRC As UInt16
        Get
            Dim Res As UInt16 = Base.RxBuffer(Base.CommandPositionOffset + EPos.CalculatedCrcHi)
            Res <<= 8
            Res += Base.RxBuffer(Base.CommandPositionOffset + EPos.CalculatedCrcLo)
            Return Res
        End Get
        Set(value As UInt16)
            Base.TxBuffer(Base.CommandPositionOffset + EPos.CalculatedCrcHi) = value >> 8 And &HFF
            Base.TxBuffer(Base.CommandPositionOffset + EPos.CalculatedCrcLo) = value And &HFF
        End Set
    End Property

    '----------------------------------------------------------------------------------------------
    Public Sub New(ByRef NewBase As CCom)
        Base = NewBase
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function Execute() As Boolean
        Dim Res As Boolean = False

        Base.Execute(CMD_LEAVE, EPos.Size)
        Base.AppendChecksum(CRC16(Base.TxBuffer, 0, Base.TxLength))
        Base.AttemptTimeout = LEAVE_TIMEOUT
        Return Base.TxRx()
    End Function

End Class
