<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtHex = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Port = New System.IO.Ports.SerialPort(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboCom = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnProgramAll = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lstOutput = New System.Windows.Forms.ListBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblPackets = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblRetries = New System.Windows.Forms.Label()
        Me.lblRetriesCPU = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnIdentify = New System.Windows.Forms.Button()
        Me.chkModBus = New System.Windows.Forms.CheckBox()
        Me.lblAppVer = New System.Windows.Forms.Label()
        Me.lblBootVer = New System.Windows.Forms.Label()
        Me.lblHrdVer = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblProductID = New System.Windows.Forms.Label()
        Me.lblFamilyID = New System.Windows.Forms.Label()
        Me.lblFirmware = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.prgTransfer = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtHex
        '
        Me.txtHex.Location = New System.Drawing.Point(16, 34)
        Me.txtHex.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtHex.Name = "txtHex"
        Me.txtHex.Size = New System.Drawing.Size(547, 26)
        Me.txtHex.TabIndex = 0
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(579, 30)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(112, 35)
        Me.btnBrowse.TabIndex = 50
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(12, 9)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(197, 20)
        Me.Label18.TabIndex = 51
        Me.Label18.Text = "Select HEX file to program"
        '
        'Port
        '
        Me.Port.BaudRate = 57600
        Me.Port.PortName = "COM6"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.prgTransfer)
        Me.GroupBox1.Controls.Add(Me.cboCom)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnProgramAll)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnIdentify)
        Me.GroupBox1.Controls.Add(Me.chkModBus)
        Me.GroupBox1.Controls.Add(Me.lblAppVer)
        Me.GroupBox1.Controls.Add(Me.lblBootVer)
        Me.GroupBox1.Controls.Add(Me.lblHrdVer)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.lblProductID)
        Me.GroupBox1.Controls.Add(Me.lblFamilyID)
        Me.GroupBox1.Controls.Add(Me.lblFirmware)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 73)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(675, 600)
        Me.GroupBox1.TabIndex = 56
        Me.GroupBox1.TabStop = False
        '
        'cboCom
        '
        Me.cboCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCom.FormattingEnabled = True
        Me.cboCom.Location = New System.Drawing.Point(150, 25)
        Me.cboCom.Name = "cboCom"
        Me.cboCom.Size = New System.Drawing.Size(176, 28)
        Me.cboCom.TabIndex = 40
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 20)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Select COM port"
        '
        'btnProgramAll
        '
        Me.btnProgramAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProgramAll.Location = New System.Drawing.Point(381, 291)
        Me.btnProgramAll.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnProgramAll.Name = "btnProgramAll"
        Me.btnProgramAll.Size = New System.Drawing.Size(263, 50)
        Me.btnProgramAll.TabIndex = 38
        Me.btnProgramAll.Text = "PROGRAM"
        Me.btnProgramAll.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lstOutput)
        Me.GroupBox4.Controls.Add(Me.btnClear)
        Me.GroupBox4.Location = New System.Drawing.Point(22, 94)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(326, 437)
        Me.GroupBox4.TabIndex = 35
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Output"
        '
        'lstOutput
        '
        Me.lstOutput.FormattingEnabled = True
        Me.lstOutput.ItemHeight = 20
        Me.lstOutput.Location = New System.Drawing.Point(21, 28)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.Size = New System.Drawing.Size(283, 344)
        Me.lstOutput.TabIndex = 57
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(21, 385)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(283, 35)
        Me.btnClear.TabIndex = 39
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblPackets)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.lblSize)
        Me.GroupBox2.Controls.Add(Me.lblAddress)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.lblRetries)
        Me.GroupBox2.Controls.Add(Me.lblRetriesCPU)
        Me.GroupBox2.Location = New System.Drawing.Point(381, 401)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(263, 130)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Packets"
        '
        'lblPackets
        '
        Me.lblPackets.AutoSize = True
        Me.lblPackets.Location = New System.Drawing.Point(141, 47)
        Me.lblPackets.Name = "lblPackets"
        Me.lblPackets.Size = New System.Drawing.Size(59, 20)
        Me.lblPackets.TabIndex = 23
        Me.lblPackets.Text = "----------"
        Me.lblPackets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 20)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Total count :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Location = New System.Drawing.Point(141, 93)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(59, 20)
        Me.lblSize.TabIndex = 21
        Me.lblSize.Text = "----------"
        Me.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(141, 70)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(59, 20)
        Me.lblAddress.TabIndex = 20
        Me.lblAddress.Text = "----------"
        Me.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(31, 93)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(105, 20)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Payload size :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(60, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 20)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Address :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRetries
        '
        Me.lblRetries.AutoSize = True
        Me.lblRetries.Location = New System.Drawing.Point(141, 24)
        Me.lblRetries.Name = "lblRetries"
        Me.lblRetries.Size = New System.Drawing.Size(59, 20)
        Me.lblRetries.TabIndex = 17
        Me.lblRetries.Text = "----------"
        Me.lblRetries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRetriesCPU
        '
        Me.lblRetriesCPU.AutoSize = True
        Me.lblRetriesCPU.Location = New System.Drawing.Point(36, 24)
        Me.lblRetriesCPU.Name = "lblRetriesCPU"
        Me.lblRetriesCPU.Size = New System.Drawing.Size(100, 20)
        Me.lblRetriesCPU.TabIndex = 16
        Me.lblRetriesCPU.Text = "Total retries :"
        Me.lblRetriesCPU.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(22, 65)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(628, 10)
        Me.GroupBox3.TabIndex = 34
        Me.GroupBox3.TabStop = False
        '
        'txtAddress
        '
        Me.txtAddress.Enabled = False
        Me.txtAddress.Location = New System.Drawing.Point(527, 25)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(54, 26)
        Me.txtAddress.TabIndex = 1
        Me.txtAddress.Text = "123"
        Me.txtAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(588, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Address"
        '
        'btnIdentify
        '
        Me.btnIdentify.Location = New System.Drawing.Point(381, 246)
        Me.btnIdentify.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnIdentify.Name = "btnIdentify"
        Me.btnIdentify.Size = New System.Drawing.Size(263, 35)
        Me.btnIdentify.TabIndex = 21
        Me.btnIdentify.Text = "Identify"
        Me.btnIdentify.UseVisualStyleBackColor = True
        '
        'chkModBus
        '
        Me.chkModBus.AutoSize = True
        Me.chkModBus.Location = New System.Drawing.Point(396, 25)
        Me.chkModBus.Name = "chkModBus"
        Me.chkModBus.Size = New System.Drawing.Size(121, 24)
        Me.chkModBus.TabIndex = 0
        Me.chkModBus.Text = "Via ModBus"
        Me.chkModBus.UseVisualStyleBackColor = True
        '
        'lblAppVer
        '
        Me.lblAppVer.AutoSize = True
        Me.lblAppVer.Location = New System.Drawing.Point(523, 209)
        Me.lblAppVer.Name = "lblAppVer"
        Me.lblAppVer.Size = New System.Drawing.Size(59, 20)
        Me.lblAppVer.TabIndex = 32
        Me.lblAppVer.Text = "----------"
        Me.lblAppVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBootVer
        '
        Me.lblBootVer.AutoSize = True
        Me.lblBootVer.Location = New System.Drawing.Point(523, 186)
        Me.lblBootVer.Name = "lblBootVer"
        Me.lblBootVer.Size = New System.Drawing.Size(59, 20)
        Me.lblBootVer.TabIndex = 31
        Me.lblBootVer.Text = "----------"
        Me.lblBootVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHrdVer
        '
        Me.lblHrdVer.AutoSize = True
        Me.lblHrdVer.Location = New System.Drawing.Point(523, 163)
        Me.lblHrdVer.Name = "lblHrdVer"
        Me.lblHrdVer.Size = New System.Drawing.Size(59, 20)
        Me.lblHrdVer.TabIndex = 30
        Me.lblHrdVer.Text = "----------"
        Me.lblHrdVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(381, 351)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(263, 35)
        Me.btnCancel.TabIndex = 22
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblProductID
        '
        Me.lblProductID.AutoSize = True
        Me.lblProductID.Location = New System.Drawing.Point(523, 140)
        Me.lblProductID.Name = "lblProductID"
        Me.lblProductID.Size = New System.Drawing.Size(59, 20)
        Me.lblProductID.TabIndex = 29
        Me.lblProductID.Text = "----------"
        Me.lblProductID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFamilyID
        '
        Me.lblFamilyID.AutoSize = True
        Me.lblFamilyID.Location = New System.Drawing.Point(523, 117)
        Me.lblFamilyID.Name = "lblFamilyID"
        Me.lblFamilyID.Size = New System.Drawing.Size(59, 20)
        Me.lblFamilyID.TabIndex = 28
        Me.lblFamilyID.Text = "----------"
        Me.lblFamilyID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFirmware
        '
        Me.lblFirmware.AutoSize = True
        Me.lblFirmware.Location = New System.Drawing.Point(523, 94)
        Me.lblFirmware.Name = "lblFirmware"
        Me.lblFirmware.Size = New System.Drawing.Size(59, 20)
        Me.lblFirmware.TabIndex = 27
        Me.lblFirmware.Text = "----------"
        Me.lblFirmware.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(435, 94)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(82, 20)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = "Firmware :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(417, 209)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(100, 20)
        Me.Label20.TabIndex = 26
        Me.Label20.Text = "App version :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(434, 117)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(83, 20)
        Me.Label24.TabIndex = 22
        Me.Label24.Text = "Family ID :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(412, 186)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(105, 20)
        Me.Label21.TabIndex = 25
        Me.Label21.Text = "Boot version :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(424, 140)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(93, 20)
        Me.Label23.TabIndex = 23
        Me.Label23.Text = "Product ID :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(377, 163)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(140, 20)
        Me.Label22.TabIndex = 24
        Me.Label22.Text = "Hardware version :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'prgTransfer
        '
        Me.prgTransfer.Location = New System.Drawing.Point(22, 551)
        Me.prgTransfer.Name = "prgTransfer"
        Me.prgTransfer.Size = New System.Drawing.Size(622, 23)
        Me.prgTransfer.TabIndex = 41
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 696)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtHex)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "T2000 Bootloader"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtHex As TextBox
    Friend WithEvents btnBrowse As Button
    Friend WithEvents Label18 As Label
    Friend WithEvents Port As IO.Ports.SerialPort
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnIdentify As Button
    Friend WithEvents lblAppVer As Label
    Friend WithEvents lblBootVer As Label
    Friend WithEvents lblHrdVer As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblProductID As Label
    Friend WithEvents lblFamilyID As Label
    Friend WithEvents lblFirmware As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkModBus As CheckBox
    Friend WithEvents lblPackets As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblSize As Label
    Friend WithEvents lblAddress As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblRetries As Label
    Friend WithEvents lblRetriesCPU As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents btnProgramAll As Button
    Friend WithEvents lstOutput As ListBox
    Friend WithEvents btnClear As Button
    Friend WithEvents cboCom As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents prgTransfer As ProgressBar
End Class
