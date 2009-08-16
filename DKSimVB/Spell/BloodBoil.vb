'
' Created by SharpDevelop.
' User: Fabien
' Date: 15/03/2009
' Time: 01:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module BloodBoil
	Friend total As Long
	Friend TotalHit As Long
	Friend TotalCrit as Long

	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	
	
		
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
		
	
	Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		
		'If TalentFrost.BloodoftheNorth = 5 Or TalentUnholy.Reaping = 3 Then
		'	runes.UseBlood(T,True)
		'Else
		runes.UseBlood(T,False)
		'End If
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
		RNG = Rnd
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
		RNG = Rnd
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
		


		
		
		If DRW.IsActive(T) Then
			If DRW.SpellHit >= 0.17 Then
				RNG = RNG+0.17
			Else
				RNG = RNG+DRW.SpellHit
			End If
			If RNG < 0.17 Then
				combatlog.write(T  & vbtab &  "DRW fail")
			Else
				RNG = Rnd
				If RNG <= drw.SpellCrit Then
					drw.total = drw.total + AvrgCrit(T)/2
					combatlog.write(T  & vbtab &  "DRW crit for " & int(AvrgCrit(T)/2) )
				Else
					drw.total = drw.total + AvrgNonCrit(T)/2
					combatlog.write(T  & vbtab &  "DRW hit for " & int(AvrgNonCrit(T)/2))
				End If
			End If
		End If
		
		RunicPower.add (10) 
		TryGreatness()
TryDeathChoice()
TryDCDeath()
		
		
		
		
		
		
		return true
		'Debug.Print T & vbTab & "DeathCoil for " & Range("Abilities!N24").Value
	End Function
	Function AvrgNonCrit(T As long) As Double
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
	Function CritCoef() As Double
		CritCoef = 1 * (1 + TalentBlood.MightofMograine * 15 / 100) 
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		CritChance = MainStat.SpellCrit
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function report As String
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

	
	
	
End Module
