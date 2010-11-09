Namespace Simulator.WowObjects.Strikes
    Friend Class BloodStrike
        Inherits Strike

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            If S.level85 Then
                BaseDamage = 850
            Else
                BaseDamage = 764
            End If

            'If sim.Sigils.DarkRider Then BaseDamage += 90
            Coeficient = 0.8
            If sim.Character.T92PTNK = 1 Then Coeficient += 0.05
            Multiplicator = 1
            logLevel = LogLevelEnum.Basic
            If sim.Character.T84PDPS = 1 Then
                DiseaseBonus = 0.1 * 1.2
            Else
                DiseaseBonus = 0.1
            End If
            Resource = New Resource(S, Resource.ResourcesEnum.BloodRune, True, 10)

        End Sub



        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then

                'UseAlf()
                Return False
            End If
            If OffHand = False Then

                Use()
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnBloodStrike)
            End If
            Return True
        End Function
    End Class
End Namespace