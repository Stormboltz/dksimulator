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
                Return False
			End If
			Sim.RunicPower.add (10)
			RNG = RngCrit

            If RNG <= CritChance Then
                LastDamage = AvrgCrit(T, Tar)
                sim.combatlog.write(T & vbtab & "BB crit for " & LastDamage)
                CritCount = CritCount + 1
                totalcrit += LastDamage
            Else
                LastDamage = AvrgNonCrit(T, Tar)
                HitCount = HitCount + 1
                totalhit += LastDamage
                sim.combatlog.write(T & vbtab & "BB hit for " & LastDamage)
            End If
            total = total + LastDamage
			
            sim.proc.TryOnSpellHit()
		Next
		return true
		
	End Function
	Overrides Function AvrgNonCrit(T As long,target as Targets.Target) As Double
		Dim tmp As Double
		tmp = 200
        tmp = tmp + (0.04 * (1 + 0.2 * sim.Character.Talents.Talent("Impurity").Value) * sim.MainStat.AP)

        If target.NumDesease > 0 Then tmp = tmp * 2
        tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
        tmp *= (1 + sim.Character.Talents.Talent("CrimsonScourge").Value * 0.2)
        If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
        Return tmp
    End Function
    
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit
	End Function
	Overrides Function AvrgCrit(T As long,target as Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function

End Class
