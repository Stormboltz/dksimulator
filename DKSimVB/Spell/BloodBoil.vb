'
' Created by SharpDevelop.
' User: Fabien
' Date: 15/03/2009
' Time: 01:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class BloodBoil
	Inherits Spells.Spell

	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		
		'If TalentFrost.BloodoftheNorth = 5 Or TalentUnholy.Reaping = 3 Then
		'	runes.UseBlood(T,True)
		'Else
		runes.UseBlood(T,False)
		'End If
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
		RNG = RNGStrike
		If mainstat.SpellHit >= 0.17 Then
			RNG = RNG+0.17
		Else
			RNG = RNG+mainstat.SpellHit
		End If
		If RNG < 0.17 Then
			combatlog.write(T  & vbtab &  "BB fail")
			MissCount = MissCount + 1
			Exit function
		End If
		RNG = RNGStrike
		dim dégat as Integer
		If RNG <= CritChance Then
			dégat = AvrgCrit(T)
			combatlog.write(T  & vbtab &  "BB crit for " & dégat  )
			CritCount = CritCount + 1
			
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			combatlog.write(T  & vbtab &  "BB hit for " & dégat )
		End If
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		
		RunicPower.add (10) 
		TryGreatness()
TryDeathChoice()
TryDCDeath()
		
		
		
		
		
		
		return true
		'Debug.Print T & vbTab & "DeathCoil for " & Range("Abilities!N24").Value
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 200
		tmp = tmp + (0.04 * (1 + 0.04 * TalentUnholy.Impurity) * MainStat.AP)
		tmp = tmp * (1+ 0.1*talentblood.BloodyStrikes)
		if sim.NumDesease > 0 then tmp = tmp * 2
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if CinderglacierProc > 0 then
			tmp = tmp * 1.2
			CinderglacierProc = CinderglacierProc -1
 		end if
		return tmp
		
		
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + TalentBlood.MightofMograine * 15 / 100) 
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = MainStat.SpellCrit
	End Function
	overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	overrides Function report As String
		dim tmp as String
		tmp = "Blood Boil" & VBtab
	
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

	
	
	
End Class
