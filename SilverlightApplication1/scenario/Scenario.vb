Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO


Namespace Simulator.Scenarios
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

            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & sim.ScenarioPath, FileMode.Open, FileAccess.Read, sim.isoStore)
                xmlScenario = XDocument.Load(isoStream)
            End Using



            Dim e As Element
            Dim xNode As XElement
            Dim id As Integer
            For Each xNode In xmlScenario.Element("Scenario").Elements
                id = xNode.Attribute("id").Value
                e = New Element(Me)
                For Each el In xNode.Elements
                    Select Case el.Name
                        Case "CanTakeDiseaseDamage"
                            e.CanTakeDiseaseDamage = xNode.Element("CanTakeDiseaseDamage").Value
                        Case "CanTakePetDamage"
                            e.CanTakePetDamage = xNode.Element("CanTakePetDamage").Value
                        Case "CanTakePlayerStrike"
                            e.CanTakePlayerStrike = xNode.Element("CanTakePlayerStrike").Value
                        Case "AddPop"
                            e.AddPop = xNode.Element("AddPop").Value
                        Case "DamageBonus"
                            e.DamageBonus = xNode.Element("DamageBonus").Value
                        Case "Start"
                            e.Start = sim.TimeStamp + (xNode.Element("Start").Value * xNode.Element("Start").Attribute("multi").Value)
                        Case "length"
                            e.length = xNode.Element("length").Value * xNode.Element("length").Attribute("multi").Value
                        Case "SpreadDisease"
                            e.SpreadDisease = xNode.Element("SpreadDisease").Value
                        Case "FightStop"
                            e.FightStop = sim.TimeStamp + xNode.Element("FightStop").Value * xNode.Element("FightStop").Attribute("multi").Value
                    End Select
                Next

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
