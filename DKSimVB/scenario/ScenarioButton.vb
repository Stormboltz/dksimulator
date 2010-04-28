'
' Created by SharpDevelop.
' User: Fabien
' Date: 30/12/2009
' Time: 17:28
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Scenarios
Friend Class ElementButton
	Inherits System.Windows.Forms.GroupBox
	
'	friend button as System.Windows.Forms.Button
'	Friend lbl As System.Windows.Forms.Label
'
	Friend buttonAdd As  new System.Windows.Forms.Button
	Friend buttonRemove As new System.Windows.Forms.Button
	Friend buttonUp As new  System.Windows.Forms.Button
	Friend buttonDown As New System.Windows.Forms.Button
	
	Friend chkCanTakePetDamage as New System.Windows.Forms.CheckBox
	Friend chkCanTakePlayerStrike As New System.Windows.Forms.CheckBox
	Friend chkCanTakeDiseaseDamage As New System.Windows.Forms.CheckBox
	Friend txtStart As New System.Windows.Forms.TextBox
	Friend txtLength As New System.Windows.Forms.TextBox
	
	Friend colControl As New Collection
	
	Friend XmlBuid as Xml.XmlNode
	
	
	
	
	
	Protected Mainfrm As ScenarioEditor
	
	
	Friend number as Integer
	
	
	Sub New()
		MyBase.New
		Me.Height = 145
		Me.Width = 200
		
	End Sub
	Sub build(xElement As Xml.XmlNode)
		Dim N As Xml.XmlNode
		Dim xDoc As new Xml.XmlDocument
		Dim x As Integer = 10
		Dim y As Integer = 15
		
		
		Dim chk As CheckBox
		Dim tBox As myTextButton
		dim lbl as Label
		
		
		xDoc.LoadXml(xElement.OuterXml)
		XmlBuid = xElement
		me.SetName(xDoc.ChildNodes.Item(0).Attributes.GetNamedItem("caption").InnerText)
		For Each N In xDoc.ChildNodes.Item(0).ChildNodes
			If N.Attributes.GetNamedItem("type").InnerText = "checkbox" Then
				chk = New CheckBox
				Me.Controls.Add(chk)
				With chk
					.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
					.Location = New System.Drawing.Point(x, y)
					.Size = New System.Drawing.Size(50, 20)
					Try
						.Checked = N.InnerText
					Catch
						.Checked =false
					End Try
					
					.AutoSize = true
					.Text = N.Attributes.GetNamedItem("caption").InnerText
					.Name = N.Name
				End With
				colControl.Add(chk)
			End If
			
			If N.Attributes.GetNamedItem("type").InnerText = "textbox" Then
				tBox = New myTextButton
				Me.Controls.Add(tBox)
				With tBox
					.Location = New System.Drawing.Point(x, y)
					.Size = New System.Drawing.Size(50, 20)
					Try
						.Text = Int(N.InnerText)
					Catch
						.Text  = 0
					End Try
					
					.multi = N.Attributes.GetNamedItem("multi").InnerText
					.caption = N.Attributes.GetNamedItem("caption").InnerText
					.Name = N.Name
				End With
				lbl = New Label
				Me.Controls.Add(lbl)
				With lbl
					.Location = New System.Drawing.Point(tBox.Left + tBox.Width + x, y)
					.Size = New System.Drawing.Size(50, 20)
					.AutoSize = true
					.Text = N.Attributes.GetNamedItem("caption").InnerText
					.Name = "lbl" & N.Name
				End With
				colControl.Add(tBox)
			End If
			y += 25
		Next
	End Sub
	
	Sub init(m As ScenarioEditor)
		
		Mainfrm = m
		number = 0
		InitCommonControls
	End Sub
	
	
	Sub InitCommonControls
		'button = New Button
		'lbl = New Label
		
		'Me.Controls.Add(button)
		'Me.Controls.Add(lbl)
		Me.Controls.Add(buttonAdd)
		Me.Controls.Add(buttonRemove)
		Me.Controls.Add(buttonUp)
		Me.Controls.Add(buttonDown)
		
		With buttonAdd
			.Size = New System.Drawing.Size(20, 20)
			.TextAlign= System.Drawing.ContentAlignment.MiddleLeft
			.Font= New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
			.Location = New System.Drawing.Point(140, 5)
			.Text = ""
			.Image = new Bitmap("images\icons\add.jpg")
			.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
			AddHandler .Click, AddressOf Me.buttonAddclick
			.Visible = false
		End With
		
		With buttonRemove
			.Size = New System.Drawing.Size(20, 20)
			.TextAlign= System.Drawing.ContentAlignment.MiddleLeft
			.Font= New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
			.Location = New System.Drawing.Point(140, 25)
			.Text = ""
			.Image = new Bitmap("images\icons\remove.jpg")
			.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
			AddHandler .Click, AddressOf Me.buttonRemoveclick
			.Visible = false
		End With
		With buttonUp
			.Size = New System.Drawing.Size(20, 20)
			.TextAlign= System.Drawing.ContentAlignment.MiddleLeft
			.Font= New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
			.Location = New System.Drawing.Point(170, 5)
			.Image = new Bitmap("images\icons\up.jpg")
			.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
			AddHandler .Click, AddressOf Me.buttonUpclick
			.Visible = false
		End With
		
		With buttonDown
			.Size = New System.Drawing.Size(20, 20)
			.TextAlign= System.Drawing.ContentAlignment.MiddleLeft
			.Font= New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
			.Location = New System.Drawing.Point(170, 25)
			.Text = ""
			.Image = new Bitmap("images\icons\down.jpg")
			.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
			AddHandler .Click, AddressOf Me.buttonDownclick
			.Visible = false
		End With
	End Sub
	
	Sub SetName(n As String)
		on error resume next
		Me.Text = n
		me.Name = n
	End Sub
	
	Sub buttonAddclick(sender As Object, e As EventArgs)
		Mainfrm.AddElement(me)
	End Sub
	Sub buttonRemoveclick(sender As Object, e As EventArgs)
		Mainfrm.RemoveElement(me)
	End Sub
	Sub buttonUpclick(sender As Object, e As EventArgs)
		Mainfrm.MoveUp(me)
	End Sub
	Sub buttonDownclick(sender As Object, e As EventArgs)
		Mainfrm.MoveDown(me)
	End Sub
	
	
End Class

Friend Class myTextButton
	Inherits System.Windows.Forms.TextBox
		Friend multi As Integer
		Friend caption As String

	End Class


End Namespace
