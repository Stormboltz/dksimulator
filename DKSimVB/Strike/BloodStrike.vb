
Friend class BloodStrike
	Inherits Strikes.Strike
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		
		
		
		If sim.MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		
		Dim MHHit As Boolean
		Dim OHHit As Boolean
		MHHit = True
		OHHit = True
		
		If sim.MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			
			If sim.DoMyStrikeHit = false Then
				combatlog.write(T  & vbtab &  "MH/OH BS fail")
				MissCount = MissCount + 1
				MHHit = False
				OHHit = false
			End If
		Else
			OHHit = false
			If sim.DoMyStrikeHit = false Then
				combatlog.write(T  & vbtab &  "BS fail")
				MissCount = MissCount + 1
				Exit function
			End If
		End If
		
		If MHHit Or OHHit Then
			If MHHit Then
				RNG = sim.RandomNumberGenerator.RNGStrike
				dim dégat as Integer
				If RNG <= CritChance Then
					dégat = AvrgCrit(T,true)
					CritCount = CritCount + 1
					combatlog.write(T  & vbtab &  "BS crit for " & dégat )
					sim.tryOnCrit
				Else
					dégat = AvrgNonCrit(T,true)
					HitCount = HitCount + 1
					combatlog.write(T  & vbtab &  "BS hit for " & dégat )
				End If
				if sim.Lissage then dégat = AvrgCrit(T,true)*CritChance + AvrgNonCrit(T,true)*(1-CritChance )
				total = total + dégat
				sim.TryOnMHHitProc
				sim.proc.TryT92PDPS
			End If
			If OHHit Then
				dim dégat as Integer
				If RNG <= CritChance Then
					dégat = AvrgCrit(T,false)
					combatlog.write(T  & vbtab &  "OH BS crit for " & dégat )
					sim.tryOnCrit
				Else
					dégat = AvrgNonCrit(T,false)
					combatlog.write(T  & vbtab &  "OH BS hit for " & dégat )
				End If
				if sim.Lissage then dégat = AvrgCrit(T,false)*CritChance + AvrgNonCrit(T,false)*(1-CritChance )
				total = total + dégat
				sim.TryOnOHHitProc
				sim.proc.TryT92PDPS
			End If
			
			sim.Sigils.tryHauntedDreams()
			
			
			If rng < 0.05*talentblood.SuddenDoom Then
				sim.deathcoil.ApplyDamage(T,true)
			End If
			If TalentFrost.BloodoftheNorth = 3 Or TalentUnholy.Reaping = 3 Then
				sim.runes.UseBlood(T,True)
			Else
				sim.runes.UseBlood(T,False)
			End If
			If sim.Desolation.Bonus > 0 Then
				sim.Desolation.Apply(T)
			End If
			Sim.RunicPower.add (10)
			Return True
		End If
	End Function
	
	public Overrides Function AvrgNonCrit(T as Long, MH as Boolean) As Double
		Dim tmp As Double
		If MH Then
			tmp = sim.MainStat.NormalisedMHDamage * 0.4
		Else
			tmp = sim.MainStat.NormalisedOHDamage * 0.4
		End If
		tmp = tmp + 305.6
		if sim.MainStat.T84PDPS = 1 then
			tmp = tmp * (1 + 0.125 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.125 * Sim.NumDesease)
		End If
		tmp = tmp * (1 + TalentBlood.BloodyStrikes * 5 / 100)
		tmp = tmp * (1 + TalentFrost.BloodoftheNorth * 5 / 100)
		
		if sim.sigils.DarkRider then tmp = tmp + 45 + 22.5 * Sim.NumDesease
		if sim.glyph.BloodStrike then tmp = tmp * (1.2)
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		AvrgNonCrit = tmp
	End Function
	
	public Overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + TalentBlood.MightofMograine * 15 / 100) * (1 + TalentFrost.GuileOfGorefiend * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.crit + TalentBlood.Subversion * 3 / 100
	End Function
	public Overrides Function AvrgCrit(T as long,MH as Boolean) As Double
		AvrgCrit = AvrgNonCrit(T,MH) * (1 + CritCoef)
	End Function
	
	
End Class