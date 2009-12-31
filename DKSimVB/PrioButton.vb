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
	Friend buttonDown As new System.Windows.Forms.Button
	
	
	Sub init()
		Me.Height = 40
		Me.Width = 200
		
		button = New Button
		lbl = New Label
		
		
		Me.Controls.Add(button)
		Me.Controls.Add(lbl)
		
		Me.Controls.Add(buttonAdd)
		Me.Controls.Add(buttonRemove)
		Me.Controls.Add(buttonUp)
		Me.Controls.Add(buttonDown)
		
		
		
		button.Size = New System.Drawing.Size(35, 35)
		button.TextAlign= System.Drawing.ContentAlignment.BottomRight
		button.ForeColor=System.Drawing.Color.White
		button.Font= New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		button.Location = New System.Drawing.Point(5, 5)
		
		lbl.Enabled = false
		lbl.Location = New System.Drawing.Point(40, 20)
		lbl.Size = New System.Drawing.Size(100, 20)
		lbl.TabIndex = 30
		lbl.Text = "BloodStrike"
		
		
		buttonAdd.Size = New System.Drawing.Size(30, 18)
		buttonAdd.TextAlign= System.Drawing.ContentAlignment.MiddleLeft
		'buttonAdd.ForeColor=System.Drawing.Color.White
		buttonAdd.Font= New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		buttonAdd.Location = New System.Drawing.Point(120, 5)
		buttonAdd.Text = "+"
		
		buttonRemove.Size = New System.Drawing.Size(30, 18)
		buttonRemove.TextAlign= System.Drawing.ContentAlignment.MiddleLeft
		'buttonRemove.ForeColor=System.Drawing.Color.White
		buttonRemove.Font= New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		buttonRemove.Location = New System.Drawing.Point(120, 20)
		buttonRemove.Text = "-"
		
		buttonUp.Size = New System.Drawing.Size(30, 18)
		buttonUp.TextAlign= System.Drawing.ContentAlignment.MiddleLeft
		'buttonUp.ForeColor=System.Drawing.Color.White
		buttonUp.Font= New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		buttonUp.Location = New System.Drawing.Point(170, 5)
		buttonUp.Text = "DO"
		
		buttonDown.Size = New System.Drawing.Size(30, 18)
		buttonDown.TextAlign= System.Drawing.ContentAlignment.MiddleLeft
		'buttonDown.ForeColor=System.Drawing.Color.White
		buttonDown.Font= New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		buttonDown.Location = New System.Drawing.Point(170, 20)
		buttonDown.Text = "UP"
		
		
	End Sub
	
End Class
