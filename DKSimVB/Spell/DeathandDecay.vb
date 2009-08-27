'
' Created by SharpDevelop.
' User: Fabien
' Date: 24/03/2009
' Time: 22:35
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module DeathandDecay
	Friend total As Long
	Friend TotalHit As Long
	Friend TotalCrit as Long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount As Integer
	Friend nextTick As Long
	Friend ActiveUntil as Long
	Friend CD As Long
	
	
		
	Sub init()
		cd = 0
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		nextTick = 0
		ActiveUntil = 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
	
	Function isAvailable(T As Long) As Boolean
		if CD > T then return false
		if runes.BFU(T) then return true
	End Function
	
	Function Apply(T As Long) As Boolean
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
		nextTick = T+100
		runes.UseBlood(T, False)
		runes.UseFU(T, False)
		ActiveUntil = T+1000
		cd = T + 3000 - TalentUnholy.Morbidity*500
		combatlog.write(T  & vbtab &  "D&D ")
		RunicPower.add(15)
		
		return true
	End Function
	
	Function ApplyDamage(T As long) As boolean
		Dim RNG As Double

		If DoMySpellHit = false Then
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "D&D fail")
			MissCount = MissCount + 1
			Exit function
		End If
		RNG = RNGStrike
		dim dégat as Integer
		If RNGStrike <= CritChance Then
			dégat = AvrgCrit(T)
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "D&D crit for " & dégat)
			CritCount = CritCount + 1
		Else
			dégat= AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "D&D hit for " & dégat)
		End If
		
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		
		
		
		nextTick = T+100
		if nextTick > ActiveUntil then nextTick = T-1
		return true

	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 62
		tmp = tmp + (0.0475 * (1 + 0.04 * TalentUnholy.Impurity) * MainStat.AP)
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if glyph.DeathandDecay then tmp = tmp *1.2
		return tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1 
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		CritChance = MainStat.SpellCrit
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (0.5 + CritCoef)
	End Function
	Function report As String
		dim tmp as String
		tmp = "Death and Deacy" & VBtab
	
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
