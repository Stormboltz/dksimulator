'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 11:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class DeathStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
        MyBase.New(S)
        BaseDamage = 222.75
        If sim.Sigils.Awareness Then BaseDamage = BaseDamage + 445.5
        Coeficient = 1.5
        Multiplicator = 1 + sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 15 / 100
        SpecialCritChance = sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 3 / 100 + sim.MainStat.T72PDPS * 5 / 100
        logLevel = LogLevelEnum.Basic




    End Sub

	'A deadly attack that deals 75% weapon damage plus 222.75
	'and heals the Death Knight for a percent of damage done
	'for each of <his/her> diseases on the target.

    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean


        If MyBase.ApplyDamage(T) = False Then Return False
        If OffHand = False Then
            UseGCD(T)
            sim.RunicPower.add(25 + 5 * sim.Character.Talents.Talent("Dirge").Value)
            sim.RunicPower.add(5 * sim.MainStat.T74PDPS)
            If sim.proc.DRM.TryMe(T) Then
                sim.Runes.UseFU(T, True)
            Else
                sim.Runes.UseFU(T, False)
            End If
            sim.proc.tryProcs(Procs.ProcOnType.OnFU)
            If sim.DRW.IsActive(T) Then
                sim.DRW.DRWDeathStrike()
            End If
        End If
        Return True
    End Function
	public Overrides Function AvrgNonCrit(T as long,target As Targets.Target) As Double
		Dim tmp As Double
        tmp = MyBase.AvrgNonCrit(T, target)
        If sim.Character.Glyph.DeathStrike Then
            tmp = tmp * (1 + Math.Max(sim.RunicPower.GetValue, 25) / 100)
        End If
        Return tmp
    End Function
End Class
