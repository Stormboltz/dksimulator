Namespace Simulator.WowObjects.Spells
    Friend Class IcyTouch
        Inherits Spells.Spell
        Sub New(ByVal S As Sim)
            MyBase.New(S)
        
                BaseDamage = 505 + 547 / 2

            Coeficient = (0.2) 'Free Imp Icy Touch 

            logLevel = LogLevelEnum.Basic
            DamageSchool = DamageSchoolEnum.Frost
            Dim rp As Integer = 10 + (sim.Character.Talents.Talent("ChillOfTheGrave").Value * 5)
            Resource = New Resource(S, Resource.ResourcesEnum.FrostRune, False, rp)
        End Sub

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Dim ret As Boolean = MyBase.ApplyDamage(T)
            UseGCD(T)
            sim.proc.KillingMachine.Fade()

            If ret = False Then
                Return False
            End If

            If Sim.DRW.IsActive(T) Then
                Sim.DRW.DRWIcyTouch()
            End If
            Use()
            Return True
        End Function
        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
            If Sim.ExecuteRange Then tmp *= (1 + 0.06 * Sim.Character.Talents.Talent("MercilessCombat").Value)
            If Sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
            tmp *= sim.RuneForge.RazorIceMultiplier(T)
            sim.Targets.MainTarget.FrostFever.Apply(T)
            'Moved this here as an IcyTouch with 1 CG charge left will reapply a CG buffed FF
            Return tmp
        End Function

        Overrides Function CritChance() As Double
            If Sim.proc.KillingMachine.IsActive Then
                Return 1
            Else
                Return MyBase.CritChance
            End If
        End Function
    End Class
End Namespace
