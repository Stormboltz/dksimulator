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
	Friend EditType as String
	
	
	Sub LoadAvailablePrio
		Dim Doc As new Xml.XmlDocument
		Dim node As XmlNode
		grpAvailablePrio.Controls.Clear
		Doc.Load(Application.StartupPath & "\config\PrioritiesList.xml")
		dim btn as PrioButton
		Dim i As Integer
		on error resume next
		For Each node In doc.SelectSingleNode("//Priorities").ChildNodes
			btn = New PrioButton
			Me.grpAvailablePrio.Controls.Add(btn)
			btn.init(me)
			btn.Top = 10+ btn.Height * i
			btn.SetName(node.Name)
			btn.buttonAdd.Visible = True
			i += 1
		Next
	End Sub
	
	Sub LoadAvailableRota
		Dim Doc As new Xml.XmlDocument
		Dim node As XmlNode
		dim btn as PrioButton
		Dim i As Integer
		grpAvailablePrio.Controls.Clear
		Doc.Load(Application.StartupPath & "\config\RotationList.xml")
		For Each node In doc.SelectSingleNode("//Rotations").ChildNodes
			btn = New PrioButton
			Me.grpAvailablePrio.Controls.Add(btn)
			btn.init(me)
			btn.Top = 10+ btn.Height * i
			i += 1
			btn.SetName(node.Name)
			btn.chkRetry.Visible = True
			btn.buttonAdd.Visible = true
		Next
		
	End Sub
	
	
	Sub OpenIntroForEdit(Filepath As String)
		Dim doc As New Xml.XmlDocument

		Me.grpCurrentPrio.Controls.Clear
		LoadAvailableRota
		Doc.Load(Filepath)
		dim Node as Xml.XmlNode
		dim btn as PrioButton
		Dim i As Integer
		
		For Each Node In Doc.SelectSingleNode("//Intro").ChildNodes
		
			
			btn = New PrioButton
			btn.init(me)
			btn.Top = 10+ btn.Height * i
			btn.SetName (node.Name)
			btn.buttonRemove.Visible = true
			btn.buttonUp.Visible = true
			btn.buttonDown.Visible = True
			btn.chkRetry.Visible = True
			Me.grpCurrentPrio.Controls.Add(btn)
			btn.number = i
			i += 1
			If Node.Attributes.GetNamedItem("retry").Value = 0 Then
				btn.chkRetry.Checked = false
			Else
				btn.chkRetry.Checked = true
			End If
		Next
		EditType = "intro"
	End Sub
	
	
	Sub OpenPrioForEdit(Filepath As String)
		Dim doc As New Xml.XmlDocument

		LoadAvailablePrio
		Me.grpCurrentPrio.Controls.Clear
		
		Doc.Load(Filepath)
		
		dim Node as Xml.XmlNode
		dim btn as PrioButton
		Dim i As Integer

		For Each Node In Doc.SelectSingleNode("//Priority").ChildNodes
			btn = New PrioButton
			btn.init(me)
			btn.Top = 10+ btn.Height * i
			btn.SetName (node.Name)
			btn.buttonRemove.Visible = true
			btn.buttonUp.Visible = true
			btn.buttonDown.Visible = true
			Me.grpCurrentPrio.Controls.Add(btn)
			btn.number = i
			i += 1
		Next
		EditType = "prio"
	End Sub
	
	Sub OpenRotaForEdit(Filepath As String)
		Dim doc As New Xml.XmlDocument
		LoadAvailableRota
		Me.grpCurrentPrio.Controls.Clear
		Doc.Load(Filepath)
		
		dim Node as Xml.XmlNode

		
		dim btn as PrioButton
		Dim i As Integer
		
		For Each Node In Doc.SelectSingleNode("//Rotation/Rotation").ChildNodes
			btn = New PrioButton
			btn.init(me)
			btn.Top = 10+ btn.Height * i
			btn.SetName (node.Name)
			btn.buttonRemove.Visible = true
			btn.buttonUp.Visible = true
			btn.buttonDown.Visible = True
			btn.chkRetry.Visible = True
			Me.grpCurrentPrio.Controls.Add(btn)
			btn.number = i
			i += 1
			If Node.Attributes.GetNamedItem("retry").Value = 0 Then
				btn.chkRetry.Checked = false
			Else
				btn.chkRetry.Checked = true
			End If
		Next
		EditType = "rota"
	End Sub
	
'	Sub CleanPrioEditorCombo()
'		dim crtl as Control
'		For Each crtl In ComboCollection
'			Me.tbPrioEditor.Controls.Remove (crtl)
'		Next
'		ComboCollection.Clear
'	End Sub
	
	
	
	
	Sub CmdSaveRotationClick(sender As Object, e As EventArgs)
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
		Dim i As Integer
		dim p as PrioButton
		For i=0 To grpCurrentPrio.Controls.Count-1
			For Each p In grpCurrentPrio.Controls
				If p.number = i Then
					newElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, p.Name,"")
					Select Case EditType
						Case "prio"
						Case Else
							newAttrib = xmlDoc.CreateNode(xml.XmlNodeType.Attribute,"","retry","")
							If p.chkRetry.Checked Then
								newAttrib.Value = 1
							Else
								newAttrib.Value = 0
							End If
							newElem.Attributes.Append(newAttrib)
					End Select
					root.AppendChild(newElem)
				End If
			Next
		next
		'Save
		xmlDoc.Save(EditorFilePAth)
		loadWindow
		me.tabControl1.SelectedIndex = 0
	End Sub
	
	Sub MoveUp(s As PrioButton)
		Dim x As Integer
		Dim y As Integer
		dim p as PrioButton
		For Each p In Me.grpCurrentPrio.Controls
			If (p.number = s.number-1) And  s.Equals(p)=false Then
				s.number -= 1
				x = s.Left
				y = s.Top
				s.Left = p.Left
				s.Top = p.Top
				p.Left = x
				p.top = y
				p.number += 1
				exit sub
			End If
		Next
	End Sub
	
	Sub MoveDown(s As PrioButton)
		Dim x As Integer
		Dim y As Integer
		dim p as PrioButton
		For Each p In Me.grpCurrentPrio.Controls
			If (p.number = s.number+1) And  s.Equals(p)=false  Then
				s.number += 1
				x = s.Left
				y = s.Top
				s.Left = p.Left
				s.Top = p.Top
				p.Left = x
				p.top = y
				p.number -= 1
				exit sub
			End If
		Next
	End Sub
	
	Sub RemovePrio(s As PrioButton)
		dim p as PrioButton
		For Each p In Me.grpCurrentPrio.Controls
			If (p.number > s.number) Then
				MoveUp(p)
			End If
		Next
		grpCurrentPrio.Controls.Remove(s)
	End Sub
	Sub AddPrio(s As PrioButton)
		dim  i as Integer
		i = Me.grpCurrentPrio.Controls.Count
		
		dim btn as new PrioButton
		btn.init(me)
		btn.Top = 10+ btn.Height * i
		btn.SetName (s.Name)
		
		
		btn.buttonRemove.Visible = true
		btn.buttonUp.Visible = true
		btn.buttonDown.Visible = true
		Me.grpCurrentPrio.Controls.Add(btn)
		btn.number = i
		
		If EditType = "prio" Then
		Else
			btn.chkRetry.Visible = true
		End If
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
