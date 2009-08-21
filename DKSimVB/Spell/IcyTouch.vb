Friend module IcyTouch
	
	Friend total As long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
		Friend TotalHit As Long
	Friend TotalCrit as Long

	Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10

		
		RNG = Rnd
		If mainstat.SpellHit >= 0.17 Then
			RNG = RNG+0.17
		Else
			RNG = RNG+mainstat.SpellHit
		End If
		If RNG < 0.17 Then
			combatlog.write(T  & vbtab &  "IT fail")
			proc.KillingMachine  = False
			MissCount = MissCount + 1
			Exit function
		End If
		
		RNG = Rnd
		
		Dim dégat As Integer
		Dim ccT As Double
			ccT = CritChance
		If RNG <= ccT Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			combatlog.write(T  & vbtab &  "IT crit for " & dégat )
		Else
			HitCount = HitCount + 1
			dégat =  AvrgNonCrit(T)
			combatlog.write(T  & vbtab &  "IT hit for " & dégat)
		End If
		
		if Lissage then dégat = AvrgCrit(T)*ccT + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		
		
		
		RunicPower.add (10 + (TalentFrost.ChillOfTheGrave * 2.5))
		
		If glyph.IcyTouch Then RunicPower.add (10)



		
		
		If DRW.IsActive(T) Then
			DRW.IcyTouch
		End If
		
		runes.UseFrost(T,false)
		proc.KillingMachine  = False
		FrostFever.Apply(T)
		TryGreatness()
		TryDeathChoice()
		TryDCDeath()
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		
		Dim tmp As Double
		tmp = 236
		
		tmp = tmp + (0.1 * (1 + 0.04 * TalentUnholy.Impurity) * MainStat.AP)
		tmp = tmp * (1 + TalentFrost.ImprovedIcyTouch * 5 / 100)
		If sim.NumDesease > 0 Then 	tmp = tmp * (1 + TalentFrost.GlacierRot * 6.6666666 / 100)
		If (T/sim.MaxTime) >= 0.75 Then tmp = tmp *(1+ 0.06*talentfrost.MercilessCombat)
		If sigils.FrozenConscience Then tmp = tmp +111
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		if MHRazorice or (OHRazorice and mainstat.DualW)  then tmp = tmp *1.10
		if CinderglacierProc > 0 then
			tmp = tmp * 1.2
			CinderglacierProc = CinderglacierProc -1
 		end if
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		CritChance = MainStat.SpellCrit + TalentFrost.Rime * 5 / 100
		If proc.KillingMachine  = True Then return 1
		
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function report As String
		dim tmp as String
		tmp = "Icy Touch" & VBtab
	
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
		Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
end module