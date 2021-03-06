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
		if sim.Character.talentfrost.HowlingBlast <> 1 then return false
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
			Sim.RunicPower.add (sim.Character.talentfrost.ChillOfTheGrave * 2.5)
		Else
			sim.runes.UseFU(T,False)
			Sim.RunicPower.add (15 + (sim.Character.talentfrost.ChillOfTheGrave * 2.5))
		End If
		Dim Tar As Targets.Target
		
		For Each Tar In sim.Targets.AllTargets
			RNG = RngCrit
			Dim d�gat As Integer
			Dim ccT As Double
			ccT = CritChance
			If RNG <= ccT Then
				CritCount = CritCount + 1
				d�gat = AvrgCrit(T,Tar)
				sim.combatlog.write(T  & vbtab &  "HB crit for " & d�gat )
				totalcrit += d�gat
			Else
				HitCount = HitCount + 1
				d�gat = AvrgNonCrit(T,Tar)
				sim.combatlog.write(T  & vbtab &  "HB hit for " & d�gat)
				totalhit += d�gat
			End If
			total = total + d�gat
			sim.TryOnSpellHit
		Next
	
		
		sim.proc.KillingMachine.Use
		if sim.character.glyph.HowlingBlast then
			sim.Targets.MainTarget.FrostFever.Apply(T)
		End If
		
		return true
	End Function
	overrides Function AvrgNonCrit(T As Long, target As Targets.Target ) As Double
		if target is nothing then target = sim.Targets.MainTarget
		Dim tmp As Double
		tmp = 585
		tmp = tmp + (0.2 * (1 + 0.04 * sim.Character.talentunholy.Impurity) * sim.MainStat.AP)
		tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
		if target.NumDesease > 0 or (target.Debuff.BloodPlague+target.Debuff.FrostFever>0) then 	tmp = tmp * (1 + sim.Character.talentfrost.GlacierRot * 6.6666666 / 100)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		If sim.ExecuteRange Then tmp = tmp *(1+ 0.06*sim.Character.talentfrost.MercilessCombat)
		tmp *= sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
		if sim.RuneForge.CheckCinderglacier(True) > 0 then tmp *= 1.2
		AvrgNonCrit = tmp
	End Function
	overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + sim.Character.talentfrost.GuileOfGorefiend * 0.5 * 15 / 100) 'GoG works off the 1.5 spell crit modifier or something like that
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
	overrides Function AvrgCrit(T As long,target As Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	


End Class
