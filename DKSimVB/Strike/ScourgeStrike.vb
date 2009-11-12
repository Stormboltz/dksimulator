Friend Class ScourgeStrike
	Inherits strikes.Strike
	
	Private tmpPhysical As Double
	Private tmpMagical As Double
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
		If sim.MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		
		If DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab &  "SS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		dim d�gat as Integer
		
		If sim.Patch33 Then 'Patch 3.3 #######(
			tmpPhysical = 0
			tmpMagical = 0
			'Physical part
			RNG = MyRNG
			If RNG <= CritChance Then
				CritCount = CritCount + 1
				tmpPhysical = AvrgNonCritPhysical(T)* (1 + CritCoef)
				'sim.combatlog.write(T  & vbtab &  "SS Physical crit for " & d�gat )
				sim.tryOnCrit
			Else
				HitCount = HitCount + 1
				tmpPhysical = AvrgNonCritPhysical(T)
				'sim.combatlog.write(T  & vbtab &  "SS Physical hit for " & d�gat )
			End If
			d�gat = tmpPhysical
			'Magical part
			sim.ScourgeStrikeMagical.ApplyDamage(d�gat,T)
'			RNG = MyRNG
'			If RNG <= CritChance Then
'				Magiccrit = Magiccrit + 1
'				tmpMagical = AvrgNonCritMagical(T)* (1 + CritCoef)
'				'd�gat = d�gat +  tmpPhysical + tmpMagical
'				'sim.combatlog.write(T  & vbtab &  "SS Magical crit for " & d�gat )
'				sim.tryOnCrit
'			Else
'				MagicHit = MagicHit + 1
'				tmpMagical = AvrgNonCritMagical(T)
'				'd�gat = d�gat +  tmpPhysical + tmpMagical
'				'sim.combatlog.write(T  & vbtab &  "SS Magical hit for " & d�gat )
'			End If
		Else ')######
			RNG = MyRNG
			If RNG <= CritChance Then
				CritCount = CritCount + 1
				d�gat = AvrgCrit(T)
				sim.combatlog.write(T  & vbtab &  "SS crit for " & d�gat )
				sim.tryOnCrit
			Else
				HitCount = HitCount + 1
				d�gat = AvrgNonCrit(T)
				sim.combatlog.write(T  & vbtab &  "SS hit for " & d�gat )
			End If
		End If

		If sim.Patch33 Then
			tmpPhysical = math.round(tmpPhysical,0)
			'tmpMagical = math.round(tmpMagical,0)
			'MagicTotal = MagicTotal + tmpMagical
			sim.combatlog.write(T  & vbtab &  "SS hit for " & tmpPhysical & " physical")
			'sim.combatlog.write(T  & vbtab &  "SS hit for " & tmpMagical & " magical")
			total = total + tmpPhysical
		Else
			total = total +  d�gat
		End If
		
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
		sim.proc.Virulence.TryMe(t)
		sim.TryOnMHHitProc
		return true
	End Function
	
	Function AvrgNonCritPhysical(T As Long) As Double
		tmpPhysical = sim.MainStat.NormalisedMHDamage
		tmpPhysical = tmpPhysical * 0.50
		tmpPhysical = tmpPhysical + 400
		
		If sim.sigils.Awareness Then tmpPhysical = tmpPhysical + 189
		If sim.sigils.ArthriticBinding Then tmpPhysical = tmpPhysical + 91.35
		tmpPhysical = tmpPhysical * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		tmpPhysical = tmpPhysical * (1 + 6.6666666 * TalentUnholy.Outbreak / 100)
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
		
		tmp = tmp * (1 + sim.mainstat.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodGorged)
		if sim.proc.T104PDPSFAde >= T then tmp = tmp * 1.03
		tmp = tmp * (1 + 0.13 *  sim.Buff.SpellDamageTaken)
		tmp = tmp * (1-0.05) 'Average partial resist
		tmpMagical = tmpMagical * tmp
		tmpMagical = tmpMagical * (1 + TalentFrost.BlackIce * 2 / 100)
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
		tmp = tmp * (1 + 6.6666666 * TalentUnholy.Outbreak / 100)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
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
		CritCoef = 1 + TalentUnholy.ViciousStrikes * 15 / 100
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	
	
	Function MagicalCritCoef() As Double
		return  (1+0.06*sim.mainstat.CSD)
	End Function
	
	Function MagicalCritChance() As Double
		dim tmp as Double
		tmp = sim.MainStat.crit + sim.MainStat.T72PDPS * 5 / 100
		return  tmp
	End Function
	
	public Overrides Function CritChance() As Double
		dim tmp as Double
		tmp = sim.MainStat.crit + TalentUnholy.ViciousStrikes * 3 / 100 + sim.MainStat.T72PDPS * 5 / 100 + talentblood.Subversion * 3 / 100
		return  tmp
	End Function
	
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	Public Overloads Overrides Function report() As String
		If sim.Patch33 Then
		End If
		Return MyBase.report()
	End Function
	
	
	Function MagicReport As String
		dim tmp as String
		tmp = ShortenName(me.ToString) & " Magical"  & VBtab
		
		If total.ToString().Length < 8 Then
			tmp = tmp & MagicTotal & "   " & VBtab
		Else
			tmp = tmp & MagicTotal & VBtab
		End If
		tmp = tmp & toDecimal(100*MagicTotal/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(MagicHit+MagicCrit) & VBtab
		tmp = tmp & toDecimal(100*MagicHit/(MagicHit+0+MagicCrit)) & VBtab
		tmp = tmp & toDecimal(100*MagicCrit/(MagicHit+0+MagicCrit)) & VBtab
		tmp = tmp & toDecimal(100*0/(MagicHit+0+MagicCrit)) & VBtab
		tmp = tmp & toDecimal(MagicTotal/(MagicHit+MagicCrit)) & VBtab
		If sim.MainStat.FrostPresence Then
			tmp = tmp & toDecimal((100 * MagicTotal * ThreadMultiplicator * 2.0735 ) / sim.TimeStamp) & VBtab
		End If

		tmp = tmp & vbCrLf
		return tmp
	End Function
	
	
End Class
