Friend Class PlagueStrike
	Inherits Strikes.Strike
		Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim MHHit As Boolean
		Dim OHHit As Boolean
		MHHit = True
		OHHit = True
		
		
		Dim RNG As Double
		If sim.MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		
		
		If sim.MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			'MH
			If sim.DoMyStrikeHit = false Then
				MissCount = MissCount + 1
				combatlog.write(T  & vbtab &  "MH PS fail")
				MHHit = False
				OHHit=false
			End If
		Else
			OHHit = false
			If sim.DoMyStrikeHit = false Then
				MissCount = MissCount + 1
				combatlog.write(T  & vbtab &  "PS fail")
				Exit function
			End If
		End If
		
		if sim.sigils.Strife then
			sim.proc.StrifeFade = T+1000
		End If
		
		Dim dégat As Integer
		
		If MHHit Or OHHit Then
			RNG = sim.RandomNumberGenerator.RNGStrike
			If MHHit Then
				RNG = sim.RandomNumberGenerator.RNGStrike
				If RNG <= CritChance Then
					CritCount = CritCount + 1
					dégat = AvrgCrit(T,true)
					combatlog.write(T  & vbtab &  "PS crit for " & dégat  )
					sim.tryOnCrit
					
				Else
					HitCount = HitCount + 1
					dégat = AvrgNonCrit(T,true)
					combatlog.write(T  & vbtab &  "PS hit for " & dégat )
				End If
				if sim.Lissage then dégat = AvrgCrit(T,true)*CritChance + AvrgNonCrit(T,true)*(1-CritChance )
				total = total + dégat
				sim.TryOnMHHitProc
			End If
			If OHHit Then
				If RNG <= CritChance Then
					dégat = AvrgCrit(T,false)
					combatlog.write(T  & vbtab &  "OH PS crit for " & dégat  )
					sim.tryOnCrit
					
				Else
					dégat = AvrgNonCrit(T,false)
					combatlog.write(T  & vbtab &  "OH PS hit for " & dégat )
				End If
				if sim.Lissage then dégat = AvrgCrit(T,false)*CritChance + AvrgNonCrit(T,false)*(1-CritChance )
				total = total + dégat
				sim.TryOnOHHitProc
			End If
			
			
			sim.runes.UseUnholy(T,False)
			sim.BloodPlague.Apply(T)
			
			
			If sim.DRW.IsActive(T) Then
				sim.drw.PlagueStrike
			End If
			Sim.RunicPower.add (10 + TalentUnholy.Dirge * 2.5)
			
			
			
			Return True
		End If
	End Function
	public Overrides Function AvrgNonCrit(T As long, MH as Boolean) As Double
		Dim tmp As Double
		
		If MH Then
			tmp = sim.MainStat.NormalisedMHDamage * 0.5
		Else
			tmp = sim.MainStat.NormalisedOHDamage * 0.5
		End If
		
		tmp = tmp + 189
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentUnholy.Outbreak * 10 / 100)
		If sim.glyph.PlagueStrike Then tmp = tmp * (1.2)
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		AvrgNonCrit = tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = (1 + TalentUnholy.ViciousStrikes * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		Dim tmp As Double
		
		tmp = sim.MainStat.crit + TalentUnholy.ViciousStrikes * 3 / 100 + sim.MainStat.T72PTNK*0.1
		
		return tmp
	End Function
	public Overrides Function AvrgCrit(T As long, MH as Boolean) As Double
		AvrgCrit = AvrgNonCrit(T, MH) * (1 + CritCoef)
	End Function
	
end Class