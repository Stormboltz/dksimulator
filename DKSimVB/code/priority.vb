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
					If sim.BloodTap.IsAvailable(Timestamp) and rune1.death = false and rune2.death = false    Then
						sim.BloodTap.Use(Timestamp)
						'debug.Print("BT")
					End If
					
					
				Case "GhoulFrenzy"
					if ghoul.IsFrenzyAvailable(Timestamp) and CanUseGCD(Timestamp)  Then
						ghoul.Frenzy(Timestamp)
						exit sub
					end if
					
				Case "ScourgeStrike"
					If runes.FU(TimeStamp) = True and CanUseGCD(Timestamp)  Then
						sim.ScourgeStrike.ApplyDamage(TimeStamp)
						'debug.Print("SS")
						exit sub
					End If
				Case "PlagueStrike"
					If runes.Unholy(TimeStamp) and CanUseGCD(Timestamp) Then
						sim.PlagueStrike.ApplyDamage(TimeStamp)
						'debug.Print("PS")
						exit sub
					End If
				Case "DRMObliterate"
					If runes.DRMFU(TimeStamp) = True and CanUseGCD(Timestamp) Then
						sim.Obliterate.ApplyDamage(TimeStamp)
						'debug.Print("OB")
						exit sub
					End If
				Case "Obliterate"
					If runes.FU(TimeStamp) = True and CanUseGCD(Timestamp) Then
						sim.Obliterate.ApplyDamage(TimeStamp)
						'debug.Print("OB")
						exit sub
					End If
					
				Case "KMFrostStrike"
					If sim.FrostStrike.isAvailable(TimeStamp) = True and proc.KillingMachine and CanUseGCD(Timestamp)  Then
						sim.FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
				Case "FrostStrike"
					If sim.FrostStrike.isAvailable(TimeStamp) = True and CanUseGCD(Timestamp) Then
						sim.FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
					
				Case "FrostStrikeMaxRp"
					If RunicPower.MaxValue = RunicPower.Value and CanUseGCD(Timestamp)  Then
						sim.FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
				Case "DRMDeathStrike"
					If runes.DRMFU(TimeStamp) and CanUseGCD(Timestamp) Then
						sim.DeathStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If	
				Case "DeathStrike"
					If runes.FU(TimeStamp) and CanUseGCD(Timestamp) Then
						sim.DeathStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If
				Case "BloodStrike"
					If runes.Blood(TimeStamp) and CanUseGCD(Timestamp) Then
						sim.BloodStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If
					
				Case "HeartStrike"
					If runes.Blood(TimeStamp) = True and CanUseGCD(Timestamp) Then
						sim.Heartstrike.ApplyDamage(TimeStamp)
						'debug.Print("HS")
						exit sub
					End If
				Case "Rime"
					If proc.rime and sim.HowlingBlast.isAvailable(TimeStamp) and CanUseGCD(Timestamp) Then
						sim.HowlingBlast.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "FrostFever"
					If glyph.Disease Then
						if sim.Pestilence.PerfectUsage(TimeStamp) then
							sim.Pestilence.use(TimeStamp)
							Exit Sub
						Else
							If sim.FrostFever.isActive(TimeStamp) = False or sim.pestilence.FFToReapply Then
								If talentfrost.HowlingBlast = 1 And glyph.HowlingBlast And sim.HowlingBlast.isAvailable(TimeStamp)  Then
									If proc.rime Or runes.FU(TimeStamp) Then
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
						If sim.FrostFever.PerfectUsage(TimeStamp) = true Then
							If talentfrost.HowlingBlast = 1 And glyph.HowlingBlast And sim.HowlingBlast.isAvailable(TimeStamp)  Then
								If proc.rime Or runes.FU(TimeStamp) Then
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
					
					
				Case "BloodPlague"
					If glyph.Disease Then
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
						
						If sim.BloodPlague.PerfectUsage(TimeStamp) = true then
							If runes.Unholy(TimeStamp) = True Then
								sim.PlagueStrike.ApplyDamage(TimeStamp)
								exit sub
							End If
						End If
					End If
					
				Case "IcyTouch"
					If runes.Frost(TimeStamp) = True and CanUseGCD(Timestamp) Then
						sim.IcyTouch.ApplyDamage(TimeStamp)
						exit sub
					End If
					
				Case "DeathCoilMaxRp"
					If RunicPower.MaxValue = RunicPower.Value and CanUseGCD(Timestamp) Then
						sim.deathcoil.ApplyDamage(TimeStamp,False)
						'debug.Print("DC")
						exit sub
					End If
				Case "DeathCoil"
					If sim.deathcoil.isAvailable(TimeStamp) = True and CanUseGCD(Timestamp) Then
						sim.deathcoil.ApplyDamage(TimeStamp,False)
						'debug.Print("DC")
						exit sub
					End If
				Case "BloodBoil"
					If runes.Blood(TimeStamp) = True and CanUseGCD(Timestamp) Then
						sim.BloodBoil.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "Pestilance"
					
				Case "HowlingBlast"
					If sim.HowlingBlast.isAvailable(TimeStamp) Then
						If proc.rime Or runes.FU(TimeStamp) and CanUseGCD(Timestamp)  Then
							sim.HowlingBlast.ApplyDamage(TimeStamp)
							runes.UnReserveFU(TimeStamp)
							Exit Sub
						Else
							runes.ReserveFU(TimeStamp)
						End If
					Else
					End If
				Case "KMHowlingBlast"
					If sim.HowlingBlast.isAvailable(TimeStamp) and proc.KillingMachine Then
						If proc.rime Or runes.FU(TimeStamp) and CanUseGCD(Timestamp) Then
							sim.HowlingBlast.ApplyDamage(TimeStamp)
							runes.UnReserveFU(TimeStamp)
							Exit Sub
						Else
							runes.ReserveFU(TimeStamp)
						End If
					Else
					End If
				Case "KMRime"
					If Proc.Rime and proc.KillingMachine and CanUseGCD(Timestamp)  Then
						sim.HowlingBlast.ApplyDamage(TimeStamp)
					Else
					End If
				Case "DeathandDecay"
					If sim.DeathAndDecay.isAvailable(TimeStamp) and CanUseGCD(Timestamp) Then
						sim.DeathAndDecay.Apply(TimeStamp)
						Exit Sub
					End If
			End Select
			doNext:
		Next
	End sub
	
	
End Module
