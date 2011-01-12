Namespace Simulator.WowObjects.Strikes
    Friend Class PlagueStrike
        Inherits Strike
        Sub New(ByVal S As Sim)
            MyBase.New(S)

            BaseDamage = 420


            Coeficient = 1
            Coeficient += sim.Character.Talents.Talent("RageOfRivendare").Value * 15 / 100
            logLevel = LogLevelEnum.Basic
            AdditionalCritChance += sim.Character.T72PTNK * 0.1
            _CritCoef = (1 + 0.06 * sim.Character.CSD)
            Resource = New Resource(S, Resource.ResourcesEnum.UnholyRune, False, 10)
        End Sub
        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                'UseAlf()
                Return False
            End If

            If OffHand = False Then
                Use()
                If Not sim.NextPatch Then
                    If sim.Targets.MainTarget.BloodPlague.isActive(T) Then
                        sim.proc.CrimsonScourge.TryMe(T)
                    End If
                End If


                sim.Targets.MainTarget.BloodPlague.Apply(T)
                If sim.DRW.IsActive(T) Then
                    sim.DRW.DRWPlagueStrike()
                End If
            End If
            Return True
        End Function


    End Class
End Namespace