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

        Sub BaseApplyMe(ByVal T As Long)
            MyBase.ApplyMe(T)
        End Sub

        Overrides Sub ApplyMe(ByVal T As Long)
            If DamageType = "" Then
                BaseApplyMe(T)
                Exit Sub
            End If
            CD = T + InternalCD * 100
            Dim tmp As Double
            Select Case DamageType
                Case "Shadowmourne"
                    If Fade <= T Then Stack = 0

                    If Stack < 9 Then
                        Stack += 1
                        CD = T
                        Fade = T + ProcLenght * 100
                    Else
                        Fade = CD
                        If Rng3 < (0.17 - Sim.Character.SpellHit) Then
                            MissCount = MissCount + 1
                            Exit Sub
                        End If
                        tmp = ProcValue * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)
                        totalhit += tmp
                        If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.ToString & " proc")

                        HitCount += 1
                        AddUptime(T)
                    End If
                Case "Bryntroll"
                    If Rng3 < (0.17 - Sim.Character.SpellHit) Then
                        MissCount = MissCount + 1
                        Exit Sub
                    End If
                    tmp = ProcValue * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)
                    HitCount = HitCount + 1
                    totalhit += tmp

                Case "TinyAbomination"
                    Me.Stack += 1
                    If Me.Stack = 8 Then
                        Me.Stack = 0
                        If Sim.Character.DualW Then
                            If Rng4 > 0.5 Then
                                tmp = Sim.MainHand.AvrgNonCrit(T) / 2
                                Me.CD = T + 1
                                Sim.proc.tryProcs(ProcsManager.ProcOnType.OnMHhit)
                                Me.CD = 0
                            Else

                                tmp = Sim.OffHand.AvrgNonCrit(T) / 2
                                Me.CD = T + 1
                                Sim.proc.tryProcs(ProcsManager.ProcOnType.OnOHhit)
                                Me.CD = 0
                            End If
                        Else
                            tmp = Sim.MainHand.AvrgNonCrit(T) / 2
                        End If
                        If RngCrit < Sim.Character.crit Then
                            tmp = tmp * 2
                            CritCount += 1
                            TotalCrit += tmp
                        Else
                            hitCount += 1
                            TotalHit += tmp
                        End If
                    End If
                Case "DeathbringersWill"
                    Dim RNG As Double
                    RNG = Rng3
                    AddUptime(T)
                    If RNG < 0.33 Then
                        ProcType = sim.Stat.Strength
                    ElseIf RNG < 0.66 Then
                        ProcType = sim.Stat.Crit
                    Else
                        ProcType = sim.Stat.Haste
                    End If
                    If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.ToString & " proc")
                    Fade = T + ProcLenght * 100
                    HitCount += 1
                Case "DeathbringersWillHeroic"
                    Dim RNG As Double
                    RNG = Rng3
                    AddUptime(T)
                    If RNG < 0.33 Then
                        ProcType = sim.Stat.Strength
                    ElseIf RNG < 0.66 Then
                        ProcType = sim.Stat.Crit
                    Else
                        ProcType = sim.Stat.Haste
                    End If
                    If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.ToString & " proc")
                    Fade = T + ProcLenght * 100
                    HitCount += 1
                Case "arcane"
                    If Rng3 < (0.17 - Sim.Character.SpellHit) Then
                        MissCount = MissCount + 1
                        Exit Sub
                    End If
                    If RngCrit <= Sim.Character.SpellCrit Then
                        CritCount = CritCount + 1
                        tmp = ProcValue * 1.5 * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)
                        Totalcrit += tmp
                    Else
                        tmp = ProcValue * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)
                        HitCount = HitCount + 1
                        Totalhit += tmp
                    End If
                Case "shadow"
                    If Rng3 < (0.17 - Sim.Character.SpellHit) Then
                        MissCount = MissCount + 1
                        Exit Sub
                    End If
                    If RngCrit <= Sim.Character.SpellCrit Then
                        CritCount = CritCount + 1
                        tmp = ProcValue * 1.5 * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)

                        totalcrit += tmp
                    Else
                        tmp = ProcValue * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)

                        HitCount = HitCount + 1
                        totalhit += tmp
                    End If
                Case "SaroniteBomb"
                    tmp = ProcValue * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)
                    HitCount = HitCount + 1
                    totalhit += tmp
                Case "SapperCharge"
                    If RngCrit <= Sim.Character.crit Then
                        CritCount = CritCount + 1
                        tmp = ProcValue * 1.5 * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)

                        totalcrit += tmp
                    Else
                        tmp = ProcValue * Sim.Character.StandardMagicalDamageMultiplier(Sim.TimeStamp)
                        HitCount = HitCount + 1
                        totalhit += tmp
                    End If

                Case "physical"
                    If RngCrit <= Sim.Character.crit Then
                        CritCount = CritCount + 1
                        tmp = ProcValue * 2 * Sim.Character.StandardPhysicalDamageMultiplier(Sim.TimeStamp)
                        totalcrit += tmp
                    Else
                        tmp = ProcValue * Sim.Character.StandardPhysicalDamageMultiplier(Sim.TimeStamp)
                        HitCount = HitCount + 1
                        totalhit += tmp
                    End If
                Case "FallenCrusader", "3368"
                    Sim.RuneForge.ProcFallenCrusader(Me, T)

                Case "Razorice", "3370"
                    Sim.RuneForge.ProcRazorIce(Me, T)

                Case "Cinderglacier"
                    Sim.RuneForge.ProcCinderglacier(Me, T)

                Case "torrent"
                    Sim.RunicPower.add(Me.ProcValue)
                    HitCount = HitCount + 1


                Case "BloodWorms"
                    tmp = 50 + 0.006 * Sim.Character.AP
                    tmp = tmp * 10
                    tmp = tmp * (1 + Sim.Character.Haste)
                    tmp = tmp * Sim.GhoulStat.PhysicalDamageMultiplier(T)
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