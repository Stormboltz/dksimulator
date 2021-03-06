﻿'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 10:41 AM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Partial Class GearSelectorMainForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GearSelectorMainForm))
		Me.cmdExtrator = New System.Windows.Forms.Button
		Me.gbStats = New System.Windows.Forms.GroupBox
		Me.txtArP = New System.Windows.Forms.TextBox
		Me.lblArP = New System.Windows.Forms.Label
		Me.txtHit = New System.Windows.Forms.TextBox
		Me.lblHit = New System.Windows.Forms.Label
		Me.txtHaste = New System.Windows.Forms.TextBox
		Me.txtIntel = New System.Windows.Forms.TextBox
		Me.txtAP = New System.Windows.Forms.TextBox
		Me.lblHaste = New System.Windows.Forms.Label
		Me.lblInt = New System.Windows.Forms.Label
		Me.lblAP = New System.Windows.Forms.Label
		Me.txtCrit = New System.Windows.Forms.TextBox
		Me.txtAgi = New System.Windows.Forms.TextBox
		Me.txtExp = New System.Windows.Forms.TextBox
		Me.txtArmor = New System.Windows.Forms.TextBox
		Me.lblCrit = New System.Windows.Forms.Label
		Me.lblExp = New System.Windows.Forms.Label
		Me.lblAgi = New System.Windows.Forms.Label
		Me.lblArM = New System.Windows.Forms.Label
		Me.txtStr = New System.Windows.Forms.TextBox
		Me.lblStr = New System.Windows.Forms.Label
		Me.gbWeapons = New System.Windows.Forms.GroupBox
		Me.rDW = New System.Windows.Forms.RadioButton
		Me.r2Hand = New System.Windows.Forms.RadioButton
		Me.txtOHWSpeed = New System.Windows.Forms.TextBox
		Me.txtMHWSpeed = New System.Windows.Forms.TextBox
		Me.label40 = New System.Windows.Forms.Label
		Me.label38 = New System.Windows.Forms.Label
		Me.txtOHDPS = New System.Windows.Forms.TextBox
		Me.txtMHDPS = New System.Windows.Forms.TextBox
		Me.label39 = New System.Windows.Forms.Label
		Me.label37 = New System.Windows.Forms.Label
		Me.gbMisc = New System.Windows.Forms.GroupBox
		Me.chkTailorEnchant = New System.Windows.Forms.CheckBox
		Me.chkMeta = New System.Windows.Forms.CheckBox
		Me.chkAshenBand = New System.Windows.Forms.CheckBox
		Me.chkAccelerators = New System.Windows.Forms.CheckBox
		Me.chkIngenieer = New System.Windows.Forms.CheckBox
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.gbWeaponProc = New System.Windows.Forms.GroupBox
		Me.cmbWeaponProc2 = New System.Windows.Forms.TextBox
		Me.cmbWeaponProc1 = New System.Windows.Forms.TextBox
		Me.gbTrinkets = New System.Windows.Forms.GroupBox
		Me.cmbTrinket2 = New System.Windows.Forms.TextBox
		Me.cmbTrinket1 = New System.Windows.Forms.TextBox
		Me.gbSetBonus = New System.Windows.Forms.GroupBox
		Me.cmbSetBonus2 = New System.Windows.Forms.TextBox
		Me.cmbSetBonus1 = New System.Windows.Forms.TextBox
		Me.groupBox5 = New System.Windows.Forms.GroupBox
		Me.chkArcaneTorrent = New System.Windows.Forms.CheckBox
		Me.chkBerzerking = New System.Windows.Forms.CheckBox
		Me.chkBloodFury = New System.Windows.Forms.CheckBox
		Me.txtOHExpBonus = New System.Windows.Forms.TextBox
		Me.txtMHExpBonus = New System.Windows.Forms.TextBox
		Me.label41 = New System.Windows.Forms.Label
		Me.label42 = New System.Windows.Forms.Label
		Me.toolStrip2 = New System.Windows.Forms.ToolStrip
		Me.cmdQuickEP = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.cmdGetDps = New System.Windows.Forms.ToolStripButton
		Me.lblDPS = New System.Windows.Forms.ToolStripLabel
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.toolStripLabel1 = New System.Windows.Forms.ToolStripLabel
		Me.cmbRace = New System.Windows.Forms.ToolStripComboBox
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.cmdSave = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
		Me.cmdSaveAsNew = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
		Me.toolStripLabel2 = New System.Windows.Forms.ToolStripLabel
		Me.cmbSkill1 = New System.Windows.Forms.ToolStripComboBox
		Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
		Me.toolStripLabel3 = New System.Windows.Forms.ToolStripLabel
		Me.cmbSkill2 = New System.Windows.Forms.ToolStripComboBox
		Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip
		Me.toolStripLabel4 = New System.Windows.Forms.ToolStripLabel
		Me.cmbFlask = New System.Windows.Forms.ToolStripComboBox
		Me.toolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
		Me.toolStripLabel5 = New System.Windows.Forms.ToolStripLabel
		Me.cmbFood = New System.Windows.Forms.ToolStripComboBox
		Me.toolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
		Me.ddConsumable = New System.Windows.Forms.ToolStripDropDownButton
		Me.toolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
		Me.cmdArmoryImport = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator
		Me.gbStats.SuspendLayout
		Me.gbWeapons.SuspendLayout
		Me.gbMisc.SuspendLayout
		Me.groupBox1.SuspendLayout
		Me.gbWeaponProc.SuspendLayout
		Me.gbTrinkets.SuspendLayout
		Me.gbSetBonus.SuspendLayout
		Me.groupBox5.SuspendLayout
		Me.toolStrip2.SuspendLayout
		Me.toolStrip1.SuspendLayout
		Me.SuspendLayout
		'
		'cmdExtrator
		'
		Me.cmdExtrator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdExtrator.Location = New System.Drawing.Point(9, 25342)
		Me.cmdExtrator.Name = "cmdExtrator"
		Me.cmdExtrator.Size = New System.Drawing.Size(75, 23)
		Me.cmdExtrator.TabIndex = 0
		Me.cmdExtrator.Text = "Extract"
		Me.cmdExtrator.UseVisualStyleBackColor = true
		AddHandler Me.cmdExtrator.Click, AddressOf Me.CmdExtratorClick
		'
		'gbStats
		'
		Me.gbStats.Controls.Add(Me.txtArP)
		Me.gbStats.Controls.Add(Me.lblArP)
		Me.gbStats.Controls.Add(Me.txtHit)
		Me.gbStats.Controls.Add(Me.lblHit)
		Me.gbStats.Controls.Add(Me.txtHaste)
		Me.gbStats.Controls.Add(Me.txtIntel)
		Me.gbStats.Controls.Add(Me.txtAP)
		Me.gbStats.Controls.Add(Me.lblHaste)
		Me.gbStats.Controls.Add(Me.lblInt)
		Me.gbStats.Controls.Add(Me.lblAP)
		Me.gbStats.Controls.Add(Me.txtCrit)
		Me.gbStats.Controls.Add(Me.txtAgi)
		Me.gbStats.Controls.Add(Me.txtExp)
		Me.gbStats.Controls.Add(Me.txtArmor)
		Me.gbStats.Controls.Add(Me.lblCrit)
		Me.gbStats.Controls.Add(Me.lblExp)
		Me.gbStats.Controls.Add(Me.lblAgi)
		Me.gbStats.Controls.Add(Me.lblArM)
		Me.gbStats.Controls.Add(Me.txtStr)
		Me.gbStats.Controls.Add(Me.lblStr)
		Me.gbStats.Location = New System.Drawing.Point(6, 14)
		Me.gbStats.Name = "gbStats"
		Me.gbStats.Size = New System.Drawing.Size(265, 298)
		Me.gbStats.TabIndex = 15
		Me.gbStats.TabStop = false
		Me.gbStats.Text = "Statistics"
		'
		'txtArP
		'
		Me.txtArP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtArP.Location = New System.Drawing.Point(159, 235)
		Me.txtArP.Name = "txtArP"
		Me.txtArP.Size = New System.Drawing.Size(100, 20)
		Me.txtArP.TabIndex = 9
		'
		'lblArP
		'
		Me.lblArP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblArP.Location = New System.Drawing.Point(35, 235)
		Me.lblArP.Name = "lblArP"
		Me.lblArP.Size = New System.Drawing.Size(118, 28)
		Me.lblArP.TabIndex = 13
		Me.lblArP.Text = "Armor Penetration Rating"
		Me.lblArP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtHit
		'
		Me.txtHit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtHit.Location = New System.Drawing.Point(159, 155)
		Me.txtHit.Name = "txtHit"
		Me.txtHit.Size = New System.Drawing.Size(100, 20)
		Me.txtHit.TabIndex = 6
		'
		'lblHit
		'
		Me.lblHit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblHit.Location = New System.Drawing.Point(58, 157)
		Me.lblHit.Name = "lblHit"
		Me.lblHit.Size = New System.Drawing.Size(94, 18)
		Me.lblHit.TabIndex = 12
		Me.lblHit.Text = "Hit Rating"
		Me.lblHit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtHaste
		'
		Me.txtHaste.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtHaste.Location = New System.Drawing.Point(159, 209)
		Me.txtHaste.Name = "txtHaste"
		Me.txtHaste.Size = New System.Drawing.Size(100, 20)
		Me.txtHaste.TabIndex = 8
		'
		'txtIntel
		'
		Me.txtIntel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtIntel.Location = New System.Drawing.Point(159, 68)
		Me.txtIntel.Name = "txtIntel"
		Me.txtIntel.Size = New System.Drawing.Size(100, 20)
		Me.txtIntel.TabIndex = 3
		'
		'txtAP
		'
		Me.txtAP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtAP.Location = New System.Drawing.Point(159, 120)
		Me.txtAP.Name = "txtAP"
		Me.txtAP.Size = New System.Drawing.Size(100, 20)
		Me.txtAP.TabIndex = 5
		'
		'lblHaste
		'
		Me.lblHaste.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblHaste.Location = New System.Drawing.Point(39, 211)
		Me.lblHaste.Name = "lblHaste"
		Me.lblHaste.Size = New System.Drawing.Size(114, 18)
		Me.lblHaste.TabIndex = 7
		Me.lblHaste.Text = "Haste Rating"
		Me.lblHaste.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblInt
		'
		Me.lblInt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblInt.Location = New System.Drawing.Point(78, 68)
		Me.lblInt.Name = "lblInt"
		Me.lblInt.Size = New System.Drawing.Size(75, 18)
		Me.lblInt.TabIndex = 6
		Me.lblInt.Text = "Intelligence"
		Me.lblInt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblAP
		'
		Me.lblAP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblAP.Location = New System.Drawing.Point(64, 120)
		Me.lblAP.Name = "lblAP"
		Me.lblAP.Size = New System.Drawing.Size(89, 31)
		Me.lblAP.TabIndex = 14
		Me.lblAP.Text = "Attack Power (Green Number)"
		Me.lblAP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtCrit
		'
		Me.txtCrit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtCrit.Location = New System.Drawing.Point(159, 183)
		Me.txtCrit.Name = "txtCrit"
		Me.txtCrit.Size = New System.Drawing.Size(100, 20)
		Me.txtCrit.TabIndex = 7
		'
		'txtAgi
		'
		Me.txtAgi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtAgi.Location = New System.Drawing.Point(159, 42)
		Me.txtAgi.Name = "txtAgi"
		Me.txtAgi.Size = New System.Drawing.Size(100, 20)
		Me.txtAgi.TabIndex = 2
		'
		'txtExp
		'
		Me.txtExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtExp.Location = New System.Drawing.Point(159, 267)
		Me.txtExp.Name = "txtExp"
		Me.txtExp.Size = New System.Drawing.Size(100, 20)
		Me.txtExp.TabIndex = 10
		'
		'txtArmor
		'
		Me.txtArmor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtArmor.Location = New System.Drawing.Point(159, 94)
		Me.txtArmor.Name = "txtArmor"
		Me.txtArmor.Size = New System.Drawing.Size(100, 20)
		Me.txtArmor.TabIndex = 4
		'
		'lblCrit
		'
		Me.lblCrit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblCrit.Location = New System.Drawing.Point(59, 183)
		Me.lblCrit.Name = "lblCrit"
		Me.lblCrit.Size = New System.Drawing.Size(94, 18)
		Me.lblCrit.TabIndex = 8
		Me.lblCrit.Text = "Crit Rating"
		Me.lblCrit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblExp
		'
		Me.lblExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblExp.Location = New System.Drawing.Point(28, 267)
		Me.lblExp.Name = "lblExp"
		Me.lblExp.Size = New System.Drawing.Size(125, 28)
		Me.lblExp.TabIndex = 11
		Me.lblExp.Text = "Expertise Rating"
		Me.lblExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblAgi
		'
		Me.lblAgi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblAgi.Location = New System.Drawing.Point(78, 42)
		Me.lblAgi.Name = "lblAgi"
		Me.lblAgi.Size = New System.Drawing.Size(75, 18)
		Me.lblAgi.TabIndex = 10
		Me.lblAgi.Text = "Agility"
		Me.lblAgi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblArM
		'
		Me.lblArM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblArM.Location = New System.Drawing.Point(78, 94)
		Me.lblArM.Name = "lblArM"
		Me.lblArM.Size = New System.Drawing.Size(75, 18)
		Me.lblArM.TabIndex = 9
		Me.lblArM.Text = "Armor"
		Me.lblArM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtStr
		'
		Me.txtStr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtStr.Location = New System.Drawing.Point(159, 16)
		Me.txtStr.Name = "txtStr"
		Me.txtStr.Size = New System.Drawing.Size(100, 20)
		Me.txtStr.TabIndex = 1
		'
		'lblStr
		'
		Me.lblStr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lblStr.Location = New System.Drawing.Point(78, 16)
		Me.lblStr.Name = "lblStr"
		Me.lblStr.Size = New System.Drawing.Size(75, 18)
		Me.lblStr.TabIndex = 5
		Me.lblStr.Text = "Strength"
		Me.lblStr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'gbWeapons
		'
		Me.gbWeapons.Controls.Add(Me.rDW)
		Me.gbWeapons.Controls.Add(Me.r2Hand)
		Me.gbWeapons.Controls.Add(Me.txtOHWSpeed)
		Me.gbWeapons.Controls.Add(Me.txtMHWSpeed)
		Me.gbWeapons.Controls.Add(Me.label40)
		Me.gbWeapons.Controls.Add(Me.label38)
		Me.gbWeapons.Controls.Add(Me.txtOHDPS)
		Me.gbWeapons.Controls.Add(Me.txtMHDPS)
		Me.gbWeapons.Controls.Add(Me.label39)
		Me.gbWeapons.Controls.Add(Me.label37)
		Me.gbWeapons.Location = New System.Drawing.Point(6, 318)
		Me.gbWeapons.Name = "gbWeapons"
		Me.gbWeapons.Size = New System.Drawing.Size(259, 174)
		Me.gbWeapons.TabIndex = 18
		Me.gbWeapons.TabStop = false
		Me.gbWeapons.Text = "Weapon(s)"
		'
		'rDW
		'
		Me.rDW.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.rDW.Location = New System.Drawing.Point(174, 19)
		Me.rDW.Name = "rDW"
		Me.rDW.Size = New System.Drawing.Size(79, 24)
		Me.rDW.TabIndex = 12
		Me.rDW.Text = "Dual wield"
		Me.rDW.UseVisualStyleBackColor = true
		AddHandler Me.rDW.CheckedChanged, AddressOf Me.RDWCheckedChanged
		'
		'r2Hand
		'
		Me.r2Hand.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.r2Hand.Checked = true
		Me.r2Hand.Location = New System.Drawing.Point(100, 19)
		Me.r2Hand.Name = "r2Hand"
		Me.r2Hand.Size = New System.Drawing.Size(104, 24)
		Me.r2Hand.TabIndex = 11
		Me.r2Hand.TabStop = true
		Me.r2Hand.Text = "2 Hand"
		Me.r2Hand.UseVisualStyleBackColor = true
		AddHandler Me.r2Hand.CheckedChanged, AddressOf Me.R2HandCheckedChanged
		'
		'txtOHWSpeed
		'
		Me.txtOHWSpeed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtOHWSpeed.Enabled = false
		Me.txtOHWSpeed.Location = New System.Drawing.Point(141, 135)
		Me.txtOHWSpeed.Name = "txtOHWSpeed"
		Me.txtOHWSpeed.Size = New System.Drawing.Size(100, 20)
		Me.txtOHWSpeed.TabIndex = 16
		Me.txtOHWSpeed.Text = "0"
		'
		'txtMHWSpeed
		'
		Me.txtMHWSpeed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtMHWSpeed.Location = New System.Drawing.Point(141, 75)
		Me.txtMHWSpeed.Name = "txtMHWSpeed"
		Me.txtMHWSpeed.Size = New System.Drawing.Size(100, 20)
		Me.txtMHWSpeed.TabIndex = 14
		Me.txtMHWSpeed.Text = "0"
		'
		'label40
		'
		Me.label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label40.Location = New System.Drawing.Point(60, 135)
		Me.label40.Name = "label40"
		Me.label40.Size = New System.Drawing.Size(75, 31)
		Me.label40.TabIndex = 9
		Me.label40.Text = "OH Weapon Speed"
		Me.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label38
		'
		Me.label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label38.Location = New System.Drawing.Point(46, 75)
		Me.label38.Name = "label38"
		Me.label38.Size = New System.Drawing.Size(89, 31)
		Me.label38.TabIndex = 7
		Me.label38.Text = "MH Weapon Speed"
		Me.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtOHDPS
		'
		Me.txtOHDPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtOHDPS.Enabled = false
		Me.txtOHDPS.Location = New System.Drawing.Point(141, 109)
		Me.txtOHDPS.Name = "txtOHDPS"
		Me.txtOHDPS.Size = New System.Drawing.Size(100, 20)
		Me.txtOHDPS.TabIndex = 15
		Me.txtOHDPS.Text = "0"
		'
		'txtMHDPS
		'
		Me.txtMHDPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtMHDPS.Location = New System.Drawing.Point(141, 49)
		Me.txtMHDPS.Name = "txtMHDPS"
		Me.txtMHDPS.Size = New System.Drawing.Size(100, 20)
		Me.txtMHDPS.TabIndex = 13
		Me.txtMHDPS.Text = "0"
		'
		'label39
		'
		Me.label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label39.Location = New System.Drawing.Point(60, 109)
		Me.label39.Name = "label39"
		Me.label39.Size = New System.Drawing.Size(75, 18)
		Me.label39.TabIndex = 8
		Me.label39.Text = "Off Hand DPS"
		Me.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label37
		'
		Me.label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label37.Location = New System.Drawing.Point(50, 49)
		Me.label37.Name = "label37"
		Me.label37.Size = New System.Drawing.Size(85, 18)
		Me.label37.TabIndex = 6
		Me.label37.Text = "Main Hand DPS"
		Me.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'gbMisc
		'
		Me.gbMisc.Controls.Add(Me.chkTailorEnchant)
		Me.gbMisc.Controls.Add(Me.chkMeta)
		Me.gbMisc.Controls.Add(Me.chkAshenBand)
		Me.gbMisc.Controls.Add(Me.chkAccelerators)
		Me.gbMisc.Controls.Add(Me.chkIngenieer)
		Me.gbMisc.Location = New System.Drawing.Point(6, 498)
		Me.gbMisc.Name = "gbMisc"
		Me.gbMisc.Size = New System.Drawing.Size(259, 169)
		Me.gbMisc.TabIndex = 19
		Me.gbMisc.TabStop = false
		Me.gbMisc.Text = "Misc."
		'
		'chkTailorEnchant
		'
		Me.chkTailorEnchant.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkTailorEnchant.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkTailorEnchant.Location = New System.Drawing.Point(147, 49)
		Me.chkTailorEnchant.Name = "chkTailorEnchant"
		Me.chkTailorEnchant.Size = New System.Drawing.Size(104, 24)
		Me.chkTailorEnchant.TabIndex = 25
		Me.chkTailorEnchant.Text = "Tailor Enchant"
		Me.chkTailorEnchant.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkTailorEnchant.UseVisualStyleBackColor = true
		'
		'chkMeta
		'
		Me.chkMeta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkMeta.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkMeta.Location = New System.Drawing.Point(147, 19)
		Me.chkMeta.Name = "chkMeta"
		Me.chkMeta.Size = New System.Drawing.Size(104, 24)
		Me.chkMeta.TabIndex = 24
		Me.chkMeta.Text = "3% Crit Damage"
		Me.chkMeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkMeta.UseVisualStyleBackColor = true
		'
		'chkAshenBand
		'
		Me.chkAshenBand.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkAshenBand.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkAshenBand.Location = New System.Drawing.Point(43, 139)
		Me.chkAshenBand.Name = "chkAshenBand"
		Me.chkAshenBand.Size = New System.Drawing.Size(208, 24)
		Me.chkAshenBand.TabIndex = 26
		Me.chkAshenBand.Text = "Ashen Band of Endless Vengeance"
		Me.chkAshenBand.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkAshenBand.UseVisualStyleBackColor = true
		'
		'chkAccelerators
		'
		Me.chkAccelerators.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkAccelerators.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkAccelerators.Location = New System.Drawing.Point(78, 109)
		Me.chkAccelerators.Name = "chkAccelerators"
		Me.chkAccelerators.Size = New System.Drawing.Size(173, 24)
		Me.chkAccelerators.TabIndex = 26
		Me.chkAccelerators.Text = "Hyperspeed Accelerators"
		Me.chkAccelerators.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkAccelerators.UseVisualStyleBackColor = true
		'
		'chkIngenieer
		'
		Me.chkIngenieer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkIngenieer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkIngenieer.Location = New System.Drawing.Point(78, 79)
		Me.chkIngenieer.Name = "chkIngenieer"
		Me.chkIngenieer.Size = New System.Drawing.Size(173, 24)
		Me.chkIngenieer.TabIndex = 26
		Me.chkIngenieer.Text = "Hand-Mounted Pyro Rocket"
		Me.chkIngenieer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkIngenieer.UseVisualStyleBackColor = true
		'
		'groupBox1
		'
		Me.groupBox1.AutoSize = true
		Me.groupBox1.Controls.Add(Me.gbWeaponProc)
		Me.groupBox1.Controls.Add(Me.gbTrinkets)
		Me.groupBox1.Controls.Add(Me.gbSetBonus)
		Me.groupBox1.Controls.Add(Me.gbMisc)
		Me.groupBox1.Controls.Add(Me.gbWeapons)
		Me.groupBox1.Controls.Add(Me.groupBox5)
		Me.groupBox1.Controls.Add(Me.gbStats)
		Me.groupBox1.Location = New System.Drawing.Point(720, 28)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(277, 1109)
		Me.groupBox1.TabIndex = 20
		Me.groupBox1.TabStop = false
		'
		'gbWeaponProc
		'
		Me.gbWeaponProc.Controls.Add(Me.cmbWeaponProc2)
		Me.gbWeaponProc.Controls.Add(Me.cmbWeaponProc1)
		Me.gbWeaponProc.Location = New System.Drawing.Point(7, 1018)
		Me.gbWeaponProc.Name = "gbWeaponProc"
		Me.gbWeaponProc.Size = New System.Drawing.Size(258, 72)
		Me.gbWeaponProc.TabIndex = 21
		Me.gbWeaponProc.TabStop = false
		Me.gbWeaponProc.Text = "Weapon Proc"
		'
		'cmbWeaponProc2
		'
		Me.cmbWeaponProc2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbWeaponProc2.Location = New System.Drawing.Point(12, 43)
		Me.cmbWeaponProc2.Name = "cmbWeaponProc2"
		Me.cmbWeaponProc2.Size = New System.Drawing.Size(231, 20)
		Me.cmbWeaponProc2.TabIndex = 21
		'
		'cmbWeaponProc1
		'
		Me.cmbWeaponProc1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbWeaponProc1.Location = New System.Drawing.Point(13, 17)
		Me.cmbWeaponProc1.Name = "cmbWeaponProc1"
		Me.cmbWeaponProc1.Size = New System.Drawing.Size(230, 20)
		Me.cmbWeaponProc1.TabIndex = 21
		'
		'gbTrinkets
		'
		Me.gbTrinkets.Controls.Add(Me.cmbTrinket2)
		Me.gbTrinkets.Controls.Add(Me.cmbTrinket1)
		Me.gbTrinkets.Location = New System.Drawing.Point(6, 927)
		Me.gbTrinkets.Name = "gbTrinkets"
		Me.gbTrinkets.Size = New System.Drawing.Size(259, 83)
		Me.gbTrinkets.TabIndex = 22
		Me.gbTrinkets.TabStop = false
		Me.gbTrinkets.Text = "Trinkets"
		'
		'cmbTrinket2
		'
		Me.cmbTrinket2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbTrinket2.Location = New System.Drawing.Point(14, 47)
		Me.cmbTrinket2.Name = "cmbTrinket2"
		Me.cmbTrinket2.Size = New System.Drawing.Size(229, 20)
		Me.cmbTrinket2.TabIndex = 21
		'
		'cmbTrinket1
		'
		Me.cmbTrinket1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbTrinket1.Location = New System.Drawing.Point(14, 21)
		Me.cmbTrinket1.Name = "cmbTrinket1"
		Me.cmbTrinket1.Size = New System.Drawing.Size(229, 20)
		Me.cmbTrinket1.TabIndex = 21
		'
		'gbSetBonus
		'
		Me.gbSetBonus.Controls.Add(Me.cmbSetBonus2)
		Me.gbSetBonus.Controls.Add(Me.cmbSetBonus1)
		Me.gbSetBonus.Location = New System.Drawing.Point(6, 834)
		Me.gbSetBonus.Name = "gbSetBonus"
		Me.gbSetBonus.Size = New System.Drawing.Size(259, 77)
		Me.gbSetBonus.TabIndex = 23
		Me.gbSetBonus.TabStop = false
		Me.gbSetBonus.Text = "Set Bonus"
		'
		'cmbSetBonus2
		'
		Me.cmbSetBonus2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbSetBonus2.Location = New System.Drawing.Point(13, 48)
		Me.cmbSetBonus2.Name = "cmbSetBonus2"
		Me.cmbSetBonus2.Size = New System.Drawing.Size(234, 20)
		Me.cmbSetBonus2.TabIndex = 24
		'
		'cmbSetBonus1
		'
		Me.cmbSetBonus1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbSetBonus1.Location = New System.Drawing.Point(13, 21)
		Me.cmbSetBonus1.Name = "cmbSetBonus1"
		Me.cmbSetBonus1.Size = New System.Drawing.Size(233, 20)
		Me.cmbSetBonus1.TabIndex = 23
		'
		'groupBox5
		'
		Me.groupBox5.Controls.Add(Me.chkArcaneTorrent)
		Me.groupBox5.Controls.Add(Me.chkBerzerking)
		Me.groupBox5.Controls.Add(Me.chkBloodFury)
		Me.groupBox5.Controls.Add(Me.txtOHExpBonus)
		Me.groupBox5.Controls.Add(Me.txtMHExpBonus)
		Me.groupBox5.Controls.Add(Me.label41)
		Me.groupBox5.Controls.Add(Me.label42)
		Me.groupBox5.Location = New System.Drawing.Point(6, 673)
		Me.groupBox5.Name = "groupBox5"
		Me.groupBox5.Size = New System.Drawing.Size(259, 155)
		Me.groupBox5.TabIndex = 21
		Me.groupBox5.TabStop = false
		Me.groupBox5.Text = "Racial"
		'
		'chkArcaneTorrent
		'
		Me.chkArcaneTorrent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkArcaneTorrent.Location = New System.Drawing.Point(92, 128)
		Me.chkArcaneTorrent.Name = "chkArcaneTorrent"
		Me.chkArcaneTorrent.RightToLeft = System.Windows.Forms.RightToLeft.Yes
		Me.chkArcaneTorrent.Size = New System.Drawing.Size(155, 24)
		Me.chkArcaneTorrent.TabIndex = 20
		Me.chkArcaneTorrent.Text = "Arcane Torrent"
		Me.chkArcaneTorrent.UseVisualStyleBackColor = true
		'
		'chkBerzerking
		'
		Me.chkBerzerking.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkBerzerking.Location = New System.Drawing.Point(63, 98)
		Me.chkBerzerking.Name = "chkBerzerking"
		Me.chkBerzerking.RightToLeft = System.Windows.Forms.RightToLeft.Yes
		Me.chkBerzerking.Size = New System.Drawing.Size(184, 24)
		Me.chkBerzerking.TabIndex = 20
		Me.chkBerzerking.Text = "Berserking"
		Me.chkBerzerking.UseVisualStyleBackColor = true
		'
		'chkBloodFury
		'
		Me.chkBloodFury.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.chkBloodFury.Location = New System.Drawing.Point(37, 68)
		Me.chkBloodFury.Name = "chkBloodFury"
		Me.chkBloodFury.RightToLeft = System.Windows.Forms.RightToLeft.Yes
		Me.chkBloodFury.Size = New System.Drawing.Size(210, 24)
		Me.chkBloodFury.TabIndex = 20
		Me.chkBloodFury.Text = "Blood Fury + Pet Damage"
		Me.chkBloodFury.UseVisualStyleBackColor = true
		'
		'txtOHExpBonus
		'
		Me.txtOHExpBonus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtOHExpBonus.Location = New System.Drawing.Point(147, 45)
		Me.txtOHExpBonus.Name = "txtOHExpBonus"
		Me.txtOHExpBonus.Size = New System.Drawing.Size(100, 20)
		Me.txtOHExpBonus.TabIndex = 19
		'
		'txtMHExpBonus
		'
		Me.txtMHExpBonus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.txtMHExpBonus.Location = New System.Drawing.Point(147, 19)
		Me.txtMHExpBonus.Name = "txtMHExpBonus"
		Me.txtMHExpBonus.Size = New System.Drawing.Size(100, 20)
		Me.txtMHExpBonus.TabIndex = 18
		'
		'label41
		'
		Me.label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label41.Location = New System.Drawing.Point(37, 47)
		Me.label41.Name = "label41"
		Me.label41.Size = New System.Drawing.Size(104, 18)
		Me.label41.TabIndex = 17
		Me.label41.Text = "Off Hand Expertise"
		Me.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label42
		'
		Me.label42.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label42.Location = New System.Drawing.Point(31, 21)
		Me.label42.Name = "label42"
		Me.label42.Size = New System.Drawing.Size(110, 18)
		Me.label42.TabIndex = 16
		Me.label42.Text = "Main Hand Expertise"
		Me.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'toolStrip2
		'
		Me.toolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdQuickEP, Me.toolStripSeparator3, Me.cmdGetDps, Me.lblDPS, Me.toolStripSeparator1, Me.toolStripSeparator2, Me.toolStripLabel1, Me.cmbRace, Me.toolStripSeparator4, Me.cmdSave, Me.toolStripSeparator5, Me.cmdSaveAsNew, Me.toolStripSeparator8, Me.toolStripLabel2, Me.cmbSkill1, Me.toolStripSeparator6, Me.toolStripLabel3, Me.cmbSkill2, Me.toolStripSeparator7, Me.cmdArmoryImport, Me.toolStripSeparator12})
		Me.toolStrip2.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip2.Name = "toolStrip2"
		Me.toolStrip2.Size = New System.Drawing.Size(997, 25)
		Me.toolStrip2.TabIndex = 24
		Me.toolStrip2.Text = "toolStrip1"
		'
		'cmdQuickEP
		'
		Me.cmdQuickEP.BackColor = System.Drawing.SystemColors.Control
		Me.cmdQuickEP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.cmdQuickEP.Image = CType(resources.GetObject("cmdQuickEP.Image"),System.Drawing.Image)
		Me.cmdQuickEP.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.cmdQuickEP.Name = "cmdQuickEP"
		Me.cmdQuickEP.Size = New System.Drawing.Size(106, 22)
		Me.cmdQuickEP.Text = "Get Quick EP Values"
		AddHandler Me.cmdQuickEP.Click, AddressOf Me.TsGetQuickEPClick
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
		'
		'cmdGetDps
		'
		Me.cmdGetDps.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.cmdGetDps.Image = CType(resources.GetObject("cmdGetDps.Image"),System.Drawing.Image)
		Me.cmdGetDps.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.cmdGetDps.Name = "cmdGetDps"
		Me.cmdGetDps.Size = New System.Drawing.Size(79, 22)
		Me.cmdGetDps.Text = "Get Quick DPS"
		AddHandler Me.cmdGetDps.Click, AddressOf Me.CmdGetDpsClick
		'
		'lblDPS
		'
		Me.lblDPS.Name = "lblDPS"
		Me.lblDPS.Size = New System.Drawing.Size(33, 22)
		Me.lblDPS.Text = "0 dps"
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
		'
		'toolStripLabel1
		'
		Me.toolStripLabel1.Name = "toolStripLabel1"
		Me.toolStripLabel1.Size = New System.Drawing.Size(38, 22)
		Me.toolStripLabel1.Text = "Race: "
		'
		'cmbRace
		'
		Me.cmbRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbRace.Name = "cmbRace"
		Me.cmbRace.Size = New System.Drawing.Size(80, 25)
		AddHandler Me.cmbRace.SelectedIndexChanged, AddressOf Me.CmbRaceSelectedIndexChanged
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(6, 25)
		'
		'cmdSave
		'
		Me.cmdSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.cmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(35, 22)
		Me.cmdSave.Text = "Save"
		AddHandler Me.cmdSave.Click, AddressOf Me.CmdSaveClick
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		Me.toolStripSeparator5.Size = New System.Drawing.Size(6, 25)
		'
		'cmdSaveAsNew
		'
		Me.cmdSaveAsNew.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.cmdSaveAsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.cmdSaveAsNew.Image = CType(resources.GetObject("cmdSaveAsNew.Image"),System.Drawing.Image)
		Me.cmdSaveAsNew.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.cmdSaveAsNew.Name = "cmdSaveAsNew"
		Me.cmdSaveAsNew.Size = New System.Drawing.Size(72, 22)
		Me.cmdSaveAsNew.Text = "Save as new"
		AddHandler Me.cmdSaveAsNew.Click, AddressOf Me.CmdSaveAsNewClick
		'
		'toolStripSeparator8
		'
		Me.toolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.toolStripSeparator8.Name = "toolStripSeparator8"
		Me.toolStripSeparator8.Size = New System.Drawing.Size(6, 25)
		'
		'toolStripLabel2
		'
		Me.toolStripLabel2.Name = "toolStripLabel2"
		Me.toolStripLabel2.Size = New System.Drawing.Size(37, 22)
		Me.toolStripLabel2.Text = "Skill 1:"
		'
		'cmbSkill1
		'
		Me.cmbSkill1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbSkill1.Name = "cmbSkill1"
		Me.cmbSkill1.Size = New System.Drawing.Size(100, 25)
		AddHandler Me.cmbSkill1.SelectedIndexChanged, AddressOf Me.CmbSkillClick
		'
		'toolStripSeparator6
		'
		Me.toolStripSeparator6.Name = "toolStripSeparator6"
		Me.toolStripSeparator6.Size = New System.Drawing.Size(6, 25)
		'
		'toolStripLabel3
		'
		Me.toolStripLabel3.Name = "toolStripLabel3"
		Me.toolStripLabel3.Size = New System.Drawing.Size(37, 22)
		Me.toolStripLabel3.Text = "Skill 2:"
		'
		'cmbSkill2
		'
		Me.cmbSkill2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbSkill2.Name = "cmbSkill2"
		Me.cmbSkill2.Size = New System.Drawing.Size(100, 25)
		AddHandler Me.cmbSkill2.SelectedIndexChanged, AddressOf Me.CmbSkillClick
		'
		'toolStripSeparator7
		'
		Me.toolStripSeparator7.Name = "toolStripSeparator7"
		Me.toolStripSeparator7.Size = New System.Drawing.Size(6, 25)
		'
		'toolStrip1
		'
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripLabel4, Me.cmbFlask, Me.toolStripSeparator9, Me.toolStripLabel5, Me.cmbFood, Me.toolStripSeparator10, Me.ddConsumable, Me.toolStripSeparator11})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 25)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(997, 25)
		Me.toolStrip1.TabIndex = 25
		Me.toolStrip1.Text = "toolStrip2"
		'
		'toolStripLabel4
		'
		Me.toolStripLabel4.Name = "toolStripLabel4"
		Me.toolStripLabel4.Size = New System.Drawing.Size(38, 22)
		Me.toolStripLabel4.Text = "Flask: "
		'
		'cmbFlask
		'
		Me.cmbFlask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbFlask.Name = "cmbFlask"
		Me.cmbFlask.Size = New System.Drawing.Size(150, 25)
		AddHandler Me.cmbFlask.SelectedIndexChanged, AddressOf Me.CmbFlaskSelectionChange
		'
		'toolStripSeparator9
		'
		Me.toolStripSeparator9.Name = "toolStripSeparator9"
		Me.toolStripSeparator9.Size = New System.Drawing.Size(6, 25)
		'
		'toolStripLabel5
		'
		Me.toolStripLabel5.Name = "toolStripLabel5"
		Me.toolStripLabel5.Size = New System.Drawing.Size(38, 22)
		Me.toolStripLabel5.Text = "Food: "
		'
		'cmbFood
		'
		Me.cmbFood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbFood.Name = "cmbFood"
		Me.cmbFood.Size = New System.Drawing.Size(150, 25)
		AddHandler Me.cmbFood.SelectedIndexChanged, AddressOf Me.cmbFoodSelectionChange
		'
		'toolStripSeparator10
		'
		Me.toolStripSeparator10.Name = "toolStripSeparator10"
		Me.toolStripSeparator10.Size = New System.Drawing.Size(6, 25)
		'
		'ddConsumable
		'
		Me.ddConsumable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.ddConsumable.Image = CType(resources.GetObject("ddConsumable.Image"),System.Drawing.Image)
		Me.ddConsumable.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.ddConsumable.Name = "ddConsumable"
		Me.ddConsumable.Size = New System.Drawing.Size(83, 22)
		Me.ddConsumable.Text = "Consumables"
		'
		'toolStripSeparator11
		'
		Me.toolStripSeparator11.Name = "toolStripSeparator11"
		Me.toolStripSeparator11.Size = New System.Drawing.Size(6, 25)
		'
		'cmdArmoryImport
		'
		Me.cmdArmoryImport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.cmdArmoryImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.cmdArmoryImport.Image = CType(resources.GetObject("cmdArmoryImport.Image"),System.Drawing.Image)
		Me.cmdArmoryImport.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.cmdArmoryImport.Name = "cmdArmoryImport"
		Me.cmdArmoryImport.Size = New System.Drawing.Size(46, 22)
		Me.cmdArmoryImport.Text = "Armory"
		AddHandler Me.cmdArmoryImport.Click, AddressOf Me.CmdArmoryImportClick
		'
		'toolStripSeparator12
		'
		Me.toolStripSeparator12.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.toolStripSeparator12.Name = "toolStripSeparator12"
		Me.toolStripSeparator12.Size = New System.Drawing.Size(6, 25)
		'
		'GearSelectorMainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = true
		Me.ClientSize = New System.Drawing.Size(1009, 786)
		Me.Controls.Add(Me.toolStrip1)
		Me.Controls.Add(Me.toolStrip2)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.cmdExtrator)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "GearSelectorMainForm"
		Me.Text = "Gear Selector"
		AddHandler Load, AddressOf Me.MainFormLoad
		Me.gbStats.ResumeLayout(false)
		Me.gbStats.PerformLayout
		Me.gbWeapons.ResumeLayout(false)
		Me.gbWeapons.PerformLayout
		Me.gbMisc.ResumeLayout(false)
		Me.groupBox1.ResumeLayout(false)
		Me.gbWeaponProc.ResumeLayout(false)
		Me.gbWeaponProc.PerformLayout
		Me.gbTrinkets.ResumeLayout(false)
		Me.gbTrinkets.PerformLayout
		Me.gbSetBonus.ResumeLayout(false)
		Me.gbSetBonus.PerformLayout
		Me.groupBox5.ResumeLayout(false)
		Me.groupBox5.PerformLayout
		Me.toolStrip2.ResumeLayout(false)
		Me.toolStrip2.PerformLayout
		Me.toolStrip1.ResumeLayout(false)
		Me.toolStrip1.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private toolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
	Private cmdArmoryImport As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
	Private ddConsumable As System.Windows.Forms.ToolStripDropDownButton
	Private toolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Friend cmbFood As System.Windows.Forms.ToolStripComboBox
	Private toolStripLabel5 As System.Windows.Forms.ToolStripLabel
	Private toolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
	Friend cmbFlask As System.Windows.Forms.ToolStripComboBox
	Private toolStripLabel4 As System.Windows.Forms.ToolStripLabel
	Private toolStrip2 As System.Windows.Forms.ToolStrip
	Private toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
	Friend cmbSkill2 As System.Windows.Forms.ToolStripComboBox
	Private toolStripLabel3 As System.Windows.Forms.ToolStripLabel
	Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
	Friend cmbSkill1 As System.Windows.Forms.ToolStripComboBox
	Private toolStripLabel2 As System.Windows.Forms.ToolStripLabel
	Private toolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private toolStripLabel1 As System.Windows.Forms.ToolStripLabel
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private lblArP As System.Windows.Forms.Label
	Private lblHit As System.Windows.Forms.Label
	Private lblHaste As System.Windows.Forms.Label
	Private lblInt As System.Windows.Forms.Label
	Private lblAP As System.Windows.Forms.Label
	Private lblCrit As System.Windows.Forms.Label
	Private lblExp As System.Windows.Forms.Label
	Private lblAgi As System.Windows.Forms.Label
	Private lblArM As System.Windows.Forms.Label
	Private lblStr As System.Windows.Forms.Label
	Private lblDPS As System.Windows.Forms.ToolStripLabel
	Private cmdGetDps As System.Windows.Forms.ToolStripButton
	Private cmdQuickEP As System.Windows.Forms.ToolStripButton
	Private cmdSaveAsNew As System.Windows.Forms.ToolStripButton
	Private cmbWeaponProc1 As System.Windows.Forms.TextBox
	Private cmbWeaponProc2 As System.Windows.Forms.TextBox
	Private gbWeaponProc As System.Windows.Forms.GroupBox
	Private cmbSetBonus1 As System.Windows.Forms.TextBox
	Private cmbSetBonus2 As System.Windows.Forms.TextBox
	Private gbSetBonus As System.Windows.Forms.GroupBox
	Private cmbTrinket1 As System.Windows.Forms.TextBox
	Private cmbTrinket2 As System.Windows.Forms.TextBox
	Private gbTrinkets As System.Windows.Forms.GroupBox
	Private label42 As System.Windows.Forms.Label
	Private label41 As System.Windows.Forms.Label
	Private txtMHExpBonus As System.Windows.Forms.TextBox
	Private txtOHExpBonus As System.Windows.Forms.TextBox
	Private chkBloodFury As System.Windows.Forms.CheckBox
	Private chkBerzerking As System.Windows.Forms.CheckBox
	Private chkArcaneTorrent As System.Windows.Forms.CheckBox
	Private groupBox5 As System.Windows.Forms.GroupBox
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private chkIngenieer As System.Windows.Forms.CheckBox
	Private chkAccelerators As System.Windows.Forms.CheckBox
	Private chkAshenBand As System.Windows.Forms.CheckBox
	Private chkMeta As System.Windows.Forms.CheckBox
	Private chkTailorEnchant As System.Windows.Forms.CheckBox
	Private gbMisc As System.Windows.Forms.GroupBox
	Private label37 As System.Windows.Forms.Label
	Private label39 As System.Windows.Forms.Label
	Private txtMHDPS As System.Windows.Forms.TextBox
	Private txtOHDPS As System.Windows.Forms.TextBox
	Private label38 As System.Windows.Forms.Label
	Private label40 As System.Windows.Forms.Label
	Private txtMHWSpeed As System.Windows.Forms.TextBox
	Private txtOHWSpeed As System.Windows.Forms.TextBox
	Private r2Hand As System.Windows.Forms.RadioButton
	Private rDW As System.Windows.Forms.RadioButton
	Private gbWeapons As System.Windows.Forms.GroupBox
	Friend cmbRace As System.Windows.Forms.ToolStripComboBox
	Private txtArmor As System.Windows.Forms.TextBox
	Private txtExp As System.Windows.Forms.TextBox
	Private txtAgi As System.Windows.Forms.TextBox
	Private txtCrit As System.Windows.Forms.TextBox
	Private txtAP As System.Windows.Forms.TextBox
	Private txtIntel As System.Windows.Forms.TextBox
	Private txtHaste As System.Windows.Forms.TextBox
	Private txtHit As System.Windows.Forms.TextBox
	Private txtArP As System.Windows.Forms.TextBox
	Private gbStats As System.Windows.Forms.GroupBox
	Private txtStr As System.Windows.Forms.TextBox
	Private cmdSave As System.Windows.Forms.ToolStripButton
	Private cmdExtrator As System.Windows.Forms.Button
	
	
	
	
End Class
