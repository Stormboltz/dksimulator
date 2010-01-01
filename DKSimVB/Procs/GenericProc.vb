'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/9/2009
' Heure: 2:04 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Proc
	Friend CD as Integer
	Friend Fade As Integer
	Friend ProcChance As Double
	Friend _RNG As Random
	friend Equiped As Integer
	Friend ProcLenght As Integer
	Friend ProcValue As Integer
	Friend InternalCD As Integer
	protected Sim as Sim
	Public Total as long
	Friend HitCount As long
	Friend MissCount As long
	Friend CritCount As long
	
	Friend TotalHit As long
	Friend TotalCrit As long
	
	
	Friend DamageType As String
	Friend Count As Integer
	Public Name As String
	Friend ProcType As String
	Friend ProcOn As procs.ProcOnType
	Public ThreadMultiplicator As Double
	
	Friend previousFade As Long
	friend Uptime as long

	Function RNGProc As Double
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(me.ToString)+RNGSeeder)
		End If
		return _RNG.NextDouble
	End Function
	
	Sub New()
		_RNG = nothing
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		ProcChance = 0
		Equiped = 0
		ProcLenght = 0
		ProcValue = 0
		InternalCD = 0
		count = 0
		ThreadMultiplicator = 1
		
		TotalHit = 0
		TotalCrit = 0
		
	End Sub
	Sub New(S As Sim)
		Me.New
		name = Me.ToString
		Sim = S
		sim.proc.AllProcs.Add(me)
	End Sub
	
	
	Overridable Sub Equip()
		Equiped = 1
		sim.proc.EquipedTrinkets.Add(me)
		Select Case Me.ProcOn
			Case Procs.ProcOnType.OnMisc
				
			Case procs.ProcOnType.OnCrit
				sim.proc.OnCritProcs.add(me)
			Case procs.ProcOnType.OnDamage
				sim.proc.OnDamageProcs.add(me)
			Case procs.ProcOnType.OnDoT
				sim.proc.OnDoTProcs.add(me)
			Case procs.ProcOnType.OnHit
				sim.proc.OnHitProcs.add(me)
			Case procs.ProcOnType.OnMHhit
				sim.proc.OnMHhitProcs.add(me)
			Case procs.ProcOnType.OnOHhit
				sim.proc.OnOHhitProcs.add(Me)
			Case procs.ProcOnType.OnMHWhiteHit
				sim.proc.OnMHWhitehitProcs.add(Me)
			Case Else
				debug.Print ("No proc on value for " & me.Name)
		End Select
	End Sub
	
	
	
	Overridable Function IsActive() As Boolean
		if Fade >= sim.TimeStamp then return true
	End Function
	
	Overridable Function Use() As Boolean
		Fade = 0
		count = 0
		RemoveUptime(sim.TimeStamp)
	End Function
	
	Overridable Sub TryMe(T As Long)
		dim tmp as Integer
		If Equiped = 0 Or CD > T Then Exit Sub
		If RNGProc <= ProcChance Then
			CD = T + InternalCD * 100
			Select Case DamageType
				Case ""
					If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
					Fade = T + ProcLenght * 100
					AddUptime(T)
					HitCount += 1
				Case "DeathbringersWill"
					Dim RNG As Double
					RNG = Rnd
					AddUptime(T)
					If RNG < 0.33 Then
						ProcType = "str"
					ElseIf RNG < 0.66 Then
						ProcType = "crit"
					Else
						If sim.TalentUnholy.Gargoyle = 1 Then
							ProcType = "haste"
						Else
							ProcType = "arp"
						End If
					End If
					
					If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
					Fade = T + ProcLenght * 100
					HitCount += 1
				Case "DeathbringersWillHeroic"
					Dim RNG As Double
					RNG = Rnd
					AddUptime(T)
					If RNG < 0.33 Then
						ProcType = "str"
					ElseIf RNG < 0.66 Then
						ProcType = "crit"
					Else
						If sim.TalentBlood.Hysteria = 1 Then
							ProcType = "arp"
						Else
							ProcType = "haste"
						End If
					End If
					
					If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
					Fade = T + ProcLenght * 100
					HitCount += 1
				Case "arcane"
					If RNGProc < (0.17 - sim.MainStat.SpellHit) Then
						MissCount = MissCount + 1
					Exit sub
					End If
					If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.SpellCrit Then
						CritCount = CritCount + 1
						tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						Totalcrit +=  tmp
					Else
						tmp = ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						HitCount = HitCount + 1
						Totalhit +=  tmp
					End If
				Case "shadow"
					If RNGProc < (0.17 - sim.MainStat.SpellHit) Then
						MissCount = MissCount + 1
					Exit sub
					End If
					If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.SpellCrit Then
						CritCount = CritCount + 1
						
						tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.TalentFrost.BlackIce * 2 / 100)
						totalcrit += tmp
					Else
						tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						HitCount = HitCount + 1
						totalhit += tmp
					End If
				Case "physical"
					If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.Crit Then
						CritCount = CritCount + 1
						tmp = ProcValue * 2 * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
						totalcrit += tmp
					Else
						tmp= ProcValue * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
						HitCount = HitCount + 1
						totalhit += tmp
					End If
				Case "razorice"
					HitCount = HitCount + 1
					tmp = procvalue
					totalhit += tmp
				Case "torrent"
					sim.RunicPower.add (Me.ProcValue)
					HitCount = HitCount + 1
				Case "cinderglacier"
					HitCount = HitCount + 1
					sim.RuneForge.CinderglacierProc = 2
				Case "Bryntroll"
					If RNGProc < (0.17 - sim.MainStat.SpellHit) Then
						MissCount = MissCount + 1
						Exit sub
					End If
					tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					HitCount = HitCount + 1
					totalhit += tmp
			End Select
			total += tmp
		end if
	End Sub
	Overridable Public Sub Merge()
	End Sub
	
	Overridable Function report as String
		Dim tmp As String
		
		If Name = "Virulence" Then
			tmp=""
		End If
		
		tmp = name & VBtab
		
		tmp = tmp & total & VBtab
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		
		tmp = tmp & toDecimal(HitCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(totalhit/(HitCount)) & VBtab
		
		tmp = tmp & toDecimal(CritCount) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(totalcrit/(CritCount)) & VBtab
				
		tmp = tmp & toDecimal(MissCount) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab

		If sim.FrostPresence Then
			tmp = tmp & toDecimal((100 * total * ThreadMultiplicator * 2.0735 ) / sim.TimeStamp) & VBtab
		End If
		
		tmp = tmp & ""& toDecimal(100*uptime/sim.MaxTime)  & "" & VBtab

		tmp = tmp & vbCrLf
		
		
		tmp = replace(tmp, VBtab & 0, vbtab)
		return tmp
	End Function
	
	
	Sub AddUptime(T As Long)
		dim tmp as Long
		If ProcLenght*100 + T > sim.MaxTime Then
			tmp = (sim.MaxTime - T)/100
		Else
			tmp = ProcLenght
		End If
		
		If previousfade < T  Then
		 	uptime += tmp*100
		Else
			uptime += tmp*100 - (previousFade-T)
		End If
		previousFade = T + tmp*100
	End Sub
	Sub RemoveUptime(T As Long)
		If previousfade < T  Then
		Else
			uptime -= (previousFade-T)
		End If
		previousFade = T 
	End Sub
	
	
End Class
