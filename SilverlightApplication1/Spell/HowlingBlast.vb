Namespace Simulator.WowObjects.Spells
    Friend Class HowlingBlast
        Inherits Spell
        Sub New(ByVal S As sim)
            MyBase.New(S)
            BaseDamage = 1079
            Coeficient = 0.2
            Multiplicator = 1
            If S.Character.Talents.GetNumOfThisSchool(Character.Talents.Schools.Frost) > 20 Then
                Multiplicator = Multiplicator * 1.2 'Frozen Heart
            End If
            logLevel = LogLevelEnum.Basic
        End Sub
        Function isAvailable(ByVal T As Long) As Boolean
            If sim.Character.Talents.Talent("HowlingBlast").Value <> 1 Then Return False
            Return True
        End Function

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Dim RNG As Double
            UseGCD(T)

            If sim.proc.Rime.IsActive Then
                sim.proc.Rime.Use()

            Else
                sim.Runes.UseFrost(T, False)
                sim.RunicPower.add(15)
            End If
            Dim Tar As Targets.Target

            For Each Tar In sim.Targets.AllTargets

                If DoMySpellHit() = False Then
                    sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                    sim.proc.KillingMachine.Use()
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


            sim.proc.KillingMachine.Use()
            If sim.character.glyph.HowlingBlast Then
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
            Return tmp
        End Function

        Overrides Function CritChance() As Double
            If sim.proc.KillingMachine.IsActive Then
                Return 1
            End If
            Return MyBase.CritChance
        End Function



    End Class
End Namespace
