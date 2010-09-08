'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/9/2009
' Heure: 2:04 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Procs
    Public Class Proc
        Inherits WowObject

        Friend CD As Integer
        Friend Fade As Integer
        Friend ProcChance As Double
        Friend Equiped As Integer
        Friend ProcLenght As Integer
        Friend ProcValue As Integer
        Friend InternalCD As Integer
        Friend Stack As Integer
        Friend MaxStack As Integer
        Friend ProcTypeStack As Sim.Stat
        Friend ProcValueStack As Integer

        Friend CurrentStack As Integer

        Friend ProcType As Sim.Stat
        Friend ProcOn As ProcsManager.ProcOnType

        Friend previousFade As Long

        Friend isDebuff As Boolean




        Sub New()
            _RNG1 = Nothing
            ProcChance = 0
            Equiped = 0
            ProcLenght = 0
            ProcValue = 0
            InternalCD = 0
            CurrentStack = 0
            ThreadMultiplicator = 1
            Total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
            isDebuff = False
        End Sub
        Sub New(ByVal S As Sim)
            Me.New()
            _name = Me.ToString
            Sim = S
            Sim.proc.AllProcs.Add(Me)
        End Sub


        Overridable Sub Equip()
            If Equiped = 1 Then Exit Sub
            Equiped = 1
            Sim.proc.EquipedProc.Add(Me)
            Sim.DamagingObject.Add(Me)
        End Sub

        Overridable Sub UnEquip()
            Me.Equiped = 0
            Sim.proc.EquipedProc.Remove(Me)
            Sim.DamagingObject.Remove(Me)
        End Sub


        Overridable Function IsActiveAt(ByVal T As Long) As Boolean
            If Fade >= T Then
                Return True
            Else
                Return False
            End If
        End Function



        Overridable Function IsActive() As Boolean
            If Fade >= Sim.TimeStamp Then
                Return True
            Else
                Return False
            End If
        End Function

        Sub Use()
            CurrentStack -= 1
            If CurrentStack <= 0 Then
                Fade = Sim.TimeStamp
                RemoveUptime(Sim.TimeStamp)
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
            If Sim.EPStat <> "" Then Exit Sub 'useless as no report generated.
            If ProcLenght Then
                AddUptime(T)
            End If
            HitCount += 1
        End Sub

        Overridable Sub ApplyMe(ByVal T As Long)
            CD = T + InternalCD * 100
            If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.ToString & " proc")
            If MaxStack <> 0 Then
                Stack = Math.Min(Stack + 1, MaxStack)
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
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub

        Sub AddUptime(ByVal T As Long)
            Dim tmp As Long

            If ProcLenght * 100 + T > Sim.NextReset Then
                tmp = (Sim.NextReset - T) / 100
            Else
                tmp = ProcLenght
            End If

            If previousFade < T Then
                uptime += tmp * 100
            Else
                uptime += tmp * 100 - (previousFade - T)
            End If
            previousFade = T + tmp * 100
        End Sub
        Sub RemoveUptime(ByVal T As Long)
            If previousFade < T Then
            Else
                uptime -= (previousFade - T)
            End If
            previousFade = T
        End Sub
    End Class
    Class MightOfFrozenWastes
        Inherits Proc
        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub
        Public Overrides Sub ApplyMe(ByVal T As Long)
            Sim.RunicPower.add(100)
            Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.Name & " proc")
            MyBase.ApplyMe(T)
        End Sub

    End Class
    Class RunicEmpowerment
        Inherits Proc

        Sub New(ByVal S As Sim)
            MyBase.New(S)

        End Sub

        Public Overrides Sub ApplyMe(ByVal T As Long)
            Dim d As Double '= RngCrit
            Dim DepletedRunes As New List(Of Runes.CataRune)
            If Sim.Runes.BloodRune1.Value = 0 Then DepletedRunes.Add(Sim.Runes.BloodRune1)
            If Sim.Runes.BloodRune2.Value = 0 Then DepletedRunes.Add(Sim.Runes.BloodRune2)

            If Sim.Runes.UnholyRune1.Value = 0 Then DepletedRunes.Add(Sim.Runes.UnholyRune1)
            If Sim.Runes.UnholyRune2.Value = 0 Then DepletedRunes.Add(Sim.Runes.UnholyRune2)

            If Sim.Runes.FrostRune1.Value = 0 Then DepletedRunes.Add(Sim.Runes.FrostRune1)
            If Sim.Runes.FrostRune2.Value = 0 Then DepletedRunes.Add(Sim.Runes.FrostRune2)

            If DepletedRunes.Count = 0 Then Return
            d = (DepletedRunes.Count - 1) * RngCrit
            Dim dec As Decimal = Convert.ToDecimal(d)
            Dim i As Integer
            i = Decimal.Round(dec, 0)

            Try
                DepletedRunes.Item(i).Value = 100
                Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.Name & "on " & DepletedRunes.Item(i).Name)
            Catch ex As Exception
                msgBox(ex.StackTrace)
            End Try

            Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.Name & " proc")
            MyBase.ApplyMe(T)
        End Sub

    End Class
End Namespace
