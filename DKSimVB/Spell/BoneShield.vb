'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 24/09/2009
' Heure: 21:55
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class BoneShield
	Inherits Spells.Spell
	Friend Charge As Integer
	Friend previousFade As Long
	
	Function BuffLength() as Integer
		Return 300
	End Function
	
	
	
	Sub New(MySim as Sim)
		sim = MySim
	End Sub
	
	Sub UseCharge(T as Long)
		Charge = Charge -1
		If Charge = 0 Then
			Me.ActiveUntil = T
			RemoveUptime(T)
			Charge = 0
		End If
	End Sub
	
	Sub PreBuff
		if sim.TalentUnholy.BoneShield = 1 then
			Me.CD = 60*100
			Me.ActiveUntil = sim.TimeStamp + BuffLength*100
			AddUptime(sim.TimeStamp)
		end if
	End Sub
	Public Overloads Overrides Sub Init()
		MyBase.Init()
		
	End Sub
	
	
	Function Use(T as Long) As Boolean
		If sim.TalentUnholy.BoneShield = 0 Then Return False
		If sim.runes.Unholy(T) = False Then
			If sim.BloodTap.IsAvailable(T) Then
				sim.BloodTap.Use(T)
			Else
				return false
			End If
		End If
		
		If sim.KeepBloodSync Then
			If sim.BloodToSync = True Then
				sim.BloodToSync  = False
			Else
				sim.BloodToSync  = true
			End If
		End If
		
		
		me.CD = T + 60*100
		Me.ActiveUntil = T + BuffLength*100
		sim.runes.UseDeathBlood(T,true)
		UseGCD(T)
		sim.RunicPower.add(10)
		sim.combatlog.write(T  & vbtab &  "Bone Shield")
		Charge = 3
		If sim.Glyph.BoneShield Then Charge += 1
		HitCount += 1
		AddUptime(T)
	End Function
	
	Function IsAvailable(T As Long) As Boolean
		If sim.TalentUnholy.BoneShield =0 Then Return False
		If ActiveUntil > T Then Return False
		if sim.BloodTap.IsAvailable(T)=false Then Return false
		If CD > T Then Return False
		return true
	End Function
	
	Function Value(T As Long) As integer
		If ActiveUntil > T Then
			Return 1
		Else
			Return 0
		End If
	End Function

	Sub AddUptime(T As Long)
		dim tmp as Long
		If ActiveUntil > sim.NextReset Then
			tmp = (sim.NextReset - T)
		Else
			tmp = ActiveUntil - T
		End If
		
		If previousfade < T  Then
		 	uptime += tmp
		Else
			uptime += tmp - (previousFade-T)
		End If
		previousFade = T + tmp
	End Sub
	
	Sub RemoveUptime(T As Long)
		If previousfade < T  Then
		Else
			uptime -= (previousFade-T)
		End If
		previousFade = T
	End Sub

	
	
End Class
