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
                    Case "RuneStrike"
                        If sim.RuneStrike.trigger Then
                            If sim.RunicPower.CheckRS(20) And sim.CanUseGCD(TimeStamp) Then
                                sim.RuneStrike.ApplyDamage(TimeStamp)
                                Return
                            End If
                        End If


                    Case "CinderDisease"
                        If sim.Targets.MainTarget.BloodPlague.Cinder = True And sim.Targets.MainTarget.FrostFever.Cinder = True Then GoTo doNext
                        If sim.RuneForge.CheckCinderglacier(False) = 0 Then GoTo doNext
                        If runes.Unholy() And sim.CanUseGCD(TimeStamp) And sim.Targets.MainTarget.BloodPlague.Cinder = False Then
                            sim.PlagueStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                        If runes.Frost(TimeStamp) And sim.CanUseGCD(TimeStamp) And sim.Targets.MainTarget.FrostFever.Cinder = False Then
                            sim.IcyTouch.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If

                    Case "BloodTap"
                        If sim.BloodTap.IsAvailable() And sim.Runes.BloodRune1.death = False And sim.Runes.BloodRune2.death = False Then
                            sim.BloodTap.Use()
                            'Diagnostics.Debug.WriteLine("BT")
                        End If
                    Case "BoneShield"
                        If sim.BoneShield.IsAvailable() Then
                            sim.BoneShield.Use()
                            Exit Sub
                        End If

                        'Case "GhoulFrenzy"
                        '    If sim.Frenzy.IsFrenzyAvailable(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
                        '        sim.Frenzy.Frenzy(TimeStamp)
                        '        Exit Sub
                        '    End If
                    Case "DarkTransformation"
                        If sim.DarkTransformation.IsAvailable Then
                            sim.DarkTransformation.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "FesteringStrike"
                        If runes.BF = True And sim.CanUseGCD(TimeStamp) Then
                            sim.FesteringStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("SS")
                            Exit Sub
                        End If

                    Case "ScourgeStrike"
                        If runes.Unholy() = True And sim.CanUseGCD(TimeStamp) Then
                            sim.ScourgeStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("SS")
                            Exit Sub
                        End If
                    Case "PlagueStrike"
                        If runes.Unholy() And sim.CanUseGCD(TimeStamp) Then
                            sim.PlagueStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("PS")
                            Exit Sub
                        End If
                    Case "DRMObliterate"
                        If runes.DRMFU(TimeStamp) = True And sim.CanUseGCD(TimeStamp) Then
                            sim.Obliterate.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("OB")
                            Exit Sub
                        End If
                    Case "Obliterate"
                        If runes.FU(TimeStamp) = True And sim.CanUseGCD(TimeStamp) Then
                            sim.Obliterate.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("OB")
                            Exit Sub
                        End If

                    Case "KMFrostStrike"
                        If sim.FrostStrike.isAvailable(TimeStamp) = True And sim.proc.KillingMachine.IsActive And sim.CanUseGCD(TimeStamp) Then
                            sim.FrostStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("FS")
                            Exit Sub
                        End If
                    Case "FrostStrike"
                        If sim.FrostStrike.isAvailable(TimeStamp) = True And sim.CanUseGCD(TimeStamp) Then
                            sim.FrostStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("FS")
                            Exit Sub
                        End If

                    Case "FrostStrikeMaxRp"
                        If sim.RunicPower.CheckMax(20) And sim.CanUseGCD(TimeStamp) Then
                            sim.FrostStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("FS")
                            Exit Sub
                        End If
                    Case "DRMDeathStrike"
                        If runes.DRMFU(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
                            sim.DeathStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("BS")
                            Exit Sub
                        End If
                    Case "DeathStrike"
                        If runes.FU(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
                            sim.DeathStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("BS")
                            Exit Sub
                        End If
                    Case "BloodStrike"
                        If sim.Character.Talents.Talent("Reaping").Value = 3 Or sim.Character.Talents.Talent("BloodoftheNorth").Value = 3 Then
                            If runes.Blood(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
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
                        Else
                            If runes.AnyBlood(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
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
                        End If





                    Case "HeartStrike"
                        If runes.AnyBlood(TimeStamp) = True And sim.CanUseGCD(TimeStamp) Then
                            sim.HeartStrike.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("HS")
                            Exit Sub
                        End If
                    Case "Rime"
                        If sim.HowlingBlast.isAvailable And sim.CanUseGCD(TimeStamp) Then
                            sim.HowlingBlast.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "FrostFever"
                        If sim.Targets.Count > 1 And sim.KeepDiseaseOnOthersTarget Then
                            If sim.Targets.MainTarget.FrostFever.isActive(TimeStamp) And sim.Targets.MainTarget.BloodPlague.isActive(TimeStamp) Then
                                If sim.Targets.IsFrostFeverOnAll(TimeStamp) = False And sim.Targets.IsBloodPlagueOnAll(TimeStamp) = False Then
                                    If runes.Blood(TimeStamp) Then
                                        sim.Pestilence.use()
                                    End If
                                End If
                            End If
                        End If
                        If sim.Character.Glyph.Disease Then
                            If sim.Pestilence.PerfectUsage(TimeStamp) Then
                                sim.Pestilence.use()
                                Exit Sub
                            Else
                                If sim.Targets.MainTarget.FrostFever.ShouldReapply(TimeStamp) Then
                                    If sim.Character.Glyph.HowlingBlast And sim.HowlingBlast.isAvailable() Then
                                        If sim.proc.Rime.IsActive Or runes.Frost(TimeStamp) Then
                                            sim.HowlingBlast.ApplyDamage(TimeStamp)
                                            Exit Sub
                                        End If
                                    End If
                                    If runes.Frost(TimeStamp) = True Then
                                        sim.IcyTouch.ApplyDamage(TimeStamp)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Else
                            If sim.Targets.MainTarget.FrostFever.PerfectUsage(TimeStamp) = True Or sim.Targets.MainTarget.FrostFever.ToReApply Then
                                If sim.Character.Glyph.HowlingBlast And sim.HowlingBlast.isAvailable() Then
                                    If sim.proc.Rime.IsActive Or runes.Frost(TimeStamp) Then
                                        sim.HowlingBlast.ApplyDamage(TimeStamp)
                                        Exit Sub
                                    End If
                                End If
                                If runes.Frost(TimeStamp) = True Then
                                    sim.IcyTouch.ApplyDamage(TimeStamp)
                                    Exit Sub
                                End If
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
                                    If runes.Blood(TimeStamp) Then
                                        sim.Pestilence.use()
                                    End If
                                End If
                            End If
                        End If
                        If sim.Character.Glyph.Disease Then
                            If sim.Pestilence.PerfectUsage(TimeStamp) Then
                                sim.Pestilence.use()
                                Exit Sub
                            Else
                                If sim.Targets.MainTarget.BloodPlague.ShouldReapply(TimeStamp) Then
                                    If runes.Unholy() = True Then
                                        sim.PlagueStrike.ApplyDamage(TimeStamp)
                                        Exit Sub
                                    End If
                                End If
                            End If

                        Else

                            If sim.Targets.MainTarget.BloodPlague.PerfectUsage(TimeStamp) Or sim.Targets.MainTarget.BloodPlague.ToReApply Then
                                If runes.Unholy() = True Then
                                    sim.PlagueStrike.ApplyDamage(TimeStamp)
                                    Exit Sub
                                End If
                            End If
                        End If

                    Case "IcyTouch"
                        If runes.Frost(TimeStamp) = True And sim.CanUseGCD(TimeStamp) Then
                            sim.IcyTouch.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If

                    Case "DeathCoilMaxRp"
                        If sim.RunicPower.CheckMax(0) And sim.CanUseGCD(TimeStamp) Then
                            sim.DeathCoil.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("DC")
                            Exit Sub
                        End If
                    Case "DeathCoil"
                        If sim.DeathCoil.isAvailable() = True And sim.CanUseGCD(TimeStamp) Then
                            sim.DeathCoil.ApplyDamage(TimeStamp)
                            'Diagnostics.Debug.WriteLine("DC")
                            Exit Sub
                        End If
                    Case "BloodBoil"
                        If (runes.Blood(TimeStamp) = True Or sim.proc.CrimsonScourge.IsActive) And sim.CanUseGCD(TimeStamp) Then
                            If sim.BoneShieldUsageStyle = 3 Then
                                If sim.BoneShield.IsAvailable() Then
                                    sim.BoneShield.Use()
                                    Exit Sub
                                End If
                                If sim.PillarOfFrost.IsAvailable() Then
                                    sim.PillarOfFrost.Use()
                                End If
                            End If
                            sim.BloodBoil.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case ("CrimsonScourge")
                        If Not sim.proc.CrimsonScourge.IsActive Then
                            sim.BloodBoil.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "ScarletFever"
                        If (runes.Blood(TimeStamp) = True Or sim.proc.CrimsonScourge.IsActive) Then
                            If Not sim.proc.ScarletFever.IsActive Then
                                sim.BloodBoil.ApplyDamage(TimeStamp)
                                Exit Sub
                            End If
                        End If
                    Case "Pestilance"

                    Case "HowlingBlast"
                        If sim.HowlingBlast.isAvailable() Then

                            sim.HowlingBlast.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "KMHowlingBlast"
                        If sim.HowlingBlast.isAvailable() And sim.proc.KillingMachine.IsActive() Then
                                sim.HowlingBlast.ApplyDamage(TimeStamp)
                                Exit Sub
                        End If
                    Case "KMRime"
                        If sim.HowlingBlast.isAvailable() And sim.proc.Rime.IsActive And sim.proc.KillingMachine.IsActive And sim.CanUseGCD(TimeStamp) Then
                            sim.HowlingBlast.ApplyDamage(TimeStamp)
                            Exit Sub
                        Else
                        End If
                    Case "FadeRime"
                        If sim.HowlingBlast.isAvailable() And sim.proc.Rime.IsActive And sim.proc.Rime.Fade < TimeStamp + 250 And sim.CanUseGCD(TimeStamp) Then
                            sim.HowlingBlast.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Case "DeathandDecay"
                        If sim.DeathandDecay.isAvailable() And sim.CanUseGCD(TimeStamp) Then
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
