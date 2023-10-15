
Public Module MCommand

    '----------------------------------------------------------------------------------------------
    '=====  Processes  ============================================================================
    '----------------------------------------------------------------------------------------------
    Public Const WN_INT As UInt16 = &H400   'Messages from internal process
    Public Const WN_ADD As UInt16 = &H401   'Messages from internal process dedicated for addresses
    Public Const WM_COM As UInt16 = &H402   'Messages from communication process

    '----------------------------------------------------------------------------------------------
    '=====  Internal message codes  ===============================================================
    '----------------------------------------------------------------------------------------------
    Public Const INT_MSG_SUCCESS As Byte = 0
    Public Const INT_MSG_CANCELLED As Byte = 1
    Public Const INT_MSG_NO_ANSWER As Byte = 2
    Public Const INT_MSG_CRC_MISMATCH As Byte = 3
    Public Const INT_MSG_RETRIES As Byte = 4
    Public Const INT_MSG_PACKET_COUNT As Byte = 5
    Public Const INT_MSG_PACKET_ADDRESS As Byte = 6
    Public Const INT_MSG_PACKET_SIZE As Byte = 7
    Public Const INT_MSG_HEX_FILE As Byte = 8
    'Add new messages here

    '----------------------------------------------------------------------------------------------
    '=====  Communication parameters  =============================================================
    '----------------------------------------------------------------------------------------------
    Public Const PACKET_SIZE_MAX As Integer = 2048
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Const PAYLOAD_SIZE As Integer = 1024
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Const sizeofChecksum As Integer = sizeofUint16
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Const FIRM_NONE As Byte = 255
    Public Const FIRM_APP As Byte = 0
    Public Const FIRM_BOOTL As Byte = 1
    Public Const FIRM_BOOTU As Byte = 2
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Const ERASE_TIMEOUT As Integer = 15000   '15 seconds between erase command attempts
    Public Const ENTER_TIMEOUT As Integer = 10000   '10 seconds between enter command attempts
    Public Const LEAVE_TIMEOUT As Integer = 10000   '10 seconds between leave command attempts
    Public Const TXRX_TIMEOUT As Integer = 1000     '1 second between any other commands attempts
    Public Const TXRX_ATTEMPTS As Integer = 3

    '----------------------------------------------------------------------------------------------
    '=====  Communication commands  ===============================================================
    '----------------------------------------------------------------------------------------------
    Public Const HEADER_FRAMEWORK_VALUE As UInt32 = &HF8E0_D2C4UI   'Framework protocol header magic number
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Const CMD_IDENTIFY As Byte = 1
    Public Const CMD_ENTER As Byte = 2
    Public Const CMD_LEAVE As Byte = 3
    Public Const CMD_FIRMWARE_ERASE As Byte = 4
    Public Const CMD_FIRMWARE_WRITE As Byte = 5
    Public Const CMD_FIRMWARE_READ As Byte = 6
    Public Const CMD_ASSET_ERASE As Byte = 7
    Public Const CMD_ASSET_WRITE As Byte = 8
    Public Const CMD_ASSET_READ As Byte = 9
    Public Const CMD_DATA_WRITE As Byte = 10
    Public Const CMD_DATA_READ As Byte = 11
    'Add new commands here
    Public Const CMD_LAST As Byte = CMD_DATA_READ   '<-- Replace by newly added command
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Const CMD_ERROR_FLAG As Byte = &H80

    '----------------------------------------------------------------------------------------------
    '=====  Communication commands error codes  ===================================================
    '----------------------------------------------------------------------------------------------
    Public Const ERR_UNKNOWN_COMMAND As Byte = 1
    Public Const ERR_INVALID_FIRMWARE As Byte = 2
    Public Const ERR_FORBIDDEN As Byte = 3
    Public Const ERR_BANNERS As Byte = 4
    Public Const ERR_ADDRESS As Byte = 5
    Public Const ERR_SECTOR As Byte = 6
    Public Const ERR_ERASE As Byte = 7
    Public Const ERR_WRITE As Byte = 8
    Public Const ERR_READ As Byte = 9
    Public Const ERR_FIRMWARE_CRC As Byte = 10
    Public Const ERR_ASSETS_CRC As Byte = 11
    Public Const ERR_ASSETS_VERSION As Byte = 12

    '----------------------------------------------------------------------------------------------
    '=====  Communication CRC  ====================================================================
    '----------------------------------------------------------------------------------------------
    Public ReadOnly CRCTableHi As Byte() =
    {
        &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40,
        &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41,
        &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41,
        &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40,
        &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41,
        &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40,
        &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40,
        &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41,
        &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41,
        &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40,
        &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40,
        &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41,
        &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40,
        &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41,
        &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41,
        &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40
    }
    Public ReadOnly CRCTableLo As Byte() =
    {
        &H0, &HC0, &HC1, &H1, &HC3, &H3, &H2, &HC2, &HC6, &H6, &H7, &HC7, &H5, &HC5, &HC4, &H4,
        &HCC, &HC, &HD, &HCD, &HF, &HCF, &HCE, &HE, &HA, &HCA, &HCB, &HB, &HC9, &H9, &H8, &HC8,
        &HD8, &H18, &H19, &HD9, &H1B, &HDB, &HDA, &H1A, &H1E, &HDE, &HDF, &H1F, &HDD, &H1D, &H1C, &HDC,
        &H14, &HD4, &HD5, &H15, &HD7, &H17, &H16, &HD6, &HD2, &H12, &H13, &HD3, &H11, &HD1, &HD0, &H10,
        &HF0, &H30, &H31, &HF1, &H33, &HF3, &HF2, &H32, &H36, &HF6, &HF7, &H37, &HF5, &H35, &H34, &HF4,
        &H3C, &HFC, &HFD, &H3D, &HFF, &H3F, &H3E, &HFE, &HFA, &H3A, &H3B, &HFB, &H39, &HF9, &HF8, &H38,
        &H28, &HE8, &HE9, &H29, &HEB, &H2B, &H2A, &HEA, &HEE, &H2E, &H2F, &HEF, &H2D, &HED, &HEC, &H2C,
        &HE4, &H24, &H25, &HE5, &H27, &HE7, &HE6, &H26, &H22, &HE2, &HE3, &H23, &HE1, &H21, &H20, &HE0,
        &HA0, &H60, &H61, &HA1, &H63, &HA3, &HA2, &H62, &H66, &HA6, &HA7, &H67, &HA5, &H65, &H64, &HA4,
        &H6C, &HAC, &HAD, &H6D, &HAF, &H6F, &H6E, &HAE, &HAA, &H6A, &H6B, &HAB, &H69, &HA9, &HA8, &H68,
        &H78, &HB8, &HB9, &H79, &HBB, &H7B, &H7A, &HBA, &HBE, &H7E, &H7F, &HBF, &H7D, &HBD, &HBC, &H7C,
        &HB4, &H74, &H75, &HB5, &H77, &HB7, &HB6, &H76, &H72, &HB2, &HB3, &H73, &HB1, &H71, &H70, &HB0,
        &H50, &H90, &H91, &H51, &H93, &H53, &H52, &H92, &H96, &H56, &H57, &H97, &H55, &H95, &H94, &H54,
        &H9C, &H5C, &H5D, &H9D, &H5F, &H9F, &H9E, &H5E, &H5A, &H9A, &H9B, &H5B, &H99, &H59, &H58, &H98,
        &H88, &H48, &H49, &H89, &H4B, &H8B, &H8A, &H4A, &H4E, &H8E, &H8F, &H4F, &H8D, &H4D, &H4C, &H8C,
        &H44, &H84, &H85, &H45, &H87, &H47, &H46, &H86, &H82, &H42, &H43, &H83, &H41, &H81, &H80, &H40
    }
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Function CRC16(ByRef Data As Byte(), StartIndex As Integer, Length As Integer) As UInt16
        Dim CRCHi As Byte = &HFF
        Dim CRCLo As Byte = &HFF
        Dim Index As UInt16
        Dim Res As UInt16

        Do
            Index = CRCHi Xor Data(StartIndex)
            CRCHi = CRCLo Xor CRCTableHi(Index)
            CRCLo = CRCTableLo(Index)
            StartIndex += 1
            Length -= 1
        Loop While Length > 0

        Res = CRCHi
        Res <<= 8
        Res += CRCLo
        Return Res
    End Function

    '----------------------------------------------------------------------------------------------
    '=====  ModBus parameters  ====================================================================
    '----------------------------------------------------------------------------------------------
    Public Const MODBUS_PAYLOAD_SIZE As Integer = 128
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Const MODBUS_ADDR_MIN As Integer = 1
    Public Const MODBUS_ADDR_MAX As Integer = 247
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Const MODBUS_MEIT_FUNCTION As Byte = &H2B
    Public Const MODBUS_MEIT_FUNCTION_UPGRADE As Byte = &H1

    ''----------------------------------------------------------------------------------------------
    ''=====  FLASH Memory map ======================================================================
    ''----------------------------------------------------------------------------------------------
    Public Const FLEXT_MAP_ADDRESS As UInt32 = &H9000_0000UI

End Module
