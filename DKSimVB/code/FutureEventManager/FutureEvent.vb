'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 11/04/2010
' Heure: 12:11
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class FutureEvent
	Friend T As Long
	Friend Ev As String
	
	Sub New (Timestamp As Long, EventName As String)
		Me.T = Timestamp
		me.Ev = EventName
	End Sub
	
	
	
End Class
