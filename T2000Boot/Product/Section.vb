
Public Enum ESectionCategory
    Firmware
    Asset
    Data
End Enum

Public Class CSection
    Public Category As ESectionCategory
    Public Origin As UInt32
    Public Length As UInt32
    Public SectorSize As UInt32

    '----------------------------------------------------------------------------------------------
    Public Sub New(NewCategory As ESectionCategory, NewOrigin As UInt32, NewLength As UInt32, NewSectorSize As UInt32)
        Category = NewCategory
        Origin = NewOrigin
        Length = NewLength
        SectorSize = NewSectorSize
    End Sub
End Class

Public Class CSections
    Inherits List(Of CSection)

    Default Public Shadows ReadOnly Property Sections(Idx As Integer) As CSection
        Get
            Return Item(Idx)
        End Get
    End Property

    '----------------------------------------------------------------------------------------------
    Public Shadows Function GetSection(Address As UInt32) As CSection
        For i As Integer = 0 To Count - 1
            If Address >= Sections(i).Origin And Address < (Sections(i).Origin + Sections(i).Length) Then
                Return Sections(i)
            End If
        Next
        Return Nothing
    End Function
End Class
