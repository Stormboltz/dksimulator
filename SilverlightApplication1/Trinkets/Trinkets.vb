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

            Dim SharpenedTwilightScaleHeroic As New Trinket(S)
            With SharpenedTwilightScaleHeroic
                ._Name = "SharpenedTwilightScaleHeroic"
                .ProcOn = ProcsManager.ProcOnType.OnDamage
                .ProcChance = 0.35
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "Sharpened Twilight Scale Heroic", Simulator.Sim.Stat.AP, 1742, 15))
                .ProcLenght = 15
            End With
            Dim SharpenedTwilightScale As New Trinket(S)
            With SharpenedTwilightScale
                ._Name = "SharpenedTwilightScale"
                .ProcOn = ProcsManager.ProcOnType.OnDamage
                .ProcChance = 0.35
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "Sharpened Twilight Scale", Simulator.Sim.Stat.AP, 1304, 15))
                .ProcLenght = 15
            End With

            Dim HerkumlWarToken = New Trinket(S)
            With HerkumlWarToken
                ._Name = "HerkumlWarToken"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 1
                .InternalCD = 0
                .MaxStack = 20
                .Effects.Add(New SpellBuff(S, "Herkuml War Token", Simulator.Sim.Stat.AP, 17, 20, 10))
                .ProcValueStack = 17
                .ProcLenght = 10
            End With

            Dim MarkofSupremacy = New Trinket(S)
            With MarkofSupremacy
                ._Name = "MarkofSupremacy"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 0.5
                .InternalCD = 120
                .ProcLenght = 20
                .Effects.Add(New SpellBuff(sim, "Mark of Supremacy", Simulator.Sim.Stat.AP, 1024, 20))
                .ProcValue = "1024"
            End With

            Dim VengeanceoftheForsaken = New Trinket(S)
            With VengeanceoftheForsaken
                ._Name = "VengeanceoftheForsaken"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 0.5
                .InternalCD = 120
                .ProcLenght = "20"
                .Effects.Add(New SpellBuff(sim, "Vengeance of the Forsaken", Simulator.Sim.Stat.AP, 860, 20))
                .ProcValue = "860"
            End With

            Dim VengeanceoftheForsakenHeroic = New Trinket(S)
            With VengeanceoftheForsakenHeroic
                ._Name = "VengeanceoftheForsakenHeroic"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 0.5
                .InternalCD = 120
                .ProcLenght = 20
                .Effects.Add(New SpellBuff(sim, "Vengeance of the Forsaken Heroic", Simulator.Sim.Stat.AP, 1000, 20))
                .ProcValue = "1000"
            End With

            Dim TinyAbomination = New Trinket(S)
            With TinyAbomination
                ._Name = "TinyAbomination"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 0.5
                .ProcValue = 0
                .DamageType = "TinyAbomination"
                .HasteSensible = True
            End With


            Dim DeathbringersWill = New Trinket(S)
            With DeathbringersWill
                ._Name = "DeathbringersWill"
                .ProcChance = 0.35
                .ProcLenght = 30
                .ProcValue = 600
                .InternalCD = 105
                .Effects.Add(New SpellBuff(S, "Deathbringers Will", Simulator.Sim.Stat.Haste, 600, 30))
                .DamageType = "DeathbringersWill"
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Dim DeathbringersWillHeroic = New Trinket(S)
            With DeathbringersWillHeroic
                ._Name = "DeathbringersWillHeroic"
                .ProcChance = 0.35
                .ProcLenght = 30
                .ProcValue = 700
                .InternalCD = 105
                .DamageType = "DeathbringersWill"
                .Effects.Add(New SpellBuff(S, "Deathbringers Will Heroic", Simulator.Sim.Stat.Haste, 700, 30))
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With


            Dim WhisperingFangedSkull = New Trinket(S)
            With WhisperingFangedSkull
                ._Name = "WhisperingFangedSkull"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 1100
                .InternalCD = 45
                .Effects.Add(New SpellBuff(sim, "Whispering Fanged Skull", Simulator.Sim.Stat.AP, 1100, 15))
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Dim WhisperingFangedSkullHeroic = New Trinket(S)
            With WhisperingFangedSkullHeroic
                ._Name = "WhisperingFangedSkullHeroic"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 1250
                .InternalCD = 45
                .DamageType = ""
                .Effects.Add(New SpellBuff(sim, "Whispering Fanged Skull Heroic", Simulator.Sim.Stat.AP, 1250, 15))
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Dim NeedleEncrustedScorpion = New Trinket(S)
            With NeedleEncrustedScorpion
                ._Name = "NeedleEncrustedScorpion"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 678
                .InternalCD = 45
                .DamageType = ""
                .Effects.Add(New SpellBuff(S, "Needle-Encrusted Scorpion", Simulator.Sim.Stat.Crit, 678, 10))
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With


            Dim DeathChoice = New Trinket(S)
            With DeathChoice
                ._Name = "DeathChoice"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 450
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "Death Choice", Simulator.Sim.Stat.Strength, 450, 15))
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Dim DeathChoiceHeroic = New Trinket(S)
            With DeathChoiceHeroic
                ._Name = "DeathChoiceHeroic"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 510
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "Death Choice Heroic", Simulator.Sim.Stat.Strength, 510, 15))
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Dim Greatness = New Trinket(S)
            With Greatness
                ._Name = "Greatness"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 300
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "Greatness", Simulator.Sim.Stat.Strength, 300, 15))

                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Dim MjolRune = New Trinket(S)
            With MjolRune
                ._Name = "MjolnirRunestone"
                .ProcChance = 0.15
                .ProcLenght = 10
                .ProcValue = 665
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "Mjolnir Runestone", Simulator.Sim.Stat.Crit, 665, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            Dim GrimToll = New Trinket(S)
            With GrimToll
                ._Name = "GrimToll"
                .ProcChance = 0.15
                .ProcLenght = 10
                .ProcValue = 612
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "GrimToll", Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            Dim BitterAnguish = New Trinket(S)
            With BitterAnguish
                ._Name = "BitterAnguish"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 410
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "Bitter Anguish", Simulator.Sim.Stat.Haste, 410, 10))
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With

            Dim Mirror = New Trinket(S)
            With Mirror
                ._Name = "Mirror"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 1000
                .InternalCD = 45
                .Effects.Add(New SpellBuff(sim, "Mirror of truth", Simulator.Sim.Stat.AP, 1000, 10))
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With

            Dim Pyrite = New Trinket(S)
            With Pyrite
                ._Name = "Pyrite"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 1234
                .InternalCD = 45
                .Effects.Add(New SpellBuff(sim, "Pyrite", Simulator.Sim.Stat.AP, 1234, 10))
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With

            Dim OldGod = New Trinket(S)
            With OldGod
                ._Name = "OldGod"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 1284
                .InternalCD = 45
                .Effects.Add(New SpellBuff(sim, "Old God", Simulator.Sim.Stat.AP, 1284, 10))
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With

            Dim Victory = New Trinket(S)
            With Victory
                ._Name = "Victory"
                .ProcChance = 0.2
                .ProcLenght = 10
                .ProcValue = 1008
                .InternalCD = 45
                .Effects.Add(New SpellBuff(sim, "Victory", Simulator.Sim.Stat.AP, 1008, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            Dim DarkMatter = New Trinket(S)
            With DarkMatter
                ._Name = "DarkMatter"
                .ProcChance = 0.15
                .ProcLenght = 10
                .ProcValue = 612
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "DarkMatter", Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            Dim Comet = New Trinket(S)
            With Comet
                .ProcChance = 0.15
                .ProcLenght = 10
                .ProcValue = 726
                .InternalCD = 45
                .Effects.Add(New SpellBuff(S, "Comet's Trail", Simulator.Sim.Stat.Haste, .ProcValue, 10))
                ._Name = "Comet"
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            Dim DCDeath = New Trinket(S)
            With DCDeath
                .ProcChance = 0.15
                .ProcLenght = 0
                .ProcValue = 2000
                .InternalCD = 45
                .DamageType = "shadow"
                ._Name = "DCDeath"
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Dim Necromantic = New Trinket(S)
            With Necromantic
                .ProcChance = 0.1
                .ProcLenght = 0
                .ProcValue = 1050
                .InternalCD = 15
                .DamageType = "shadow"
                ._Name = "Necromantic"
                .ProcOn = ProcsManager.ProcOnType.OnDoT
            End With

            Dim Bandit = New Trinket(S)
            With Bandit
                .ProcChance = 0.1
                .ProcLenght = 0
                .ProcValue = 1880
                .InternalCD = 45
                .DamageType = "arcane"
                ._Name = "Bandit"
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With



            Dim tk = New Trinket(S)
            With tk
                ._Name = "ZabroksLuckyTooth"
                .ProcChance = 0.5
                .ProcLenght = 15
                .InternalCD = 90
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Mastery, 1095, 15))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Tank-Commander Insignia"
                .ProcChance = 0.3
                .ProcLenght = 20
                .InternalCD = 90
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Haste, 1314, 20))
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Shrine-Cleansing Purifier"
                .ProcChance = 0.3
                .ProcLenght = 20
                .InternalCD = 90
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Haste, 1314, 20))
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Oremantles Favor"
                .ProcChance = 0.5
                .ProcLenght = 20
                .InternalCD = 120
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 970, 20))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk


                ._Name = "Right Eye of Rajh Heroic"
                .ProcChance = 0.19
                .ProcLenght = 14
                .InternalCD = 49
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk


                ._Name = "Souls Anguish"
                .ProcChance = 0.2
                .ProcLenght = 15
                .InternalCD = 50
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Right Eye of Rajh"
                .ProcChance = 0.21
                .ProcLenght = 16
                .InternalCD = 51
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Might of the Ocean Heroic"
                .ProcChance = 0.22
                .ProcLenght = 17
                .InternalCD = 52
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Mark of Khardros Heroic"
                .ProcChance = 0.23
                .ProcLenght = 18
                .InternalCD = 53
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)

            With tk
                ._Name = "Mark of Khardros"
                .ProcChance = 0.24
                .ProcLenght = 19
                .InternalCD = 54
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Magnetite Mirror"
                .ProcChance = 0.25
                .ProcLenght = 20
                .InternalCD = 55
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Magnetite Mirror Heroic"
                .ProcChance = 0.26
                .ProcLenght = 21
                .InternalCD = 56
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Kvaldir Battle Standard"
                .ProcChance = 0.27
                .ProcLenght = 22
                .InternalCD = 57
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Kvaldir Battle Standard"
                .ProcChance = 0.28
                .ProcLenght = 23
                .InternalCD = 58
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Heart of Solace Heroic"
                .ProcChance = 0.29
                .ProcLenght = 24
                .InternalCD = 59
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Heart of Solace"
                .ProcChance = 0.3
                .ProcLenght = 25
                .InternalCD = 60
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Harrisons Insignia of Panache"
                .ProcChance = 0.31
                .ProcLenght = 26
                .InternalCD = 61
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Figurine - King of Boars"
                .ProcChance = 0.32
                .ProcLenght = 27
                .InternalCD = 62
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "License to Slay"
                .ProcChance = 0.33
                .ProcLenght = 28
                .InternalCD = 63
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Impatience of Youth"
                .ProcChance = 0.34
                .ProcLenght = 29
                .InternalCD = 64
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Impatience of Youth"
                .ProcChance = 0.35
                .ProcLenght = 30
                .InternalCD = 65
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Heart of Rage"
                .ProcChance = 0.36
                .ProcLenght = 31
                .InternalCD = 66
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Heart of Rage Heroic"
                .ProcChance = 0.37
                .ProcLenght = 32
                .InternalCD = 67
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Fury of Angerforge"
                .ProcChance = 0.38
                .ProcLenght = 33
                .InternalCD = 68
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Darkmoon Card: Hurricane"
                .ProcChance = 0.39
                .ProcLenght = 34
                .InternalCD = 69
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Crushing Weight Heroic"
                .ProcChance = 0.4
                .ProcLenght = 35
                .InternalCD = 70
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With
            tk = New Trinket(S)
            With tk
                ._Name = "Crushing Weight"
                .ProcChance = 0.41
                .ProcLenght = 36
                .InternalCD = 71
                .Effects.Add(New SpellBuff(S, ._Name, Simulator.Sim.Stat.Crit, 612, 10))
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With





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