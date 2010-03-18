'
' Created by SharpDevelop.
' User: Fabien
' Date: 30/12/2009
' Time: 17:28
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class PrioButton
	Inherits System.Windows.Forms.GroupBox
	
	friend button as System.Windows.Forms.Button
	Friend lbl As System.Windows.Forms.Label
	
	Friend buttonAdd As  new System.Windows.Forms.Button
	Friend buttonRemove As new System.Windows.Forms.Button
	Friend buttonUp As new  System.Windows.Forms.Button
	Friend buttonDown As New System.Windows.Forms.Button
	Friend chkRetry As New System.Windows.Forms.CheckBox
	
	
	protected Mainfrm as MainForm 
	
	Friend number as Integer
	
	
	Sub init(m As MainForm)
		Mainfrm = m
		
		
		Me.Height = 45
		Me.Width = 200
		number = 0
		
		button = New Button
		lbl = New Label
		
		
		Me.Controls.Add(button)
		Me.Controls.Add(lbl)
		
		Me.Controls.Add(buttonAdd)
		Me.Controls.Add(buttonRemove)
		Me.Controls.Add(buttonUp)
		Me.Controls.Add(buttonDown)
		Me.Controls.Add(chkRetry)
		
		
		button.Size = New System.Drawing.Size(40, 40)
		button.TextAlign= System.Drawing.ContentAlignment.BottomRight
		button.ForeColor=System.Drawing.Color.White
		button.Font= New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		button.Location = New System.Drawing.Point(5, 5)
		
		lbl.Enabled = false
		lbl.Location = New System.Drawing.Point(45, 25)
		lbl.Size = New System.Drawing.Size(20, 20)
		lbl.AutoSize = true
		lbl.TabIndex = 30
		lbl.Text = "BloodStrike"
		
		With chkRetry
			.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
			.Location = New System.Drawing.Point(45, 5)
			.Text  = "Retry/wait"
			.Size = New System.Drawing.Size(50, 20)
			.Checked= False
			.Visible = false
			.AutoSize = true
		End With
		
		
		
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
		me.button.Image = new Bitmap("images\spell\" & name & ".jpg")
		me.button.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.lbl.Text = Name
		me.Name = name
	End Sub
	
	Sub buttonAddclick(sender As Object, e As EventArgs)
		Mainfrm.AddPrio(me)
	End Sub
	Sub buttonRemoveclick(sender As Object, e As EventArgs)
		Mainfrm.RemovePrio(me)
	End Sub
	Sub buttonUpclick(sender As Object, e As EventArgs)
		Mainfrm.MoveUp(me)
	End Sub
	Sub buttonDownclick(sender As Object, e As EventArgs)
		Mainfrm.MoveDown(me)
	End Sub
End Class
