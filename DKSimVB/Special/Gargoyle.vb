Friend Class Gargoyle
	Inherits Supertype
	
	Friend NextGargoyleStrike As Long

	Friend ActiveUntil As Long
	Friend cd As Long
	Private StrikeCastTime As Long
	Private AP As Integer
	Private SpellHit As Double
	
	Sub New(S as Sim)
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		cd = 0
		ActiveUntil= 0
		TotalHit = 0
		TotalCrit = 0
		Sim = S
		sim.DamagingObject.Add(Me)
		ThreadMultiplicator = 0
		HasteSensible = True
		isGuardian = true
	End Sub
	
	Function Summon(ByVal T As Long) As Boolean
		If sim.RuneForge.AreStarsAligned(T) = False Then
			Return False
		End If

		If cd <= T Then
			StrikeCastTime = (2.0 / sim.MainStat.Haste) * 100  'no haste cap for Garg.
			AP = sim.MainStat.AP
			sim.RunicPower.Value = sim.RunicPower.Value - 60
			sim.CombatLog.write(T & vbTab & "Gargoyle use" & vbTab & "RP left = " & sim.RunicPower.Value)
			cd = T + 3 * 60 * 100
			ActiveUntil = T + 30 * 100
			SpellHit = sim.MainStat.SpellHit
			UseGCD(T)
			NextGargoyleStrike = T + 1000
			sim.CombatLog.write(T & vbTab & "Summon Gargoyle")
			Return True
		End If
	End Function
	Sub UseGCD(ByVal T As Long)
		sim.UseGCD(T, True)
	End Sub
	Function ApplyDamage(ByVal T As Long) As Boolean
		NextGargoyleStrike = T + StrikeCastTime
		'Debug.Print( (2 * 100) / (1 + SpellHaste) )
		Dim RNG As Double

		RNG = RngHit
		If SpellHit >= 0.17 Then
			RNG = RNG + 0.17
		Else
			RNG = RNG + SpellHit
		End If
		If RNG < 0.17 Then
			If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Gargoyle Strike fail")
			MissCount = MissCount + 1
			Exit Function
		End If

		RNG = RngCrit
		Dim dégat As Integer
		If RNG <= CritChance() Then
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			TotalCrit += dégat
			If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Gargoyle Strike crit for " & dégat)
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			TotalHit += dégat
			If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Gargoyle Strike hit for " & dégat)
		End If


		total = total + dégat

		Return True
	End Function
Function AvrgNonCrit(T As long) As Double
	Dim tmp As Double
	tmp = 120
	tmp = tmp + ( AP*0.3333)
	tmp = tmp * MagicalDamageMultiplier(t)
	If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
	End If
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
	tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
	tmp = tmp * (1 + 0.13 *  sim.Buff.SpellDamageTaken)
	return tmp
End Function
Function SpellCrit() As Single
	Dim tmp As Double
	tmp = tmp + 3 *  sim.Buff.CritChanceTaken
	tmp = tmp + 5 *  sim.Buff.SpellCrit
	tmp = tmp + 5 *  sim.Buff.SpellCritTaken
	SpellCrit = tmp / 100
End Function
	Public Sub cleanup()
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub


end Class 
