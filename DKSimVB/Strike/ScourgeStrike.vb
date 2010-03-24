Friend Class ScourgeStrike
	Inherits strikes.Strike
	
	Private tmpPhysical As integer
	Private tmpMagical As integer
	Private MagicHit As long
	Private MagicCrit As long
	Friend MagicTotal As Long
	
	
	Sub New(S As sim)
		MyBase.New(s)
		MagicCrit = 0
		MagicHit = 0
		MagicTotal = 0
	End Sub
	
	public Overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		'scourgestrike glyph
		UseGCD(T)
		
		If DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab &  "SS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		dim dégat as Integer
		

			tmpPhysical = 0
			tmpMagical = 0
			'Physical part
			RNG = MyRNG
			If RNG <= CritChance Then
				CritCount = CritCount + 1
				dégat = AvrgNonCritPhysical(T)* (1 + CritCoef)
				sim.combatlog.write(T  & vbtab &  "SS Physical crit for " & dégat )
				totalcrit += dégat
				sim.tryOnCrit
				sim.ScourgeStrikeMagical.ApplyDamage(dégat,T,true)
			Else
				HitCount = HitCount + 1
				dégat = AvrgNonCritPhysical(T)
				totalhit += dégat
				sim.combatlog.write(T  & vbtab &  "SS Physical hit for " & dégat )
				sim.ScourgeStrikeMagical.ApplyDamage(dégat,T,false)
			End If
			
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
		Sim.RunicPower.add (15 + sim.TalentUnholy.Dirge * 2.5 + 5*sim.MainStat.T74PDPS)
		sim.TryOnFU
		sim.TryOnMHHitProc
		return true
	End Function
	
	Function AvrgNonCritPhysical(T As Long) As Double
		tmpPhysical = sim.MainStat.NormalisedMHDamage
		tmpPhysical = tmpPhysical * 0.70
		tmpPhysical = tmpPhysical + 560
		If sim.sigils.Awareness Then tmpPhysical = tmpPhysical + 189
		If sim.sigils.ArthriticBinding Then tmpPhysical = tmpPhysical + 91.35
		tmpPhysical = tmpPhysical * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		tmpPhysical = tmpPhysical * (1 + 6.6666666 * sim.TalentUnholy.Outbreak / 100)
		If sim.MainStat.T102PDPS<>0 Then tmpPhysical = tmpPhysical * 1.1
		Return tmpPhysical
	End Function
	Function AvrgNonCritMagical(T As Long) As Double
		tmpMagical = tmpPhysical
		If sim.MainStat.T84PDPS = 1 Then
			tmpMagical = tmpMagical * (0.25 * Sim.NumDesease * 1.2)
		Else
			tmpMagical = tmpMagical * (0.25 * Sim.NumDesease )
		End If
		Dim tmp As Double
		tmp = 1
		
		tmp = tmp * (1 + sim.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * sim.TalentBlood.BloodGorged)
		if sim.proc.T104PDPSFAde >= T then tmp = tmp * 1.03
		tmp = tmp * (1 + 0.13 *  sim.Buff.SpellDamageTaken)
		tmp = tmp * (1-0.05) 'Average partial resist
		tmpMagical = tmpMagical * tmp
		tmpMagical = tmpMagical * (1 + sim.TalentFrost.BlackIce * 2 / 100)
		If sim.RuneForge.CinderglacierProc > 0 Then
			tmpMagical = tmpMagical * 1.2
			sim.RuneForge.CinderglacierProc = sim.RuneForge.CinderglacierProc -1
		End If

		Return tmpMagical
	End Function
	public Overrides Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		
		tmp = sim.MainStat.NormalisedMHDamage
		tmp = tmp * 0.40
		tmp = tmp + 357.19
		If sim.sigils.Awareness Then tmp = tmp + 189
		if sim.sigils.ArthriticBinding then tmp = tmp + 91.35
		if sim.MainStat.T84PDPS = 1 then
			tmp = tmp * (1 + 0.10 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.10 * Sim.NumDesease )
		end if
		tmp = tmp * (1 + 6.6666666 * sim.TalentUnholy.Outbreak / 100)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + sim.TalentFrost.BlackIce * 2 / 100)
		if sim.RuneForge.CinderglacierProc > 0 then
			tmp = tmp * 1.2
			sim.RuneForge.CinderglacierProc = sim.RuneForge.CinderglacierProc -1
		End If
		If sim.MainStat.T102PDPS<>0 Then
			tmp = tmp * 1.1
		End If
		AvrgNonCrit = tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = 1 + sim.TalentUnholy.ViciousStrikes * 15 / 100
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	
	
	
	public Overrides Function CritChance() As Double
		dim tmp as Double
		tmp = sim.MainStat.crit + sim.TalentUnholy.ViciousStrikes * 3 / 100 + sim.MainStat.T72PDPS * 5 / 100 + sim.TalentBlood.Subversion * 3 / 100
		return  tmp
	End Function
	
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	

	
	Public Overrides sub Merge()
		Total += sim.ScourgeStrikeMagical.Total
		TotalHit += sim.ScourgeStrikeMagical.TotalHit
		TotalCrit += sim.ScourgeStrikeMagical.TotalCrit

		MissCount = (MissCount + sim.ScourgeStrikeMagical.MissCount)/2
		HitCount = (HitCount + sim.ScourgeStrikeMagical.HitCount)/2
		CritCount = (CritCount + sim.ScourgeStrikeMagical.CritCount)/2
		
		sim.ScourgeStrikeMagical.Total = 0
		sim.ScourgeStrikeMagical.TotalHit = 0
		sim.ScourgeStrikeMagical.TotalCrit = 0
	End sub
	
	
	
	
End Class
