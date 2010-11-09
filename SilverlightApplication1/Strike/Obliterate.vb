'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:29
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Strikes
    Friend Class Obliterate
        Inherits Strike


        Sub New(ByVal S As Sim)
            MyBase.New(S)
            'TOTAL = (MHdamage+Base Damage)*coef* Multiplicator
            If S.level85 Then
                BaseDamage = 650
            Else
                BaseDamage = 584
            End If
            Coeficient = 1.6
            Coeficient += sim.Character.Talents.Talent("Annihilation").Value * 15 / 100
            If sim.Character.Glyph("Obliterate") Then Coeficient += 0.25
            If sim.Character.T102PDPS <> 0 Then Coeficient += 0.1
            logLevel = LogLevelEnum.Basic
            If Sim.Character.T72PDPS Then
                AdditionalCritChance = 0.05
            Else
                AdditionalCritChance = 0
            End If

            If Sim.Character.T84PDPS = 1 Then
                DiseaseBonus = (0.125 * 1.2)
            Else
                DiseaseBonus = (0.125)
            End If
            Dim rp As Integer = 20 + 5 * sim.Character.Talents.Talent("ChillOfTheGrave").Value + 5 * sim.Character.T74PDPS
            If sim.Character.Talents("DRM") = 1 Then
                Resource = New Resource(sim, Resource.ResourcesEnum.FrostUnholy, True, rp)
            Else
                Resource = New Resource(sim, Resource.ResourcesEnum.FrostUnholy, False, rp)
            End If

        End Sub

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                'UseAlf()
                Return False
            End If

            If OffHand = False Then
                sim.proc.KillingMachine.Fade()
                Use()
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnFU)
                Sim.proc.Rime.TryMe(T)
                If Sim.DRW.IsActive(T) Then
                    Sim.DRW.DRWObliterate()
                End If
            End If
            Return True
        End Function

        Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = MyBase.AvrgNonCrit(T, target)
            If Sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * Sim.Character.Talents.Talent("MercilessCombat").Value)
            Return tmp
        End Function

        Public Overrides Function CritChance() As Double
            If sim.proc.KillingMachine.IsActive Then Return 1
            Return MyBase.CritChance()
        End Function
    End Class
End Namespace