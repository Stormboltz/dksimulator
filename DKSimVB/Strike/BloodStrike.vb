
Friend module BloodStrike
	
	Friend total As long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit as Long
	
	
	Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		
		
		
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		
		Dim MHHit As Boolean
		Dim OHHit As Boolean
		MHHit = True
		OHHit = True
		
		If MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			If DoMyStrikeHit = false Then
				combatlog.write(T  & vbtab &  "MH/OH BS fail")
				MissCount = MissCount + 1
				MHHit = False
				OHHit = false
			End If
		Else
			OHHit = false
			If DoMyStrikeHit = false Then
				combatlog.write(T  & vbtab &  "BS fail")
				MissCount = MissCount + 1
				Exit function
			End If
		End If
		
		If MHHit Or OHHit Then
			If MHHit Then
				RNG = RNGStrike
				dim dégat as Integer
				If RNG <= CritChance Then
					dégat = AvrgCrit(T,true)
					CritCount = CritCount + 1
					combatlog.write(T  & vbtab &  "BS crit for " & dégat )
					TryBitterAnguish()
					TryMirror()
					TryPyrite()
					TryOldGod()
				Else
					dégat = AvrgNonCrit(T,true)
					HitCount = HitCount + 1
					combatlog.write(T  & vbtab &  "BS hit for " & dégat )
				End If
				if Lissage then dégat = AvrgCrit(T,true)*CritChance + AvrgNonCrit(T,true)*(1-CritChance )
				total = total + dégat
				TryMHCinderglacier
				TryMHFallenCrusader
				TryT92PDPS
				TryMjolRune
				TryGrimToll
				TryGreatness()
				TryDeathChoice()
				TryDCDeath()
				TryVictory()
				TryBandit()
				TryDarkMatter()
				TryComet()
			End If
			If OHHit Then
				dim dégat as Integer
				If RNG <= CritChance Then
					dégat = AvrgCrit(T,false)
					combatlog.write(T  & vbtab &  "OH BS crit for " & dégat )
					TryBitterAnguish()
				Else
					dégat = AvrgNonCrit(T,false)
					combatlog.write(T  & vbtab &  "OH BS hit for " & dégat )
				End If
				if Lissage then dégat = AvrgCrit(T,false)*CritChance + AvrgNonCrit(T,false)*(1-CritChance )
				total = total + dégat
				TryOHCinderglacier
				TryOHFallenCrusader
				TryOHBerserking
				TryT92PDPS
				TryMjolRune
				TryGrimToll
				TryGreatness()
				TryDeathChoice()
				TryDCDeath()
				TryVictory()
				TryBandit()
				TryDarkMatter()
				TryComet()
			End If
			
			tryHauntedDreams()
			
			
			If rng < 0.05*talentblood.SuddenDoom Then
				deathcoil.ApplyDamage(T,true)
			End If
			If TalentFrost.BloodoftheNorth = 3 Or TalentUnholy.Reaping = 3 Then
				runes.UseBlood(T,True)
			Else
				runes.UseBlood(T,False)
			End If
			If Desolation.Bonus > 0 Then
				Desolation.Apply(T)
			End If
			RunicPower.add (10)
			Return True
		End If
	End Function
	
	Function AvrgNonCrit(T as Long, MH as Boolean) As Double
		Dim tmp As Double
		If MH Then
			tmp = MainStat.NormalisedMHDamage * 0.4
		Else
			tmp = MainStat.NormalisedOHDamage * 0.4
		End If
		tmp = tmp + 305.6
		if SetBonus.T84PDPS = 1 then
			tmp = tmp * (1 + 0.125 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.125 * Sim.NumDesease)
		End If
		tmp = tmp * (1 + TalentBlood.BloodyStrikes * 5 / 100)
		tmp = tmp * (1 + TalentFrost.BloodoftheNorth * 5 / 100)
		
		if sigils.DarkRider then tmp = tmp + 45 + 22.5 * Sim.NumDesease
		if glyph.BloodStrike then tmp = tmp * (1.2)
		tmp = tmp * MainStat.StandardPhysicalDamageMultiplier(T)
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		AvrgNonCrit = tmp
	End Function
	
	Function CritCoef() As Double
		CritCoef = 1 * (1 + TalentBlood.MightofMograine * 15 / 100) * (1 + TalentFrost.GuileOfGorefiend * 15 / 100)
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		CritChance = MainStat.crit + TalentBlood.Subversion * 3 / 100
	End Function
	Function AvrgCrit(T as long,MH as Boolean) As Double
		AvrgCrit = AvrgNonCrit(T,MH) * (1 + CritCoef)
	End Function
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
		
	End Sub
	Function report As String
		dim tmp as String
		tmp = "Blood Strike" & VBtab
		
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
End Module