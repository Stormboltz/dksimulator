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
	
Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	

	
	
	Function IsAvailable(T As Long) As Boolean
		If TalentFrost.UnbreakableArmor = 0 Then return false 
		If CD >= T Then Return False
		If sim.BloodTap.IsAvailable(T) and sim.Runes.Frost(T)=false Then return true
	End Function
	Function Use(T As Long) As Boolean
		If TalentFrost.UnbreakableArmor = 0 Then Return False
		
		
		If sim.runes.Frost(T) = False Then
			If sim.BloodTap.IsAvailable(T) Then
				sim.BloodTap.Use(T)
			Else
				return false
			End If
		End If
		cd = t + 60 * 100
		sim.Runes.UseFrost(T,false)
		ActiveUntil= T + 20 * 100
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
		sim.RunicPower.add(10)
		sim.combatlog.write(T  & vbtab &  "Unbreakable Armor")
		
		
		
		me.HitCount = me.HitCount +1
		return true
	End Function
	Function isActive() As Boolean
		if ActiveUntil >= sim.TimeStamp then return true
	End Function

		
End Class
