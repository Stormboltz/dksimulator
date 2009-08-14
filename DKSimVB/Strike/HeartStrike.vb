'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:11
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module HeartStrike
	Friend total As long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount As Integer
		Friend TotalHit As Long
	Friend TotalCrit as Long

	
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
		
		If Hysteria.IsAvailable(T) then Hysteria.use(T)
		
		RNG = Rnd	
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100 + sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150 + sim._MainFrm.txtLatency.Text/10
		End If
		
		If mainstat.Expertise >= 0.065 Then
			RNG = RNG+0.065
		Else
			RNG=RNG + mainstat.Expertise
		End If
		If mainstat.Hit >= 0.08 Then
			RNG = RNG+0.08
		Else
			RNG = RNG+mainstat.Hit
		End If
		If RNG < 0.145 Then
			combatlog.write(T  & vbtab &  "HS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		
		if sigils.HauntedDreams then
			RNG = Rnd
			if RNG <= 0.15 then
				HauntedDreamsFade = T+1000
			end if
		end if 
		RNG = Rnd
		dim dégat as Integer
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			combatlog.write(T  & vbtab &  "HS crit for " & dégat)
		Else
			HitCount = HitCount + 1
			dégat =  AvrgNonCrit(T)
			combatlog.write(T  & vbtab &  "HS hit for " & dégat)
		End If
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		RNG = Rnd
		If rng < 0.05*talentblood.SuddenDoom Then
			deathcoil.ApplyDamage(T,true)
		End If
		If TalentFrost.BloodoftheNorth = 3 Or TalentUnholy.Reaping = 3 Then 
			runes.UseBlood(T,True)
		Else
			runes.UseBlood(T,False)
		End If
		
		If DRW.IsActive(T) Then
			DRW.HeartStrike
		End If
		RunicPower.add (10)
		TryMHCinderglacier
		TryMHFallenCrusader
		TryT92PDPS
		TryMjolRune
		TryGrimToll
		
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		'total damage increased by 12.5% for each of your diseases on the target.
		tmp = MainStat.NormalisedMHDamage * 0.5
		tmp = tmp + 368
		if SetBonus.T84PDPS = 1 then
			tmp = tmp * (1 + 0.1 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.1 * Sim.NumDesease)
		end if
		tmp = tmp * (1 + TalentBlood.BloodyStrikes * 15 / 100)
		tmp = tmp * (1 + TalentFrost.BloodoftheNorth * 5 / 100)
		
		if sigils.DarkRider then tmp = tmp + 45 + 22.5 * Sim.NumDesease
		tmp = tmp * MainStat.StandardPhysicalDamageMultiplier(T)
		AvrgNonCrit = tmp
	End Function
	
	Function CritCoef() As Double
		CritCoef = 1* (1 + TalentBlood.MightofMograine * 15 / 100)
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		CritChance = MainStat.crit + TalentBlood.Subversion * 3 / 100
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function report As String
		dim tmp as String
		tmp = "Heart Strike" & VBtab
	
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
End Module
