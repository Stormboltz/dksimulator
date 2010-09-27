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
            BaseDamage = 222.75
            If sim.Sigils.Awareness Then BaseDamage = BaseDamage + 445.5
            Coeficient = 1.5
            Multiplicator = 1 + sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 15 / 100
            SpecialCritChance = sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 3 / 100 + sim.Character.T72PDPS * 5 / 100
            logLevel = LogLevelEnum.Basic
            Glyphed = sim.Character.Glyph("DeathStrike")
            Dim rp As Integer = 20 + 5 * sim.Character.T74PDPS
            If sim.Character.Talents("DRM") = 1 Then
                Resource = New Resource(S, ResourcesEnum.FrostUnholy, True, rp)
            Else
                Resource = New Resource(S, ResourcesEnum.FrostUnholy, False, rp)
            End If


        End Sub

        'A deadly attack that deals 75% weapon damage plus 222.75
        'and heals the Death Knight for a percent of damage done
        'for each of <his/her> diseases on the target.

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then

                UseAlf()
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