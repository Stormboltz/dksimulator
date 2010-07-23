Friend Class IcyTouch
	inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New(s)
		If s.FrostPresence = 1 Then
			ThreadMultiplicator = 7
		End If
	End Sub
	
	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		UseGCD(T)
		If DoMySpellHit = false Then
			sim.combatlog.write(T  & vbtab &  "IT fail")
			sim.proc.KillingMachine.Use
			MissCount = MissCount + 1
            Return False
		End If
        sim.RunicPower.add(10 + (sim.Character.Talents.Talent("ChillOfTheGrave").Value * 2.5))
        RNG = RngCrit
        Dim dégat As Integer
        Dim ccT As Double
        ccT = CritChance
        If RNG <= ccT Then
            CritCount = CritCount + 1
            dégat = AvrgCrit(T)
            totalcrit += dégat
            sim.combatlog.write(T & vbtab & "IT crit for " & dégat)
        Else
            HitCount = HitCount + 1
            dégat = AvrgNonCrit(T)
            totalhit += dégat
            sim.combatlog.write(T & vbtab & "IT hit for " & dégat)
        End If
        total = total + dégat

        If sim.DRW.IsActive(T) Then
            sim.DRW.DRWIcyTouch()
        End If
        sim.runes.UseFrost(T, False)
        sim.proc.KillingMachine.Use()
        sim.proc.TryOnSpellHit()
        Return True
    End Function
    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        If target Is Nothing Then target = sim.Targets.MainTarget

        Dim tmp As Double
        tmp = 473

        tmp = tmp + (0.1 * (1 + 0.04 * sim.Character.Talents.Talent("Impurity").Value) * sim.MainStat.AP)
        tmp = tmp * (1 + sim.Character.Talents.Talent("ImprovedIcyTouch").Value * 5 / 100)

        If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
        If sim.sigils.FrozenConscience Then tmp = tmp + 111


        tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
        tmp *= sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target

        sim.Targets.MainTarget.FrostFever.Apply(T)
        'Moved this here as an IcyTouch with 1 CG charge left will reapply a CG buffed FF
        'I'm pretty sure GlacierRot will not apply to the first icy touch if there are no other diseases up
        If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
        AvrgNonCrit = tmp
    End Function
    Overrides Function CritCoef() As Double
        CritCoef = 1
        CritCoef = CritCoef * (1 + 0.06 * sim.mainstat.CSD)
    End Function
    Overrides Function CritChance() As Double
        CritChance = sim.MainStat.SpellCrit + sim.Character.Talents.Talent("Rime").Value * 5 / 100
        If sim.proc.KillingMachine.IsActive Then Return 1

    End Function
	overrides Function AvrgCrit(T As long,target As Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
End Class