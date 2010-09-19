'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/22/2010
' Heure: 3:40 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Spells
    Public Class BloodPresence
        Inherits Spells.Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
            Resource = New Resource(S, ResourcesEnum.AllRunicPower, 0, False)
        End Sub

        Sub CancelAura()
            sim.BloodPresence = 0
        End Sub

        Overrides Sub Use()
            MyBase.Use()
            sim.BloodPresence = 1
            sim.FrostPresenceSwitch.CancelAura()
            sim.UnholyPresenceSwitch.cancelAura()
            sim.CombatLog.write(sim.TimeStamp & vbTab & "Switch to Blood Presence")
            Me.HitCount = Me.HitCount + 1
            sim._UseGCD(sim.TimeStamp, 1)
        End Sub

    End Class
End Namespace