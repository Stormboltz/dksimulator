Namespace Simulator.WowObjects.Spells
    Friend Class HowlingBlast
        Inherits Spell
        Dim alternateRessource As Resource

        Friend Glyphed As Boolean

        Sub New(ByVal S As sim)
            MyBase.New(S)
                BaseDamage = 1153 + 1251 / 2

            Coeficient = 0.4
            Multiplicator = 1

            Resource = New Resource(S, Resource.ResourcesEnum.FrostRune, False, 10 + sim.Character.Talents.Talent("ChillOfTheGrave").Value * 5)
            alternateRessource = New Resource(S, Resource.ResourcesEnum.None, False, sim.Character.Talents.Talent("ChillOfTheGrave").Value * 5)
            If sim.Character.Talents.Talent("HowlingBlast").Value = 1 Then talented = True
            'If sim.Character.Talents.MainSpec = Character.Talents.Schools.Frost Then
            '    Multiplicator = Multiplicator * 1.2 'Frozen Heart
            'End If
            If sim.Character.Glyph("HowlingBlast") Then Glyphed = True
            logLevel = LogLevelEnum.Basic
            DamageSchool = DamageSchoolEnum.Frost
        End Sub
        Overrides Function isAvailable() As Boolean
            If Not talented Then Return False
            If sim.proc.Rime.IsActive Then Return True
            Return MyBase.IsAvailable
        End Function

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Dim RNG As Double
            UseGCD(T)
            Dim OneHit As Boolean
           

            Dim Tar As Targets.Target

            For Each Tar In sim.Targets.AllTargets
                If DoMySpellHit() = False Then
                    sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                    MissCount = MissCount + 1
                Else
                    OneHit = True
                    RNG = RngCrit
                    Dim ccT As Double
                    ccT = CritChance()
                    If RNG <= ccT Then
                        CritCount = CritCount + 1
                        LastDamage = AvrgCrit(T, Tar)
                        sim.CombatLog.write(T & vbTab & Me.Name & " crit for " & LastDamage)
                        TotalCrit += LastDamage
                    Else
                        HitCount = HitCount + 1
                        LastDamage = AvrgNonCrit(T, Tar)
                        sim.CombatLog.write(T & vbTab & Me.Name & " hit for " & LastDamage)
                        TotalHit += LastDamage
                    End If
                    total = total + LastDamage
                    sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnDamage)
                    If Glyphed Then
                        Tar.FrostFever.Apply(T)
                    End If
                End If
            Next
            If OneHit Then
                If sim.proc.Rime.IsActive Then
                    sim.proc.Rime.Cancel()
                    alternateRessource.Use()
                Else
                    Resource.Use()
                End If
                Return True
            Else
                sim.proc.Rime.Cancel()
                Return False
            End If

            
            Return True
        End Function
        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim tmp As Double = MyBase.AvrgNonCrit(T, target)

            If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
            tmp *= sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
            If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
            tmp *= 1 + sim.Character.Mastery.Value * 2
            Return tmp
        End Function

        Overrides Function CritChance() As Double
            'If sim.proc.KillingMachine.IsActive Then
            '    Return 1
            'End If
            Return MyBase.CritChance
        End Function



    End Class
End Namespace
