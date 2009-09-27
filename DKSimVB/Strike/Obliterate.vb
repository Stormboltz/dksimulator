'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:29
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Obliterate
	Inherits Strikes.Strike
	
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim MHHit As Boolean
		Dim OHHit As Boolean
		MHHit = True
		OHHit = true
		Dim RNG As Double
		
		If sim.MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		RNG = sim.RandomNumberGenerator.RNGStrike

		If sim.MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			If sim.DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab &  "MH/OH OB fail" & vbtab & RNG)
				MissCount = MissCount + 1
				MHHit=False
				OHHit=false
			End If
		Else
			OHHit = false
			If sim.DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab &  "OB fail" & vbtab & RNG)
				MissCount = MissCount + 1
				Exit function
			End If
		End If
		
		if MHHit or OHHit then
			
			If talentblood.DRM = 3 Then
				sim.runes.UseFU(T,True)
			Else
				sim.runes.UseFU(T,False)
			End If
			
			If talentfrost.Annihilation <> 3 Then
				sim.frostfever.FadeAt=T
				sim.bloodplague.FadeAt=T
			End If
			
			dim dégat as Integer
			Dim ccT As Double
			ccT = CritChance
			If MHHit Then
				RNG = sim.RandomNumberGenerator.RNGStrike
				If RNG <= ccT Then
					CritCount = CritCount + 1
					dégat =  AvrgCrit(T,true)
					sim.combatlog.write(T  & vbtab &  "OB crit for " & dégat )
					sim.tryOnCrit
					
				Else
					HitCount = HitCount + 1
					dégat =  AvrgNonCrit(T,true)
					sim.combatlog.write(T  & vbtab &  "OB hit for " & dégat )
				End If

				total = total + dégat
				sim.TryOnMHHitProc
				sim.proc.tryRime
			End If
			
			If OHHit Then
				If RNG <= ccT Then
					
					dégat =  AvrgCrit(T,false)
					sim.combatlog.write(T  & vbtab &  "OH OB crit for " & dégat )
					sim.tryOnCrit
				Else
					
					dégat =  AvrgNonCrit(T,false)
					sim.combatlog.write(T  & vbtab &  "OH OB hit for " & dégat )
				End If

				total = total + dégat
				sim.TryOnOHHitProc
			End If
			
			If sim.DRW.IsActive(T) Then
				sim.drw.Obliterate
			End If
			sim.proc.VirulenceFade = T + 2000
			
			Sim.runicpower.add(15 + 2.5*talentfrost.ChillOfTheGrave  + 5*sim.MainStat.T74PDPS )
		End If
		
		
		return true
	End Function
	
	
	public Overrides Function AvrgNonCrit(T as long, MH as Boolean) As Double
		Dim tmp As Double
		If MH Then
			tmp = sim.MainStat.NormalisedMHDamage * 0.8 + 467.2
		Else
			tmp = sim.MainStat.NormalisedOHDamage * 0.8 + 467.2
		End If
		
		if sim.sigils.Awareness then tmp = tmp + 336
		if sim.MainStat.T84PDPS = 1 then
			tmp = tmp * (1 + 0.125 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.125 * Sim.NumDesease)
		end if
		if (T/sim.MaxTime) >= 0.75 then tmp = tmp *(1+ 0.06*talentfrost.MercilessCombat)
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		If sim.glyph.Obliterate Then tmp = tmp *1.2
		
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		return  tmp
	End Function
	
	
	public Overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + talentfrost.GuileOfGorefiend * 15 / 100)
		return  CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	
	
	public Overrides Function CritChance() As Double
		If sim.DeathChill.IsAvailable(sim.TimeStamp) Then
			sim.Deathchill.use(sim.TimeStamp)
			sim.DeathChill.Active = false
			Return 1
		End If
		return  sim.MainStat.crit +  talentfrost.rime*5/100 + talentblood.Subversion*3/100 + sim.MainStat.T72PDPS * 5/100
		
	End Function
	
	public Overrides Function AvrgCrit(T As long, MH as Boolean) As Double
		return AvrgNonCrit(T, MH) * (1 + CritCoef)
	End Function
	
	
End class
