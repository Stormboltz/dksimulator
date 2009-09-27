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
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub

	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		
		'If TalentFrost.BloodoftheNorth = 5 Or TalentUnholy.Reaping = 3 Then
		'	runes.UseBlood(T,True)
		'Else
		sim.runes.UseBlood(T,False)
		'End If
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
		Dim intCount As Integer
		For intCount = 1 To Sim.NumberOfEnemies
			If sim.DoMySpellHit = false Then
				sim.combatlog.write(T  & vbtab &  "BB fail")
				MissCount = MissCount + 1
				Exit function
			End If
			RNG = sim.RandomNumberGenerator.RNGStrike
			dim dégat as Integer
			If RNG <= CritChance Then
				dégat = AvrgCrit(T)
				sim.combatlog.write(T  & vbtab &  "BB crit for " & dégat  )
				CritCount = CritCount + 1
			Else
				dégat = AvrgNonCrit(T)
				HitCount = HitCount + 1
				sim.combatlog.write(T  & vbtab &  "BB hit for " & dégat )
			End If
			

			total = total + dégat
			
			Sim.TryOnSpellHit
		Next intCount
		
		Sim.RunicPower.add (10) 
		
		return true
		'Debug.Print T & vbTab & "DeathCoil for " & Range("Abilities!N24").Value
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 200
		tmp = tmp + (0.04 * (1 + 0.04 * TalentUnholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1+ 0.1*talentblood.BloodyStrikes)
		if sim.NumDesease > 0 then tmp = tmp * 2
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if sim.runeforge.CinderglacierProc > 0 then
			tmp = tmp * 1.2
			sim.runeforge.CinderglacierProc = sim.runeforge.CinderglacierProc -1
 		end if
		return tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + TalentBlood.MightofMograine * 15 / 100) 
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit
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
