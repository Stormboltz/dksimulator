Namespace Simulator.Character
    Public Class Stat

        Friend Name As String

        Friend AdditiveBuff As Integer
        Friend MultiplicativeBuff As Integer = 1

        Private Stat As Sim.Stat
        Protected sim As Sim
        Friend BaseValue As Integer


        Sub New(ByVal Type As Sim.Stat, ByVal S As Sim)
            sim = S
            Select Case Type
                Case sim.Stat.AP
                    Name = "Attack Power"
                Case sim.Stat.Armor
                    Name = "Armor"
                Case sim.Stat.ArP
                    Name = "Armor Penetration Rating"
                Case sim.Stat.Crit
                    Name = "Critical Stike Rating"
                Case sim.Stat.Expertise
                    Name = "Expertise Rating"
                Case sim.Stat.Haste
                    Name = "Haste Rating"
                Case sim.Stat.Hit
                    Name = "Hit Rating"
                Case sim.Stat.Mastery
                    Name = "Mastery Rating"
                Case sim.Stat.Strength
                    Name = "Strength"
                Case sim.Stat.Agility
                    Name = "Agility"
                Case Else
                    Diagnostics.Debug.WriteLine("WTF is this stack")
            End Select

        End Sub

        Function Value() As Integer
            Dim tmp As Integer
            If AdditiveBuff <> 0 Then
                BaseValue += AdditiveBuff
                AdditiveBuff = 0
            End If
            tmp = (BaseValue + VariableValue()) * MultiplicativeBuff

            Return tmp
        End Function
        Function MaxValue() As Integer
            Dim tmp As Integer
            If AdditiveBuff <> 0 Then
                BaseValue += AdditiveBuff
                AdditiveBuff = 0
            End If
            tmp = (BaseValue + sim.proc.GetMaxPossibleBonus(Stat)) * MultiplicativeBuff

            Return tmp
        End Function
        Function VariableValue() As Integer
            Return 0
        End Function

    End Class
End Namespace

