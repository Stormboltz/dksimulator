'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 07/10/2009
' Heure: 19:14
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.

Imports System.Xml
Public Partial Class MainForm
	
	
	Sub loadWindow()
		Dim item As String
		Dim sTemp As String
		sTemp = CmbCharacter.SelectedItem
		CmbCharacter.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Characters\")
			CmbCharacter.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ) )
		Next
		CmbCharacter.SelectedItem=sTemp
		
		
		sTemp = cmbGearSelector.SelectedItem
		cmbGearSelector.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\CharactersWithGear\")
			cmbGearSelector.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ) )
		Next
		cmbGearSelector.SelectedItem=sTemp
		
		
		stemp = cmbTemplate.SelectedItem
		cmbTemplate.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Templates\")
			cmbTemplate.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ))
		Next
		cmbTemplate.SelectedItem = stemp
		
		sTemp = cmbIntro.SelectedItem
		cmbIntro.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Intro\")
			cmbIntro.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ) )
		Next
		cmbIntro.SelectedItem = sTemp
		
		
		stemp = cmbPrio.SelectedItem
		cmbPrio.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Priority\")
			cmbPrio.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\")))
		Next
		cmbPrio.SelectedItem = stemp
		
		
		sTemp = cmbRotation.SelectedItem
		cmbRotation.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Rotation\")
			cmbRotation.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\")))
		Next
		cmbRotation.SelectedItem = sTemp
		
		
		stemp = cmdPresence.SelectedItem
		cmdPresence.Items.Clear
		cmdPresence.Items.Add("Blood")
		cmdPresence.Items.Add("Unholy")
		cmdPresence.Items.Add("Frost")
		cmdPresence.SelectedItem = stemp
		
		stemp = cmbSigils.SelectedItem
		cmbSigils.Items.Clear
		cmbSigils.Items.Add("None")
		cmbSigils.Items.Add("WildBuck")
		cmbSigils.Items.Add("FrozenConscience")
		cmbSigils.Items.Add("DarkRider")
		cmbSigils.Items.Add("ArthriticBinding")
		cmbSigils.Items.Add("Awareness")
		cmbSigils.Items.Add("Strife")
		cmbSigils.Items.Add("HauntedDreams")
		cmbSigils.Items.Add("VengefulHeart")
		cmbSigils.Items.Add("Virulence")
		cmbSigils.Items.Add("HangedMan")
		cmbSigils.SelectedItem = stemp
		'cmbSigils.Sorted=true
		
		stemp = cmbRuneMH.SelectedItem
		cmbRuneMH.Items.Clear
		cmbRuneMH.Items.Add("None")
		cmbRuneMH.Items.Add("Cinderglacier")
		cmbRuneMH.Items.Add("Razorice")
		cmbRuneMH.Items.Add("FallenCrusader")
		cmbRuneMH.SelectedItem = stemp
		
		stemp= cmbRuneOH.SelectedItem
		cmbRuneOH.Items.Clear
		cmbRuneOH.Items.Add("None")
		cmbRuneOH.Items.Add("Cinderglacier")
		cmbRuneOH.Items.Add("Razorice")
		cmbRuneOH.Items.Add("FallenCrusader")
		cmbRuneOH.Items.Add("Berserking")
		cmbRuneOH.SelectedItem = stemp
		
		stemp= cmbBShOption.SelectedItem
		cmbBShOption.Items.Clear
		cmbBShOption.Items.Add("Instead of Blood Strike")
		cmbBShOption.Items.Add("Instead of Blood Boil")
		cmbBShOption.Items.Add("After BS/BB")
		cmbBShOption.SelectedItem = stemp
		
		stemp = cmbICCBuff.SelectedItem
		cmbICCBuff.Items.Clear
		cmbICCBuff.Items.Add("0")
		cmbICCBuff.Items.Add("5")
		cmbICCBuff.Items.Add("10")
		cmbICCBuff.Items.Add("15")
		cmbICCBuff.Items.Add("20")
		cmbICCBuff.Items.Add("25")
		cmbICCBuff.Items.Add("30")
		cmbICCBuff.SelectedItem = stemp
		
		
		
		
		SimConstructor.PetFriendly = me.ckPet.Checked
		LockSaveButtons
		
	End Sub
	
	Sub LoadTrinket()
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		Doc.Load(Application.StartupPath & "\config\TrinketList.xml")
		dim item as Control
		For Each item In groupBox3.Controls
			item.Dispose
		Next
		groupBox3.Controls.Clear
		Dim xNode As Xml.XmlNode
		
		For Each xNode In Doc.SelectsingleNode("/TrinketList").ChildNodes
			dim ckTrinket as new CheckBox
			ckTrinket.Left = 10
			ckTrinket.Top = 10 + (20 * groupBox3.Controls.Count)
			ckTrinket.Name = "chkEP" & xNode.Name
			ckTrinket.Text = xNode.Name
			ckTrinket.AutoSize = true
			groupBox3.Controls.Add(ckTrinket)
			'debug.Print (xNode.Name)
		Next
	End Sub
	
	
	Sub LoadTankOptions()
		on error goto OUT
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("TankConfig.xml")
		Dim ctrl As Control
		dim txtBox as TextBox
		For Each ctrl in gbTank.Controls
			If ctrl.Name.StartsWith ("txt") Then
				txtBox = ctrl
				txtBox.Text = doc.SelectSingleNode("//config/Stats/" & txtBox.Name ).InnerText
			End If
		Next
		OUT:
	End Sub
	
	Sub saveTankOptions()
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<config></config>")
		Dim xmlStat As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Stats", "")
		Dim root as xml.XmlElement = doc.DocumentElement
		root.AppendChild(xmlStat)
		Dim newElem As xml.XmlNode
		Dim ctrl As Control
		dim txtBox as TextBox
		For Each ctrl in gbTank.Controls
			If ctrl.Name.StartsWith ("txt") Then
				txtBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, txtBox.Name, "")
				newElem.InnerText = txtBox.Text
				xmlStat.AppendChild(newElem)
			End If
		Next
		doc.Save("TankConfig.xml")
	End Sub

	Sub SaveEPOptions
		
		'on error resume next
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<config></config>")
		' Create a new element node.
		Dim xmlStat As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Stats", "")
		Dim xmlSet As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Sets", "")
		dim xmlTrinket As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Trinket", "")
		Dim root as xml.XmlElement = doc.DocumentElement
		root.AppendChild(xmlStat)
		root.AppendChild(xmlSet)
		root.AppendChild(xmlTrinket)
		Dim newElem As xml.XmlNode
		
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in groupBox1.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				xmlStat.AppendChild(newElem)
			End If
		Next
		For Each ctrl in groupBox2.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				xmlSet.AppendChild(newElem)
			End If
		Next
		For Each ctrl in groupBox3.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				xmlTrinket.AppendChild(newElem)
			End If
		Next
		doc.Save("EPconfig.xml")
	End Sub
	
	Sub LoadEPOptions
		on error goto sortie
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in groupBox1.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/Stats/" & chkBox.Name ).InnerText
			End If
		Next
		For Each ctrl in groupBox2.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/Sets/" & chkBox.Name ).InnerText
			End If
		Next
		
		For Each ctrl in groupBox3.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/Trinket/" & chkBox.Name ).InnerText
			End If
		Next
		
		
		sortie:
	End Sub
	
	Sub LoadBuffOption
		on error goto sortie
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("Buffconfig.xml")
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in grpBuff.Controls
			If ctrl.Name.StartsWith ("chk") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/" & chkBox.Name ).InnerText
			End If
		Next
		sortie:
	End Sub
	
	Sub SaveBuffOption
		'on error resume next
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<config></config>")
		' Create a new element node.
		Dim root as xml.XmlElement = doc.DocumentElement
		
		Dim newElem As xml.XmlNode
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in grpBuff.Controls
			If ctrl.Name.StartsWith ("chk") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				root.AppendChild(newElem)
			End If
		Next
		doc.Save("Buffconfig.xml")
	End Sub
	
	
	
	Sub saveConfig
		on error resume next
		'	Try
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<config></config>")
		
		Dim root As xml.XmlElement = doc.DocumentElement
		
		
		Dim newElem As xml.XmlNode
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "UseCharacter", "")
		newElem.InnerText = rCharacter.Checked
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Character", "")
		newElem.InnerText = cmbCharacter.SelectedItem.tostring
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "CharacterWithGear", "")
		newElem.InnerText = cmbGearSelector.SelectedItem.tostring
		root.AppendChild(newElem)
		
		
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "template", "")
		newElem.InnerText = cmbTemplate.SelectedItem.tostring
		root.AppendChild(newElem)
		
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "mode", "")
		If rdPrio.Checked Then
			newElem.InnerText = "priority"
		Else
			newElem.InnerText = "rotation"
		End If
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "intro", "")
		newElem.InnerText = cmbIntro.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "priority", "")
		newElem.InnerText = cmbPrio.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "rotation", "")
		newElem.InnerText = cmbRotation.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "presence", "")
		newElem.InnerText = cmdPresence.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "sigil", "")
		newElem.InnerText = cmbSigils.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "mh", "")
		newElem.InnerText = cmbRuneMH.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "oh", "")
		newElem.InnerText = cmbRuneOH.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		'Latence
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "latency", "")
		newElem.InnerText = txtLatency.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		'Sim Time
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "simtime", "")
		newElem.InnerText = txtSimtime.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		'Genere combat log
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "log", "")
		newElem.InnerText = chkCombatLog.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		
		'Detailed
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "logdetail", "")
		newElem.InnerText = ckLogRP.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		
		'Ghouls2haste
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ShowProc", "")
		newElem.InnerText = chkShowProc.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		'FCWait
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "WaitFC", "")
		newElem.InnerText = chkWaitFC.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		'Pets
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "pet", "")
		newElem.InnerText = ckPet.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		'Patch
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Patch", "")
		newElem.InnerText = chkPatch.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodSync", "")
		newElem.InnerText = chkBloodSync.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Enemies", "")
		newElem.InnerText = txtNumberOfEnemies.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BShOption", "")
		newElem.InnerText = cmbBShOption.SelectedItem.ToString
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ICCBuff", "")
		newElem.InnerText = cmbICCBuff.SelectedItem.ToString
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
	
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "txtManyFights", "")
		newElem.InnerText = txtManyFights.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "chkManyFights", "")
		newElem.InnerText = chkManyFights.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "txtAMSrp", "")
		newElem.InnerText = txtAMSrp.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "txtAMScd", "")
		newElem.InnerText = txtAMScd.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "chkMergeReport", "")
		newElem.InnerText = chkMergeReport.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "txtReportName", "")
		newElem.InnerText = txtReportName.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)

		doc.Save("config.xml")
		
		'	Catch e As Exception
		
		'	End Try
	End Sub
	
	Sub loadConfig
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		on error resume next
		doc.Load("config.xml")
		Select Case doc.SelectSingleNode("//config/UseCharacter").InnerText
			Case True
				rCharacter.Checked = True
				rCharwithGear.Checked = false
			Case False
				rCharacter.Checked = false
				rCharwithGear.Checked = true
		End Select
		
		
		
		cmbCharacter.SelectedItem = doc.SelectSingleNode("//config/Character").InnerText
		cmbGearSelector.SelectedItem = doc.SelectSingleNode("//config/CharacterWithGear").InnerText
		cmbTemplate.SelectedItem = doc.SelectSingleNode("//config/template").InnerText
		If doc.SelectSingleNode("//config/mode").InnerText <> "rotation" Then
			rdPrio.Checked = true
		Else
			rdRot.Checked = true
		End If
		cmbIntro.SelectedItem = doc.SelectSingleNode("//config/intro").InnerText
		cmbPrio.SelectedItem = doc.SelectSingleNode("//config/priority").InnerText
		cmbRotation.SelectedItem= doc.SelectSingleNode("//config/rotation").InnerText
		cmdPresence.SelectedItem = doc.SelectSingleNode("//config/presence").InnerText
		cmbSigils.SelectedItem = doc.SelectSingleNode("//config/sigil").InnerText
		cmbRuneMH.SelectedItem = doc.SelectSingleNode("//config/mh").InnerText
		cmbRuneOH.SelectedItem = doc.SelectSingleNode("//config/oh").InnerText
		cmbBShOption.SelectedItem = doc.SelectSingleNode("//config/BShOption").InnerText
		cmbICCBuff.SelectedItem = doc.SelectSingleNode("//config/ICCBuff").InnerText
		txtLatency.Text = doc.SelectSingleNode("//config/latency").InnerText
		
		txtSimtime.Text = doc.SelectSingleNode("//config/simtime").InnerText
		chkCombatLog.Checked = doc.SelectSingleNode("//config/log").InnerText
		ckLogRP.Checked = doc.SelectSingleNode("//config/logdetail").InnerText
		chkShowProc.Checked = doc.SelectSingleNode("//config/ShowProc").InnerText
		chkWaitFC.Checked = doc.SelectSingleNode("//config/WaitFC").InnerText
		chkPatch.Checked = doc.SelectSingleNode("//config/Patch").InnerText
		
		ckPet.Checked = doc.SelectSingleNode("//config/pet").InnerText
		txtNumberOfEnemies.Text  = doc.SelectSingleNode("//config/Enemies").InnerText
		
		txtManyFights.Text = doc.SelectSingleNode("//config/txtManyFights").InnerText
		chkManyFights.Checked = doc.SelectSingleNode("//config/chkManyFights").InnerText
		txtAMSrp.Text = doc.SelectSingleNode("//config/txtAMSrp").InnerText
		txtAMScd.Text = doc.SelectSingleNode("//config/txtAMScd").InnerText
		
		chkMergeReport.Checked = doc.SelectSingleNode("//config/chkMergeReport").InnerText
		txtReportName.Text = doc.SelectSingleNode("//config/txtReportName").InnerText
 		chkBloodSync.Checked = doc.SelectSingleNode("//config/BloodSync").InnerText
				
		errH:
	End Sub
	
	Sub LoadScaling()
		on error goto OUT
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("Scalingconfig.xml")
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in gbScaling.Controls
			If ctrl.Name.StartsWith ("chkSca") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/Stats/" & chkBox.Name ).InnerText
			End If
		Next
		OUT:
	End Sub
	
	Sub saveScaling()
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<config></config>")
		Dim xmlStat As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Stats", "")
		Dim root as xml.XmlElement = doc.DocumentElement
		root.AppendChild(xmlStat)
		Dim newElem As xml.XmlNode
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in gbScaling.Controls
			If ctrl.Name.StartsWith ("chk") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				xmlStat.AppendChild(newElem)
			End If
		Next
		doc.Save("Scalingconfig.xml")
	End Sub
	
	Sub SaveAll()
		
	End Sub
	Sub LockSaveButtons()
		cmdSave.Enabled = False
		cmdSaveNew.Enabled = False
		cmdSaveCharacter.Enabled=False
		cmdSaveRotation.Enabled=False
		cmdSaveTemplate.Enabled = False
	End Sub
End Class
