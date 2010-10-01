'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 19:42
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator
    Friend Class priority
        Friend prio As New Collection

        Private sim As Sim
        Sub New(ByVal S As Sim)
            sim = S
            'Runes = sim.Runes
        End Sub

        Function runes() As Simulator.WowObjects.Runes.runes
            Return sim.Runes
        End Function


        Sub DoNext(ByVal TimeStamp As Long)

            If sim.Rotation.MyIntro.Count > 0 And sim.Rotation.IntroStep < sim.Rotation.MyIntro.Count Then Exit Sub

            Dim HighestPrio As Integer
            HighestPrio = 1

            For Each item As String In prio
                Select Case item
                    Case "ShadowInfusion"
                        If sim.Ghoul.ShadowInfusion.Equiped Then
                            If sim.Ghoul.ShadowInfusion.IsAvailable(sim.TimeStamp) Then
                                If sim.Ghoul.ShadowInfusion.Stack < 5 Then
                                    If sim.DeathCoil.isAvailable() Then
                                        sim.DeathCoil.ApplyDamage(TimeStamp)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Else
                            'removeme from prio
                        End If
                    Case "SuddenDoom"
                        If sim.proc.SuddenDoom.IsActive Then
                            sim.DeathCoil.ApplyDamage(TimeStamp)
                            Return
                        End If
                    Case "RuneStrike"
                        If sim.RuneStrike.IsAvailable Then
                            If sim.RunicPower.CheckRS(20) Then
                                sim.RuneStrike.ApplyDamage(TimeStamp)
                                Return
                            End If
                        End If
                    Case "CinderDisease"
                        If sim.Targets.MainTarget.BloodPlague.Cinder = True And sim.Targets.MainTarget.FrostFever.Cinder = True Then GoTo doNext
                        If sim.RuneForge.CheckCinderglacier(False) = 0 Then GoTo doNext

                        If sim.Targets.MainTarget.BloodPlague.Cinder = False Then
                            If sim.PlagueStrike.IsAvailable Then
                                sim.PlagueStrike.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                        End If
                        If sim.Targets.MainTarget.FrostFever.Cinder = False Then
                            If sim.IcyTouch.IsAvailable Then
                                sim.IcyTouch.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                        End If


                    Case "BloodTap"
                        If sim.BloodTap.IsAvailable() And (Not sim.Runes.Blood) Then
                            sim.BloodTap.Use()
                            Exit Sub
                        End If
                    Case "BoneShield"
                        If sim.BoneShield.IsAvailable() Then
                            sim.BoneShield.Use()
                            Exit Sub
                        End If

                    Case "DarkTransformation"
                        If sim.DarkTransformation.IsAvailable Then
                            sim.DarkTransformation.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "FesteringStrike"
                        If sim.FesteringStrike.IsAvailable Then
                            sim.FesteringStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If

                    Case "ScourgeStrike"
                        If sim.ScourgeStrike.IsAvailable Then
                            sim.ScourgeStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "PlagueStrike"
                        If sim.PlagueStrike.IsAvailable Then
                            sim.PlagueStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "KMObliterate"
                        If sim.proc.KillingMachine.IsActive Then
                            If sim.Obliterate.IsAvailable Then
                                sim.Obliterate.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                        End If
                    Case "Obliterate"
                        If sim.Obliterate.IsAvailable Then
                            sim.Obliterate.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If

                    Case "KMFrostStrike"
                        If sim.proc.KillingMachine.IsActive Then
                            If sim.FrostStrike.IsAvailable(TimeStamp) = True Then
                                sim.FrostStrike.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                        End If

                    Case "FrostStrike"
                        If sim.FrostStrike.IsAvailable(TimeStamp) = True Then
                            sim.FrostStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If

                    Case "FrostStrikeMaxRp"
                        If sim.RunicPower.CheckMax(20) Then
                            sim.FrostStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "DRMDeathStrike"
                        If runes.DRMFU() Then
                            sim.DeathStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "DeathStrike"
                        If sim.DeathStrike.IsAvailable Then
                            sim.DeathStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "BloodStrike"
                        If sim.BloodStrike.IsAvailable Then
                            If sim.BoneShieldUsageStyle = 1 Then
                                If sim.BoneShield.IsAvailable() Then
                                    sim.BoneShield.Use()
                                    Exit Sub
                                End If
                                If sim.PillarOfFrost.IsAvailable() Then
                                    sim.PillarOfFrost.Use()
                                    Exit Sub
                                End If
                            End If
                            sim.BloodStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "HeartStrike"
                        If sim.HeartStrike.IsAvailable Then
                            sim.HeartStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "Rime"
                        If sim.proc.Rime.IsActive Then
                            sim.HowlingBlast.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "FrostFever"
                        If sim.Targets.Count > 1 And sim.KeepDiseaseOnOthersTarget Then
                            If sim.Targets.MainTarget.FrostFever.isActive(TimeStamp) And sim.Targets.MainTarget.BloodPlague.isActive(TimeStamp) Then
                                If sim.Targets.IsFrostFeverOnAll(TimeStamp) = False And sim.Targets.IsBloodPlagueOnAll(TimeStamp) = False Then
                                    If sim.Pestilence.IsAvailable Then
                                        sim.Pestilence.use()
                                    End If
                                End If
                            End If
                        End If
                        If sim.Targets.MainTarget.FrostFever.PerfectUsage(TimeStamp) = True Or sim.Targets.MainTarget.FrostFever.ToReApply Then
                            If sim.HowlingBlast.Glyphed And sim.HowlingBlast.isAvailable() Then
                                sim.HowlingBlast.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                            If sim.IcyTouch.IsAvailable Then
                                sim.IcyTouch.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                        End If
                    Case "EmpowerRuneWeapon"
                        If sim.ERW.CD <= TimeStamp Then
                            sim.ERW.Use()
                        End If
                    Case "BloodPlague"
                        If sim.Targets.Count > 1 And sim.KeepDiseaseOnOthersTarget Then
                            If sim.Targets.MainTarget.FrostFever.isActive(TimeStamp) And sim.Targets.MainTarget.BloodPlague.isActive(TimeStamp) Then
                                If sim.Targets.IsFrostFeverOnAll(TimeStamp) = False And sim.Targets.IsBloodPlagueOnAll(TimeStamp) = False Then
                                    If sim.Pestilence.IsAvailable Then
                                        sim.Pestilence.use()
                                    End If
                                End If
                            End If
                        End If
                        If sim.Targets.MainTarget.BloodPlague.PerfectUsage(TimeStamp) Or sim.Targets.MainTarget.BloodPlague.ToReApply Then
                            If sim.PlagueStrike.IsAvailable Then
                                sim.PlagueStrike.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                        End If
                    Case "IcyTouch"
                        If sim.IcyTouch.IsAvailable Then
                            sim.IcyTouch.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If

                    Case "DeathCoilMaxRp"
                        If sim.RunicPower.CheckMax(15) Then
                            sim.DeathCoil.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "DeathCoil"
                        If sim.DeathCoil.isAvailable() Then
                            sim.DeathCoil.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "BloodBoil"
                        If sim.BloodBoil.IsAvailable Then
                            If sim.BoneShieldUsageStyle = 3 Then
                                If sim.BoneShield.IsAvailable() Then
                                    sim.BoneShield.Use()
                                    Exit Sub
                                End If
                                If sim.PillarOfFrost.IsAvailable() Then
                                    sim.PillarOfFrost.Use()
                                    Exit Sub
                                End If
                            End If
                            sim.BloodBoil.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "CrimsonScourge"
                        If sim.proc.CrimsonScourge.IsActive Then
                            sim.BloodBoil.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "ScarletFever"
                        If Not sim.proc.ScarletFever.Effects(0).IsActive Then
                            If sim.BloodBoil.IsAvailable Then
                                sim.BloodBoil.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                        End If
                    Case "HowlingBlast"
                        If sim.HowlingBlast.isAvailable() Then
                            sim.HowlingBlast.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If

                    Case "FadeRime"
                        If sim.proc.Rime.IsActive Then
                            If sim.proc.Rime.Fade < TimeStamp + 250 Then
                                If sim.HowlingBlast.isAvailable() Then
                                    sim.HowlingBlast.ApplyDamage(TimeStamp)
                                    Exit Sub
                                End If
                            End If
                        End If
                    Case "DeathandDecay"
                        If sim.DeathandDecay.isAvailable() Then
                            sim.DeathandDecay.Apply(TimeStamp)
                            Exit Sub
                        End If
                    Case "Horn"
                        If sim.Horn.isAvailable() Then
                            sim.Horn.use()
                            Exit Sub
                        End If
                End Select
doNext:
            Next

        End Sub


    End Class
End Namespace
