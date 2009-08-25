Friend module ScourgeStrike
	
	Friend total As long
	
	Friend TotalHit As Long
	Friend TotalCrit as Long
	
	
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		
		TotalHit = 0
		TotalCrit = 0
		
		
		
	End Sub
	
	Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		'scourgestrike glyph
		
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		
		If DoMyStrikeHit = false Then
			combatlog.write(T  & vbtab &  "SS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		dim dégat as Integer
		RNG = RNGStrike
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			combatlog.write(T  & vbtab &  "SS crit for " & dégat )
			TryBitterAnguish()
			TryMirror()
			TryPyrite()
			TryOldGod()
			
		Else
			HitCount = HitCount + 1
			dégat = AvrgNonCrit(T)
			combatlog.write(T  & vbtab &  "SS hit for " & dégat )
		End If
		
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		
		
		If glyph.ScourgeStrike Then
			RNG = RNGProc
			If RNG > 0.75 Then
				combatlog.write(T  & vbtab &  "SS glyph proc")
				BloodPlague.Apply(T)
				FrostFever.Apply(T)
			End If
		End If
		runes.UseFU(T,False)
		RunicPower.add (15 + TalentUnholy.Dirge * 2.5 + 5*SetBonus.T74PDPS)
		proc.VirulenceFade = T + 2000
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
		'Debug.Print T & vbTab & "ScourgeStrike for " & Range("Abilities!N11").Value
		return true
	End Function
	Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		tmp = MainStat.NormalisedMHDamage
		tmp = tmp * 0.40
		tmp = tmp + 357.19
		if sigils.Awareness then tmp = tmp + 189
		if SetBonus.T84PDPS = 1 then
			tmp = tmp * (1 + 0.10 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.10 * Sim.NumDesease )
		end if
		tmp = tmp * (1 + 6.6666666 * TalentUnholy.Outbreak / 100)
		if sigils.ArthriticBinding then tmp = tmp + 203
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if CinderglacierProc > 0 then
			tmp = tmp * 1.2
			CinderglacierProc = CinderglacierProc -1
		end if
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1 + TalentUnholy.ViciousStrikes * 15 / 100
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		dim tmp as Double
		tmp = MainStat.crit + (TalentUnholy.ViciousStrikes * 3 / 100) + (SetBonus.T72PDPS * 5/100)
		return  tmp
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	Function report As String
		dim tmp as String
		tmp = "Scourge Strike" & VBtab
		
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