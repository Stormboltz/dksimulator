'
' Created by SharpDevelop.
' User: Fabien
' Date: 06/04/2009
' Time: 22:07
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Module RuneStrike
	
	Friend total As Long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
		Friend TotalHit As Long
	Friend TotalCrit as Long

	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		_NextWhiteMainHit = 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
	Function ApplyDamage(T As long) As boolean
		Dim dégat As Integer
		Dim BCB As Double
		Dim Nec As Double
		Dim MeleeMissChance As Single
		RunicPower.Value = RunicPower.Value - 20
		
		Dim RNG As Double
		RNG = Rnd
		MeleeMissChance = 0.08
		
		If mainstat.Hit > MeleeMissChance Then
			MeleeMissChance = 0
		Else
			MeleeMissChance = MeleeMissChance - mainstat.Hit
		End If
		
		If RNG < (MeleeMissChance) Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Rune Strike fail")
			exit function
		End If
		
		RNG = Rnd
		If RNG < CritChance Then
			'CRIT !
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "Rune Strike crit for " & dégat )
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Rune Strike hit for " & dégat )
		End If
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		
		If Talentfrost.KillingMachine > 0 Then
			RNG = Rnd
			If RNG < (Talentfrost.KillingMachine)*MainStat.MHWeaponSpeed/60 Then
				if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Killing Machine Proc")
				proc.KillingMachine  = true
			End If
		End If
		
		If MHRazorice Then applyRazorice()
		If TalentUnholy.Necrosis > 0 Then
			Nec = Necrosis.ApplyDamage(dégat, T)
		End If
		RNG = Rnd * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			BCB = BloodCakedBlade.ApplyDamage(T,true)
		End If
		TryMHCinderglacier
		TryMHFallenCrusader
		TryMjolRune
		TryGrimToll
		If proc.ScentOfBloodProc > 0 Then
			proc.ScentOfBloodProc  = proc.ScentOfBloodProc  -1
			RunicPower.add(5)
		End If
		
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = MainStat.MHBaseDamage * 1.5
		tmp = tmp * MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1+ SetBonus.T82PTNK*0.1)
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		CritChance = MainStat.crit + glyph.RuneStrike * 0.1
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function report As String
		dim tmp as String
		tmp = "Runic Strike" & VBtab
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & int(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & int(HitCount+CritCount) & VBtab
		tmp = tmp & int(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
end module



