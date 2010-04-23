Friend Class PlagueStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
	
		If OffHand = False Then
			UseGCD(T)	

			If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHPlagueStrike.ApplyDamage(T)
			If DoMyStrikeHit = false Then
				MissCount = MissCount + 1
				sim.combatlog.write(T  & vbtab &  "PS fail")
				Exit function
			End If
		Else
			If DoMyToTHit = False Then Exit Function

		End If
		If OffHand = False Then
			Sim.RunicPower.add (10 + sim.Character.talentunholy.Dirge * 2.5)
		End If
		
		Dim dégat As Integer
		RNG = RngCrit
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			sim.combatlog.write(T  & vbtab &  "PS crit for " & dégat  )
			sim.tryOnCrit
			
			totalcrit += dégat
		Else
			HitCount = HitCount + 1
			dégat = AvrgNonCrit(T)
			totalhit += dégat
			sim.combatlog.write(T  & vbtab &  "PS hit for " & dégat )
		End If
		total = total + dégat
		If OffHand = False Then
			sim.TryOnMHHitProc
			sim.runes.UseUnholy(T,False)
			
		Else
			sim.TryOnOHHitProc
		End If
		sim.proc.strife.tryme(t)
		sim.BloodPlague.Apply(T)
		If sim.DRW.IsActive(T) Then
			sim.drw.DRWPlagueStrike
		End If
		Return True
	End Function
	public Overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		
		If OffHand = False Then
			tmp = sim.MainStat.NormalisedMHDamage * 0.5
		Else
			tmp = sim.MainStat.NormalisedOHDamage * 0.5
		End If
		
		tmp = tmp + 189
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1 + sim.Character.talentunholy.Outbreak * 10 / 100)
		If sim.character.glyph.PlagueStrike Then tmp = tmp * (1.2)
		If OffHand  Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + sim.Character.talentfrost.NervesofColdSteel * 8.3333 / 100)
		End If
		AvrgNonCrit = tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = (1 + sim.Character.talentunholy.ViciousStrikes * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		Dim tmp As Double
		
		tmp = sim.MainStat.crit + sim.Character.talentunholy.ViciousStrikes * 3 / 100 + sim.MainStat.T72PTNK*0.1
		
		return tmp
	End Function
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function

	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHPlagueStrike.Total
		TotalHit += sim.OHPlagueStrike.TotalHit
		TotalCrit += sim.OHPlagueStrike.TotalCrit

		MissCount = (MissCount + sim.OHPlagueStrike.MissCount)/2
		HitCount = (HitCount + sim.OHPlagueStrike.HitCount)/2
		CritCount = (CritCount + sim.OHPlagueStrike.CritCount)/2
		
		sim.OHPlagueStrike.Total = 0
		sim.OHPlagueStrike.TotalHit = 0
		sim.OHPlagueStrike.TotalCrit = 0
	End sub
	
	
	
	
end Class