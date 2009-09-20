'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 19/09/2009
' Heure: 15:52
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class TemplateButton
	Inherits System.Windows.Forms.Button
	
	Friend MaxValue As Integer
	Friend Value As Integer
	Friend School as string
	
	Sub init()
		me.Size = New System.Drawing.Size(35, 35)
			me.TextAlign= System.Drawing.ContentAlignment.BottomRight
			me.ForeColor=System.Drawing.Color.White
			me.Font= New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
	End Sub
	
	
	Sub AddPoint
		SetValue (Value +1)
	End Sub
	Sub removePoint
		SetValue(Value -1)
	End Sub
	
	Protected Overloads Overrides Sub OnMouseDown(mevent As MouseEventArgs)
		MyBase.OnMouseDown(mevent)
		If mevent.Button = MouseButtons.Right Then
			removePoint
		Else
			AddPoint
		End If
	End Sub
	
	Sub SetValue(x As Integer)
		on error resume next
		value = X
		If value > MaxValue Then value = Maxvalue
		If Value <= 0 Then
			Value = 0
			me.Image = new Bitmap("images\" & Me.Name & "G.jpg")
		Else
			me.Image = new Bitmap("images\" & Me.Name & ".jpg")
		End If
		me.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.Text = value & "/" & MaxValue
		MainForm.SetTalentPointnumber
	End Sub
	
	
	
	
End Class
