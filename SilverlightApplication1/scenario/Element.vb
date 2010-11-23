
Namespace Simulator.Scenarios
    Public Class Element
        Friend CanTakePetDamage As Boolean = True
        Friend CanTakeDiseaseDamage As Boolean = True
        Friend CanTakePlayerStrike As Boolean = True
        Friend AddPop As Integer = 0
        Friend DamageBonus As Integer = 0
        Friend SpreadDisease As Boolean = True
        Friend FightStop As Integer

        Friend Start As Long
        Friend length As Long
        Protected sim As Sim
        Protected Scenario As Scenario


        Sub New(ByVal Scenar As Scenario)
            Scenario = Scenar
            sim = Scenario.sim
            Scenario.Elements.Add(Me)
        End Sub

        Function Ending() As Long
            Return (Start + length)
        End Function




    End Class
End Namespace
