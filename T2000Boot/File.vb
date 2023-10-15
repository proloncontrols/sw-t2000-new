
'=============================================================================
'         PPPPPPP   RRRRRRR    OOOOOO   LL      OOOOOO   NN    NN
'         PP    PP  RR    RR  OO    OO  LL     OO    OO  NNN   NN
'         PP    PP  RR    RR  OO    OO  LL     OO    OO  NN N  NN
'         PPPPPPP   RRRRRRR   OO    OO  LL     OO    OO  NN NN NN
'         PP        RR  RR    OO    OO  LL     OO    OO  NN  N NN
'         PP        RR   RR   OO    OO  LL     OO    OO  NN   NNN
'         PP        RR    RR   OOOOOO   LLLLLL  OOOOOO   NN    NN
'
'                        (c) Copyright  2022-2023
'-----------------------------------------------------------------------------
'         File: File.vb
'        Date : -----------
'       Author: Jean-Francois Barriere
'-----------------------------------------------------------------------------
'  Description: Files manipulation class
'=============================================================================
Imports System.IO


'------------------------------------------------------------------------------
'  File classes hierarchy
'
'   -------
'  | CFile |                     <-- Holds currently opened file name
'   -------                          Holds/manipulates text reader/writer streams
'     |   -------------
'     |--| CFileProlon |         <-- Holds/manipulates Prolon's files standard header
'     |   -------------
'     |     |   -----------
'     |     |--| CFileAAAA |     <-- Prolon text file type AAAA
'     |     |   -----------
'     |     |   -----------
'     |     |--| CFileBBBB |     <-- Prolon text file type BBBB
'     |     |   -----------
'     |     |   -----------
'     |     \--| CFileCCCC |     <-- Prolon text file type CCCC
'     |         -----------
'     |              .
'     |              .
'     |              .
'     |
'     |   ----------
'     \--| CFileHex |            <-- Manipulates HEX files (Intel, Motorola...)
'         ----------
'              .
'              .
'              .
'
'------------------------------------------------------------------------------


Public Enum EFileMode
    FileModeRead = 0
    FileModeWrite
    FileModeEnd
End Enum


Public MustInherit Class CFile

    '-----| DATA |-------------------------------------------------------------
    Protected m_FileName As String = String.Empty     'Currently opened file name
    Protected m_SR As StreamReader = Nothing          'Reader/writer streams used for text files
    Protected m_SW As StreamWriter = Nothing          '


    '-----| PROPERTIES |-------------------------------------------------------
    Public ReadOnly Property FileName As String
        Get
            Return m_FileName
        End Get
    End Property


    '-----| METHODS |----------------------------------------------------------
    Public Sub Open(Mode As EFileMode, FileName As String)
        Close()
        If String.IsNullOrEmpty(FileName) Then
            Throw New CExceptionInvalidArgument(Me.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name, "File name is empty or NULL")
        End If
        If Mode >= EFileMode.FileModeEnd Then
            Throw New CExceptionInvalidArgument(Me.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name, "Invalid file mode")
        End If
        If Mode = EFileMode.FileModeRead Then
            m_SR = New StreamReader(FileName)
        End If
        If Mode = EFileMode.FileModeWrite Then
            m_SW = New StreamWriter(FileName)
        End If
        m_FileName = FileName
    End Sub

    Public Sub Close()
        If m_SR IsNot Nothing Then
            m_SR.Close()
            m_SR.Dispose()
            m_SR = Nothing
        End If
        If m_SW IsNot Nothing Then
            m_SW.Close()
            m_SW.Dispose()
            m_SW = Nothing
        End If
        m_FileName = String.Empty
    End Sub

End Class
