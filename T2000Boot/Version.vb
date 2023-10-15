
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
'         File: Version.vb
'        Date : July 14 2022
'       Author: Jean-Francois Barriere
'------------------------------------------------------------------------------
'  Description: Version manipulation class
'==============================================================================


Public Class CVersion

    '-----| DATA |-------------------------------------------------------------
    Private m_Text As String
    Private m_Major As Integer
    Private m_Minor As Integer
    Private m_Build As Integer


    '-----| PROPERTIES |-------------------------------------------------------
    Public ReadOnly Property Text As String
        Get
            Return m_Text
        End Get
    End Property
    Public ReadOnly Property Major As Integer
        Get
            Return m_Major
        End Get
    End Property
    Public ReadOnly Property Minor As Integer
        Get
            Return m_Minor
        End Get
    End Property
    Public ReadOnly Property Build As Integer
        Get
            Return m_Build
        End Get
    End Property


    '-----| OPERATORS |--------------------------------------------------------
    Public Shared Operator =(V1 As CVersion, V2 As CVersion)
        If V1.m_Major <> V2.m_Major Then
            Return False
        End If
        If V1.m_Minor <> V2.m_Minor Then
            Return False
        End If
        If V1.m_Build <> V2.m_Build Then
            Return False
        End If
        Return True
    End Operator

    Public Shared Operator <>(V1 As CVersion, V2 As CVersion)
        Return Not (V1 = V2)
    End Operator

    Public Shared Operator >(V1 As CVersion, V2 As CVersion)
        If V1.m_Major > V2.m_Major Then
            Return True
        ElseIf V1.m_Major = V2.m_Major Then
            If V1.m_Minor > V2.m_Minor Then
                Return True
            ElseIf V1.m_Minor = V2.m_Minor Then
                If V1.m_Build > V2.m_Build Then
                    Return True
                End If
            End If
        End If
        Return False
    End Operator

    Public Shared Operator <(V1 As CVersion, V2 As CVersion)
        If V1.m_Major < V2.m_Major Then
            Return True
        ElseIf V1.m_Major = V2.m_Major Then
            If V1.m_Minor < V2.m_Minor Then
                Return True
            ElseIf V1.m_Minor = V2.m_Minor Then
                If V1.m_Build < V2.m_Build Then
                    Return True
                End If
            End If
        End If
        Return False
    End Operator

    Public Shared Operator >=(V1 As CVersion, V2 As CVersion)
        If (V1 = V2) Or (V1 > V2) Then
            Return True
        End If
        Return False
    End Operator

    Public Shared Operator <=(V1 As CVersion, V2 As CVersion)
        If (V1 = V2) Or (V1 < V2) Then
            Return True
        End If
        Return False
    End Operator


    '-----| CONSTRUCTION |-----------------------------------------------------
    Public Sub New()
        Assign(0, 0, 0)
    End Sub

    Public Sub New(Text As String)
        Assign(Text)
    End Sub

    Public Sub New(Major As Integer, Minor As Integer, Build As Integer)
        Assign(Major, Minor, Build)
    End Sub

    Public Sub New(Version As CVersion)
        Assign(Version)
    End Sub


    '-----| METHODS |----------------------------------------------------------
    Public Sub Assign(Version As CVersion)
        m_Major = Version.m_Major
        m_Minor = Version.m_Minor
        m_Build = Version.m_Build
        m_Text = Version.m_Text
    End Sub

    Public Sub Assign(Major As Integer, Minor As Integer, Build As Integer)
        m_Major = Major
        m_Minor = Minor
        m_Build = Build
        m_Text = m_Major & "." & m_Minor.ToString("D2") & "." & m_Build.ToString("D3")
    End Sub

    Public Sub Assign(Text As String)
        Assign(0, 0, 0)
        Dim Parts As String() = Text.Split({"."c}, 3)
        If Parts.Count = 3 Then
            Assign(Parts(0), Parts(1), Parts(2))
        End If
    End Sub

End Class
