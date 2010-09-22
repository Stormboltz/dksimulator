'
' Created by SharpDevelop.
' User: e0030653
' Date: 01/04/2009
' Time: 15:33
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Spells
    Friend Class Pestilence
        Inherits Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Basic
            Dim D As Boolean = (sim.Character.Talents.Talent("Reaping").Value + sim.Character.Talents.Talent("BloodoftheNorth").Value > 0)
            Resource = New Resource(S, ResourcesEnum.BloodRune, D, 15)
        End Sub

        Overrides Sub use()
            Dim T As Double = sim.TimeStamp
            UseGCD(T)


            If DoMySpellHit() = False Then
                sim.CombatLog.write(T & vbTab & "Pestilence fail")
                MissCount = MissCount + 1
                Return
            End If
            MyBase.Use()

            sim.CombatLog.write(T & vbTab & "Pestilence")
            HitCount = HitCount + 1

            Dim Tar As Targets.Target
            If sim.Targets.MainTarget.BloodPlague.FadeAt > T Then
                For Each Tar In sim.Targets.AllTargets
                    If Tar.Equals(sim.Targets.MainTarget) = False Then
                        Tar.BloodPlague.Apply(T, Tar)
                    End If
                Next
            End If
            If sim.Targets.MainTarget.FrostFever.FadeAt > T Then
                For Each Tar In sim.Targets.AllTargets
                    If Tar.Equals(sim.Targets.MainTarget) = False Then
                        Tar.FrostFever.Apply(T, Tar)
                    End If
                Next
            End If

            If sim.Character.Glyph.GoD Then
                If sim.Targets.MainTarget.BloodPlague.FadeAt > T Then
                    sim.Targets.MainTarget.BloodPlague.Refresh(T)
                End If
                If sim.Targets.MainTarget.FrostFever.FadeAt > T Then
                    sim.Targets.MainTarget.FrostFever.Refresh(T)
                End If
            End If


        End Sub
        Function PerfectUsage(ByVal T As Double) As Boolean
            Dim tmp1 As Long
            Dim tmp2 As Long

            Dim blood As Boolean

            If sim.Character.Talents("SummonGargoyle") = 1 Then
                blood = sim.Runes.BloodOnly(T)
            Else
                blood = sim.Runes.AnyBlood(T)
            End If
            If blood Then
                tmp1 = Math.Min(sim.Targets.MainTarget.BloodPlague.FadeAt, sim.Targets.MainTarget.FrostFever.FadeAt)
                If tmp1 < T Then
                    Return False
                End If

                If sim.Targets.MainTarget.BloodPlague.FadeAt <> sim.Targets.MainTarget.FrostFever.FadeAt Then
                    Return True
                End If
                If tmp1 - T > 800 Then Return False
                tmp2 = sim.Runes.GetNextBloodCD(T)
                If tmp2 > tmp1 Or tmp2 = 0 Then
                    Return True
                End If
            Else
                T = T
            End If
            Return False
        End Function

    End Class

End Namespace