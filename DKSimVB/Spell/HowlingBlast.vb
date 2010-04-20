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
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	Function isAvailable(T As Long) As Boolean
		if sim.TalentFrost.HowlingBlast <> 1 then return false
		if cd <= T then return true
	End Function
	
	overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		UseGCD(T)
		cd = T + 800
		
		If DoMySpellHit = false Then
			sim.combatlog.write(T  & vbtab &  "HB fail")
			sim.proc.KillingMachine.Use
			sim.Proc.rime.Use
			MissCount = MissCount + 1
			Exit function
		End If
		
		If sim.proc.rime.IsActive Then
			sim.Proc.rime.Use
			Sim.RunicPower.add (sim.TalentFrost.ChillOfTheGrave * 2.5)
		Else
			sim.runes.UseFU(T,False)
			Sim.RunicPower.add (15 + (sim.TalentFrost.ChillOfTheGrave * 2.5))
		End If
		
		Dim intCount As Integer
		For intCount = 1 To Sim.NumberOfEnemies
			RNG = RngCrit
			Dim dégat As Integer
			Dim ccT As Double
			ccT = CritChance
			If RNG <= ccT Then
				CritCount = CritCount + 1
				dégat = AvrgCrit(T)
				sim.combatlog.write(T  & vbtab &  "HB crit for " & dégat )
				totalcrit += dégat
			Else
				HitCount = HitCount + 1
				dégat = AvrgNonCrit(T)
				sim.combatlog.write(T  & vbtab &  "HB hit for " & dégat)
				totalhit += dégat
			End If
			total = total + dégat
			sim.TryOnSpellHit
		Next intCount
		
		
		
		sim.proc.KillingMachine.Use
		if sim.glyph.HowlingBlast then
			sim.FrostFever.Apply(T)
		End If
		
		return true
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 585
		tmp = tmp + (0.2 * (1 + 0.04 * sim.TalentUnholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1 + sim.TalentFrost.BlackIce * 2 / 100)
		if sim.NumDesease > 0 or (sim.Buff.BloodPlague+sim.Buff.FrostFever>0) then 	tmp = tmp * (1 + sim.TalentFrost.GlacierRot * 6.6666666 / 100)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		If sim.ExecuteRange Then tmp = tmp *(1+ 0.06*sim.talentfrost.MercilessCombat)
		tmp *= sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
		if sim.RuneForge.CheckCinderglacier(True) > 0 then tmp *= 1.2
		AvrgNonCrit = tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + sim.TalentFrost.GuileOfGorefiend * 0.5 * 15 / 100) 'GoG works off the 1.5 spell crit modifier or something like that
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit
		If sim.proc.KillingMachine.IsActive Then
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
