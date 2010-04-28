'
' Created by SharpDevelop.
' User: Fabien
' Date: 28/03/2009
' Time: 18:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Scenarios
	Friend Partial Class ScenarioEditor
		Friend Sub New()
			' The Me.InitializeComponent call is required for Windows Forms designer support.
			Me.InitializeComponent()
		End Sub
		Friend EditorFilePAth as String = Application.StartupPath & "\scenario\Scenario.xml"
		
		
		Sub BtOkClick(sender As Object, e As EventArgs)
			me.DialogResult = DialogResult.ok
		End Sub
		
		
		Sub LoadAvailableScenario
			Dim i As Integer
			dim btn as ElementButton
			
			Dim xDoc As new Xml.XmlDocument
			xDoc.Load(application.StartupPath & "\Config\Scenarios.xml")
			dim xNode as Xml.XmlNode
			For Each xNode In xDoc.SelectNodes("/Scenarios/Element")
				btn = New ElementButton
				Me.grpAvailableScenario.Controls.Add(btn)
				btn.init(me)
				'btn.InitCommonControls
				btn.build(xNode)
				btn.Top = 10+ btn.Height * i
				btn.buttonAdd.Visible = True
				i += 1
			Next
			
			
			
		End Sub
		
		Sub OpenForEdit(FilePath As String)
			EditorFilePAth = FilePath
			Dim doc As New Xml.XmlDocument
			LoadAvailableScenario
			Me.grpCurrentScenario.Controls.Clear
			Doc.Load(Filepath)
			dim Node as Xml.XmlNode
			dim btn as ElementButton
			Dim i As Integer
			
			For Each Node In doc.SelectNodes("/Scenario/Element")
				btn = New ElementButton
				Me.grpCurrentScenario.Controls.Add(btn)
				btn.init(me)
				btn.build(Node)
				btn.Top = 10+ btn.Height * i
				btn.buttonRemove.Visible = true
				btn.buttonUp.Visible = true
				btn.buttonDown.Visible = true
				btn.buttonAdd.Visible = False
				btn.number = i
				i += 1
			Next
			
		End Sub
		
		Sub MoveUp(s as ElementButton)
			Dim x As Integer
			Dim y As Integer
			dim p as ElementButton
			For Each p In Me.grpCurrentScenario.Controls
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
		
		Sub MoveDown(s As ElementButton)
			Dim x As Integer
			Dim y As Integer
			dim p as ElementButton
			For Each p In Me.grpCurrentScenario.Controls
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
		
		Sub RemoveElement(s As ElementButton)
			dim p as ElementButton
			For Each p In Me.grpCurrentScenario.Controls
				If (p.number > s.number) Then
					MoveUp(p)
				End If
			Next
			grpCurrentScenario.Controls.Remove(s)
		End Sub
		Sub AddElement(s As ElementButton)
			dim  i as Integer
			i = Me.grpCurrentScenario.Controls.Count
			dim btn as new Scenarios.ElementButton
			btn.init(Me)
			btn.build(s.XmlBuid)
			btn.Top = 10+ btn.Height * i
			btn.SetName (s.Name)
			btn.buttonRemove.Visible = true
			btn.buttonUp.Visible = true
			btn.buttonDown.Visible = true
			Me.grpCurrentScenario.Controls.Add(btn)
			btn.number = i
		End Sub
		
		
		Sub CmdSaveClick(sender As Object, e As EventArgs)
			Dim xmlDoc As new Xml.XmlDocument
			Dim root As xml.XmlElement
			xmlDoc.LoadXml("<Scenario></Scenario>")
			root = xmlDoc.DocumentElement
			Dim newElem As xml.XmlNode
			dim PArentElem as Xml.XmlNode
			Dim i As Integer
			Dim p As ElementButton
			dim xAt as Xml.XmlAttribute
			
			For Each p In grpCurrentScenario.Controls
				PArentElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, "Element","")
				xAt = xmlDoc.CreateAttribute("id")
				xAt.InnerText = i
				PArentElem.Attributes.Append(xAt)
				
				xAt = xmlDoc.CreateAttribute("caption")
				xAt.InnerText = p.Text
				PArentElem.Attributes.Append(xAt)
				
				root.AppendChild(PArentElem)
				
				Dim ctr As Control
				Dim chk As CheckBox
				Dim txt As myTextButton
				
				
				
				For Each ctr In p.colControl
					If TypeOf ctr Is CheckBox Then
						chk = ctr
						newElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, chk.Name,"")
						xAt = xmlDoc.CreateAttribute("type")
						xAt.InnerText = "checkbox"
						newElem.Attributes.Append(xAt)
						xAt = xmlDoc.CreateAttribute("caption")
						xAt.InnerText = chk.Text
						newElem.Attributes.Append(xAt)
						newElem.InnerText = chk.Checked
						PArentElem.AppendChild(newElem)
					End If
					
					If TypeOf ctr Is myTextButton Then
						txt = ctr
						newElem = xmlDoc.CreateNode(xml.XmlNodeType.Element, txt.Name,"")
						xAt = xmlDoc.CreateAttribute("type")
						xAt.InnerText = "textbox"
						newElem.Attributes.Append(xAt)
						
						xAt = xmlDoc.CreateAttribute("caption")
						xAt.InnerText = txt.caption
						newElem.Attributes.Append(xAt)
						
						xAt = xmlDoc.CreateAttribute("multi")
						xAt.InnerText = txt.multi
						newElem.Attributes.Append(xAt)
						newElem.InnerText = txt.Text
						PArentElem.AppendChild(newElem)
					End If
				Next
				
				i += 1
			next
			'Save
			xmlDoc.Save(EditorFilePAth)
		End Sub
		
		Sub CmdSaveAsNewClick(sender As Object, e As EventArgs)
			Dim truc As New Form1
			Dim res As DialogResult
			res = truc.ShowDialog
			If truc.textBox1.Text  <> "" And res = DialogResult.OK Then
				EditorFilePAth = application.StartupPath & "\Scenario\" & truc.textBox1.Text & ".xml"
				CmdSaveClick(sender, e)
			Else
				exit sub
			End If
			truc.Dispose
			RefreshScenarioList
		End Sub
		
		Sub ScenarioEditorLoad(sender As Object, e As EventArgs)
			RefreshScenarioList
		End Sub
		
		Sub CmdLoadClick(sender As Object, e As EventArgs)
			OpenForEdit(Application.StartupPath & "\scenario\" & cmbScenario.SelectedItem.ToString )
		End Sub
		
		Sub RefreshScenarioList()
			Dim sTemp As String
			Dim item As String
			
			try
				sTemp = cmbScenario.SelectedItem.ToString
			Catch
			End Try
			
			cmbScenario.Items.Clear
			For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\scenario\")
				cmbScenario.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ) )
			Next
			cmbScenario.SelectedItem = sTemp
			
		End Sub
		
		
	End Class
End Namespace
