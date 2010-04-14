'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 22/09/2009
' Heure: 16:09
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Runes
	
Public Class rune
	Inherits Supertype
	
	Friend reserved As Boolean
	Friend death As Boolean
	Friend BTuntil as Long
	'Protected Sim As Sim
	
	Friend AvailableTime As Long
    
    Sub SetAvailableTime(T As Long)
    	If AvailableTime > 0 Then
    		If AvailableTime > T Then Uptime -= (Math.Min(AvailableTime, Sim.NextReset) - T)
    	End If
    	AvailableTime = T
    End Sub

	Sub Reset()
		AvailableTime = 0
		Death = False
		Reserved = False
	End Sub
	
	
	Sub New(S As Sim)
		Sim = S
		Reset()
	End Sub
	
	Sub Use(T As Long, D As Boolean)
		dim NextAvailable as Long
		death = D
		HitCount+=1
		if BTuntil > T then death = true
		If AvailableTime <> 0 Then
			If T - AvailableTime <= 200 Then
				NextAvailable = AvailableTime + RuneRefreshtime
			Else
			'	Uptime += T - AvailableTime - 200
				NextAvailable = T + 800
			End If
		else
			NextAvailable = T + RuneRefreshtime
		End If
		uptime += RuneRefreshTime
		If NextAvailable > Sim.NextReset Then
			uptime -= NextAvailable - Sim.NextReset
		End If

		AvailableTime = NextAvailable
		
		sim.proc.tryT104PDPS(T)
		sim.FutureEventManager.Add(AvailableTime,"Rune")
	End Sub

	Function RuneRefreshtime As Integer
		If sim.UnholyPresence Then
			return 1000 - 50*sim.talentunholy.ImprovedUnholyPresence
		Else
			return 1000
		End If
	End Function
	Function Available(T as Long) As Boolean
		If AvailableTime <= T  Then Return True
		return false
	End Function
End Class
End Namespace
