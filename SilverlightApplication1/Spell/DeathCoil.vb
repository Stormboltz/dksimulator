Namespace Simulator.WowObjects.Spells
    Friend Class DeathCoil
        Inherits Spells.Spell
        Sub New(ByVal S As Sim)
            MyBase.New(S)
            'Base Damage
                BaseDamage = 985
            Resource = New Resource(S, Resource.ResourcesEnum.RunicPower, 40 - (3 * sim.Character.Talents.Talent("RunicCorruption").Value))
            Coeficient = 0.3
            Multiplicator += sim.Character.Talents.Talent("Morbidity").Value * 0.05
            If sim.Character.Glyph("DeathCoil") Then
                Multiplicator += 0.15
            End If
            AdditionalCritChance += 8 * sim.Character.T82PDPS / 100
            AdditionalCritChance += 5 * sim.Character.T112PDPS / 100
            logLevel = LogLevelEnum.Basic

        End Sub



        Overrides Function isAvailable() As Boolean
            If MyBase.IsAvailable Then Return True
            Return sim.proc.SuddenDoom.IsActive
        End Function

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean

            If sim.RunicPower.Check(60) Then
                If sim.DRW.Talented Then
                    If sim.DRW.cd < T Then
                        If sim.DRW.Summon(T) Then Return True
                    End If
                ElseIf sim.Gargoyle.Talented Then
                    If sim.Gargoyle.cd < T Then
                        If sim.Gargoyle.Summon(T) Then Return True
                    End If
                End If
            End If

            
            UseGCD(T)
            Dim ret As Boolean = MyBase.ApplyDamage(T)
            If sim.proc.SuddenDoom.IsActive Then
                sim.proc.SuddenDoom.Cancel()
            Else
                Resource.Use()
            End If
            If ret = False Then
                Return False
            End If

            sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.onRPDump)
            If sim.Character.Talents.Talent("UnholyBlight").Value = 1 Then
                sim.UnholyBlight.Apply(T, LastDamage)
            End If
            If sim.DRW.IsActive(T) Then
                sim.DRW.DRWDeathCoil()
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
