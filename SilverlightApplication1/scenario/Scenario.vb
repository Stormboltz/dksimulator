Imports System.Xml.Linq

'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/22/2010
' Heure: 2:12 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Scenarios
    Public Class Scenario
        Friend sim As Sim
        Friend Elements As New Collection


        Sub New(ByVal s As Sim)
            sim = s
        End Sub

        Sub SoftReset()
            On Error Resume Next
            Elements.Clear()
            Dim xmlScenario As New XDocument
            xmlScenario.Load(sim.ScenarioPath)
            Dim e As Element
            Dim xNode As XElement
            Dim id As Integer
            For Each xNode In xmlScenario.Element("/Scenario/Element").Elements
                id = xNode.Attribute("id").Value
                e = New Element(Me)
                e.CanTakeDiseaseDamage = xNode.Element("/Scenario/Element[@id=" & id & "]/CanTakeDiseaseDamage").Value
                e.CanTakePetDamage = xNode.Element("/Scenario/Element[@id=" & id & "]/CanTakePetDamage").Value
                e.CanTakePlayerStrike = xNode.Element("/Scenario/Element[@id=" & id & "]/CanTakePlayerStrike").Value
                e.AddPop = xNode.Element("/Scenario/Element[@id=" & id & "]/AddPop").Value
                e.DamageBonus = xNode.Element("/Scenario/Element[@id=" & id & "]/DamageBonus").Value
                e.Start = sim.TimeStamp + (xNode.Element("/Scenario/Element[@id=" & id & "]/Start").Value * xNode.Element("/Scenario/Element[@id=" & id & "]/Start").Attribute("multi").Value)
                e.length = xNode.Element("/Scenario/Element[@id=" & id & "]/length").Value * xNode.Element("/Scenario/Element[@id=" & id & "]/length").Attribute("multi").Value
                e.SpreadDisease = xNode.Element("/Scenario/Element[@id=" & id & "]/SpreadDisease").Value
                e.FightStop = sim.TimeStamp + xNode.Element("/Scenario/Element[@id=" & id & "]/FightStop").Value * xNode.Element("/Scenario/Element[@id=" & id & "]/FightStop").Attribute("multi").Value
                If e.CanTakeDiseaseDamage = False Or e.CanTakePetDamage = False Or e.CanTakePlayerStrike = False Then
                    sim.FutureEventManager.Add(e.Start, "Scenario")
                End If
                If e.DamageBonus <> 0 Then
                    sim.FutureEventManager.Add(e.Start, "SuperBuff")
                End If
                If e.AddPop <> 0 Then
                    sim.FutureEventManager.Add(e.Start, "AddPop")
                    sim.FutureEventManager.Add(e.Ending, "AddDepop")
                End If
                If e.FightStop <> 0 Then
                    If sim.FightLength = 0 Then
                        sim.FightLength = e.FightStop / 100
                    End If
                    sim.FutureEventManager.Add(e.FightStop, "FightStop")
                    sim.NextReset = e.FightStop
                    If sim.NextReset > sim.MaxTime Then sim.NextReset = sim.MaxTime
                End If

            Next
        End Sub


    End Class
End Namespace
