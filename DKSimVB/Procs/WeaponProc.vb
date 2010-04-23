'
' Created by SharpDevelop.
' Date: 8/04/2010
' Time: 1:13 p.m.
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Class WeaponProc
	Inherits Proc
	Friend DamageType As String

	Sub New(S As Sim)
		MyBase.New(S)
		_Name = ""
	End Sub
	
	Sub BaseApplyMe(T As Long)
		MyBase.ApplyMe(T)
	End Sub
	
	Overrides Sub ApplyMe(T As Long)
		If DamageType = "" Then
			BaseApplyMe(T)
			Exit Sub
		End If
		CD = T + InternalCD * 100
		dim tmp as Double
		Select Case DamageType
			Case "Shadowmourne"
				If Fade <= T Then Stack = 0
				
				If Stack < 9 Then
					Stack +=1
					CD = T
					Fade = T + ProcLenght * 100
				Else
					Fade = CD
					If Rng3 < (0.17 - sim.MainStat.SpellHit) Then
						MissCount = MissCount + 1
						Exit sub
					End If
					tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					totalhit += tmp
					If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
					
					HitCount += 1
					AddUptime(T)
				End If
			Case "Bryntroll"
				If Rng3 < (0.17 - sim.MainStat.SpellHit) Then
					MissCount = MissCount + 1
					Exit sub
				End If
				tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
				HitCount = HitCount + 1
				totalhit += tmp
				
			Case "TinyAbomination"
				Me.Stack +=1
				If Me.Stack =8 Then
					Me.Stack=0
					if sim.MainStat.DualW then
						If Rng4 > 0.5 Then
							tmp = sim.MainHand.AvrgNonCrit(T)/2
							me.CD = T+1
							sim.TryOnMHHitProc
							me.CD = 0
						Else
							
							tmp = sim.offhand.AvrgNonCrit(T)/2
							Me.CD = T+1
							sim.TryOnOHHitProc
							me.CD = 0
						End If
					Else
						tmp = sim.MainHand.AvrgNonCrit(T)/2
					End If
					If RngCrit < sim.MainStat.crit Then
						tmp = tmp*2
						CritCount += 1
						TotalCrit += tmp
					else
						hitCount += 1
						TotalHit += tmp
					End If
				End If
			Case "DeathbringersWill"
				Dim RNG As Double
				RNG = Rng3
				AddUptime(T)
				If RNG < 0.33 Then
					ProcType = "str"
				ElseIf RNG < 0.66 Then
					ProcType = "crit"
				Else
					ProcType = "haste"
				End If
				If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
				Fade = T + ProcLenght * 100
				HitCount += 1
			Case "DeathbringersWillHeroic"
				Dim RNG As Double
				RNG = Rng3
				AddUptime(T)
				If RNG < 0.33 Then
					ProcType = "str"
				ElseIf RNG < 0.66 Then
					ProcType = "crit"
				Else
					ProcType = "haste"
				End If
				If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
				Fade = T + ProcLenght * 100
				HitCount += 1
			Case "arcane"
				If Rng3 < (0.17 - sim.MainStat.SpellHit) Then
					MissCount = MissCount + 1
					Exit sub
				End If
				If RngCrit <= sim.MainStat.SpellCrit Then
					CritCount = CritCount + 1
					tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					Totalcrit +=  tmp
				Else
					tmp = ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					HitCount = HitCount + 1
					Totalhit +=  tmp
				End If
			Case "shadow"
				If Rng3 < (0.17 - sim.MainStat.SpellHit) Then
					MissCount = MissCount + 1
					Exit sub
				End If
				If RngCrit <= sim.MainStat.SpellCrit Then
					CritCount = CritCount + 1
					tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
					totalcrit += tmp
				Else
					tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
					HitCount = HitCount + 1
					totalhit += tmp
				End If
			Case "SaroniteBomb"
				tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
				HitCount = HitCount + 1
				totalhit += tmp
			Case "SapperCharge"
				If RngCrit <= sim.MainStat.Crit Then
					CritCount = CritCount + 1
					tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
					totalcrit += tmp
				Else
					tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					HitCount = HitCount + 1
					totalhit += tmp
				End If
				
			Case "physical"
				If RngCrit <= sim.MainStat.Crit Then
					CritCount = CritCount + 1
					tmp = ProcValue * 2 * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
					totalcrit += tmp
				Else
					tmp= ProcValue * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
					HitCount = HitCount + 1
					totalhit += tmp
				End If
			Case "FallenCrusader"
				sim.RuneForge.ProcFallenCrusader(Me, T)
				
			Case "Razorice"
				sim.RuneForge.ProcRazorice(Me, T)
			
			Case "Cinderglacier"
				sim.RuneForge.ProcCinderglacier(Me, T)

			Case "torrent"
				sim.RunicPower.add (Me.ProcValue)
				HitCount = HitCount + 1

			
			Case "BloodWorms"
				tmp =  50 + 0.006*sim.MainStat.AP
				tmp = tmp * 10
				tmp = tmp * (1+sim.MainStat.Haste)
				tmp = tmp * sim.GhoulStat.PhysicalDamageMultiplier(T)
				dim RNG as Double = RngCrit
				If Rng < 0.33 Then
					tmp = tmp * 2
					HitCount = HitCount + 20
				ElseIf Rng < 0.66
					tmp = tmp *3
					HitCount = HitCount + 30
				Else
					tmp = tmp *4
					HitCount = HitCount + 40
				End If
			Case Else
				debug.Print ( ME.Name & " not implemented")
		End Select
		
		'Disabling Haste scaling for most procs
		'If sim.EPStat = "EP HasteEstimated" and HasteSensible Then
		'	tmp = tmp*sim.MainStat.EstimatedHasteBonus
		'End If
		total += tmp
	End Sub


	
End Class
