
'
Namespace Simulator.WowObjects.Spells
    Public Class BloodPresence
        Inherits Spells.Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
            Resource = New Resource(S, Resource.ResourcesEnum.AllRunicPower)
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
            sim._UseGCD(1)
        End Sub

    End Class
End Namespace