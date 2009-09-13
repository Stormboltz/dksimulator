'
' Created by SharpDevelop.
' User: Fabien
' Date: 06/04/2009
' Time: 22:07
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Module RuneStrike
	
	Friend total As Long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit as Long
	
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
		
	End Sub
	Function ApplyDamage(T As long) As boolean
		Dim dégat As Integer
		Dim BCB As Double
		Dim Nec As Double
		Dim MeleeMissChance As Single
		Dim RNG As Double
		
		RunicPower.Value = RunicPower.Value - 20
		RNG = RNGWhiteHit
		MeleeMissChance = math.Min(mainstat.Hit, 0.08)

		If MeleeMissChance + RNG < 0.08 Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Rune Strike fail")
			exit function
		End If
		
		RNG = RNGWhiteHit

		
		If MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			'Off hand
			If RNG < CritChance Then
				'CRIT !
				dégat = AvrgOHCrit(T)
				If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "Rune Strike OH crit for " & dégat )
			Else
				dégat = AvrgOHNonCrit(T)
				if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Rune Strike OH hit for " & dégat )
			End If
			
		End If
		
		'MainHand
		If RNG < CritChance Then
			'CRIT !
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "Rune Strike crit for " & dégat )
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Rune Strike hit for " & dégat )
		End If
'		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		TryMHKillingMachine
		
		If MHRazorice Then applyRazorice()
		If TalentUnholy.Necrosis > 0 Then
			Nec = sim.Necrosis.Apply(dégat, T)
		End If
		RNG = RNGWhiteHit * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			BCB = sim.BloodCakedBlade.ApplyDamage(T,true)
		End If
		TryMHCinderglacier
		TryMHFallenCrusader
		TryMjolRune
		TryGrimToll
		TryGreatness()
		TryDeathChoice()
		TryDCDeath()
		TryVictory()
		TryBandit()
		TryDarkMatter()
		TryComet()
		If proc.ScentOfBloodProc > 0 Then
			proc.ScentOfBloodProc  = proc.ScentOfBloodProc  -1
			RunicPower.add(5)
		End If
		
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = MainStat.MHBaseDamage * 1.5
		tmp = tmp * MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1+ SetBonus.T82PTNK*0.1)
		return tmp
	End Function
	
	
	Function AvrgOHNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = MainStat.OHBaseDamage * 1.5
		tmp = tmp * MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1+ SetBonus.T82PTNK*0.1)
		tmp = tmp * 0.5
		tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		return tmp
	End Function
	
	Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	
	Function CritChance() As Double
		return MainStat.crit + glyph.RuneStrike * 0.1
	End Function
	Function AvrgCrit(T As long) As Double
		return AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	Function AvrgOHCrit(T As long) As Double
		return AvrgOHNonCrit(T) * (1 + CritCoef)
	End Function
	Function report As String
		dim tmp as String
		tmp = "Runic Strike" & VBtab
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
end module



