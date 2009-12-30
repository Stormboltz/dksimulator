Friend Class PlagueStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		UseGCD(T)	
	
		If sim.MainStat.DualW And sim.TalentFrost.ThreatOfThassarian = 3 Then
			if offhand=false then sim.OHPlagueStrike.ApplyDamage(T)
		End If
		
		If DoMyStrikeHit = false Then
			MissCount = MissCount + 1
			sim.combatlog.write(T  & vbtab &  "PS fail")
			Exit function
		End If
		
		Dim d�gat As Integer
		RNG = MyRNG
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			d�gat = AvrgCrit(T)
			sim.combatlog.write(T  & vbtab &  "PS crit for " & d�gat  )
			sim.tryOnCrit
			
			totalcrit += d�gat
		Else
			HitCount = HitCount + 1
			d�gat = AvrgNonCrit(T)
			totalhit += d�gat
			sim.combatlog.write(T  & vbtab &  "PS hit for " & d�gat )
		End If
		total = total + d�gat
		If OffHand = False Then
			sim.TryOnMHHitProc
			sim.runes.UseUnholy(T,False)
			Sim.RunicPower.add (10 + sim.TalentUnholy.Dirge * 2.5)
		Else
			sim.TryOnOHHitProc
		End If
		sim.proc.strife.tryme(t)
		sim.BloodPlague.Apply(T)
		If sim.DRW.IsActive(T) Then
			sim.drw.PlagueStrike
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
		tmp = tmp * (1 + sim.TalentUnholy.Outbreak * 10 / 100)
		If sim.glyph.PlagueStrike Then tmp = tmp * (1.2)
		If OffHand  Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		AvrgNonCrit = tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = (1 + sim.TalentUnholy.ViciousStrikes * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		Dim tmp As Double
		
		tmp = sim.MainStat.crit + sim.TalentUnholy.ViciousStrikes * 3 / 100 + sim.MainStat.T72PTNK*0.1
		
		return tmp
	End Function
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
end Class