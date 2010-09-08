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
        End Sub

        Function use(ByVal T As Double) As Boolean
            UseGCD(T)
            If DoMySpellHit = False Then
                Sim.CombatLog.write(T & vbTab & "Pestilence fail")
                MissCount = MissCount + 1
                Return False
            End If
            Sim.RunicPower.add(15)
            Sim.CombatLog.write(T & vbTab & "Pestilence")
            HitCount = HitCount + 1

            If Sim.proc.ReapingBotN.TryMe(T) Then
                Sim.Runes.UseBlood(T, True)
            Else
                Sim.Runes.UseBlood(T, False)
            End If

            Dim Tar As Targets.Target
            If Sim.Targets.MainTarget.BloodPlague.FadeAt > T Then
                For Each Tar In Sim.Targets.AllTargets
                    If Tar.Equals(Sim.Targets.MainTarget) = False Then
                        Tar.BloodPlague.Apply(T, Tar)
                    End If
                Next
            End If
            If Sim.Targets.MainTarget.FrostFever.FadeAt > T Then
                For Each Tar In Sim.Targets.AllTargets
                    If Tar.Equals(Sim.Targets.MainTarget) = False Then
                        Tar.FrostFever.Apply(T, Tar)
                    End If
                Next
            End If

            If Sim.Character.Glyph.Disease Then
                If Sim.Targets.MainTarget.BloodPlague.FadeAt > T Then
                    Sim.Targets.MainTarget.BloodPlague.Refresh(T)
                End If
                If Sim.Targets.MainTarget.FrostFever.FadeAt > T Then
                    Sim.Targets.MainTarget.FrostFever.Refresh(T)
                End If
            End If

            Return True
        End Function
        Function PerfectUsage(ByVal T As Double) As Boolean
            Dim tmp1 As Long
            Dim tmp2 As Long

            Dim blood As Boolean

            If Sim.Character.Talents.Talent("Gargoyle").Value = 1 Then
                blood = Sim.Runes.BloodOnly(T)
            Else
                blood = Sim.Runes.AnyBlood(T)
            End If
            If blood Then
                tmp1 = Math.Min(Sim.Targets.MainTarget.BloodPlague.FadeAt, Sim.Targets.MainTarget.FrostFever.FadeAt)
                If tmp1 < T Then
                    Return False
                End If

                If Sim.Targets.MainTarget.BloodPlague.FadeAt <> Sim.Targets.MainTarget.FrostFever.FadeAt Then
                    Return True
                End If
                If tmp1 - T > 800 Then Return False
                tmp2 = Sim.Runes.GetNextBloodCD(T)
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