'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 07/10/2009
' Heure: 19:07
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Imports System.Xml
Public Partial Class MainForm
	
	
	Sub CmdEditTemplateClick(sender As Object, e As EventArgs)
		tabControl1.SelectedIndex = 3
		'CreateTreeTemplate()
		on error goto errH
		Dim tr As IO.Textreader
		EditorFilePAth = Application.StartupPath & "\Templates\" & cmbTemplate.Text
		tr =  new IO.StreamReader(EditorFilePAth )
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		dim xmlDoc as New Xml.XmlDocument
		xmlDoc.Load(EditorFilePAth)
		TemplatePath = EditorFilePAth
		displaytemplateInEditor (EditorFilePAth)
		cmdSaveTemplate.Enabled=True		
		cmdSave.Enabled=True
		cmdSaveNew.Enabled = True
		errH:
	End Sub
	
	
	
	
	Sub SetTalentPointnumber
		Dim BT As TemplateButton
		Dim b As Integer
		Dim f As Integer
		dim u as Integer
		
		
		For Each BT In btList
			Select Case BT.School
				Case "blood"
					b = b + bt.Value
				Case "frost"
					f = f + bt.Value
				Case "unholy"
					u = u + bt.Value
			End Select
		Next
		lblBlood.Text = b
		lblFrost.Text = f
		lblUnholy.Text = u
	End Sub
	
	Sub CreateTreeTemplate()
		Dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load("template.xml")
		Dim xNode As Xml.XmlNode
		Dim xParentNode As Xml.XmlNode
		Dim xNodeList As Xml.XmlNodeList
		Dim myBT As TemplateButton
		
		
		btList.Clear
		Me.tbTpl.Select
		
		xNodeList = XmlDoc.SelectNodes("/Talents/blood")
		xParentNode = XmlDoc.SelectSingleNode("/Talents/blood")
		For Each xNode In xParentNode.ChildNodes
			myBT = New TemplateButton
			myBT.Name = xNode.Name
			myBT.School = "blood"
			myBT.Text = xNode.Name
			Me.tbTpl.Controls.Add(myBT)
			myBT.Location = New System.Drawing.Point(-10+(xNode.Attributes.GetNamedItem("col")).Value*35, -20+xNode.Attributes.GetNamedItem("row").value*35)
			myBT.MaxValue = xNode.InnerText
			toolTip.SetToolTip(myBT,myBT.Name)
			btList.Add(myBT,xNode.Name)
			myBT.init
		Next
		
		xParentNode = XmlDoc.SelectSingleNode("/Talents/frost")
		For Each xNode In xParentNode.ChildNodes
			myBT = New TemplateButton
			myBT.Name = xNode.Name
			myBT.School = "frost"
			Me.tbTpl.Controls.Add(myBT)
			myBT.Location = New System.Drawing.Point(140+(xNode.Attributes.GetNamedItem("col")).Value*35, -20+xNode.Attributes.GetNamedItem("row").value*35)
			myBT.Text = xNode.Name
			myBT.MaxValue = xNode.InnerText
			btList.Add(myBT,xNode.Name)
			myBT.init
		Next
		
		xParentNode = XmlDoc.SelectSingleNode("/Talents/unholy")
		For Each xNode In xParentNode.ChildNodes
			myBT = New TemplateButton
			myBT.Name = xNode.Name
			myBT.School = "unholy"
			Me.tbTpl.Controls.Add(myBT)
			myBT.Location = New System.Drawing.Point(300+(xNode.Attributes.GetNamedItem("col")).Value*35, -20+xNode.Attributes.GetNamedItem("row").value*35)
			myBT.Text = xNode.Name
			myBT.MaxValue = xNode.InnerText
			btList.Add(myBT,xNode.Name)
			myBT.init
		Next
		
		xParentNode = XmlDoc.SelectSingleNode("/Talents/Glyphs")
		For Each xNode In xParentNode.ChildNodes
			cmbGlyph1.Items.Add(xNode.Name)
			cmbGlyph2.Items.Add(xNode.Name)
			cmbGlyph3.Items.Add(xNode.Name)
		Next
		
		
	End Sub
	
	Sub DisplayTemplateInEditor(path As String)
		dim xmlDoc as New Xml.XmlDocument
		xmlDoc.Load(EditorFilePAth)
		Dim xNode As XmlNode
		Dim xParentNode As XmlNode
		Dim xNodelist As XmlNodeList
		dim i as Integer
		
		xParentNode = XmlDoc.SelectSingleNode("/Talents")
		dim BT as TemplateButton
		On Error Resume Next
		For Each BT In btList
			BT.SetValue(XmlDoc.SelectSingleNode("/Talents/" & BT.Name).InnerText )
		Next
		xParentNode = XmlDoc.SelectSingleNode("/Talents/Glyphs")
		
		For Each xNode In xParentNode.ChildNodes
			If xNode.InnerText = 1 Then
				Select Case i
					Case 0
						cmbGlyph1.SelectedItem=xNode.name
					Case 1
						cmbGlyph2.SelectedItem=xNode.name
					Case 2
						cmbGlyph3.SelectedItem=xNode.name
				End Select
				i=i+1
			End If
		Next
		
		
		
	End Sub
	
		Sub CmdImportTemplateClick(sender As Object, e As EventArgs)
		ImportTemplate("")
	End Sub
	Sub ImportTemplate(name As String)
		
		Dim URL As String = txtImportTemplate.Text
		Dim tmp As String
		
		On Error GoTo errH
		
		tmp = strings.right(URL,URL.Length - instr(url,"="))
		tmp = strings.left (tmp,instr(tmp,"&")-1)
		
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<Talents></Talents>")
		Dim root As xml.XmlElement = doc.DocumentElement
		Dim newElem as xml.XmlNode
		
		Dim i As Integer = 1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "URL", "")
		newElem.InnerText = txtImportTemplate.Text
		root.AppendChild(newElem)
		
		
		'Blood
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Butchery", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Subversion", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BladeBarrier", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BladedArmor", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ScentOfBlood", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Weapspec", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RuneTap", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Darkconv", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "DRM", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IRuneTap", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "SpellDeflection", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Vendetta", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodyStrikes", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Vot3W", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MarkBlood", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodyVengeance", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "AbominationMight", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodWorms", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Hysteria", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IBloodPresence", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ImprovedDeathStrike", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "SuddenDoom", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Vampiric", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "WillNecropolis", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HeartStrike", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MightofMograine", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodGorged", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "DRW", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		
		'FROST
		
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ImprovedIcyTouch", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RPM", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Toughness", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IcyReach", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BlackIce", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "NervesofColdSteel", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IcyTalons", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "LichBorne", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Annihilation", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "KillingMachine", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ChillOfTheGrave", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "EndlessWinter", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Frigid", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "GlacierRot", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Deathchill", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ImprovedIcyTalons", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MercilessCombat", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Rime", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ChillBlains", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HungeringCold", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IFrostPresence", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ThreatOfThassarian", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodoftheNorth", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "UnbreakableArmor", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Acclimatation", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "FrostStrike", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "GuileOfGorefiend", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "TundraStalker", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HowlingBlast", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		' Unholy
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ViciousStrikes", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Virulence", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Anticipation", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Epidemic", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Morbidity", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "UnholyCommand", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RavenousDead", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Outbreak", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Necrosis", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "CorpseExplosion", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "PaleHorse", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodCakedBlade", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "NightoftheDead", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "UnholyBlight", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Impurity", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Dirge", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Desecration", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MagicSuppression", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Reaping", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MasterOfGhouls", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Desolation", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "AMZ", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ImprovedUnholyPresence", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "GhoulFrenzy", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "CryptFever", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BoneShield", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "WanderingPlague", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "EbonPlaguebringer", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ScourgeStrike", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RageofRivendare", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "SummonGargoyle", "")
		newElem.InnerText = strings.mid(tmp,i,1)
		root.AppendChild(newElem)
		i=i+1
		
		
		'Glyphs
		
		tmp = strings.right(URL,URL.Length - instr(url,"glyph=")-5)
		tmp = strings.left (tmp,instr(tmp,"&")-1)
		dim glyph1 as String
		dim glyph2 as String
		dim glyph3 as String
		glyph1 = tmp.Chars(0) + tmp.Chars(1)
		glyph2 = tmp.Chars(2) + tmp.Chars(3)
		glyph3 = tmp.Chars(4) + tmp.Chars(5)
		
		dim glyphID as String
		
		Dim xmlGlyph As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Glyphs", "")
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodStrike", "")
		glyphID = "03"
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		
		glyphID = "08"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "DarkDeath", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		
		glyphID = "10"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "DeathStrike", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "13"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "FrostStrike", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "14"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HowlingBlast", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "17"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IcyTouch", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "18"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Obliterate", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "19"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "PlagueStrike", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "22"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ScourgeStrike", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "25"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "UnholyBlight", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "27"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Ghoul", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "11"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "DeathandDecay", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "06"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "DRW", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "12"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Disease", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		glyphID = "20"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RuneStrike", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		
		glyphID = "04"
		xmlGlyph.AppendChild(newElem)
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BoneShield", "")
		If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
			newElem.InnerText = 1
		Else
			newElem.InnerText = 0
		End If
		xmlGlyph.AppendChild(newElem)
		
		root.AppendChild(xmlGlyph)
		
		
		
		if name = "" then
			Dim truc As New Form1
			Dim res As DialogResult
			res = truc.ShowDialog
			If truc.textBox1.Text  <> "" And res = DialogResult.OK Then
				doc.Save(application.StartupPath & "\Templates\" & truc.textBox1.Text & ".xml")
				EditorFilePAth = Strings.Left(EditorFilePAth,strings.InStrRev(EditorFilePAth,"\"))
			Else
				exit sub
			End If
			truc.Dispose
		Else
			doc.Save(name)
		End If
		
		'Msgbox ( "Import done.")
		loadWindow
		tabControl1.SelectedIndex = 0
		Exit Sub
		errH :
		msgbox("Error while importing talents !")
	End Sub
	
	Sub CmdSaveTemplateClick(sender As Object, e As EventArgs)
		Dim sTemp As String
		dim BT as TemplateButton
		sTemp = "="
		For Each BT In btList
			sTemp = sTemp & BT.Value.ToString
		Next
		sTemp = sTemp & "&glyph="
		sTemp = sTemp & GlobalFunction.GetIdFromGlyphName(cmbGlyph1.SelectedItem.ToString)
		sTemp = sTemp & GlobalFunction.GetIdFromGlyphName(cmbGlyph2.SelectedItem.ToString)
		sTemp = sTemp & GlobalFunction.GetIdFromGlyphName(cmbGlyph3.SelectedItem.ToString)
		sTemp = sTemp  & "&"
		txtImportTemplate.Text = sTemp
		ImportTemplate(Me.TemplatePath)
		LockSaveButtons
	End Sub
	
	Sub CmdSaveNewTemplateClick(sender As Object, e As EventArgs)
		Dim sTemp As String
		dim BT as TemplateButton
		sTemp = "="
		For Each BT In btList
			sTemp = sTemp & BT.Value.ToString
		Next
		on error resume next
		sTemp = sTemp & "&glyph="
		sTemp = sTemp & GlobalFunction.GetIdFromGlyphName(cmbGlyph1.SelectedItem.ToString)
		sTemp = sTemp & GlobalFunction.GetIdFromGlyphName(cmbGlyph2.SelectedItem.ToString)
		sTemp = sTemp & GlobalFunction.GetIdFromGlyphName(cmbGlyph3.SelectedItem.ToString)
		sTemp = sTemp  & "&"
		txtImportTemplate.Text = sTemp
		ImportTemplate("")
		LockSaveButtons
	End Sub
	
	
	Sub CmdStartTalentDpsValueClick(sender As Object, e As EventArgs)
		if LoadBeforeSim = false then exit sub
		Me.tabControl1.SelectedIndex = 1
		
		SimConstructor.StartSpecDpsValue(PBsim,txtSimtime.Text,me)
	End Sub
End Class
