'
' Created by SharpDevelop.
' Date: 8/04/2010
' Time: 1:13 p.m.
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Procs
    Public Class WeaponProc
        Inherits Proc
        Friend DamageType As String

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            _Name = ""
        End Sub

        Overrides Sub ApplyMe(ByVal T As Long)
            If DamageType = "" Then
                MyBase.ApplyMe(T)
                Exit Sub
            End If
            CD = T + InternalCD * 100
            Dim tmp As Double
            Select Case DamageType
                Case "Fallen Crusader", "3368"
                    MyBase.ApplyMe(T)
                    Exit Sub
                Case "Bryntroll"
                    If RngCrit < (0.17 - sim.Character.SpellHit.Value) Then
                        MissCount = MissCount + 1
                        Exit Sub
                    End If
                    tmp = ProcValue * sim.Character.StandardMagicalDamageMultiplier(sim.TimeStamp)
                    HitCount = HitCount + 1
                    TotalHit += tmp

                Case "TinyAbomination"
                    Me.Stack += 1
                    If Me.Stack = 8 Then
                        Me.Stack = 0
                        If sim.Character.DualW Then
                            If RngCrit > 0.5 Then
                                tmp = sim.MainHand.AvrgNonCrit(T) / 2
                                Me.CD = T + 1
                                sim.proc.tryProcs(ProcsManager.ProcOnType.OnMHhit)
                                Me.CD = 0
                            Else

                                tmp = sim.OffHand.AvrgNonCrit(T) / 2
                                Me.CD = T + 1
                                sim.proc.tryProcs(ProcsManager.ProcOnType.OnOHhit)
                                Me.CD = 0
                            End If
                        Else
                            tmp = sim.MainHand.AvrgNonCrit(T) / 2
                        End If
                        If RngCrit < sim.Character.Crit.Value Then
                            tmp = tmp * 2
                            CritCount += 1
                            TotalCrit += tmp
                        Else
                            HitCount += 1
                            TotalHit += tmp
                        End If
                    End If
                Case "DeathbringersWill"
                    Dim RNG As Double
                    RNG = RngCrit
                    AddUptime(T)
                    If RNG < 0.33 Then
                        Effects(0).Stat = Simulator.Sim.Stat.Strength
                    ElseIf RNG < 0.66 Then
                        Effects(0).Stat = Simulator.Sim.Stat.Crit
                    Else
                        Effects(0).Stat = Simulator.Sim.Stat.Haste
                    End If
                    Effects(0).Apply()
                    If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & Me.ToString & " proc")
                    Fade = T + ProcLenght * 100
                    HitCount += 1
                Case "arcane"
                    If RngCrit < (0.17 - sim.Character.SpellHit.Value) Then
                        MissCount = MissCount + 1
                        Exit Sub
                    End If
                    If RngCrit <= sim.Character.SpellCrit.Value Then
                        CritCount = CritCount + 1
                        tmp = ProcValue * 1.5 * sim.Character.StandardMagicalDamageMultiplier(sim.TimeStamp)
                        TotalCrit += tmp
                    Else
                        tmp = ProcValue * sim.Character.StandardMagicalDamageMultiplier(sim.TimeStamp)
                        HitCount = HitCount + 1
                        TotalHit += tmp
                    End If
                Case "shadow"
                    If RngHit() < (0.17 - sim.Character.SpellHit.Value) Then
                        MissCount = MissCount + 1
                        Exit Sub
                    End If
                    tmp = ProcValue * sim.Character.StandardMagicalDamageMultiplier(sim.TimeStamp)
                    HitCount = HitCount + 1
                    TotalHit += tmp
                Case "SaroniteBomb"
                    tmp = ProcValue * sim.Character.StandardMagicalDamageMultiplier(sim.TimeStamp)
                    HitCount = HitCount + 1
                    TotalHit += tmp
                Case "SapperCharge"
                    If RngCrit <= sim.Character.Crit.Value Then
                        CritCount = CritCount + 1
                        tmp = ProcValue * 1.5 * sim.Character.StandardMagicalDamageMultiplier(sim.TimeStamp)

                        TotalCrit += tmp
                    Else
                        tmp = ProcValue * sim.Character.StandardMagicalDamageMultiplier(sim.TimeStamp)
                        HitCount = HitCount + 1
                        TotalHit += tmp
                    End If

                Case "physical"
                    If RngCrit <= sim.Character.Crit.Value Then
                        CritCount = CritCount + 1
                        tmp = ProcValue * 2 * sim.Character.StandardPhysicalDamageMultiplier(sim.TimeStamp)
                        TotalCrit += tmp
                    Else
                        tmp = ProcValue * sim.Character.StandardPhysicalDamageMultiplier(sim.TimeStamp)
                        HitCount = HitCount + 1
                        TotalHit += tmp
                    End If
                Case "Razorice", "3370"
                    sim.RuneForge.ProcRazorIce(Me, T)

                Case "Cinderglacier", "3369"
                    sim.RuneForge.ProcCinderglacier(Me, T)

                Case "torrent"
                    sim.RunicPower.add(Me.ProcValue)
                    HitCount = HitCount + 1
                Case "BloodWorms"
                    tmp = 50 + 0.006 * sim.Character.AP.CurrentValue
                    tmp = tmp * 10
                    tmp = tmp * (1 + sim.Character.Haste.Value)
                    tmp = tmp * sim.GhoulStat.PhysicalDamageMultiplier(T)
                    Dim RNG As Double = RngCrit
                    If RNG < 0.33 Then
                        tmp = tmp * 2
                        HitCount = HitCount + 20
                    ElseIf RNG < 0.66 Then
                        tmp = tmp * 3
                        HitCount = HitCount + 30
                    Else
                        tmp = tmp * 4
                        HitCount = HitCount + 40
                    End If
                Case Else
                    Diagnostics.Debug.WriteLine(Me.Name & " not implemented")
            End Select

            'Disabling Haste scaling for most procs
            'If sim.EPStat = "EP HasteEstimated" and HasteSensible Then
            '	tmp = tmp*sim.MainStat.EstimatedHasteBonus
            'End If
            total += tmp
        End Sub



    End Class
End Namespace