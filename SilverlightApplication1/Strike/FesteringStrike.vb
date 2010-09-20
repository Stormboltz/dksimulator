Namespace Simulator.WowObjects.Strikes
    Public Class FesteringStrike
        Inherits Strike
        Sub New(ByVal S As Sim)
            MyBase.New(S)
            BaseDamage = 840
            Coeficient = 1.5
            Multiplicator = 1
            Multiplicator *= (1 + sim.Character.Talents.Talent("RageOfRivendare").Value * 15 / 100)
            Resource = New Resource(S, ResourcesEnum.BloodFrostRune, 25, True)
            logLevel = LogLevelEnum.Basic
        End Sub

        'Festering Strike *New* - An instant attack that deals 150% weapon damage 
        'plus 840.54 and increases the duration of your Blood Plague, Frost Fever, 
        'and Chains of Ice effects on the target by up to 6 sec. / 5 yd range, Blood+Frost

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If MyBase.ApplyDamage(T) = False Then
                UseAlf()

                UseGCD(T)
                Return False
            End If

            If OffHand = False Then
                UseGCD(T)
                Use()

                Sim.Targets.MainTarget.BloodPlague.IncreaseDuration(600)
                Sim.Targets.MainTarget.FrostFever.IncreaseDuration(600)

            End If
            Return True
        End Function





    End Class
End Namespace