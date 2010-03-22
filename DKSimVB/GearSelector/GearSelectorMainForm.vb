'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 10:41 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Imports System.Xml
Public Partial Class GearSelectorMainForm
	
	friend EquipmentList as new Collection
	Friend InLoad As Boolean
	Friend EnchantSelector As New EnchantSelector
	Friend GemSelector As New GemSelector(me)
	Friend GearSelector As new GearSelector(me)
	Friend ItemDB As  Xml.XmlDocument
	Friend GemDB As  Xml.XmlDocument
	Friend GemBonusDB As  Xml.XmlDocument
	Friend EnchantDB As  Xml.XmlDocument
	Friend trinketDB As  Xml.XmlDocument
	Friend SetBonusDB As  Xml.XmlDocument
	Friend WeapProcDB As  Xml.XmlDocument
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
	End Sub
	
	Sub CmdExtratorClick(sender As Object, e As EventArgs)
		'exit sub
		Dim MyExtractor As new Extractor
		MyExtractor.Start
	End Sub
	
	Sub cmdSaveAsNewClick(sender As Object, e As EventArgs)
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
		'LoadMycharacter
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
	
	Sub LoadMycharacter
		InLoad = true
		Dim xmlChar As New Xml.XmlDocument
		xmlChar.Load(Application.StartupPath & "\CharactersWithGear\" & FilePath)
		Dim root As xml.XmlElement = xmlChar.DocumentElement
		Dim iSlot As EquipSlot
		Try
			cmbRace.SelectedItem = xmlChar.SelectSingleNode("/character/race").InnerText
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
	
	
	
	
	
	Sub MainFormLoad(sender As Object, e As EventArgs)
		
		'				Dim xtr As New Extractor
		'				xtr.init
		'				xtr.GetSigils
		'				exit sub
		
		Dim Gear As New GearLoader
		
		Gear.Init
		
		ItemDB = ParentFrame.ItemDB
		GemDB = ParentFrame.GemDB
		GemBonusDB = ParentFrame.GemBonusDB
		EnchantDB = ParentFrame.EnchantDB
		trinketDB = ParentFrame.trinketDB
		SetBonusDB = ParentFrame.SetBonusDB
		WeapProcDB = ParentFrame.WeapProcDB
		cmdExtrator.Hide
		
		
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
		
		Dim HeadSlot As New EquipSlot
		With HeadSlot
			.Text = "Head"
			.init(Me,1)
			.Location = New System.Drawing.Point(20,0)
			
		End With
		Dim NeckSlot As New EquipSlot
		With NeckSlot
			.Text = "Neck"
			.init(Me,2)
			.Location = New System.Drawing.Point(20,80)
			
		End With
		Dim ShoulderSlot As New EquipSlot
		With ShoulderSlot
			.Text = "Shoulder"
			.init(Me,3)
			.Location = New System.Drawing.Point(20,160)
			
		End With
		
		Dim BackSlot As New EquipSlot
		With BackSlot
			.Text = "Back"
			.init(Me,16)
			.Location = New System.Drawing.Point(20,240)
			
		End With
		
		Dim ChestSlot As New EquipSlot
		With ChestSlot
			.Text = "Chest"
			.init(Me,5)
			.Location = New System.Drawing.Point(20,320)
			
		End With
		
		Dim WristSlot As New EquipSlot
		With WristSlot
			.Text = "Wrist"
			.init(Me,9)
			.Location = New System.Drawing.Point(20,400)
			
		End With
		
		
		Dim TwoHWeapSlot As New EquipSlot
		With TwoHWeapSlot
			.Text = "TwoHand"
			.init(Me,17)
			.Location = New System.Drawing.Point(20,480)
		End With
		
		Dim MHWeapSlot As New EquipSlot
		With MHWeapSlot
			.Text = "MainHand"
			.init(Me,13)
			.Location = New System.Drawing.Point(20,480)
			.Visible = false
		End With
		
		Dim OHWeapSlot As New EquipSlot
		With OHWeapSlot
			.Text = "OffHand"
			.init(Me,13)
			.Location = New System.Drawing.Point(20,560)
			.Visible = false
		End With
		
		Dim SigilSlot As New EquipSlot
		With SigilSlot
			.Text = "Sigil"
			.init(Me,28)
			.Location = New System.Drawing.Point(20,640)
			
		End With
		
		Dim HandSlot As New EquipSlot
		With HandSlot
			.Text = "Hand"
			.init(Me,10)
			.Location = New System.Drawing.Point(330,000)
			
		End With
		
		Dim BeltSlot As New EquipSlot
		With BeltSlot
			.Text = "Waist"
			.init(Me,6)
			.Location = New System.Drawing.Point(330,80)
			
		End With
		
		Dim LegSlot As New EquipSlot
		With LegSlot
			.Text = "Legs"
			.init(Me,7)
			.Location = New System.Drawing.Point(330,160)
			
		End With
		Dim FeetSlot As New EquipSlot
		With FeetSlot
			.Text = "Feets"
			.init(Me,8)
			.Location = New System.Drawing.Point(330,240)
			
		End With
		
		Dim ring1Slot As New EquipSlot
		With ring1Slot
			.Text = "Finger1"
			.init(Me,11)
			.Location = New System.Drawing.Point(330,320)
			
		End With
		
		Dim ring2Slot As New EquipSlot
		With ring2Slot
			.Text = "Finger2"
			.init(Me,11)
			.Location = New System.Drawing.Point(330,400)
			
		End With
		
		Dim Trinket1Slot As New EquipSlot
		With Trinket1Slot
			.Text = "Trinket1"
			.init(Me,12)
			.Location = New System.Drawing.Point(330,480)
			
		End With
		Dim Trinket2Slot As New EquipSlot
		With Trinket2Slot
			.Text = "Trinket2"
			.init(Me,12)
			.Location = New System.Drawing.Point(330,560)
			
		End With
		LoadMycharacter
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
	
	Sub CmdQuickEPClick(sender As Object, e As EventArgs)
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
End Class
