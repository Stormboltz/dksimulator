'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/22/2010
' Heure: 3:46 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Spells
    Public Class FrostPresence

        Inherits Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
            Resource = New Resource(S, ResourcesEnum.AllRunicPower)
        End Sub
        Sub CancelAura()
            sim.FrostPresence = 0
        End Sub


        Overrides Sub Use()
            sim.BloodPresenceSwitch.CancelAura()
            sim.UnholyPresenceSwitch.CancelAura()
            sim.FrostPresence = 10 + (2.5 * sim.Character.Talents.Talent("IFrostPresence").Value)
            MyBase.Use()
            sim.CombatLog.write(sim.TimeStamp & vbTab & "Switch to Frost Presence")
            Me.HitCount = Me.HitCount + 1
            sim._UseGCD(1)


        End Sub

        Sub SetForFree()
            Sim.BloodPresence = 0
            Sim.UnholyPresence = 0
            Sim.FrostPresence = 10 + (2.5 * Sim.Character.Talents.Talent("IFrostPresence").Value)
        End Sub


    End Class
End Namespace