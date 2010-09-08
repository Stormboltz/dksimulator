Namespace Simulator.WowObjects.Strikes
    Public Class RuneStrike
        Inherits Strike

        Friend trigger As Boolean

        Sub New(ByVal S As sim)
            MyBase.New(S)
            sim = S
            ThreadMultiplicator = 1.5 * 1.17

            logLevel = LogLevelEnum.Basic

            BaseDamage = 0
            Coeficient = 2

            Multiplicator = 1
            Multiplicator *= (1 + sim.Character.T82PTNK * 0.1)


            If sim.Character.Glyph.RuneStrike Then
                SpecialCritChance = 0.1
            End If



        End Sub

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            BaseDamage = 20 * sim.Character.AP / 100
            trigger = False
            If MyBase.ApplyDamage(T) = False Then
                sim.RunicPower.Use(10)
                Return False
            End If

            If OffHand = False Then
                UseGCD(T)
                sim.RunicPower.Use(20)
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.onRPDump)
                If sim.DRW.IsActive(T) Then
                    sim.DRW.DRWRuneStrike()
                End If
            End If
            Return True
        End Function
    End Class



End Namespace