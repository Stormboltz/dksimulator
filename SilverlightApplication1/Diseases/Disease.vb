'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 13/09/2009
' Heure: 14:25
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Diseases
    Public Class Disease
        Inherits WowObject

        Friend nextTick As Long
        Friend FadeAt As Long

        Friend AP As Integer

        Friend DamageTick As Integer
        Friend ScourgeStrikeGlyphCounter As Integer
        Friend OtherTargetsFade As Integer
        Friend CritChance As Double
        Friend Multiplier As Double

        Private _Lenght As Integer
        Friend previousFade As Long


        Friend Cinder As Boolean


        Friend ToReApply As Boolean
        Friend ToReApplyWithFest As Boolean

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            init()
            Sim = S
            Sim.DamagingObject.Add(Me)
        End Sub

        Protected Overridable Sub init()
            nextTick = 0
            FadeAt = 0
            total = 0
            MissCount = 0
            HitCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
            AP = 0
            OtherTargetsFade = 0
            ThreadMultiplicator = 1
            ToReApply = True
            ToReApplyWithFest = False
            _RNG1 = Nothing
        End Sub

        Function Lenght() As Integer
            If _Lenght = 0 Then
                _Lenght = 2100 + 400 * sim.Character.Talents.Talent("Epidemic").Value
            End If
            Return _Lenght
        End Function

        Sub IncreaseDuration(ByVal T As Long)
            ToReApply = False
            ToReApplyWithFest = False
            FadeAt += T
            uptime += T
        End Sub

        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
            nextTick = 0
            FadeAt = 0

            ToReApply = True
            ToReApplyWithFest = False
        End Sub

       

        Overridable Function isActive(ByVal T As Long) As Boolean
            If T > FadeAt Then
                isActive = False
            Else
                isActive = True
            End If
        End Function

         

        Overridable Function CalculateCritChance(ByVal T As Long) As Double
            Return sim.Character.SpellCrit.Value
        End Function

        Overridable Function CalculateMultiplier(ByVal T As Long, ByVal target As Targets.Target) As Double

            Dim tmp As Double
            Multiplicator = 1

            tmp = Sim.Character.StandardMagicalDamageMultiplier(T)
            If Sim.RuneForge.CheckCinderglacier(False) > 0 Then tmp *= 1.2
            Multiplicator += sim.Character.Talents.Talent("EbonPlaguebringer").Value * 15 / 100


            If sim.Character.Talents.MainSpec = (Character.Talents.Schools.Unholy) Then
                If Not sim.NextPatch Then
                    Multiplicator += 5 * sim.Character.Mastery.Value
                Else
                    Multiplicator += 0.1 * sim.Character.Talents("Virulence")
                End If

            End If
            If target.Equals(sim.Targets.MainTarget) = False Then
                tmp = tmp / 2
                tmp *= (1 + sim.Character.Talents.Talent("Contagion").Value * 0.5)
            End If

            Return tmp
        End Function

        Function Apply(ByVal T As Long) As Boolean
            Apply(T, Sim.Targets.MainTarget)
            Return True
        End Function

        Overridable Function Apply(ByVal T As Long, ByVal target As Targets.Target) As Boolean
            ToReApply = False
            ToReApplyWithFest = False
            If nextTick <= T Then
                nextTick = T + 3 * 100
            End If

            Sim.FutureEventManager.Add(nextTick, "Disease")
            ScourgeStrikeGlyphCounter = 0
            CritChance = CalculateCritChance(T)
            If Sim.RuneForge.CheckCinderglacier(False) > 0 Then
                Cinder = True
            Else
                Cinder = False
            End If
            Multiplier = CalculateMultiplier(T, target)
            Refresh(T)
            Return True
        End Function

        Overridable Function Refresh(ByVal T As Long) As Boolean
            
            FadeAt = T + Lenght()
            AP = sim.Character.AP.CurrentValue
            DamageTick = AvrgNonCrit(T)
            AddUptime(T)
            Return True
        End Function

        Overridable Function AvrgNonCrit(ByVal T As Long) As Double
            Return Multiplier * 1.15 * (26 + 0.055 * AP)
        End Function

        Function ApplyDamage(ByVal T As Long) As Boolean
            Dim tmp As Double
            Dim intCount As Integer
            RngHit()

            If intCount > 1 And OtherTargetsFade < T Then Return True
            If RngCrit < CritChance Then
                tmp = AvrgCrit(T)
                CritCount = CritCount + 1
                totalcrit += tmp
            Else
                tmp = DamageTick
                HitCount = HitCount + 1
                totalhit += tmp
            End If
            total = total + tmp

            sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnDoT)
            nextTick = T + 300
            sim.FutureEventManager.Add(nextTick, "Disease")
            If FadeAt < T + 1000 Then
                ToReApplyWithFest = True
                If nextTick > FadeAt Then
                    ToReApply = True
                End If
            End If
            
            If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & Me.ToString & " hit for " & tmp)

            Return True
        End Function

        Protected _CritCoef As Double = -1
        Overridable Function CritCoef() As Double
            If _CritCoef <> -1 Then Return _CritCoef
            _CritCoef = 1 + 0.06 * Sim.Character.CSD
            Return _CritCoef
        End Function

        Overridable Function AvrgCrit(ByVal T As Long) As Double
            Return DamageTick * (1 + CritCoef())
        End Function
        Public Sub cleanup()
            total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub
        Sub AddUptime(ByVal T As Long)
            If Not sim.CalculateUPtime Then Return
            Dim tmp As Long
            If Lenght() + T > sim.NextReset Then
                tmp = (sim.NextReset - T)
            Else
                tmp = Lenght()
            End If

            If previousFade < T Then
                uptime += tmp
            Else
                uptime += tmp - (previousFade - T)
            End If
            previousFade = T + tmp
        End Sub
        Sub RemoveUptime(ByVal T As Long)
            If Not sim.CalculateUPtime Then Return
            If previousFade < T Then
            Else
                uptime -= (previousFade - T)
            End If
            previousFade = T
        End Sub


    End Class
End Namespace