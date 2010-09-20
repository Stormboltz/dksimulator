Namespace Simulator.WowObjects.Spells
    Friend Class HowlingBlast
        Inherits Spell
        Dim alternateRessource As Resource
        Dim talented As Boolean
        Friend Glyphed As Boolean

        Sub New(ByVal S As sim)
            MyBase.New(S)
            BaseDamage = 1079
            Coeficient = 0.2
            Multiplicator = 1
            Resource = New Resource(S, ResourcesEnum.FrostRune, 15, False)
            alternateRessource = New Resource(S, ResourcesEnum.None, 0, False)
            If sim.Character.Talents.Talent("HowlingBlast").Value = 1 Then talented = True
            If S.Character.Talents.GetNumOfThisSchool(Character.Talents.Schools.Frost) > 20 Then
                Multiplicator = Multiplicator * 1.2 'Frozen Heart
            End If
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

            If sim.proc.Rime.IsActive Then
                sim.proc.Rime.Use()
            Else
                Resource.Use()
            End If

            Dim Tar As Targets.Target

            For Each Tar In sim.Targets.AllTargets
                If DoMySpellHit() = False Then
                    sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                    sim.proc.Rime.Use()
                    MissCount = MissCount + 1
                Else
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
                    sim.RunicPower.add(sim.Character.Talents.Talent("ChillOfTheGrave").Value * 5)
                    sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnDamage)
                End If
            Next

            If Glyphed Then
                sim.Targets.MainTarget.FrostFever.Apply(T)
            End If

            Return True
        End Function
        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim tmp As Double = MyBase.AvrgNonCrit(T, target)

            If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
            tmp *= sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
            If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
            tmp *= 1 + sim.Character.Mastery.Value * 2.5
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
