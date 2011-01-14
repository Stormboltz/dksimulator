'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 11:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Strikes
    Friend Class DeathStrike
        Inherits Strike
        Dim Glyphed As Boolean
        Sub New(ByVal S As Sim)
            MyBase.New(S)
                BaseDamage = 330

            Coeficient = 1.5
            If sim.NextPatch Then
                Coeficient += sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 30 / 100
            Else
                Coeficient += sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 15 / 100
            End If

            Coeficient += sim.Character.T112PTNK * 5 / 100
            AdditionalCritChance = sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 3 / 100 + sim.Character.T72PDPS * 5 / 100
            logLevel = LogLevelEnum.Basic
            Glyphed = sim.Character.Glyph("DeathStrike")
            Dim rp As Integer = 20 + 5 * sim.Character.T74PDPS
            If sim.Character.Talents("DRM") = 1 Then
                Resource = New Resource(S, Resource.ResourcesEnum.FrostUnholy, True, rp)
            Else
                Resource = New Resource(S, Resource.ResourcesEnum.FrostUnholy, False, rp)
            End If
        End Sub

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                Return False
            End If
            If OffHand = False Then
                Use()
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnFU)
                If sim.DRW.IsActive(T) Then sim.DRW.DRWDeathStrike()
            End If
            Return True
        End Function
        Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = MyBase.AvrgNonCrit(T, target)
            If Glyphed Then
                tmp = tmp * (1 + (Math.Max(sim.RunicPower.GetValue, 100) * 0.004))
            End If
            Return tmp
        End Function
    End Class
End Namespace