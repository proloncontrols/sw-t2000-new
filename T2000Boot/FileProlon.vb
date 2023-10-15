
'==============================================================================
'         PPPPPPP   RRRRRRR    OOOOOO   LL      OOOOOO   NN    NN
'         PP    PP  RR    RR  OO    OO  LL     OO    OO  NNN   NN
'         PP    PP  RR    RR  OO    OO  LL     OO    OO  NN N  NN
'         PPPPPPP   RRRRRRR   OO    OO  LL     OO    OO  NN NN NN
'         PP        RR  RR    OO    OO  LL     OO    OO  NN  N NN
'         PP        RR   RR   OO    OO  LL     OO    OO  NN   NNN
'         PP        RR    RR   OOOOOO   LLLLLL  OOOOOO   NN    NN

'                        (c) Copyright  2022-2022
'------------------------------------------------------------------------------
'         File: FileProlon.vb
'         Date: July 15 2022
'       Author: Jean-Francois Barriere
'------------------------------------------------------------------------------
'  Description: Prolon files manipulation base class
'==============================================================================
Imports System.IO


Public MustInherit Class CFileProlon
    Inherits CFile

    '-----| CONSTANTS |--------------------------------------------------------
    Private Const SIGNATURE As String = "ProlonFileSignature"


    '-----| CLASSES |----------------------------------------------------------
    Public Class CExceptionFileProlon
        Inherits CException
        Public Sub New(ClassName As String, FunctionName As String, Message As String)
            m_Exception = "Prolon file"
            m_ClassName = ClassName
            m_FunctionName = FunctionName
            m_Message = Message
        End Sub
    End Class

    Public Class CHeader
        Public Signature As String = Nothing
        Public Description As String = Nothing
        Public Version As CVersion = New CVersion
    End Class


    '-----| DATA |-------------------------------------------------------------
    Private m_Description As String
    Protected m_Header As CHeader = New CHeader


    '-----| CONSTRUCTION |-----------------------------------------------------
    Public Sub New(Description As String)
        m_Description = Description
    End Sub


    '-----| METHODS-PUBLIC |---------------------------------------------------
    Public Sub Load()
        'Open(m_FileName)
        ReadHeader()
    End Sub

    Public Sub Save()
        'Open(m_FileName)
        WriteHeader()
    End Sub

    Public Sub SaveAs(FileName As String)
        'Open(FileName)
        WriteHeader()
    End Sub


    '-----| METHODS-PRIVATE |--------------------------------------------------
    Private Sub ReadHeader()
        'Initialize header with defaults
        m_Header.Description = String.Empty
        m_Header.Version.Assign(0, 0, 0)

        'Open specified file for reading, read file header and give control back to the calling derived class in order to terminate its specific reading
        m_SR = New StreamReader(File.OpenRead(m_FileName))
        m_Header.Signature = m_SR.ReadLine()
        If m_Header.Signature <> SIGNATURE Then
            Throw New CExceptionFileProlon(Me.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name, "Not a Prolon file")
        End If

        m_Header.Description = m_SR.ReadLine()
        If m_Header.Description <> m_Description Then
            Throw New CExceptionFileProlon(Me.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name, "Prolon file description mismatch")
        End If

        m_Header.Version.Assign(m_SR.ReadLine())
    End Sub

    Private Sub WriteHeader()
        'In Prolon text file mode, always force creation mode when writing (delete existing if any then create again)
        If File.Exists(m_FileName) Then
            File.Delete(m_FileName)
        End If

        'Open specified file for writing, write file header and give control back to the calling derived class in order to terminate its specific writing
        m_SW = New StreamWriter(File.OpenWrite(m_FileName))
        m_SW.WriteLine(SIGNATURE)
        m_SW.WriteLine(m_Description)
        m_SW.WriteLine(m_Header.Version.Text)
    End Sub

End Class
