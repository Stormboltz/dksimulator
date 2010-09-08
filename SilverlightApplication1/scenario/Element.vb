'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/22/2010
' Heure: 2:13 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
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
