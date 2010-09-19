'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Spells
    Friend Class PillarOfFrost
        Inherits spells.Spell
        Friend previousFade As Long
        Friend Talented As Boolean
        Friend Proc As Procs.Proc
        Friend Buff As Procs.SpellBuff

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Basic
            If Sim.Character.Talents.Talent("PillarOfFrost").Value <> 0 Then Talented = True
            Buff = (New Procs.SpellBuff(S, "Pillar Of Frost", Simulator.Sim.Stat.Strength, 1.2, 20))
            Resource = New Resource(S, ResourcesEnum.BloodTap, 15, True)
        End Sub



        Overrides Function IsAvailable() As Boolean
            If Not Talented Then Return False
            If CD >= sim.TimeStamp Then Return False
            Return MyBase.IsAvailable
        End Function
        Overrides Sub Use()
            MyBase.Use()
            CD = sim.TimeStamp + 60 * 100
            Buff.Apply()
            sim._UseGCD(sim.TimeStamp, 1)
            sim.CombatLog.write(sim.TimeStamp & vbTab & "Pillar of Frost")
            Me.HitCount = Me.HitCount + 1
        End Sub
    End Class
End Namespace
