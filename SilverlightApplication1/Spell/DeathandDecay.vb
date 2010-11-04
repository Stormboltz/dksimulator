'
' Created by SharpDevelop.
' User: Fabien
' Date: 24/03/2009
' Time: 22:35
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Spells
    Friend Class DeathandDecay
        Inherits Spell
        Dim Length As Integer
        Friend nextTick As Long

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            If s.level85 Then
                BaseDamage = 46
            Else
                BaseDamage = 31
            End If

            Coeficient = (0.0475)
            Multiplicator = 1
            Multiplicator += sim.Character.Talents.Talent("Morbidity").Value * 0.1
            If sim.Character.T102PTNK = 1 Then Multiplicator += 0.2
            Length = 1000
            If sim.Character.Glyph("DeathAndDecay") Then Length *= 1.5
            logLevel = LogLevelEnum.Basic
            Resource = New Resource(sim, ResourcesEnum.UnholyRune, False, 10)
        End Sub

        Public Overloads Overrides Sub Init()
            MyBase.Init()
            nextTick = 0
            ThreadMultiplicator = 1.9
        End Sub

        Overrides Function isAvailable() As Boolean
            If CD > sim.TimeStamp Then Return False
            Return MyBase.IsAvailable
        End Function

        Function Apply(ByVal T As Long) As Boolean
            UseGCD(T)
            nextTick = T + 100
            ActiveUntil = T + Length
            CD = T + 3000
            Use()
            Sim.CombatLog.write(T & vbTab & "D&D ")
            Sim.FutureEventManager.Add(nextTick, "D&D")

            Return True
        End Function

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Dim RNG As Double

            Dim Tar As Targets.Target

            For Each Tar In Sim.Targets.AllTargets
                If DoMySpellHit() = False Then
                    If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                    MissCount = MissCount + 1
                Else
                    RNG = RngCrit
                    If RNG <= CritChance() Then
                        LastDamage = AvrgCrit(T, Tar)
                        If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & Me.Name & " crit for " & LastDamage)
                        CritCount = CritCount + 1
                        TotalCrit += LastDamage
                    Else
                        LastDamage = AvrgNonCrit(T, Tar)
                        HitCount = HitCount + 1
                        TotalHit += LastDamage
                        If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & Me.Name & " hit for " & LastDamage)
                    End If
                    total = total + LastDamage
                End If
            Next
            nextTick = T + 100
            If nextTick > ActiveUntil Then
                nextTick = T - 1
            Else
                Sim.FutureEventManager.Add(nextTick, "D&D")
            End If
            Return True
        End Function

        Overrides Function AvrgCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            AvrgCrit = AvrgNonCrit(T, target) * (0.5 + CritCoef())
        End Function


    End Class
End Namespace