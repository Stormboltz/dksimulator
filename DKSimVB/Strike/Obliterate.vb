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
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim MHHit As Boolean
		Dim OHHit As Boolean
		MHHit = True
		OHHit = true
		Dim RNG As Double
		
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		RNG = RNGStrike

		If MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			If DoMyStrikeHit = false Then
				combatlog.write(T  & vbtab &  "MH/OH OB fail" & vbtab & RNG)
				MissCount = MissCount + 1
				MHHit=False
				OHHit=false
			End If
		Else
			OHHit = false
			If DoMyStrikeHit = false Then
				combatlog.write(T  & vbtab &  "OB fail" & vbtab & RNG)
				MissCount = MissCount + 1
				Exit function
			End If
		End If
		
		if MHHit or OHHit then
			
			If talentblood.DRM = 3 Then
				runes.UseFU(T,True)
			Else
				runes.UseFU(T,False)
			End If
			
			If talentfrost.Annihilation <> 3 Then
				sim.frostfever.FadeAt=T
				sim.bloodplague.FadeAt=T
			End If
			
			dim dégat as Integer
			Dim ccT As Double
			ccT = CritChance
			If MHHit Then
				RNG = RNGStrike
				If RNG <= ccT Then
					CritCount = CritCount + 1
					dégat =  AvrgCrit(T,true)
					combatlog.write(T  & vbtab &  "OB crit for " & dégat )
					TryBitterAnguish()
					TryMirror()
					TryPyrite()
					TryOldGod()
					
				Else
					HitCount = HitCount + 1
					dégat =  AvrgNonCrit(T,true)
					combatlog.write(T  & vbtab &  "OB hit for " & dégat )
				End If
				if Lissage then dégat = AvrgCrit(T,true)*ccT + AvrgNonCrit(T,true)*(1-ccT )
				total = total + dégat
				TryMHCinderglacier
				TryMHFallenCrusader
				TryMjolRune
				TryGrimToll
				proc.tryRime
				TryGreatness()
				TryDeathChoice()
				TryDCDeath()
				TryVictory()
				TryBandit()
				TryDarkMatter()
				TryComet()
			End If
			
			If OHHit Then
				If RNG <= ccT Then
					
					dégat =  AvrgCrit(T,false)
					combatlog.write(T  & vbtab &  "OH OB crit for " & dégat )
					TryBitterAnguish()
					TryMirror()
					TryPyrite()
					TryOldGod()
					
				Else
					
					dégat =  AvrgNonCrit(T,false)
					combatlog.write(T  & vbtab &  "OH OB hit for " & dégat )
				End If
				if Lissage then dégat = AvrgCrit(T,false)*ccT + AvrgNonCrit(T,false)*(1-ccT )
				total = total + dégat
				TryOHCinderglacier
				TryOHBerserking
				TryOHFallenCrusader
				TryMjolRune
				TryGrimToll
				proc.tryRime
				TryGreatness()
				TryDeathChoice()
				TryDCDeath()
				TryVictory()
				TryBandit()
				TryDarkMatter()
				TryComet()
			End If
			
			If DRW.IsActive(T) Then
				drw.Obliterate
			End If
			proc.VirulenceFade = T + 2000
			
			runicpower.add(15 + 2.5*talentfrost.ChillOfTheGrave  + 5*SetBonus.T74PDPS )
		End If
		
		
		return true
	End Function
	
	
	public Overrides Function AvrgNonCrit(T as long, MH as Boolean) As Double
		Dim tmp As Double
		If MH Then
			tmp = MainStat.NormalisedMHDamage * 0.8 + 467.2
		Else
			tmp = MainStat.NormalisedOHDamage * 0.8 + 467.2
		End If
		
		if sigils.Awareness then tmp = tmp + 336
		if SetBonus.T84PDPS = 1 then
			tmp = tmp * (1 + 0.125 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.125 * Sim.NumDesease)
		end if
		if (T/sim.MaxTime) >= 0.75 then tmp = tmp *(1+ 0.06*talentfrost.MercilessCombat)
		tmp = tmp * MainStat.StandardPhysicalDamageMultiplier(T)
		If glyph.Obliterate Then tmp = tmp *1.2
		
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		return  tmp
	End Function
	
	
	public Overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + talentfrost.GuileOfGorefiend * 15 / 100)
		return  CritCoef * (1+0.06*mainstat.CSD)
	End Function
	
	
	public Overrides Function CritChance() As Double
		If sim.DeathChill.IsAvailable(sim.TimeStamp) Then
			sim.Deathchill.use(sim.TimeStamp)
			sim.DeathChill.Active = false
			Return 1
		End If
		return  MainStat.crit +  talentfrost.rime*5/100 + talentblood.Subversion*3/100 + SetBonus.T72PDPS * 5/100
		
	End Function
	
	public Overrides Function AvrgCrit(T As long, MH as Boolean) As Double
		return AvrgNonCrit(T, MH) * (1 + CritCoef)
	End Function
	
	
End class
