'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:24
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module FrostStrike
	Friend total As Long
	Friend TotalHit As Long
	Friend TotalCrit as Long
	
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	
	
	Function isAvailable(T As long) As Boolean
		if glyph.FrostStrike then
			If RunicPower.Value >= 32 Then isAvailable = True
		Else
			If RunicPower.Value >= 40 Then isAvailable = True
		end if

		
	End Function
	
	Function ApplyDamage(T As Long) As Boolean
		
		Dim RNG As Double
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		If glyph.FrostStrike Then
			RunicPower.Value = RunicPower.Value - 32
		Else
			RunicPower.Value = RunicPower.Value - 40
		End If
		
		Dim MHHit As Boolean
		Dim OHHit As Boolean
		MHHit = True
		OHHit = True
		
		If MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			'MH
			RNG = Rnd
			If mainstat.Expertise >= 0.065 Then
				RNG = RNG+0.065
			Else
				RNG= RNG + mainstat.Expertise
			End If
			If mainstat.Hit >= 0.08 Then
				RNG = RNG+0.08
			Else
				RNG = RNG+mainstat.Hit
			End If
			If RNG < 0.145 Then
				combatlog.write(T  & vbtab & "MH FS fail")
				MissCount = MissCount + 1
				MHHit = False
				OHHit = false
			End If
		Else
			OHHit = false
			RNG = Rnd
			If mainstat.Expertise >= 0.065 Then
				RNG = RNG+0.065
			Else
				RNG= RNG + mainstat.Expertise
			End If
			If mainstat.Hit >= 0.08 Then
				RNG = RNG+0.08
			Else
				RNG = RNG+mainstat.Hit
			End If
			If RNG < 0.145 Then
				combatlog.write(T  & vbtab & "FS fail")
				proc.KillingMachine  = False
				MissCount = MissCount + 1
				Exit function
			End If
		end if
			Dim ccT As Double
			dim dégat as Integer
			ccT = CritChance
			If MHHit Or OHHit Then
				If MHHit Then
					RNG = Rnd
					If RNG < ccT Then
						dégat = AvrgCrit(T,true)
						combatlog.write(T  & vbtab &  "FS crit for " & dégat )
						CritCount = CritCount + 1
					Else
						dégat = AvrgNonCrit(T,true)
						HitCount = HitCount + 1
						combatlog.write(T  & vbtab &  "FS hit for " & dégat )
					End If
					if Lissage then dégat = AvrgCrit(T,true)*ccT + AvrgNonCrit(T,true)*(1-ccT )
					total = total + dégat
					TryMHCinderglacier
					TryMHFallenCrusader
					TryMjolRune
					TryGrimToll
				End If
				If OHHit Then
					If RNG < ccT Then
						dégat = AvrgCrit(T,false)
						combatlog.write(T  & vbtab &  "OH FS crit for " & dégat )
					Else
						dégat = AvrgNonCrit(T,false)
						combatlog.write(T  & vbtab &  "OH FS hit for " & dégat )
					End If
					if Lissage then dégat = AvrgCrit(T,false)*ccT + AvrgNonCrit(T,false)*(1-ccT )
					total = total + dégat
					TryOHCinderglacier
					TryOHFallenCrusader
					TryMjolRune
					TryGrimToll
				End If
				proc.KillingMachine  = False
				Return True
			End If
		End Function
		Function AvrgNonCrit(T As long, MH as Boolean) As Double
			Dim tmp As Double
			If MH Then
				tmp = mainstaT.NormalisedMHDamage*0.55
			Else
				tmp = mainstaT.NormalisedOHDamage*0.55
			End If
			
			tmp = tmp + 150
			if sigils.VengefulHeart then tmp= tmp + 113
			tmp = tmp * (1+ talentfrost.BloodoftheNorth * 5 /100)
			If sim.NumDesease > 0 Then 	tmp = tmp * (1 + TalentFrost.GlacierRot * 6.6666666 / 100)
			if (T/sim.MaxTime) >= 0.75 then tmp = tmp *(1+ 0.06*talentfrost.MercilessCombat)
			tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
			tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
			if MHRazorice or (OHRazorice and mainstat.DualW)  then tmp = tmp *1.10
			if CinderglacierProc > 0 then
				tmp = tmp * 1.2
				CinderglacierProc = CinderglacierProc -1
			End If
			If MH=false Then
				tmp = tmp * 0.5
				tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
			End If
			AvrgNonCrit = tmp
		End Function
		Function CritCoef() As Double
			CritCoef =  1 * (1 + Talentfrost.GuileOfGorefiend * 15 / 100)
			CritCoef = CritCoef * (1+0.06*mainstat.CSD)
		End Function
		Function CritChance() As Double
			CritChance = MainStat.Crit + 8/100 * SetBonus.T82PDPS
			if proc.KillingMachine  = true then return 1
		End Function
		Function AvrgCrit(T As long, MH as Boolean) As Double
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
			tmp = "Frost Strike" & VBtab
			
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
