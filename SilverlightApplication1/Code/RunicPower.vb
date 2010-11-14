Namespace Simulator
    Friend Class RunicPower
        Private Value As Double
        Friend MaxValue As Integer
        Friend Wasted As Integer
        Friend Total As Integer
        Friend RPBonus As Double
        Protected sim As Sim
        Sub New(ByVal S As Sim)
            sim = S
            Value = 0
            Total = 0

            RPBonus = 1
            If sim.FrostPresence > 0 Then
                RPBonus += 0.1
            ElseIf sim.Character.Talents("IFrostPresence") > 0 Then
                RPBonus += (0.02 * sim.Character.Talents("IFrostPresence"))
            End If



        End Sub

        Sub SoftReset()
            MaxValue = 100
            'If sim.Character.Talents.Talent("IcyTalons").Value > 0 Then MaxValue += 30
            MaxValue += sim.Character.Talents.Talent("RunicPowerMastery").Value * 10
            Value = 0 'Start fight with no RP
        End Sub

        Sub Use(ByVal Cost As Integer)
            Value -= Cost
            Total += Cost
        End Sub
        Sub UseAll()
            Total += Value
            Value = 0
        End Sub

        Function Check(ByVal Cost As Integer) As Boolean
            Return Value >= Cost
        End Function

        Function Report() As String
            Return "Total runic power used: " & Total & " (" & Wasted & " wasted)"
        End Function

        Function CheckRS(ByVal Cost As Integer) As Boolean
            If sim.SaveRPForRS Then
                Return Value >= Cost + 20
            Else
                Return Value >= Cost
            End If
        End Function

        Function GetValue() As Integer
            Return Value
        End Function

        Function CheckMax(ByVal Delta As Integer) As Boolean
            Return Value + Delta >= MaxValue
        End Function


        Sub add(ByVal i As Double) ' Before presence
            i = i * RPBonus
            sim.Threat = sim.Threat + i * 5
            Value = i + Value
            'Diagnostics.Debug.WriteLine ("RP= " & Value)
            If Value > MaxValue Then
                Wasted += Value - MaxValue
                Value = MaxValue
            End If
            If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "Runic Power = " & Value)
        End Sub


    End Class
End Namespace
