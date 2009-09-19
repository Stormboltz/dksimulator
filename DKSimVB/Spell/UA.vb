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
		If TalentFrost.UnbreakableArmor = 0 Then
			return false
		End If
		If T <= cd Then
			return false
		End If
		If sim.BloodTap.IsAvailable(T) and sim.Runes.Frost(T)=false Then
			return true
		End If
	End Function
	Function Use(T As long) As Boolean
		cd = t + 60 * 100
		sim.BloodTap.Use(T)
		'UseUnholy(T,false)
		ActiveUntil= T + 20 * 100
		If sim.MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100 + sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150 + sim._MainFrm.txtLatency.Text/10
		End If
		sim.combatlog.write(T  & vbtab &  "Unbreakable Armor")
		return true
	End Function
	Function isActive() As Boolean
		if ActiveUntil >= sim.TimeStamp then return true
	End Function

		
End Class
