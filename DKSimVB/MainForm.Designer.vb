﻿'
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
		Me.components = New System.ComponentModel.Container
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
		Me.label20 = New System.Windows.Forms.Label
		Me.cmbBShOption = New System.Windows.Forms.ComboBox
		Me.chkDisease = New System.Windows.Forms.CheckBox
		Me.txtNumberOfEnemies = New System.Windows.Forms.TextBox
		Me.label19 = New System.Windows.Forms.Label
		Me.rdPrio = New System.Windows.Forms.RadioButton
		Me.rdRot = New System.Windows.Forms.RadioButton
		Me.label18 = New System.Windows.Forms.Label
		Me.txtManyFights = New System.Windows.Forms.TextBox
		Me.chkManyFights = New System.Windows.Forms.CheckBox
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
		Me.tbEPOptions = New System.Windows.Forms.TabPage
		Me.groupBox3 = New System.Windows.Forms.GroupBox
		Me.chkEPComet = New System.Windows.Forms.CheckBox
		Me.chkEPDarkMatter = New System.Windows.Forms.CheckBox
		Me.chkEPBandit = New System.Windows.Forms.CheckBox
		Me.chkEPNecromantic = New System.Windows.Forms.CheckBox
		Me.chkEPVictory = New System.Windows.Forms.CheckBox
		Me.chkEPDCDeath = New System.Windows.Forms.CheckBox
		Me.chkEPGreatness = New System.Windows.Forms.CheckBox
		Me.chkEPDeathChoice = New System.Windows.Forms.CheckBox
		Me.chkEPPyrite = New System.Windows.Forms.CheckBox
		Me.chkEPOldGod = New System.Windows.Forms.CheckBox
		Me.chkEPMirror = New System.Windows.Forms.CheckBox
		Me.chkEPBitterAnguish = New System.Windows.Forms.CheckBox
		Me.chkEPGrimToll = New System.Windows.Forms.CheckBox
		Me.chkEPMjolRune = New System.Windows.Forms.CheckBox
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.chkEP4PT9 = New System.Windows.Forms.CheckBox
		Me.chkEP2PT9 = New System.Windows.Forms.CheckBox
		Me.chkEP4PT8 = New System.Windows.Forms.CheckBox
		Me.chkEP2PT8 = New System.Windows.Forms.CheckBox
		Me.chkEP4PT7 = New System.Windows.Forms.CheckBox
		Me.chkEP2T7 = New System.Windows.Forms.CheckBox
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.chkEPSMHSpeed = New System.Windows.Forms.CheckBox
		Me.chkEPExp = New System.Windows.Forms.CheckBox
		Me.chkEPSMHDPS = New System.Windows.Forms.CheckBox
		Me.chkEPAfterSpellHitRating = New System.Windows.Forms.CheckBox
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
		Me.chkCrypticFever = New System.Windows.Forms.CheckBox
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
		Me.tbTpl = New System.Windows.Forms.TabPage
		Me.lblUnholy = New System.Windows.Forms.Label
		Me.lblFrost = New System.Windows.Forms.Label
		Me.lblBlood = New System.Windows.Forms.Label
		Me.cmbGlyph3 = New System.Windows.Forms.ComboBox
		Me.cmbGlyph2 = New System.Windows.Forms.ComboBox
		Me.cmbGlyph1 = New System.Windows.Forms.ComboBox
		Me.cmdSaveNewTemplate = New System.Windows.Forms.Button
		Me.cmdSaveTemplate = New System.Windows.Forms.Button
		Me.tbCaling = New System.Windows.Forms.TabPage
		Me.label21 = New System.Windows.Forms.Label
		Me.cmdScaling = New System.Windows.Forms.Button
		Me.gbScaling = New System.Windows.Forms.GroupBox
		Me.chkScaExp = New System.Windows.Forms.CheckBox
		Me.chkScaHit = New System.Windows.Forms.CheckBox
		Me.chkScaArP = New System.Windows.Forms.CheckBox
		Me.chkScaHaste = New System.Windows.Forms.CheckBox
		Me.chkScaCrit = New System.Windows.Forms.CheckBox
		Me.chkScaAgility = New System.Windows.Forms.CheckBox
		Me.chkScaStr = New System.Windows.Forms.CheckBox
		Me.tbTank = New System.Windows.Forms.TabPage
		Me.gbTank = New System.Windows.Forms.GroupBox
		Me.label24 = New System.Windows.Forms.Label
		Me.label23 = New System.Windows.Forms.Label
		Me.txtFBAvoidance = New System.Windows.Forms.TextBox
		Me.txtFPBossSwing = New System.Windows.Forms.TextBox
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.chkScaCritA = New System.Windows.Forms.CheckBox
		Me.chkScaHasteA = New System.Windows.Forms.CheckBox
		Me.chkScaArPA = New System.Windows.Forms.CheckBox
		Me.chkScaHitA = New System.Windows.Forms.CheckBox
		Me.chkScaExpA = New System.Windows.Forms.CheckBox
		Me.tbTools.SuspendLayout
		Me.tabPage1.SuspendLayout
		Me.HtmlReport.SuspendLayout
		Me.tabPage3.SuspendLayout
		Me.tabControl1.SuspendLayout
		Me.tbEPOptions.SuspendLayout
		Me.groupBox3.SuspendLayout
		Me.groupBox2.SuspendLayout
		Me.groupBox1.SuspendLayout
		Me.tbBuff.SuspendLayout
		Me.grpBuff.SuspendLayout
		Me.tbTpl.SuspendLayout
		Me.tbCaling.SuspendLayout
		Me.gbScaling.SuspendLayout
		Me.tbTank.SuspendLayout
		Me.gbTank.SuspendLayout
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
		Me.PBsim.Size = New System.Drawing.Size(699, 23)
		Me.PBsim.TabIndex = 2
		'
		'btEP
		'
		Me.btEP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.btEP.Location = New System.Drawing.Point(438, 515)
		Me.btEP.Name = "btEP"
		Me.btEP.Size = New System.Drawing.Size(245, 23)
		Me.btEP.TabIndex = 4
		Me.btEP.Text = "Start EP"
		Me.btEP.UseVisualStyleBackColor = true
		AddHandler Me.btEP.Click, AddressOf Me.BtEPClick
		'
		'lblVersion
		'
		Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblVersion.Location = New System.Drawing.Point(532, 0)
		Me.lblVersion.Name = "lblVersion"
		Me.lblVersion.Size = New System.Drawing.Size(152, 19)
		Me.lblVersion.TabIndex = 8
		Me.lblVersion.Text = "Version "
		Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblDPS
		'
		Me.lblDPS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblDPS.BackColor = System.Drawing.Color.Transparent
		Me.lblDPS.Location = New System.Drawing.Point(297, 528)
		Me.lblDPS.Name = "lblDPS"
		Me.lblDPS.Size = New System.Drawing.Size(103, 13)
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
		Me.tbTools.Size = New System.Drawing.Size(691, 471)
		Me.tbTools.TabIndex = 5
		Me.tbTools.Text = "Tools"
		Me.tbTools.UseVisualStyleBackColor = true
		'
		'cmdImportArmory
		'
		Me.cmdImportArmory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.cmdImportArmory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.cmdImportArmory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.cmdImportArmory.ForeColor = System.Drawing.Color.Ivory
		Me.cmdImportArmory.Image = CType(resources.GetObject("cmdImportArmory.Image"),System.Drawing.Image)
		Me.cmdImportArmory.Location = New System.Drawing.Point(635, 67)
		Me.cmdImportArmory.Name = "cmdImportArmory"
		Me.cmdImportArmory.Size = New System.Drawing.Size(45, 36)
		Me.cmdImportArmory.TabIndex = 5
		Me.cmdImportArmory.Text = "Ok"
		Me.cmdImportArmory.TextAlign = System.Drawing.ContentAlignment.BottomRight
		Me.cmdImportArmory.UseVisualStyleBackColor = false
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
		Me.cmdImportTemplate.Location = New System.Drawing.Point(637, 18)
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
		Me.txtImportTemplate.Size = New System.Drawing.Size(623, 20)
		Me.txtImportTemplate.TabIndex = 0
		'
		'tabPage1
		'
		Me.tabPage1.Controls.Add(Me.cmdSaveNew)
		Me.tabPage1.Controls.Add(Me.cmdSave)
		Me.tabPage1.Controls.Add(Me.rtfEditor)
		Me.tabPage1.Location = New System.Drawing.Point(4, 22)
		Me.tabPage1.Name = "tabPage1"
		Me.tabPage1.Size = New System.Drawing.Size(691, 471)
		Me.tabPage1.TabIndex = 4
		Me.tabPage1.Text = "Editor"
		Me.tabPage1.UseVisualStyleBackColor = true
		'
		'cmdSaveNew
		'
		Me.cmdSaveNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdSaveNew.Location = New System.Drawing.Point(599, 0)
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
		Me.cmdSave.Location = New System.Drawing.Point(509, 0)
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
		Me.rtfEditor.Size = New System.Drawing.Size(685, 448)
		Me.rtfEditor.TabIndex = 0
		Me.rtfEditor.Text = ""
		'
		'HtmlReport
		'
		Me.HtmlReport.Controls.Add(Me.webBrowser1)
		Me.HtmlReport.Location = New System.Drawing.Point(4, 22)
		Me.HtmlReport.Name = "HtmlReport"
		Me.HtmlReport.Size = New System.Drawing.Size(691, 471)
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
		Me.webBrowser1.Size = New System.Drawing.Size(691, 471)
		Me.webBrowser1.TabIndex = 0
		'
		'tabPage3
		'
		Me.tabPage3.Controls.Add(Me.label20)
		Me.tabPage3.Controls.Add(Me.cmbBShOption)
		Me.tabPage3.Controls.Add(Me.chkDisease)
		Me.tabPage3.Controls.Add(Me.txtNumberOfEnemies)
		Me.tabPage3.Controls.Add(Me.label19)
		Me.tabPage3.Controls.Add(Me.rdPrio)
		Me.tabPage3.Controls.Add(Me.rdRot)
		Me.tabPage3.Controls.Add(Me.label18)
		Me.tabPage3.Controls.Add(Me.txtManyFights)
		Me.tabPage3.Controls.Add(Me.chkManyFights)
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
		Me.tabPage3.Size = New System.Drawing.Size(691, 471)
		Me.tabPage3.TabIndex = 2
		Me.tabPage3.Text = "Configuration"
		Me.tabPage3.UseVisualStyleBackColor = true
		'
		'label20
		'
		Me.label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label20.Location = New System.Drawing.Point(366, 365)
		Me.label20.Name = "label20"
		Me.label20.Size = New System.Drawing.Size(127, 21)
		Me.label20.TabIndex = 41
		Me.label20.Text = "Use Bone shield/UA"
		AddHandler Me.label20.Click, AddressOf Me.Label20Click
		'
		'cmbBShOption
		'
		Me.cmbBShOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbBShOption.FormattingEnabled = true
		Me.cmbBShOption.Location = New System.Drawing.Point(494, 365)
		Me.cmbBShOption.Name = "cmbBShOption"
		Me.cmbBShOption.Size = New System.Drawing.Size(149, 21)
		Me.cmbBShOption.TabIndex = 40
		'
		'chkDisease
		'
		Me.chkDisease.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkDisease.Checked = true
		Me.chkDisease.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkDisease.Location = New System.Drawing.Point(585, 329)
		Me.chkDisease.Name = "chkDisease"
		Me.chkDisease.Size = New System.Drawing.Size(98, 41)
		Me.chkDisease.TabIndex = 39
		Me.chkDisease.Text = "Keep disease on targets"
		Me.chkDisease.UseVisualStyleBackColor = true
		'
		'txtNumberOfEnemies
		'
		Me.txtNumberOfEnemies.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtNumberOfEnemies.Location = New System.Drawing.Point(529, 335)
		Me.txtNumberOfEnemies.Name = "txtNumberOfEnemies"
		Me.txtNumberOfEnemies.Size = New System.Drawing.Size(50, 20)
		Me.txtNumberOfEnemies.TabIndex = 38
		Me.txtNumberOfEnemies.Text = "1"
		AddHandler Me.txtNumberOfEnemies.TextChanged, AddressOf Me.TxtNumberOfEnemiesTextChanged
		'
		'label19
		'
		Me.label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label19.Location = New System.Drawing.Point(421, 338)
		Me.label19.Name = "label19"
		Me.label19.Size = New System.Drawing.Size(102, 13)
		Me.label19.TabIndex = 37
		Me.label19.Text = "Number of enemies"
		'
		'rdPrio
		'
		Me.rdPrio.Location = New System.Drawing.Point(6, 94)
		Me.rdPrio.Name = "rdPrio"
		Me.rdPrio.Size = New System.Drawing.Size(104, 24)
		Me.rdPrio.TabIndex = 36
		Me.rdPrio.TabStop = true
		Me.rdPrio.Text = "Priority"
		Me.rdPrio.UseVisualStyleBackColor = true
		AddHandler Me.rdPrio.CheckedChanged, AddressOf Me.RdPrioCheckedChanged
		'
		'rdRot
		'
		Me.rdRot.Location = New System.Drawing.Point(112, 94)
		Me.rdRot.Name = "rdRot"
		Me.rdRot.Size = New System.Drawing.Size(104, 24)
		Me.rdRot.TabIndex = 36
		Me.rdRot.TabStop = true
		Me.rdRot.Text = "Rotation"
		Me.rdRot.UseVisualStyleBackColor = true
		AddHandler Me.rdRot.CheckedChanged, AddressOf Me.RdRotCheckedChanged
		'
		'label18
		'
		Me.label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label18.Location = New System.Drawing.Point(112, 449)
		Me.label18.Name = "label18"
		Me.label18.Size = New System.Drawing.Size(79, 13)
		Me.label18.TabIndex = 35
		Me.label18.Text = "s long fights"
		'
		'txtManyFights
		'
		Me.txtManyFights.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtManyFights.Location = New System.Drawing.Point(56, 446)
		Me.txtManyFights.Name = "txtManyFights"
		Me.txtManyFights.Size = New System.Drawing.Size(50, 20)
		Me.txtManyFights.TabIndex = 34
		Me.txtManyFights.Text = "350"
		'
		'chkManyFights
		'
		Me.chkManyFights.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.chkManyFights.Location = New System.Drawing.Point(6, 444)
		Me.chkManyFights.Name = "chkManyFights"
		Me.chkManyFights.Size = New System.Drawing.Size(60, 24)
		Me.chkManyFights.TabIndex = 33
		Me.chkManyFights.Text = "Many"
		Me.chkManyFights.UseVisualStyleBackColor = true
		'
		'label17
		'
		Me.label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label17.Location = New System.Drawing.Point(6, 397)
		Me.label17.Name = "label17"
		Me.label17.Size = New System.Drawing.Size(116, 13)
		Me.label17.TabIndex = 32
		Me.label17.Text = "Interrupt fighting every"
		'
		'label15
		'
		Me.label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label15.Location = New System.Drawing.Point(278, 397)
		Me.label15.Name = "label15"
		Me.label15.Size = New System.Drawing.Size(16, 13)
		Me.label15.TabIndex = 31
		Me.label15.Text = "s"
		'
		'txtInterruptAmount
		'
		Me.txtInterruptAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtInterruptAmount.Enabled = false
		Me.txtInterruptAmount.Location = New System.Drawing.Point(222, 394)
		Me.txtInterruptAmount.Name = "txtInterruptAmount"
		Me.txtInterruptAmount.Size = New System.Drawing.Size(50, 20)
		Me.txtInterruptAmount.TabIndex = 30
		Me.txtInterruptAmount.Text = "0"
		'
		'txtInterruptCd
		'
		Me.txtInterruptCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtInterruptCd.Enabled = false
		Me.txtInterruptCd.Location = New System.Drawing.Point(128, 394)
		Me.txtInterruptCd.Name = "txtInterruptCd"
		Me.txtInterruptCd.Size = New System.Drawing.Size(50, 20)
		Me.txtInterruptCd.TabIndex = 29
		Me.txtInterruptCd.Text = "0"
		'
		'label16
		'
		Me.label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label16.Location = New System.Drawing.Point(184, 397)
		Me.label16.Name = "label16"
		Me.label16.Size = New System.Drawing.Size(32, 13)
		Me.label16.TabIndex = 28
		Me.label16.Text = "s for"
		'
		'label14
		'
		Me.label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label14.Location = New System.Drawing.Point(171, 423)
		Me.label14.Name = "label14"
		Me.label14.Size = New System.Drawing.Size(20, 13)
		Me.label14.TabIndex = 27
		Me.label14.Text = "s"
		'
		'txtAMScd
		'
		Me.txtAMScd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtAMScd.Location = New System.Drawing.Point(115, 420)
		Me.txtAMScd.Name = "txtAMScd"
		Me.txtAMScd.Size = New System.Drawing.Size(50, 20)
		Me.txtAMScd.TabIndex = 26
		Me.txtAMScd.Text = "60"
		'
		'txtAMSrp
		'
		Me.txtAMSrp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.txtAMSrp.Location = New System.Drawing.Point(6, 420)
		Me.txtAMSrp.Name = "txtAMSrp"
		Me.txtAMSrp.Size = New System.Drawing.Size(50, 20)
		Me.txtAMSrp.TabIndex = 25
		Me.txtAMSrp.Text = "0"
		'
		'label13
		'
		Me.label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.label13.Location = New System.Drawing.Point(62, 423)
		Me.label13.Name = "label13"
		Me.label13.Size = New System.Drawing.Size(47, 13)
		Me.label13.TabIndex = 24
		Me.label13.Text = "rp every"
		'
		'chkGhoulHaste
		'
		Me.chkGhoulHaste.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkGhoulHaste.Location = New System.Drawing.Point(494, 392)
		Me.chkGhoulHaste.Name = "chkGhoulHaste"
		Me.chkGhoulHaste.Size = New System.Drawing.Size(194, 24)
		Me.chkGhoulHaste.TabIndex = 23
		Me.chkGhoulHaste.Text = "Ghoul double dips haste buff"
		Me.chkGhoulHaste.UseVisualStyleBackColor = true
		'
		'txtLatency
		'
		Me.txtLatency.Location = New System.Drawing.Point(88, 350)
		Me.txtLatency.Name = "txtLatency"
		Me.txtLatency.Size = New System.Drawing.Size(50, 20)
		Me.txtLatency.TabIndex = 21
		Me.txtLatency.Text = "150"
		AddHandler Me.txtLatency.TextChanged, AddressOf Me.TxtLatencyTextChanged
		'
		'txtSimtime
		'
		Me.txtSimtime.Location = New System.Drawing.Point(112, 324)
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
		Me.chkWaitFC.Location = New System.Drawing.Point(309, 444)
		Me.chkWaitFC.Name = "chkWaitFC"
		Me.chkWaitFC.Size = New System.Drawing.Size(179, 24)
		Me.chkWaitFC.TabIndex = 20
		Me.chkWaitFC.Text = "Wait for FC proc for GG/DRW"
		Me.chkWaitFC.UseVisualStyleBackColor = true
		'
		'cmdEditRot
		'
		Me.cmdEditRot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdEditRot.Location = New System.Drawing.Point(646, 175)
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
		Me.cmdEditPrio.Location = New System.Drawing.Point(646, 135)
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
		Me.cmdEditChar.Location = New System.Drawing.Point(646, 31)
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
		Me.cmdEditTemplate.Location = New System.Drawing.Point(646, 71)
		Me.cmdEditTemplate.Name = "cmdEditTemplate"
		Me.cmdEditTemplate.Size = New System.Drawing.Size(42, 23)
		Me.cmdEditTemplate.TabIndex = 19
		Me.cmdEditTemplate.Text = "Edit"
		Me.cmdEditTemplate.UseVisualStyleBackColor = true
		AddHandler Me.cmdEditTemplate.Click, AddressOf Me.CmdEditTemplateClick
		'
		'ckLogRP
		'
		Me.ckLogRP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.ckLogRP.Enabled = false
		Me.ckLogRP.Location = New System.Drawing.Point(494, 444)
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
		Me.ckPet.Location = New System.Drawing.Point(309, 418)
		Me.ckPet.Name = "ckPet"
		Me.ckPet.Size = New System.Drawing.Size(179, 24)
		Me.ckPet.TabIndex = 17
		Me.ckPet.Text = "Use Pets(Ghoul, Gargoyle)"
		Me.ckPet.UseVisualStyleBackColor = true
		AddHandler Me.ckPet.CheckedChanged, AddressOf Me.CkPetCheckedChanged
		'
		'chkCombatLog
		'
		Me.chkCombatLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkCombatLog.Location = New System.Drawing.Point(494, 418)
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
		Me.label8.Location = New System.Drawing.Point(393, 281)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(100, 13)
		Me.label8.TabIndex = 15
		Me.label8.Text = "OffHand"
		'
		'cmbRuneOH
		'
		Me.cmbRuneOH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbRuneOH.FormattingEnabled = true
		Me.cmbRuneOH.Location = New System.Drawing.Point(393, 297)
		Me.cmbRuneOH.Name = "cmbRuneOH"
		Me.cmbRuneOH.Size = New System.Drawing.Size(250, 21)
		Me.cmbRuneOH.TabIndex = 14
		'
		'label7
		'
		Me.label7.Location = New System.Drawing.Point(6, 281)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(100, 13)
		Me.label7.TabIndex = 13
		Me.label7.Text = "MainHand"
		'
		'cmbRuneMH
		'
		Me.cmbRuneMH.FormattingEnabled = true
		Me.cmbRuneMH.Location = New System.Drawing.Point(6, 297)
		Me.cmbRuneMH.Name = "cmbRuneMH"
		Me.cmbRuneMH.Size = New System.Drawing.Size(250, 21)
		Me.cmbRuneMH.TabIndex = 12
		'
		'label6
		'
		Me.label6.Location = New System.Drawing.Point(6, 161)
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
		Me.cmbRotation.Location = New System.Drawing.Point(6, 177)
		Me.cmbRotation.Name = "cmbRotation"
		Me.cmbRotation.Size = New System.Drawing.Size(637, 21)
		Me.cmbRotation.TabIndex = 10
		'
		'label5
		'
		Me.label5.Location = New System.Drawing.Point(6, 241)
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
		Me.cmbSigils.Location = New System.Drawing.Point(6, 257)
		Me.cmbSigils.Name = "cmbSigils"
		Me.cmbSigils.Size = New System.Drawing.Size(637, 21)
		Me.cmbSigils.TabIndex = 8
		'
		'label10
		'
		Me.label10.Location = New System.Drawing.Point(6, 353)
		Me.label10.Name = "label10"
		Me.label10.Size = New System.Drawing.Size(76, 13)
		Me.label10.TabIndex = 7
		Me.label10.Text = "Latency in ms"
		'
		'label4
		'
		Me.label4.Location = New System.Drawing.Point(6, 327)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(100, 13)
		Me.label4.TabIndex = 7
		Me.label4.Text = "Simulated time in h"
		'
		'label3
		'
		Me.label3.Location = New System.Drawing.Point(6, 201)
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
		Me.cmdPresence.Location = New System.Drawing.Point(6, 217)
		Me.cmdPresence.Name = "cmdPresence"
		Me.cmdPresence.Size = New System.Drawing.Size(637, 21)
		Me.cmdPresence.TabIndex = 4
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(6, 121)
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
		Me.cmbPrio.Location = New System.Drawing.Point(6, 137)
		Me.cmbPrio.Name = "cmbPrio"
		Me.cmbPrio.Size = New System.Drawing.Size(637, 21)
		Me.cmbPrio.TabIndex = 1
		'
		'cmbCharacter
		'
		Me.cmbCharacter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbCharacter.FormattingEnabled = true
		Me.cmbCharacter.Location = New System.Drawing.Point(6, 33)
		Me.cmbCharacter.Name = "cmbCharacter"
		Me.cmbCharacter.Size = New System.Drawing.Size(637, 21)
		Me.cmbCharacter.TabIndex = 0
		'
		'cmbTemplate
		'
		Me.cmbTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbTemplate.FormattingEnabled = true
		Me.cmbTemplate.Location = New System.Drawing.Point(6, 73)
		Me.cmbTemplate.Name = "cmbTemplate"
		Me.cmbTemplate.Size = New System.Drawing.Size(637, 21)
		Me.cmbTemplate.TabIndex = 0
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
		Me.tabControl1.Controls.Add(Me.tbEPOptions)
		Me.tabControl1.Controls.Add(Me.tbBuff)
		Me.tabControl1.Controls.Add(Me.tbTpl)
		Me.tabControl1.Controls.Add(Me.tbCaling)
		Me.tabControl1.Controls.Add(Me.tbTank)
		Me.tabControl1.Location = New System.Drawing.Point(0, 0)
		Me.tabControl1.Name = "tabControl1"
		Me.tabControl1.SelectedIndex = 0
		Me.tabControl1.Size = New System.Drawing.Size(699, 497)
		Me.tabControl1.TabIndex = 3
		'
		'tbEPOptions
		'
		Me.tbEPOptions.Controls.Add(Me.groupBox3)
		Me.tbEPOptions.Controls.Add(Me.groupBox2)
		Me.tbEPOptions.Controls.Add(Me.groupBox1)
		Me.tbEPOptions.Location = New System.Drawing.Point(4, 22)
		Me.tbEPOptions.Name = "tbEPOptions"
		Me.tbEPOptions.Size = New System.Drawing.Size(691, 471)
		Me.tbEPOptions.TabIndex = 7
		Me.tbEPOptions.Text = "EP Options"
		Me.tbEPOptions.UseVisualStyleBackColor = true
		'
		'groupBox3
		'
		Me.groupBox3.Controls.Add(Me.chkEPComet)
		Me.groupBox3.Controls.Add(Me.chkEPDarkMatter)
		Me.groupBox3.Controls.Add(Me.chkEPBandit)
		Me.groupBox3.Controls.Add(Me.chkEPNecromantic)
		Me.groupBox3.Controls.Add(Me.chkEPVictory)
		Me.groupBox3.Controls.Add(Me.chkEPDCDeath)
		Me.groupBox3.Controls.Add(Me.chkEPGreatness)
		Me.groupBox3.Controls.Add(Me.chkEPDeathChoice)
		Me.groupBox3.Controls.Add(Me.chkEPPyrite)
		Me.groupBox3.Controls.Add(Me.chkEPOldGod)
		Me.groupBox3.Controls.Add(Me.chkEPMirror)
		Me.groupBox3.Controls.Add(Me.chkEPBitterAnguish)
		Me.groupBox3.Controls.Add(Me.chkEPGrimToll)
		Me.groupBox3.Controls.Add(Me.chkEPMjolRune)
		Me.groupBox3.Location = New System.Drawing.Point(346, 48)
		Me.groupBox3.Name = "groupBox3"
		Me.groupBox3.Size = New System.Drawing.Size(330, 420)
		Me.groupBox3.TabIndex = 6
		Me.groupBox3.TabStop = false
		Me.groupBox3.Text = "Trinket"
		'
		'chkEPComet
		'
		Me.chkEPComet.Location = New System.Drawing.Point(153, 199)
		Me.chkEPComet.Name = "chkEPComet"
		Me.chkEPComet.Size = New System.Drawing.Size(141, 24)
		Me.chkEPComet.TabIndex = 2
		Me.chkEPComet.Text = "Comet"
		Me.chkEPComet.UseVisualStyleBackColor = true
		'
		'chkEPDarkMatter
		'
		Me.chkEPDarkMatter.Location = New System.Drawing.Point(153, 169)
		Me.chkEPDarkMatter.Name = "chkEPDarkMatter"
		Me.chkEPDarkMatter.Size = New System.Drawing.Size(141, 24)
		Me.chkEPDarkMatter.TabIndex = 2
		Me.chkEPDarkMatter.Text = "DarkMatter"
		Me.chkEPDarkMatter.UseVisualStyleBackColor = true
		'
		'chkEPBandit
		'
		Me.chkEPBandit.Location = New System.Drawing.Point(153, 139)
		Me.chkEPBandit.Name = "chkEPBandit"
		Me.chkEPBandit.Size = New System.Drawing.Size(141, 24)
		Me.chkEPBandit.TabIndex = 2
		Me.chkEPBandit.Text = "Bandit"
		Me.chkEPBandit.UseVisualStyleBackColor = true
		'
		'chkEPNecromantic
		'
		Me.chkEPNecromantic.Location = New System.Drawing.Point(153, 109)
		Me.chkEPNecromantic.Name = "chkEPNecromantic"
		Me.chkEPNecromantic.Size = New System.Drawing.Size(141, 24)
		Me.chkEPNecromantic.TabIndex = 2
		Me.chkEPNecromantic.Text = "Necromantic"
		Me.chkEPNecromantic.UseVisualStyleBackColor = true
		'
		'chkEPVictory
		'
		Me.chkEPVictory.Location = New System.Drawing.Point(153, 79)
		Me.chkEPVictory.Name = "chkEPVictory"
		Me.chkEPVictory.Size = New System.Drawing.Size(141, 24)
		Me.chkEPVictory.TabIndex = 2
		Me.chkEPVictory.Text = "Victory"
		Me.chkEPVictory.UseVisualStyleBackColor = true
		'
		'chkEPDCDeath
		'
		Me.chkEPDCDeath.Location = New System.Drawing.Point(153, 49)
		Me.chkEPDCDeath.Name = "chkEPDCDeath"
		Me.chkEPDCDeath.Size = New System.Drawing.Size(141, 24)
		Me.chkEPDCDeath.TabIndex = 2
		Me.chkEPDCDeath.Text = "Death"
		Me.chkEPDCDeath.UseVisualStyleBackColor = true
		'
		'chkEPGreatness
		'
		Me.chkEPGreatness.Location = New System.Drawing.Point(153, 19)
		Me.chkEPGreatness.Name = "chkEPGreatness"
		Me.chkEPGreatness.Size = New System.Drawing.Size(141, 24)
		Me.chkEPGreatness.TabIndex = 2
		Me.chkEPGreatness.Text = "Greatness"
		Me.chkEPGreatness.UseVisualStyleBackColor = true
		'
		'chkEPDeathChoice
		'
		Me.chkEPDeathChoice.Location = New System.Drawing.Point(6, 199)
		Me.chkEPDeathChoice.Name = "chkEPDeathChoice"
		Me.chkEPDeathChoice.Size = New System.Drawing.Size(141, 24)
		Me.chkEPDeathChoice.TabIndex = 2
		Me.chkEPDeathChoice.Text = "DeathChoice"
		Me.chkEPDeathChoice.UseVisualStyleBackColor = true
		'
		'chkEPPyrite
		'
		Me.chkEPPyrite.Location = New System.Drawing.Point(6, 169)
		Me.chkEPPyrite.Name = "chkEPPyrite"
		Me.chkEPPyrite.Size = New System.Drawing.Size(141, 24)
		Me.chkEPPyrite.TabIndex = 2
		Me.chkEPPyrite.Text = "Pyrite"
		Me.chkEPPyrite.UseVisualStyleBackColor = true
		'
		'chkEPOldGod
		'
		Me.chkEPOldGod.Location = New System.Drawing.Point(6, 139)
		Me.chkEPOldGod.Name = "chkEPOldGod"
		Me.chkEPOldGod.Size = New System.Drawing.Size(141, 24)
		Me.chkEPOldGod.TabIndex = 2
		Me.chkEPOldGod.Text = "OldGod"
		Me.chkEPOldGod.UseVisualStyleBackColor = true
		'
		'chkEPMirror
		'
		Me.chkEPMirror.Location = New System.Drawing.Point(6, 109)
		Me.chkEPMirror.Name = "chkEPMirror"
		Me.chkEPMirror.Size = New System.Drawing.Size(141, 24)
		Me.chkEPMirror.TabIndex = 2
		Me.chkEPMirror.Text = "Mirror"
		Me.chkEPMirror.UseVisualStyleBackColor = true
		'
		'chkEPBitterAnguish
		'
		Me.chkEPBitterAnguish.Location = New System.Drawing.Point(6, 79)
		Me.chkEPBitterAnguish.Name = "chkEPBitterAnguish"
		Me.chkEPBitterAnguish.Size = New System.Drawing.Size(141, 24)
		Me.chkEPBitterAnguish.TabIndex = 2
		Me.chkEPBitterAnguish.Text = "BitterAnguish"
		Me.chkEPBitterAnguish.UseVisualStyleBackColor = true
		'
		'chkEPGrimToll
		'
		Me.chkEPGrimToll.Location = New System.Drawing.Point(6, 49)
		Me.chkEPGrimToll.Name = "chkEPGrimToll"
		Me.chkEPGrimToll.Size = New System.Drawing.Size(141, 24)
		Me.chkEPGrimToll.TabIndex = 2
		Me.chkEPGrimToll.Text = "GrimToll"
		Me.chkEPGrimToll.UseVisualStyleBackColor = true
		'
		'chkEPMjolRune
		'
		Me.chkEPMjolRune.Location = New System.Drawing.Point(6, 19)
		Me.chkEPMjolRune.Name = "chkEPMjolRune"
		Me.chkEPMjolRune.Size = New System.Drawing.Size(141, 24)
		Me.chkEPMjolRune.TabIndex = 2
		Me.chkEPMjolRune.Text = "MjolnirRunestone"
		Me.chkEPMjolRune.UseVisualStyleBackColor = true
		'
		'groupBox2
		'
		Me.groupBox2.Controls.Add(Me.chkEP4PT9)
		Me.groupBox2.Controls.Add(Me.chkEP2PT9)
		Me.groupBox2.Controls.Add(Me.chkEP4PT8)
		Me.groupBox2.Controls.Add(Me.chkEP2PT8)
		Me.groupBox2.Controls.Add(Me.chkEP4PT7)
		Me.groupBox2.Controls.Add(Me.chkEP2T7)
		Me.groupBox2.Location = New System.Drawing.Point(193, 48)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(128, 372)
		Me.groupBox2.TabIndex = 3
		Me.groupBox2.TabStop = false
		Me.groupBox2.Text = "Sets"
		'
		'chkEP4PT9
		'
		Me.chkEP4PT9.Checked = true
		Me.chkEP4PT9.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEP4PT9.Location = New System.Drawing.Point(6, 169)
		Me.chkEP4PT9.Name = "chkEP4PT9"
		Me.chkEP4PT9.Size = New System.Drawing.Size(141, 24)
		Me.chkEP4PT9.TabIndex = 4
		Me.chkEP4PT9.Text = "4P T9"
		Me.chkEP4PT9.UseVisualStyleBackColor = true
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
		Me.groupBox1.Controls.Add(Me.chkEPAfterSpellHitRating)
		Me.groupBox1.Controls.Add(Me.chkEPSpHit)
		Me.groupBox1.Controls.Add(Me.chkEPHit)
		Me.groupBox1.Controls.Add(Me.chkEPArP)
		Me.groupBox1.Controls.Add(Me.chkEPHaste)
		Me.groupBox1.Controls.Add(Me.chkEPCrit)
		Me.groupBox1.Controls.Add(Me.chkEPAgility)
		Me.groupBox1.Controls.Add(Me.chkEPStr)
		Me.groupBox1.Location = New System.Drawing.Point(36, 48)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(148, 372)
		Me.groupBox1.TabIndex = 2
		Me.groupBox1.TabStop = false
		Me.groupBox1.Text = "Common stats"
		'
		'chkEPSMHSpeed
		'
		Me.chkEPSMHSpeed.Checked = true
		Me.chkEPSMHSpeed.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkEPSMHSpeed.Location = New System.Drawing.Point(6, 319)
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
		Me.chkEPExp.Location = New System.Drawing.Point(6, 259)
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
		Me.chkEPSMHDPS.Location = New System.Drawing.Point(6, 289)
		Me.chkEPSMHDPS.Name = "chkEPSMHDPS"
		Me.chkEPSMHDPS.Size = New System.Drawing.Size(104, 24)
		Me.chkEPSMHDPS.TabIndex = 13
		Me.chkEPSMHDPS.Text = "Main Hand DPS"
		Me.chkEPSMHDPS.UseVisualStyleBackColor = true
		'
		'chkEPAfterSpellHitRating
		'
		Me.chkEPAfterSpellHitRating.Location = New System.Drawing.Point(6, 229)
		Me.chkEPAfterSpellHitRating.Name = "chkEPAfterSpellHitRating"
		Me.chkEPAfterSpellHitRating.Size = New System.Drawing.Size(136, 24)
		Me.chkEPAfterSpellHitRating.TabIndex = 13
		Me.chkEPAfterSpellHitRating.Text = "After Spell Hit rating"
		Me.chkEPAfterSpellHitRating.UseVisualStyleBackColor = true
		AddHandler Me.chkEPAfterSpellHitRating.CheckedChanged, AddressOf Me.ChkEPAfterSpellHitRatingCheckedChanged
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
		AddHandler Me.chkEPSpHit.CheckedChanged, AddressOf Me.ChkEPSpHitCheckedChanged
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
		Me.tbBuff.Size = New System.Drawing.Size(691, 471)
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
		Me.grpBuff.Controls.Add(Me.chkCrypticFever)
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
		'chkCrypticFever
		'
		Me.chkCrypticFever.Checked = true
		Me.chkCrypticFever.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkCrypticFever.Location = New System.Drawing.Point(177, 230)
		Me.chkCrypticFever.Name = "chkCrypticFever"
		Me.chkCrypticFever.Size = New System.Drawing.Size(104, 24)
		Me.chkCrypticFever.TabIndex = 0
		Me.chkCrypticFever.Text = "Cryptic Fever"
		Me.chkCrypticFever.UseVisualStyleBackColor = true
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
		'tbTpl
		'
		Me.tbTpl.AutoScroll = true
		Me.tbTpl.Controls.Add(Me.lblUnholy)
		Me.tbTpl.Controls.Add(Me.lblFrost)
		Me.tbTpl.Controls.Add(Me.lblBlood)
		Me.tbTpl.Controls.Add(Me.cmbGlyph3)
		Me.tbTpl.Controls.Add(Me.cmbGlyph2)
		Me.tbTpl.Controls.Add(Me.cmbGlyph1)
		Me.tbTpl.Controls.Add(Me.cmdSaveNewTemplate)
		Me.tbTpl.Controls.Add(Me.cmdSaveTemplate)
		Me.tbTpl.Location = New System.Drawing.Point(4, 22)
		Me.tbTpl.Name = "tbTpl"
		Me.tbTpl.Size = New System.Drawing.Size(691, 471)
		Me.tbTpl.TabIndex = 9
		Me.tbTpl.Text = "Template"
		Me.tbTpl.UseVisualStyleBackColor = true
		'
		'lblUnholy
		'
		Me.lblUnholy.Location = New System.Drawing.Point(348, 425)
		Me.lblUnholy.Name = "lblUnholy"
		Me.lblUnholy.Size = New System.Drawing.Size(100, 23)
		Me.lblUnholy.TabIndex = 2
		Me.lblUnholy.Text = "0"
		'
		'lblFrost
		'
		Me.lblFrost.Location = New System.Drawing.Point(202, 425)
		Me.lblFrost.Name = "lblFrost"
		Me.lblFrost.Size = New System.Drawing.Size(100, 23)
		Me.lblFrost.TabIndex = 2
		Me.lblFrost.Text = "0"
		'
		'lblBlood
		'
		Me.lblBlood.Location = New System.Drawing.Point(18, 425)
		Me.lblBlood.Name = "lblBlood"
		Me.lblBlood.Size = New System.Drawing.Size(100, 23)
		Me.lblBlood.TabIndex = 2
		Me.lblBlood.Text = "0"
		'
		'cmbGlyph3
		'
		Me.cmbGlyph3.FormattingEnabled = true
		Me.cmbGlyph3.Location = New System.Drawing.Point(559, 130)
		Me.cmbGlyph3.Name = "cmbGlyph3"
		Me.cmbGlyph3.Size = New System.Drawing.Size(121, 21)
		Me.cmbGlyph3.TabIndex = 1
		'
		'cmbGlyph2
		'
		Me.cmbGlyph2.FormattingEnabled = true
		Me.cmbGlyph2.Location = New System.Drawing.Point(559, 103)
		Me.cmbGlyph2.Name = "cmbGlyph2"
		Me.cmbGlyph2.Size = New System.Drawing.Size(121, 21)
		Me.cmbGlyph2.TabIndex = 1
		'
		'cmbGlyph1
		'
		Me.cmbGlyph1.FormattingEnabled = true
		Me.cmbGlyph1.Location = New System.Drawing.Point(559, 76)
		Me.cmbGlyph1.Name = "cmbGlyph1"
		Me.cmbGlyph1.Size = New System.Drawing.Size(121, 21)
		Me.cmbGlyph1.TabIndex = 1
		'
		'cmdSaveNewTemplate
		'
		Me.cmdSaveNewTemplate.Location = New System.Drawing.Point(478, 3)
		Me.cmdSaveNewTemplate.Name = "cmdSaveNewTemplate"
		Me.cmdSaveNewTemplate.Size = New System.Drawing.Size(108, 23)
		Me.cmdSaveNewTemplate.TabIndex = 0
		Me.cmdSaveNewTemplate.Text = "Save as new"
		Me.cmdSaveNewTemplate.UseVisualStyleBackColor = true
		AddHandler Me.cmdSaveNewTemplate.Click, AddressOf Me.CmdSaveNewTemplateClick
		'
		'cmdSaveTemplate
		'
		Me.cmdSaveTemplate.Location = New System.Drawing.Point(592, 3)
		Me.cmdSaveTemplate.Name = "cmdSaveTemplate"
		Me.cmdSaveTemplate.Size = New System.Drawing.Size(93, 23)
		Me.cmdSaveTemplate.TabIndex = 0
		Me.cmdSaveTemplate.Text = "Save Template"
		Me.cmdSaveTemplate.UseVisualStyleBackColor = true
		AddHandler Me.cmdSaveTemplate.Click, AddressOf Me.CmdSaveTemplateClick
		'
		'tbCaling
		'
		Me.tbCaling.Controls.Add(Me.label21)
		Me.tbCaling.Controls.Add(Me.cmdScaling)
		Me.tbCaling.Controls.Add(Me.gbScaling)
		Me.tbCaling.Location = New System.Drawing.Point(4, 22)
		Me.tbCaling.Name = "tbCaling"
		Me.tbCaling.Size = New System.Drawing.Size(691, 471)
		Me.tbCaling.TabIndex = 10
		Me.tbCaling.Text = "Stat Scaling"
		Me.tbCaling.UseVisualStyleBackColor = true
		'
		'label21
		'
		Me.label21.Location = New System.Drawing.Point(18, 4)
		Me.label21.Name = "label21"
		Me.label21.Size = New System.Drawing.Size(363, 81)
		Me.label21.TabIndex = 12
		Me.label21.Text = "The stat scaling fucntion runs multiple simulation incrementing at each run. "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Ex"& _ 
		"cept for Strengh and Agility, character stats are replaced or added. "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Select 1h"& _ 
		" simulation time or it may take long."
		'
		'cmdScaling
		'
		Me.cmdScaling.Location = New System.Drawing.Point(306, 418)
		Me.cmdScaling.Name = "cmdScaling"
		Me.cmdScaling.Size = New System.Drawing.Size(75, 23)
		Me.cmdScaling.TabIndex = 11
		Me.cmdScaling.Text = "Sim Scaling"
		Me.cmdScaling.UseVisualStyleBackColor = true
		AddHandler Me.cmdScaling.Click, AddressOf Me.CmdScalingClick
		'
		'gbScaling
		'
		Me.gbScaling.Controls.Add(Me.chkScaExpA)
		Me.gbScaling.Controls.Add(Me.chkScaExp)
		Me.gbScaling.Controls.Add(Me.chkScaHitA)
		Me.gbScaling.Controls.Add(Me.chkScaHit)
		Me.gbScaling.Controls.Add(Me.chkScaArPA)
		Me.gbScaling.Controls.Add(Me.chkScaHasteA)
		Me.gbScaling.Controls.Add(Me.chkScaArP)
		Me.gbScaling.Controls.Add(Me.chkScaCritA)
		Me.gbScaling.Controls.Add(Me.chkScaHaste)
		Me.gbScaling.Controls.Add(Me.chkScaCrit)
		Me.gbScaling.Controls.Add(Me.chkScaAgility)
		Me.gbScaling.Controls.Add(Me.chkScaStr)
		Me.gbScaling.Location = New System.Drawing.Point(18, 88)
		Me.gbScaling.Name = "gbScaling"
		Me.gbScaling.Size = New System.Drawing.Size(426, 232)
		Me.gbScaling.TabIndex = 3
		Me.gbScaling.TabStop = false
		Me.gbScaling.Text = "Common stats"
		'
		'chkScaExp
		'
		Me.chkScaExp.Checked = true
		Me.chkScaExp.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkScaExp.Location = New System.Drawing.Point(6, 199)
		Me.chkScaExp.Name = "chkScaExp"
		Me.chkScaExp.Size = New System.Drawing.Size(158, 24)
		Me.chkScaExp.TabIndex = 14
		Me.chkScaExp.Text = "Replace Expertise rating"
		Me.chkScaExp.UseVisualStyleBackColor = true
		'
		'chkScaHit
		'
		Me.chkScaHit.Checked = true
		Me.chkScaHit.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkScaHit.Location = New System.Drawing.Point(6, 169)
		Me.chkScaHit.Name = "chkScaHit"
		Me.chkScaHit.Size = New System.Drawing.Size(158, 24)
		Me.chkScaHit.TabIndex = 16
		Me.chkScaHit.Text = "Replace Hit rating"
		Me.chkScaHit.UseVisualStyleBackColor = true
		'
		'chkScaArP
		'
		Me.chkScaArP.Checked = true
		Me.chkScaArP.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkScaArP.Location = New System.Drawing.Point(6, 139)
		Me.chkScaArP.Name = "chkScaArP"
		Me.chkScaArP.Size = New System.Drawing.Size(166, 24)
		Me.chkScaArP.TabIndex = 15
		Me.chkScaArP.Text = "Replace Armor Penetration"
		Me.chkScaArP.UseVisualStyleBackColor = true
		'
		'chkScaHaste
		'
		Me.chkScaHaste.Checked = true
		Me.chkScaHaste.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkScaHaste.Location = New System.Drawing.Point(6, 109)
		Me.chkScaHaste.Name = "chkScaHaste"
		Me.chkScaHaste.Size = New System.Drawing.Size(158, 24)
		Me.chkScaHaste.TabIndex = 10
		Me.chkScaHaste.Text = "Replace Haste Rating"
		Me.chkScaHaste.UseVisualStyleBackColor = true
		'
		'chkScaCrit
		'
		Me.chkScaCrit.Checked = true
		Me.chkScaCrit.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkScaCrit.Location = New System.Drawing.Point(6, 79)
		Me.chkScaCrit.Name = "chkScaCrit"
		Me.chkScaCrit.Size = New System.Drawing.Size(158, 24)
		Me.chkScaCrit.TabIndex = 9
		Me.chkScaCrit.Text = "Replace Critical rating"
		Me.chkScaCrit.UseVisualStyleBackColor = true
		'
		'chkScaAgility
		'
		Me.chkScaAgility.Checked = true
		Me.chkScaAgility.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkScaAgility.Location = New System.Drawing.Point(259, 49)
		Me.chkScaAgility.Name = "chkScaAgility"
		Me.chkScaAgility.Size = New System.Drawing.Size(104, 24)
		Me.chkScaAgility.TabIndex = 12
		Me.chkScaAgility.Text = "Add Agility"
		Me.chkScaAgility.UseVisualStyleBackColor = true
		'
		'chkScaStr
		'
		Me.chkScaStr.Checked = true
		Me.chkScaStr.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkScaStr.Location = New System.Drawing.Point(259, 19)
		Me.chkScaStr.Name = "chkScaStr"
		Me.chkScaStr.Size = New System.Drawing.Size(104, 24)
		Me.chkScaStr.TabIndex = 11
		Me.chkScaStr.Text = "Add Strength"
		Me.chkScaStr.UseVisualStyleBackColor = true
		'
		'tbTank
		'
		Me.tbTank.Controls.Add(Me.gbTank)
		Me.tbTank.Location = New System.Drawing.Point(4, 22)
		Me.tbTank.Name = "tbTank"
		Me.tbTank.Size = New System.Drawing.Size(691, 471)
		Me.tbTank.TabIndex = 11
		Me.tbTank.Text = "Tank options"
		Me.tbTank.UseVisualStyleBackColor = true
		'
		'gbTank
		'
		Me.gbTank.Controls.Add(Me.label24)
		Me.gbTank.Controls.Add(Me.label23)
		Me.gbTank.Controls.Add(Me.txtFBAvoidance)
		Me.gbTank.Controls.Add(Me.txtFPBossSwing)
		Me.gbTank.Location = New System.Drawing.Point(18, 17)
		Me.gbTank.Name = "gbTank"
		Me.gbTank.Size = New System.Drawing.Size(313, 191)
		Me.gbTank.TabIndex = 2
		Me.gbTank.TabStop = false
		Me.gbTank.Text = "Frost Presence options"
		AddHandler Me.gbTank.Enter, AddressOf Me.GroupBox4Enter
		'
		'label24
		'
		Me.label24.Location = New System.Drawing.Point(112, 48)
		Me.label24.Name = "label24"
		Me.label24.Size = New System.Drawing.Size(190, 17)
		Me.label24.TabIndex = 11
		Me.label24.Text = "Character Avoidance chance in %"
		'
		'label23
		'
		Me.label23.Location = New System.Drawing.Point(112, 22)
		Me.label23.Name = "label23"
		Me.label23.Size = New System.Drawing.Size(186, 20)
		Me.label23.TabIndex = 13
		Me.label23.Text = "Boss swing speed in second"
		'
		'txtFBAvoidance
		'
		Me.txtFBAvoidance.Location = New System.Drawing.Point(6, 45)
		Me.txtFBAvoidance.Name = "txtFBAvoidance"
		Me.txtFBAvoidance.Size = New System.Drawing.Size(100, 20)
		Me.txtFBAvoidance.TabIndex = 8
		Me.txtFBAvoidance.Text = "50"
		'
		'txtFPBossSwing
		'
		Me.txtFPBossSwing.Location = New System.Drawing.Point(6, 19)
		Me.txtFPBossSwing.Name = "txtFPBossSwing"
		Me.txtFPBossSwing.Size = New System.Drawing.Size(100, 20)
		Me.txtFPBossSwing.TabIndex = 7
		Me.txtFPBossSwing.Text = "2"
		'
		'chkScaCritA
		'
		Me.chkScaCritA.Location = New System.Drawing.Point(259, 79)
		Me.chkScaCritA.Name = "chkScaCritA"
		Me.chkScaCritA.Size = New System.Drawing.Size(161, 24)
		Me.chkScaCritA.TabIndex = 9
		Me.chkScaCritA.Text = "Add Critical rating"
		Me.chkScaCritA.UseVisualStyleBackColor = true
		AddHandler Me.chkScaCritA.CheckedChanged, AddressOf Me.ChkScaCritACheckedChanged
		'
		'chkScaHasteA
		'
		Me.chkScaHasteA.Location = New System.Drawing.Point(259, 109)
		Me.chkScaHasteA.Name = "chkScaHasteA"
		Me.chkScaHasteA.Size = New System.Drawing.Size(161, 24)
		Me.chkScaHasteA.TabIndex = 10
		Me.chkScaHasteA.Text = "Add Haste Rating"
		Me.chkScaHasteA.UseVisualStyleBackColor = true
		'
		'chkScaArPA
		'
		Me.chkScaArPA.Location = New System.Drawing.Point(259, 139)
		Me.chkScaArPA.Name = "chkScaArPA"
		Me.chkScaArPA.Size = New System.Drawing.Size(169, 24)
		Me.chkScaArPA.TabIndex = 15
		Me.chkScaArPA.Text = "Add Armor Penetration"
		Me.chkScaArPA.UseVisualStyleBackColor = true
		'
		'chkScaHitA
		'
		Me.chkScaHitA.Location = New System.Drawing.Point(259, 169)
		Me.chkScaHitA.Name = "chkScaHitA"
		Me.chkScaHitA.Size = New System.Drawing.Size(161, 24)
		Me.chkScaHitA.TabIndex = 16
		Me.chkScaHitA.Text = "Add Hit rating"
		Me.chkScaHitA.UseVisualStyleBackColor = true
		'
		'chkScaExpA
		'
		Me.chkScaExpA.Location = New System.Drawing.Point(259, 199)
		Me.chkScaExpA.Name = "chkScaExpA"
		Me.chkScaExpA.Size = New System.Drawing.Size(161, 24)
		Me.chkScaExpA.TabIndex = 14
		Me.chkScaExpA.Text = "Add Expertise rating"
		Me.chkScaExpA.UseVisualStyleBackColor = true
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(699, 567)
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
		Me.tbEPOptions.ResumeLayout(false)
		Me.groupBox3.ResumeLayout(false)
		Me.groupBox2.ResumeLayout(false)
		Me.groupBox1.ResumeLayout(false)
		Me.tbBuff.ResumeLayout(false)
		Me.grpBuff.ResumeLayout(false)
		Me.tbTpl.ResumeLayout(false)
		Me.tbCaling.ResumeLayout(false)
		Me.gbScaling.ResumeLayout(false)
		Me.tbTank.ResumeLayout(false)
		Me.gbTank.ResumeLayout(false)
		Me.gbTank.PerformLayout
		Me.ResumeLayout(false)
	End Sub
	Friend chkScaCritA As System.Windows.Forms.CheckBox
	Friend chkScaHasteA As System.Windows.Forms.CheckBox
	Friend chkScaArPA As System.Windows.Forms.CheckBox
	Friend chkScaHitA As System.Windows.Forms.CheckBox
	Friend chkScaExpA As System.Windows.Forms.CheckBox
	Private txtFBAvoidance As System.Windows.Forms.TextBox
	Private txtFPBossSwing As System.Windows.Forms.TextBox
	Private gbTank As System.Windows.Forms.GroupBox
	Private label23 As System.Windows.Forms.Label
	Private label24 As System.Windows.Forms.Label
	Private tbTank As System.Windows.Forms.TabPage
	Private gbScaling As System.Windows.Forms.GroupBox
	Friend chkScaStr As System.Windows.Forms.CheckBox
	Friend chkScaAgility As System.Windows.Forms.CheckBox
	Friend chkScaCrit As System.Windows.Forms.CheckBox
	Friend chkScaHaste As System.Windows.Forms.CheckBox
	Friend chkScaArP As System.Windows.Forms.CheckBox
	Friend chkScaHit As System.Windows.Forms.CheckBox
	Friend chkScaExp As System.Windows.Forms.CheckBox
	Private label21 As System.Windows.Forms.Label
	Private tbCaling As System.Windows.Forms.TabPage
	Private cmdScaling As System.Windows.Forms.Button
	Private cmbBShOption As System.Windows.Forms.ComboBox
	Private label20 As System.Windows.Forms.Label
	Friend lblBlood As System.Windows.Forms.Label
	Friend lblFrost As System.Windows.Forms.Label
	Friend lblUnholy As System.Windows.Forms.Label
	Friend chkDisease As System.Windows.Forms.CheckBox
	Private label19 As System.Windows.Forms.Label
	Friend txtNumberOfEnemies As System.Windows.Forms.TextBox
	Private cmbGlyph1 As System.Windows.Forms.ComboBox
	Private cmbGlyph2 As System.Windows.Forms.ComboBox
	Private cmbGlyph3 As System.Windows.Forms.ComboBox
	Private cmdSaveTemplate As System.Windows.Forms.Button
	Private cmdSaveNewTemplate As System.Windows.Forms.Button
	Private toolTip As System.Windows.Forms.ToolTip
	Private tbTpl As System.Windows.Forms.TabPage
	Private rdPrio As System.Windows.Forms.RadioButton
	Private rdRot As System.Windows.Forms.RadioButton
	Private chkCrypticFever As System.Windows.Forms.CheckBox
	Friend txtManyFights As System.Windows.Forms.TextBox
	Friend chkManyFights As System.Windows.Forms.CheckBox
	Private label18 As System.Windows.Forms.Label
	Friend chkEPAfterSpellHitRating As System.Windows.Forms.CheckBox
	Friend chkEPDCDeath As System.Windows.Forms.CheckBox
	Friend chkEPMjolRune As System.Windows.Forms.CheckBox
	Friend chkEPDarkMatter As System.Windows.Forms.CheckBox
	Friend chkEPComet As System.Windows.Forms.CheckBox
	Friend chkEPGrimToll As System.Windows.Forms.CheckBox
	Friend chkEPBitterAnguish As System.Windows.Forms.CheckBox
	Friend chkEPMirror As System.Windows.Forms.CheckBox
	Friend chkEPOldGod As System.Windows.Forms.CheckBox
	Friend chkEPPyrite As System.Windows.Forms.CheckBox
	Friend chkEPDeathChoice As System.Windows.Forms.CheckBox
	Friend chkEPGreatness As System.Windows.Forms.CheckBox
	Friend chkEPVictory As System.Windows.Forms.CheckBox
	Friend chkEPNecromantic As System.Windows.Forms.CheckBox
	Friend chkEPBandit As System.Windows.Forms.CheckBox
	Friend chkEP4PT9 As System.Windows.Forms.CheckBox
	Private groupBox3 As System.Windows.Forms.GroupBox
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
	Private chkGhoulHaste As System.Windows.Forms.CheckBox
	Private label12 As System.Windows.Forms.Label
	Private txtArmory As System.Windows.Forms.TextBox
	Private cmdImportArmory As System.Windows.Forms.Button
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
