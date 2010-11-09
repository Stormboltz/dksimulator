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
            Coeficient = 1.5
            Multiplicator += sim.Character.T82PTNK * 0.1
            If sim.Character.Glyph("RuneStrike") Then
                AdditionalCritChance = 0.1
            End If
            CanBeDodge = False
            Resource = New Resource(S, Resource.ResourcesEnum.RunicPower, 30)
        End Sub
        Public Overrides Function IsAvailable() As Boolean
            If trigger Then
                Return MyBase.IsAvailable()
            Else
                Return False
            End If
        End Function

        Public Overrides Sub Use()
            MyBase.Use()
            trigger = False
        End Sub

        Public Overrides Sub UseAlf()
            MyBase.UseAlf()
            trigger = False
        End Sub

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If sim.level85 Then
                BaseDamage = 15 * sim.Character.AP / 100
            Else
                BaseDamage = 20 * sim.Character.AP / 100
            End If

            trigger = False
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                Return False
            End If

            If OffHand = False Then
                Use()
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.onRPDump)
                If sim.DRW.IsActive(T) Then
                    sim.DRW.DRWRuneStrike()
                End If
            End If
            Return True
        End Function
    End Class



End Namespace