'
' Created by SharpDevelop.
' User: Fabien
' Date: 24/03/2009
' Time: 22:35
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class DeathandDecay
	inherits Spells.Spell

	Friend nextTick As Long
	
	Sub New(MySim as Sim)
		sim = Mysim
	End Sub
	
	
	Public Overloads Overrides Sub Init()
		MyBase.init()
		nextTick = 0
	End Sub
	
	
	 
	Function isAvailable(T As Long) As Boolean
		if CD > T then return false
		if sim.runes.BFU(T) then return true
	End Function
	
	Function Apply(T As Long) As Boolean
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
		nextTick = T+100
		sim.runes.UseBlood(T, False)
		sim.runes.UseFU(T, False)
		ActiveUntil = T+1000
		cd = T + 3000 - TalentUnholy.Morbidity*500
		combatlog.write(T  & vbtab &  "D&D ")
		Sim.RunicPower.add(15)
		
		return true
	End Function
	
	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double

		If sim.DoMySpellHit = false Then
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "D&D fail")
			MissCount = MissCount + 1
			Exit function
		End If
		RNG = sim.RandomNumberGenerator.RNGStrike
		dim dégat as Integer
		If sim.RandomNumberGenerator.RNGStrike <= CritChance Then
			dégat = AvrgCrit(T)
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "D&D crit for " & dégat)
			CritCount = CritCount + 1
		Else
			dégat= AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "D&D hit for " & dégat)
		End If
		
		
		if sim.Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		
		
		
		nextTick = T+100
		if nextTick > ActiveUntil then nextTick = T-1
		return true

	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 62
		tmp = tmp + (0.0475 * (1 + 0.04 * TalentUnholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if sim.glyph.DeathandDecay then tmp = tmp *1.2
		return tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1 
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit
	End Function
	overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (0.5 + CritCoef)
	End Function
	

End Class
