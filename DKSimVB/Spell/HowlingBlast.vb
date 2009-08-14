'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 22:48
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module HowlingBlast
	
	Friend cd as long	
	Friend total As long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
		Friend TotalHit As Long
	Friend TotalCrit as Long

	
	
	Function isAvailable(T As Long) As Boolean
		if TalentFrost.HowlingBlast <> 1 then return false
		if cd <= T then return true
	End Function
	
	
	Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
		cd = T + 800
		RNG = RandomNumberGenerator.NextDouble()
		If mainstat.Hit >= 0.17 Then
			RNG = RNG+0.17
		Else
			RNG = RNG+mainstat.SpellHit
		End If
		If RNG < 0.17 Then
			combatlog.write(T  & vbtab &  "HB fail")
			proc.KillingMachine  = False
			Proc.rime = False
			MissCount = MissCount + 1
			Exit function
		End If
		

		RNG = RandomNumberGenerator.NextDouble()
		Dim dégat As Integer
		Dim ccT As Double
			ccT = CritChance
		If RNG <= ccT Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			combatlog.write(T  & vbtab &  "HB crit for " & dégat )
		Else
	
			HitCount = HitCount + 1
			dégat = AvrgNonCrit(T)
			combatlog.write(T  & vbtab &  "HB hit for " & dégat)
		End If
		
		if Lissage then dégat = AvrgCrit(T)*ccT + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		If Proc.rime Then
			Proc.rime = False
			RunicPower.add (TalentFrost.ChillOfTheGrave * 2.5)
		Else
			runes.UseFU(T,False)
			RunicPower.add (15 + (TalentFrost.ChillOfTheGrave * 2.5))
		End If

		proc.KillingMachine  = false
		if glyph.HowlingBlast then		
			FrostFever.Apply(T)
		End If
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		
		Dim tmp As Double
		tmp = 585
		tmp = tmp + (0.2 * (1 + 0.04 * TalentUnholy.Impurity) * MainStat.AP)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if sim.NumDesease > 0 then 	tmp = tmp * (1 + TalentFrost.GlacierRot * 6.6666666 / 100)
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		If (T/sim.MaxTime) >= 0.75 Then tmp = tmp *(1+ 0.06*talentfrost.MercilessCombat)
		if MHRazorice or (OHRazorice and mainstat.DualW)  then tmp = tmp *1.10
		if CinderglacierProc > 0 then
			tmp = tmp * 1.2
			CinderglacierProc = CinderglacierProc -1
 		end if
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1 * (1 + Talentfrost.GuileOfGorefiend * 0.5 * 15 / 100) 'GoG works off the 1.5 spell crit modifier or something like that
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		CritChance = MainStat.SpellCrit 
		If proc.KillingMachine  = True Then
			Return 1
		Else
			If DeathChill.IsAvailable(sim.TimeStamp) Then
				Deathchill.use(sim.TimeStamp)
				DeathChill.Active = false
				Return 1
			End If
		End If
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
		Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		cd = 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
	
	Function report As String
		dim tmp as String
		tmp = "Howling Blast" & VBtab
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
