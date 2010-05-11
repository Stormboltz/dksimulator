'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 19:42
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class priority
	Friend prio As New Collection
	
	Private sim as Sim
	Sub New(S As Sim)
		Sim = S
		'Runes = sim.Runes
	End Sub
	
	Function runes As runes.runes
		Return sim.Runes
	End Function
	
	
	Sub DoNext(TimeStamp As Long )
		
		If sim.Rotation.MyIntro.Count > 0 and sim.Rotation.IntroStep < sim.Rotation.MyIntro.Count Then exit sub
		
		Dim HighestPrio As Integer
		HighestPrio = 1
		
		For Each item as String In prio
			Select Case item
				Case "CinderDisease"
					if sim.Targets.MainTarget.BloodPlague.Cinder = true and sim.Targets.MainTarget.FrostFever.Cinder = true then goto doNext
					If sim.RuneForge.CheckCinderglacier(False) = 0 Then goto doNext
					If runes.Unholy(TimeStamp) and sim.CanUseGCD(Timestamp) and sim.Targets.MainTarget.BloodPlague.Cinder = false  Then
						sim.PlagueStrike.ApplyDamage(TimeStamp)
						exit sub
					End If
					If runes.Frost(TimeStamp) and sim.CanUseGCD(Timestamp) and sim.Targets.MainTarget.FrostFever.Cinder = false  Then
						sim.IcyTouch.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "BloodSync"
					If sim.character.glyph.Disease Then
						if sim.Pestilence.PerfectUsage(TimeStamp) then
							sim.Pestilence.use(TimeStamp)
							Exit Sub
						End If
					End If
					If prio.Contains("BloodStrike") and sim.BloodToSync Then
						If sim.runes.Blood(TimeStamp) And sim.CanUseGCD(Timestamp) Then
							If sim.BoneShieldUsageStyle = 1 Then
								If sim.BoneShield.IsAvailable(TimeStamp) Then
									sim.BoneShield.Use(TimeStamp)
									exit sub
								End If
								If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
									sim.UnbreakableArmor.Use(TimeStamp)
									exit sub
								End If
							End If
							sim.BloodStrike.ApplyDamage(TimeStamp)
							exit sub
						End If
					End If
				Case "BloodTap"
					If sim.BloodTap.IsAvailable(Timestamp) and sim.Runes.BloodRune1.death = false and sim.Runes.BloodRune2.death = false Then
						sim.BloodTap.Use(Timestamp)
                        'Diagnostics.Debug.WriteLine("BT")
                    End If
                Case "BoneShield"
                    If sim.BoneShield.IsAvailable(TimeStamp) Then
                        sim.BoneShield.Use(TimeStamp)
                        Exit Sub
                    End If

                Case "GhoulFrenzy"
                    If sim.Frenzy.IsFrenzyAvailable(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
                        sim.Frenzy.Frenzy(TimeStamp)
                        Exit Sub
                    End If

                Case "ScourgeStrike"
                    If runes.FU(TimeStamp) = True And sim.CanUseGCD(TimeStamp) Then
                        sim.ScourgeStrike.ApplyDamage(TimeStamp)
                        'Diagnostics.Debug.WriteLine("SS")
                        Exit Sub
                    End If
                Case "PlagueStrike"
                    If runes.Unholy(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
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
                Case "Desolation"
                    If runes.AnyBlood(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
                        If sim.BoneShieldUsageStyle = 1 Then
                            If sim.BoneShield.IsAvailable(TimeStamp) Then
                                sim.BoneShield.Use(TimeStamp)
                                Exit Sub
                            End If
                            If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
                                sim.UnbreakableArmor.Use(TimeStamp)
                                Exit Sub
                            End If
                        End If
                        If sim.BloodStrike.CheckDesolation(TimeStamp) Then
                            sim.BloodStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    End If
                Case "BloodStrike"
                    If sim.Character.TalentUnholy.Reaping = 3 Or sim.Character.TalentFrost.BloodoftheNorth = 3 Then
                        If runes.Blood(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
                            If sim.BoneShieldUsageStyle = 1 Then
                                If sim.BoneShield.IsAvailable(TimeStamp) Then
                                    sim.BoneShield.Use(TimeStamp)
                                    Exit Sub
                                End If
                                If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
                                    sim.UnbreakableArmor.Use(TimeStamp)
                                    Exit Sub
                                End If
                            End If
                            sim.BloodStrike.ApplyDamage(TimeStamp)
                            Exit Sub
                        End If
                    Else
                        If runes.AnyBlood(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
                            If sim.BoneShieldUsageStyle = 1 Then
                                If sim.BoneShield.IsAvailable(TimeStamp) Then
                                    sim.BoneShield.Use(TimeStamp)
                                    Exit Sub
                                End If
                                If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
                                    sim.UnbreakableArmor.Use(TimeStamp)
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
                    If sim.proc.Rime.IsActive And sim.HowlingBlast.isAvailable(TimeStamp) And sim.CanUseGCD(TimeStamp) Then
                        sim.HowlingBlast.ApplyDamage(TimeStamp)
                        Exit Sub
                    End If
                Case "FrostFever"
                    If sim.Targets.Count > 1 And sim.KeepDiseaseOnOthersTarget Then
                        If sim.Targets.MainTarget.FrostFever.isActive(TimeStamp) And sim.Targets.MainTarget.BloodPlague.isActive(TimeStamp) Then
                            If sim.Targets.IsFrostFeverOnAll(TimeStamp) = False And sim.Targets.IsBloodPlagueOnAll(TimeStamp) = False Then
                                If runes.Blood(TimeStamp) Then
                                    sim.Pestilence.use(TimeStamp)
                                End If
                            End If
                        End If
                    End If
                    If sim.Character.Glyph.Disease Then
                        If sim.Pestilence.PerfectUsage(TimeStamp) Then
                            sim.Pestilence.use(TimeStamp)
                            Exit Sub
                        Else
                            If sim.Targets.MainTarget.FrostFever.ShouldReapply(TimeStamp) Then
                                If sim.Character.TalentFrost.HowlingBlast = 1 And sim.Character.Glyph.HowlingBlast And sim.HowlingBlast.isAvailable(TimeStamp) Then
                                    If sim.proc.Rime.IsActive Or runes.FU(TimeStamp) Then
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
                            If sim.Character.TalentFrost.HowlingBlast = 1 And sim.Character.Glyph.HowlingBlast And sim.HowlingBlast.isAvailable(TimeStamp) Then
                                If sim.proc.Rime.IsActive Or runes.FU(TimeStamp) Then
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
                        sim.ERW.Use(TimeStamp)
                    End If
                Case "BloodPlague"
                    If sim.Targets.Count > 1 And sim.KeepDiseaseOnOthersTarget Then
                        If sim.Targets.MainTarget.FrostFever.isActive(TimeStamp) And sim.Targets.MainTarget.BloodPlague.isActive(TimeStamp) Then
                            If sim.Targets.IsFrostFeverOnAll(TimeStamp) = False And sim.Targets.IsBloodPlagueOnAll(TimeStamp) = False Then
                                If runes.Blood(TimeStamp) Then
                                    sim.Pestilence.use(TimeStamp)
                                End If
                            End If
                        End If
                    End If
                    If sim.Character.Glyph.Disease Then
                        If sim.Pestilence.PerfectUsage(TimeStamp) Then
                            sim.Pestilence.use(TimeStamp)
                            Exit Sub
                        Else
                            If sim.Targets.MainTarget.BloodPlague.ShouldReapply(TimeStamp) Then
                                If runes.Unholy(TimeStamp) = True Then
                                    sim.PlagueStrike.ApplyDamage(TimeStamp)
                                    Exit Sub
                                End If
                            End If
                        End If

                    Else

                        If sim.Targets.MainTarget.BloodPlague.PerfectUsage(TimeStamp) Or sim.Targets.MainTarget.BloodPlague.ToReApply Then
                            If runes.Unholy(TimeStamp) = True Then
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
                        sim.DeathCoil.ApplyDamage(TimeStamp, False)
                        'Diagnostics.Debug.WriteLine("DC")
                        Exit Sub
                    End If
                Case "DeathCoil"
                    If sim.DeathCoil.isAvailable(TimeStamp) = True And sim.CanUseGCD(TimeStamp) Then
                        sim.DeathCoil.ApplyDamage(TimeStamp, False)
                        'Diagnostics.Debug.WriteLine("DC")
                        Exit Sub
                    End If
				Case "BloodBoil"
					If runes.Blood(TimeStamp) = True And sim.CanUseGCD(Timestamp) Then
						If sim.BoneShieldUsageStyle = 3 Then
							If sim.BoneShield.IsAvailable(TimeStamp) Then
								sim.BoneShield.Use(TimeStamp)
								exit sub
							End If
							If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
								sim.UnbreakableArmor.Use(TimeStamp)
							End If
						End If
						sim.BloodBoil.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "Pestilance"
					
				Case "HowlingBlast"
					If sim.HowlingBlast.isAvailable(TimeStamp) Then
						If sim.proc.rime.IsActive Or runes.FU(TimeStamp) and sim.CanUseGCD(Timestamp)  Then
							sim.HowlingBlast.ApplyDamage(TimeStamp)
							runes.UnReserveFU(TimeStamp)
							Exit Sub
						Else
							runes.ReserveFU(TimeStamp)
						End If
					Else
					End If
				Case "KMHowlingBlast"
					If sim.HowlingBlast.isAvailable(TimeStamp) and sim.proc.KillingMachine.IsActive() Then
						If sim.proc.rime.IsActive Or runes.FU(TimeStamp) and sim.CanUseGCD(Timestamp) Then
							sim.HowlingBlast.ApplyDamage(TimeStamp)
							runes.UnReserveFU(TimeStamp)
							Exit Sub
						Else
							runes.ReserveFU(TimeStamp)
						End If
					Else
					End If
				Case "KMRime"
					If  sim.HowlingBlast.isAvailable(TimeStamp) and sim.proc.Rime.IsActive and sim.proc.KillingMachine.IsActive and sim.CanUseGCD(Timestamp)  Then
						sim.HowlingBlast.ApplyDamage(TimeStamp)
						exit sub
					Else
					End If
				Case "FadeRime"
					If  sim.HowlingBlast.isAvailable(TimeStamp) and sim.proc.Rime.IsActive and sim.proc.Rime.Fade< TimeStamp+250 and sim.CanUseGCD(Timestamp)  Then
						sim.HowlingBlast.ApplyDamage(TimeStamp)
						exit sub
					Else
					End If
					
				Case "DeathandDecay"
					If sim.DeathAndDecay.isAvailable(TimeStamp) and sim.CanUseGCD(Timestamp) Then
						sim.DeathAndDecay.Apply(TimeStamp)
						Exit Sub
					End If
				Case "Horn"
					If sim.Horn.isAvailable(TimeStamp) Then
						sim.Horn.use(TimeStamp)
						exit sub
					End If
			End Select
			doNext:
		Next
	End sub
	
	
End Class