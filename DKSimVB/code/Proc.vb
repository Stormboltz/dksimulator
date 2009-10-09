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
	Friend ScentOfBloodProc as Integer
	Friend VirulenceFade as Integer
	Friend T92PDPSFAde As Integer
	Friend T92PDPSCd As Long
	Protected Sim as Sim
	Friend T104PDPSFAde As Integer

	

	
	
	Sub New(S as Sim)
		Rime = False
		KillingMachine = False
		ScentOfBlood = 0
		VirulenceFade = 0
		T92PDPSFAde = 0
		T92PDPSCd = 0
		T104PDPSFAde= 0
		sim = S
	End Sub
	
	

	
	Sub TryRime()
		If sim.RandomNumberGenerator.RNGProc <= 5 * talentfrost.Rime/100 Then
			Rime= True
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Rime sim.proc")
			sim.HowlingBlast.cd = 0
		End If
	End Sub
	Sub TryMHKillingMachine()
		dim RNG as Double
		If Talentfrost.KillingMachine > 0 Then
			RNG =  sim.RandomNumberGenerator.RNGWhiteHit
			If RNG < (Talentfrost.KillingMachine)*sim.MainStat.MHWeaponSpeed/60 Then
				if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Killing Machine sim.proc")
				sim.proc.KillingMachine  = true
			End If
		End If
	End Sub
	
	Sub TryOHKillingMachine()
		dim RNG as Double
		If Talentfrost.KillingMachine > 0 Then
			RNG = sim.RandomNumberGenerator.RNGWhiteHit
			If RNG < (Talentfrost.KillingMachine)*sim.MainStat.OHWeaponSpeed/60 Then
				if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Killing Machine sim.proc")
				KillingMachine  = true
			End If
		End If
	End Sub
	
	
	
	
	Sub TryT92PDPS()
		If sim.MainStat.T92PDPS = 0 Or sim.proc.T92PDPSCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.5 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "T92PDPS proc")
			T92PDPSFAde = sim.TimeStamp + 15 * 100
			T92PDPSCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	Sub tryT104PDPS(T As Long)
		If sim.MainStat.T104PDPS = 0 Then Exit Sub
		'Debug.Print(T & vbtab & sim.Runes.Rune1.AvailableTime & vbtab & sim.Runes.Rune2.AvailableTime & vbtab & sim.Runes.Rune3.AvailableTime & vbtab & sim.Runes.Rune4.AvailableTime & vbtab & sim.Runes.Rune5.AvailableTime & vbtab  & sim.Runes.Rune6.AvailableTime)
		If sim.Runes.Rune1.AvailableTime < T Then Exit Sub
		If sim.Runes.Rune2.AvailableTime < T Then Exit Sub
		If sim.Runes.Rune3.AvailableTime < T Then Exit Sub
		If sim.Runes.Rune4.AvailableTime < T Then Exit Sub
		If sim.Runes.Rune5.AvailableTime < T Then Exit Sub
		If sim.Runes.Rune6.AvailableTime < T Then Exit Sub
		T104PDPSFAde = T +15 *100
	End Sub
	
	
	
	
	Sub TryScentOfBlood(T As Long)
		If TalentBlood.ScentOfBlood = 0 Then Exit Sub
		If sim.RandomNumberGenerator.RNGTank <= 15 Then
			ScentOfBloodProc = TalentBlood.ScentOfBlood
		End If
		
		
	End Sub
	
	
	
	
end Class
