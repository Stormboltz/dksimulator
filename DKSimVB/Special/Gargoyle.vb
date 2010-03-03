Friend Class Gargoyle
	Inherits Supertype
	
	Friend NextGargoyleStrike As Long

	Friend ActiveUntil As Long
	Friend cd As Long
	Private SpellHaste As Double
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
		HasteSensible = true
	End Sub
	
Function Summon(T As Long) as  boolean
	If sim.runeforge.AreStarsAligned(T) = False Then
 		return false
 	End If
	SpellHaste = sim.MainStat.Haste
	AP = sim.MainStat.AP
	If cd <= T Then
		Sim.RunicPower.Value = Sim.RunicPower.Value - 60
		sim.combatlog.write(T  & vbtab &  "Gargoyle use" & vbtab & "RP left = " & Sim.RunicPower.Value )
		cd = T + 3 * 60 * 100
		ActiveUntil = T + 30 * 100
		SpellHit = sim.MainStat.SpellHit
		UseGCD(T)
		NextGargoyleStrike = T + 1000
		sim.combatlog.write(T  & vbtab &  "Summon Gargoyle")
		return true
	End If
End Function
sub UseGCD(T as Long)
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste)) + sim.latency/10
	End sub
Function ApplyDamage(T As long) As boolean
	NextGargoyleStrike =  T + math.Max((2 * 100) / (1 + SpellHaste),1)
	'Debug.Print( (2 * 100) / (1 + SpellHaste) )
	Dim RNG As Double
	
	RNG = sim.RandomNumberGenerator.RNGPet
	If SpellHit >= 0.17 Then
		RNG = RNG+0.17
	Else
		RNG = RNG+SpellHit
	End If
	If RNG < 0.17 Then
		if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Gargoyle Strike fail")
		MissCount = MissCount + 1
		Exit function
	End If
	
	RNG = sim.RandomNumberGenerator.RNGPet
	dim dégat as Integer
	If RNG <= CritChance Then
		dégat = AvrgCrit(T)
		CritCount = CritCount + 1
		totalcrit += dégat 
		if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Gargoyle Strike crit for " & dégat )
	Else
		dégat = AvrgNonCrit(T)
		HitCount = HitCount + 1
		totalhit += dégat 
		If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "Gargoyle Strike hit for " & dégat )
	End If
	

	total = total + dégat
		
	return true
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
