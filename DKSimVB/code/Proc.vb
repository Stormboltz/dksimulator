'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 00:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Proc
	Friend KillingMachine As Boolean
	Friend Rime as Boolean
	Friend StrifeFade as double
	Friend HauntedDreamsFade As Double
	Friend ScentOfBloodCD As Long
	Friend ScentOfBloodProc as Integer
	Friend VirulenceFade as Integer
	Friend T92PDPSFAde as Integer
	
	
	Sub TryRime()
		dim RNG as Double
		RNG = RandomNumberGenerator.NextDouble()
		If RNG <= 5 * talentfrost.Rime/100 Then
			Proc.Rime= True
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "Rime proc")
			HowlingBlast.cd = 0
		End If
	End Sub
	Sub init
		Proc.Rime = False
		KillingMachine = False
		ScentOfBlood = 0
		VirulenceFade = 0
		T92PDPSFAde = 0
	End Sub
	
	Sub TryT92PDPS()
		If SetBonus.T92PDPS = 0 Then Exit Sub
		dim RNG as Double
		RNG = RandomNumberGenerator.NextDouble()
		If RNG <= 0.5 Then
			T92PDPSFAde = sim.TimeStamp + 1500
		End If
	End Sub
	
	
	Sub GetUseScentOfBlood(T as Long)
		ScentOfBloodCD = T + 1000
		ScentOfBloodProc = TalentBlood.ScentOfBlood
	End Sub
	
	
	
end Module
