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
        logLevel = LogLevelEnum.Basic
        DiseaseBonus = 1
    End Sub

    Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        UseGCD(T)
        sim.Runes.UseBlood(T, False)

        For Each Tar As Targets.Target In sim.Targets.AllTargets
            'TODO Diffrent debuff for each target
            MyBase.ApplyDamage(T)
            sim.proc.tryProcs(Procs.ProcOnType.OnDamage)
        Next
        sim.RunicPower.add(15)
        Return True
    End Function
    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
        If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
        Return tmp
    End Function
End Class
