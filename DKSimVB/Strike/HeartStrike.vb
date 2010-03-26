'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:11
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class HeartStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	public Overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		
		If sim.Hysteria.IsAvailable(T)  Then sim.Hysteria.use(T)
		
		UseGCD(T)
		
		If DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab &  "HS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		
		Dim intCount As Integer
		For intCount = 1 To Sim.NumberOfEnemies
			if intCount <= 2 then
				RNG = MyRNG
				Dim dégat As Integer
				
				If RNG <= CritChance Then
					CritCount = CritCount + 1
					If intCount = 2 Then
						dégat =  AvrgCritOnSecondtarget(T)
						
						totalcrit += dégat
					Else
						dégat = AvrgCrit(T)
						
						totalcrit += dégat
					End If
					sim.combatlog.write(T  & vbtab &  "HS crit for " & dégat)
				Else
					HitCount = HitCount + 1
					If intCount = 2 Then
						dégat =  AvrgNonCritOnSecondtarget(T)
						totalhit += dégat
					Else
						dégat =  AvrgNonCrit(T)
						totalhit += dégat
					End If
					sim.combatlog.write(T  & vbtab &  "HS hit for " & dégat)
				End If
				
				
				total = total + dégat
				sim.proc.HauntedDreams.TryMe(T)
				sim.proc.T92PDPS.TryMe(T)
				sim.TryOnMHHitProc
				RNG = MyRNG
				If sim.proc.SuddenDoom.TryMe(T) Then sim.DeathCoil.ApplyDamage(T, True)
			End If
		Next intCount
		
		If sim.proc.ReapingBotN.TryMe(T) Then
			sim.Runes.UseBlood(T, True)
		Else
			sim.Runes.UseBlood(T, False)
		End If
		
		If sim.DRW.IsActive(T) Then
			sim.DRW.HeartStrike
		End If
		Sim.RunicPower.add (10)
		
		return true
	End Function
	public Overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = sim.MainStat.NormalisedMHDamage * 0.5
		tmp = tmp + 368
		if sim.MainStat.T84PDPS = 1 then
			tmp = tmp * (1 + 0.1 * Sim.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.1 * Sim.NumDesease)
		end if
		tmp = tmp * (1 + sim.TalentBlood.BloodyStrikes * 15 / 100)
		tmp = tmp * (1 + sim.TalentFrost.BloodoftheNorth * 5 / 100)
		
		if sim.sigils.DarkRider then tmp = tmp + 45 + 22.5 * Sim.NumDesease
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		
		If sim.MainStat.T102PDPS<>0 Then
			tmp = tmp * 1.07
		End If
		if sim.MainStat.T92PTNK =1 then tmp = tmp *1.05
		AvrgNonCrit = tmp
	End Function
	
	Function AvrgNonCritOnSecondtarget(T As long) As Double
		Dim tmp As Double
		Dim NumDesease As Integer
		NumDesease=0
		if sim.BloodPlague.OtherTargetsFade > T then NumDesease = NumDesease + 1
		if sim.FrostFever.OtherTargetsFade > T then NumDesease = NumDesease + 1
		
		tmp = sim.MainStat.NormalisedMHDamage * 0.5
		tmp = tmp + 368
		if sim.MainStat.T84PDPS = 1 then
			tmp = tmp * (1 + 0.1 * NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.1 * NumDesease)
		end if
		tmp = tmp * (1 + sim.TalentBlood.BloodyStrikes * 15 / 100)
		tmp = tmp * (1 + sim.TalentFrost.BloodoftheNorth * 5 / 100)
		
		if sim.sigils.DarkRider then tmp = tmp + 45 + 22.5 * Sim.NumDesease
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp / 2
		return tmp
	End Function
	
	Function AvrgCritOnSecondtarget(T As long) As Double
		return AvrgNonCritOnSecondtarget(T) * (1 + CritCoef)
	End Function
	
	public Overrides Function CritCoef() As Double
		CritCoef = 1* (1 + sim.TalentBlood.MightofMograine * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.crit + sim.TalentBlood.Subversion * 3 / 100
	End Function
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
End Class
