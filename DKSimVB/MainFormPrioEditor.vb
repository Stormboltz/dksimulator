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

	Function CreateCombobox() As Windows.Forms.ComboBox
		Dim tmpCMB As New ComboBox
		ComboCollection.Add(tmpCMB)
		Me.tbPrioEditor.Controls.Add(tmpCMB)
		tmpCMB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		tmpCMB.FormattingEnabled = true
		tmpCMB.Location = New System.Drawing.Point(25, 15 + 25*ComboCollection.Count )
		tmpCMB.Name = "cmbPrio" & ComboCollection.Count-1
		tmpCMB.Size = New System.Drawing.Size(250, 21)
		FillThisComboBox (tmpCMB)
		return tmpCMB
	End Function
	
	Sub LoadAvailablePrio
		Dim Doc As new Xml.XmlDocument
		dim node as XmlNode
		
		Doc.Load("PrioritiesList.xml")
		For Each node In doc.SelectSingleNode("//Priorities").ChildNodes
			AvailablePrio.Add(node.Name)
		Next
	End Sub
	
End Class
