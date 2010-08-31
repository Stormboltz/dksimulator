Friend Class UnholyBlight
	inherits Spells.Spell
	
	Sub New(S As sim)
        MyBase.New(S)
        logLevel = LogLevelEnum.Detailled
	End Sub
	
	
	Function Apply(T As Long,damage As Integer) As Double
        LastDamage = damage * 0.1
        If sim.Character.Glyph.UnholyBlight Then LastDamage = LastDamage * 1.4
        HitCount = HitCount + 1
        total = total + LastDamage
        If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "UnholyBlight hit for " & LastDamage)
        Return True
	End Function
End Class
