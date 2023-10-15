
Public Enum ESegmentCategory
    Firmware
    Asset
    Data
End Enum



Public MustInherit Class CSegment
    Public Category As ESegmentCategory
    Public Sector As Integer
    Public HexSegment As CIntelHexSegment

    Public Sub New(NewCategory As ESegmentCategory, ByRef NewHexSegment As CIntelHexSegment)
        Category = NewCategory
        HexSegment = NewHexSegment
    End Sub

    Public MustOverride Function Program(ByRef Product As CProduct) As Boolean
End Class



Public Class CSegments
    Inherits List(Of CSegment)

    Default Public Shadows ReadOnly Property Segments(Idx As Integer) As CSegment
        Get
            Return Item(Idx)
        End Get
    End Property
End Class



Public Class CSegmentFirmware
    Inherits CSegment

    Public Sub New(ByRef NewHexSegment As CIntelHexSegment)
        MyBase.New(ESegmentCategory.Firmware, NewHexSegment)
    End Sub

    Public Overrides Function Program(ByRef Product As CProduct) As Boolean
        Dim SegmentOffset As UInt32
        Dim PayloadSize As Integer
        Dim PayloadIsEmpty As Boolean
        Dim HexData As CIntelHexSegmentData = New CIntelHexSegmentData
        Dim CRCData(Product.CmdFirmwareWrite.PayloadSize) As Byte

        If Not Product.LoadHexSegment(HexData, HexSegment.Address) Then
            Return False
        End If

        PayloadSize = Product.CmdFirmwareWrite.PayloadSize

        SegmentOffset = 0
        Do
            Product.CmdFirmwareWrite.Firmware = 0
            Product.CmdFirmwareWrite.Address = HexData.Address + SegmentOffset
            Product.CmdFirmwareWrite.Length = PayloadSize

            PayloadIsEmpty = True
            For i As Integer = 0 To PayloadSize - 1
                Product.CmdFirmwareWrite.Data(i) = HexData.Data(SegmentOffset + i)
                CRCData(i) = HexData.Data(SegmentOffset + i)
                If Product.CmdFirmwareWrite.Data(i) <> &HFF Then
                    PayloadIsEmpty = False
                End If
            Next

            If Not PayloadIsEmpty Then
                If Not Product.CmdFirmwareWrite.Execute() Then
                    Return False
                Else
                    Product.ProgressIncrement()
                    Product.CRCCalculate(CRCData, PayloadSize)
                    Product.PacketCount += 1
                    Product.PostMessage(WN_INT, INT_MSG_PACKET_COUNT, Product.PacketCount)
                    Dim Tmp As UInt32 = HexData.Address + SegmentOffset
                    Product.PostMessage(WN_ADD, INT_MSG_PACKET_ADDRESS, Tmp >> 1)
                    Product.PostMessage(WN_INT, INT_MSG_PACKET_SIZE, PayloadSize)
                End If
            End If

            SegmentOffset += Product.PayloadSize
        Loop Until SegmentOffset >= SEGMENT_SIZE

        Return True
    End Function
End Class



Public Class CSegmentAsset
    Inherits CSegment

    Public Sub New(ByRef NewHexSegment As CIntelHexSegment, NewSector As Integer)
        MyBase.New(ESegmentCategory.Asset, NewHexSegment)
        Sector = NewSector
    End Sub

    Public Overrides Function Program(ByRef Product As CProduct) As Boolean
        Dim SegmentOffset As UInt32
        Dim PayloadSize As Integer
        Dim PayloadIsEmpty As Boolean
        Dim HexData As CIntelHexSegmentData = New CIntelHexSegmentData
        Dim CRCData(Product.CmdAssetWrite.PayloadSize) As Byte

        If Not Product.LoadHexSegment(HexData, HexSegment.Address) Then
            Return False
        End If

        PayloadSize = Product.CmdAssetWrite.PayloadSize

        SegmentOffset = 0
        Do
            Product.CmdAssetWrite.Address = HexData.Address + SegmentOffset
            Product.CmdAssetWrite.Length = PayloadSize

            PayloadIsEmpty = True
            For i As Integer = 0 To PayloadSize - 1
                Product.CmdAssetWrite.Data(i) = HexData.Data(SegmentOffset + i)
                CRCData(i) = HexData.Data(SegmentOffset + i)
                If Product.CmdAssetWrite.Data(i) <> &HFF Then
                    PayloadIsEmpty = False
                End If
            Next

            If Not PayloadIsEmpty Then
                If Not Product.CmdAssetWrite.Execute() Then
                    Return False
                Else
                    Product.ProgressIncrement()
                    Product.CRCCalculate(CRCData, PayloadSize)
                    Product.PacketCount += 1
                    Product.PostMessage(WN_INT, INT_MSG_PACKET_COUNT, Product.PacketCount)
                    Dim Tmp As UInt32 = HexData.Address + SegmentOffset
                    Product.PostMessage(WN_ADD, INT_MSG_PACKET_ADDRESS, Tmp >> 1)
                    Product.PostMessage(WN_INT, INT_MSG_PACKET_SIZE, PayloadSize)
                End If
            End If

            SegmentOffset += Product.PayloadSize
        Loop Until SegmentOffset >= SEGMENT_SIZE

        Return True
    End Function
End Class



Public Class CSegmentData
    Inherits CSegment

    Public Sub New(ByRef NewHexSegment As CIntelHexSegment)
        MyBase.New(ESegmentCategory.Data, NewHexSegment)
    End Sub

    Public Overrides Function Program(ByRef Product As CProduct) As Boolean
        Dim SegmentOffset As UInt32
        Dim PayloadSize As Integer
        Dim PayloadIsEmpty As Boolean
        Dim HexData As CIntelHexSegmentData = New CIntelHexSegmentData
        Dim CRCData(Product.CmdAssetWrite.PayloadSize) As Byte

        If Not Product.LoadHexSegment(HexData, HexSegment.Address) Then
            Return False
        End If

        PayloadSize = Product.CmdAssetWrite.PayloadSize

        SegmentOffset = 0
        Do
            Product.CmdAssetWrite.Address = HexData.Address + SegmentOffset
            Product.CmdAssetWrite.Length = PayloadSize

            PayloadIsEmpty = True
            For i As Integer = 0 To PayloadSize - 1
                Product.CmdAssetWrite.Data(i) = HexData.Data(SegmentOffset + i)
                CRCData(i) = HexData.Data(SegmentOffset + i)
                If Product.CmdAssetWrite.Data(i) <> &HFF Then
                    PayloadIsEmpty = False
                End If
            Next

            If Not PayloadIsEmpty Then
                If Not Product.CmdDataWrite.Execute() Then
                    Return False
                Else
                    Product.ProgressIncrement()
                    Product.CRCCalculate(CRCData, PayloadSize)
                    Product.PacketCount += 1
                    Product.PostMessage(WN_INT, INT_MSG_PACKET_COUNT, Product.PacketCount)
                    Dim Tmp As UInt32 = HexData.Address + SegmentOffset
                    Product.PostMessage(WN_ADD, INT_MSG_PACKET_ADDRESS, Tmp >> 1)
                    Product.PostMessage(WN_INT, INT_MSG_PACKET_SIZE, PayloadSize)
                End If
            End If

            SegmentOffset += Product.PayloadSize
        Loop Until SegmentOffset >= SEGMENT_SIZE

        Return True
    End Function
End Class
