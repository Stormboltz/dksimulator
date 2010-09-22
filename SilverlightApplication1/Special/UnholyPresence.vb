Namespace Simulator.WowObjects.Spells


    Public Class UnholyPresence
        Inherits Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
            Resource = New Resource(S, ResourcesEnum.AllRunicPower)
        End Sub

        Sub CancelAura()
            If sim.UnholyPresence <> 0 Then
                sim.Character.RuneRegeneration.RemoveMulti(1.1 + sim.Character.Talents.Talent("ImprovedUnholyPresence").Value * 0.025)
            End If
            sim.UnholyPresence = 0
        End Sub
        

        Overrides Sub Use()
            sim.BloodPresenceSwitch.CancelAura()
            sim.FrostPresenceSwitch.CancelAura()
            sim.Character.RuneRegeneration.AddMulti(1.1 + sim.Character.Talents.Talent("ImprovedUnholyPresence").Value * 0.025)
            sim.UnholyPresence = 1
            sim.CombatLog.write(sim.TimeStamp & vbTab & "Switch to Unholy Presence")
            Me.HitCount = Me.HitCount + 1
            sim._UseGCD(sim.TimeStamp, 1)
        End Sub



    End Class
End Namespace