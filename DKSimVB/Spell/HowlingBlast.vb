'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 22:48
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class HowlingBlast
	Inherits Spells.Spell
	
	Function isAvailable(T As Long) As Boolean
		if TalentFrost.HowlingBlast <> 1 then return false
		if cd <= T then return true
	End Function
	
	
	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
		cd = T + 800
		
		If DoMySpellHit = false Then
			combatlog.write(T  & vbtab &  "HB fail")
			proc.KillingMachine  = False
			Proc.rime = False
			MissCount = MissCount + 1
			Exit function
		End If
		RNG = RNGStrike
		Dim d�gat As Integer
		Dim ccT As Double
		ccT = CritChance
		If RNG <= ccT Then
			CritCount = CritCount + 1
			d�gat = AvrgCrit(T)
			combatlog.write(T  & vbtab &  "HB crit for " & d�gat )
		Else
			HitCount = HitCount + 1
			d�gat = AvrgNonCrit(T)
			combatlog.write(T  & vbtab &  "HB hit for " & d�gat)
		End If
		
		if Lissage then d�gat = AvrgCrit(T)*ccT + AvrgNonCrit(T)*(1-CritChance )
		total = total + d�gat
		
		If Proc.rime Then
			Proc.rime = False
			RunicPower.add (TalentFrost.ChillOfTheGrave * 2.5)
		Else
			runes.UseFU(T,False)
			RunicPower.add (15 + (TalentFrost.ChillOfTheGrave * 2.5))
		End If
		
		proc.KillingMachine  = false
		if glyph.HowlingBlast then
			sim.FrostFever.Apply(T)
		End If
		TryGreatness()
		TryDeathChoice()
		TryDCDeath()
		return true
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		
		Dim tmp As Double
		tmp = 585
		tmp = tmp + (0.2 * (1 + 0.04 * TalentUnholy.Impurity) * MainStat.AP)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if sim.NumDesease > 0 then 	tmp = tmp * (1 + TalentFrost.GlacierRot * 6.6666666 / 100)
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		If (T/sim.MaxTime) >= 0.75 Then tmp = tmp *(1+ 0.06*talentfrost.MercilessCombat)
		if MHRazorice or (OHRazorice and mainstat.DualW)  then tmp = tmp *1.10
		if CinderglacierProc > 0 then
			tmp = tmp * 1.2
			CinderglacierProc = CinderglacierProc -1
		end if
		AvrgNonCrit = tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + Talentfrost.GuileOfGorefiend * 0.5 * 15 / 100) 'GoG works off the 1.5 spell crit modifier or something like that
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = MainStat.SpellCrit
		If proc.KillingMachine  = True Then
			Return 1
		Else
			If sim.DeathChill.IsAvailable(sim.TimeStamp) Then
				sim.Deathchill.use(sim.TimeStamp)
				sim.DeathChill.Active = false
				Return 1
			End If
		End If
	End Function
	overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	


End Class
