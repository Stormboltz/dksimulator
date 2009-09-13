Friend class DeathCoil 
	Inherits Spells.Spell
	

	

	Function isAvailable(T As long) As Boolean
		If DRW.cd <= T And TalentBlood.DRW = 1 And RunicPower.Value < 100 Then Return False
		If Gargoyle.cd <= T And talentunholy.SummonGargoyle = 1 And RunicPower.Value < 100 Then Return False
		'If glyph.DeathStrike And RunicPower.Value <= 65  Then Return False 'This is not really important
		If RunicPower.Value >= 40 Then isAvailable = True
	End Function
	
    overrides	Function ApplyDamage(T As long,SDoom as Boolean) As boolean
		Dim RNG As Double

		If SDoom=False  Then
			Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
			RunicPower.Value = RunicPower.Value - 40
		End If
		
		If DoMySpellHit = false Then
			combatlog.write(T  & vbtab &  "DC fail")
			MissCount = MissCount + 1
			Exit function
		End If
		
		RNG = RNGStrike
		dim dégat as Integer
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			dégat= AvrgCrit(T)
			If SDoom Then
				combatlog.write(T  & vbtab &  "DC SDoom crit for " & dégat & vbtab & "RP left = " & RunicPower.Value)
			Else
				combatlog.write(T  & vbtab &  "DC crit for " & dégat & vbtab & "RP left = " & RunicPower.Value)
			End If
		Else
			dégat= AvrgNonCrit(T)
			HitCount = HitCount + 1
			If SDoom Then
				combatlog.write(T  & vbtab &  "DC SDoom hit for " & dégat & vbtab & "RP left = " & RunicPower.Value)
			Else
				combatlog.write(T  & vbtab &  "DC hit for " & dégat & vbtab & "RP left = " & RunicPower.Value)
			End If
			
		End If
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		TryGreatness()
		TryDeathChoice()
		TryDCDeath()
		If TalentUnholy.UnholyBlight = 1 Then
			sim.UnholyBlight.Apply(T,dégat)
		End If
		If DRW.IsActive(T) Then
			DRW.DeathCoil
		End If
		return true
		'Debug.Print T & vbTab & "DeathCoil for " & Range("Abilities!N24").Value
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 443
		If sigils.VengefulHeart Then tmp= tmp + 380
		if Sigils.WildBuck then tmp = tmp + 80
		tmp = tmp + (0.15 * (1 + 0.04 * TalentUnholy.Impurity) * MainStat.AP)
		tmp = tmp * (1 + TalentUnholy.Morbidity * 5 / 100)
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		If glyph.DarkDeath Then
			tmp = tmp * (1.15)
		End If
		if CinderglacierProc > 0 then
			tmp = tmp * 1.2
			CinderglacierProc = CinderglacierProc -1
 		end if
		return tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = MainStat.SpellCrit + 8/100 * SetBonus.T82PDPS
	End Function
	overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
end class