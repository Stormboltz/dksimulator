Namespace Simulator.WowObjects.Spells
    Friend Class UnholyBlight
        Inherits Spell
        Friend Glyphed As Boolean

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
            Glyphed = sim.Character.Glyph("UnholyBlight")
        End Sub


        Function Apply(ByVal T As Long, ByVal damage As Integer) As Double
            LastDamage = damage * 0.1
            If Glyphed Then LastDamage = LastDamage * 1.4
            HitCount = HitCount + 1
            total = total + LastDamage
            If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "UnholyBlight hit for " & LastDamage)
            Return True
        End Function
    End Class
End Namespace