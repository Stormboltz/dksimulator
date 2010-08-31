﻿'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 13/09/2009
' Heure: 14:25
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Strikes

    Public Class Strike
        Inherits Supertype
        Friend OffHand As Boolean
        Friend OffHandStrike As Strike



        Sub UseGCD(ByVal T As Long)
            sim.UseGCD(T, False)
        End Sub
        Public Sub New()
            DamageSchool = DamageSchoolEnum.Physical
            init()
        End Sub
        Protected Overridable Sub init()
            total = 0
            MissCount = 0
            HitCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
            ThreadMultiplicator = 1
            DiseaseBonus = 0

            _RNG1 = Nothing
        End Sub

        Sub New(ByVal S As Sim)
            Me.New()
            sim = S
            sim.DamagingObject.Add(Me)
        End Sub


        Overrides Function Name() As String
            If _Name <> "" Then Return _Name
            If OffHand = False Then
                Return Me.ToString
            Else
                Return Me.ToString & "(OH)"
            End If
        End Function

        Function DoMyToTHit() As Boolean
            RngHit()
            Return True
        End Function

        Function DoMyStrikeHit() As Boolean
            Dim RNG As Double
            RNG = RngHit()
            Dim exp As Double

            If Me.OffHand Then
                exp = sim.MainStat.OHExpertise
            Else
                exp = sim.MainStat.MHExpertise
            End If


            If sim.BloodPresence = 1 Then
                If Math.Min(exp, 0.065) + Math.Min(exp, 0.14) + Math.Min(sim.MainStat.Hit, 0.08) + RNG < 0.285 Then
                    Return False
                Else
                    Return True
                End If
            Else
                If Math.Min(exp, 0.065) + Math.Min(sim.MainStat.Hit, 0.08) + RNG < 0.145 Then
                    Return False
                Else
                    Return True
                End If
            End If
        End Function

        Public Overridable Function isAvailable(ByVal T As Long) As Boolean
            Return False
        End Function

        Public Overridable Function ApplyDamage(ByVal T As Long) As Boolean
            Dim RNG As Double
            LastDamage = 0

            If OffHand = False Then
                If sim.proc.ThreatOfThassarian.TryMe(T) Then OffHandStrike.ApplyDamage(T)
                If DoMyStrikeHit() = False Then
                    Select Case logLevel
                        Case LogLevelEnum.Basic
                            sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                        Case LogLevelEnum.Detailled
                            sim.CombatLog.WriteDetails(T & vbTab & Me.Name & " fail")
                    End Select

                    MissCount += 1
                    Return False
                End If
            Else

                If DoMyToTHit() = False Then Return False
            End If

            RNG = RngCrit

            If RNG <= CritChance() Then
                LastDamage = AvrgCrit(T)
                CritCount = CritCount + 1
                Select logLevel
                    Case LogLevelEnum.Basic
                        sim.CombatLog.write(T & vbTab & Me.Name & " crit for " & LastDamage)
                    Case LogLevelEnum.Detailled
                        sim.CombatLog.WriteDetails(T & vbTab & Me.Name & " crit for " & LastDamage)
                End Select

                TotalCrit += LastDamage
                sim.proc.tryProcs(Procs.ProcOnType.OnCrit)
            Else
                LastDamage = AvrgNonCrit(T)
                HitCount = HitCount + 1
                TotalHit += LastDamage
                Select Case logLevel
                    Case LogLevelEnum.Basic
                        sim.CombatLog.write(T & vbTab & Me.Name & " hit for " & LastDamage)
                    Case LogLevelEnum.Detailled
                        sim.CombatLog.WriteDetails(T & vbTab & Me.Name & " hit for " & LastDamage)
                End Select

            End If
            total = total + LastDamage

            If OffHand = False Then
                sim.proc.tryProcs(Procs.ProcOnType.OnMHhit)
            Else
                sim.proc.tryProcs(Procs.ProcOnType.OnOHhit)
            End If
            Return True
        End Function


        Overridable Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            If OffHand = False Then
                tmp = sim.MainStat.NormalisedMHDamage * Coeficient
            Else
                tmp = sim.MainStat.NormalisedOHDamage * Coeficient
            End If
            tmp += BaseDamage
            Select Case DamageSchool
                Case DamageSchoolEnum.Physical
                    tmp *= sim.MainStat.StandardPhysicalDamageMultiplier(T, target)
                Case Else
                    tmp *= sim.MainStat.StandardMagicalDamageMultiplier(T, target)
            End Select
            tmp *= Multiplicator
            If DiseaseBonus <> 0 Then
                tmp = tmp * (1 + DiseaseBonus * target.NumDesease)
            End If
            If OffHand Then
                tmp = tmp * OffDamageBonus()
            End If

            Return tmp
        End Function

        Function AvrgNonCrit(ByVal T As Long) As Double
            Return AvrgNonCrit(T, sim.Targets.MainTarget)
        End Function
        Function AvrgCrit(ByVal T As Long) As Double
            Return AvrgCrit(T, sim.Targets.MainTarget)
        End Function
        Function AvrgCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
        End Function

        Protected _CritCoef As Double = -1
        Overridable Function CritCoef() As Double
            If _CritCoef <> -1 Then Return _CritCoef
            _CritCoef = 1 + 0.06 * sim.MainStat.CSD
            Return _CritCoef
        End Function

        Overridable Function CritChance() As Double
            Return sim.MainStat.crit + SpecialCritChance
        End Function

        Private _OffDamageBonus As Double = -1
        Function OffDamageBonus() As Double
            If _OffDamageBonus = -1 Then
                _OffDamageBonus = 0.5 * (1 + sim.Character.Talents.Talent("NervesofColdSteel").Value * 8.3333 / 100)
            End If
            Return _OffDamageBonus
        End Function

        Public Overridable Sub cleanup()
            total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub

        Public Overrides Sub Merge()
            If sim.MainStat.DualW = False Then Exit Sub
            total += OffHandStrike.total
            TotalHit += OffHandStrike.TotalHit
            TotalCrit += OffHandStrike.TotalCrit

            MissCount = (MissCount + OffHandStrike.MissCount) / 2
            HitCount = (HitCount + OffHandStrike.HitCount) / 2
            CritCount = (CritCount + OffHandStrike.CritCount) / 2

            OffHandStrike.total = 0
            OffHandStrike.TotalHit = 0
            OffHandStrike.TotalCrit = 0
        End Sub

    End Class
end Namespace