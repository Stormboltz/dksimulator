'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:47
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module BloodCakedBlade
	Friend total As  long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit as Long
	
	
	Function ApplyDamage(T As long,MH as Boolean) As boolean
		Dim RNG As Double
		RNG = RandomNumberGenerator.NextDouble()
		If mainstat.Expertise >= 0.065 Then
			RNG = RNG+0.065
		Else
			RNG=RNG + mainstat.Expertise
		End If
		If mainstat.Hit >= 0.08 Then
			RNG = RNG+0.08
		Else
			RNG = RNG+mainstat.Hit
		End If
		
		If RNG < 0.145 Then
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "BCB fail")
			MissCount = MissCount + 1
			Exit function
		End If
		
		total = total + AvrgNonCrit(T,MH)
		
		If MH Then
			TryMHCinderglacier
			TryMHFallenCrusader
		Else
			TryOHCinderglacier
			TryOHFallenCrusader
		End If
		
		
		HitCount = HitCount + 1
		if combatlog.LogDetails then combatlog.write(T  & vbtab &  "BCB hit for " & int(AvrgNonCrit(T,MH)))
		return true
	End Function
	Function AvrgNonCrit(T as long, MH as Boolean) As Double
		Dim tmp As Double
		If MH Then
			'tmp = MainStat.NormalisedMHDamage
			tmp = MainStat.MHBaseDamage
		Else
			tmp = MainStat.OHBaseDamage
			'tmp = MainStat.NormalisedOHDamage
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		tmp = tmp * (0.25 + 0.125 * Sim.NumDesease)
		tmp = tmp * MainStat.StandardPhysicalDamageMultiplier(T)
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		
	End Function
	Function CritChance() As Double
		CritChance = MainStat.crit
	End Function
	Function AvrgCrit(T as long,MH as Boolean) As Double
		AvrgCrit = AvrgNonCrit(T,MH) * (1 + CritCoef)
	End Function
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
		
	End Sub
	Function report As String
		dim tmp as String
		tmp = "Blood Caked Blade" & VBtab
		
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & int(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & int(HitCount+CritCount) & VBtab
		tmp = tmp & int(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
End Module
