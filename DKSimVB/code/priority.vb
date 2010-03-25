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
					if sim.BloodPlague.Cinder = true and sim.FrostFever.Cinder = true then goto doNext
					If sim.RuneForge.CinderglacierProc = 0 Then goto doNext
					If runes.Unholy(TimeStamp) and sim.CanUseGCD(Timestamp) and sim.BloodPlague.Cinder = false  Then
						sim.PlagueStrike.ApplyDamage(TimeStamp)
						exit sub
					End If
					If runes.Frost(TimeStamp) and sim.CanUseGCD(Timestamp) and sim.FrostFever.Cinder = false  Then
						sim.IcyTouch.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "BloodSync"
					If prio.Contains("BloodStrike") and sim.BloodToSync Then
						If runes.anyBlood(TimeStamp) And sim.CanUseGCD(Timestamp) Then
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
						'debug.Print("BT")
					End If
				Case "BoneShield"
					if sim.BoneShield.IsAvailable(Timestamp) Then
						sim.BoneShield.Use(Timestamp)
						exit sub
					End If
					
				Case "GhoulFrenzy"
					if sim.Frenzy.IsFrenzyAvailable(Timestamp) and sim.CanUseGCD(Timestamp)  Then
						sim.Frenzy.Frenzy(Timestamp)
						exit sub
					end if
					
				Case "ScourgeStrike"
					If runes.FU(TimeStamp) = True and sim.CanUseGCD(Timestamp)  Then
						sim.ScourgeStrike.ApplyDamage(TimeStamp)
						'debug.Print("SS")
						exit sub
					End If
				Case "PlagueStrike"
					If runes.Unholy(TimeStamp) and sim.CanUseGCD(Timestamp) Then
						sim.PlagueStrike.ApplyDamage(TimeStamp)
						'debug.Print("PS")
						exit sub
					End If
				Case "DRMObliterate"
					If runes.DRMFU(TimeStamp) = True and sim.CanUseGCD(Timestamp) Then
						sim.Obliterate.ApplyDamage(TimeStamp)
						'debug.Print("OB")
						exit sub
					End If
				Case "Obliterate"
					If runes.FU(TimeStamp) = True and sim.CanUseGCD(Timestamp) Then
						sim.Obliterate.ApplyDamage(TimeStamp)
						'debug.Print("OB")
						exit sub
					End If
					
				Case "KMFrostStrike"
					If sim.FrostStrike.isAvailable(TimeStamp) = True and sim.proc.KillingMachine.IsActive and sim.CanUseGCD(Timestamp)  Then
						sim.FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
				Case "FrostStrike"
					If sim.FrostStrike.isAvailable(TimeStamp) = True and sim.CanUseGCD(Timestamp) Then
						sim.FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
					
				Case "FrostStrikeMaxRp"
					If Sim.RunicPower.MaxValue = Sim.RunicPower.Value and sim.CanUseGCD(Timestamp)  Then
						sim.FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
				Case "DRMDeathStrike"
					If runes.DRMFU(TimeStamp) and sim.CanUseGCD(Timestamp) Then
						sim.DeathStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If
				Case "DeathStrike"
					If runes.FU(TimeStamp) and sim.CanUseGCD(Timestamp) Then
						sim.DeathStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If
				Case "Desolation"
					If runes.anyBlood(TimeStamp) And sim.CanUseGCD(Timestamp) Then
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
						If Not sim.proc.Desolation.IsActiveAt(TimeStamp) Then
							sim.BloodStrike.ApplyDamage(TimeStamp)
							Exit Sub
						End If
					End If
				Case "BloodStrike"
					If sim.TalentUnholy.Reaping = 3 or sim.TalentFrost.BloodoftheNorth = 3 Then
						If runes.Blood(TimeStamp) And sim.CanUseGCD(Timestamp) Then
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
							Exit Sub
						End If
					Else
						If runes.anyBlood(TimeStamp) And sim.CanUseGCD(Timestamp) Then
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
					
					
					
					
					
				Case "HeartStrike"
					If runes.anyBlood(TimeStamp) = True and sim.CanUseGCD(Timestamp) Then
						sim.Heartstrike.ApplyDamage(TimeStamp)
						'debug.Print("HS")
						exit sub
					End If
				Case "Rime"
					If sim.proc.rime.IsActive and sim.HowlingBlast.isAvailable(TimeStamp) and sim.CanUseGCD(Timestamp) Then
						sim.HowlingBlast.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "FrostFever"
					If sim.NumberOfEnemies > 1 And sim.FrostFever.OtherTargetsFade < TimeStamp And sim.BloodPlague.OtherTargetsFade < TimeStamp and sim.KeepDiseaseOnOthersTarget Then
						If runes.Blood(TimeStamp) Then
							sim.Pestilence.use(TimeStamp)
						End If
					End If
					If sim.glyph.Disease Then
						if sim.Pestilence.PerfectUsage(TimeStamp) then
							sim.Pestilence.use(TimeStamp)
							Exit Sub
						Else
							If sim.FrostFever.isActive(TimeStamp) = False or sim.pestilence.FFToReapply Then
								If sim.TalentFrost.HowlingBlast = 1 And sim.glyph.HowlingBlast And sim.HowlingBlast.isAvailable(TimeStamp)  Then
									If sim.proc.rime.IsActive Or runes.FU(TimeStamp) Then
										sim.HowlingBlast.ApplyDamage(TimeStamp)
										exit sub
									End If
								end if
								if runes.Frost(TimeStamp) = True Then
									sim.IcyTouch.ApplyDamage(TimeStamp)
									Exit Sub
								End If
							End If
						End If
					Else
						If sim.FrostFever.PerfectUsage(TimeStamp) = true or sim.FrostFever.ToReApply Then
							If sim.TalentFrost.HowlingBlast = 1 And sim.glyph.HowlingBlast And sim.HowlingBlast.isAvailable(TimeStamp)  Then
								If sim.proc.rime.IsActive Or runes.FU(TimeStamp) Then
									sim.HowlingBlast.ApplyDamage(TimeStamp)
									exit sub
								End If
							end if
							if runes.Frost(TimeStamp) = True Then
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
					If sim.glyph.Disease Then
						if sim.Pestilence.PerfectUsage(TimeStamp)  then
							sim.Pestilence.use(TimeStamp)
							Exit Sub
						Else
							If sim.BloodPlague.isActive(TimeStamp) = False or sim.pestilence.BPToReapply then
								If runes.Unholy(TimeStamp) = True Then
									sim.PlagueStrike.ApplyDamage(TimeStamp)
									exit sub
								End If
							End If
						End If
						
					Else
						
						If sim.BloodPlague.PerfectUsage(TimeStamp) or sim.BloodPlague.ToReApply then
							If runes.Unholy(TimeStamp) = True Then
								sim.PlagueStrike.ApplyDamage(TimeStamp)
								exit sub
							End If
						End If
					End If
					
				Case "IcyTouch"
					If runes.Frost(TimeStamp) = True and sim.CanUseGCD(Timestamp) Then
						sim.IcyTouch.ApplyDamage(TimeStamp)
						exit sub
					End If
					
				Case "DeathCoilMaxRp"
					If Sim.RunicPower.MaxValue = Sim.RunicPower.Value and sim.CanUseGCD(Timestamp) Then
						sim.deathcoil.ApplyDamage(TimeStamp,False)
						'debug.Print("DC")
						exit sub
					End If
				Case "DeathCoil"
					If sim.deathcoil.isAvailable(TimeStamp) = True and sim.CanUseGCD(Timestamp) Then
						sim.deathcoil.ApplyDamage(TimeStamp,False)
						'debug.Print("DC")
						exit sub
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
					Else
					End If
				Case "FadeRime"
					If  sim.HowlingBlast.isAvailable(TimeStamp) and sim.proc.Rime.IsActive and sim.proc.Rime.Fade< TimeStamp+250 and sim.CanUseGCD(Timestamp)  Then
						sim.HowlingBlast.ApplyDamage(TimeStamp)
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
