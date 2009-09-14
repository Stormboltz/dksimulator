'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 00:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class proc
	Friend KillingMachine As Boolean
	Friend Rime as Boolean
	Friend StrifeFade as double
	Friend HauntedDreamsFade As Double
	Friend ScentOfBloodCD As Long
	Friend ScentOfBloodProc as Integer
	Friend VirulenceFade as Integer
	Friend T92PDPSFAde As Integer
	Friend T92PDPSCd As Long
	
	Sub New()
		Rime = False
		KillingMachine = False
		ScentOfBlood = 0
		VirulenceFade = 0
		T92PDPSFAde = 0
		T92PDPSCd = 0
	End Sub
	
	Sub TryRime()
		If sim.RandomNumberGenerator.RNGProc <= 5 * talentfrost.Rime/100 Then
			Rime= True
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "Rime sim.proc")
			sim.HowlingBlast.cd = 0
		End If
	End Sub
	Sub TryMHKillingMachine()
		dim RNG as Double
		If Talentfrost.KillingMachine > 0 Then
			RNG =  sim.RandomNumberGenerator.RNGWhiteHit
			If RNG < (Talentfrost.KillingMachine)*sim.MainStat.MHWeaponSpeed/60 Then
				if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "Killing Machine sim.proc")
				sim.proc.KillingMachine  = true
			End If
		End If
	End Sub
	
	Sub TryOHKillingMachine()
		dim RNG as Double
		If Talentfrost.KillingMachine > 0 Then
			RNG = sim.RandomNumberGenerator.RNGWhiteHit
			If RNG < (Talentfrost.KillingMachine)*sim.MainStat.OHWeaponSpeed/60 Then
				if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "Killing Machine sim.proc")
				KillingMachine  = true
			End If
		End If
	End Sub
	
	
	
	
	Sub TryT92PDPS()
		If sim.MainStat.T92PDPS = 0 Or sim.proc.T92PDPSCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.5 Then
			T92PDPSFAde = sim.TimeStamp + 15 * 100
			T92PDPSCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	
	Sub GetUseScentOfBlood(T as Long)
		ScentOfBloodCD = T
		ScentOfBloodProc = TalentBlood.ScentOfBlood
	End Sub
	
	
	
end Class
