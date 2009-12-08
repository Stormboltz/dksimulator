Friend Class IcyTouch
	inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
		
		
		
		If DoMySpellHit = false Then
			sim.combatlog.write(T  & vbtab &  "IT fail")
			sim.proc.KillingMachine.Use
			MissCount = MissCount + 1
			Exit function
		End If
		
		RNG = MyRNG
		
		Dim dégat As Integer
		Dim ccT As Double
		ccT = CritChance
		If RNG <= ccT Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			sim.combatlog.write(T  & vbtab &  "IT crit for " & dégat )
		Else
			HitCount = HitCount + 1
			dégat =  AvrgNonCrit(T)
			sim.combatlog.write(T  & vbtab &  "IT hit for " & dégat)
		End If
		

		total = total + dégat
		
		
		
		
		Sim.RunicPower.add (10 + (TalentFrost.ChillOfTheGrave * 2.5))
		If sim.DRW.IsActive(T) Then
			sim.DRW.IcyTouch
		End If
		
		sim.runes.UseFrost(T,false)
		sim.proc.KillingMachine.Use
		sim.FrostFever.Apply(T)
		sim.TryOnSpellHit
		return true
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		
		Dim tmp As Double
		tmp = 236
		
		tmp = tmp + (0.1 * (1 + 0.04 * TalentUnholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1 + TalentFrost.ImprovedIcyTouch * 5 / 100)
		If sim.NumDesease > 0 Then 	tmp = tmp * (1 + TalentFrost.GlacierRot * 6.6666666 / 100)
		If (T/sim.MaxTime) >= 0.75 Then tmp = tmp *(1+ 0.06*talentfrost.MercilessCombat)
		If sim.sigils.FrozenConscience Then tmp = tmp +111
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp *(1+sim.RuneForge.RazorIceStack/100) 'TODO: only on main target
		if sim.runeforge.CinderglacierProc > 0 then
			tmp = tmp * 1.2
			sim.runeforge.CinderglacierProc = sim.runeforge.CinderglacierProc -1
		end if
		AvrgNonCrit = tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit + TalentFrost.Rime * 5 / 100
		If sim.proc.KillingMachine.IsActive Then return 1
		
	End Function
	overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
End Class