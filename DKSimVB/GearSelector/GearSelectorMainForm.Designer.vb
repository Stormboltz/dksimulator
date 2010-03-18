'
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
		Me.cmdExtrator = New System.Windows.Forms.Button
		Me.cmdLoadGear = New System.Windows.Forms.Button
		Me.cmdSave = New System.Windows.Forms.Button
		Me.gbStats = New System.Windows.Forms.GroupBox
		Me.txtArP = New System.Windows.Forms.TextBox
		Me.label35 = New System.Windows.Forms.Label
		Me.txtHit = New System.Windows.Forms.TextBox
		Me.label32 = New System.Windows.Forms.Label
		Me.txtHaste = New System.Windows.Forms.TextBox
		Me.txtIntel = New System.Windows.Forms.TextBox
		Me.txtAP = New System.Windows.Forms.TextBox
		Me.label34 = New System.Windows.Forms.Label
		Me.label29 = New System.Windows.Forms.Label
		Me.label31 = New System.Windows.Forms.Label
		Me.txtCrit = New System.Windows.Forms.TextBox
		Me.txtAgi = New System.Windows.Forms.TextBox
		Me.txtExp = New System.Windows.Forms.TextBox
		Me.txtArmor = New System.Windows.Forms.TextBox
		Me.label33 = New System.Windows.Forms.Label
		Me.label36 = New System.Windows.Forms.Label
		Me.label28 = New System.Windows.Forms.Label
		Me.label30 = New System.Windows.Forms.Label
		Me.txtStr = New System.Windows.Forms.TextBox
		Me.label27 = New System.Windows.Forms.Label
		Me.cmbRace = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
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
		Me.gbStats.SuspendLayout
		Me.gbWeapons.SuspendLayout
		Me.gbMisc.SuspendLayout
		Me.groupBox1.SuspendLayout
		Me.gbWeaponProc.SuspendLayout
		Me.gbTrinkets.SuspendLayout
		Me.gbSetBonus.SuspendLayout
		Me.groupBox5.SuspendLayout
		Me.SuspendLayout
		'
		'cmdExtrator
		'
		Me.cmdExtrator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdExtrator.Location = New System.Drawing.Point(729, 4232)
		Me.cmdExtrator.Name = "cmdExtrator"
		Me.cmdExtrator.Size = New System.Drawing.Size(75, 23)
		Me.cmdExtrator.TabIndex = 0
		Me.cmdExtrator.Text = "Extract"
		Me.cmdExtrator.UseVisualStyleBackColor = true
		AddHandler Me.cmdExtrator.Click, AddressOf Me.CmdExtratorClick
		'
		'cmdLoadGear
		'
		Me.cmdLoadGear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdLoadGear.Location = New System.Drawing.Point(722, 12)
		Me.cmdLoadGear.Name = "cmdLoadGear"
		Me.cmdLoadGear.Size = New System.Drawing.Size(75, 23)
		Me.cmdLoadGear.TabIndex = 2
		Me.cmdLoadGear.Text = "Load"
		Me.cmdLoadGear.UseVisualStyleBackColor = true
		AddHandler Me.cmdLoadGear.Click, AddressOf Me.CmdLoadGearClick
		'
		'cmdSave
		'
		Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdSave.Location = New System.Drawing.Point(819, 12)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(72, 23)
		Me.cmdSave.TabIndex = 4
		Me.cmdSave.Text = "Save"
		Me.cmdSave.UseVisualStyleBackColor = true
		AddHandler Me.cmdSave.Click, AddressOf Me.CmdSaveClick
		'
		'gbStats
		'
		Me.gbStats.Controls.Add(Me.txtArP)
		Me.gbStats.Controls.Add(Me.label35)
		Me.gbStats.Controls.Add(Me.txtHit)
		Me.gbStats.Controls.Add(Me.label32)
		Me.gbStats.Controls.Add(Me.txtHaste)
		Me.gbStats.Controls.Add(Me.txtIntel)
		Me.gbStats.Controls.Add(Me.txtAP)
		Me.gbStats.Controls.Add(Me.label34)
		Me.gbStats.Controls.Add(Me.label29)
		Me.gbStats.Controls.Add(Me.label31)
		Me.gbStats.Controls.Add(Me.txtCrit)
		Me.gbStats.Controls.Add(Me.txtAgi)
		Me.gbStats.Controls.Add(Me.txtExp)
		Me.gbStats.Controls.Add(Me.txtArmor)
		Me.gbStats.Controls.Add(Me.label33)
		Me.gbStats.Controls.Add(Me.label36)
		Me.gbStats.Controls.Add(Me.label28)
		Me.gbStats.Controls.Add(Me.label30)
		Me.gbStats.Controls.Add(Me.txtStr)
		Me.gbStats.Controls.Add(Me.label27)
		Me.gbStats.Location = New System.Drawing.Point(6, 14)
		Me.gbStats.Name = "gbStats"
		Me.gbStats.Size = New System.Drawing.Size(207, 298)
		Me.gbStats.TabIndex = 15
		Me.gbStats.TabStop = false
		Me.gbStats.Text = "Statistics"
		'
		'txtArP
		'
		Me.txtArP.Location = New System.Drawing.Point(101, 235)
		Me.txtArP.Name = "txtArP"
		Me.txtArP.Size = New System.Drawing.Size(100, 20)
		Me.txtArP.TabIndex = 9
		'
		'label35
		'
		Me.label35.Location = New System.Drawing.Point(-4, 235)
		Me.label35.Name = "label35"
		Me.label35.Size = New System.Drawing.Size(99, 28)
		Me.label35.TabIndex = 13
		Me.label35.Text = "Armor Penetration Rating"
		Me.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtHit
		'
		Me.txtHit.Location = New System.Drawing.Point(101, 155)
		Me.txtHit.Name = "txtHit"
		Me.txtHit.Size = New System.Drawing.Size(100, 20)
		Me.txtHit.TabIndex = 6
		'
		'label32
		'
		Me.label32.Location = New System.Drawing.Point(20, 155)
		Me.label32.Name = "label32"
		Me.label32.Size = New System.Drawing.Size(75, 18)
		Me.label32.TabIndex = 12
		Me.label32.Text = "Hit Rating"
		Me.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtHaste
		'
		Me.txtHaste.Location = New System.Drawing.Point(101, 209)
		Me.txtHaste.Name = "txtHaste"
		Me.txtHaste.Size = New System.Drawing.Size(100, 20)
		Me.txtHaste.TabIndex = 8
		'
		'txtIntel
		'
		Me.txtIntel.Location = New System.Drawing.Point(101, 68)
		Me.txtIntel.Name = "txtIntel"
		Me.txtIntel.Size = New System.Drawing.Size(100, 20)
		Me.txtIntel.TabIndex = 3
		'
		'txtAP
		'
		Me.txtAP.Location = New System.Drawing.Point(101, 120)
		Me.txtAP.Name = "txtAP"
		Me.txtAP.Size = New System.Drawing.Size(100, 20)
		Me.txtAP.TabIndex = 5
		'
		'label34
		'
		Me.label34.Location = New System.Drawing.Point(20, 209)
		Me.label34.Name = "label34"
		Me.label34.Size = New System.Drawing.Size(75, 18)
		Me.label34.TabIndex = 7
		Me.label34.Text = "Haste Rating"
		Me.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label29
		'
		Me.label29.Location = New System.Drawing.Point(20, 68)
		Me.label29.Name = "label29"
		Me.label29.Size = New System.Drawing.Size(75, 18)
		Me.label29.TabIndex = 6
		Me.label29.Text = "Intelligence"
		Me.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label31
		'
		Me.label31.Location = New System.Drawing.Point(6, 120)
		Me.label31.Name = "label31"
		Me.label31.Size = New System.Drawing.Size(89, 31)
		Me.label31.TabIndex = 14
		Me.label31.Text = "Attack Power (Green Number)"
		Me.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtCrit
		'
		Me.txtCrit.Location = New System.Drawing.Point(101, 183)
		Me.txtCrit.Name = "txtCrit"
		Me.txtCrit.Size = New System.Drawing.Size(100, 20)
		Me.txtCrit.TabIndex = 7
		'
		'txtAgi
		'
		Me.txtAgi.Location = New System.Drawing.Point(101, 42)
		Me.txtAgi.Name = "txtAgi"
		Me.txtAgi.Size = New System.Drawing.Size(100, 20)
		Me.txtAgi.TabIndex = 2
		'
		'txtExp
		'
		Me.txtExp.Location = New System.Drawing.Point(101, 267)
		Me.txtExp.Name = "txtExp"
		Me.txtExp.Size = New System.Drawing.Size(100, 20)
		Me.txtExp.TabIndex = 10
		'
		'txtArmor
		'
		Me.txtArmor.Location = New System.Drawing.Point(101, 94)
		Me.txtArmor.Name = "txtArmor"
		Me.txtArmor.Size = New System.Drawing.Size(100, 20)
		Me.txtArmor.TabIndex = 4
		'
		'label33
		'
		Me.label33.Location = New System.Drawing.Point(20, 183)
		Me.label33.Name = "label33"
		Me.label33.Size = New System.Drawing.Size(75, 18)
		Me.label33.TabIndex = 8
		Me.label33.Text = "Crit Rating"
		Me.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label36
		'
		Me.label36.Location = New System.Drawing.Point(8, 267)
		Me.label36.Name = "label36"
		Me.label36.Size = New System.Drawing.Size(87, 18)
		Me.label36.TabIndex = 11
		Me.label36.Text = "Expertise Rating"
		Me.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label28
		'
		Me.label28.Location = New System.Drawing.Point(20, 42)
		Me.label28.Name = "label28"
		Me.label28.Size = New System.Drawing.Size(75, 18)
		Me.label28.TabIndex = 10
		Me.label28.Text = "Agility"
		Me.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label30
		'
		Me.label30.Location = New System.Drawing.Point(20, 94)
		Me.label30.Name = "label30"
		Me.label30.Size = New System.Drawing.Size(75, 18)
		Me.label30.TabIndex = 9
		Me.label30.Text = "Armor"
		Me.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtStr
		'
		Me.txtStr.Location = New System.Drawing.Point(101, 16)
		Me.txtStr.Name = "txtStr"
		Me.txtStr.Size = New System.Drawing.Size(100, 20)
		Me.txtStr.TabIndex = 1
		'
		'label27
		'
		Me.label27.Location = New System.Drawing.Point(20, 16)
		Me.label27.Name = "label27"
		Me.label27.Size = New System.Drawing.Size(75, 18)
		Me.label27.TabIndex = 5
		Me.label27.Text = "Strength"
		Me.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cmbRace
		'
		Me.cmbRace.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmbRace.FormattingEnabled = true
		Me.cmbRace.Location = New System.Drawing.Point(785, 41)
		Me.cmbRace.Name = "cmbRace"
		Me.cmbRace.Size = New System.Drawing.Size(100, 21)
		Me.cmbRace.TabIndex = 16
		AddHandler Me.cmbRace.SelectedIndexChanged, AddressOf Me.CmbRaceSelectedIndexChanged
		'
		'label1
		'
		Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label1.Location = New System.Drawing.Point(705, 41)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(75, 18)
		Me.label1.TabIndex = 17
		Me.label1.Text = "Race"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
		Me.gbWeapons.Size = New System.Drawing.Size(207, 174)
		Me.gbWeapons.TabIndex = 18
		Me.gbWeapons.TabStop = false
		Me.gbWeapons.Text = "Weapon(s)"
		'
		'rDW
		'
		Me.rDW.Location = New System.Drawing.Point(122, 19)
		Me.rDW.Name = "rDW"
		Me.rDW.Size = New System.Drawing.Size(104, 24)
		Me.rDW.TabIndex = 12
		Me.rDW.Text = "Dual wield"
		Me.rDW.UseVisualStyleBackColor = true
		AddHandler Me.rDW.CheckedChanged, AddressOf Me.RDWCheckedChanged
		'
		'r2Hand
		'
		Me.r2Hand.Checked = true
		Me.r2Hand.Location = New System.Drawing.Point(48, 19)
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
		Me.txtOHWSpeed.Enabled = false
		Me.txtOHWSpeed.Location = New System.Drawing.Point(89, 135)
		Me.txtOHWSpeed.Name = "txtOHWSpeed"
		Me.txtOHWSpeed.Size = New System.Drawing.Size(100, 20)
		Me.txtOHWSpeed.TabIndex = 16
		Me.txtOHWSpeed.Text = "0"
		'
		'txtMHWSpeed
		'
		Me.txtMHWSpeed.Location = New System.Drawing.Point(89, 75)
		Me.txtMHWSpeed.Name = "txtMHWSpeed"
		Me.txtMHWSpeed.Size = New System.Drawing.Size(100, 20)
		Me.txtMHWSpeed.TabIndex = 14
		Me.txtMHWSpeed.Text = "0"
		'
		'label40
		'
		Me.label40.Location = New System.Drawing.Point(8, 135)
		Me.label40.Name = "label40"
		Me.label40.Size = New System.Drawing.Size(75, 31)
		Me.label40.TabIndex = 9
		Me.label40.Text = "OH Weapon Speed"
		Me.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label38
		'
		Me.label38.Location = New System.Drawing.Point(-6, 75)
		Me.label38.Name = "label38"
		Me.label38.Size = New System.Drawing.Size(89, 31)
		Me.label38.TabIndex = 7
		Me.label38.Text = "MH Weapon Speed"
		Me.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtOHDPS
		'
		Me.txtOHDPS.Enabled = false
		Me.txtOHDPS.Location = New System.Drawing.Point(89, 109)
		Me.txtOHDPS.Name = "txtOHDPS"
		Me.txtOHDPS.Size = New System.Drawing.Size(100, 20)
		Me.txtOHDPS.TabIndex = 15
		Me.txtOHDPS.Text = "0"
		'
		'txtMHDPS
		'
		Me.txtMHDPS.Location = New System.Drawing.Point(89, 49)
		Me.txtMHDPS.Name = "txtMHDPS"
		Me.txtMHDPS.Size = New System.Drawing.Size(100, 20)
		Me.txtMHDPS.TabIndex = 13
		Me.txtMHDPS.Text = "0"
		'
		'label39
		'
		Me.label39.Location = New System.Drawing.Point(8, 109)
		Me.label39.Name = "label39"
		Me.label39.Size = New System.Drawing.Size(75, 18)
		Me.label39.TabIndex = 8
		Me.label39.Text = "Off Hand DPS"
		Me.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label37
		'
		Me.label37.Location = New System.Drawing.Point(-2, 49)
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
		Me.gbMisc.Size = New System.Drawing.Size(210, 169)
		Me.gbMisc.TabIndex = 19
		Me.gbMisc.TabStop = false
		Me.gbMisc.Text = "Misc."
		'
		'chkTailorEnchant
		'
		Me.chkTailorEnchant.Location = New System.Drawing.Point(10, 49)
		Me.chkTailorEnchant.Name = "chkTailorEnchant"
		Me.chkTailorEnchant.Size = New System.Drawing.Size(104, 24)
		Me.chkTailorEnchant.TabIndex = 25
		Me.chkTailorEnchant.Text = "Tailor Enchant"
		Me.chkTailorEnchant.UseVisualStyleBackColor = true
		'
		'chkMeta
		'
		Me.chkMeta.Location = New System.Drawing.Point(10, 19)
		Me.chkMeta.Name = "chkMeta"
		Me.chkMeta.Size = New System.Drawing.Size(104, 24)
		Me.chkMeta.TabIndex = 24
		Me.chkMeta.Text = "3% Crit Damage"
		Me.chkMeta.UseVisualStyleBackColor = true
		'
		'chkAshenBand
		'
		Me.chkAshenBand.Location = New System.Drawing.Point(10, 137)
		Me.chkAshenBand.Name = "chkAshenBand"
		Me.chkAshenBand.Size = New System.Drawing.Size(208, 24)
		Me.chkAshenBand.TabIndex = 26
		Me.chkAshenBand.Text = "Ashen Band of Endless Vengeance"
		Me.chkAshenBand.UseVisualStyleBackColor = true
		'
		'chkAccelerators
		'
		Me.chkAccelerators.Location = New System.Drawing.Point(10, 109)
		Me.chkAccelerators.Name = "chkAccelerators"
		Me.chkAccelerators.Size = New System.Drawing.Size(173, 24)
		Me.chkAccelerators.TabIndex = 26
		Me.chkAccelerators.Text = "Hyperspeed Accelerators"
		Me.chkAccelerators.UseVisualStyleBackColor = true
		'
		'chkIngenieer
		'
		Me.chkIngenieer.Location = New System.Drawing.Point(10, 79)
		Me.chkIngenieer.Name = "chkIngenieer"
		Me.chkIngenieer.Size = New System.Drawing.Size(173, 24)
		Me.chkIngenieer.TabIndex = 26
		Me.chkIngenieer.Text = "Hand-Mounted Pyro Rocket"
		Me.chkIngenieer.UseVisualStyleBackColor = true
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.groupBox1.AutoSize = true
		Me.groupBox1.Controls.Add(Me.gbWeaponProc)
		Me.groupBox1.Controls.Add(Me.gbTrinkets)
		Me.groupBox1.Controls.Add(Me.gbSetBonus)
		Me.groupBox1.Controls.Add(Me.gbMisc)
		Me.groupBox1.Controls.Add(Me.gbWeapons)
		Me.groupBox1.Controls.Add(Me.groupBox5)
		Me.groupBox1.Controls.Add(Me.gbStats)
		Me.groupBox1.Location = New System.Drawing.Point(645, 68)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(240, 1109)
		Me.groupBox1.TabIndex = 20
		Me.groupBox1.TabStop = false
		'
		'gbWeaponProc
		'
		Me.gbWeaponProc.Controls.Add(Me.cmbWeaponProc2)
		Me.gbWeaponProc.Controls.Add(Me.cmbWeaponProc1)
		Me.gbWeaponProc.Location = New System.Drawing.Point(7, 1018)
		Me.gbWeaponProc.Name = "gbWeaponProc"
		Me.gbWeaponProc.Size = New System.Drawing.Size(217, 72)
		Me.gbWeaponProc.TabIndex = 21
		Me.gbWeaponProc.TabStop = false
		Me.gbWeaponProc.Text = "Weapon Proc"
		'
		'cmbWeaponProc2
		'
		Me.cmbWeaponProc2.Location = New System.Drawing.Point(13, 43)
		Me.cmbWeaponProc2.Name = "cmbWeaponProc2"
		Me.cmbWeaponProc2.Size = New System.Drawing.Size(189, 20)
		Me.cmbWeaponProc2.TabIndex = 21
		'
		'cmbWeaponProc1
		'
		Me.cmbWeaponProc1.Location = New System.Drawing.Point(13, 17)
		Me.cmbWeaponProc1.Name = "cmbWeaponProc1"
		Me.cmbWeaponProc1.Size = New System.Drawing.Size(189, 20)
		Me.cmbWeaponProc1.TabIndex = 21
		'
		'gbTrinkets
		'
		Me.gbTrinkets.Controls.Add(Me.cmbTrinket2)
		Me.gbTrinkets.Controls.Add(Me.cmbTrinket1)
		Me.gbTrinkets.Location = New System.Drawing.Point(6, 927)
		Me.gbTrinkets.Name = "gbTrinkets"
		Me.gbTrinkets.Size = New System.Drawing.Size(217, 83)
		Me.gbTrinkets.TabIndex = 22
		Me.gbTrinkets.TabStop = false
		Me.gbTrinkets.Text = "Trinkets"
		'
		'cmbTrinket2
		'
		Me.cmbTrinket2.Location = New System.Drawing.Point(13, 47)
		Me.cmbTrinket2.Name = "cmbTrinket2"
		Me.cmbTrinket2.Size = New System.Drawing.Size(188, 20)
		Me.cmbTrinket2.TabIndex = 21
		'
		'cmbTrinket1
		'
		Me.cmbTrinket1.Location = New System.Drawing.Point(13, 21)
		Me.cmbTrinket1.Name = "cmbTrinket1"
		Me.cmbTrinket1.Size = New System.Drawing.Size(188, 20)
		Me.cmbTrinket1.TabIndex = 21
		'
		'gbSetBonus
		'
		Me.gbSetBonus.Controls.Add(Me.cmbSetBonus2)
		Me.gbSetBonus.Controls.Add(Me.cmbSetBonus1)
		Me.gbSetBonus.Location = New System.Drawing.Point(6, 834)
		Me.gbSetBonus.Name = "gbSetBonus"
		Me.gbSetBonus.Size = New System.Drawing.Size(214, 77)
		Me.gbSetBonus.TabIndex = 23
		Me.gbSetBonus.TabStop = false
		Me.gbSetBonus.Text = "Set Bonus"
		'
		'cmbSetBonus2
		'
		Me.cmbSetBonus2.Location = New System.Drawing.Point(13, 48)
		Me.cmbSetBonus2.Name = "cmbSetBonus2"
		Me.cmbSetBonus2.Size = New System.Drawing.Size(189, 20)
		Me.cmbSetBonus2.TabIndex = 24
		'
		'cmbSetBonus1
		'
		Me.cmbSetBonus1.Location = New System.Drawing.Point(13, 21)
		Me.cmbSetBonus1.Name = "cmbSetBonus1"
		Me.cmbSetBonus1.Size = New System.Drawing.Size(188, 20)
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
		Me.groupBox5.Size = New System.Drawing.Size(228, 155)
		Me.groupBox5.TabIndex = 21
		Me.groupBox5.TabStop = false
		Me.groupBox5.Text = "Racial"
		'
		'chkArcaneTorrent
		'
		Me.chkArcaneTorrent.Location = New System.Drawing.Point(61, 128)
		Me.chkArcaneTorrent.Name = "chkArcaneTorrent"
		Me.chkArcaneTorrent.RightToLeft = System.Windows.Forms.RightToLeft.Yes
		Me.chkArcaneTorrent.Size = New System.Drawing.Size(155, 24)
		Me.chkArcaneTorrent.TabIndex = 20
		Me.chkArcaneTorrent.Text = "Arcane Torrent"
		Me.chkArcaneTorrent.UseVisualStyleBackColor = true
		'
		'chkBerzerking
		'
		Me.chkBerzerking.Location = New System.Drawing.Point(32, 98)
		Me.chkBerzerking.Name = "chkBerzerking"
		Me.chkBerzerking.RightToLeft = System.Windows.Forms.RightToLeft.Yes
		Me.chkBerzerking.Size = New System.Drawing.Size(184, 24)
		Me.chkBerzerking.TabIndex = 20
		Me.chkBerzerking.Text = "Berserking"
		Me.chkBerzerking.UseVisualStyleBackColor = true
		'
		'chkBloodFury
		'
		Me.chkBloodFury.Location = New System.Drawing.Point(6, 68)
		Me.chkBloodFury.Name = "chkBloodFury"
		Me.chkBloodFury.RightToLeft = System.Windows.Forms.RightToLeft.Yes
		Me.chkBloodFury.Size = New System.Drawing.Size(210, 24)
		Me.chkBloodFury.TabIndex = 20
		Me.chkBloodFury.Text = "Blood Fury + Pet Damage"
		Me.chkBloodFury.UseVisualStyleBackColor = true
		'
		'txtOHExpBonus
		'
		Me.txtOHExpBonus.Location = New System.Drawing.Point(116, 45)
		Me.txtOHExpBonus.Name = "txtOHExpBonus"
		Me.txtOHExpBonus.Size = New System.Drawing.Size(100, 20)
		Me.txtOHExpBonus.TabIndex = 19
		'
		'txtMHExpBonus
		'
		Me.txtMHExpBonus.Location = New System.Drawing.Point(116, 19)
		Me.txtMHExpBonus.Name = "txtMHExpBonus"
		Me.txtMHExpBonus.Size = New System.Drawing.Size(100, 20)
		Me.txtMHExpBonus.TabIndex = 18
		'
		'label41
		'
		Me.label41.Location = New System.Drawing.Point(6, 47)
		Me.label41.Name = "label41"
		Me.label41.Size = New System.Drawing.Size(104, 18)
		Me.label41.TabIndex = 17
		Me.label41.Text = "Off Hand Expertise"
		Me.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label42
		'
		Me.label42.Location = New System.Drawing.Point(0, 21)
		Me.label42.Name = "label42"
		Me.label42.Size = New System.Drawing.Size(110, 18)
		Me.label42.TabIndex = 16
		Me.label42.Text = "Main Hand Expertise"
		Me.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'GearSelectorMainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = true
		Me.ClientSize = New System.Drawing.Size(982, 687)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.cmbRace)
		Me.Controls.Add(Me.cmdSave)
		Me.Controls.Add(Me.cmdLoadGear)
		Me.Controls.Add(Me.cmdExtrator)
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
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
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
	Private label1 As System.Windows.Forms.Label
	Private cmbRace As System.Windows.Forms.ComboBox
	Private label27 As System.Windows.Forms.Label
	Private label30 As System.Windows.Forms.Label
	Private label28 As System.Windows.Forms.Label
	Private label36 As System.Windows.Forms.Label
	Private label33 As System.Windows.Forms.Label
	Private txtArmor As System.Windows.Forms.TextBox
	Private txtExp As System.Windows.Forms.TextBox
	Private txtAgi As System.Windows.Forms.TextBox
	Private txtCrit As System.Windows.Forms.TextBox
	Private label31 As System.Windows.Forms.Label
	Private label29 As System.Windows.Forms.Label
	Private label34 As System.Windows.Forms.Label
	Private txtAP As System.Windows.Forms.TextBox
	Private txtIntel As System.Windows.Forms.TextBox
	Private txtHaste As System.Windows.Forms.TextBox
	Private label32 As System.Windows.Forms.Label
	Private txtHit As System.Windows.Forms.TextBox
	Private label35 As System.Windows.Forms.Label
	Private txtArP As System.Windows.Forms.TextBox
	Private gbStats As System.Windows.Forms.GroupBox
	Private txtStr As System.Windows.Forms.TextBox
	Private cmdSave As System.Windows.Forms.Button
	Private cmdLoadGear As System.Windows.Forms.Button
	Private cmdExtrator As System.Windows.Forms.Button
	
	
	
	
End Class
