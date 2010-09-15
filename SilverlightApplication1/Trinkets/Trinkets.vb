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


        Friend GrimToll As Trinket
        Friend BitterAnguish As Trinket
        Friend Mirror As Trinket
        Friend DCDeath As Trinket
        Friend Victory As Trinket
        Friend Necromantic As Trinket
        Friend Bandit As Trinket
        Friend Pyrite As Trinket
        Friend DarkMatter As Trinket
        Friend OldGod As Trinket
        Friend Comet As Trinket
        Friend DeathChoice As Trinket
        Friend DeathChoiceHeroic As Trinket
        Friend Greatness As Trinket
        Friend MjolRune As Trinket
        Friend DeathbringersWill As Trinket
        Friend DeathbringersWillHeroic As Trinket
        Friend HerkumlWarToken As Trinket
        Friend MarkofSupremacy As Trinket
        Friend VengeanceoftheForsaken As Trinket
        Friend VengeanceoftheForsakenHeroic As Trinket
        Friend WhisperingFangedSkull As Trinket
        Friend WhisperingFangedSkullHeroic As Trinket
        Friend NeedleEncrustedScorpion As Trinket
        Friend TinyAbomination As Trinket






        Protected sim As Sim
        Sub New(ByVal S As Sim)
            sim = S
            XmlCharacter = S.XmlCharacter



            HerkumlWarToken = New Trinket(S)
            With HerkumlWarToken
                ._Name = "HerkumlWarToken"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 1
                .InternalCD = 0
                .MaxStack = 20
                .Effect = New SpellBuff(S, "Herkuml War Token", Simulator.Sim.Stat.AP, 17, 20, 10)
                .ProcValueStack = 17
                .ProcLenght = 10
            End With

            MarkofSupremacy = New Trinket(S)
            With MarkofSupremacy
                ._Name = "MarkofSupremacy"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 0.5
                .InternalCD = 120
                .ProcLenght = 20
                .Effect = New SpellBuff(sim, "Mark of Supremacy", Simulator.Sim.Stat.AP, 1024, 20)
                .ProcValue = "1024"
            End With

            VengeanceoftheForsaken = New Trinket(S)
            With VengeanceoftheForsaken
                ._Name = "VengeanceoftheForsaken"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 0.5
                .InternalCD = 120
                .ProcLenght = "20"
                .Effect = New SpellBuff(sim, "Vengeance of the Forsaken", Simulator.Sim.Stat.AP, 860, 20)
                .ProcValue = "860"
            End With

            VengeanceoftheForsakenHeroic = New Trinket(S)
            With VengeanceoftheForsakenHeroic
                ._Name = "VengeanceoftheForsakenHeroic"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 0.5
                .InternalCD = 120
                .ProcLenght = 20
                .Effect = New SpellBuff(sim, "Vengeance of the Forsaken Heroic", Simulator.Sim.Stat.AP, 1000, 20)
                .ProcValue = "1000"
            End With

            TinyAbomination = New Trinket(S)
            With TinyAbomination
                ._Name = "TinyAbomination"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                .ProcChance = 0.5
                .ProcValue = 0
                .DamageType = "TinyAbomination"
                .HasteSensible = True
            End With


            DeathbringersWill = New Trinket(S)
            With DeathbringersWill
                ._Name = "DeathbringersWill"
                .ProcChance = 0.35
                .ProcLenght = 30
                .ProcValue = 600
                .InternalCD = 105
                .Effect = New SpellBuff(S, "Deathbringers Will", Simulator.Sim.Stat.Haste, 600, 30)
                .DamageType = "DeathbringersWill"
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            DeathbringersWillHeroic = New Trinket(S)
            With DeathbringersWillHeroic
                ._Name = "DeathbringersWillHeroic"
                .ProcChance = 0.35
                .ProcLenght = 30
                .ProcValue = 700
                .InternalCD = 105
                .DamageType = "DeathbringersWill"
                .Effect = New SpellBuff(S, "Deathbringers Will Heroic", Simulator.Sim.Stat.Haste, 700, 30)
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With


            WhisperingFangedSkull = New Trinket(S)
            With WhisperingFangedSkull
                ._Name = "WhisperingFangedSkull"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 1100
                .InternalCD = 45
                .Effect = New SpellBuff(sim, "Whispering Fanged Skull", Simulator.Sim.Stat.AP, 1100, 15)
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            WhisperingFangedSkullHeroic = New Trinket(S)
            With WhisperingFangedSkullHeroic
                ._Name = "WhisperingFangedSkullHeroic"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 1250
                .InternalCD = 45
                .DamageType = ""
                .Effect = New SpellBuff(sim, "Whispering Fanged Skull Heroic", Simulator.Sim.Stat.AP, 1250, 15)
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            NeedleEncrustedScorpion = New Trinket(S)
            With NeedleEncrustedScorpion
                ._Name = "NeedleEncrustedScorpion"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 678
                .InternalCD = 45
                .DamageType = ""
                .Effect = New SpellBuff(S, "Needle-Encrusted Scorpion", Simulator.Sim.Stat.Crit, 678, 10)
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With


            DeathChoice = New Trinket(S)
            With DeathChoice
                ._Name = "DeathChoice"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 450
                .InternalCD = 45
                .Effect = New SpellBuff(S, "Death Choice", Simulator.Sim.Stat.Strength, 450, 15)
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            DeathChoiceHeroic = New Trinket(S)
            With DeathChoiceHeroic
                ._Name = "DeathChoiceHeroic"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 510
                .InternalCD = 45
                .Effect = New SpellBuff(S, "Death Choice Heroic", Simulator.Sim.Stat.Strength, 510, 15)
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Greatness = New Trinket(S)
            With Greatness
                ._Name = "Greatness"
                .ProcChance = 0.35
                .ProcLenght = 15
                .ProcValue = 300
                .InternalCD = 45
                .Effect = New SpellBuff(S, "Greatness", Simulator.Sim.Stat.Strength, 300, 15)

                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            MjolRune = New Trinket(S)
            With MjolRune
                ._Name = "MjolRune"
                .ProcChance = 0.15
                .ProcLenght = 10
                .ProcValue = 665
                .InternalCD = 45
                .Effect = New SpellBuff(S, "Mjol Rune", Simulator.Sim.Stat.Crit, 665, 10)
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            GrimToll = New Trinket(S)
            With GrimToll
                ._Name = "GrimToll"
                .ProcChance = 0.15
                .ProcLenght = 10
                .ProcValue = 612
                .InternalCD = 45
                .Effect = New SpellBuff(S, "GrimToll", Simulator.Sim.Stat.Crit, 612, 10)
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            BitterAnguish = New Trinket(S)
            With BitterAnguish
                ._Name = "BitterAnguish"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 410
                .InternalCD = 45
                .Effect = New SpellBuff(S, "Bitter Anguish", Simulator.Sim.Stat.Haste, 410, 10)
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With

            Mirror = New Trinket(S)
            With Mirror
                ._Name = "Mirror"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 1000
                .InternalCD = 45
                .Effect = New SpellBuff(sim, "Mirror of truth", Simulator.Sim.Stat.AP, 1000, 10)
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With

            Pyrite = New Trinket(S)
            With Pyrite
                ._Name = "Pyrite"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 1234
                .InternalCD = 45
                .Effect = New SpellBuff(sim, "Pyrite", Simulator.Sim.Stat.AP, 1234, 10)
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With

            OldGod = New Trinket(S)
            With OldGod
                ._Name = "OldGod"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 1284
                .InternalCD = 45
                .Effect = New SpellBuff(sim, "Old God", Simulator.Sim.Stat.AP, 1284, 10)
                .ProcOn = ProcsManager.ProcOnType.OnCrit
            End With

            Victory = New Trinket(S)
            With Victory
                ._Name = "Victory"
                .ProcChance = 0.2
                .ProcLenght = 10
                .ProcValue = 1008
                .InternalCD = 45
                .Effect = New SpellBuff(sim, "Victory", Simulator.Sim.Stat.AP, 1008, 10)
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            DarkMatter = New Trinket(S)
            With DarkMatter
                ._Name = "DarkMatter"
                .ProcChance = 0.15
                .ProcLenght = 10
                .ProcValue = 612
                .InternalCD = 45
                .Effect = New SpellBuff(S, "DarkMatter", Simulator.Sim.Stat.Crit, 612, 10)
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            Comet = New Trinket(S)
            With Comet
                .ProcChance = 0.15
                .ProcLenght = 10
                .ProcValue = 726
                .InternalCD = 45
                .Effect = New SpellBuff(S, "Comet's Trail", Simulator.Sim.Stat.Haste, .ProcValue, 10)
                ._Name = "Comet"
                .ProcOn = ProcsManager.ProcOnType.OnHit
            End With

            DCDeath = New Trinket(S)
            With DCDeath
                .ProcChance = 0.15
                .ProcLenght = 0
                .ProcValue = 2000
                .InternalCD = 45
                .DamageType = "shadow"
                ._Name = "DCDeath"
                .ProcOn = ProcsManager.ProcOnType.OnDamage
            End With

            Necromantic = New Trinket(S)
            With Necromantic
                .ProcChance = 0.1
                .ProcLenght = 0
                .ProcValue = 1050
                .InternalCD = 15
                .DamageType = "shadow"
                ._Name = "Necromantic"
                .ProcOn = ProcsManager.ProcOnType.OnDoT
            End With

            Bandit = New Trinket(S)
            With Bandit
                .ProcChance = 0.1
                .ProcLenght = 0
                .ProcValue = 1880
                .InternalCD = 45
                .DamageType = "arcane"
                ._Name = "Bandit"
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

        Sub SoftReset()
            Dim tk As Trinket

            For Each tk In sim.TrinketsCollection
                tk.CD = 0
            Next
        End Sub


    End Class
End Namespace