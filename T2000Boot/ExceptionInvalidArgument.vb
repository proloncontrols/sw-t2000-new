
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
'         File: ExceptionInvalidArgument.vb
'        Date : July 14 2022
'       Author: Jean-Francois Barriere
'------------------------------------------------------------------------------
'  Description: Invalid argument exception class
'==============================================================================


Public Class CExceptionInvalidArgument
    Inherits CException

    '-----| CONSTRUCTION |-----------------------------------------------------
    Public Sub New(ClassName As String, FunctionName As String, Message As String)
        m_Exception = "Invalid argument"
        m_ClassName = ClassName
        m_FunctionName = FunctionName
        m_Message = Message
    End Sub

End Class
