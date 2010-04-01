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
		dim d�gat as Integer
		

			tmpPhysical = 0
			tmpMagical = 0
			'Physical part
			RNG = RngCrit
			If RNG <= CritChance Then
				CritCount = CritCount + 1
				d�gat = AvrgNonCrit(T)* (1 + CritCoef)
				sim.combatlog.write(T  & vbtab &  "SS Physical crit for " & d�gat )
				totalcrit += d�gat
				sim.tryOnCrit
				sim.ScourgeStrikeMagical.ApplyDamage(d�gat,T,true)
			Else
				HitCount = HitCount + 1
				d�gat = AvrgNonCrit(T)
				totalhit += d�gat
				sim.combatlog.write(T  & vbtab &  "SS Physical hit for " & d�gat )
				sim.ScourgeStrikeMagical.ApplyDamage(d�gat,T,false)
			End If
			
			total = total + d�gat
		
		
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
	
	Overrides Function AvrgNonCrit(T As Long) As Double
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
