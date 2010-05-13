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
	
	Sub New(MySim As Sim)
		MyBase.New(MySim)
	End Sub
	
	Public Overloads Overrides Sub Init()
		MyBase.init()
		nextTick = 0
		ThreadMultiplicator = 1.9
	End Sub
	
	Function isAvailable(T As Long) As Boolean
		if CD > T then return false
        If sim.Runes.BFU(T) Then Return True
        Return False
	End Function
	
	Function Apply(T As Long) As Boolean
		UseGCD(T)
		nextTick = T+100
		sim.runes.UseBlood(T, False)
		sim.runes.UseFU(T, False)
		ActiveUntil = T+1000
		cd = T + 3000 - sim.Character.talentunholy.Morbidity*500
		Sim.RunicPower.add(15)
		sim.combatlog.write(T  & vbtab &  "D&D ")
		sim.FutureEventManager.Add(nextTick,"D&D")
		
		return true
	End Function
	
	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double

		Dim Tar As Targets.Target
		
		For Each Tar In sim.Targets.AllTargets
			If DoMySpellHit = false Then
				if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "D&D fail")
				MissCount = MissCount + 1
                Return False
			End If
			RNG = RngCrit
			dim dégat as Integer
			If RNG <= CritChance Then
				dégat = AvrgCrit(T,Tar)
				if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "D&D crit for " & dégat)
				CritCount = CritCount + 1
				totalcrit += dégat
			Else
				dégat= AvrgNonCrit(T,Tar)
				HitCount = HitCount + 1
				totalhit += dégat
				if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "D&D hit for " & dégat)
			End If
			

			total = total + dégat
		Next
		nextTick = T+100
		If nextTick > ActiveUntil Then
			nextTick = T-1
		Else
			sim.FutureEventManager.Add(nextTick,"D&D")
		End If
		return true
	End Function
	Overrides Function AvrgNonCrit(T As long,target as Targets.Target) As Double
		Dim tmp As Double
		tmp = 62
		tmp = tmp + (0.0475 * (1 + 0.04 * sim.Character.talentunholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
		If sim.character.glyph.DeathandDecay Then tmp = tmp *1.2
		If sim.MainStat.T102PTNK =1 Then tmp = tmp *1.2
		return tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit
	End Function
	Overrides Function AvrgCrit(T As long,target as Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T,target) * (0.5 + CritCoef)
	End Function
	

End Class
