'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 10:41 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Partial Class MainForm
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub CmdExtratorClick(sender As Object, e As EventArgs)
		'exit sub
		Dim MyExtractor As new Extractor
		MyExtractor.Start
	End Sub
	
	Sub CmdLoadGearClick(sender As Object, e As EventArgs)
		Dim Gear As New GearLoader
		dim contr as Control
		Gear.Init
		LoadMycharacter
	End Sub
	
	
	Sub CmdSaveClick(sender As Object, e As EventArgs)
		SaveMycharacter
	End Sub
	
	Sub SaveMycharacter
		Dim xmlChar As New Xml.XmlDocument
		xmlChar.LoadXml("<character/>")
		
		Dim root As xml.XmlElement = xmlChar.DocumentElement
		Dim newElem As xml.XmlNode
		Dim gemNode as Xml.XmlNode
		Dim contr As Control
		dim iSlot as ListViewItem
		For Each iSlot In lvCharacter.Items
			try
				newElem = xmlChar.CreateNode(xml.XmlNodeType.Element, iSlot.Text, "")
				newElem.InnerText = iSlot.SubItems.Item(3).Text
				
				
					
				Try
				gemNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "gem1", "")
				gemNode.InnerText = lvGem1.Items.Item(iSlot.Index).SubItems.Item(2).Text
				newElem.AppendChild(gemNode)
				catch
				End Try
				Try
				gemNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "gem2", "")
				gemNode.InnerText = lvGem2.Items.Item(iSlot.Index).SubItems.Item(2).Text
				newElem.AppendChild(gemNode)
				catch
				End Try
				Try
				
				gemNode = xmlChar.CreateNode(xml.XmlNodeType.Element, "gem3", "")
				gemNode.InnerText = lvGem3.Items.Item(iSlot.Index).SubItems.Item(2).Text
				newElem.AppendChild(gemNode)
				catch
				End Try
				
				root.AppendChild(newElem)
				
				
				
				
				
				
			Catch
			End Try
			
		Next
		xmlChar.Save("mycharacter.xml")
	End Sub
	
	Sub LoadMycharacter
		Dim xmlChar As New Xml.XmlDocument
		xmlChar.Load("mycharacter.xml")
		Dim root As xml.XmlElement = xmlChar.DocumentElement
		Dim newElem As xml.XmlNode
		Dim contr As Control
		dim iSlot as ListViewItem
		For Each iSlot In lvCharacter.Items
			Try
				 DisplayItem( xmlChar.SelectSingleNode("/character/" & iSlot.Text).InnerText,iSlot)
				 Try
				 	DisplayGem(xmlChar.SelectSingleNode("/character/" & iSlot.Text & "/gem1").InnerText,lvGem1.Items.Item(iSlot.Index))
				 Catch
				 End Try
				 Try
				 	DisplayGem(xmlChar.SelectSingleNode("/character/" & iSlot.Text & "/gem2").InnerText,lvGem2.Items.Item(iSlot.Index))
				 Catch
				 End Try
				 Try
				 	DisplayGem(xmlChar.SelectSingleNode("/character/" & iSlot.Text & "/gem3").InnerText,lvGem3.Items.Item(iSlot.Index))
				 Catch
				 End Try
				 
			catch
			end try
			
		Next
	End Sub
	
	
	
	Sub Button1Click(sender As Object, e As EventArgs)
		Dim HeadLoad As New GearSelector
		HeadLoad.Show
		HeadLoad.Visible = true
		HeadLoad.LoadItem("1")
	End Sub
	
	Sub Button2Click(sender As Object, e As EventArgs)
		Dim HeadLoad As New GearSelector
		HeadLoad.Show
		HeadLoad.Visible = true
		HeadLoad.LoadItem("2")
	End Sub
	
	
	
	Sub MainFormLoad(sender As Object, e As EventArgs)
		lvCharacter.Items.Add("Head").SubItems.Add("1")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Neck").SubItems.Add("2")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Shoulder").SubItems.Add("3")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Back").SubItems.Add("16")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Chest").SubItems.Add("5")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Wrist").SubItems.Add("9")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Hand").SubItems.Add("10")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Wraist").SubItems.Add("6")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Legs").SubItems.Add("7")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Feets").SubItems.Add("8")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Ring1").SubItems.Add("11")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Ring2").SubItems.Add("11")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Trinket1").SubItems.Add("12")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Trinket2").SubItems.Add("12")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("TwoH").SubItems.Add("17")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("MH").SubItems.Add("13")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("OH").SubItems.Add("13")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
		lvCharacter.Items.Add("Relic").SubItems.Add("28")
		lvGem1.Items.Add("")
		lvGem2.Items.Add("")
		lvGem3.Items.Add("")
		lvGembonus.Items.Add("")
	End Sub
	
	Sub LvCharacterSelectedIndexChanged(sender As Object, e As EventArgs)
		Dim item As ListViewItem
		if sender.SelectedItems.count = 0 then exit sub
		
		Try
			item = sender.SelectedItems.Item(0)
			Dim HeadLoad As New GearSelector
			
			'HeadLoad.Visible = true
			HeadLoad.LoadItem(item.SubItems.Item(1).Text)
			HeadLoad.ShowDialog(Me)
			If HeadLoad.DialogResult = DialogResult.OK Then
				DisplayItem(HeadLoad.SelectedItem,item)
				
				
				'debug.Print (HeadLoad.SelectedItem)
			End If
			HeadLoad.Dispose
			me.Focus
		Catch
			If Err.Number <> 0 Then
				Dim msg As String
				
				Msg = "Error # " & Str(Err.Number) & " was generated by " _
					& Err.Source & ControlChars.CrLf & Err.Description
				debug.Print(Msg)
			End If
			
			
		End Try
	End Sub
	
	Sub DisplayItem(id As String,lItem As ListViewItem)
		Dim xmlDB As New Xml.XmlDocument
		dim xmlItem As New Xml.XmlDocument
		xmlDB.Load("itemDB.xml")
		xmlItem.LoadXml (xmlDB.SelectSingleNode("/items/item[id="& id &"]").OuterXml)
		
		If lItem.subitems.Count < 3 Then
			lItem.subitems.Add(xmlItem.SelectSingleNode("/item/name").InnerText)
			Else
			lItem.subitems.item(2).text = xmlItem.SelectSingleNode("/item/name").InnerText
		End If
		
		If lItem.subitems.Count < 4 Then
			lItem.subitems.Add(xmlItem.SelectSingleNode("/item/id").InnerText)
			Else
			lItem.subitems.item(3).text = xmlItem.SelectSingleNode("/item/id").InnerText
		End If
		
		
		lvGem1.Items.Item(lItem.Index).Text = GemSlotColorName(xmlItem.SelectSingleNode("/item/gem1").InnerText)
		lvGem1.Items.Item(lItem.Index).BackColor = GemSlotColor(xmlItem.SelectSingleNode("/item/gem1").InnerText)
		lvGem2.Items.Item(lItem.Index).Text = GemSlotColorName(xmlItem.SelectSingleNode("/item/gem2").InnerText)
		lvGem2.Items.Item(lItem.Index).BackColor = GemSlotColor(xmlItem.SelectSingleNode("/item/gem2").InnerText)
		lvGem3.Items.Item(lItem.Index).Text = GemSlotColorName(xmlItem.SelectSingleNode("/item/gem3").InnerText)
		lvGem3.Items.Item(lItem.Index).BackColor = GemSlotColor(xmlItem.SelectSingleNode("/item/gem3").InnerText)
		lvGembonus.Items.Item(lItem.Index).Text = xmlItem.SelectSingleNode("/item/gembonus").InnerText
	End Sub
	
	Sub DisplayGem(id As String,lItem As ListViewItem)
		Dim xmlDB As New Xml.XmlDocument
		dim xmlItem As New Xml.XmlDocument
		xmlDB.Load("gems.xml")
		xmlItem.LoadXml (xmlDB.SelectSingleNode("/gems/item[id="& id &"]").OuterXml)
		lItem.Text = xmlItem.SelectSingleNode("/item/name").InnerText
		lItem.SubItems.Item(2).Text = id
		lItem.BackColor = GemColor(xmlItem.SelectSingleNode("/item/subclass").InnerText)
	End Sub
	
	
	
	
	
	
	Sub LvGem1SelectedIndexChanged(sender As Object, e As EventArgs)
		Dim item As ListViewItem
		Dim lv As ListView
		if sender.SelectedItems.count = 0 then exit sub
		
		Try
			lv  =sender
			item = sender.SelectedItems.Item(0)
			Dim GemSelector As New GemSelector
			GemSelector.LoadItem()
			GemSelector.ShowDialog(Me)
			If GemSelector.DialogResult = DialogResult.OK Then
				DisplayGem(GemSelector.SelectedItem,item)
			End If
			GemSelector.Dispose
			me.Focus
		Catch
			If Err.Number <> 0 Then
				Dim msg As String
				
				Msg = "Error # " & Str(Err.Number) & " was generated by " _
					& Err.Source & ControlChars.CrLf & Err.Description
				debug.Print(Msg)
			End If
			
			
		End Try
	End Sub
	
End Class
