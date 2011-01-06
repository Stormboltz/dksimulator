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
                BaseDamage = 297 * 1.33

            Coeficient = (0.08)
            Multiplicator += sim.Character.Talents.Talent("CrimsonScourge").Value * 0.2
            logLevel = LogLevelEnum.Basic
            DiseaseBonus = 1
            If sim.Character.Talents.MainSpec = Character.Talents.Schools.Blood Then
                Resource = New Resource(S, Resource.ResourcesEnum.BloodOrDeathRune, False, 10)
            Else
                Resource = New Resource(S, Resource.ResourcesEnum.BloodRune, False, 10)
            End If

        End Sub

        Public Overrides Function IsAvailable() As Boolean
            Return MyBase.IsAvailable()
            If sim.proc.CrimsonScourge.IsActive Then Return True
        End Function


        Public Overrides Sub Use()
            If sim.proc.CrimsonScourge.IsActive Then
                sim.proc.CrimsonScourge.Cancel()
            Else
                MyBase.Use()
            End If
        End Sub



        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            UseGCD(T)
            Use()
            For Each Tar As Targets.Target In sim.Targets.AllTargets
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