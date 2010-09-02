Friend Class PlagueStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
        MyBase.New(S)
        BaseDamage = 378
        Coeficient = 1
        Multiplicator = (1 + sim.Character.Talents.Talent("CorruptingStrikes").Value * 10 / 100)
        If sim.Character.Glyph.PlagueStrike Then Multiplicator *= 1.2
        logLevel = LogLevelEnum.Basic
        SpecialCritChance = sim.Character.Talents.Talent("ViciousStrikes").Value * 3 / 100
        SpecialCritChance += sim.MainStat.T72PTNK * 0.1

        _CritCoef = (1 + sim.Character.Talents.Talent("ViciousStrikes").Value * 15 / 100)
        _CritCoef = _CritCoef * (1 + 0.06 * sim.MainStat.CSD)

	End Sub
    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        UseGCD(T)
        If MyBase.ApplyDamage(T) = False Then
            sim.Runes.UseUnholy(T, False, True)
            Return False
        End If

        If OffHand = False Then

            sim.RunicPower.add(15 + sim.Character.Talents.Talent("Dirge").Value * 5)
            
            sim.Runes.UseUnholy(T, False)
            If sim.Targets.MainTarget.BloodPlague.isActive(T) Then
                sim.proc.CrimsonScourge.TryMe(T)
            End If

            sim.proc.Strife.TryMe(T)
            sim.Targets.MainTarget.BloodPlague.Apply(T)
            If sim.DRW.IsActive(T) Then
                sim.DRW.DRWPlagueStrike()
            End If
        End If
        Return True
    End Function
    

end Class