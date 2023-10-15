
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
'         File: FileHex.vb
'        Date : -----------
'       Author: Jean-Francois Barriere
'-----------------------------------------------------------------------------
'  Description: HEX files manipulation class
'=============================================================================


'------------------------------------------------------------------------------
'  HEX files classes hierarchy
'
'   -------
'  | CFile |
'   -------
'     |   ---------- 
'     \--| CFileHex |
'         ----------
'           |   ---------------
'           \--| CFileHexIntel |
'               ---------------
'
'------------------------------------------------------------------------------


Public Class CFileHexSegment

    Public Address As UInt32
    Public DataCount As Integer
    Public Data(65536) As Byte

End Class


Public Class CFileHex
    Inherits CFile

    Public Segment As CFileHexSegment = New CFileHexSegment

    Public Overloads Sub Close()
        MyBase.Close()
    End Sub

End Class


Public Class CExceptionFileHex
    Inherits CException

    Public Sub New(ClassName As String, FunctionName As String, Message As String)
        m_Exception = "HEX file"
        m_ClassName = ClassName
        m_FunctionName = FunctionName
        m_Message = Message
    End Sub

End Class
