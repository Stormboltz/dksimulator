
Friend class BloodStrike
	Inherits Strikes.Strike
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		dim dégat as Integer

		UseGCD(T)
		
		If sim.MainStat.DualW And sim.TalentFrost.ThreatOfThassarian = 3 Then
			if offhand=false then sim.OHBloodStrike.ApplyDamage(T)
		End If
		
		If DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab &  "BS fail")
			MissCount  += 1
			return false
		End If
		If sim.KeepBloodSync Then
			If sim.BloodToSync = True Then
				sim.BloodToSync  = False
			Else
				sim.BloodToSync  = true
			End If
		End If
		
		RNG = MyRNG
		
		If RNG <= CritChance Then
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			sim.combatlog.write(T  & vbtab &  "BS crit for " & dégat )
			totalcrit += dégat
			sim.tryOnCrit
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			totalhit += dégat
			sim.combatlog.write(T  & vbtab &  "BS hit for " & dégat )
		End If
		total = total + dégat
		
		If offhand = False Then
			sim.TryOnMHHitProc
			If sim.TalentFrost.BloodoftheNorth = 3 Or sim.TalentUnholy.Reaping = 3 Then
				sim.runes.UseBlood(T,True)
			Else
				sim.runes.UseBlood(T,False)
			End If
			Sim.RunicPower.add (10)
		Else
			sim.TryOnOHHitProc
		End If
		sim.proc.T92PDPS.TryMe(T)
		sim.proc.HauntedDreams.TryMe(t)
		If rng < 0.05*sim.talentblood.SuddenDoom Then sim.deathcoil.ApplyDamage(T,True)
		If sim.Desolation.Bonus > 0 Then sim.Desolation.Apply(T)
		Return True

End Function

public Overrides Function AvrgNonCrit(T as Long) As Double
	Dim tmp As Double
	
	If offhand = false Then
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
	tmp = tmp * (1 + sim.TalentBlood.BloodyStrikes * 5 / 100)
	tmp = tmp * (1 + sim.TalentFrost.BloodoftheNorth * 5 / 100)
	
	if sim.sigils.DarkRider then tmp = tmp + 45 + 22.5 * Sim.NumDesease
	if sim.glyph.BloodStrike then tmp = tmp * (1.2)
	tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
	If offhand Then
		tmp = tmp * 0.5
		tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 5 / 100)
	End If
	if sim.MainStat.T92PTNK =1 then tmp = tmp *1.05
	AvrgNonCrit = tmp
End Function

public Overrides Function CritCoef() As Double
	CritCoef = 1 * (1 + sim.TalentBlood.MightofMograine * 15 / 100) * (1 + sim.TalentFrost.GuileOfGorefiend * 15 / 100)
	CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
End Function
public Overrides Function CritChance() As Double
	CritChance = sim.MainStat.crit + sim.TalentBlood.Subversion * 3 / 100
End Function
public Overrides Function AvrgCrit(T as long) As Double
	AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
End Function


	Public Overrides sub Merge()
		Total += sim.OHBloodStrike.Total
		TotalHit += sim.OHBloodStrike.TotalHit
		TotalCrit += sim.OHBloodStrike.TotalCrit

		MissCount = (MissCount + sim.OHBloodStrike.MissCount)/2
		HitCount = (HitCount + sim.OHBloodStrike.HitCount)/2
		CritCount = (CritCount + sim.OHBloodStrike.CritCount)/2
		
		sim.OHBloodStrike.Total = 0
		sim.OHBloodStrike.TotalHit = 0
		sim.OHBloodStrike.TotalCrit = 0
	End sub

End Class