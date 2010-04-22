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
	Friend txtLength as New System.Windows.Forms.TextBox
	
	
	
	
	
	Protected Mainfrm As ScenarioEditor
	
	
	Friend number as Integer
	
	
	Sub New()
		MyBase.New
		Me.Height = 145
		Me.Width = 200
		
	End Sub
	
	
	Sub init(m As ScenarioEditor)
		Mainfrm = m
		number = 0
		InitCommonControls
				
		Me.Controls.Add(chkCanTakePetDamage)
		With chkCanTakePetDamage
			.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
			.Location = New System.Drawing.Point(10, 10)
			.Size = New System.Drawing.Size(50, 20)
			.Checked= False
			.AutoSize = true
			.Text  = "Can Take Pet Damage"
		End With
		
		Me.Controls.Add(chkCanTakePlayerStrike)
		With chkCanTakePlayerStrike
			.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
			.Location = New System.Drawing.Point(10, 35)
			.Size = New System.Drawing.Size(50, 20)
			.Checked= False
			.AutoSize = true
			.Text  = "Can Take Player Strike"
		End With
		
		
		Me.Controls.Add(chkCanTakeDiseaseDamage)
		With chkCanTakeDiseaseDamage
			.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
			.Location = New System.Drawing.Point(10, 60)
			.Size = New System.Drawing.Size(50, 20)
			.Checked= False
			.AutoSize = true
			.Text  = "Can Take Disease Damage"
		End With
		
		
		
		Me.Controls.Add(txtStart)
		With txtStart
			.Location = New System.Drawing.Point(10, 85)
			.Size = New System.Drawing.Size(50, 20)
			.Text = "60"
		End With
		
		Dim lbl As Label
		lbl = New Label
		Me.Controls.Add(lbl)
		With lbl
			.Location = New System.Drawing.Point(txtStart.Left + txtStart.Width + 10, txtStart.Top)
			.Size = New System.Drawing.Size(50, 20)
			.Text = "60"
			.Text = "Start time (in second)"
			.AutoSize = True
		End With
		
		Me.Controls.Add(txtLength)
		With txtLength
			.Location = New System.Drawing.Point(10, 110)
			.Size = New System.Drawing.Size(50, 20)
			.Text = "10"
		End With
		
		lbl = New Label
		Me.Controls.Add(lbl)
		With lbl
			.Location = New System.Drawing.Point(txtLength.Left + txtLength.Width + 10, txtLength.Top)
			.Size = New System.Drawing.Size(50, 20)
			.Text = "During (in second)"
			.AutoSize = True
		End With
		
		
		
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
'		
'		button.Size = New System.Drawing.Size(40, 40)
'		button.TextAlign= System.Drawing.ContentAlignment.BottomRight
'		button.ForeColor=System.Drawing.Color.White
'		button.Font= New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
'		button.Location = New System.Drawing.Point(5, 5)
'		
'		lbl.Enabled = false
'		lbl.Location = New System.Drawing.Point(45, 25)
'		lbl.Size = New System.Drawing.Size(20, 20)
'		lbl.AutoSize = true
'		lbl.TabIndex = 30
'		lbl.Text = "BloodStrike"
		
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
	
	Sub SetName(name As String)
		on error resume next
		
		Me.Text = Name
		me.Name = name
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
End Namespace
