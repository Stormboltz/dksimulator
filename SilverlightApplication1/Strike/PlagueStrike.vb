Namespace Simulator.WowObjects.Strikes
    Friend Class PlagueStrike
        Inherits Strike
        Sub New(ByVal S As Sim)
            MyBase.New(S)
            If S.level85 Then
                BaseDamage = 420 * 100 / 100
            Else
                BaseDamage = 378 * 100 / 100
            End If

            Coeficient = 1
            Multiplicator += sim.Character.Talents.Talent("RageOfRivendare").Value * 15 / 100
            logLevel = LogLevelEnum.Basic
            SpecialCritChance += sim.Character.T72PTNK * 0.1
            _CritCoef = (1 + 0.06 * sim.Character.CSD)
            Resource = New Resource(S, ResourcesEnum.UnholyRune, False, 10)
        End Sub
        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                'UseAlf()
                Return False
            End If

            If OffHand = False Then
                Use()

                If sim.Targets.MainTarget.BloodPlague.isActive(T) Then
                    sim.proc.CrimsonScourge.TryMe(T)
                End If
                sim.proc.Strife.TryMe(T)
                sim.Targets.MainTarget.BloodPlague.Apply(T)
                If sim.DRW.IsActive(T) Then
                    sim.DRW.DRWPlagueStrike()
                End If
            End If
            Return True
        End Function


    End Class
End Namespace