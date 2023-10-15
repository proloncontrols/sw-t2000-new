
Imports System.IO

Public Module MIntelHex
    Public Const FILE_STREAM_BUFFER_SIZE As Integer = 4096
    Public Const SEGMENT_SIZE As Integer = 65536

    '----------------------------------------------------------------------------------------------
    Public Const RECORD_TYPE_DATA As Integer = 0
    Public Const RECORD_TYPE_END_OF_FILE As Integer = 1
    Public Const RECORD_TYPE_EXTENDED_SEGMENT_ADDRESS As Integer = 2
    Public Const RECORD_TYPE_START_SEGMENT_ADDRESS As Integer = 3
    Public Const RECORD_TYPE_EXTENDED_LINEAR_ADDRESS As Integer = 4
    Public Const RECORD_TYPE_START_LINEAR_ADDRESS As Integer = 5
End Module



Public Class CFileIntelHex

    Private m_Stream As FileStream
    Private m_Record As CIntelHexRecord

    '----------------------------------------------------------------------------------------------
    Public HexSegments As List(Of CIntelHexSegment)

    '----------------------------------------------------------------------------------------------
    Public Sub New()
        m_Stream = Nothing
        m_Record = New CIntelHexRecord
        HexSegments = New List(Of CIntelHexSegment)
    End Sub

    '----------------------------------------------------------------------------------------------
    Function LoadHexFile(FileName As String) As Boolean
        Dim Done As Boolean
        Dim CurRec As String
        Dim CurRecStreamPosition As UInt64

        CloseHexFile()

        If String.IsNullOrEmpty(FileName) Then
            Return False
        End If

        Try
            m_Stream = New FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.None, FILE_STREAM_BUFFER_SIZE)
        Catch ex As Exception
            Return False
        End Try

        If m_Stream Is Nothing Then
            Return False
        End If

        Done = False
        Do
            CurRecStreamPosition = m_Stream.Position
            CurRec = ReadLine()
            If CurRec Is Nothing Then
                Done = True
            Else
                m_Record.Decode(CurRec)
                If m_Record.Type = RECORD_TYPE_END_OF_FILE Then
                    Done = True
                ElseIf m_Record.Type = RECORD_TYPE_EXTENDED_LINEAR_ADDRESS Then
                    HexSegments.Add(New CIntelHexSegment(m_Record.Segment, CurRecStreamPosition))
                End If
            End If
        Loop Until Done

        Return True
    End Function

    '----------------------------------------------------------------------------------------------
    Public Sub CloseHexFile()
        HexSegments.Clear()
        If m_Stream IsNot Nothing Then
            m_Stream.Dispose()
        End If
        m_Stream = Nothing
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Function ReadLine() As String
        Dim Ch As Integer
        Dim Line As String = Nothing

        Do
            Ch = m_Stream.ReadByte()
            If Ch = -1 Then
                Return Nothing
            End If
            If Chr(Ch) <> vbCrLf Then
                Line += Chr(Ch)
            End If
        Loop Until Chr(Ch) = vbCr
        Line += Chr(m_Stream.ReadByte())   'Get line feed character
        Return Line
    End Function

    '----------------------------------------------------------------------------------------------
    Public Function GetSegment(Address As UInt32) As CIntelHexSegment
        For Each Segment In HexSegments
            If Segment.Address = Address Then
                Return Segment
            End If
        Next
        Return Nothing
    End Function

    '----------------------------------------------------------------------------------------------
    Public Function LoadHexSegment(ByRef Segment As CIntelHexSegmentData, Address As UInt32) As Boolean
        Dim Done As Boolean
        Dim CurRec As String
        Dim CurSegment As CIntelHexSegment = GetSegment(Address)

        'Retrieve HEX file segment from specified address
        If CurSegment Is Nothing Then
            Return False
        End If

        'Goto specified segment stream position
        m_Stream.Seek(CurSegment.Position, SeekOrigin.Begin)

        'Read record at position
        CurRec = ReadLine()
        If CurRec Is Nothing Then
            Return False
        End If

        'Decode and validate if record is of expected type
        m_Record.Decode(CurRec)
        If m_Record.Type <> RECORD_TYPE_EXTENDED_LINEAR_ADDRESS Then
            Return False
        End If

        'Decode and validate if address matches that of specified segment
        If m_Record.Segment <> CurSegment.Address Then
            Return False
        End If

        'We have the correct segment, initialize destination
        Segment.Address = m_Record.Segment
        For i As Integer = 0 To SEGMENT_SIZE - 1
            Segment.Data(i) = &HFF
        Next

        'Extract actual segment data from HEX file into destination buffer
        Done = False
        Do
            CurRec = ReadLine()
            If CurRec Is Nothing Then
                Return False
            Else
                m_Record.Decode(CurRec)
                If m_Record.Type = RECORD_TYPE_EXTENDED_LINEAR_ADDRESS Or m_Record.Type = RECORD_TYPE_END_OF_FILE Then
                    Done = True
                ElseIf m_Record.Type = RECORD_TYPE_DATA Then
                    For i = 0 To m_Record.Length - 1
                        Segment.Data(m_Record.Offset + i) = m_Record.Data(i)
                    Next
                End If
            End If
        Loop Until Done

        Return True
    End Function

End Class



Public Class CIntelHexRecord
    Const POS_SOR As Integer = 0      'Start position in text record: Start Of Record
    Const POS_LENGTH As Integer = 1   'Start position in text record: Number of data bytes in record
    Const POS_OFFSET As Integer = 3   'Start position in text record: Offset of data bytes in record
    Const POS_TYPE As Integer = 7     'Start position in text record: Type of record
    Const POS_DATA As Integer = 9     'Start position in text record: Data payload in record

    '----------------------------------------------------------------------------------------------
    Public Length As Byte
    Public Offset As UInt16
    Public Type As Byte
    Public Segment As UInt32
    Public Data(255) As Byte
    Public Checksum As Byte
    Public Record As String

    '----------------------------------------------------------------------------------------------
    Public Function Decode(Rec As String) As Boolean
        Dim Count As Integer
        Dim CalculatedChecksum As UInt16

        If String.IsNullOrEmpty(Rec) Or Rec(POS_SOR) <> ":" Then
            Return False
        End If

        Length = Decode8bit(Rec, POS_LENGTH)
        Offset = Decode16bit(Rec, POS_OFFSET)
        Type = Decode8bit(Rec, POS_TYPE)
        For Count = 0 To Length - 1
            Data(Count) = Decode8bit(Rec, POS_DATA + (Count * 2))
        Next
        Checksum = Decode8bit(Rec, POS_DATA + (Length * 2))

        CalculatedChecksum = Length
        CalculatedChecksum += (Offset >> 8) And &HFF
        CalculatedChecksum += Offset And &HFF
        CalculatedChecksum += Type
        For Count = 0 To Length - 1
            CalculatedChecksum += Data(Count)
        Next
        CalculatedChecksum += Checksum
        CalculatedChecksum = CalculatedChecksum And &HFF
        If CalculatedChecksum <> 0 Then
            Return False
        End If

        Segment = 0
        If Type = RECORD_TYPE_EXTENDED_LINEAR_ADDRESS Then
            Segment = Data(0)
            Segment <<= 8
            Segment += Data(1)
            Segment <<= 16
        End If

        Record = Rec
        Return True
    End Function

    '----------------------------------------------------------------------------------------------
    Private Function Decode8bit(InString As String, FromPosition As Integer) As Byte
        Dim Res As Byte

        Res = ConvertByte(InString(FromPosition)) * &H10
        Res += ConvertByte(InString(FromPosition + 1))

        Return Res
    End Function

    '----------------------------------------------------------------------------------------------
    Private Function Decode16bit(InString As String, FromPosition As Integer) As UInt16
        Dim Res As UInt16

        Res = ConvertByte(InString(FromPosition)) * &H1000
        Res += ConvertByte(InString(FromPosition + 1)) * &H100
        Res += ConvertByte(InString(FromPosition + 2)) * &H10
        Res += ConvertByte(InString(FromPosition + 3))

        Return Res
    End Function

    '----------------------------------------------------------------------------------------------
    Private Function ConvertByte(Ch As Char) As Integer
        Dim Tmp As Integer = Convert.ToByte(Ch)

        If Tmp >= &H30 And Tmp <= &H39 Then
            Return Tmp - &H30
        End If
        Return Tmp - &H37
    End Function
End Class



Public Class CIntelHexSegment
    Public Address As UInt32
    Public Position As UInt64

    Public Sub New(NewAddress As UInt32, NewPosition As UInt32)
        Address = NewAddress
        Position = NewPosition
    End Sub
End Class



Public Class CIntelHexSegmentData
    Public Address As UInt32
    Public Data(SEGMENT_SIZE) As Byte
End Class
