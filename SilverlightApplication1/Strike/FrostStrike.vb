'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:24
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Strikes
    Friend Class FrostStrike
        Inherits Strike

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            If S.level85 Then
                BaseDamage = 278
            Else
                BaseDamage = 250

            End If

            Coeficient = 1.21

            Dim c As Integer = 40
            If sim.Character.Glyph("froststrike") Then c -= 8
            Resource = New Resource(S, Resource.ResourcesEnum.RunicPower, c)

            AdditionalCritChance += 8 * sim.Character.T82PDPS / 100
            AdditionalCritChance += 5 * sim.Character.T112PDPS / 100
            DamageSchool = DamageSchoolEnum.Frost
            logLevel = LogLevelEnum.Basic
        End Sub

       
        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Dim ret As Boolean = MyBase.ApplyDamage(T)

            If Not OffHand Then UseGCD()
            If ret = False Then
                'UseAlf()
                Return False
            End If
            If OffHand = False Then
                sim.proc.KillingMachine.Fade()
                Use()
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.onRPDump)
            End If
            Return True
        End Function
        Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
            If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
            tmp *= sim.RuneForge.RazorIceMultiplier(T)
            If sim.RuneForge.CheckCinderglacier(OffHand) > 0 Then tmp *= 1.2
            tmp *= 1 + sim.Character.Mastery.Value * 2
            Return tmp
        End Function
        Public Overrides Function CritChance() As Double
            If sim.proc.KillingMachine.IsActive = True Then
                Return 1
            Else
                Return MyBase.CritChance
            End If
        End Function

    End Class
End Namespace
