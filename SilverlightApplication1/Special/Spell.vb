'
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


        Protected CD As Long
        Friend ActiveUntil As Long
        Friend UnMissable As Boolean


        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
            CD = 0
            ActiveUntil = 0
        End Sub


        Function DoMySpellHit() As Boolean
            Dim RNG As Double
            RNG = RngHit()
            If UnMissable Then Return True
            If Math.Min(sim.Character.SpellHit.Value, 0.17) + RNG < 0.17 Then
                Return False
            Else
                Return True
            End If
        End Function


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
            MyBase.New(S)
            Init()
            Sim = S
            sim.DamagingObject.Add(Me)
            Multiplicator = 1
        End Sub
        Sub UseGCD(ByVal T As Long)
            sim.UseGCD(True)
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
            Return sim.Character.SpellCrit.Value + AdditionalCritChance
        End Function

        Overridable Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = BaseDamage + sim.Character.AP.CurrentValue * Coeficient
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

        Overridable Function IsAvailable() As Boolean
            Return Me.Resource.IsAvailable
        End Function

        Overridable Sub Use()
            Me.Resource.Use()
        End Sub
        Overridable Sub UseAlf()
            Me.Resource.UseAlf()
        End Sub

    End Class
End Namespace