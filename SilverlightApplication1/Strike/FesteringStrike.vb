Namespace Simulator.WowObjects.Strikes
    Public Class FesteringStrike
        Inherits Strike
        Sub New(ByVal S As Sim)
            MyBase.New(S)

            BaseDamage = 560


            Coeficient = 1.5
            Multiplicator = 1
            Coeficient += (sim.Character.Talents.Talent("RageOfRivendare").Value * 15 / 100)
            Resource = New Resource(S, Resource.ResourcesEnum.BloodFrostRune, True, 20)
            logLevel = LogLevelEnum.Basic
        End Sub

        'Festering Strike *New* - An instant attack that deals 150% weapon damage 
        'plus 840.54 and increases the duration of your Blood Plague, Frost Fever, 
        'and Chains of Ice effects on the target by up to 6 sec. / 5 yd range, Blood+Frost

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                'UseAlf()


                Return False
            End If

            If OffHand = False Then

                Use()

                sim.Targets.MainTarget.BloodPlague.IncreaseDuration(800)
                sim.Targets.MainTarget.FrostFever.IncreaseDuration(800)

            End If
            Return True
        End Function





    End Class
End Namespace