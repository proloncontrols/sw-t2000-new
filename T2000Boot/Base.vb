
'
' CFileIntelHex
'  |
'  \-- CBase
'       |
'       \-- CCom
'            |
'            \-- CProduct
'                 |
'                 \-- CThermostat
'                      |
'                      \-- CT2000
'

Public Class CBase
    Inherits CFileIntelHex

    Private Owner As Form
    Public Declare Function PostMessage Lib "User32.dll" Alias "PostMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr

    Public Sub New(ByRef NewOwner As Form)
        Owner = NewOwner
    End Sub

    Public Sub PostMessage(Message As Integer, WPrarm As Integer, LParam As Integer)
        Owner.BeginInvoke(Sub() PostMessage(Owner.Handle, Message, WPrarm, LParam))
    End Sub

    Public Sub OutputMessage(Message As String)
        Owner.BeginInvoke(Sub() frmMain.lstOutput.Items.Add(Message))
    End Sub

    Public Sub UpdateButtons(State As Boolean)
        Owner.BeginInvoke(Sub() frmMain.Buttons(State))
    End Sub

    Public Sub WaitCursor()
        Owner.BeginInvoke(Sub() Owner.Cursor = Cursors.WaitCursor)
    End Sub

    Public Sub DefaultCursor()
        Owner.BeginInvoke(Sub() Owner.Cursor = Cursors.Default)
    End Sub

    Public Sub ProgressInit(Steps As Integer)
        Owner.BeginInvoke(Sub() frmMain.prgTransfer.Minimum = 0)
        Owner.BeginInvoke(Sub() frmMain.prgTransfer.Maximum = Steps)
        Owner.BeginInvoke(Sub() frmMain.prgTransfer.Step = 1)
        Owner.BeginInvoke(Sub() frmMain.prgTransfer.Value = 0)
    End Sub

    Public Sub ProgressIncrement()
        Owner.BeginInvoke(Sub() frmMain.prgTransfer.Value += 1)
    End Sub
End Class
