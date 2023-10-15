
Imports System.IO
Imports System.IO.Ports

Public Class frmMain

    '----------------------------------------------------------------------------------------------
    Private LastDir As String = ""

    '----------------------------------------------------------------------------------------------
    Private m_LastTxtAddressState As Boolean

    '----------------------------------------------------------------------------------------------
    Public Product As CProduct 'CT2000
    Public CurrentFirmware As Byte = FIRM_NONE

    '----------------------------------------------------------------------------------------------
    Public Function GetCurrentFirmware() As Byte
        Return CurrentFirmware
    End Function

    '----------------------------------------------------------------------------------------------
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        Dim Cmd As Integer = m.WParam

        If m.Msg = WN_INT Then   'Internal messages

            If Cmd = INT_MSG_CANCELLED Then
                MsgBox("Process cancelled by the user")

            ElseIf Cmd = INT_MSG_NO_ANSWER Then
                MsgBox("No answer from product")

            ElseIf Cmd = INT_MSG_CRC_MISMATCH Then
                MsgBox("Received packet CRC error")

            ElseIf Cmd = INT_MSG_RETRIES Then
                lblRetries.Text = m.LParam

            ElseIf Cmd = INT_MSG_PACKET_COUNT Then
                lblPackets.Text = m.LParam

            ElseIf Cmd = INT_MSG_PACKET_SIZE Then
                lblSize.Text = m.LParam

            ElseIf Cmd = INT_MSG_HEX_FILE Then
                MsgBox("Error while parsing HEX file")

            End If

        ElseIf m.Msg = WN_ADD Then   'Addresses display
            Dim Address As UInt32 = m.LParam
            Address <<= 1
            lblAddress.Text = String.Format("0x{0:X8}", Address)


        ElseIf m.Msg = WM_COM Then   'Bootloader messages

            If (Cmd And CMD_ERROR_FLAG) = CMD_ERROR_FLAG Then

                Dim Res As Integer = m.LParam

                If Res = ERR_UNKNOWN_COMMAND Then
                    MsgBox("Unknown command")

                ElseIf Res = ERR_INVALID_FIRMWARE Then
                    MsgBox("Invalid firmware")

                ElseIf Res = ERR_FORBIDDEN Then
                    MsgBox("Command is forbidden in current firmware")

                ElseIf Res = ERR_BANNERS Then
                    MsgBox("Application header/footer mismatch")

                ElseIf Res = ERR_ADDRESS Then
                    MsgBox("Invalid address/range for device")

                ElseIf Res = ERR_SECTOR Then
                    MsgBox("Invalid sector/range for device")

                ElseIf Res = ERR_ERASE Then
                    MsgBox("Error while erasing device")

                ElseIf Res = ERR_WRITE Then
                    MsgBox("Error while writing to device")

                ElseIf Res = ERR_READ Then
                    MsgBox("Error while reading from device")

                ElseIf Res = ERR_FIRMWARE_CRC Then
                    MsgBox("Calculated firmware CRC mismatch")

                ElseIf Res = ERR_ASSETS_CRC Then
                    MsgBox("Calculated assets CRC mismatch")

                End If

            Else

                Dim BootCmd As Integer = Cmd And (Not CMD_ERROR_FLAG)

                If BootCmd = CMD_IDENTIFY Then
                    ProcessIdentify()
                End If

            End If

        End If

        MyBase.WndProc(m)

    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        cboCom.Items.AddRange(SerialPort.GetPortNames)
        Product = New CT2000(Me, Port)
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Port.Close()
    End Sub


    '----------------------------------------------------------------------------------------------
    Private Sub chkModBus_CheckedChanged(sender As Object, e As EventArgs) Handles chkModBus.CheckedChanged
        txtAddress.Enabled = chkModBus.Checked
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim openDlg As New OpenFileDialog()

        openDlg.InitialDirectory = LastDir
        openDlg.Filter = "HEX files (*.hex)|*.hex|All files (*.*)|*.*"
        openDlg.RestoreDirectory = True

        If openDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txtHex.Text = openDlg.FileName
            LastDir = Path.GetDirectoryName(openDlg.FileName)
        End If
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub btnIdentify_Click(sender As Object, e As EventArgs) Handles btnIdentify.Click
        If Not Port.IsOpen Then
            MsgBox("No COM port selected")
            Return
        End If

        InitText()
        Product.Identify(chkModBus.Checked, txtAddress.Text)
    End Sub

    Private Sub ProcessIdentify()
        CurrentFirmware = Product.CmdIdentify.Firmware
        Select Case CurrentFirmware
            Case FIRM_APP
                lblFirmware.Text = "Application"
            Case FIRM_BOOTL
                lblFirmware.Text = "BootLoader"
            Case FIRM_BOOTU
                lblFirmware.Text = "BootUpdater"
        End Select

        lblFamilyID.Text = String.Format("0x{0:X2}", Product.CmdIdentify.Family)
        lblProductID.Text = String.Format("0x{0:X2}", Product.CmdIdentify.Product)
        lblHrdVer.Text = String.Format("0x{0:X4}", Product.CmdIdentify.HardwareVersion)
        lblBootVer.Text = String.Format("0x{0:X4}", Product.CmdIdentify.BootloaderVersion)
        lblAppVer.Text = String.Format("0x{0:X4}", Product.CmdIdentify.ApplicationVersion)
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Product.Cancel()
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub InitText()
        lblFirmware.Text = BlankValue
        lblFamilyID.Text = BlankValue
        lblProductID.Text = BlankValue
        lblHrdVer.Text = BlankValue
        lblBootVer.Text = BlankValue
        lblAppVer.Text = BlankValue
        m_LastTxtAddressState = txtAddress.Enabled
    End Sub

    '----------------------------------------------------------------------------------------------
    Public Sub Buttons(State As Boolean)
        txtHex.Enabled = State
        btnBrowse.Enabled = State
        btnIdentify.Enabled = State
        btnProgramAll.Enabled = State
        btnClear.Enabled = State
        btnCancel.Enabled = Not State
        chkModBus.Enabled = State
        If State = False Then
            txtAddress.Enabled = False
        Else
            txtAddress.Enabled = m_LastTxtAddressState
        End If
        cboCom.Enabled = State
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub btnProgramAll_Click(sender As Object, e As EventArgs) Handles btnProgramAll.Click

        If Not Port.IsOpen Then
            MsgBox("No COM port selected")
            Return
        End If

        If chkModBus.Checked Then
            Dim Valid As Boolean = True

            If String.IsNullOrEmpty(txtAddress.Text) Then
                Valid = False
            ElseIf txtAddress.Text < MODBUS_ADDR_MIN Or txtAddress.Text > MODBUS_ADDR_MAX Then
                Valid = False
            End If

            If Not Valid Then
                MsgBox(String.Format("You must enter a valid ModBus address: ( {0:d} - {1:d} )", MODBUS_ADDR_MIN, MODBUS_ADDR_MAX))
                Return
            End If
        End If

        If String.IsNullOrEmpty(txtHex.Text) Then
            MsgBox("You must select a HEX file")
            Return
        End If

        InitText()
        lblRetries.Text = BlankValue
        lblPackets.Text = BlankValue
        lblAddress.Text = BlankValue
        lblSize.Text = BlankValue

        Product.Program(chkModBus.Checked, txtAddress.Text, txtHex.Text)
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        For i = 0 To lstOutput.Items.Count - 1
            lstOutput.Items.RemoveAt(0)
        Next
    End Sub

    '----------------------------------------------------------------------------------------------
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCom.SelectedIndexChanged
        Port.Close()
        Port.PortName = cboCom.SelectedItem
        Try
            Port.Open()
        Catch ex As Exception
            MsgBox("Error opening port")
        End Try
    End Sub

End Class
