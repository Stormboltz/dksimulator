Namespace Simulator.WowObjects.Spells
    Friend Class IcyTouch
        Inherits Spells.Spell
        Sub New(ByVal S As Sim)
            MyBase.New(S)
            If S.BloodPresence = 1 Then
                ThreadMultiplicator = 7
            End If
            BaseDamage = 473
            If Sim.Sigils.FrozenConscience Then BaseDamage += 111
            Coeficient = (0.1)

            If S.Character.Talents.GetNumOfThisSchool(Character.Talents.Schools.Frost) > 20 Then
                Multiplicator = Multiplicator * 1.2 'Frozen Heart
            End If
            logLevel = LogLevelEnum.Basic
            DamageSchool = DamageSchoolEnum.Frost
        End Sub

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Dim ret As Boolean = MyBase.ApplyDamage(T)
            UseGCD(T)
            Sim.proc.KillingMachine.Use()
            If ret = False Then
                Return False
            End If
            Sim.RunicPower.add(15 + (Sim.Character.Talents.Talent("ChillOfTheGrave").Value * 5))
            If Sim.DRW.IsActive(T) Then
                Sim.DRW.DRWIcyTouch()
            End If
            Sim.Runes.UseFrost(T, False)
            Return True
        End Function
        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
            If Sim.ExecuteRange Then tmp *= (1 + 0.06 * Sim.Character.Talents.Talent("MercilessCombat").Value)
            If Sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
            tmp *= sim.RuneForge.RazorIceMultiplier(T)
            If sim.Character.Talents.GetNumOfThisSchool(Character.Talents.Schools.Frost) > 20 Then
                tmp *= 1 + sim.Character.Mastery.Value * 2.5
            End If
            'TODO: only on main target
            Sim.Targets.MainTarget.FrostFever.Apply(T)
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
