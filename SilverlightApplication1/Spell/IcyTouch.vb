Friend Class IcyTouch
	inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New(s)
        If S.BloodPresence = 1 Then
            ThreadMultiplicator = 7
        End If
        BaseDamage = 473
        If sim.Sigils.FrozenConscience Then BaseDamage += 111
        Coeficient = (0.1 * (1 + 0.2 * sim.Character.Talents.Talent("Impurity").Value)) * (1 + sim.Character.Talents.Talent("ImprovedIcyTouch").Value * 10 / 100)
        Multiplicator = (1 + sim.Character.Talents.Talent("ImprovedIcyTouch").Value * 10 / 100)



	End Sub
	
	overrides Function ApplyDamage(T As long) As boolean
        Dim ret As Boolean = MyBase.ApplyDamage(T)
        UseGCD(T)
        sim.proc.KillingMachine.Use()
        If ret = False Then
            Return False
        End If
        sim.RunicPower.add(10 + (sim.Character.Talents.Talent("ChillOfTheGrave").Value * 5))
        If sim.DRW.IsActive(T) Then
            sim.DRW.DRWIcyTouch()
        End If
        sim.runes.UseFrost(T, False)
        Return True
    End Function
    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        If target Is Nothing Then target = sim.Targets.MainTarget
        Dim lastMult As Double = Multiplicator
        If sim.RuneForge.CheckCinderglacier(True) > 0 Then
            Multiplicator *= 1.2
        End If
        If sim.ExecuteRange Then Multiplicator = Multiplicator * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
        Multiplicator *= sim.RuneForge.RazorIceMultiplier(T)
        Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
        Multiplicator = lastMult

        'TODO: only on main target
        sim.Targets.MainTarget.FrostFever.Apply(T)
        'Moved this here as an IcyTouch with 1 CG charge left will reapply a CG buffed FF
        Return tmp
    End Function
   
    Overrides Function CritChance() As Double
        If sim.proc.KillingMachine.IsActive Then
            Return 1
        Else
            Return MyBase.CritChance
        End If
    End Function
End Class