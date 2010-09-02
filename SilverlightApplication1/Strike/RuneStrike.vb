'
' Created by SharpDevelop.
' User: Fabien
' Date: 06/04/2009
' Time: 22:07
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class RuneStrike
	Inherits Strikes.Strike
	
	Friend trigger as Boolean

	Sub New(S As sim )
		MyBase.New(s)
		sim = S
		ThreadMultiplicator = 1.5 * 1.17

        logLevel = LogLevelEnum.Basic

        BaseDamage = 0
        Coeficient = 2

        Multiplicator = 1
        Multiplicator *= (1 + sim.MainStat.T82PTNK * 0.1)


        If sim.Character.Glyph.RuneStrike Then
            SpecialCritChance = 0.1
        End If



	End Sub
	
	overrides Function ApplyDamage(T As long) As boolean
        BaseDamage = 20 * sim.MainStat.AP / 100
        trigger = False
        If MyBase.ApplyDamage(T) = False Then
            sim.RunicPower.Use(10)
            Return False
        End If

        If OffHand = False Then
            UseGCD(T)
            sim.RunicPower.Use(20)
            sim.proc.tryProcs(Procs.ProcOnType.onRPDump)
            If sim.DRW.IsActive(T) Then
                sim.DRW.DRWRuneStrike()
            End If
        End If
        Return True
    End Function
End Class



