Friend class DeathCoil
	Inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New(s)
	End Sub

	

	Function isAvailable(T As long) As Boolean
		'If sim.DRW.cd <= T And sim.TalentBlood.DRW = 1 And Sim.RunicPower.Value < 100 Then Return False
		'If sim.Gargoyle.cd <= T And sim.TalentUnholy.SummonGargoyle = 1 And Sim.RunicPower.Value < 100 Then Return False
		'If glyph.DeathStrike And RunicPower.Value <= 65  Then Return False 'This is not really important
		If sim.SaveRPForRS Then
			If Sim.RunicPower.Value >= 60 Then isAvailable = True
		Else
			If Sim.RunicPower.Value >= 40 Then isAvailable = True
		End If
		
	End Function
	
    overrides Function ApplyDamage(T As long,SDoom as Boolean) As boolean
		Dim RNG As Double
		
		If SDoom Then
			
		Else
			if sim.TalentBlood.DRW = 1 then
				If sim.DRW.cd < T And sim.RunicPower.Value  >= 60 Then
					if sim.DRW.Summon(T) = True Then return true
				End If
			End If
			If sim.PetFriendly And sim.talentunholy.SummonGargoyle = 1 Then
				If sim.Gargoyle.cd < T and sim.RunicPower.Value >= 60 Then
					If sim.Gargoyle.Summon(T) = True Then return true
				end if
			End If
			UseGCD(T)
			Sim.RunicPower.Value = Sim.RunicPower.Value - 40
		End If
		
		If DoMySpellHit = false Then
			sim.combatlog.write(T  & vbtab &  "DC fail")
			MissCount = MissCount + 1
			Exit function
		End If
		
		RNG = RngCrit
		dim dégat as Integer
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			dégat= AvrgCrit(T)
			totalcrit += dégat


			If SDoom Then
				If sim.CombatLog.LogDetails Then
					sim.combatlog.write(T  & vbtab &  "DC SDoom crit for " & dégat & vbtab & "RP left = " & Sim.RunicPower.Value)
				End If
			Else
				sim.combatlog.write(T  & vbtab &  "DC crit for " & dégat & vbtab & "RP left = " & Sim.RunicPower.Value)
			End If
		Else
			dégat= AvrgNonCrit(T)
			totalhit += dégat
			HitCount = HitCount + 1
			If SDoom Then
				If sim.CombatLog.LogDetails Then
					sim.combatlog.write(T  & vbtab &  "DC SDoom hit for " & dégat & vbtab & "RP left = " & Sim.RunicPower.Value)
				End If
			Else
				sim.combatlog.write(T  & vbtab &  "DC hit for " & dégat & vbtab & "RP left = " & Sim.RunicPower.Value)
			End If
			
		End If
		

		total = total + dégat
		sim.TryOnSpellHit
		If sim.TalentUnholy.UnholyBlight = 1 Then
			sim.UnholyBlight.Apply(T,dégat)
		End If
		If sim.DRW.IsActive(T) Then
			sim.DRW.DRWDeathCoil
		End If
		return true
		
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 443
		If sim.sigils.VengefulHeart Then tmp= tmp + 380
		if sim.Sigils.WildBuck then tmp = tmp + 80
		tmp = tmp + (0.15 * (1 + 0.04 * sim.TalentUnholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1 + sim.TalentUnholy.Morbidity * 5 / 100)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + sim.TalentFrost.BlackIce * 2 / 100)
		If sim.glyph.DarkDeath Then
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
	overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
end class