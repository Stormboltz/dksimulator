'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 24/09/2009
' Heure: 23:21
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'




Public Class Frenzy
	Inherits Spells.Spell
	
	Sub New(s as Sim)
		MyBase.New(s)
	End Sub
	
	
	Function IsFrenzyAvailable(T As Long) As Boolean
		if sim.TalentUnholy.GhoulFrenzy = 0 then return false
		If CD < T  And sim.runes.Unholy(T) Then Return True
	End Function
	
	Function IsAutoFrenzyAvailable(T As Long) As Boolean
		If sim.TalentUnholy.GhoulFrenzy = 0 Then Return False
		If sim.Rotate = True Then
			If sim.Rotation.MyRotation.Contains("GhoulFrenzy") Then Return False
		Else
			if sim.Priority.prio.Contains("GhoulFrenzy") then return false
		End If
		
		
		If CD < T  Then 
			If sim.runes.Unholy(T)=False Then 
				If sim.runes.Blood(T) = False Then
					if sim.BloodTap.IsAvailable(T) Then Return True
				End If
			End If
		 End If
	End Function
	
	Function Frenzy(T As Long) As Boolean
		If sim.runes.Unholy(T) = False Then
			If sim.BloodTap.IsAvailable(T) Then
				sim.BloodTap.Use(T)
			Else
				return false
			End If
		End If
		UseGCD(T)
		sim.runes.UseDeathBlood(T,True)
		Sim.RunicPower.add(10)
		CD = T+3000
		me.ActiveUntil = T+3000
		If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "Using Ghoul Frenzy")
		HitCount =  HitCount +1
		return true
	End Function
	
	
End Class
