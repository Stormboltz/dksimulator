﻿'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 13/09/2009
' Heure: 14:25
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Spells
    Public Class Spell
        Inherits WowObject


        Friend CD As Long
        Friend ActiveUntil As Long
        Friend UnMissable As Boolean



        Function DoMySpellHit() As Boolean
            Dim RNG As Double
            RNG = RngHit()
            If UnMissable Then Return True


            If Math.Min(Sim.Character.SpellHit, 0.17) + RNG < 0.17 Then
                Return False
            Else
                Return True
            End If
        End Function

        Sub New()
            Init()
        End Sub




        Overridable Sub Init()
            total = 0
            MissCount = 0
            HitCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
            CD = 0
            ActiveUntil = 0
            ThreadMultiplicator = 1
            _RNG1 = Nothing
            DamageSchool = DamageSchoolEnum.OtherMagical
        End Sub

        Sub New(ByVal S As Sim)
            Me.New()
            Sim = S
            Sim.DamagingObject.Add(Me)
        End Sub
        Sub UseGCD(ByVal T As Long)
            Sim.UseGCD(T, True)
        End Sub

        Public Overridable Function ApplyDamage(ByVal T As Long) As Boolean
            Dim RNG As Double
            LastDamage = 0
            If DoMySpellHit() = False Then
                Sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                MissCount = MissCount + 1
                Return False
            End If
            RNG = RngCrit

            If RNG <= CritChance() Then
                CritCount = CritCount + 1
                LastDamage = AvrgCrit(T)
                TotalCrit += LastDamage
                Sim.CombatLog.write(T & vbTab & Me.Name & " crit for " & LastDamage)
            Else
                LastDamage = AvrgNonCrit(T)
                TotalHit += LastDamage
                HitCount = HitCount + 1
                Sim.CombatLog.write(T & vbTab & Me.Name & " hit for " & LastDamage & vbTab)
            End If
            total = total + LastDamage
            sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnDamage)
            Return True
        End Function


        Protected _CritCoef As Double = -1
        Overridable Function CritCoef() As Double
            If _CritCoef <> -1 Then Return _CritCoef
            _CritCoef = 1 + 0.06 * Sim.Character.CSD
            Return _CritCoef
        End Function

        Overridable Function CritChance() As Double
            Return Sim.Character.SpellCrit + SpecialCritChance
        End Function

        Overridable Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = BaseDamage + Sim.Character.AP * Coeficient
            tmp = tmp * Multiplicator
            tmp = tmp * Sim.Character.StandardMagicalDamageMultiplier(T)
            If DiseaseBonus <> 0 Then ' For BloodBloil
                tmp = tmp * (1 + DiseaseBonus)
            End If
            Return tmp
        End Function
        Overridable Function AvrgCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Return (AvrgNonCrit(T, target) * (1 + CritCoef()))
        End Function


        Function AvrgNonCrit(ByVal T As Long) As Double
            Return AvrgNonCrit(T, Sim.Targets.MainTarget)
        End Function
        Function AvrgCrit(ByVal T As Long) As Double
            Return AvrgCrit(T, Sim.Targets.MainTarget)
        End Function

        Public Sub cleanup()
            total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub


    End Class
End Namespace