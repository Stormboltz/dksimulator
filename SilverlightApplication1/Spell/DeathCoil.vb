Namespace Simulator.WowObjects.Spells
    Friend Class DeathCoil
        Inherits Spells.Spell
        Sub New(ByVal S As Sim)
            MyBase.New(S)
            'Base Damage
            BaseDamage = 885
            If Sim.Sigils.VengefulHeart Then BaseDamage += 380
            If sim.Sigils.WildBuck Then BaseDamage += 80
            Resource = New Resource(S, ResourcesEnum.RunicPower, 40 - (3 * sim.Character.Talents.Talent("RunicCorruption").Value))
            Coeficient = (0.15)
            Multiplicator = 1 + sim.Character.Talents.Talent("Morbidity").Value * 0.05
            If sim.Character.Glyph("DeathCoil") Then
                Multiplicator = Multiplicator * (1.15)
            End If
            SpecialCritChance = 8 * Sim.Character.T82PDPS / 100

            logLevel = LogLevelEnum.Basic

        End Sub



        Overrides Function isAvailable() As Boolean
            If MyBase.IsAvailable Then Return True
            Return sim.proc.SuddenDoom.IsActive
        End Function

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean

            If Sim.proc.SuddenDoom.IsActive Then
                sim.proc.SuddenDoom.Cancel()
            Else
                If sim.RunicPower.Check(60) Then
                    If sim.Character.Talents.Talent("DRW").Value = 1 Then
                        If sim.DRW.cd < T Then
                            If sim.DRW.Summon(T) = True Then Return True
                        End If
                    ElseIf sim.PetFriendly And sim.Character.Talents.Talent("SummonGargoyle").Value = 1 Then
                        If sim.Gargoyle.cd < T Then
                            If sim.Gargoyle.Summon(T) = True Then Return True
                        End If
                    End If
                End If
                Resource.Use()
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
