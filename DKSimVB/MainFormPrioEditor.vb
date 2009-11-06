'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 07/10/2009
' Heure: 19:22
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Imports System.Xml
Public Partial Class MainForm
	Friend AvailablePrio As new Collection
	Friend ComboCollection As New Collection
	Friend EditType as String
	
	
	
	
	Sub FillThisComboBox(cmb As ComboBox)
		dim i as Integer
		For i = 1 To AvailablePrio.Count
			cmb.Items.Add (AvailablePrio.Item(i))
		Next
		
		
		
	End Sub

	Function CreateCombobox( Tag as String) As Windows.Forms.ComboBox
		Dim tmpCMB As New ComboBox
		ComboCollection.Add(tmpCMB)
		Me.tbPrioEditor.Controls.Add(tmpCMB)
		tmpCMB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		tmpCMB.FormattingEnabled = true
		tmpCMB.Location = New System.Drawing.Point(25, 15 + 25*ComboCollection.Count )
		tmpCMB.Name = "cmbPrio" & ComboCollection.Count-1
		tmpCMB.Size = New System.Drawing.Size(250, 21)
		tmpCMB.DropDownStyle = ComboBoxStyle.DropDownList
		FillThisComboBox (tmpCMB)
		tmpCMB.Tag = Tag
		tmpCMB.Sorted = true
		return tmpCMB
	End Function
	
	Sub LoadAvailablePrio
		Dim Doc As new Xml.XmlDocument
		dim node as XmlNode
		AvailablePrio.Clear
		Doc.Load("PrioritiesList.xml")
		For Each node In doc.SelectSingleNode("//Priorities").ChildNodes
			AvailablePrio.Add(node.Name)
		Next
		
	End Sub
	
	Sub LoadAvailableRota
		Dim Doc As new Xml.XmlDocument
		dim node as XmlNode
		AvailablePrio.Clear
		Doc.Load("RotationList.xml")
		For Each node In doc.SelectSingleNode("//Rotations").ChildNodes
			AvailablePrio.Add(node.Name & " retry='0'")
			AvailablePrio.Add(node.Name & " retry='1'")
		Next
		
	End Sub
	
	
	Sub OpenIntroForEdit(Filepath As String)
		Dim doc As New Xml.XmlDocument
		CleanPrioEditorCombo
		LoadAvailableRota
		Doc.Load(Filepath)
		dim Nod as Xml.XmlNode
		dim cmbx as ComboBox
		For Each Nod In Doc.SelectSingleNode("//Intro").ChildNodes
			cmbx = CreateCombobox("")
			If Nod.Attributes.GetNamedItem("retry").Value = 0 Then
				cmbx.SelectedItem =Nod.Name & " retry='0'"
			Else
				cmbx.SelectedItem =Nod.Name & " retry='1'"
			End If
		Next
		EditType = "intro"
	End Sub
	
	
	Sub OpenPrioForEdit(Filepath As String)
		Dim doc As New Xml.XmlDocument
		CleanPrioEditorCombo
		LoadAvailablePrio
		Doc.Load(Filepath)
		
		dim Nod as Xml.XmlNode
		dim cmbx as ComboBox
		For Each Nod In Doc.SelectSingleNode("//Priority").ChildNodes
			cmbx = CreateCombobox("")
			cmbx.SelectedItem =Nod.Name
		Next
		EditType = "prio"
	End Sub
	
	Sub OpenRotaForEdit(Filepath As String)
		Dim doc As New Xml.XmlDocument
		CleanPrioEditorCombo
		LoadAvailableRota
		Doc.Load(Filepath)
		
		dim Nod as Xml.XmlNode
		Dim cmbx As ComboBox
		
		For Each Nod In Doc.SelectSingleNode("//Rotation/Rotation").ChildNodes
			cmbx = CreateCombobox("")
			If Nod.Attributes.GetNamedItem("retry").Value = 0 Then
				cmbx.SelectedItem =Nod.Name & " retry='0'"
			Else
				cmbx.SelectedItem =Nod.Name & " retry='1'"
			End If
		Next
		EditType = "rota"
	End Sub
	
	Sub CleanPrioEditorCombo()
		dim crtl as Control
		For Each crtl In 		ComboCollection
			Me.tbPrioEditor.Controls.Remove 	(crtl)
		Next
		ComboCollection.Clear
	End Sub
	
	
	
	
	Sub CmdAddPrioItemClick(sender As Object, e As EventArgs)
		CreateCombobox("")
	End Sub
	
	
	
	
	Sub CmdSaveRotationClick(sender As Object, e As EventArgs)
		Dim cmb As object
		Dim xmlDoc As new Xml.XmlDocument
		dim newAttrib as Xml.XmlAttribute
		Dim root As xml.XmlElement
		
		Select Case EditType
			Case "prio"
				xmlDoc.LoadXml("<Priority></Priority>")
				root = xmlDoc.DocumentElement
			Case "rota"
				xmlDoc.LoadXml("<Rotation><Rotation></Rotation></Rotation>")
				root = xmlDoc.SelectSingleNode("/Rotation/Rotation")
			Case "intro"
				xmlDoc.LoadXml("<Intro></Intro>")
				root = xmlDoc.SelectSingleNode("/Intro")
		End Select
		
		Dim newElem As xml.XmlNode
		dim sTmp as String
		dim i as Integer
		For i=1 To ComboCollection.Count
			cmb = ComboCollection(i)
			If instr(cmb.SelectedItem,"None") or cmb.SelectedItem="" Then
			Else
				
				If instr(cmb.SelectedItem, " retry='0'") Then
					sTmp = replace(cmb.SelectedItem," retry='0'","")
					sTmp = trim(sTmp)
					newElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, sTmp,"")
					newAttrib = xmlDoc.CreateNode(xml.XmlNodeType.Attribute,"","retry","")
					newAttrib.Value = 0
					newElem.Attributes.Append(newAttrib)
				Else If instr(cmb.SelectedItem, " retry='1'") Then
					sTmp = replace(cmb.SelectedItem," retry='1'","")
					sTmp = trim(sTmp)
					newElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, sTmp,"")
					newAttrib = xmlDoc.CreateNode(xml.XmlNodeType.Attribute,"","retry","")
					newAttrib.Value = 1
					newElem.Attributes.Append(newAttrib)
				Else
					newElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, cmb.SelectedItem,"")
				End If
				root.AppendChild(newElem)
			end if
		Next
			
		
		'Save
		xmlDoc.Save(EditorFilePAth)
		loadWindow
		me.tabControl1.SelectedIndex = 0
	End Sub
	
	
	
	
	
	Sub CmdSaveRotationAsNewClick(sender As Object, e As EventArgs)
		Dim truc As New Form1
		Dim res As DialogResult
		res = truc.ShowDialog
		If truc.textBox1.Text  <> "" and res = DialogResult.OK Then
			EditorFilePAth = Strings.Left(EditorFilePAth,strings.InStrRev(EditorFilePAth,"\")) & truc.textBox1.Text & ".xml"
			CmdSaveRotationClick(sender, e)
		Else
			exit sub
		End If
		truc.Dispose
	End Sub
End Class
