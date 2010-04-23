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
			Exit function
		End If
		Sim.RunicPower.add (10 + (sim.Character.talentfrost.ChillOfTheGrave * 2.5))
		RNG = RngCrit
		Dim dégat As Integer
		Dim ccT As Double
		ccT = CritChance
		If RNG <= ccT Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			totalcrit += dégat
			sim.combatlog.write(T  & vbtab &  "IT crit for " & dégat )
		Else
			HitCount = HitCount + 1
			dégat =  AvrgNonCrit(T)
			totalhit += dégat
			sim.combatlog.write(T  & vbtab &  "IT hit for " & dégat)
		End If
		total = total + dégat
		
		If sim.DRW.IsActive(T) Then
			sim.DRW.DRWIcyTouch
		End If
		sim.runes.UseFrost(T,false)
		sim.proc.KillingMachine.Use
		sim.TryOnSpellHit
		return true
	End Function
	Function AvrgNonCrit(T As Long,Optional target As Targets.Target = Nothing) As Double
		if target is nothing then target = sim.MainTarget
		
		Dim tmp As Double
		tmp = 236
		
		tmp = tmp + (0.1 * (1 + 0.04 * sim.Character.talentunholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1 + sim.Character.talentfrost.ImprovedIcyTouch * 5 / 100)
		if sim.NumDesease > 0 or (target.Debuff.BloodPlague+target.Debuff.FrostFever>0) Then 	tmp = tmp * (1 + sim.Character.talentfrost.GlacierRot * 6.6666666 / 100)
		If sim.ExecuteRange Then tmp = tmp *(1+ 0.06*sim.Character.talentfrost.MercilessCombat)
		If sim.sigils.FrozenConscience Then tmp = tmp +111
		tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
		
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp *= sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
		
		sim.FrostFever.Apply(T) 
		'Moved this here as an IcyTouch with 1 CG charge left will reapply a CG buffed FF
		'I'm pretty sure GlacierRot will not apply to the first icy touch if there are no other diseases up
		if sim.RuneForge.CheckCinderglacier(True) > 0 then tmp *= 1.2
		AvrgNonCrit = tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit + sim.Character.talentfrost.Rime * 5 / 100
		If sim.proc.KillingMachine.IsActive Then return 1
		
	End Function
	overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
End Class