Friend module Gargoyle
	
	Friend NextGargoyleStrike As Long
	Friend total As Long
	Friend ActiveUntil As Long
	Friend cd As Long
	Friend NextRuneTick as Long
	Private SpellHaste As Double
	Private AP As Integer
	Private SpellHit As Double
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
		Friend TotalHit As Long
	Friend TotalCrit as Long

	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		cd = 0
		ActiveUntil= 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
	
	
Sub Summon(T As Long)
	
	
 If AreStarsAligned(T) = False Then
 	exit sub
 End If
	
	SpellHaste = MainStat.SpellHaste
	AP = MainStat.AP
	If cd <= T Then
		RunicPower.Value = RunicPower.Value - 60
		combatlog.write(T  & vbtab &  "Gargoyle use" & vbtab & "RP left = " & RunicPower.Value )
		cd = T + (3*6000)
		ActiveUntil = T + 3000
		SpellHit = MainStat.SpellHit
		Sim.NextFreeGCD = T + (150 / (1 + mainstat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
		NextRuneTick = T+1000
		NextGargoyleStrike = T
		combatlog.write(T  & vbtab &  "Summon Gargoyle")
	End If
	
End Sub

Function ConsumeRP (T As Long)  As Boolean
	exit function
	RunicPower.Value = RunicPower.Value - 3
	If RunicPower.Value < 0 Then
		RunicPower.Value = 0
		ActiveUntil = T-1
		combatlog.write(T  & vbtab &  "Gargoyle kicked!")
	Else
		if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Gargoyle drain" & vbtab & "RP left = " & RunicPower.Value )
		NextRuneTick = T + 100
	End If
	
End Function


Function ApplyDamage(T As long) As boolean
	NextGargoyleStrike= T + (200 / (1 + SpellHaste))
	Dim RNG As Double
	RNG = Rnd
	If SpellHit >= 0.17 Then
		RNG = RNG+0.17
	Else
		RNG = RNG+SpellHit
	End If
	If RNG < 0.17 Then
		if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Gargoyle Strike fail")
		MissCount = MissCount + 1
		Exit function
	End If
	RNG = Rnd
	dim dégat as Integer
	If RNG <= CritChance Then
		dégat = AvrgCrit(T)
		CritCount = CritCount + 1
		if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Gargoyle Strike crit for " & dégat )
	Else
		dégat = AvrgNonCrit(T)
		HitCount = HitCount + 1
		If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "Gargoyle Strike hit for " & dégat )
	End If
	
	if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
	total = total + dégat
		
	
	return true
End Function
Function AvrgNonCrit(T As long) As Double
	Dim tmp As Double
	tmp = 120
	tmp = tmp + ( AP*0.3333)
	'tmp = tmp + (0.15 * (1 + 0.04 * TalentUnholy.Impurity) * MainStat.AP)
	tmp = tmp * MagicalDamageMultiplier(t)
	return tmp
End Function
Function CritCoef() As Double
	CritCoef = 1
End Function
Function CritChance() As Double
	CritChance = SpellCrit
End Function
Function AvrgCrit(T As long) As Double
	AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
End Function

Function MagicalDamageMultiplier(T as long) As Double
	Dim tmp As Double
	tmp = 1
	tmp = tmp * (1 + 0.03 * Buff.PcDamage)
	tmp = tmp * (1 + 0.13 * Buff.SpellDamageTaken)
	return tmp
End Function

Function SpellCrit() As Single
	Dim tmp As Double
	'tmp = Character.SpellCritRating / 45.91
	tmp = tmp + 3 * Buff.CritChanceTaken
	tmp = tmp + 5 * Buff.SpellCrit
	tmp = tmp + 5 * Buff.SpellCritTaken
	SpellCrit = tmp / 100
End Function
Function report As String
	dim tmp as String
	tmp = "Gargoyle" & VBtab
	
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
end module
