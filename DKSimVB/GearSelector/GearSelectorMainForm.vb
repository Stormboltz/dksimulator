'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 10:41 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Imports System.Xml
Imports System.Net

Public Partial Class GearSelectorMainForm
	
	friend EquipmentList as new Collection
	Friend InLoad As Boolean
	Friend EnchantSelector As New EnchantSelector(me)
	Friend GemSelector As New GemSelector(me)
	Friend GearSelector As new GearSelector(me)
	Friend ItemDB As  Xml.XmlDocument
	Friend GemDB As  Xml.XmlDocument
	Friend GemBonusDB As  Xml.XmlDocument
	Friend EnchantDB As  Xml.XmlDocument
	Friend trinketDB As  Xml.XmlDocument
	Friend SetBonusDB As  Xml.XmlDocument
	Friend WeapProcDB As  Xml.XmlDocument
	Friend FoodDB As  Xml.XmlDocument
	Friend FlaskDB As  Xml.XmlDocument
	Friend ConsumableDB as Xml.XmlDocument
	
	
	dim space as Integer = 10
	Dim HeadSlot As New EquipSlot
	Dim NeckSlot As New EquipSlot
	Dim ShoulderSlot As New EquipSlot
	Dim BackSlot As New EquipSlot
	Dim ChestSlot As New EquipSlot
	Dim WristSlot As New EquipSlot
	Dim TwoHWeapSlot As New EquipSlot
	Dim MHWeapSlot As New EquipSlot
	Dim OHWeapSlot As New EquipSlot
	Dim SigilSlot As New EquipSlot
	Dim HandSlot As New EquipSlot
	Dim BeltSlot As New EquipSlot
	Dim LegSlot As New EquipSlot
	Dim FeetSlot As New EquipSlot
	Dim ring1Slot As New EquipSlot
	Dim ring2Slot As New EquipSlot
	Dim Trinket1Slot As New EquipSlot
	Dim Trinket2Slot As New EquipSlot
	Dim Food As Food
	dim Flask as Flask
	
	
	Friend FilePath As String
	dim LastDPSResult as Integer
	
	Friend ParentFrame as MainForm
	Friend EPvalues as EPValues
	
	Public Sub New(PFrame as MainForm)
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		ParentFrame = PFrame
		EPvalues = PFrame.EPVal
		InitDisplay
	End Sub
	
	Sub CmdExtratorClick(sender As Object, e As EventArgs)
		'exit sub
		Dim MyExtractor As new Extractor
		MyExtractor.Start
	End Sub
	

	
	Sub GetStats()
		if InLoad then exit sub
		Dim iSlot As EquipSlot
		Dim Strength As Integer
		Dim Intel as Integer
		Dim Agility as Integer
		Dim BonusArmor as Integer
		Dim Armor As Integer
		Dim HasteRating As Integer
		Dim ExpertiseRating As Integer
		Dim HitRating as Integer
		Dim AttackPower As Integer
		Dim CritRating As Integer
		Dim ArmorPenetrationRating As Integer
		Dim Speed1 As string = "0"
		Dim Speed2 As String = "0"
		Dim DPS1 As string = "0"
		Dim DPS2 As String = "0"
		
		
		chkMeta.Checked = false
		chkTailorEnchant.Checked = False
		chkIngenieer.Checked = False
		chkAccelerators.Checked = False
		chkAshenBand.Checked = False
		chkBloodFury.Checked = False
		chkBerzerking.Checked = False
		chkArcaneTorrent.Checked = False
		
		cmbSetBonus1.text = ""
		cmbSetBonus2.text = ""
		
		Dim cSetBonus as new Collections.ArrayList
		
		
		
		Select Case cmbRace.SelectedItem.ToString
			Case "Blood Elf"
				Strength = 172
				Agility = 114
				Intel = 39
			Case "Draenei"
				Strength = 176
				Agility = 109
				Intel = 36
			Case "Dwarf"
				Strength = 177
				Agility = 108
				Intel = 34
			Case "Gnome"
				Strength = 170
				Agility = 115
				Intel = 42
			Case "Human"
				Strength = 175
				Agility = 112
				Intel = 35
			Case "Night Elf"
				Strength = 172
				Agility = 117
				Intel = 35
			Case "Orc"
				Strength = 178
				Agility = 109
				Intel = 32
			Case "Tauren"
				Strength = 180
				Agility = 107
				Intel = 30
			Case "Troll"
				Strength = 176
				Agility = 114
				Intel = 31
			Case "Undead"
				Strength = 174
				Agility = 110
				Intel = 33
			Case "Goblin"
				Strength = 174
				Agility = 110
				Intel = 33
			Case "Worgen"
				Strength = 174
				Agility = 110
				Intel = 33
		End Select
		'Food
		Strength += food.Strength
		Agility += food.Agility
		HasteRating += food.HasteRating
		ExpertiseRating += food.ExpertiseRating
		HitRating += food.HitRating
		AttackPower += food.AttackPower
		CritRating += food.CritRating
		ArmorPenetrationRating +=food.ArmorPenetrationRating
		
		'Flask
		Strength += flask.Strength
		Agility += flask.Agility
		HasteRating += flask.HasteRating
		ExpertiseRating += flask.ExpertiseRating
		HitRating += flask.HitRating
		AttackPower += flask.AttackPower
		CritRating += flask.CritRating
		ArmorPenetrationRating +=flask.ArmorPenetrationRating
		Armor += flask.armor
		
		
		txtMHExpBonus.Text = 0
		txtOHExpBonus.Text = 0
		
		For Each iSlot In Me.EquipmentList
			
			if iSlot.Item.Id = 0 then GoTo NextItem
			If iSlot.SlotId = 17 And rDW.Checked Then GoTo NextItem
			If iSlot.SlotId = 13 And r2Hand.Checked Then GoTo NextItem
			
			Dim subc As Integer = ItemDB.SelectSingleNode("/items/item[id=" & iSlot.Item.Id & "]/subclass").InnerText
			
			If iSlot.Text = "TwoHand" or iSlot.Text = "MainHand" Then
				DPS1 = iSlot.Item.DPS
				Speed1 = iSlot.Item.Speed
				Try
					cmbWeaponProc1.Text = "MH" & WeapProcDB.SelectSingleNode("/WeaponProcList/proc[@id=" & iSlot.Item.Id & "]").Attributes.GetNamedItem("name").InnerText
				Catch
					cmbWeaponProc1.Text = ""
				End Try
				Select Case cmbRace.SelectedItem.ToString
					Case "Dwarf"
						If subc = 4 Or subc = 5 Then
							txtMHExpBonus.Text = 5
						End If
					Case "Human"
						If subc = 4 Or subc = 5 Or subc = 7 Or subc = 8 Then
							txtMHExpBonus.Text = 3
						End If
					Case "Orc"
						If subc = 0 Or subc = 1 Then
							txtMHExpBonus.Text = 5
						End If
				End Select
				
				
				
			End If
			
			If iSlot.Text = "OffHand" Then
				DPS2 = iSlot.Item.DPS
				Speed2 = iSlot.Item.Speed
				
				Try
					cmbWeaponProc2.Text = "MH" & WeapProcDB.SelectSingleNode("/WeaponProcList/proc[@id=" & iSlot.Item.Id & "]").Attributes.GetNamedItem("name").InnerText
				Catch
					cmbWeaponProc1.Text = ""
				End Try
				
				Select Case cmbRace.SelectedItem.ToString
					Case "Dwarf"
						If subc = 4 Or subc = 5 Then
							txtOHExpBonus.Text = 5
						End If
					Case "Human"
						If subc = 4 Or subc = 5 Or subc = 7 Or subc = 8 Then
							txtOHExpBonus.Text = 3
						End If
					Case "Orc"
						If subc = 0 Or subc = 1 Then
							txtOHExpBonus.Text = 5
						End If
				End Select
			End If
			If iSlot.Item.setid <> 0 Then
				cSetBonus.Add(iSlot.Item.setid)
			End If
			
			If iSlot.text = "Trinket1" Then
				Try
					cmbTrinket1.Text = trinketDB.SelectSingleNode("/TrinketList/trinket[@id=" & iSlot.Item.Id & "]").Attributes.GetNamedItem("name").InnerText
				Catch
					cmbTrinket1.Text = ""
				End Try
			End If
			If iSlot.text = "Trinket2" Then
				Try
					cmbTrinket2.Text = trinketDB.SelectSingleNode("/TrinketList/trinket[@id=" & iSlot.Item.Id & "]").Attributes.GetNamedItem("name").InnerText
				Catch
					cmbTrinket2.Text = ""
				End Try
			End If
			
			
			
			If iSlot.Item.Id <> 0 Then
				Strength += iSlot.Item.Strength
				Agility += iSlot.Item.Agility
				BonusArmor += iSlot.Item.BonusArmor
				Armor += iSlot.Item.Armor
				HasteRating += iSlot.Item.HasteRating
				ExpertiseRating += iSlot.Item.ExpertiseRating
				HitRating += iSlot.Item.HitRating
				AttackPower += iSlot.Item.AttackPower
				CritRating += iSlot.Item.CritRating
				ArmorPenetrationRating +=iSlot.Item.ArmorPenetrationRating
				
				
				If iSlot.Item.gem1.Id <> 0 Then
					Strength += iSlot.Item.gem1.Strength
					Agility += iSlot.Item.gem1.Agility
					HasteRating += iSlot.Item.gem1.HasteRating
					ExpertiseRating += iSlot.Item.gem1.ExpertiseRating
					HitRating += iSlot.Item.gem1.HitRating
					AttackPower += iSlot.Item.gem1.AttackPower
					CritRating += iSlot.Item.gem1.CritRating
					ArmorPenetrationRating +=iSlot.Item.gem1.ArmorPenetrationRating
				End If
				If iSlot.Item.gem2.Id <> 0 Then
					Strength += iSlot.Item.gem2.Strength
					Agility += iSlot.Item.gem2.Agility
					HasteRating += iSlot.Item.gem2.HasteRating
					ExpertiseRating += iSlot.Item.gem2.ExpertiseRating
					HitRating += iSlot.Item.gem2.HitRating
					AttackPower += iSlot.Item.gem2.AttackPower
					CritRating += iSlot.Item.gem2.CritRating
					ArmorPenetrationRating +=iSlot.Item.gem2.ArmorPenetrationRating
				End If
				If iSlot.Item.gem3.Id <> 0 Then
					Strength += iSlot.Item.gem3.Strength
					Agility += iSlot.Item.gem3.Agility
					HasteRating += iSlot.Item.gem3.HasteRating
					ExpertiseRating += iSlot.Item.gem3.ExpertiseRating
					HitRating += iSlot.Item.gem3.HitRating
					AttackPower += iSlot.Item.gem3.AttackPower
					CritRating += iSlot.Item.gem3.CritRating
					ArmorPenetrationRating +=iSlot.Item.gem3.ArmorPenetrationRating
				End If
				
				If iSlot.Item.IsGembonusActif And iSlot.Item.gembonus <> 0 Then
					Try
						Strength += 		GemBonusDB.SelectSingleNode("/bonus/item[id=" & iSlot.Item.gembonus & "]/Strength").InnerText
						Agility += 			GemBonusDB.SelectSingleNode("/bonus/item[id=" & iSlot.Item.gembonus & "]/Agility").InnerText
						HasteRating += 		GemBonusDB.SelectSingleNode("/bonus/item[id=" & iSlot.Item.gembonus & "]/HasteRating").InnerText
						ExpertiseRating += 	GemBonusDB.SelectSingleNode("/bonus/item[id=" & iSlot.Item.gembonus & "]/ExpertiseRating").InnerText
						HitRating += 		GemBonusDB.SelectSingleNode("/bonus/item[id=" & iSlot.Item.gembonus & "]/HitRating").InnerText
						AttackPower += 		GemBonusDB.SelectSingleNode("/bonus/item[id=" & iSlot.Item.gembonus & "]/AttackPower").InnerText
						CritRating += 		GemBonusDB.SelectSingleNode("/bonus/item[id=" & iSlot.Item.gembonus & "]/CritRating").InnerText
						ArmorPenetrationRating += GemBonusDB.SelectSingleNode("/bonus/item[id=" & iSlot.Item.gembonus & "]/ArmorPenetrationRating").InnerText
					catch
					End Try
				End If
				
				If iSlot.Item.Enchant.Id <> 0 Then
					Strength += iSlot.Item.Enchant.Strength
					Agility += iSlot.Item.Enchant.Agility
					HasteRating += iSlot.Item.Enchant.HasteRating
					ExpertiseRating += iSlot.Item.Enchant.ExpertiseRating
					HitRating += iSlot.Item.Enchant.HitRating
					AttackPower += iSlot.Item.Enchant.AttackPower
					CritRating += iSlot.Item.Enchant.CritRating
					ArmorPenetrationRating +=iSlot.Item.Enchant.ArmorPenetrationRating
				End If
			End If
			
			' Meta Gem
			If iSlot.Item.Gem1.Id = 41398 or iSlot.Item.Gem1.Id = 41285 Then
				chkMeta.Checked = true
			End If
			' Tailor enchant
			If iSlot.Item.Enchant.Id = 7 Then
				chkTailorEnchant.Checked = true
			End If
			' PyroRocket
			If iSlot.Item.Enchant.Id = 3603 Then
				chkIngenieer.Checked = true
			End If
			' Hyperspeed accelerator
			If iSlot.Item.Enchant.Id = 3604 Then
				chkAccelerators.Checked = true
			End If
			
			' Ashen band
			If iSlot.Item.Id = 50401 Or iSlot.Item.Id=50402 Or iSlot.Item.Id=52572  Or iSlot.Item.Id= 52571 Then
				chkAshenBand.Checked = true
			End If
			NextItem:
		Next
		' Bloodfury
		If cmbRace.SelectedItem = "Orc" Then
			chkBloodFury.Checked = true
		End If
		' Berzerking
		If cmbRace.SelectedItem = "Troll" Then
			chkBerzerking.Checked = true
		End If
		' Arcane torrent
		If cmbRace.SelectedItem = "Blood Elf" Then
			chkArcaneTorrent.Checked = true
		End If
		' Set bonus1
		If cSetBonus.Count > 0 Then
			cSetBonus.Sort
			TransformToSet(cSetBonus)
			Dim i As Integer
			Dim sId As String
			
			Do Until cSetBonus.Count = 0
				
				sId = cSetBonus.Item(0)
				i = CollectionDuplicateCount (cSetBonus,cSetBonus.Item(0))
				If i >= 4 Then
					cmbSetBonus1.text = cSetBonus.Item(0)
					cmbSetBonus1.text = cmbSetBonus1.text.Replace("DPS","4PDPS")
					cmbSetBonus1.text = cmbSetBonus1.text.Replace("TNK","4PTNK")
				End If
				If i >= 2 Then
					If cmbSetBonus1.text <> "" Then
						cmbSetBonus2.text = cSetBonus.Item(0)
						cmbSetBonus2.text = cmbSetBonus2.text.Replace("DPS","2PDPS")
						cmbSetBonus2.text = cmbSetBonus2.text.Replace("TNK","2PTNK")
					Else
						cmbSetBonus1.text = cSetBonus.Item(0)
						cmbSetBonus1.text = cmbSetBonus1.text.Replace("DPS","2PDPS")
						cmbSetBonus1.text = cmbSetBonus1.text.Replace("TNK","2PTNK")
					End If
				End If
				Do Until cSetBonus.Contains(sId) = False
					cSetBonus.Remove(sId)
				Loop
			Loop
		End If
		
		Armor += Agility*2
		txtStr.Text = Strength
		txtAgi.Text = Agility
		'		BonusArmor
		txtArmor.Text = Armor
		txtHaste.Text = HasteRating
		lblHaste.Text = "Haste Rating (" & toDDecimal(HasteRating/25.22) & "%)"
		
		txtExp.Text = ExpertiseRating
		lblExp.Text = "Expertise Rating(" &  (toDDecimal(ExpertiseRating/32.79))*4 & ")"
		txtHit.Text = HitRating
		lblHit.Text = "Hit Rating(" &  toDDecimal(HitRating/32.79) & "%)"
		txtAP.Text = AttackPower
		txtArP.Text = ArmorPenetrationRating
		lblArP.Text = "Armor Penetration Rating(" & toDDecimal(ArmorPenetrationRating/13.99) & ")"
		
		txtCrit.Text = CritRating
		lblCrit.Text = "CritRating(" &  toDDecimal(CritRating/45.91) & "%)"
		txtIntel.Text = Intel
		txtMHDPS.Text = DPS1
		txtMHWSpeed.Text = Speed1
		txtOHDPS.Text = DPS2
		txtOHWSpeed.Text = Speed2
	End Sub
	Function TransformToSet(col As Collections.ArrayList) As Collections.ArrayList
		Dim s As String
		dim i as Integer
		
		For i=0 To col.Count-1
			s = SetBonusDB.SelectSingleNode("/SetBonus/set[id="& col.Item(i) & "]").Attributes.GetNamedItem("name").InnerText
			s += SetBonusDB.SelectSingleNode("/SetBonus/set[id="& col.Item(i) & "]").Attributes.GetNamedItem("type").InnerText
			col.Item(i) = s
		Next
		return col
	End Function
	
	Function CollectionDuplicateCount(col As Collections.ArrayList,Value as String) as Integer
		Dim i As Integer = 0
		dim v as String
		For Each v In col
			if v= Value then i +=1
		Next
		return i
	End Function
	
	
	
	
	
	Sub CmdSaveClick(sender As Object, e As EventArgs)
		SaveMycharacter
		me.Close
	End Sub
	
	Sub SaveMycharacter
		Dim xmlChar As New Xml.XmlDocument
		xmlChar.LoadXml("<character/>")
		
		Dim root As xml.XmlElement = xmlChar.DocumentElement
		Dim newElem As xml.XmlNode
		Dim gemNode as Xml.XmlNode
		Dim iSlot As EquipSlot
		
		newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "race", "")
		Try
			newElem.InnerText = cmbRace.SelectedItem
			root.AppendChild(newElem)
		Catch
		End Try
		
		newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "skill1", "")
		Try
			newElem.InnerText = cmbskill1.SelectedItem
			root.AppendChild(newElem)
		Catch
		End Try
		
		newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "skill2", "")
		Try
			newElem.InnerText = cmbskill2.SelectedItem
			root.AppendChild(newElem)
		Catch
		End Try
		
		newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "food", "")
		Try
			newElem.InnerText = cmbFood.SelectedItem
			root.AppendChild(newElem)
		Catch
		End Try
		
		newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "flask", "")
		Try
			newElem.InnerText = cmbFlask.SelectedItem
			root.AppendChild(newElem)
		Catch
		End Try
		
		
		
		
		newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "DW", "")
		Try
			newElem.InnerText = rDW.Checked
			root.AppendChild(newElem)
		Catch
		End Try
		
		newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "TwoHand", "")
		Try
			newElem.InnerText = r2Hand.Checked
			root.AppendChild(newElem)
		Catch
		End Try
		
		For Each iSlot In me.EquipmentList
			try
				newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, iSlot.Text, "")
				Try
					gemNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "id", "")
					gemNode.InnerText = iSlot.Item.Id
					newElem.AppendChild(gemNode)
				Catch
				End Try
				Try
					gemNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "gem1", "")
					gemNode.InnerText = iSlot.Item.Gem1.Id
					newElem.AppendChild(gemNode)
				catch
				End Try
				Try
					gemNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "gem2", "")
					gemNode.InnerText = iSlot.Item.Gem2.Id
					newElem.AppendChild(gemNode)
				catch
				End Try
				Try
					gemNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "gem3", "")
					gemNode.InnerText = iSlot.Item.Gem3.Id
					newElem.AppendChild(gemNode)
				catch
				End Try
				Try
					gemNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "enchant", "")
					gemNode.InnerText = iSlot.item.Enchant.Id
					newElem.AppendChild(gemNode)
				Catch
				End Try
				root.AppendChild(newElem)
			Catch
			End Try
		Next
		
		Dim xElem As XmlElement
		
		Dim xStat As XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "stat", "")
		root.AppendChild(xStat)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "Strength", "")
		xElem.InnerText = CheckForInt(txtStr.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "Agility", "")
		xElem.InnerText = CheckForInt(txtAgi.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "Intel", "")
		xElem.InnerText = CheckForInt(txtIntel.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "Armor", "")
		xElem.InnerText = CheckForInt(txtArmor.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "AttackPower", "")
		xElem.InnerText = CheckForInt(txtAP.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "HitRating", "")
		xElem.InnerText = CheckForInt(txthit.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "CritRating", "")
		xElem.InnerText = CheckForInt(txtcrit.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "HasteRating", "")
		xElem.InnerText = CheckForInt(txtHaste.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "ArmorPenetrationRating", "")
		xElem.InnerText = CheckForInt(txtArP.Text)
		xStat.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "ExpertiseRating", "")
		xElem.InnerText = CheckForInt(txtExp.Text)
		xStat.AppendChild(xElem)
		
		
		Dim xweapon As XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "weapon", "")
		root.AppendChild(xweapon)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "count", "")
		If r2Hand.Checked=True Then
			xElem.InnerText = 1
		Else
			xElem.InnerText = 2
		End If
		xweapon.AppendChild(xElem)
		
		
		
		Dim xweapon1 As XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "mainhand", "")
		Dim xweapon2 As XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "offhand", "")
		xweapon.AppendChild(xweapon1)
		xweapon.AppendChild(xweapon2)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "dps", "")
		xElem.InnerText = CheckFordouble(txtMHDPS.Text)
		xweapon1.AppendChild(xElem)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "speed", "")
		xElem.InnerText = CheckFordouble(txtMHWSpeed.Text)
		xweapon1.AppendChild(xElem)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "dps", "")
		xElem.InnerText = CheckFordouble(txtOHDPS.Text)
		xweapon2.AppendChild(xElem)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "speed", "")
		xElem.InnerText = CheckFordouble(txtOHWSpeed.Text)
		xweapon2.AppendChild(xElem)
		
		
		Dim xSet As XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "Set", "")
		root.AppendChild(xSet)
		
		
		Try
			xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, cmbSetBonus1.Text, "")
			xElem.InnerText = 1
			xSet.AppendChild(xElem)
		Catch
			
		End Try
		
		Try
			xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, cmbSetBonus2.Text, "")
			xElem.InnerText = 1
			xSet.AppendChild(xElem)
		Catch
			
		End Try
		
		
		
		
		
		
		
		
		Dim xtrinket As XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "trinket", "")
		root.AppendChild(xtrinket)
		Try
			xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, cmbTrinket1.Text, "")
			xElem.InnerText = 1
			xtrinket.AppendChild(xElem)
		Catch
		End Try
		
		Try
			xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, cmbTrinket2.Text, "")
			xElem.InnerText = 1
			xtrinket.AppendChild(xElem)
		Catch
		End Try
		
		Dim xWeaponProc As xml.XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "WeaponProc", "")
		root.AppendChild(xWeaponProc)
		If cmbWeaponProc1.Text <> "" Then
			xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, cmbWeaponProc1.Text, "")
			xElem.InnerText = 1
			xWeaponProc.AppendChild(xElem)
		End If
		
		If cmbWeaponProc2.Text <> "" Then
			xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, cmbWeaponProc2.Text, "")
			xElem.InnerText = 1
			xWeaponProc.AppendChild(xElem)
		end if
		
		
		Dim xMisc As XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "misc", "")
		root.AppendChild(xMisc)
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "HandMountedPyroRocket", "")
		xElem.InnerText = chkIngenieer.Checked
		xMisc.AppendChild(xElem)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "HyperspeedAccelerators", "")
		xElem.InnerText = chkAccelerators.Checked
		xMisc.AppendChild(xElem)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, "ChaoticSkyflareDiamond", "")
		xElem.InnerText = chkMeta.Checked
		xMisc.AppendChild(xElem)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element,"TailorEnchant" , "")
		xElem.InnerText = chkTailorEnchant.Checked
		xMisc.AppendChild(xElem)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element,"AshenBand" , "")
		xElem.InnerText = chkAshenBand.Checked
		xMisc.AppendChild(xElem)
		Dim itm As System.Windows.Forms.ToolStripMenuItem
		
		For Each itm  In ddConsumable.DropDownItems
			xElem = xmlChar.CreateNode(xml.XmlNodeType.Element, itm.Name , "")
			xElem.InnerText = itm.Checked
			xMisc.AppendChild(xElem)
		Next
		
		Dim xRacial As XmlNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "racials", "")
		root.AppendChild(xRacial)
		
		'Racials
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element,"MHExpertiseBonus" , "")
		xElem.InnerText = CheckForint(txtMHExpBonus.Text)
		xRacial.AppendChild(xElem)
		
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element,"OHExpertiseBonus" , "")
		xElem.InnerText = CheckForint(txtOHExpBonus.Text)
		xRacial.AppendChild(xElem)
		
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element,"Orc" , "")
		xElem.InnerText = chkBloodFury.Checked
		xRacial.AppendChild(xElem)
		
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element,"Troll" , "")
		xElem.InnerText = chkBerzerking.Checked
		xRacial.AppendChild(xElem)
		
		xElem = xmlChar.CreateNode(xml.XmlNodeType.Element,"BloodElf" , "")
		xElem.InnerText = chkArcaneTorrent.Checked
		xRacial.AppendChild(xElem)
		
		
		
		
		
		xmlChar.Save(Application.StartupPath & "\CharactersWithGear\" & FilePath)
	End Sub
	
	
	Sub ImportMyCharacter
		dim realmName as string = "Chants+eternels"
		dim characterName as String = "Kahorie"
		Dim webClient As WebClient = New WebClient()
		
		webClient.QueryString.Add("r", realmName)
		webClient.QueryString.Add("n", characterName)
		webClient.Headers.Add("user-agent", "MSIE 7.0")
		webClient.Proxy = WebRequest.GetSystemWebProxy
		webClient.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials
		dim xmlReaderSettings as XmlReaderSettings = new XmlReaderSettings()
		xmlReaderSettings.IgnoreComments = true
		xmlReaderSettings.IgnoreWhitespace = True
		Dim iSlot As EquipSlot
		Dim  xmlReader As XmlReader = XmlReader.Create(webClient.OpenRead("http://eu.wowarmory.com/character-sheet.xml"), xmlReaderSettings)
		Do While xmlReader.Read()
			If (xmlReader.NodeType = XmlNodeType.Element And xmlReader.Name = "item") Then
				debug.Print	("Name= " & xmlReader.GetAttribute("name") & "slot=" & xmlReader.GetAttribute(" slot") )
				
				For Each iSlot In Me.EquipmentList
					If iSlot.Text = ArmorySlot2MySlot(xmlReader.GetAttribute("slot")) Then
						Try
							iSlot.Item.LoadItem(xmlReader.GetAttribute("id"))
							iSlot.DisplayItem
							iSlot.Item.gem1.Attach(xmlReader.GetAttribute("gem0Id"))
							iSlot.Item.gem2.Attach(xmlReader.GetAttribute("gem1Id"))
							iSlot.Item.gem3.Attach(xmlReader.GetAttribute("gem2Id"))
							iSlot.DisplayGem
							iSlot.Item.Enchant.Attach(xmlReader.GetAttribute("permanentEnchantItemId"))
							iSlot.DisplayEnchant()
						Catch ex As System.Exception
							debug.Print (ex.ToString)
						End Try
						
					End If
				next
			End If
		Loop
	End Sub
	Function ArmorySlot2MySlot(armorySlotId As Integer) As string
		Select Case armorySlotId
			Case 0
				return "Head"
			Case 1
				return "Neck"
			Case 2
				Return "Shoulder"
			Case 3
				Return "Back"
			Case 4
				Return "Chest"
			Case 5
				Return "Waist"
			Case 6
				Return "Legs"
			Case 7
				Return "Feets"
			Case 8
				Return "Wrist"
			Case 9
				Return "Hand"
			Case 10
				Return "Finger1"
			Case 11
				Return "Finger2"
			Case 12
				Return "Trinket1"
			Case 13
				Return "Trinket2"
			Case 14
				Return "Back"
			Case 15
				Return "TwoHand"
			Case 16
				Return "OffHand"
			Case 17
				Return "Sigil"
			
			Case Else
				Return ""
				End Select
				
	End Function
	
	Sub LoadMycharacter
		InLoad = true
		Dim xmlChar As New Xml.XmlDocument
		xmlChar.Load(Application.StartupPath & "\CharactersWithGear\" & FilePath)
		'Dim root As xml.XmlElement = xmlChar.DocumentElement
		Dim iSlot As EquipSlot
		Try
			cmbRace.SelectedItem = xmlChar.SelectSingleNode("/character/race").InnerText
		Catch
		End Try
		
		
		Try
			cmbfood.SelectedItem = xmlChar.SelectSingleNode("/character/food").InnerText
		Catch
		End Try
		
		Try
			cmbflask.SelectedItem = xmlChar.SelectSingleNode("/character/flask").InnerText
		Catch
		End Try
		
		
		Try
			cmbskill1.SelectedItem = xmlChar.SelectSingleNode("/character/skill1").InnerText
		Catch
		End Try
		
		Try
			cmbskill2.SelectedItem = xmlChar.SelectSingleNode("/character/skill2").InnerText
		Catch
		End Try
		
		
		
		Try
			rDW.Checked = xmlChar.SelectSingleNode("/character/DW").InnerText
		Catch
		End Try
		
		
		Try
			r2Hand.Checked = xmlChar.SelectSingleNode("/character/TwoHand").InnerText
			
		Catch
		End Try
		
		
		Dim itm As System.Windows.Forms.ToolStripMenuItem
		
		For Each itm  In ddConsumable.DropDownItems
			Try
				itm.Checked = xmlChar.SelectSingleNode("/character/misc/" & itm.Name ).InnerText
			Catch
				itm.Checked = false
			End Try
		Next
		
		
		
		
		
		For Each iSlot In me.EquipmentList
			Try
				iSlot.Item.LoadItem(xmlChar.SelectSingleNode("/character/" & iSlot.Text & "/id").InnerText)
				iSlot.DisplayItem
				Try
					iSlot.Item.gem1.Attach(xmlChar.SelectSingleNode("/character/" & iSlot.Text & "/gem1").InnerText)
				Catch
				End Try
				Try
					iSlot.Item.gem2.Attach(xmlChar.SelectSingleNode("/character/" & iSlot.Text & "/gem2").InnerText)
				Catch
				End Try
				Try
					iSlot.Item.gem3.Attach(xmlChar.SelectSingleNode("/character/" & iSlot.Text & "/gem3").InnerText)
				Catch
				End Try
				iSlot.DisplayGem
				
				Try
					iSlot.Item.Enchant.Attach(xmlChar.SelectSingleNode("/character/" & iSlot.Text & "/enchant").InnerText)
					iSlot.DisplayEnchant()
				Catch
				End Try
				
				
				
			catch
			end try
		Next
		InLoad = false
		GetStats
	End Sub
	
	Sub Redraw()
		With HeadSlot
			.Location = New System.Drawing.Point(space*2,toolStrip1.Top + toolStrip1.Height+space)
		End With
		With NeckSlot
			.Location = New System.Drawing.Point(HeadSlot.Left,HeadSlot.Top+HeadSlot.Height+space)
		End With
		With ShoulderSlot
			.Location = New System.Drawing.Point(NeckSlot.Left,NeckSlot.Top+NeckSlot.Height+space)
		End With
		With BackSlot
			.Location = New System.Drawing.Point(ShoulderSlot.Left,ShoulderSlot.Top+ShoulderSlot.Height+space)
		End With
		With ChestSlot
			.Location = New System.Drawing.Point(BackSlot.Left,BackSlot.Top+BackSlot.Height+space)
		End With
		With WristSlot
			.Location = New System.Drawing.Point(ChestSlot.Left,ChestSlot.Top+ChestSlot.Height+space)
		End With
		With TwoHWeapSlot
			.Location = New System.Drawing.Point(WristSlot.Left,WristSlot.Top+WristSlot.Height+space)
		End With
		With MHWeapSlot
			.Location = New System.Drawing.Point(WristSlot.Left,WristSlot.Top+WristSlot.Height+space)
		End With
		With OHWeapSlot
			.Location = New System.Drawing.Point(MHWeapSlot.Left,MHWeapSlot.Top+MHWeapSlot.Height+space)
		End With
		With SigilSlot
			.Location = New System.Drawing.Point(OHWeapSlot.left,OHWeapSlot.Top+OHWeapSlot.Height+space)
		End With
		With HandSlot
			.Location = New System.Drawing.Point(HeadSlot.left+HeadSlot.Width+space,HeadSlot.Top)
		End With
		With BeltSlot
			.Location = New System.Drawing.Point(HandSlot.left,HandSlot.Top+HandSlot.Height+space)
		End With
		With LegSlot
			.Location = New System.Drawing.Point(BeltSlot.left,BeltSlot.Top+BeltSlot.Height+space)
		End With
		With FeetSlot
			.Location = New System.Drawing.Point(LegSlot.left,LegSlot.Top+LegSlot.Height+space)
		End With
		With ring1Slot
			.Location = New System.Drawing.Point(FeetSlot.left,FeetSlot.Top+FeetSlot.Height+space)
		End With
		With ring2Slot
			.Location = New System.Drawing.Point(ring1Slot.left,ring1Slot.Top+ring1Slot.Height+space)
		End With
		With Trinket1Slot
			.Location = New System.Drawing.Point(ring2Slot.left,ring2Slot.Top+ring2Slot.Height+space)
		End With
		With Trinket2Slot
			.Location = New System.Drawing.Point(Trinket1Slot.left,Trinket1Slot.Top+Trinket1Slot.Height+space)
		End With
		groupBox1.Left = Trinket1Slot.left + Trinket1Slot.Width + space
		groupBox1.Top = HeadSlot.Top
		Me.Size = New Size(groupBox1.Left + groupBox1.Width + space , SigilSlot.Top + SigilSlot.Height + space)
		InLoad = false
	End Sub
	
	Sub InitDisplay()
		InLoad = true
		ItemDB = ParentFrame.ItemDB
		GemDB = ParentFrame.GemDB
		GemBonusDB = ParentFrame.GemBonusDB
		EnchantDB = ParentFrame.EnchantDB
		trinketDB = ParentFrame.trinketDB
		SetBonusDB = ParentFrame.SetBonusDB
		WeapProcDB = ParentFrame.WeapProcDB
		FlaskDB = ParentFrame.FlaskDB
		FoodDB = ParentFrame.FoodDB
		ConsumableDB = ParentFrame.ConsumableDB
		Flask = New Flask(Me)
		Food = new Food(me)
		
		
		
		cmdExtrator.Hide
		Dim x As Xml.XmlNode
		cmbFlask.Items.Add("")
		For Each x in FlaskDB.SelectNodes("/flask/item/name")
			cmbFlask.Items.Add(x.InnerText)
		Next
		cmbFood.Items.Add("")
		For Each x in FoodDB.SelectNodes("/food/item/name")
			cmbFood.Items.Add(x.InnerText)
		Next
		dim itm as System.Windows.Forms.ToolStripMenuItem
		For Each x In ConsumableDB.SelectNodes("/Consumables/item")
			itm = New ToolStripMenuItem(x.InnerText)
			itm.Name = x.InnerText.Replace(" ","")
			ddConsumable.DropDownItems.Add(itm)
			itm.CheckOnClick = true
		Next
		
		cmbRace.Items.Add ("Blood Elf")
		cmbRace.Items.Add ("Draenei")
		cmbRace.Items.Add ("Dwarf")
		cmbRace.Items.Add ("Gnome")
		cmbRace.Items.Add ("Human")
		cmbRace.Items.Add ("Night Elf")
		cmbRace.Items.Add ("Orc")
		cmbRace.Items.Add ("Tauren")
		cmbRace.Items.Add ("Troll")
		cmbRace.Items.Add ("Undead")
		cmbRace.Items.Add ("Goblin")
		cmbRace.Items.Add ("Worgen")
		cmbRace.SelectedIndex = 0
		
		
		cmbSkill1.Items.Add("Alchemy")
		cmbSkill1.Items.Add("Blacksmithing")
		cmbSkill1.Items.Add("Enchanting")
		cmbSkill1.Items.Add("Engineering")
		cmbSkill1.Items.Add("Inscription")
		cmbSkill1.Items.Add("Jewelcrafting")
		cmbSkill1.Items.Add("Leatherworking")
		cmbSkill1.Items.Add("Herb Gathering")
		cmbSkill1.Items.Add("Mining")
		cmbSkill1.Items.Add("Skinning")
		cmbSkill1.Items.Add("Tailoring")
		cmbSkill1.SelectedIndex = 0
		
		cmbSkill2.Items.Add("Alchemy")
		cmbSkill2.Items.Add("Blacksmithing")
		cmbSkill2.Items.Add("Enchanting")
		cmbSkill2.Items.Add("Engineering")
		cmbSkill2.Items.Add("Inscription")
		cmbSkill2.Items.Add("Jewelcrafting")
		cmbSkill2.Items.Add("Leatherworking")
		cmbSkill2.Items.Add("Herb Gathering")
		cmbSkill2.Items.Add("Mining")
		cmbSkill2.Items.Add("Skinning")
		cmbSkill2.Items.Add("Tailoring")
		cmbSkill2.SelectedIndex = 0
		
		
		With HeadSlot
			.Text = "Head"
			.init(Me,1)
			.Location = New System.Drawing.Point(space*2,toolStrip1.Top + toolStrip1.Height+space)
			
		End With
		
		With NeckSlot
			.Text = "Neck"
			.init(Me,2)
			.Location = New System.Drawing.Point(HeadSlot.Left,HeadSlot.Top+HeadSlot.Height+space)
			
		End With
		
		With ShoulderSlot
			.Text = "Shoulder"
			.init(Me,3)
			.Location = New System.Drawing.Point(NeckSlot.Left,NeckSlot.Top+NeckSlot.Height+space)
		End With
		
		
		With BackSlot
			.Text = "Back"
			.init(Me,16)
			.Location = New System.Drawing.Point(ShoulderSlot.Left,ShoulderSlot.Top+ShoulderSlot.Height+space)
			
		End With
		
		
		With ChestSlot
			.Text = "Chest"
			.init(Me,5)
			.Location = New System.Drawing.Point(BackSlot.Left,BackSlot.Top+BackSlot.Height+space)
		End With
		
		
		With WristSlot
			.Text = "Wrist"
			.init(Me,9)
			.Location = New System.Drawing.Point(ChestSlot.Left,ChestSlot.Top+ChestSlot.Height+space)
			
		End With
		

		
		With TwoHWeapSlot
			.Text = "TwoHand"
			.init(Me,17)
			.Location = New System.Drawing.Point(WristSlot.Left,WristSlot.Top+WristSlot.Height+space)
		End With
		
		
		With MHWeapSlot
			.Text = "MainHand"
			.init(Me,13)
			.Location = New System.Drawing.Point(WristSlot.Left,WristSlot.Top+WristSlot.Height+space)
			.Visible = false
		End With
		
		
		With OHWeapSlot
			.Text = "OffHand"
			.init(Me,13)
			.Location = New System.Drawing.Point(MHWeapSlot.Left,MHWeapSlot.Top+MHWeapSlot.Height+space)
			.Visible = false
		End With
		
		
		With SigilSlot
			.Text = "Sigil"
			.init(Me,28)
			.Location = New System.Drawing.Point(OHWeapSlot.left,OHWeapSlot.Top+OHWeapSlot.Height+space)
			
		End With
		
		
		With HandSlot
			.Text = "Hand"
			.init(Me,10)
			.Location = New System.Drawing.Point(HeadSlot.left+HeadSlot.Width+space,HeadSlot.Top)
			
		End With
		
		
		With BeltSlot
			.Text = "Waist"
			.init(Me,6)
			.Location = New System.Drawing.Point(HandSlot.left,HandSlot.Top+HandSlot.Height+space)
		End With
		
		
		With LegSlot
			.Text = "Legs"
			.init(Me,7)
			.Location = New System.Drawing.Point(BeltSlot.left,BeltSlot.Top+BeltSlot.Height+space)
			
		End With
		
		With FeetSlot
			.Text = "Feets"
			.init(Me,8)
			.Location = New System.Drawing.Point(LegSlot.left,LegSlot.Top+LegSlot.Height+space)
			
		End With
		
		
		With ring1Slot
			.Text = "Finger1"
			.init(Me,11)
			.Location = New System.Drawing.Point(FeetSlot.left,FeetSlot.Top+FeetSlot.Height+space)
			
		End With
		
	
		With ring2Slot
			.Text = "Finger2"
			.init(Me,11)
			.Location = New System.Drawing.Point(ring1Slot.left,ring1Slot.Top+ring1Slot.Height+space)
			
		End With
		
		
		With Trinket1Slot
			.Text = "Trinket1"
			.init(Me,12)
			.Location = New System.Drawing.Point(ring2Slot.left,ring2Slot.Top+ring2Slot.Height+space)
			
		End With
		
		With Trinket2Slot
			.Text = "Trinket2"
			.init(Me,12)
			.Location = New System.Drawing.Point(Trinket1Slot.left,Trinket1Slot.Top+Trinket1Slot.Height+space)
		End With
		
		groupBox1.Left = Trinket1Slot.left + Trinket1Slot.Width + space
		groupBox1.Top = HeadSlot.Top
		Me.Size = New Size(groupBox1.Left + groupBox1.Width + space , SigilSlot.Top + SigilSlot.Height + space)
		InLoad = false
	End Sub
	
	Sub MainFormLoad(sender As Object, e As EventArgs)
		
'		Dim xtr As New Extractor
'''
'		xtr.Start
'''
'''
'		exit sub
		'Me.Size = New Size(980, 800)
		LoadMycharacter
		'ImportMyCharacter
	End Sub
	
	
	Sub CmbRaceSelectedIndexChanged(sender As Object, e As EventArgs)
		GetStats
	End Sub
	
	Sub RDWCheckedChanged(sender As Object, e As EventArgs)
		If RDW.Checked Then
			r2Hand.Checked = False
			txtOHDPS.Enabled = true
			txtOHWSpeed.Enabled = True
			dim eq as EquipSlot
			For Each eq In EquipmentList
				If eq.text = "TwoHand" Then eq.visible = False
				If eq.text = "MainHand" Then eq.visible = true
				If eq.text = "OffHand" Then eq.visible = true
			Next
			GetStats
		End If
	End Sub
	
	Sub R2HandCheckedChanged(sender As Object, e As EventArgs)
		If r2Hand.Checked Then
			RDW.Checked = False
			txtOHDPS.Enabled = false
			txtOHWSpeed.Enabled = False
			dim eq as EquipSlot
			For Each eq In EquipmentList
				If eq.text = "TwoHand" Then eq.visible = true
				If eq.text = "MainHand" Then eq.visible = false
				If eq.text = "OffHand" Then eq.visible = false
			Next
			GetStats
		End If
	End Sub
	
	
	
	Sub CmdGetDpsClick(sender As Object, e As EventArgs)
		Dim tmp As String
		Dim i As Integer
		tmp = Me.FilePath
		Me.FilePath = "tmp.xml"
		SaveMycharacter
		Me.ParentFrame.cmbGearSelector.Items.Add ("tmp.xml")
		Me.ParentFrame.cmbGearSelector.SelectedItem =  "tmp.xml"
		If Me.ParentFrame.LoadBeforeSim = True Then
			i = SimConstructor.GetFastDPS(Me.ParentFrame)
			lblDPS.Text = i & " dps (" & i-LastDPSResult & ")"
			LastDPSResult = i
		End If
		
		Me.ParentFrame.cmbGearSelector.SelectedItem = tmp
		Me.FilePath = tmp
		Me.ParentFrame.cmbGearSelector.Items.Remove("tmp.xml")
		My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\CharactersWithGear\" & "tmp.xml")
	End Sub
	
	Sub TsGetQuickEPClick(sender As Object, e As EventArgs)
		Dim tmp As String
		tmp = Me.FilePath
		Me.FilePath = "tmp.xml"
		SaveMycharacter
		Me.ParentFrame.cmbGearSelector.Items.Add ("tmp.xml")
		Me.ParentFrame.cmbGearSelector.SelectedItem =  "tmp.xml"
		If Me.ParentFrame.LoadBeforeSim = True Then
			SimConstructor.GetFastEPValue(Me.ParentFrame)
		End If
		Me.ParentFrame.cmbGearSelector.SelectedItem = tmp
		Me.FilePath = tmp
		Me.ParentFrame.cmbGearSelector.Items.Remove("tmp.xml")
		My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\CharactersWithGear\" & "tmp.xml")
		Dim dis As New EPDisplay(me)
		dis.ShowDialog
	End Sub
	

	
	Sub CmdSaveAsNewClick(sender As Object, e As EventArgs)
		Dim truc As New Form1
		Dim res As DialogResult
		res = truc.ShowDialog
		If truc.textBox1.Text  <> "" And res = DialogResult.OK Then
			FilePath = truc.textBox1.Text & ".xml"
			SaveMycharacter
		Else
			exit sub
		End If
		truc.Dispose
		me.Close
	End Sub
	
	
	Sub CmbSkillClick(sender As Object, e As EventArgs)
		Dim eq As EquipSlot
		If InLoad Then Exit Sub
		InLoad = true
		For Each eq In EquipmentList
			eq.Item.LoadItem(eq.Item.Id)
			eq.DisplayItem
		Next
		InLoad = False
		GetStats
	End Sub
	
	Sub CmbFlaskSelectionChange(sender As Object, e As EventArgs)
			Flask.Attach(CmbFlask.SelectedItem.ToString)
			GetStats
	End Sub
	
	Sub cmbFoodSelectionChange(sender As Object, e As EventArgs)
			Food.Attach(CmbFood.SelectedItem.ToString)
			GetStats
	End Sub
	
	
	
	Sub GreedyToolStripMenuItemClick(sender As Object, e As EventArgs)
		debug.Print(e.ToString)
	End Sub
End Class
