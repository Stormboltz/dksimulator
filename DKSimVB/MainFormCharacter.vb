'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/10/2009
' Heure: 11:10 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Imports System.Xml
Public Partial Class MainForm
	
	Sub LoadCharacter(FilePath As String)
		on error resume next
		Dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load (FilePath)
		txtStr.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Strength").InnerText)
		txtAgi.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Agility").InnerText)
		txtIntel.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Intel").InnerText)
		txtArmor.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Armor").InnerText)
		txtAP.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/AttackPower").InnerText)
		txtHit.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/HitRating").InnerText)
		txtCrit.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/CritRating").InnerText)
		txtHaste.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/HasteRating").InnerText)
		txtArP.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/ArmorPenetrationRating").InnerText)
		txtExp.Text = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/ExpertiseRating").InnerText)
		
		If XmlDoc.SelectSingleNode("//character/weapon/count").InnerText = 2 Then
			r2Hand.Checked=False
			rDW.Checked = true
		Else
			r2Hand.Checked=true
			rDW.Checked = false
		End If
		txtMHDPS.Text = XmlDoc.SelectSingleNode("//character/weapon/mainhand/dps").InnerText
		txtMHWSpeed.Text = XmlDoc.SelectSingleNode("//character/weapon/mainhand/speed").InnerText
		txtOHDPS.Text = XmlDoc.SelectSingleNode("//character/weapon/offhand/dps").InnerText
		txtOHWSpeed.Text = XmlDoc.SelectSingleNode("//character/weapon/offhand/speed").InnerText
		
		Dim i As Integer
		Dim node As XmlNode
		cmbSetBonus1.SelectedItem = nothing
		cmbSetBonus2.SelectedItem = nothing
		For Each node In xmldoc.SelectSingleNode("//character/Set").ChildNodes
			if node.InnerText<>0 then
				If i=0 Then
					cmbSetBonus1.SelectedItem = node.Name
					i=i+1
				Else
					cmbSetBonus2.SelectedItem = node.Name
				End If
			End If
		Next
		
		i=0
		cmbtrinket1.SelectedItem = nothing
		cmbtrinket2.SelectedItem = nothing
		For Each node In xmldoc.SelectSingleNode("//character/trinket").ChildNodes
			if node.InnerText<>0 then
				If i=0 Then
					cmbtrinket1.SelectedItem = node.Name
					i=i+1
				Else
					cmbtrinket2.SelectedItem = node.Name
				End If
			End If
		Next
		i=0
		cmbWeaponProc1.Selecteditem = nothing
		cmbWeaponProc2.Selecteditem = nothing
		For Each node In xmldoc.SelectSingleNode("//character/WeaponProc").ChildNodes
			if node.InnerText<>0 then
				If i=0 Then
					cmbWeaponProc1.SelectedItem = node.Name
					i=i+1
				Else
					cmbWeaponProc2.SelectedItem = node.Name
				End If
			End If
		Next
		
		chkIngenieer.Checked = XmlDoc.SelectSingleNode("//character/misc/HandMountedPyroRocket").InnerText
		chkAccelerators.Checked = XmlDoc.SelectSingleNode("//character/misc/HyperspeedAccelerators").InnerText
		chkMeta.Checked = XmlDoc.SelectSingleNode("//character/misc/ChaoticSkyflareDiamond").InnerText
		chkTailorEnchant.Checked = XmlDoc.SelectSingleNode("//character/misc/TailorEnchant").InnerText
		
		
		txtMHExpBonus.Text = XmlDoc.SelectSingleNode("//character/racials/MHExpertiseBonus").InnerText
		txtOHExpBonus.Text = XmlDoc.SelectSingleNode("//character/racials/OHExpertiseBonus").InnerText
		chkBloodFury.Checked = XmlDoc.SelectSingleNode("//character/racials/Orc").InnerText
		chkBerzerking.Checked = XmlDoc.SelectSingleNode("//character/racials/Troll").InnerText
		chkArcaneTorrent.Checked = XmlDoc.SelectSingleNode("//character/racials/BloodElf").InnerText
		
	End Sub
	
	Sub InitCharacterPanel()
		Dim Doc As new Xml.XmlDocument
		Dim node As XmlNode
		Dim tmp1 As String
		dim tmp2 as String
		
		Doc.Load(Application.StartupPath & "\config\TrinketList.xml")
		tmp1 = cmbTrinket1.SelectedText
		tmp2 = cmbTrinket1.SelectedText
		For Each node In doc.SelectSingleNode("//TrinketList").ChildNodes
			cmbTrinket1.Items.Add(node.name)
			cmbTrinket2.Items.Add(node.name)
		Next
		cmbTrinket1.SelectedText = tmp1
		cmbTrinket1.SelectedText = tmp2
		
		Doc.Load(Application.StartupPath & "\config\WeaponProcList.xml")
		tmp1 = cmbWeaponProc1.SelectedText
		tmp2 = cmbWeaponProc2.SelectedText
		For Each node In doc.SelectSingleNode("//WeaponProcList").ChildNodes
			If node.Name.StartsWith("MH")  Then
				cmbWeaponProc1.Items.Add(node.name)
			Else
				cmbWeaponProc2.Items.Add(node.name)
			End If
			
			
		Next
		cmbWeaponProc1.SelectedText = tmp1
		cmbWeaponProc2.SelectedText = tmp2
		
		Doc.Load(Application.StartupPath & "\config\SetBonusList.xml")
		tmp1 = cmbSetBonus1.SelectedText
		tmp2 = cmbSetBonus2.SelectedText
		For Each node In doc.SelectSingleNode("//SetBonusList").ChildNodes
			cmbSetBonus1.Items.Add(node.name)
			cmbSetBonus2.Items.Add(node.name)
		Next
		cmbSetBonus1.SelectedText = tmp1
		cmbSetBonus2.SelectedText = tmp2
		
		cmbTrinket1.Sorted = True
		cmbTrinket2.Sorted = True
		cmbWeaponProc1.Sorted = True
		cmbWeaponProc2.Sorted = True
		cmbSetBonus1.Sorted = True
		cmbSetBonus2.Sorted = True
		
		
		txtMHExpBonus.Text = 0
		txtOHExpBonus.Text = 0
		
	End Sub
	
	
	
	Sub RDWCheckedChanged(sender As Object, e As EventArgs)
		If RDW.Checked Then
			r2Hand.Checked = False
			txtOHDPS.Enabled = true
			txtOHWSpeed.Enabled = true
		End If
	End Sub
	
	Sub R2HandCheckedChanged(sender As Object, e As EventArgs)
		If r2Hand.Checked Then
			RDW.Checked = False
			txtOHDPS.Enabled = false
			txtOHWSpeed.Enabled = false
		End If
	End Sub
	
	
	Sub SaveCharacter(filepath As String)
		Dim xmlDoc As New XmlDocument
		Dim root As XmlElement
		dim xElem as XmlElement
		xmlDoc.LoadXml("<character></character>")
		root = xmlDoc.DocumentElement
		
		Dim xStat As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "stat", "")
		
		root.AppendChild(xStat)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "Strength", "")
		xElem.InnerText = txtStr.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "Agility", "")
		xElem.InnerText = txtAgi.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "Intel", "")
		xElem.InnerText = txtIntel.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "Armor", "")
		xElem.InnerText = txtArmor.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "AttackPower", "")
		xElem.InnerText = txtAP.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "HitRating", "")
		xElem.InnerText = txthit.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "CritRating", "")
		xElem.InnerText = txtcrit.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "HasteRating", "")
		xElem.InnerText = txtHaste.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "ArmorPenetrationRating", "")
		xElem.InnerText = txtArP.Text
		xStat.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "ExpertiseRating", "")
		xElem.InnerText = txtExp.Text
		xStat.AppendChild(xElem)
		
		
		Dim xweapon As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "weapon", "")
		root.AppendChild(xweapon)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "count", "")
		If r2Hand.Checked=True Then
			xElem.InnerText = 1
		Else
			xElem.InnerText = 2
		End If
		xweapon.AppendChild(xElem)
		
		
		
		Dim xweapon1 As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "mainhand", "")
		Dim xweapon2 As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "offhand", "")
		xweapon.AppendChild(xweapon1)
		xweapon.AppendChild(xweapon2)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "dps", "")
		xElem.InnerText = txtMHDPS.Text
		xweapon1.AppendChild(xElem)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "speed", "")
		xElem.InnerText = txtMHWSpeed.Text
		xweapon1.AppendChild(xElem)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "dps", "")
		xElem.InnerText = txtOHDPS.Text
		xweapon2.AppendChild(xElem)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "speed", "")
		xElem.InnerText = txtOHWSpeed.Text
		xweapon2.AppendChild(xElem)
		
		Dim item As Object
		
		Dim xSet As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "Set", "")
		root.AppendChild(xSet)
		
		Dim tmp1 As String
		Dim tmp2 As String
		
		Try
			tmp1 = cmbSetBonus1.SelectedItem.ToString
		Catch
			tmp1 = ""
		End Try
		
		Try
			tmp2 = cmbSetBonus2.SelectedItem.ToString
		Catch
			tmp2 = ""
		End Try
		
		
		For Each item In cmbSetBonus1.Items
			xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, item, "")
			If item.ToString = tmp1 or item.ToString = tmp2 Then
				xElem.InnerText = 1
			Else
				xElem.InnerText = 0
			End If
			xSet.AppendChild(xElem)
		Next
		
		
		Try
			tmp1 = cmbTrinket1.SelectedItem.ToString
		Catch
			tmp1 = ""
		End Try
		
		Try
			tmp2 = cmbTrinket2.SelectedItem.ToString
		Catch
			tmp2 = ""
		End Try
		
		
		Dim xtrinket As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "trinket", "")
		root.AppendChild(xtrinket)
		For Each item In cmbTrinket1.Items
			xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, item, "")
			If item.ToString = tmp1 or item.ToString = tmp2 Then
				xElem.InnerText = 1
			Else
				xElem.InnerText = 0
			End If
			xtrinket.AppendChild(xElem)
		Next
		
		
		
		Try
			tmp1 = cmbWeaponProc1.SelectedItem.ToString
		Catch
			tmp1 = ""
		End Try
		
		Try
			tmp2 = cmbWeaponProc2.SelectedItem.ToString
		Catch
			tmp2 = ""
		End Try
		
		Dim xWeaponProc As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "WeaponProc", "")
		root.AppendChild(xWeaponProc)
		For Each item In cmbWeaponProc1.Items
			xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, item, "")
			If item.ToString = tmp1  Then
				xElem.InnerText = 1
			Else
				xElem.InnerText = 0
			End If
			xWeaponProc.AppendChild(xElem)
		Next
		
		For Each item In cmbWeaponProc2.Items
			xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, item, "")
			If item.ToString = tmp2  Then
				xElem.InnerText = 1
			Else
				xElem.InnerText = 0
			End If
			xWeaponProc.AppendChild(xElem)
		Next
		
		
		Dim xMisc As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "misc", "")
		root.AppendChild(xMisc)
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "HandMountedPyroRocket", "")
		xElem.InnerText = chkIngenieer.Checked
		xMisc.AppendChild(xElem)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "HyperspeedAccelerators", "")
		xElem.InnerText = chkAccelerators.Checked
		xMisc.AppendChild(xElem)
	
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "ChaoticSkyflareDiamond", "")
		xElem.InnerText = chkMeta.Checked
		xMisc.AppendChild(xElem)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element,"TailorEnchant" , "")
		xElem.InnerText = chkTailorEnchant.Checked
		xMisc.AppendChild(xElem)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element,"AshenBand" , "")
		xElem.InnerText = chkAshenBand.Checked
		xMisc.AppendChild(xElem)
		
		
		Dim xRacial As XmlNode = xmlDoc.CreateNode(xml.XmlNodeType.Element, "racials", "")
		root.AppendChild(xRacial)
		
		'Racials		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element,"MHExpertiseBonus" , "")
		xElem.InnerText = txtMHExpBonus.Text
		xRacial.AppendChild(xElem)
		
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element,"OHExpertiseBonus" , "")
		xElem.InnerText = txtOHExpBonus.Text
		xRacial.AppendChild(xElem)
		
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element,"Orc" , "")
		xElem.InnerText = chkBloodFury.Checked
		xRacial.AppendChild(xElem)
		
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element,"Troll" , "")
		xElem.InnerText = chkBerzerking.Checked
		xRacial.AppendChild(xElem)
		
		xElem = xmlDoc.CreateNode(xml.XmlNodeType.Element,"BloodElf" , "")
		xElem.InnerText = chkArcaneTorrent.Checked
		xRacial.AppendChild(xElem)
		
		
		
		xmlDoc.Save(filepath)
	End Sub
	
	
	Sub CmdSaveCharacterClick(sender As Object, e As EventArgs)
		SaveCharacter(EditorFilePAth)
		loadWindow
		tabControl1.SelectedIndex = 0
	End Sub
	
	Sub CmdSaveNewCharatecClick(sender As Object, e As EventArgs)
		Dim truc As New Form1
		Dim res As DialogResult
		res = truc.ShowDialog
		If truc.textBox1.Text  <> "" and res = DialogResult.OK Then
			EditorFilePAth = Strings.Left(EditorFilePAth,strings.InStrRev(EditorFilePAth,"\")) & truc.textBox1.Text & ".xml"
			CmdSaveCharacterClick(sender, e)
		Else
			exit sub
		End If
		truc.Dispose
	End Sub
	

End Class
