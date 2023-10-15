
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
'         File: Exception.vb
'        Date : July 14 2022
'       Author: Jean-Francois Barriere
'------------------------------------------------------------------------------
'  Description: Exceptions base class
'==============================================================================


Public MustInherit Class CException
    Inherits Exception

    '-----| DATA |-------------------------------------------------------------
    Protected m_Exception As String
    Protected m_ClassName As String
    Protected m_FunctionName As String
    Protected m_Message As String


    '-----| PROPERTIES |-------------------------------------------------------
    Public ReadOnly Property Exception As String
        Get
            Return m_Exception
        End Get
    End Property
    Public ReadOnly Property ClassName As String
        Get
            Return m_ClassName
        End Get
    End Property
    Public ReadOnly Property FunctionName As String
        Get
            Return m_FunctionName
        End Get
    End Property
    Public Overrides ReadOnly Property Message As String
        Get
            Return m_Message
        End Get
    End Property


    '-----| METHODS |----------------------------------------------------------
    Public Overloads Function ToString() As String
        Return "***** EXCEPTION ***** [" & m_Exception & "] in " & m_ClassName & "." & m_FunctionName & " '" & m_Message & "'"
    End Function

End Class
