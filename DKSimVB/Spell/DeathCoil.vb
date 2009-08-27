Friend module deathcoil
	
	Friend total As Long
		Friend TotalHit As Long
	Friend TotalCrit as Long

	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	
	Sub init
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
	
	Function isAvailable(T As long) As Boolean
		If DRW.cd <= T And TalentBlood.DRW = 1 And RunicPower.Value < 100 Then Return False
		If Gargoyle.cd <= T And talentunholy.SummonGargoyle = 1 And RunicPower.Value < 100 Then Return False
		'If glyph.DeathStrike And RunicPower.Value <= 65  Then Return False 'This is not really important
		If RunicPower.Value >= 40 Then isAvailable = True
	End Function
	
	Function ApplyDamage(T As long,SDoom as Boolean) As boolean
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
			combatlog.write(T  & vbtab &  "DC crit for " & dégat & vbtab & "RP left = " & RunicPower.Value)
		Else
			dégat= AvrgNonCrit(T)
			HitCount = HitCount + 1
			combatlog.write(T  & vbtab &  "DC hit for " & dégat & vbtab & "RP left = " & RunicPower.Value) 
		End If
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		TryGreatness()
TryDeathChoice()
TryDCDeath()
		If TalentUnholy.UnholyBlight = 1 Then	
			UnholyBlight.Apply(T,dégat)
		End If
		If DRW.IsActive(T) Then
			DRW.DeathCoil
		End If
		return true
		'Debug.Print T & vbTab & "DeathCoil for " & Range("Abilities!N24").Value
	End Function
	Function AvrgNonCrit(T As long) As Double
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
	Function CritCoef() As Double
		CritCoef = 1 
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		CritChance = MainStat.SpellCrit + 8/100 * SetBonus.T82PDPS	 
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function report As String
		dim tmp as String
		tmp = "Death Coil" & VBtab
	
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
end module