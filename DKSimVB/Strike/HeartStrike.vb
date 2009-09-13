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

	
	public Overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		
		If sim.Hysteria.IsAvailable(T)  Then sim.Hysteria.use(T)
		
		
		
		
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100 + sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150 + sim._MainFrm.txtLatency.Text/10
		End If
		
		If DoMyStrikeHit = false Then
			combatlog.write(T  & vbtab &  "HS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		tryHauntedDreams()
		
		RNG = RNGStrike
		dim dégat as Integer
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			combatlog.write(T  & vbtab &  "HS crit for " & dégat)
			TryBitterAnguish()
			TryMirror()
			TryPyrite()
			TryOldGod()
		Else
			HitCount = HitCount + 1
			dégat =  AvrgNonCrit(T)
			combatlog.write(T  & vbtab &  "HS hit for " & dégat)
		End If
		
		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		RNG = RNGStrike
		If rng < 0.05*talentblood.SuddenDoom Then
			sim.deathcoil.ApplyDamage(T,true)
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
		TryGreatness()
		TryDeathChoice()
		TryDCDeath()
		TryVictory()
		TryBandit()
		TryDarkMatter()
		TryComet()
		
		return true
	End Function
	public Overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
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
	public Overrides Function CritCoef() As Double
		CritCoef = 1* (1 + TalentBlood.MightofMograine * 15 / 100)
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = MainStat.crit + TalentBlood.Subversion * 3 / 100
	End Function
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	
End Class
