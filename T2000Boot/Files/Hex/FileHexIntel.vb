
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
'         File: FileHexIntel.vb
'        Date : -----------
'       Author: Jean-Francois Barriere
'-----------------------------------------------------------------------------
'  Description: Intel HEX file format class
'=============================================================================
Imports System.IO


Public Class CFileHexIntel
    Inherits CFileHex

    Const REC_POS_SOR As Integer = 0            'Start position index in text record: Start Of Record
    Const REC_POS_LENGTH As Integer = 1         'Start position index in text record: Number of data bytes in record
    Const REC_POS_OFFSET As Integer = 3         'Start position index in text record: Offset of data bytes in record
    Const REC_POS_TYPE As Integer = 7           'Start position index in text record: Type of record
    Const REC_POS_DATA As Integer = 9           'Start position index in text record: Data payload in record

    Const REC_TYPE_DATA As Integer = 0
    Const REC_TYPE_END_OF_FILE As Integer = 1
    'Const REC_TYPE_EXTENDED_SEGMENT_ADDRESS As Integer = 2
    'Const REC_TYPE_START_SEGMENT_ADDRESS As Integer = 3
    Const REC_TYPE_EXTENDED_LINEAR_ADDRESS As Integer = 4
    'Const REC_TYPE_START_LINEAR_ADDRESS As Integer = 5



    Private m_RecLength As Byte
    Private m_RecOffset As UInt16
    Private m_RecType As Byte
    Private m_RecData(255) As Byte
    Private m_RecChecksum As Byte

    Private m_CurRec As String



    Public Sub New(FileName As String)
        Open(EFileMode.FileModeRead, FileName)
    End Sub


    Public Function GetFirstSegment() As Boolean
        m_SR.BaseStream.Seek(0, SeekOrigin.Begin)
        m_CurRec = m_SR.ReadLine()
        DecodeRecord(m_CurRec)
        Return GetNextSegment()
    End Function


    Public Function GetNextSegment() As Boolean
        Dim i As Integer
        Dim SegmentOffset As UInt16
        Dim FirstSegment As Boolean = True

        If m_RecType = REC_TYPE_END_OF_FILE Then
            Return False
        End If

        Do

            If m_RecType = REC_TYPE_EXTENDED_LINEAR_ADDRESS Then
                If FirstSegment Then
                    Segment.Address = m_RecData(0)
                    Segment.Address <<= 8
                    Segment.Address += m_RecData(1)
                    Segment.Address <<= 16
                    m_CurRec = m_SR.ReadLine()
                    DecodeRecord(m_CurRec)
                    Segment.Address += m_RecOffset
                    Segment.DataCount = 0
                    SegmentOffset = m_RecOffset
                    For i = 0 To Segment.Data.Length - 1
                        Segment.Data(i) = &HFF
                    Next
                    FirstSegment = False
                Else
                    Return True
                End If
            End If

            If m_RecType = REC_TYPE_DATA Then
                For i = 0 To m_RecLength - 1
                    Segment.Data(m_RecOffset - SegmentOffset + i) = m_RecData(i)
                Next
                Segment.DataCount += m_RecLength
            End If

            m_CurRec = m_SR.ReadLine()
            If m_CurRec IsNot Nothing Then
                DecodeRecord(m_CurRec)
                If m_RecType = REC_TYPE_END_OF_FILE Then
                    Return True
                End If
            End If

        Loop Until m_CurRec Is Nothing

        Return False

    End Function


    Private Sub DecodeRecord(Rec As String)
        Dim Count As Integer
        Dim Checksum As UInt16

        If String.IsNullOrEmpty(Rec) Or Rec(REC_POS_SOR) <> ":" Then
            DecodeRecordError()
        End If

        m_RecLength = Decode8bit(Rec, REC_POS_LENGTH)
        m_RecOffset = Decode16bit(Rec, REC_POS_OFFSET)
        m_RecType = Decode8bit(Rec, REC_POS_TYPE)
        For Count = 0 To m_RecLength - 1
            m_RecData(Count) = Decode8bit(Rec, REC_POS_DATA + (Count * 2))
        Next
        m_RecChecksum = Decode8bit(Rec, REC_POS_DATA + (m_RecLength * 2))

        Checksum = m_RecLength
        Checksum += (m_RecOffset >> 8) And &HFF
        Checksum += m_RecOffset And &HFF
        Checksum += m_RecType
        For Count = 0 To m_RecLength - 1
            Checksum += m_RecData(Count)
        Next
        Checksum += m_RecChecksum
        Checksum = Checksum And &HFF
        If Checksum <> 0 Then
            DecodeRecordError()
        End If
    End Sub


    Private Function Decode8bit(InString As String, FromPosition As Integer) As Byte
        Dim Res As Byte

        Try
            Res = ConvertByte(InString(FromPosition)) * &H10
            Res += ConvertByte(InString(FromPosition + 1))
        Catch ex As Exception
            DecodeRecordError()
        End Try

        Return Res
    End Function


    Private Function Decode16bit(InString As String, FromPosition As Integer) As UInt16
        Dim Res As UInt16

        Try
            Res = ConvertByte(InString(FromPosition)) * &H1000
            Res += ConvertByte(InString(FromPosition + 1)) * &H100
            Res += ConvertByte(InString(FromPosition + 2)) * &H10
            Res += ConvertByte(InString(FromPosition + 3))
        Catch ex As Exception
            DecodeRecordError()
        End Try

        Return Res
    End Function


    Private Function ConvertByte(Ch As Char) As Integer
        Dim Tmp As Integer = Convert.ToByte(Ch)

        If Tmp >= &H30 And Tmp <= &H39 Then
            Return Tmp - &H30
        End If
        Return Tmp - &H37
    End Function


    Private Sub DecodeRecordError()
        Throw New CExceptionFileHex(Me.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name, "Irregular record format")
    End Sub

End Class
