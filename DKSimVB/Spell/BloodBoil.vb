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
		Dim Tar As Targets.Target
		
		For Each Tar In sim.Targets.AllTargets
			If DoMySpellHit = False Then
				sim.combatlog.write(T  & vbtab &  "BB fail")
				MissCount = MissCount + 1
				Exit function
			End If
			Sim.RunicPower.add (10)
			RNG = RngCrit
			dim d�gat as Integer
			If RNG <= CritChance Then
				d�gat = AvrgCrit(T,Tar)
				sim.combatlog.write(T  & vbtab &  "BB crit for " & d�gat  )
				CritCount = CritCount + 1
				totalcrit += d�gat
			Else
				d�gat = AvrgNonCrit(T,Tar)
				HitCount = HitCount + 1
				totalhit += d�gat
				sim.combatlog.write(T  & vbtab &  "BB hit for " & d�gat )
			End If
			total = total + d�gat
			
			Sim.TryOnSpellHit
		Next
		return true
		
	End Function
	Overrides Function AvrgNonCrit(T As long,target as Targets.Target) As Double
		Dim tmp As Double
		tmp = 200
		tmp = tmp + (0.04 * (1 + 0.04 * sim.Character.talentunholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1+ 0.1*sim.Character.talentblood.BloodyStrikes)
		if target.NumDesease > 0 then tmp = tmp * 2
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
	Overrides Function AvrgCrit(T As long,target as Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function

End Class
