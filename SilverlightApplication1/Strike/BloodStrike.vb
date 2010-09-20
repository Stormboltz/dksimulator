Namespace Simulator.WowObjects.Strikes
    Friend Class BloodStrike
        Inherits Strike

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            BaseDamage = 611.2
            If sim.Sigils.DarkRider Then BaseDamage += 90
            Coeficient = 0.8
            Multiplicator = 1

            Multiplicator = Multiplicator * (1 + sim.Character.Talents.Talent("BloodoftheNorth").Value * 5 / 100)
            If sim.Character.T92PTNK = 1 Then Multiplicator = Multiplicator * 1.05
            logLevel = LogLevelEnum.Basic
            If sim.Character.T84PDPS = 1 Then
                DiseaseBonus = 0.1 * 1.2
            Else
                DiseaseBonus = 0.1
            End If


        End Sub



        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If MyBase.ApplyDamage(T) = False Then
                UseGCD(T)
                sim.Runes.UseBlood(T, True, True)
                Return False
            End If


            If OffHand = False Then
                UseGCD(T)
                sim.RunicPower.add(15)
                sim.Runes.UseBlood(T, True)
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnBloodStrike)
            End If
            Return True
        End Function



    End Class
End Namespace