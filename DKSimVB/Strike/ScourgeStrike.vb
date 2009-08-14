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
			combatlog.write(T  & vbtab &  "SS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		dim dégat as Integer
		RNG = RandomNumberGenerator.NextDouble()
			If RNG <= CritChance Then
				CritCount = CritCount + 1
				dégat = AvrgCrit(T)
				combatlog.write(T  & vbtab &  "SS crit for " & dégat )
			Else
				HitCount = HitCount + 1
				combatlog.write(T  & vbtab &  "SS hit for " & dégat )
			End If
		
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
	
		
		If glyph.ScourgeStrike Then
			RNG = RandomNumberGenerator.NextDouble()
			If RNG > 0.75 Then
				combatlog.write(T  & vbtab &  "SS glyph proc")
				BloodPlague.Apply(T)
				FrostFever.Apply(T)
			End If
		End If
		runes.UseFU(T,False)
		RNG = RandomNumberGenerator.NextDouble()
		
		If DRW.IsActive(T) Then
			If DRW.Hit >= 0.08 Then
				RNG = RNG+0.08
			Else
				RNG = RNG+DRW.Hit
			End If
			If RNG < 0.145 Then
				combatlog.write(T  & vbtab &  "DRW fail")
			Else
				RNG = RandomNumberGenerator.NextDouble()
				If RNG <= drw.Crit Then
					drw.total = drw.total + AvrgCrit(T)/2
					combatlog.write(T  & vbtab &  "DRW crit for " & int(AvrgCrit(T)/2) )
				Else
					drw.total = drw.total + AvrgNonCrit(T)/2
					combatlog.write(T  & vbtab &  "DRW hit for " & int(AvrgNonCrit(T)/2))
				End If
			End If
		End If
		RunicPower.add (15 + TalentUnholy.Dirge * 2.5 + 5*SetBonus.T74PDPS)
		proc.VirulenceFade = T + 2000
		TryMHCinderglacier
		TryMHFallenCrusader
		TryMjolRune
		TryGrimToll
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
		tmp = tmp & int(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & int(HitCount+CritCount) & VBtab
		tmp = tmp & int(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	
end module