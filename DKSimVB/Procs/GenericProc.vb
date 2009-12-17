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
	Public Total as Integer
	Friend HitCount As Integer
	Friend MissCount As Integer
	Friend CritCount As Integer
	Friend DamageType As String
	Friend Count As Integer
	Public Name As String
	Friend ProcType As String
	Friend ProcOn As procs.ProcOnType
	
	
	
	
	

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
				sim.proc.OnOHhitProcs.add(me)
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
					HitCount += 1
				Case "DeathbringersWill"
					Dim RNG As Double
					RNG = Rnd
					
					If RNG < 0.33 Then
						ProcType = "str"
					ElseIf RNG < 0.66 Then
						ProcType = "crit"
					Else
						If TalentUnholy.Gargoyle = 1 Then
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
					
					If RNG < 0.33 Then
						ProcType = "str"
					ElseIf RNG < 0.66 Then
						ProcType = "crit"
					Else
						If TalentBlood.Hysteria = 1 Then
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
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
					Else
						tmp = ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						HitCount = HitCount + 1
					End If
				Case "shadow"
					If RNGProc < (0.17 - sim.MainStat.SpellHit) Then
						MissCount = MissCount + 1
					Exit sub
					End If
					If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.SpellCrit Then
						CritCount = CritCount + 1
						
						tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
						tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
					Else
						tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
						HitCount = HitCount + 1
					End If
				Case "physical"
					If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.Crit Then
						CritCount = CritCount + 1
						tmp = ProcValue * 2 * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
					Else
						tmp= ProcValue * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
						HitCount = HitCount + 1
					End If
					
				Case "razorice"
					HitCount = HitCount + 1
					tmp = procvalue
				Case "torrent"
					sim.RunicPower.add (Me.ProcValue)
					HitCount = HitCount + 1
				Case "cinderglacier"
					sim.RuneForge.CinderglacierProc = 2
			End Select
			total += tmp
			
		end if
	End Sub
	
	Overridable Function report as String
		dim tmp as String
		tmp = name & VBtab
		tmp = tmp & total & VBtab
		tmp = tmp & toDecimal(100*Total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(Total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	
	
	
	
End Class
