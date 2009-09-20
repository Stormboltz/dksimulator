Friend Class ScourgeStrike
	Inherits strikes.Strike
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	
	
	public Overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		'scourgestrike glyph
		
		If sim.MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		
		If sim.DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab &  "SS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		dim dégat as Integer
		RNG = sim.RandomNumberGenerator.RNGStrike
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			sim.combatlog.write(T  & vbtab &  "SS crit for " & dégat )
			sim.tryOnCrit
			
		Else
			HitCount = HitCount + 1
			dégat = AvrgNonCrit(T)
			sim.combatlog.write(T  & vbtab &  "SS hit for " & dégat )
		End If
		
		
		if sim.Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		
		
		If sim.glyph.ScourgeStrike Then
			If sim.BloodPlague.ScourgeStrikeGlyphCounter < 3 Then
				sim.BloodPlague.FadeAt = sim.BloodPlague.FadeAt + 3 * 100
				sim.BloodPlague.ScourgeStrikeGlyphCounter = sim.BloodPlague.ScourgeStrikeGlyphCounter + 1
			End If
			If sim.FrostFever.ScourgeStrikeGlyphCounter < 3 Then
				sim.FrostFever.FadeAt = sim.FrostFever.FadeAt + 3 * 100
				sim.FrostFever.ScourgeStrikeGlyphCounter = sim.FrostFever.ScourgeStrikeGlyphCounter + 1
			End If
		End If
		sim.runes.UseFU(T,False)
		Sim.RunicPower.add (15 + TalentUnholy.Dirge * 2.5 + 5*sim.MainStat.T74PDPS)
		sim.proc.VirulenceFade = T + 2000
		sim.TryOnMHHitProc
		
		
		
		'Debug.Print T & vbTab & "ScourgeStrike for " & Range("Abilities!N11").Value
		return true
	End Function
	public Overrides Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		tmp = sim.MainStat.NormalisedMHDamage
		tmp = tmp * 0.40
		tmp = tmp + 357.19
		if sim.sigils.Awareness then tmp = tmp + 189
		if sim.MainStat.T84PDPS = 1 then
			tmp = tmp * (1 + 0.10 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.10 * Sim.NumDesease )
		end if
		tmp = tmp * (1 + 6.6666666 * TalentUnholy.Outbreak / 100)
		if sim.sigils.ArthriticBinding then tmp = tmp + 203
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if sim.RuneForge.CinderglacierProc > 0 then
			tmp = tmp * 1.2
			sim.RuneForge.CinderglacierProc = sim.RuneForge.CinderglacierProc -1
		end if
		AvrgNonCrit = tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = 1 + TalentUnholy.ViciousStrikes * 15 / 100
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		dim tmp as Double
		tmp = sim.MainStat.crit + TalentUnholy.ViciousStrikes * 3 / 100 + sim.MainStat.T72PDPS * 5 / 100 '+ talentblood.Subversion * 3 / 100
		return  tmp
	End Function
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function

	
End Class
