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
		value = X
		If value > MaxValue Then value = Maxvalue
		If Value < 0 Then Value = 0
		me.Text = value
	End Sub
	
	
End Class
