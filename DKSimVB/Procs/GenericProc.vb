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
	Friend InternalCD As Integer

	Friend Stack As Integer
	Friend MaxStack As Integer
	
	
	
	Friend ProcTypeStack as String
	Friend ProcValueStack as Integer
	
	Friend Count As Integer
	
	Friend ProcType As String
	Friend ProcOn As procs.ProcOnType

	
	Friend previousFade As Long

	
	
	
	Sub New()
		_RNG1 = nothing
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
		If Equiped = 1 Then Exit Sub
		Equiped = 1
		sim.proc.EquipedProc.Add(me,Name)
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
				sim.proc.OnFUProcs.add(Me,Name)
			Case procs.ProcOnType.OnBloodStrike
				sim.proc.OnBloodStrikeProcs.add(Me,Name)
			Case Else
				debug.Print ("No proc on value for " & me.Name)
		End Select
		sim.DamagingObject.Add(me,Name)
	End Sub
	
	Overridable Sub UnEquip()
		Me.Equiped = 0
		sim.proc.EquipedProc.Remove(Name)
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
			Case procs.ProcOnType.OnBloodStrike
				sim.proc.OnBloodStrikeProcs.Remove(Name)
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
			'debug.Print (me.Name)
			Return False
		End If
		if CD > T Then Return False
		If RngHit > ProcChance Then Return False
		ApplyMe(T)
		Return True
	End Function


	Overridable Sub ApplyMe(ByVal T As Long)
		CD = T + InternalCD * 100
		If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
		If MaxStack <> 0 Then
			Stack = math.Min(Stack+1,MaxStack)
		End If
		Fade = T + ProcLenght * 100
		AddUptime(T)
		HitCount += 1
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
