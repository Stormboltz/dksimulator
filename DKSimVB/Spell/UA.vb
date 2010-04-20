'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class UnbreakableArmor
	Inherits Spells.Spell
	Friend previousFade as Long
	
	
Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	
	
	Function IsAvailable(T As Long) As Boolean
		If sim.TalentFrost.UnbreakableArmor = 0 Then return false 
		If CD >= T Then Return False
		If sim.BloodTap.IsAvailable(T) and sim.Runes.Frost(T)=false Then return true
	End Function
	Function Use(T As Long) As Boolean
		If sim.TalentFrost.UnbreakableArmor = 0 Then Return False
		If sim.runes.Frost(T) = False Then
			If sim.BloodTap.IsAvailable(T) Then
				sim.BloodTap.Use(T)
			Else
				return false
			End If
		End If
		If sim.BoneShieldUsageStyle = 1 Or sim.BoneShieldUsageStyle = 2 Then
			If sim.KeepBloodSync Then
				If sim.BloodToSync = True Then
					sim.BloodToSync  = False
				Else
					sim.BloodToSync  = true
				End If
			End If
		Else
			sim.BloodToSync = false
		End If
		cd = t + 60 * 100
		sim.Runes.UseDeathBlood(T,true)
		ActiveUntil= T + 20 * 100
		UseGCD(T)
		sim.RunicPower.add(10)
		sim.combatlog.write(T  & vbtab &  "Unbreakable Armor")
		Me.HitCount = Me.HitCount +1
		AddUptime(T)
		return true
	End Function
	Function isActive() As Boolean
		if ActiveUntil >= sim.TimeStamp then return true
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
