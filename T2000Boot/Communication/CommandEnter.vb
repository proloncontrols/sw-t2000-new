
Public Class CCommandEnter
    Private Base As CCom

    Enum EPos
        '0 = Command
        Size = 1
    End Enum

    '----------------------------------------------------------------------------------------------
    Public Sub New(ByRef NewBase As CCom)
        Base = NewBase
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function Execute() As Boolean
        Dim Res As Boolean = False

        Base.Execute(CMD_ENTER, EPos.Size)
        Base.AppendChecksum(CRC16(Base.TxBuffer, 0, Base.TxLength))
        Base.AttemptTimeout = ENTER_TIMEOUT
        Return Base.TxRx()
    End Function

End Class
