Imports System.Xml.Linq

'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/08/2009
' Heure: 21:30
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Procs
    Public Class Trinkets

        Protected XmlCharacter As XDocument





        Default ReadOnly Property Trinket(ByVal TrinketName As String) As Trinket
            Get
                Return GetTrinketByName(TrinketName)
            End Get
        End Property


        Function GetTrinketByName(ByVal TrinketName) As Trinket
            Try


                Dim Query = (From tk In sim.TrinketsCollection
                             Where tk._Name = TrinketName)
                Dim i As Integer = Query.Count
                If i = 0 Then
                    Return Nothing
                Else
                    Return Query.First
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function


        Protected sim As Sim
        Sub New(ByVal S As Sim)



            sim = S
            XmlCharacter = S.XmlCharacter

            Dim doc As XDocument = XDocument.Load("GearSelector/TrinketList.xml")

            For Each t In doc.<TrinketList>.Elements




                Dim tk = New Trinket(S)
                Try
                    With tk

                        ._Name = t.@name
                        Select Case t.@procon
                            Case "OnDoT"
                                .ProcOn = ProcsManager.ProcOnType.OnDoT
                            Case "OnCrit"
                                .ProcOn = ProcsManager.ProcOnType.OnCrit
                            Case "OnHit"
                                .ProcOn = ProcsManager.ProcOnType.OnHit
                            Case "OnDamage"
                                .ProcOn = ProcsManager.ProcOnType.OnDamage
                            Case Else
                                .ProcOn = ProcsManager.ProcOnType.OnMisc
                        End Select
                        .InternalCD = t.@internalcd

                        .ProcChance = Todouble(t.@procchance)
                        Select Case t.@procstat
                            Case "AP"
                                .ProcType = Simulator.Sim.Stat.AP
                            Case "Crit"
                                .ProcType = Simulator.Sim.Stat.Crit
                            Case "haste"
                                .ProcType = Simulator.Sim.Stat.Haste
                            Case "Mast"
                                .ProcType = Simulator.Sim.Stat.Mastery
                            Case "Strength"
                                .ProcType = Simulator.Sim.Stat.Strength
                            Case ""
                                .ProcType = Simulator.Sim.Stat.None
                            Case Else
                                .ProcType = Simulator.Sim.Stat.None
                        End Select

                        .ProcValue = t.@procvalue
                        .DamageType = t.@damagetype
                        .ProcLenght = t.@proclenght
                        .MaxStack = t.@maxstack
                        If .DamageType <> "" And .ProcType = Simulator.Sim.Stat.None Then
                        Else
                            If .MaxStack > 1 Then
                                .Effects.Add(New SpellBuff(S, ._Name, .ProcType, .ProcValue, .MaxStack, .ProcLenght))
                            Else
                                .Effects.Add(New SpellBuff(S, ._Name, .ProcType, .ProcValue, .ProcLenght))
                            End If
                        End If






                    End With
                Catch ex As Exception

                    Log.Log("Error with " & tk._Name, logging.Level.ERR)
                End Try


            Next
            'CollectDamagingTrinket
        End Sub

        Sub CollectDamagingTrinket()
            Dim tk As Trinket
            For Each tk In sim.TrinketsCollection
                If tk.DamageType <> "" Then
                    sim.DamagingObject.Add(tk)
                End If
            Next
        End Sub




    End Class
End Namespace