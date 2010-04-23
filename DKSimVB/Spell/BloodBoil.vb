'
' Created by SharpDevelop.
' User: Fabien
' Date: 15/03/2009
' Time: 01:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class BloodBoil
	Inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub

	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		UseGCD(T)
		sim.runes.UseBlood(T,False)
		
		Dim intCount As Integer
		For intCount = 1 To Sim.NumberOfEnemies
			If DoMySpellHit = False Then
				sim.combatlog.write(T  & vbtab &  "BB fail")
				MissCount = MissCount + 1
				Exit function
			End If
			Sim.RunicPower.add (10)
			
			RNG = RngCrit
			dim dégat as Integer
			If RNG <= CritChance Then
				dégat = AvrgCrit(T)
				sim.combatlog.write(T  & vbtab &  "BB crit for " & dégat  )
				CritCount = CritCount + 1
				totalcrit += dégat
			Else
				dégat = AvrgNonCrit(T)
				HitCount = HitCount + 1
				totalhit += dégat
				sim.combatlog.write(T  & vbtab &  "BB hit for " & dégat )
			End If
			total = total + dégat
			
			Sim.TryOnSpellHit
		Next intCount
		
		
		
		return true
		
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 200
		tmp = tmp + (0.04 * (1 + 0.04 * sim.Character.talentunholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1+ 0.1*sim.Character.talentblood.BloodyStrikes)
		if sim.NumDesease > 0 then tmp = tmp * 2
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
		if sim.RuneForge.CheckCinderglacier(True) > 0 then tmp *= 1.2
		return tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + sim.Character.talentblood.MightofMograine * 15 / 100) 
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit
	End Function
	overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function

End Class
