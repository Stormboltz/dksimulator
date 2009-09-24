'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 22/09/2009
' Heure: 16:09
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class rune
	Friend reserved As Boolean
	Friend death As Boolean
	Friend AvailableTime As Long
	Friend BTuntil as Long
	Protected Sim As Sim
	
	Sub New(S As Sim)
		Me.reserved=False
		Me.death=False
		Me.AvailableTime=0
		Sim = S
	End Sub
	Sub Use(T As Long, D As Boolean)
		death = D
		if BTuntil > T then death = true
		If T - AvailableTime <= 300 and AvailableTime <> 0 Then
			AvailableTime = AvailableTime + RuneRefreshtime
		Else
			AvailableTime = T + RuneRefreshtime
		End If
	End Sub

	Function RuneRefreshtime As Integer
		If sim.mainstat.UnholyPresence Then
			return 1000 - 50*talentunholy.ImprovedUnholyPresence
		Else
			return 1000
		End If
	End Function
	Function Available(T as Long) As Boolean
		If AvailableTime <= T  Then Return True
		return false
	End Function	
End Class
