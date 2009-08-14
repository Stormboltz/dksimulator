'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module UA
	Friend cd As Double
	Friend ActiveUntil As Long
	
	Function IsAvailable(T As Long) As Boolean
		If TalentFrost.UnbreakableArmor = 0 Then
			return false
		End If
		If T <= cd Then
			return false
		End If
		If BloodTap.IsAvailable(T) and Runes.Frost(T)=false Then
			return true
		End If
	End Function
	Function Use(T As long) As Boolean
		cd = t + 12000
		BloodTap.Use(T)
		'UseUnholy(T,false)
		ActiveUntil= T+2000
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100 + sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150 + sim._MainFrm.txtLatency.Text/10
		End If
		combatlog.write(T  & vbtab &  "Unbreakable Armor")
		return true
	End Function
	Function isActive() As Boolean
		if ActiveUntil >= sim.TimeStamp then return true
	End Function
	Sub init()
		cd = 0
		ActiveUntil = 0
	End Sub
		
End Module
