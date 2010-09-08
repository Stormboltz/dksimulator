'
' Created by SharpDevelop.
' User: Fabien
' Date: 15/03/2009
' Time: 01:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Spells


    Friend Class BloodBoil
        Inherits Spells.Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            BaseDamage = 200 * 1.33
            Coeficient = (0.08 * (1 + 0.2 * Sim.Character.Talents.Talent("Impurity").Value))
            Multiplicator = (1 + Sim.Character.Talents.Talent("CrimsonScourge").Value * 0.2)
            logLevel = LogLevelEnum.Basic
            DiseaseBonus = 1
        End Sub

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            UseGCD(T)
            If Sim.proc.CrimsonScourge.IsActive Then
                Sim.proc.CrimsonScourge.Use()
            Else
                Sim.Runes.UseBlood(T, False)
                Sim.RunicPower.add(15)
            End If


            For Each Tar As Targets.Target In Sim.Targets.AllTargets
                'TODO Diffrent debuff for each target
                MyBase.ApplyDamage(T)


            Next
            sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnBloodBoil)

            Return True
        End Function
        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
            If Sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
            Return tmp
        End Function
    End Class
End Namespace