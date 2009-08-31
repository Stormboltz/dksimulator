'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 19:42
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module priority
	Friend prio as New Collection
	sub DoNext(TimeStamp As long )
		Dim HighestPrio As Integer
		HighestPrio = 1
		
		For Each item as String In priority.prio
			Select Case item
				Case "BloodTap"
					If BloodTap.IsAvailable(Timestamp) and rune1.death = false and rune2.death = false    Then
						BloodTap.Use(Timestamp)
						'debug.Print("BT")
					End If
					
					
				Case "GhoulFrenzy"
					if ghoul.IsFrenzyAvailable(Timestamp) and CanUseGCD(Timestamp)  Then
						ghoul.Frenzy(Timestamp)
						exit sub
					end if
					
				Case "ScourgeStrike"
					If runes.FU(TimeStamp) = True and CanUseGCD(Timestamp)  Then
						ScourgeStrike.ApplyDamage(TimeStamp)
						'debug.Print("SS")
						exit sub
					End If
				Case "PlagueStrike"
					If runes.Unholy(TimeStamp) and CanUseGCD(Timestamp) Then
						PlagueStrike.ApplyDamage(TimeStamp)
						'debug.Print("PS")
						exit sub
					End If
				Case "DRMObliterate"
					If runes.DRMFU(TimeStamp) = True and CanUseGCD(Timestamp) Then
						Obliterate.ApplyDamage(TimeStamp)
						'debug.Print("OB")
						exit sub
					End If
				Case "Obliterate"
					If runes.FU(TimeStamp) = True and CanUseGCD(Timestamp) Then
						Obliterate.ApplyDamage(TimeStamp)
						'debug.Print("OB")
						exit sub
					End If
					
				Case "KMFrostStrike"
					If FrostStrike.isAvailable(TimeStamp) = True and proc.KillingMachine and CanUseGCD(Timestamp)  Then
						FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
				Case "FrostStrike"
					If FrostStrike.isAvailable(TimeStamp) = True and CanUseGCD(Timestamp) Then
						FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
					
				Case "FrostStrikeMaxRp"
					If RunicPower.MaxValue = RunicPower.Value and CanUseGCD(Timestamp)  Then
						FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
				Case "DRMDeathStrike"
					If runes.DRMFU(TimeStamp) and CanUseGCD(Timestamp) Then
						DeathStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If	
				Case "DeathStrike"
					If runes.FU(TimeStamp) and CanUseGCD(Timestamp) Then
						DeathStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If
				Case "BloodStrike"
					If runes.Blood(TimeStamp) and CanUseGCD(Timestamp) Then
						BloodStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If
					
				Case "HeartStrike"
					If runes.Blood(TimeStamp) = True and CanUseGCD(Timestamp) Then
						Heartstrike.ApplyDamage(TimeStamp)
						'debug.Print("HS")
						exit sub
					End If
				Case "Rime"
					If proc.rime and HowlingBlast.isAvailable(TimeStamp) and CanUseGCD(Timestamp) Then
						HowlingBlast.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "FrostFever"
					If glyph.Disease Then
						if Pestilence.PerfectUsage(TimeStamp) then
							Pestilence.use(TimeStamp)
							Exit Sub
						Else
							If FrostFever.isActive(TimeStamp) = False or FFToReapply Then
								If talentfrost.HowlingBlast = 1 And glyph.HowlingBlast And HowlingBlast.isAvailable(TimeStamp)  Then
									If proc.rime Or runes.FU(TimeStamp) Then
										HowlingBlast.ApplyDamage(TimeStamp)
										exit sub
									End If
								end if
								if runes.Frost(TimeStamp) = True Then
									IcyTouch.ApplyDamage(TimeStamp)
									Exit Sub
								End If
							End If
						End If
					Else
						If FrostFever.PerfectUsage(TimeStamp) = true Then
							If talentfrost.HowlingBlast = 1 And glyph.HowlingBlast And HowlingBlast.isAvailable(TimeStamp)  Then
								If proc.rime Or runes.FU(TimeStamp) Then
									HowlingBlast.ApplyDamage(TimeStamp)
									exit sub
								End If
							end if
							if runes.Frost(TimeStamp) = True Then
								IcyTouch.ApplyDamage(TimeStamp)
								Exit Sub
							End If
						End If
					End If
					
					
				Case "BloodPlague"
					If glyph.Disease Then
						if Pestilence.PerfectUsage(TimeStamp)  then
							Pestilence.use(TimeStamp)
							Exit Sub
						Else
							If BloodPlague.isActive(TimeStamp) = False or BPToReapply then
								If runes.Unholy(TimeStamp) = True Then
									PlagueStrike.ApplyDamage(TimeStamp)
									exit sub
								End If
							End If
						End If
						
					Else
						
						If BloodPlague.PerfectUsage(TimeStamp) = true then
							If runes.Unholy(TimeStamp) = True Then
								PlagueStrike.ApplyDamage(TimeStamp)
								exit sub
							End If
						End If
					End If
					
				Case "IcyTouch"
					If runes.Frost(TimeStamp) = True and CanUseGCD(Timestamp) Then
						IcyTouch.ApplyDamage(TimeStamp)
						exit sub
					End If
					
				Case "DeathCoilMaxRp"
					If RunicPower.MaxValue = RunicPower.Value and CanUseGCD(Timestamp) Then
						deathcoil.ApplyDamage(TimeStamp,False)
						'debug.Print("DC")
						exit sub
					End If
				Case "DeathCoil"
					If deathcoil.isAvailable(TimeStamp) = True and CanUseGCD(Timestamp) Then
						deathcoil.ApplyDamage(TimeStamp,False)
						'debug.Print("DC")
						exit sub
					End If
				Case "BloodBoil"
					If runes.Blood(TimeStamp) = True and CanUseGCD(Timestamp) Then
						BloodBoil.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "Pestilance"
					
				Case "HowlingBlast"
					If HowlingBlast.isAvailable(TimeStamp) Then
						If proc.rime Or runes.FU(TimeStamp) and CanUseGCD(Timestamp)  Then
							HowlingBlast.ApplyDamage(TimeStamp)
							runes.UnReserveFU(TimeStamp)
							Exit Sub
						Else
							runes.ReserveFU(TimeStamp)
						End If
					Else
					End If
				Case "KMHowlingBlast"
					If HowlingBlast.isAvailable(TimeStamp) and proc.KillingMachine Then
						If proc.rime Or runes.FU(TimeStamp) and CanUseGCD(Timestamp) Then
							HowlingBlast.ApplyDamage(TimeStamp)
							runes.UnReserveFU(TimeStamp)
							Exit Sub
						Else
							runes.ReserveFU(TimeStamp)
						End If
					Else
					End If
				Case "KMRime"
					If Proc.Rime and proc.KillingMachine and CanUseGCD(Timestamp)  Then
						HowlingBlast.ApplyDamage(TimeStamp)
					Else
					End If
				Case "DeathandDecay"
					If DeathAndDecay.isAvailable(TimeStamp) and CanUseGCD(Timestamp) Then
						DeathAndDecay.Apply(TimeStamp)
						Exit Sub
					End If
			End Select
			doNext:
		Next
	End sub
	
	
End Module
