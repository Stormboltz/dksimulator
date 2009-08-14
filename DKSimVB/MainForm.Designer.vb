'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:44
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class MainForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.button1 = New System.Windows.Forms.Button
		Me.PBsim = New System.Windows.Forms.ProgressBar
		Me.btEP = New System.Windows.Forms.Button
		Me.lblVersion = New System.Windows.Forms.Label
		Me.lblDPS = New System.Windows.Forms.Label
		Me.tbTools = New System.Windows.Forms.TabPage
		Me.cmdImportArmory = New System.Windows.Forms.Button
		Me.txtArmory = New System.Windows.Forms.TextBox
		Me.label12 = New System.Windows.Forms.Label
		Me.cmdImportTemplate = New System.Windows.Forms.Button
		Me.label11 = New System.Windows.Forms.Label
		Me.txtImportTemplate = New System.Windows.Forms.TextBox
		Me.tabPage1 = New System.Windows.Forms.TabPage
		Me.cmdSaveNew = New System.Windows.Forms.Button
		Me.cmdSave = New System.Windows.Forms.Button
		Me.rtfEditor = New System.Windows.Forms.RichTextBox
		Me.HtmlReport = New System.Windows.Forms.TabPage
		Me.webBrowser1 = New System.Windows.Forms.WebBrowser
		Me.tabPage3 = New System.Windows.Forms.TabPage
		Me.label17 = New System.Windows.Forms.Label
		Me.label15 = New System.Windows.Forms.Label
		Me.txtInterruptAmount = New System.Windows.Forms.TextBox
		Me.txtInterruptCd = New System.Windows.Forms.TextBox
		Me.label16 = New System.Windows.Forms.Label
		Me.label14 = New System.Windows.Forms.Label
		Me.txtAMScd = New System.Windows.Forms.TextBox
		Me.txtAMSrp = New System.Windows.Forms.TextBox
		Me.label13 = New System.Windows.Forms.Label
		Me.chkGhoulHaste = New System.Windows.Forms.CheckBox
		Me.chkLissage = New System.Windows.Forms.CheckBox
		Me.txtLatency = New System.Windows.Forms.TextBox
		Me.txtSimtime = New System.Windows.Forms.TextBox
		Me.chkWaitFC = New System.Windows.Forms.CheckBox
		Me.cmdEditRot = New System.Windows.Forms.Button
		Me.cmdEditPrio = New System.Windows.Forms.Button
		Me.cmdEditChar = New System.Windows.Forms.Button
		Me.cmdEditTemplate = New System.Windows.Forms.Button
		Me.ckLogRP = New System.Windows.Forms.CheckBox
		Me.ckPet = New System.Windows.Forms.CheckBox
		Me.chkCombatLog = New System.Windows.Forms.CheckBox
		Me.label8 = New System.Windows.Forms.Label
		Me.cmbRuneOH = New System.Windows.Forms.ComboBox
		Me.label7 = New System.Windows.Forms.Label
		Me.cmbRuneMH = New System.Windows.Forms.ComboBox
		Me.label6 = New System.Windows.Forms.Label
		Me.cmbRotation = New System.Windows.Forms.ComboBox
		Me.label5 = New System.Windows.Forms.Label
		Me.cmbSigils = New System.Windows.Forms.ComboBox
		Me.label10 = New System.Windows.Forms.Label
		Me.label4 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.cmdPresence = New System.Windows.Forms.ComboBox
		Me.label2 = New System.Windows.Forms.Label
		Me.label9 = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.cmbPrio = New System.Windows.Forms.ComboBox
		Me.cmbCharacter = New System.Windows.Forms.ComboBox
		Me.cmbTemplate = New System.Windows.Forms.ComboBox
		Me.tabControl1 = New System.Windows.Forms.TabControl
		Me.Test = New System.Windows.Forms.TabPage
		Me.cmdSaveTemplate = New System.Windows.Forms.Button
		Me.cmdSaveNewTalent = New System.Windows.Forms.Button
		Me.wbTemplate = New System.Windows.Forms.WebBrowser
		Me.tbEPOptions = New System.Windows.Forms.TabPage
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.chkEP2PT9 = New System.Windows.Forms.CheckBox
		Me.chkEP4PT8 = New System.Windows.Forms.CheckBox
		Me.chkEP2PT8 = New System.Windows.Forms.CheckBox
		Me.chkEP4PT7 = New System.Windows.Forms.CheckBox
		Me.chkEP2T7 = New System.Windows.Forms.CheckBox
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.chkEPSMHSpeed = New System.Windows.Forms.CheckBox
		Me.chkEPExp = New System.Windows.Forms.CheckBox
		Me.chkEPSMHDPS = New System.Windows.Forms.CheckBox
		Me.chkEPSpHit = New System.Windows.Forms.CheckBox
		Me.chkEPHit = New System.Windows.Forms.CheckBox
		Me.chkEPArP = New System.Windows.Forms.CheckBox
		Me.chkEPHaste = New System.Windows.Forms.CheckBox
		Me.chkEPCrit = New System.Windows.Forms.CheckBox
		Me.chkEPAgility = New System.Windows.Forms.CheckBox
		Me.chkEPStr = New System.Windows.Forms.CheckBox
		Me.tbBuff = New System.Windows.Forms.TabPage
		Me.grpBuff = New System.Windows.Forms.GroupBox
		Me.chkBloodlust = New System.Windows.Forms.CheckBox
		Me.chkDraeni = New System.Windows.Forms.CheckBox
		Me.chkBStatMulti = New System.Windows.Forms.CheckBox
		Me.chkBStatAdd = New System.Windows.Forms.CheckBox
		Me.chkBSpellHaste = New System.Windows.Forms.CheckBox
		Me.chkBSpellCrit = New System.Windows.Forms.CheckBox
		Me.chkBSpHitTaken = New System.Windows.Forms.CheckBox
		Me.chkBMeleeCrit = New System.Windows.Forms.CheckBox
		Me.chkBSpDamTaken = New System.Windows.Forms.CheckBox
		Me.chkBMeleeHaste = New System.Windows.Forms.CheckBox
		Me.chkBSpCrTaken = New System.Windows.Forms.CheckBox
		Me.chkBHaste = New System.Windows.Forms.CheckBox
		Me.chkBPhyVuln = New System.Windows.Forms.CheckBox
		Me.chkBPcDamage = New System.Windows.Forms.CheckBox
		Me.chkBCritchanceTaken = New System.Windows.Forms.CheckBox
		Me.chkBAPPc = New System.Windows.Forms.CheckBox
		Me.chkBArmorMinor = New System.Windows.Forms.CheckBox
		Me.chkBAP = New System.Windows.Forms.CheckBox
		Me.chkBArmorMaj = New System.Windows.Forms.CheckBox
		Me.chkBStrAgi = New System.Windows.Forms.CheckBox
		Me.tbTools.SuspendLayout
		Me.tabPage1.SuspendLayout
		Me.HtmlReport.SuspendLayout
		Me.tabPage3.SuspendLayout
		Me.tabControl1.SuspendLayout
		Me.Test.SuspendLayout
		Me.tbEPOptions.SuspendLayout
		Me.groupBox2.SuspendLayout
		Me.groupBox1.SuspendLayout
		Me.tbBuff.SuspendLayout
		Me.grpBuff.SuspendLayout
		Me.SuspendLayout
		'
		'button1
		'
		Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.button1.Location = New System.Drawing.Point(22, 515)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(245, 23)
		Me.button1.TabIndex = 0
		Me.button1.Text = "Start Simulator"
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'PBsim
		'
		Me.PBsim.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PBsim.Location = New System.Drawing.Point(0, 544)
		Me.PBsim.Name = "PBsim"
		Me.PBsim.Size = New System.Drawing.Size(696, 23)
		Me.PBsim.TabIndex = 2
		'
		'btEP
		'
		Me.btEP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.btEP.Location = New System.Drawing.Point(435, 515)
		Me.btEP.Name = "btEP"
		Me.btEP.Size = New System.Drawing.Size(245, 23)
		Me.btEP.TabIndex = 4
		Me.btEP.Text = "Start EP"
		Me.btEP.UseVisualStyleBackColor = true
		AddHandler Me.btEP.Click, AddressOf Me.BtEPClick
		'
		'lblVersion
		'
		Me.lblVersion.Location = New System.Drawing.Point(466, 0)
		Me.lblVersion.Name = "lblVersion"
		Me.lblVersion.Size = New System.Drawing.Size(218, 19)
		Me.lblVersion.TabIndex = 8
		Me.lblVersion.Text = "Version "
		Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblDPS
		'
		Me.lblDPS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblDPS.BackColor = System.Drawing.Color.Transparent
		Me.lblDPS.Location = New System.Drawing.Point(297, 528)
		Me.lblDPS.Name = "lblDPS"
		Me.lblDPS.Size = New System.Drawing.Size(100, 13)
		Me.lblDPS.TabIndex = 9
		Me.lblDPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'tbTools
		'
		Me.tbTools.Controls.Add(Me.cmdImportArmory)
		Me.tbTools.Controls.Add(Me.txtArmory)
		Me.tbTools.Controls.Add(Me.label12)
		Me.tbTools.Controls.Add(Me.cmdImportTemplate)
		Me.tbTools.Controls.Add(Me.label11)
		Me.tbTools.Controls.Add(Me.txtImportTemplate)
		Me.tbTools.Location = New System.Drawing.Point(4, 22)
		Me.tbTools.Name = "tbTools"
		Me.tbTools.Size = New System.Drawing.Size(688, 471)
		Me.tbTools.TabIndex = 5
		Me.tbTools.Text = "Tools"
		Me.tbTools.UseVisualStyleBackColor = true
		'
		'cmdImportArmory
		'
		Me.cmdImportArmory.Location = New System.Drawing.Point(635, 67)
		Me.cmdImportArmory.Name = "cmdImportArmory"
		Me.cmdImportArmory.Size = New System.Drawing.Size(41, 23)
		Me.cmdImportArmory.TabIndex = 5
		Me.cmdImportArmory.Text = "Ok"
		Me.cmdImportArmory.UseVisualStyleBackColor = true
		Me.cmdImportArmory.Visible = false
		AddHandler Me.cmdImportArmory.Click, AddressOf Me.CmdImportArmoryClick
		'
		'txtArmory
		'
		Me.txtArmory.Location = New System.Drawing.Point(9, 67)
		Me.txtArmory.Name = "txtArmory"
		Me.txtArmory.Size = New System.Drawing.Size(619, 20)
		Me.txtArmory.TabIndex = 4
		Me.txtArmory.Visible = false
		'
		'label12
		'
		Me.label12.Location = New System.Drawing.Point(9, 48)
		Me.label12.Name = "label12"
		Me.label12.Size = New System.Drawing.Size(100, 15)
		Me.label12.TabIndex = 3
		Me.label12.Text = "Armory Character Import"
		Me.label12.Visible = false
		'
		'cmdImportTemplate
		'
		Me.cmdImportTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdImportTemplate.Location = New System.Drawing.Point(634, 18)
		Me.cmdImportTemplate.Name = "cmdImportTemplate"
		Me.cmdImportTemplate.Size = New System.Drawing.Size(42, 23)
		Me.cmdImportTemplate.TabIndex = 2
		Me.cmdImportTemplate.Text = "Ok"
		Me.cmdImportTemplate.UseVisualStyleBackColor = true
		AddHandler Me.cmdImportTemplate.Click, AddressOf Me.CmdImportTemplateClick
		'
		'label11
		'
		Me.label11.Location = New System.Drawing.Point(9, 4)
		Me.label11.Name = "label11"
		Me.label11.Size = New System.Drawing.Size(159, 14)
		Me.label11.TabIndex = 1
		Me.label11.Text = "Import talents"
		'
		'txtImportTemplate
		'
		Me.txtImportTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtImportTemplate.Location = New System.Drawing.Point(8, 21)
		Me.txtImportTemplate.Name = "txtImportTemplate"
		Me.txtImportTemplate.Size = New System.Drawing.Size(620, 20)
		Me.txtImportTemplate.TabIndex = 0
		'
		'tabPage1
		'
		Me.tabPage1.Controls.Add(Me.cmdSaveNew)
		Me.tabPage1.Controls.Add(Me.cmdSave)
		Me.tabPage1.Controls.Add(Me.rtfEditor)
		Me.tabPage1.Location = New System.Drawing.Point(4, 22)
		Me.tabPage1.Name = "tabPage1"
		Me.tabPage1.Size = New System.Drawing.Size(688, 471)
		Me.tabPage1.TabIndex = 4
		Me.tabPage1.Text = "Editor"
		Me.tabPage1.UseVisualStyleBackColor = true
		'
		'cmdSaveNew
		'
		Me.cmdSaveNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdSaveNew.Location = New System.Drawing.Point(596, 0)
		Me.cmdSaveNew.Name = "cmdSaveNew"
		Me.cmdSaveNew.Size = New System.Drawing.Size(84, 19)
		Me.cmdSaveNew.TabIndex = 1
		Me.cmdSaveNew.Text = "Save as New"
		Me.cmdSaveNew.UseVisualStyleBackColor = true
		AddHandler Me.cmdSaveNew.Click, AddressOf Me.CmdSaveNewClick
		'
		'cmdSave
		'
		Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdSave.Location = New System.Drawing.Point(506, 0)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(84, 19)
		Me.cmdSave.TabIndex = 1
		Me.cmdSave.Text = "Save"
		Me.cmdSave.UseVisualStyleBackColor = true
		AddHandler Me.cmdSave.Click, AddressOf Me.CmdSaveClick
		'
		'rtfEditor
		'
		Me.rtfEditor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.rtfEditor.Location = New System.Drawing.Point(3, 20)
		Me.rtfEditor.Name = "rtfEditor"
		Me.rtfEditor.Size = New System.Drawing.Size(682, 448)
		Me.rtfEditor.TabIndex = 0
		Me.rtfEditor.Text = ""
		'
		'HtmlReport
		'
		Me.HtmlReport.Controls.Add(Me.webBrowser1)
		Me.HtmlReport.Location = New System.Drawing.Point(4, 22)
		Me.HtmlReport.Name = "HtmlReport"
		Me.HtmlReport.Size = New System.Drawing.Size(688, 471)
		Me.HtmlReport.TabIndex = 3
		Me.HtmlReport.Text = "Report"
		Me.HtmlReport.UseVisualStyleBackColor = true
		'
		'webBrowser1
		'
		Me.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.webBrowser1.Location = New System.Drawing.Point(0, 0)
		Me.webBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
		Me.webBrowser1.Name = "webBrowser1"
		Me.webBrowser1.Size = New System.Drawing.Size(688, 471)
		Me.webBrowser1.TabIndex = 0
		'
		'tabPage3
		'
		Me.tabPage3.Controls.Add(Me.label17)
		Me.tabPage3.Controls.Add(Me.label15)
		Me.tabPage3.Controls.Add(Me.txtInterruptAmount)
		Me.tabPage3.Controls.Add(Me.txtInterruptCd)
		Me.tabPage3.Controls.Add(Me.label16)
		Me.tabPage3.Controls.Add(Me.label14)
		Me.tabPage3.Controls.Add(Me.txtAMScd)
		Me.tabPage3.Controls.Add(Me.txtAMSrp)
		Me.tabPage3.Controls.Add(Me.label13)
		Me.tabPage3.Controls.Add(Me.chkGhoulHaste)
		Me.tabPage3.Controls.Add(Me.chkLissage)
		Me.tabPage3.Controls.Add(Me.txtLatency)
		Me.tabPage3.Controls.Add(Me.txtSimtime)
		Me.tabPage3.Controls.Add(Me.chkWaitFC)
		Me.tabPage3.Controls.Add(Me.cmdEditRot)
		Me.tabPage3.Controls.Add(Me.cmdEditPrio)
		Me.tabPage3.Controls.Add(Me.cmdEditChar)
		Me.tabPage3.Controls.Add(Me.cmdEditTemplate)
		Me.tabPage3.Controls.Add(Me.ckLogRP)
		Me.tabPage3.Controls.Add(Me.ckPet)
		Me.tabPage3.Controls.Add(Me.chkCombatLog)
		Me.tabPage3.Controls.Add(Me.label8)
		Me.tabPage3.Controls.Add(Me.cmbRuneOH)
		Me.tabPage3.Controls.Add(Me.label7)
		Me.tabPage3.Controls.Add(Me.cmbRuneMH)
		Me.tabPage3.Controls.Add(Me.label6)
		Me.tabPage3.Controls.Add(Me.cmbRotation)
		Me.tabPage3.Controls.Add(Me.label5)
		Me.tabPage3.Controls.Add(Me.cmbSigils)
		Me.tabPage3.Controls.Add(Me.label10)
		Me.tabPage3.Controls.Add(Me.label4)
		Me.tabPage3.Controls.Add(Me.label3)
		Me.tabPage3.Controls.Add(Me.cmdPresence)
		Me.tabPage3.Controls.Add(Me.label2)
		Me.tabPage3.Controls.Add(Me.label9)
		Me.tabPage3.Controls.Add(Me.label1)
		Me.tabPage3.Controls.Add(Me.cmbPrio)
		Me.tabPage3.Controls.Add(Me.cmbCharacter)
		Me.tabPage3.Controls.Add(Me.cmbTemplate)
		Me.tabPage3.Location = New System.Drawing.Point(4, 22)
		Me.tabPage3.Name = "tabPage3"
		Me.tabPage3.Size = New System.Drawing.Size(688, 471)
		Me.tabPage3.TabIndex = 2
		Me.tabPage3.Text = "Configuration"
		Me.tabPage3.UseVisualStyleBackColor = true
		'
		'label17
		'
		Me.label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label17.Location = New System.Drawing.Point(6, 423)
		Me.label17.Name = "label17"
		Me.label17.Size = New System.Drawing.Size(116, 13)
		Me.label17.TabIndex = 32
		Me.label17.Text = "Interrupt fighting every"
		'
		'label15
		'
		Me.label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label15.Location = New System.Drawing.Point(278, 423)
		Me.label15.Name = "label15"
		Me.label15.Size = New System.Drawing.Size(16, 13)
		Me.label15.TabIndex = 31
		Me.label15.Text = "s"
		'
		'txtInterruptAmount
		'
		Me.txtInterruptAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtInterruptAmount.Enabled = false
		Me.txtInterruptAmount.Location = New System.Drawing.Point(222, 420)
		Me.txtInterruptAmount.Name = "txtInterruptAmount"
		Me.txtInterruptAmount.Size = New System.Drawing.Size(50, 20)
		Me.txtInterruptAmount.TabIndex = 30
		Me.txtInterruptAmount.Text = "0"
		'
		'txtInterruptCd
		'
		Me.txtInterruptCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtInterruptCd.Enabled = false
		Me.txtInterruptCd.Location = New System.Drawing.Point(128, 420)
		Me.txtInterruptCd.Name = "txtInterruptCd"
		Me.txtInterruptCd.Size = New System.Drawing.Size(50, 20)
		Me.txtInterruptCd.TabIndex = 29
		Me.txtInterruptCd.Text = "0"
		'
		'label16
		'
		Me.label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label16.Location = New System.Drawing.Point(184, 423)
		Me.label16.Name = "label16"
		Me.label16.Size = New System.Drawing.Size(32, 13)
		Me.label16.TabIndex = 28
		Me.label16.Text = "s for"
		'
		'label14
		'
		Me.label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label14.Location = New System.Drawing.Point(171, 449)
		Me.label14.Name = "label14"
		Me.label14.Size = New System.Drawing.Size(20, 13)
		Me.label14.TabIndex = 27
		Me.label14.Text = "s"
		'
		'txtAMScd
		'
		Me.txtAMScd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtAMScd.Location = New System.Drawing.Point(115, 446)
		Me.txtAMScd.Name = "txtAMScd"
		Me.txtAMScd.Size = New System.Drawing.Size(50, 20)
		Me.txtAMScd.TabIndex = 26
		Me.txtAMScd.Text = "60"
		'
		'txtAMSrp
		'
		Me.txtAMSrp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtAMSrp.Location = New System.Drawing.Point(6, 446)
		Me.txtAMSrp.Name = "txtAMSrp"
		Me.txtAMSrp.Size = New System.Drawing.Size(50, 20)
		Me.txtAMSrp.TabIndex = 25
		Me.txtAMSrp.Text = "0"
		'
		'label13
		'
		Me.label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label13.Location = New System.Drawing.Point(62, 449)
		Me.label13.Name = "label13"
		Me.label13.Size = New System.Drawing.Size(47, 13)
		Me.label13.TabIndex = 24
		Me.label13.Text = "rp every"
		'
		'chkGhoulHaste
		'
		Me.chkGhoulHaste.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkGhoulHaste.Location = New System.Drawing.Point(491, 392)
		Me.chkGhoulHaste.Name = "chkGhoulHaste"
		Me.chkGhoulHaste.Size = New System.Drawing.Size(194, 24)
		Me.chkGhoulHaste.TabIndex = 23
		Me.chkGhoulHaste.Text = "Ghoul double dips haste buff"
		Me.chkGhoulHaste.UseVisualStyleBackColor = true
		AddHandler Me.chkGhoulHaste.CheckedChanged, AddressOf Me.ChkGhoulHasteCheckedChanged
		'
		'chkLissage
		'
		Me.chkLissage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkLissage.Location = New System.Drawing.Point(306, 392)
		Me.chkLissage.Name = "chkLissage"
		Me.chkLissage.Size = New System.Drawing.Size(179, 24)
		Me.chkLissage.TabIndex = 22
		Me.chkLissage.Text = "Smooth Results"
		Me.chkLissage.UseVisualStyleBackColor = true
		AddHandler Me.chkLissage.CheckedChanged, AddressOf Me.ChkLissageCheckedChanged
		'
		'txtLatency
		'
		Me.txtLatency.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtLatency.Location = New System.Drawing.Point(88, 394)
		Me.txtLatency.Name = "txtLatency"
		Me.txtLatency.Size = New System.Drawing.Size(50, 20)
		Me.txtLatency.TabIndex = 21
		Me.txtLatency.Text = "150"
		AddHandler Me.txtLatency.TextChanged, AddressOf Me.TxtLatencyTextChanged
		'
		'txtSimtime
		'
		Me.txtSimtime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtSimtime.Location = New System.Drawing.Point(112, 368)
		Me.txtSimtime.Name = "txtSimtime"
		Me.txtSimtime.Size = New System.Drawing.Size(50, 20)
		Me.txtSimtime.TabIndex = 6
		Me.txtSimtime.Text = "100"
		'
		'chkWaitFC
		'
		Me.chkWaitFC.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkWaitFC.Checked = true
		Me.chkWaitFC.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkWaitFC.Location = New System.Drawing.Point(306, 444)
		Me.chkWaitFC.Name = "chkWaitFC"
		Me.chkWaitFC.Size = New System.Drawing.Size(179, 24)
		Me.chkWaitFC.TabIndex = 20
		Me.chkWaitFC.Text = "Wait for FC proc for GG/DRW"
		Me.chkWaitFC.UseVisualStyleBackColor = true
		'
		'cmdEditRot
		'
		Me.cmdEditRot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdEditRot.Location = New System.Drawing.Point(643, 151)
		Me.cmdEditRot.Name = "cmdEditRot"
		Me.cmdEditRot.Size = New System.Drawing.Size(42, 23)
		Me.cmdEditRot.TabIndex = 19
		Me.cmdEditRot.Text = "Edit"
		Me.cmdEditRot.UseVisualStyleBackColor = true
		AddHandler Me.cmdEditRot.Click, AddressOf Me.CmdEditRotClick
		'
		'cmdEditPrio
		'
		Me.cmdEditPrio.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdEditPrio.Location = New System.Drawing.Point(643, 111)
		Me.cmdEditPrio.Name = "cmdEditPrio"
		Me.cmdEditPrio.Size = New System.Drawing.Size(42, 23)
		Me.cmdEditPrio.TabIndex = 19
		Me.cmdEditPrio.Text = "Edit"
		Me.cmdEditPrio.UseVisualStyleBackColor = true
		AddHandler Me.cmdEditPrio.Click, AddressOf Me.CmdEditPrioClick
		'
		'cmdEditChar
		'
		Me.cmdEditChar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdEditChar.Location = New System.Drawing.Point(643, 31)
		Me.cmdEditChar.Name = "cmdEditChar"
		Me.cmdEditChar.Size = New System.Drawing.Size(42, 23)
		Me.cmdEditChar.TabIndex = 19
		Me.cmdEditChar.Text = "Edit"
		Me.cmdEditChar.UseVisualStyleBackColor = true
		AddHandler Me.cmdEditChar.Click, AddressOf Me.CmdEditCharClick
		'
		'cmdEditTemplate
		'
		Me.cmdEditTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdEditTemplate.Location = New System.Drawing.Point(643, 71)
		Me.cmdEditTemplate.Name = "cmdEditTemplate"
		Me.cmdEditTemplate.Size = New System.Drawing.Size(42, 23)
		Me.cmdEditTemplate.TabIndex = 19
		Me.cmdEditTemplate.Text = "Edit"
		Me.cmdEditTemplate.UseVisualStyleBackColor = true
		AddHandler Me.cmdEditTemplate.Click, AddressOf Me.CmdEditTemplateClick
		'
		'ckLogRP
		'
		Me.ckLogRP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.ckLogRP.Enabled = false
		Me.ckLogRP.Location = New System.Drawing.Point(491, 444)
		Me.ckLogRP.Name = "ckLogRP"
		Me.ckLogRP.Size = New System.Drawing.Size(194, 24)
		Me.ckLogRP.TabIndex = 18
		Me.ckLogRP.Text = "Very detailled combat log"
		Me.ckLogRP.UseVisualStyleBackColor = true
		AddHandler Me.ckLogRP.CheckedChanged, AddressOf Me.CkLogRPCheckedChanged
		'
		'ckPet
		'
		Me.ckPet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.ckPet.Checked = true
		Me.ckPet.CheckState = System.Windows.Forms.CheckState.Checked
		Me.ckPet.Location = New System.Drawing.Point(306, 418)
		Me.ckPet.Name = "ckPet"
		Me.ckPet.Size = New System.Drawing.Size(179, 24)
		Me.ckPet.TabIndex = 17
		Me.ckPet.Text = "Use Pets(Ghoul, Gargoyle)"
		Me.ckPet.UseVisualStyleBackColor = true
		AddHandler Me.ckPet.CheckedChanged, AddressOf Me.CkPetCheckedChanged
		'
		'chkCombatLog
		'
		Me.chkCombatLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.chkCombatLog.Location = New System.Drawing.Point(491, 418)
		Me.chkCombatLog.Name = "chkCombatLog"
		Me.chkCombatLog.Size = New System.Drawing.Size(194, 24)
		Me.chkCombatLog.TabIndex = 16
		Me.chkCombatLog.Text = "Generate Combat Log"
		Me.chkCombatLog.UseVisualStyleBackColor = true
		AddHandler Me.chkCombatLog.CheckedChanged, AddressOf Me.ChkCombatLogCheckedChanged
		'
		'label8
		'
		Me.label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label8.Location = New System.Drawing.Point(390, 257)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(100, 13)
		Me.label8.TabIndex = 15
		Me.label8.Text = "OffHand"
		'
		'cmbRuneOH
		'
		Me.cmbRuneOH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbRuneOH.FormattingEnabled = true
		Me.cmbRuneOH.Location = New System.Drawing.Point(390, 273)
		Me.cmbRuneOH.Name = "cmbRuneOH"
		Me.cmbRuneOH.Size = New System.Drawing.Size(250, 21)
		Me.cmbRuneOH.TabIndex = 14
		AddHandler Me.cmbRuneOH.SelectedIndexChanged, AddressOf Me.CmbRuneOHSelectedIndexChanged
		'
		'label7
		'
		Me.label7.Location = New System.Drawing.Point(6, 257)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(100, 13)
		Me.label7.TabIndex = 13
		Me.label7.Text = "MainHand"
		'
		'cmbRuneMH
		'
		Me.cmbRuneMH.FormattingEnabled = true
		Me.cmbRuneMH.Location = New System.Drawing.Point(6, 273)
		Me.cmbRuneMH.Name = "cmbRuneMH"
		Me.cmbRuneMH.Size = New System.Drawing.Size(250, 21)
		Me.cmbRuneMH.TabIndex = 12
		AddHandler Me.cmbRuneMH.SelectedIndexChanged, AddressOf Me.CmbRuneMHSelectedIndexChanged
		'
		'label6
		'
		Me.label6.Location = New System.Drawing.Point(6, 137)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(100, 13)
		Me.label6.TabIndex = 11
		Me.label6.Text = "Rotation"
		'
		'cmbRotation
		'
		Me.cmbRotation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbRotation.FormattingEnabled = true
		Me.cmbRotation.Location = New System.Drawing.Point(6, 153)
		Me.cmbRotation.Name = "cmbRotation"
		Me.cmbRotation.Size = New System.Drawing.Size(634, 21)
		Me.cmbRotation.TabIndex = 10
		AddHandler Me.cmbRotation.SelectedIndexChanged, AddressOf Me.ComboBox1SelectedIndexChanged
		'
		'label5
		'
		Me.label5.Location = New System.Drawing.Point(6, 217)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(100, 13)
		Me.label5.TabIndex = 9
		Me.label5.Text = "Sigil"
		'
		'cmbSigils
		'
		Me.cmbSigils.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbSigils.FormattingEnabled = true
		Me.cmbSigils.Location = New System.Drawing.Point(6, 233)
		Me.cmbSigils.Name = "cmbSigils"
		Me.cmbSigils.Size = New System.Drawing.Size(634, 21)
		Me.cmbSigils.TabIndex = 8
		AddHandler Me.cmbSigils.SelectedIndexChanged, AddressOf Me.CmbSigilsSelectedIndexChanged
		'
		'label10
		'
		Me.label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label10.Location = New System.Drawing.Point(6, 397)
		Me.label10.Name = "label10"
		Me.label10.Size = New System.Drawing.Size(76, 13)
		Me.label10.TabIndex = 7
		Me.label10.Text = "Latency in ms"
		'
		'label4
		'
		Me.label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label4.Location = New System.Drawing.Point(6, 371)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(100, 13)
		Me.label4.TabIndex = 7
		Me.label4.Text = "Simulated time in h"
		'
		'label3
		'
		Me.label3.Location = New System.Drawing.Point(6, 177)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(100, 13)
		Me.label3.TabIndex = 5
		Me.label3.Text = "Presence"
		'
		'cmdPresence
		'
		Me.cmdPresence.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdPresence.FormattingEnabled = true
		Me.cmdPresence.Location = New System.Drawing.Point(6, 193)
		Me.cmdPresence.Name = "cmdPresence"
		Me.cmdPresence.Size = New System.Drawing.Size(634, 21)
		Me.cmdPresence.TabIndex = 4
		AddHandler Me.cmdPresence.SelectedIndexChanged, AddressOf Me.CmdPresenceSelectedIndexChanged
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(6, 97)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(100, 13)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Priority"
		'
		'label9
		'
		Me.label9.Location = New System.Drawing.Point(6, 17)
		Me.label9.Name = "label9"
		Me.label9.Size = New System.Drawing.Size(100, 13)
		Me.label9.TabIndex = 2
		Me.label9.Text = "Character"
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(6, 57)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(100, 13)
		Me.label1.TabIndex = 2
		Me.label1.Text = "Template"
		'
		'cmbPrio
		'
		Me.cmbPrio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbPrio.FormattingEnabled = true
		Me.cmbPrio.Location = New System.Drawing.Point(6, 113)
		Me.cmbPrio.Name = "cmbPrio"
		Me.cmbPrio.Size = New System.Drawing.Size(634, 21)
		Me.cmbPrio.TabIndex = 1
		AddHandler Me.cmbPrio.SelectedIndexChanged, AddressOf Me.CmbPrioSelectedIndexChanged
		'
		'cmbCharacter
		'
		Me.cmbCharacter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbCharacter.FormattingEnabled = true
		Me.cmbCharacter.Location = New System.Drawing.Point(6, 33)
		Me.cmbCharacter.Name = "cmbCharacter"
		Me.cmbCharacter.Size = New System.Drawing.Size(634, 21)
		Me.cmbCharacter.TabIndex = 0
		AddHandler Me.cmbCharacter.SelectedIndexChanged, AddressOf Me.CmbCharacterSelectedIndexChanged
		'
		'cmbTemplate
		'
		Me.cmbTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbTemplate.FormattingEnabled = true
		Me.cmbTemplate.Location = New System.Drawing.Point(6, 73)
		Me.cmbTemplate.Name = "cmbTemplate"
		Me.cmbTemplate.Size = New System.Drawing.Size(634, 21)
		Me.cmbTemplate.TabIndex = 0
		AddHandler Me.cmbTemplate.SelectedIndexChanged, AddressOf Me.CmbTemplateSelectedIndexChanged
		'
		'tabControl1
		'
		Me.tabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.tabControl1.Controls.Add(Me.tabPage3)
		Me.tabControl1.Controls.Add(Me.HtmlReport)
		Me.tabControl1.Controls.Add(Me.tabPage1)
		Me.tabControl1.Controls.Add(Me.tbTools)
		Me.tabControl1.Controls.Add(Me.Test)
		Me.tabControl1.Controls.Add(Me.tbEPOptions)
		Me.tabControl1.Controls.Add(Me.tbBuff)
		Me.tabControl1.Location = New System.Drawing.Point(0, 0)
		Me.tabControl1.Name = "tabControl1"
		Me.tabControl1.SelectedIndex = 0
		Me.tabControl1.Size = New System.Drawing.Size(696, 497)
		Me.tabControl1.TabIndex = 3
		'
		'Test
		'
		Me.Test.Controls.Add(Me.cmdSaveTemplate)
		Me.Test.Controls.Add(Me.cmdSaveNewTalent)
		Me.Test.Controls.Add(Me.wbTemplate)
		Me.Test.Location = New System.Drawing.Point(4, 22)
		Me.Test.Name = "Test"
		Me.Test.Size = New System.Drawing.Size(688, 471)
		Me.Test.TabIndex = 6
		Me.Test.Text = "Template editor(beta)"
		Me.Test.UseVisualStyleBackColor = true
		'
		'cmdSaveTemplate
		'
		Me.cmdSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdSaveTemplate.Location = New System.Drawing.Point(512, -1)
		Me.cmdSaveTemplate.Name = "cmdSaveTemplate"
		Me.cmdSaveTemplate.Size = New System.Drawing.Size(84, 23)
		Me.cmdSaveTemplate.TabIndex = 2
		Me.cmdSaveTemplate.Text = "Save"
		Me.cmdSaveTemplate.UseVisualStyleBackColor = true
		AddHandler Me.cmdSaveTemplate.Click, AddressOf Me.Button2Click
		'
		'cmdSaveNewTalent
		'
		Me.cmdSaveNewTalent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdSaveNewTalent.Location = New System.Drawing.Point(602, -1)
		Me.cmdSaveNewTalent.Name = "cmdSaveNewTalent"
		Me.cmdSaveNewTalent.Size = New System.Drawing.Size(83, 23)
		Me.cmdSaveNewTalent.TabIndex = 1
		Me.cmdSaveNewTalent.Text = "Save as new"
		Me.cmdSaveNewTalent.UseVisualStyleBackColor = true
		AddHandler Me.cmdSaveNewTalent.Click, AddressOf Me.CmdSaveTalentClick
		'
		'wbTemplate
		'
		Me.wbTemplate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.wbTemplate.Location = New System.Drawing.Point(0, 25)
		Me.wbTemplate.MinimumSize = New System.Drawing.Size(20, 20)
		Me.wbTemplate.Name = "wbTemplate"
		Me.wbTemplate.Size = New System.Drawing.Size(688, 446)
		Me.wbTemplate.TabIndex = 0
		'
		'tbEPOptions
		'
		Me.tbEPOptions.Controls.Add(Me.groupBox2)
		Me.tbEPOptions.Controls.Add(Me.groupBox1)
		Me.tbEPOptions.Location = New System.Drawing.Point(4, 22)
		Me.tbEPOptions.Name = "tbEPOptions"
		Me.tbEPOptions.Size = New System.Drawing.Size(688, 471)
		Me.tbEPOptions.TabIndex = 7
		Me.tbEPOptions.Text = "EP Options"
		Me.tbEPOptions.UseVisualStyleBackColor = true
		'
		'groupBox2
		'
		Me.groupBox2.Controls.Add(Me.chkEP2PT9)
		Me.groupBox2.Controls.Add(Me.chkEP4PT8)
		Me.groupBox2.Controls.Add(Me.chkEP2PT8)
		Me.groupBox2.Controls.Add(Me.chkEP4PT7)
		Me.groupBox2.Controls.Add(Me.chkEP2T7)
		Me.groupBox2.Location = New System.Drawing.Point(272, 48)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(153, 372)
		Me.groupBox2.TabIndex = 3
		Me.groupBox2.TabStop = false
		Me.groupBox2.Text = "Sets"
		'
		'chkEP2PT9
		'
		Me.chkEP2PT9.Checked = true
		Me.chkEP2PT9.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEP2PT9.Location = New System.Drawing.Point(6, 139)
		Me.chkEP2PT9.Name = "chkEP2PT9"
		Me.chkEP2PT9.Size = New System.Drawing.Size(141, 24)
		Me.chkEP2PT9.TabIndex = 4
		Me.chkEP2PT9.Text = "2P T9"
		Me.chkEP2PT9.UseVisualStyleBackColor = true
		'
		'chkEP4PT8
		'
		Me.chkEP4PT8.Checked = true
		Me.chkEP4PT8.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEP4PT8.Location = New System.Drawing.Point(6, 109)
		Me.chkEP4PT8.Name = "chkEP4PT8"
		Me.chkEP4PT8.Size = New System.Drawing.Size(141, 24)
		Me.chkEP4PT8.TabIndex = 5
		Me.chkEP4PT8.Text = "4P T8"
		Me.chkEP4PT8.UseVisualStyleBackColor = true
		'
		'chkEP2PT8
		'
		Me.chkEP2PT8.Checked = true
		Me.chkEP2PT8.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEP2PT8.Location = New System.Drawing.Point(6, 79)
		Me.chkEP2PT8.Name = "chkEP2PT8"
		Me.chkEP2PT8.Size = New System.Drawing.Size(141, 24)
		Me.chkEP2PT8.TabIndex = 3
		Me.chkEP2PT8.Text = "2P T8"
		Me.chkEP2PT8.UseVisualStyleBackColor = true
		'
		'chkEP4PT7
		'
		Me.chkEP4PT7.Checked = true
		Me.chkEP4PT7.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEP4PT7.Location = New System.Drawing.Point(6, 49)
		Me.chkEP4PT7.Name = "chkEP4PT7"
		Me.chkEP4PT7.Size = New System.Drawing.Size(141, 24)
		Me.chkEP4PT7.TabIndex = 1
		Me.chkEP4PT7.Text = "4P T7"
		Me.chkEP4PT7.UseVisualStyleBackColor = true
		'
		'chkEP2T7
		'
		Me.chkEP2T7.Checked = true
		Me.chkEP2T7.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEP2T7.Location = New System.Drawing.Point(6, 19)
		Me.chkEP2T7.Name = "chkEP2T7"
		Me.chkEP2T7.Size = New System.Drawing.Size(141, 24)
		Me.chkEP2T7.TabIndex = 2
		Me.chkEP2T7.Text = "2P T7"
		Me.chkEP2T7.UseVisualStyleBackColor = true
		'
		'groupBox1
		'
		Me.groupBox1.Controls.Add(Me.chkEPSMHSpeed)
		Me.groupBox1.Controls.Add(Me.chkEPExp)
		Me.groupBox1.Controls.Add(Me.chkEPSMHDPS)
		Me.groupBox1.Controls.Add(Me.chkEPSpHit)
		Me.groupBox1.Controls.Add(Me.chkEPHit)
		Me.groupBox1.Controls.Add(Me.chkEPArP)
		Me.groupBox1.Controls.Add(Me.chkEPHaste)
		Me.groupBox1.Controls.Add(Me.chkEPCrit)
		Me.groupBox1.Controls.Add(Me.chkEPAgility)
		Me.groupBox1.Controls.Add(Me.chkEPStr)
		Me.groupBox1.Location = New System.Drawing.Point(36, 48)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(227, 372)
		Me.groupBox1.TabIndex = 2
		Me.groupBox1.TabStop = false
		Me.groupBox1.Text = "Common stats"
		'
		'chkEPSMHSpeed
		'
		Me.chkEPSMHSpeed.Checked = true
		Me.chkEPSMHSpeed.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPSMHSpeed.Location = New System.Drawing.Point(6, 289)
		Me.chkEPSMHSpeed.Name = "chkEPSMHSpeed"
		Me.chkEPSMHSpeed.Size = New System.Drawing.Size(145, 24)
		Me.chkEPSMHSpeed.TabIndex = 14
		Me.chkEPSMHSpeed.Text = "Main Hand Speed"
		Me.chkEPSMHSpeed.UseVisualStyleBackColor = true
		'
		'chkEPExp
		'
		Me.chkEPExp.Checked = true
		Me.chkEPExp.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPExp.Location = New System.Drawing.Point(6, 229)
		Me.chkEPExp.Name = "chkEPExp"
		Me.chkEPExp.Size = New System.Drawing.Size(104, 24)
		Me.chkEPExp.TabIndex = 14
		Me.chkEPExp.Text = "Expertise rating"
		Me.chkEPExp.UseVisualStyleBackColor = true
		'
		'chkEPSMHDPS
		'
		Me.chkEPSMHDPS.Checked = true
		Me.chkEPSMHDPS.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPSMHDPS.Location = New System.Drawing.Point(6, 259)
		Me.chkEPSMHDPS.Name = "chkEPSMHDPS"
		Me.chkEPSMHDPS.Size = New System.Drawing.Size(104, 24)
		Me.chkEPSMHDPS.TabIndex = 13
		Me.chkEPSMHDPS.Text = "Main Hand DPS"
		Me.chkEPSMHDPS.UseVisualStyleBackColor = true
		'
		'chkEPSpHit
		'
		Me.chkEPSpHit.Checked = true
		Me.chkEPSpHit.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPSpHit.Location = New System.Drawing.Point(6, 199)
		Me.chkEPSpHit.Name = "chkEPSpHit"
		Me.chkEPSpHit.Size = New System.Drawing.Size(104, 24)
		Me.chkEPSpHit.TabIndex = 13
		Me.chkEPSpHit.Text = "Spell Hit rating"
		Me.chkEPSpHit.UseVisualStyleBackColor = true
		'
		'chkEPHit
		'
		Me.chkEPHit.Checked = true
		Me.chkEPHit.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPHit.Location = New System.Drawing.Point(6, 169)
		Me.chkEPHit.Name = "chkEPHit"
		Me.chkEPHit.Size = New System.Drawing.Size(104, 24)
		Me.chkEPHit.TabIndex = 16
		Me.chkEPHit.Text = "Hit rating"
		Me.chkEPHit.UseVisualStyleBackColor = true
		'
		'chkEPArP
		'
		Me.chkEPArP.Checked = true
		Me.chkEPArP.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPArP.Location = New System.Drawing.Point(6, 139)
		Me.chkEPArP.Name = "chkEPArP"
		Me.chkEPArP.Size = New System.Drawing.Size(112, 24)
		Me.chkEPArP.TabIndex = 15
		Me.chkEPArP.Text = "Armor Penetration"
		Me.chkEPArP.UseVisualStyleBackColor = true
		'
		'chkEPHaste
		'
		Me.chkEPHaste.Checked = true
		Me.chkEPHaste.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPHaste.Location = New System.Drawing.Point(6, 109)
		Me.chkEPHaste.Name = "chkEPHaste"
		Me.chkEPHaste.Size = New System.Drawing.Size(104, 24)
		Me.chkEPHaste.TabIndex = 10
		Me.chkEPHaste.Text = "Haste Rating"
		Me.chkEPHaste.UseVisualStyleBackColor = true
		'
		'chkEPCrit
		'
		Me.chkEPCrit.Checked = true
		Me.chkEPCrit.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPCrit.Location = New System.Drawing.Point(6, 79)
		Me.chkEPCrit.Name = "chkEPCrit"
		Me.chkEPCrit.Size = New System.Drawing.Size(104, 24)
		Me.chkEPCrit.TabIndex = 9
		Me.chkEPCrit.Text = "Critical rating"
		Me.chkEPCrit.UseVisualStyleBackColor = true
		'
		'chkEPAgility
		'
		Me.chkEPAgility.Checked = true
		Me.chkEPAgility.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPAgility.Location = New System.Drawing.Point(6, 49)
		Me.chkEPAgility.Name = "chkEPAgility"
		Me.chkEPAgility.Size = New System.Drawing.Size(104, 24)
		Me.chkEPAgility.TabIndex = 12
		Me.chkEPAgility.Text = "Agility"
		Me.chkEPAgility.UseVisualStyleBackColor = true
		'
		'chkEPStr
		'
		Me.chkEPStr.Checked = true
		Me.chkEPStr.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPStr.Location = New System.Drawing.Point(6, 19)
		Me.chkEPStr.Name = "chkEPStr"
		Me.chkEPStr.Size = New System.Drawing.Size(104, 24)
		Me.chkEPStr.TabIndex = 11
		Me.chkEPStr.Text = "Strength"
		Me.chkEPStr.UseVisualStyleBackColor = true
		'
		'tbBuff
		'
		Me.tbBuff.Controls.Add(Me.grpBuff)
		Me.tbBuff.Location = New System.Drawing.Point(4, 22)
		Me.tbBuff.Name = "tbBuff"
		Me.tbBuff.Size = New System.Drawing.Size(688, 471)
		Me.tbBuff.TabIndex = 8
		Me.tbBuff.Text = "Buffs"
		Me.tbBuff.UseVisualStyleBackColor = true
		'
		'grpBuff
		'
		Me.grpBuff.Controls.Add(Me.chkBloodlust)
		Me.grpBuff.Controls.Add(Me.chkDraeni)
		Me.grpBuff.Controls.Add(Me.chkBStatMulti)
		Me.grpBuff.Controls.Add(Me.chkBStatAdd)
		Me.grpBuff.Controls.Add(Me.chkBSpellHaste)
		Me.grpBuff.Controls.Add(Me.chkBSpellCrit)
		Me.grpBuff.Controls.Add(Me.chkBSpHitTaken)
		Me.grpBuff.Controls.Add(Me.chkBMeleeCrit)
		Me.grpBuff.Controls.Add(Me.chkBSpDamTaken)
		Me.grpBuff.Controls.Add(Me.chkBMeleeHaste)
		Me.grpBuff.Controls.Add(Me.chkBSpCrTaken)
		Me.grpBuff.Controls.Add(Me.chkBHaste)
		Me.grpBuff.Controls.Add(Me.chkBPhyVuln)
		Me.grpBuff.Controls.Add(Me.chkBPcDamage)
		Me.grpBuff.Controls.Add(Me.chkBCritchanceTaken)
		Me.grpBuff.Controls.Add(Me.chkBAPPc)
		Me.grpBuff.Controls.Add(Me.chkBArmorMinor)
		Me.grpBuff.Controls.Add(Me.chkBAP)
		Me.grpBuff.Controls.Add(Me.chkBArmorMaj)
		Me.grpBuff.Controls.Add(Me.chkBStrAgi)
		Me.grpBuff.Location = New System.Drawing.Point(30, 32)
		Me.grpBuff.Name = "grpBuff"
		Me.grpBuff.Size = New System.Drawing.Size(607, 436)
		Me.grpBuff.TabIndex = 0
		Me.grpBuff.TabStop = false
		Me.grpBuff.Text = "Buffs/Debuffs"
		'
		'chkBloodlust
		'
		Me.chkBloodlust.Checked = true
		Me.chkBloodlust.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBloodlust.Location = New System.Drawing.Point(10, 380)
		Me.chkBloodlust.Name = "chkBloodlust"
		Me.chkBloodlust.Size = New System.Drawing.Size(134, 24)
		Me.chkBloodlust.TabIndex = 0
		Me.chkBloodlust.Text = "Bloodlust"
		Me.chkBloodlust.UseVisualStyleBackColor = true
		'
		'chkDraeni
		'
		Me.chkDraeni.Checked = true
		Me.chkDraeni.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkDraeni.Location = New System.Drawing.Point(10, 350)
		Me.chkDraeni.Name = "chkDraeni"
		Me.chkDraeni.Size = New System.Drawing.Size(134, 24)
		Me.chkDraeni.TabIndex = 0
		Me.chkDraeni.Text = "Draenei"
		Me.chkDraeni.UseVisualStyleBackColor = true
		'
		'chkBStatMulti
		'
		Me.chkBStatMulti.Checked = true
		Me.chkBStatMulti.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBStatMulti.Location = New System.Drawing.Point(10, 320)
		Me.chkBStatMulti.Name = "chkBStatMulti"
		Me.chkBStatMulti.Size = New System.Drawing.Size(104, 24)
		Me.chkBStatMulti.TabIndex = 0
		Me.chkBStatMulti.Text = "10% Stat"
		Me.chkBStatMulti.UseVisualStyleBackColor = true
		'
		'chkBStatAdd
		'
		Me.chkBStatAdd.Checked = true
		Me.chkBStatAdd.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBStatAdd.Location = New System.Drawing.Point(10, 290)
		Me.chkBStatAdd.Name = "chkBStatAdd"
		Me.chkBStatAdd.Size = New System.Drawing.Size(104, 24)
		Me.chkBStatAdd.TabIndex = 0
		Me.chkBStatAdd.Text = "Stat Add"
		Me.chkBStatAdd.UseVisualStyleBackColor = true
		'
		'chkBSpellHaste
		'
		Me.chkBSpellHaste.Checked = true
		Me.chkBSpellHaste.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBSpellHaste.Location = New System.Drawing.Point(10, 260)
		Me.chkBSpellHaste.Name = "chkBSpellHaste"
		Me.chkBSpellHaste.Size = New System.Drawing.Size(104, 24)
		Me.chkBSpellHaste.TabIndex = 0
		Me.chkBSpellHaste.Text = "Spell Haste"
		Me.chkBSpellHaste.UseVisualStyleBackColor = true
		'
		'chkBSpellCrit
		'
		Me.chkBSpellCrit.Checked = true
		Me.chkBSpellCrit.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBSpellCrit.Location = New System.Drawing.Point(10, 230)
		Me.chkBSpellCrit.Name = "chkBSpellCrit"
		Me.chkBSpellCrit.Size = New System.Drawing.Size(104, 24)
		Me.chkBSpellCrit.TabIndex = 0
		Me.chkBSpellCrit.Text = "Spell crit"
		Me.chkBSpellCrit.UseVisualStyleBackColor = true
		'
		'chkBSpHitTaken
		'
		Me.chkBSpHitTaken.Checked = true
		Me.chkBSpHitTaken.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBSpHitTaken.Location = New System.Drawing.Point(177, 200)
		Me.chkBSpHitTaken.Name = "chkBSpHitTaken"
		Me.chkBSpHitTaken.Size = New System.Drawing.Size(104, 24)
		Me.chkBSpHitTaken.TabIndex = 0
		Me.chkBSpHitTaken.Text = "Spell Hit taken"
		Me.chkBSpHitTaken.UseVisualStyleBackColor = true
		'
		'chkBMeleeCrit
		'
		Me.chkBMeleeCrit.Checked = true
		Me.chkBMeleeCrit.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBMeleeCrit.Location = New System.Drawing.Point(10, 200)
		Me.chkBMeleeCrit.Name = "chkBMeleeCrit"
		Me.chkBMeleeCrit.Size = New System.Drawing.Size(104, 24)
		Me.chkBMeleeCrit.TabIndex = 0
		Me.chkBMeleeCrit.Text = "Melee crit"
		Me.chkBMeleeCrit.UseVisualStyleBackColor = true
		'
		'chkBSpDamTaken
		'
		Me.chkBSpDamTaken.Checked = true
		Me.chkBSpDamTaken.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBSpDamTaken.Location = New System.Drawing.Point(177, 170)
		Me.chkBSpDamTaken.Name = "chkBSpDamTaken"
		Me.chkBSpDamTaken.Size = New System.Drawing.Size(138, 24)
		Me.chkBSpDamTaken.TabIndex = 0
		Me.chkBSpDamTaken.Text = "Spell Damage taken"
		Me.chkBSpDamTaken.UseVisualStyleBackColor = true
		'
		'chkBMeleeHaste
		'
		Me.chkBMeleeHaste.Checked = true
		Me.chkBMeleeHaste.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBMeleeHaste.Location = New System.Drawing.Point(10, 170)
		Me.chkBMeleeHaste.Name = "chkBMeleeHaste"
		Me.chkBMeleeHaste.Size = New System.Drawing.Size(104, 24)
		Me.chkBMeleeHaste.TabIndex = 0
		Me.chkBMeleeHaste.Text = "Melee Haste"
		Me.chkBMeleeHaste.UseVisualStyleBackColor = true
		'
		'chkBSpCrTaken
		'
		Me.chkBSpCrTaken.Checked = true
		Me.chkBSpCrTaken.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBSpCrTaken.Location = New System.Drawing.Point(177, 140)
		Me.chkBSpCrTaken.Name = "chkBSpCrTaken"
		Me.chkBSpCrTaken.Size = New System.Drawing.Size(150, 24)
		Me.chkBSpCrTaken.TabIndex = 0
		Me.chkBSpCrTaken.Text = "Spell crit chance taken"
		Me.chkBSpCrTaken.UseVisualStyleBackColor = true
		'
		'chkBHaste
		'
		Me.chkBHaste.Checked = true
		Me.chkBHaste.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBHaste.Location = New System.Drawing.Point(10, 140)
		Me.chkBHaste.Name = "chkBHaste"
		Me.chkBHaste.Size = New System.Drawing.Size(104, 24)
		Me.chkBHaste.TabIndex = 0
		Me.chkBHaste.Text = "chkBHaste"
		Me.chkBHaste.UseVisualStyleBackColor = true
		'
		'chkBPhyVuln
		'
		Me.chkBPhyVuln.Checked = true
		Me.chkBPhyVuln.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBPhyVuln.Location = New System.Drawing.Point(177, 110)
		Me.chkBPhyVuln.Name = "chkBPhyVuln"
		Me.chkBPhyVuln.Size = New System.Drawing.Size(150, 24)
		Me.chkBPhyVuln.TabIndex = 0
		Me.chkBPhyVuln.Text = "Physical Vulnerability"
		Me.chkBPhyVuln.UseVisualStyleBackColor = true
		'
		'chkBPcDamage
		'
		Me.chkBPcDamage.Checked = true
		Me.chkBPcDamage.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBPcDamage.Location = New System.Drawing.Point(10, 110)
		Me.chkBPcDamage.Name = "chkBPcDamage"
		Me.chkBPcDamage.Size = New System.Drawing.Size(104, 24)
		Me.chkBPcDamage.TabIndex = 0
		Me.chkBPcDamage.Text = "% Damage"
		Me.chkBPcDamage.UseVisualStyleBackColor = true
		'
		'chkBCritchanceTaken
		'
		Me.chkBCritchanceTaken.Checked = true
		Me.chkBCritchanceTaken.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBCritchanceTaken.Location = New System.Drawing.Point(177, 80)
		Me.chkBCritchanceTaken.Name = "chkBCritchanceTaken"
		Me.chkBCritchanceTaken.Size = New System.Drawing.Size(138, 24)
		Me.chkBCritchanceTaken.TabIndex = 0
		Me.chkBCritchanceTaken.Text = "Crit chance taken"
		Me.chkBCritchanceTaken.UseVisualStyleBackColor = true
		'
		'chkBAPPc
		'
		Me.chkBAPPc.Checked = true
		Me.chkBAPPc.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBAPPc.Location = New System.Drawing.Point(10, 80)
		Me.chkBAPPc.Name = "chkBAPPc"
		Me.chkBAPPc.Size = New System.Drawing.Size(104, 24)
		Me.chkBAPPc.TabIndex = 0
		Me.chkBAPPc.Text = "% Attack Power"
		Me.chkBAPPc.UseVisualStyleBackColor = true
		'
		'chkBArmorMinor
		'
		Me.chkBArmorMinor.Checked = true
		Me.chkBArmorMinor.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBArmorMinor.Location = New System.Drawing.Point(177, 50)
		Me.chkBArmorMinor.Name = "chkBArmorMinor"
		Me.chkBArmorMinor.Size = New System.Drawing.Size(104, 24)
		Me.chkBArmorMinor.TabIndex = 0
		Me.chkBArmorMinor.Text = "Armor Minor"
		Me.chkBArmorMinor.UseVisualStyleBackColor = true
		'
		'chkBAP
		'
		Me.chkBAP.Checked = true
		Me.chkBAP.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBAP.Location = New System.Drawing.Point(10, 50)
		Me.chkBAP.Name = "chkBAP"
		Me.chkBAP.Size = New System.Drawing.Size(104, 24)
		Me.chkBAP.TabIndex = 0
		Me.chkBAP.Text = "+ Attack power"
		Me.chkBAP.UseVisualStyleBackColor = true
		'
		'chkBArmorMaj
		'
		Me.chkBArmorMaj.Checked = true
		Me.chkBArmorMaj.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBArmorMaj.Location = New System.Drawing.Point(177, 20)
		Me.chkBArmorMaj.Name = "chkBArmorMaj"
		Me.chkBArmorMaj.Size = New System.Drawing.Size(104, 24)
		Me.chkBArmorMaj.TabIndex = 0
		Me.chkBArmorMaj.Text = "Armor Major"
		Me.chkBArmorMaj.UseVisualStyleBackColor = true
		'
		'chkBStrAgi
		'
		Me.chkBStrAgi.Checked = true
		Me.chkBStrAgi.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkBStrAgi.Location = New System.Drawing.Point(10, 20)
		Me.chkBStrAgi.Name = "chkBStrAgi"
		Me.chkBStrAgi.Size = New System.Drawing.Size(104, 24)
		Me.chkBStrAgi.TabIndex = 0
		Me.chkBStrAgi.Text = "Str/Agi"
		Me.chkBStrAgi.UseVisualStyleBackColor = true
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(696, 567)
		Me.Controls.Add(Me.lblDPS)
		Me.Controls.Add(Me.lblVersion)
		Me.Controls.Add(Me.btEP)
		Me.Controls.Add(Me.tabControl1)
		Me.Controls.Add(Me.PBsim)
		Me.Controls.Add(Me.button1)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "Kahorie's DK Simulator"
		AddHandler Load, AddressOf Me.MainFormLoad
		AddHandler Closing, AddressOf Me.MainFormClose
		Me.tbTools.ResumeLayout(false)
		Me.tbTools.PerformLayout
		Me.tabPage1.ResumeLayout(false)
		Me.HtmlReport.ResumeLayout(false)
		Me.tabPage3.ResumeLayout(false)
		Me.tabPage3.PerformLayout
		Me.tabControl1.ResumeLayout(false)
		Me.Test.ResumeLayout(false)
		Me.tbEPOptions.ResumeLayout(false)
		Me.groupBox2.ResumeLayout(false)
		Me.groupBox1.ResumeLayout(false)
		Me.tbBuff.ResumeLayout(false)
		Me.grpBuff.ResumeLayout(false)
		Me.ResumeLayout(false)
	End Sub
	Friend txtInterruptAmount As System.Windows.Forms.TextBox
	Friend txtInterruptCd As System.Windows.Forms.TextBox
	Private label16 As System.Windows.Forms.Label
	Private label15 As System.Windows.Forms.Label
	Private label17 As System.Windows.Forms.Label
	Private label13 As System.Windows.Forms.Label
	Friend txtAMSrp As System.Windows.Forms.TextBox
	Friend txtAMScd As System.Windows.Forms.TextBox
	Private label14 As System.Windows.Forms.Label
	Private chkBStatMulti As System.Windows.Forms.CheckBox
	Private chkBStatAdd As System.Windows.Forms.CheckBox
	Private chkBSpHitTaken As System.Windows.Forms.CheckBox
	Private chkBSpDamTaken As System.Windows.Forms.CheckBox
	Private chkBSpCrTaken As System.Windows.Forms.CheckBox
	Private chkBPhyVuln As System.Windows.Forms.CheckBox
	Private chkBCritchanceTaken As System.Windows.Forms.CheckBox
	Private chkBArmorMinor As System.Windows.Forms.CheckBox
	Private chkBArmorMaj As System.Windows.Forms.CheckBox
	Private chkDraeni As System.Windows.Forms.CheckBox
	Private chkBloodlust As System.Windows.Forms.CheckBox
	Private chkBStrAgi As System.Windows.Forms.CheckBox
	Private chkBAP As System.Windows.Forms.CheckBox
	Private chkBAPPc As System.Windows.Forms.CheckBox
	Private chkBPcDamage As System.Windows.Forms.CheckBox
	Private chkBHaste As System.Windows.Forms.CheckBox
	Private chkBMeleeHaste As System.Windows.Forms.CheckBox
	Private chkBMeleeCrit As System.Windows.Forms.CheckBox
	Private chkBSpellCrit As System.Windows.Forms.CheckBox
	Private chkBSpellHaste As System.Windows.Forms.CheckBox
	Private grpBuff As System.Windows.Forms.GroupBox
	Private tbBuff As System.Windows.Forms.TabPage
	Friend chkEPSMHDPS As System.Windows.Forms.CheckBox
	Friend chkEPSMHSpeed As System.Windows.Forms.CheckBox
	Friend tabControl1 As System.Windows.Forms.TabControl
	Private tbEPOptions As System.Windows.Forms.TabPage
	Friend chkEPStr As System.Windows.Forms.CheckBox
	Friend chkEPAgility As System.Windows.Forms.CheckBox
	Friend chkEPCrit As System.Windows.Forms.CheckBox
	Friend chkEPHaste As System.Windows.Forms.CheckBox
	Friend chkEPArP As System.Windows.Forms.CheckBox
	Friend chkEPHit As System.Windows.Forms.CheckBox
	Friend chkEPSpHit As System.Windows.Forms.CheckBox
	Friend chkEPExp As System.Windows.Forms.CheckBox
	Friend chkEP2T7 As System.Windows.Forms.CheckBox
	Friend chkEP4PT7 As System.Windows.Forms.CheckBox
	Friend chkEP2PT8 As System.Windows.Forms.CheckBox
	Friend chkEP4PT8 As System.Windows.Forms.CheckBox
	Friend chkEP2PT9 As System.Windows.Forms.CheckBox
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private cmdSaveNewTalent As System.Windows.Forms.Button
	Private cmdSaveTemplate As System.Windows.Forms.Button
	Private chkGhoulHaste As System.Windows.Forms.CheckBox
	Private Test As System.Windows.Forms.TabPage
	Private label12 As System.Windows.Forms.Label
	Private txtArmory As System.Windows.Forms.TextBox
	Private cmdImportArmory As System.Windows.Forms.Button
	Private chkLissage As System.Windows.Forms.CheckBox
	Private wbTemplate As System.Windows.Forms.WebBrowser
	Private txtImportTemplate As System.Windows.Forms.TextBox
	Private cmdImportTemplate As System.Windows.Forms.Button
	Private label11 As System.Windows.Forms.Label
	Private tbTools As System.Windows.Forms.TabPage
	Friend txtLatency As System.Windows.Forms.TextBox
	Private label10 As System.Windows.Forms.Label
	Friend chkWaitFC As System.Windows.Forms.CheckBox
	Friend lblDPS As System.Windows.Forms.Label
	Friend cmbCharacter As System.Windows.Forms.ComboBox
	Private label9 As System.Windows.Forms.Label
	Private cmdEditChar As System.Windows.Forms.Button
	Private cmdSaveNew As System.Windows.Forms.Button
	Private cmdSave As System.Windows.Forms.Button
	Private rtfEditor As System.Windows.Forms.RichTextBox
	Private tabPage1 As System.Windows.Forms.TabPage
	Private cmdEditTemplate As System.Windows.Forms.Button
	Private cmdEditPrio As System.Windows.Forms.Button
	Private cmdEditRot As System.Windows.Forms.Button
	Private ckLogRP As System.Windows.Forms.CheckBox
	Friend webBrowser1 As System.Windows.Forms.WebBrowser
	Private HtmlReport As System.Windows.Forms.TabPage
	Friend ckPet As System.Windows.Forms.CheckBox
	Private chkCombatLog As System.Windows.Forms.CheckBox
	Friend cmbRuneOH As System.Windows.Forms.ComboBox
	Private label8 As System.Windows.Forms.Label
	Friend cmbRuneMH As System.Windows.Forms.ComboBox
	Private label7 As System.Windows.Forms.Label
	Friend cmbRotation As System.Windows.Forms.ComboBox
	Private label6 As System.Windows.Forms.Label
	Friend cmbSigils As System.Windows.Forms.ComboBox
	Private label5 As System.Windows.Forms.Label
	Private lblVersion As System.Windows.Forms.Label
	Friend txtSimtime As System.Windows.Forms.TextBox
	Private label4 As System.Windows.Forms.Label
	Friend cmdPresence As System.Windows.Forms.ComboBox
	Private label3 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Friend cmbPrio As System.Windows.Forms.ComboBox
	Friend cmbTemplate As System.Windows.Forms.ComboBox
	Private btEP As System.Windows.Forms.Button
	Private tabPage3 As System.Windows.Forms.TabPage
	Private PBsim As System.Windows.Forms.ProgressBar
	Private button1 As System.Windows.Forms.Button
	
End Class
