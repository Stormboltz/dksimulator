'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/9/2009
' Heure: 2:04 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Proc
	Inherits Supertype
	Friend CD as Integer
	Friend Fade As Integer
	Friend ProcChance As Double
	friend Equiped As Integer
	Friend ProcLenght As Integer
	Friend ProcValue As Integer
	Friend ProcValueDmg As Integer
	Friend InternalCD As Integer

	Friend Stack As Integer
	Friend MaxStack As Integer
	
	
	
	Friend ProcTypeStack as String
	Friend ProcValueStack as Integer
	
	Friend DamageType As String
	Friend Count As Integer
	
	Friend ProcType As String
	Friend ProcOn As procs.ProcOnType

	
	Friend previousFade As Long

	
	
	
	Sub New()
		_RNG = nothing
		ProcChance = 0
		Equiped = 0
		ProcLenght = 0
		ProcValue = 0
		InternalCD = 0
		count = 0
		ThreadMultiplicator = 1
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	Sub New(S As Sim)
		Me.New
		_name = Me.ToString
		Sim = S
		sim.proc.AllProcs.Add(me)
	End Sub
	
	
	Overridable Sub Equip()
		Equiped = 1
		sim.proc.EquipedTrinkets.Add(me,Name)
		Select Case Me.ProcOn
			Case Procs.ProcOnType.OnMisc
				
			Case procs.ProcOnType.OnCrit
				sim.proc.OnCritProcs.add(me,Name)
			Case procs.ProcOnType.OnDamage
				sim.proc.OnDamageProcs.add(me,Name)
			Case procs.ProcOnType.OnDoT
				sim.proc.OnDoTProcs.add(me,Name)
			Case procs.ProcOnType.OnHit
				sim.proc.OnHitProcs.add(me,Name)
			Case procs.ProcOnType.OnMHhit
				sim.proc.OnMHhitProcs.add(me,Name)
			Case procs.ProcOnType.OnOHhit
				sim.proc.OnOHhitProcs.add(me,Name)
			Case procs.ProcOnType.OnMHWhiteHit
				sim.proc.OnMHWhitehitProcs.add(me,Name)
			Case procs.ProcOnType.OnFU
				sim.proc.OnFUProcs.add(me,Name)
			Case Else
				debug.Print ("No proc on value for " & me.Name)
		End Select
		sim.DamagingObject.Add(me,Name)
	End Sub
	
	Overridable Sub UnEquip()
		Me.Equiped = 0
		sim.proc.EquipedTrinkets.Remove(Name)
		Select Case Me.ProcOn
			Case Procs.ProcOnType.OnMisc
				
			Case procs.ProcOnType.OnCrit
				sim.proc.OnCritProcs.Remove(Name)
			Case procs.ProcOnType.OnDamage
				sim.proc.OnDamageProcs.Remove(Name)
			Case procs.ProcOnType.OnDoT
				sim.proc.OnDoTProcs.Remove(Name)
			Case procs.ProcOnType.OnHit
				sim.proc.OnHitProcs.Remove(Name)
			Case procs.ProcOnType.OnMHhit
				sim.proc.OnMHhitProcs.Remove(Name)
			Case procs.ProcOnType.OnOHhit
				sim.proc.OnOHhitProcs.Remove(Name)
			Case procs.ProcOnType.OnMHWhiteHit
				sim.proc.OnMHWhitehitProcs.Remove(Name)
			Case procs.ProcOnType.OnFU
				sim.proc.OnFUProcs.Remove(Name)
			Case Else
				debug.Print ("No proc on value for " & me.Name)
		End Select
		sim.DamagingObject.remove(Name)
	End Sub
	
	
	Overridable Function IsActiveAt(ByVal T As Long) As Boolean
		If Fade >= T Then Return True
	End Function

	
	
	Overridable Function IsActive() As Boolean
		if Fade >= sim.TimeStamp then return true
	End Function
	
	Overridable Function Use() As Boolean
		Fade = 0
		count = 0
		RemoveUptime(sim.TimeStamp)
	End Function
	
	Overridable Function IsAvailable(ByVal T As Long) As Boolean
		If Equiped = 0 Or CD > T Then Return False
		Return True
	End Function

	Overridable Function TryMe(ByVal T As Long) As Boolean
		If Equiped = 0 Then
			sim.UselessCheck += 1
			debug.Print (me.Name)
			Return False
		End If
		if CD > T Then Return False
		If MyRng() > ProcChance Then Return False
		ApplyMe(T)
		Return True
	End Function


	Overridable Sub ApplyMe(ByVal T As Long)
		dim tmp as Integer
		CD = T + InternalCD * 100
		Select Case DamageType
			Case ""
				If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
				Fade = T + ProcLenght * 100
				If MaxStack <> 0 Then
					Stack = math.Min(Stack+1,MaxStack)
				End If
				AddUptime(T)
				HitCount += 1
			Case "Shadowmourne"
				If Stack < 9 Then
					Stack +=1
				Else
					Stack = 0
					If MyRng < (0.17 - sim.MainStat.SpellHit) Then
						MissCount = MissCount + 1
						Exit sub
					End If
					tmp= ProcValueDmg * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					HitCount = HitCount + 1
					totalhit += tmp
					If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
					Fade = T + ProcLenght * 100
					HitCount += 1
					CD = T + 10 * 100
					AddUptime(T)
				End If
			Case "Bryntroll"
				If MyRng < (0.17 - sim.MainStat.SpellHit) Then
					MissCount = MissCount + 1
					Exit sub
				End If
				tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
				HitCount = HitCount + 1
				totalhit += tmp
			Case "BryntrollHeroic"
				If MyRng < (0.17 - sim.MainStat.SpellHit) Then
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
						If MyRng > 0.5 Then
							tmp = sim.MainHand.AvrgNonCrit(T)/2
						Else
							tmp = sim.offhand.AvrgNonCrit(T)/2
						End If
					Else
						tmp = sim.MainHand.AvrgNonCrit(T)/2
					End If
					If MyRng < sim.MainStat.crit Then
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
				RNG = Rnd
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
				RNG = Rnd
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
				If MyRng < (0.17 - sim.MainStat.SpellHit) Then
					MissCount = MissCount + 1
					Exit sub
				End If
				If MyRng <= sim.MainStat.SpellCrit Then
					CritCount = CritCount + 1
					tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					Totalcrit +=  tmp
				Else
					tmp = ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					HitCount = HitCount + 1
					Totalhit +=  tmp
				End If
			Case "shadow"
				If MyRng < (0.17 - sim.MainStat.SpellHit) Then
					MissCount = MissCount + 1
					Exit sub
				End If
				If MyRng <= sim.MainStat.SpellCrit Then
					CritCount = CritCount + 1
					tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					tmp = tmp * (1 + sim.TalentFrost.BlackIce * 2 / 100)
					totalcrit += tmp
				Else
					tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
					tmp = tmp * (1 + sim.TalentFrost.BlackIce * 2 / 100)
					HitCount = HitCount + 1
					totalhit += tmp
				End If
			Case "SaroniteBomb"
				tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
				HitCount = HitCount + 1
				totalhit += tmp
			Case "SapperCharge"
				If MyRng <= sim.MainStat.Crit Then
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
				If MyRng <= sim.MainStat.Crit Then
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
			Case "BloodWorms"
				tmp =  50 + 0.006*sim.MainStat.AP
				tmp = tmp * 10
				tmp = tmp * (1+sim.MainStat.Haste)
				tmp = tmp * sim.GhoulStat.PhysicalDamageMultiplier(T)
				If MyRng < 0.33 Then
					tmp = tmp * 2
					HitCount = HitCount + 20
				ElseIf MyRng < 0.66
					tmp = tmp *3
					HitCount = HitCount + 30
				Else
					tmp = tmp *4
					HitCount = HitCount + 40
				End If
			Case "HangedMan"
				If Stack < 3 Then Stack += 1
				Fade = T + ProcLenght * 100
				AddUptime(T)
				HitCount += 1
			Case Else
				debug.Print ( ME.Name & " not implemented")
		End Select
		
		
		If sim.EPStat = "EP HasteEstimated" and HasteSensible Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		
		
		total += tmp
	End Sub
	
	Overrides Function report as String
		If HitCount + CritCount = 0 Then Return ""
		return MyBase.report
	End Function
	
	Public Sub cleanup()
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	
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
