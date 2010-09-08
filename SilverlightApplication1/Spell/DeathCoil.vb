Namespace Simulator.WowObjects.Spells
    Friend Class DeathCoil
        Inherits Spells.Spell
        Sub New(ByVal S As Sim)
            MyBase.New(S)
            'Base Damage
            BaseDamage = 885
            If Sim.Sigils.VengefulHeart Then BaseDamage += 380
            If Sim.Sigils.WildBuck Then BaseDamage += 80

            Coeficient = (0.15 * (1 + 0.2 * Sim.Character.Talents.Talent("Impurity").Value))
            Multiplicator = 1 + Sim.Character.Talents.Talent("Morbidity").Value * 5 / 100
            If Sim.Character.Glyph.DarkDeath Then
                Multiplicator = Multiplicator * (1.15)
            End If
            SpecialCritChance = 8 * Sim.Character.T82PDPS / 100

            logLevel = LogLevelEnum.Basic

        End Sub



        Function isAvailable(ByVal T As Long) As Boolean
            Return Sim.RunicPower.CheckRS(40) Or Sim.proc.SuddenDoom.IsActive
        End Function

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean

            If Sim.proc.SuddenDoom.IsActive Then
                Sim.proc.SuddenDoom.Use()
            Else
                If Sim.RunicPower.Check(60) Then
                    If Sim.Character.Talents.Talent("DRW").Value = 1 Then
                        If Sim.DRW.cd < T Then
                            If Sim.DRW.Summon(T) = True Then Return True
                        End If
                    ElseIf Sim.PetFriendly And Sim.Character.Talents.Talent("SummonGargoyle").Value = 1 Then
                        If Sim.Gargoyle.cd < T Then
                            If Sim.Gargoyle.Summon(T) = True Then Return True
                        End If
                    End If
                End If
                Sim.RunicPower.Use(40)
            End If
            UseGCD(T)
            Dim ret As Boolean = MyBase.ApplyDamage(T)
            If ret = False Then
                Return False
            End If

            sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.onRPDump)
            If Sim.Character.Talents.Talent("UnholyBlight").Value = 1 Then
                Sim.UnholyBlight.Apply(T, LastDamage)
            End If
            If Sim.DRW.IsActive(T) Then
                Sim.DRW.DRWDeathCoil()
            End If
            Return True

        End Function
        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = MyBase.AvrgNonCrit(T, target)
            If Sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
            Return tmp
        End Function

    End Class
End Namespace
