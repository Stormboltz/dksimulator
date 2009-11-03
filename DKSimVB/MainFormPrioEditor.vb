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
	Friend ComboCollection as new Collection
	
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
		
	End Sub
	
	Sub OpenRotaForEdit(Filepath As String)
		Dim doc As New Xml.XmlDocument
		CleanPrioEditorCombo
		LoadAvailableRota
		Doc.Load(Filepath)
		
		dim Nod as Xml.XmlNode
		Dim cmbx As ComboBox
		try
			For Each Nod In Doc.SelectSingleNode("//Rotation/Intro").ChildNodes
				cmbx = CreateCombobox("Intro")
				If Nod.Attributes.GetNamedItem("retry").Value = 0 Then
					cmbx.SelectedItem =Nod.Name & " retry='0'"
				Else
					cmbx.SelectedItem =Nod.Name & " retry='1'"
				End If
				cmbx.BackColor = color.Aqua
			Next
		Catch
		End Try
			
		
		
		For Each Nod In Doc.SelectSingleNode("//Rotation/Rotation").ChildNodes
			cmbx = CreateCombobox("")
			If Nod.Attributes.GetNamedItem("retry").Value = 0 Then
				cmbx.SelectedItem =Nod.Name & " retry='0'"
			Else
				cmbx.SelectedItem =Nod.Name & " retry='1'"
			End If
		Next
		
	End Sub
	
	Sub CleanPrioEditorCombo()
		dim crtl as Control
		For Each crtl In 		ComboCollection
			Me.tbPrioEditor.Controls.Remove 	(crtl)
		Next
		ComboCollection.Clear
	End Sub
	
	
	
End Class
