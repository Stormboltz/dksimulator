'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 22:48
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class HowlingBlast
	Inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	Function isAvailable(T As Long) As Boolean
        If sim.Character.Talents.Talent("HowlingBlast").Value <> 1 Then Return False
        If CD <= T Then
            Return True
        Else
            Return False
        End If
    End Function

    Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        Dim RNG As Double
        UseGCD(T)
        cd = T + 800

        If DoMySpellHit = False Then
            sim.combatlog.write(T & vbtab & "HB fail")
            sim.proc.KillingMachine.Use()
            sim.Proc.rime.Use()
            MissCount = MissCount + 1
            Return False
        End If

        If sim.proc.rime.IsActive Then
            sim.Proc.rime.Use()
            sim.RunicPower.add(sim.Character.Talents.Talent("ChillOfTheGrave").Value * 5)
        Else
            sim.Runes.UseFrost(T, False)
            sim.RunicPower.add(10 + (sim.Character.Talents.Talent("ChillOfTheGrave").Value * 5))
        End If
        Dim Tar As Targets.Target

        For Each Tar In sim.Targets.AllTargets
            RNG = RngCrit

            Dim ccT As Double
            ccT = CritChance
            If RNG <= ccT Then
                CritCount = CritCount + 1
                LastDamage = AvrgCrit(T, Tar)
                sim.combatlog.write(T & vbtab & "HB crit for " & LastDamage)
                totalcrit += LastDamage
            Else
                HitCount = HitCount + 1
                LastDamage = AvrgNonCrit(T, Tar)
                sim.combatlog.write(T & vbtab & "HB hit for " & LastDamage)
                totalhit += LastDamage
            End If
            total = total + LastDamage
            sim.proc.TryOnSpellHit()
        Next


        sim.proc.KillingMachine.Use()
        If sim.character.glyph.HowlingBlast Then
            sim.Targets.MainTarget.FrostFever.Apply(T)
        End If

        Return True
    End Function
    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        If target Is Nothing Then target = sim.Targets.MainTarget
        Dim tmp As Double
        tmp = 1079
        tmp = tmp + (0.2 * (1 + 0.2 * sim.Character.Talents.Talent("Impurity").Value) * sim.MainStat.AP)
        tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
        If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
        tmp *= sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
        If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
        AvrgNonCrit = tmp
    End Function
   
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit
		If sim.proc.KillingMachine.IsActive Then
			Return 1
		Else
			If sim.DeathChill.IsAvailable(sim.TimeStamp) Then
				sim.Deathchill.use(sim.TimeStamp)
				sim.DeathChill.Active = false
				Return 1
			End If
		End If
	End Function
	overrides Function AvrgCrit(T As long,target As Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	


End Class
