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

    Sub New(ByVal S As Sim)
        MyBase.New(S)
        BaseDamage = 200
        Coeficient = (0.04 * (1 + 0.2 * sim.Character.Talents.Talent("Impurity").Value))
        Multiplicator = (1 + sim.Character.Talents.Talent("CrimsonScourge").Value * 0.2)
    End Sub

    Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        Dim RNG As Double
        UseGCD(T)
        sim.Runes.UseBlood(T, False)
        Dim Tar As Targets.Target

        For Each Tar In sim.Targets.AllTargets
            If DoMySpellHit() = False Then
                sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                MissCount = MissCount + 1
            Else
                RNG = RngCrit
                If RNG <= CritChance() Then
                    LastDamage = AvrgCrit(T, Tar)
                    sim.CombatLog.write(T & vbTab & Me.Name & " crit for " & LastDamage)
                    CritCount = CritCount + 1
                    TotalCrit += LastDamage
                Else
                    LastDamage = AvrgNonCrit(T, Tar)
                    HitCount = HitCount + 1
                    TotalHit += LastDamage
                    sim.CombatLog.write(T & vbTab & Me.Name & " hit for " & LastDamage)
                End If
                total = total + LastDamage

                sim.proc.tryProcs(Procs.ProcOnType.OnDamage)
            End If
        Next
        sim.RunicPower.add(10)
        Return True
    End Function
    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
        If target.NumDesease > 0 Then tmp = tmp * 2
        If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
        Return tmp
    End Function
End Class
