﻿'
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
	
	Friend isDebuff as Boolean

	
	
	
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
		isDebuff = false
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
		sim.proc.EquipedProc.Add(me)
		Select Case Me.ProcOn
			Case Procs.ProcOnType.OnMisc
				
			Case procs.ProcOnType.OnCrit
                sim.proc.OnCritProcs.Add(Me)
			Case procs.ProcOnType.OnDamage
                sim.proc.OnDamageProcs.Add(Me)
			Case procs.ProcOnType.OnDoT
                sim.proc.OnDoTProcs.Add(Me)
			Case procs.ProcOnType.OnHit
                sim.proc.OnHitProcs.Add(Me)
			Case procs.ProcOnType.OnMHhit
                sim.proc.OnMHhitProcs.Add(Me)
			Case procs.ProcOnType.OnOHhit
                sim.proc.OnOHhitProcs.Add(Me)
			Case procs.ProcOnType.OnMHWhiteHit
                sim.proc.OnMHWhitehitProcs.Add(Me)
			Case procs.ProcOnType.OnFU
                sim.proc.OnFUProcs.Add(Me)
			Case procs.ProcOnType.OnBloodStrike
                sim.proc.OnBloodStrikeProcs.Add(Me)
            Case Procs.ProcOnType.onRPDump
                sim.proc.onRPDumpProcs.Add(Me)
            Case Else
                'Diagnostics.Debug.WriteLine("No proc on value for " & Me.Name)
        End Select
        sim.DamagingObject.Add(Me)
    End Sub

    Overridable Sub UnEquip()
        Me.Equiped = 0
        sim.proc.EquipedProc.Remove(Me)
        Select Case Me.ProcOn
            Case Procs.ProcOnType.OnMisc

            Case procs.ProcOnType.OnCrit
                sim.proc.OnCritProcs.Remove(Me)
            Case procs.ProcOnType.OnDamage
                sim.proc.OnDamageProcs.Remove(Me)
            Case procs.ProcOnType.OnDoT
                sim.proc.OnDoTProcs.Remove(Me)
            Case procs.ProcOnType.OnHit
                sim.proc.OnHitProcs.Remove(Me)
            Case procs.ProcOnType.OnMHhit
                sim.proc.OnMHhitProcs.Remove(Me)
            Case procs.ProcOnType.OnOHhit
                sim.proc.OnOHhitProcs.Remove(Me)
            Case procs.ProcOnType.OnMHWhiteHit
                sim.proc.OnMHWhitehitProcs.Remove(Me)
            Case procs.ProcOnType.OnFU
                sim.proc.OnFUProcs.Remove(Me)
            Case procs.ProcOnType.OnBloodStrike
                sim.proc.OnBloodStrikeProcs.Remove(Me)
            Case Procs.ProcOnType.onRPDump
                sim.proc.onRPDumpProcs.Remove(Me)
            Case Else
                Diagnostics.Debug.WriteLine("No proc on value for " & Me.Name)
        End Select
        sim.DamagingObject.remove(Me)
    End Sub


    Overridable Function IsActiveAt(ByVal T As Long) As Boolean
        If Fade >= T Then
            Return True
        Else
            Return False
        End If
    End Function



    Overridable Function IsActive() As Boolean
        If Fade >= sim.TimeStamp Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub Use()
        count -= 1
        If count <= 0 Then
            Fade = 0
            RemoveUptime(sim.TimeStamp)
        End If
    End Sub

    Overridable Function IsAvailable(ByVal T As Long) As Boolean
        If Equiped = 0 Or CD > T Then Return False
        Return True
    End Function

    Overridable Function TryMe(ByVal T As Long) As Boolean
        If Equiped = 0 Then
            'sim.UselessCheck += 1
            'Diagnostics.Debug.WriteLine (me.Name)
            Return False
        End If
        If CD > T Then Return False
        If RngHit > ProcChance Then Return False
        ApplyMe(T)
        Return True
    End Function

    Sub ApplyFade(ByVal T As Long)
        If sim.EPStat <> "" Then Exit Sub 'useless as no report generated.
        If ProcLenght Then
            AddUptime(T)
        End If
        HitCount += 1
    End Sub

	Overridable Sub ApplyMe(ByVal T As Long)
		CD = T + InternalCD * 100
		If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
		If MaxStack <> 0 Then
			Stack = math.Min(Stack+1,MaxStack)
        End If
        If ProcLenght Then
            Fade = T + ProcLenght * 100
        End If
        HitCount += 1
		ApplyFade(T)
	End Sub
	
	
	
	Public Sub cleanup()
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	
	Sub AddUptime(T As Long)
		Dim tmp As Long
		
		If ProcLenght*100 + T > sim.NextReset Then
			tmp = (sim.NextReset - T)/100
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
Class RunicEmpowerment
    Inherits Proc

    Sub New(ByVal S As Sim)
        MyBase.New(S)
    End Sub

    Public Overrides Sub ApplyMe(ByVal T As Long)
        Dim d As Double '= RngCrit
        Dim DepletedRunes As New List(Of CataRune)
        If sim.Runes.BloodRune1.Value = 0 Then DepletedRunes.Add(sim.Runes.BloodRune1)
        If sim.Runes.BloodRune2.Value = 0 Then DepletedRunes.Add(sim.Runes.BloodRune2)

        If sim.Runes.UnholyRune1.Value = 0 Then DepletedRunes.Add(sim.Runes.UnholyRune1)
        If sim.Runes.UnholyRune2.Value = 0 Then DepletedRunes.Add(sim.Runes.UnholyRune2)

        If sim.Runes.FrostRune1.Value = 0 Then DepletedRunes.Add(sim.Runes.FrostRune1)
        If sim.Runes.FrostRune2.Value = 0 Then DepletedRunes.Add(sim.Runes.FrostRune2)

        If DepletedRunes.Count = 0 Then Return
        d = (DepletedRunes.Count - 1) * RngCrit
        Dim dec As Decimal = Convert.ToDecimal(d)
        Dim i As Integer
        i = Decimal.Round(dec, 0)

        Try
            DepletedRunes.Item(i).Value = 100
            sim.CombatLog.write(sim.TimeStamp & vbTab & Me.Name & "on " & DepletedRunes.Item(i).Name)
        Catch ex As Exception
            msgBox(ex.StackTrace)
        End Try

        sim.CombatLog.write(sim.TimeStamp & vbTab & Me.Name & " proc")
        MyBase.ApplyMe(T)
    End Sub

End Class
