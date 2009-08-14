'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:29
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Obliterate
	Friend total As Long
	Friend TotalHit As Long
	Friend TotalCrit as Long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount As Integer
	
	
	
	
	
	
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
		
	End Sub
	Function ApplyDamage(T As Long) As Boolean
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
		
		RNG = Rnd
		
		If MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			If mainstat.Expertise >= 0.065 Then
				RNG = RNG+0.065
			Else
				RNG=RNG + mainstat.Expertise
			End If
			If mainstat.Hit >= 0.08 Then
				RNG = RNG+0.08
			Else
				RNG = RNG+mainstat.Hit
			End If
			If RNG < 0.145 Then
				combatlog.write(T  & vbtab &  "MH/OH OB fail" & vbtab & RNG)
				MissCount = MissCount + 1
				MHHit=False
				OHHit=false
			End If
		Else
			OHHit = false
			If mainstat.Expertise >= 0.065 Then
				RNG = RNG+0.065
			Else
				RNG=RNG + mainstat.Expertise
			End If
			If mainstat.Hit >= 0.08 Then
				RNG = RNG+0.08
			Else
				RNG = RNG+mainstat.Hit
			End If
			If RNG < 0.145 Then
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
				frostfever.FadeAt=T
				bloodplague.FadeAt=T
			End If
			
			dim dégat as Integer
			Dim ccT As Double
			ccT = CritChance
			If MHHit Then
				RNG = Rnd
				If RNG <= ccT Then
					CritCount = CritCount + 1
					dégat =  AvrgCrit(T,true)
					combatlog.write(T  & vbtab &  "OB crit for " & dégat )
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
			End If
			
			If OHHit Then
				If RNG <= ccT Then

					dégat =  AvrgCrit(T,false)
					combatlog.write(T  & vbtab &  "OH OB crit for " & dégat )
				Else

					dégat =  AvrgNonCrit(T,false)
					combatlog.write(T  & vbtab &  "OH OB hit for " & dégat )
				End If
				if Lissage then dégat = AvrgCrit(T,false)*ccT + AvrgNonCrit(T,false)*(1-ccT )
				total = total + dégat
				TryOHCinderglacier
				TryOHFallenCrusader
				TryMjolRune
				TryGrimToll
				proc.tryRime
			End If

			If DRW.IsActive(T) Then
				drw.Obliterate
			End If
			proc.VirulenceFade = T + 2000
			
			runicpower.add(15 + 2.5*talentfrost.ChillOfTheGrave + 2.5*talentunholy.Dirge  + 5*SetBonus.T74PDPS )
		End If
		
		
		return true
	End Function
	
	
	Function AvrgNonCrit(T as long, MH as Boolean) As Double
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
		AvrgNonCrit = tmp
	End Function
	
	
	Function CritCoef() As Double
		CritCoef = 1 * (1 + talentfrost.GuileOfGorefiend * 15 / 100)
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	
	
	Function CritChance() As Double
		If DeathChill.IsAvailable(sim.TimeStamp) Then
			Deathchill.use(sim.TimeStamp)
			DeathChill.Active = false
			Return 1
		End If
		CritChance = MainStat.crit +  talentfrost.rime*5/100 + talentblood.Subversion*3/100 + SetBonus.T72PDPS * 5/100
		
	End Function
	
	Function AvrgCrit(T As long, MH as Boolean) As Double
		AvrgCrit = AvrgNonCrit(T, MH) * (1 + CritCoef)
	End Function
	
	Function report As String
		dim tmp as String
		tmp = "Obliterate" & VBtab
		
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & int(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & int(HitCount+CritCount) & VBtab
		tmp = tmp & int(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	
End Module
