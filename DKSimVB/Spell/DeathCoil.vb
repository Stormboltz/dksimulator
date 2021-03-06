Friend class DeathCoil
	Inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New(s)
	End Sub

	

	Function isAvailable(T As long) As Boolean
		'If sim.DRW.cd <= T And sim.Character.talentblood.DRW = 1 And Sim.RunicPower.Value < 100 Then Return False
		'If sim.Gargoyle.cd <= T And sim.Character.talentunholy.SummonGargoyle = 1 And Sim.RunicPower.Value < 100 Then Return False
		'If glyph.DeathStrike And RunicPower.Value <= 65  Then Return False 'This is not really important
		return Sim.RunicPower.CheckRS(40)
	End Function
	
    overrides Function ApplyDamage(T As long,SDoom as Boolean) As boolean
		Dim RNG As Double
		
		If SDoom Then
			
		Else
			if sim.Character.talentblood.DRW = 1 then
				If sim.DRW.cd < T And sim.RunicPower.Check(60) Then
					if sim.DRW.Summon(T) = True Then return true
				End If
			End If
			If sim.PetFriendly And sim.Character.talentunholy.SummonGargoyle = 1 Then
				If sim.Gargoyle.cd < T and sim.RunicPower.Check(60) Then
					If sim.Gargoyle.Summon(T) = True Then return true
				end if
			End If
			UseGCD(T)
			Sim.RunicPower.Use(40)
		End If
		
		If DoMySpellHit = false Then
			sim.combatlog.write(T  & vbtab &  "DC fail")
			MissCount = MissCount + 1
			Exit function
		End If
		
		RNG = RngCrit
		dim d�gat as Integer
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			d�gat= AvrgCrit(T)
			totalcrit += d�gat


			If SDoom Then
				If sim.CombatLog.LogDetails Then
					sim.combatlog.write(T  & vbtab &  "DC SDoom crit for " & d�gat )
				End If
			Else
				sim.combatlog.write(T  & vbtab &  "DC crit for " & d�gat )
			End If
		Else
			d�gat= AvrgNonCrit(T)
			totalhit += d�gat
			HitCount = HitCount + 1
			If SDoom Then
				If sim.CombatLog.LogDetails Then
					sim.combatlog.write(T  & vbtab &  "DC SDoom hit for " & d�gat )
				End If
			Else
				sim.combatlog.write(T  & vbtab &  "DC hit for " & d�gat & vbtab )
			End If
			
		End If
		

		total = total + d�gat
		sim.TryOnSpellHit
		If sim.Character.talentunholy.UnholyBlight = 1 Then
			sim.UnholyBlight.Apply(T,d�gat)
		End If
		If sim.DRW.IsActive(T) Then
			sim.DRW.DRWDeathCoil
		End If
		return true
		
	End Function
	overrides Function AvrgNonCrit(T As Long, target As Targets.Target ) As Double
		Dim tmp As Double
		tmp = 443
		If sim.sigils.VengefulHeart Then tmp= tmp + 380
		if sim.Sigils.WildBuck then tmp = tmp + 80
		tmp = tmp + (0.15 * (1 + 0.04 * sim.Character.talentunholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1 + sim.Character.talentunholy.Morbidity * 5 / 100)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
		If sim.character.glyph.DarkDeath Then
			tmp = tmp * (1.15)
		End If
		if sim.RuneForge.CheckCinderglacier(True) > 0 then tmp *= 1.2
		return tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit + 8/100 * sim.MainStat.T82PDPS
	End Function
	overrides Function AvrgCrit(T As long,target As Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
end class