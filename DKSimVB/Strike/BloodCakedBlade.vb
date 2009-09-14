'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:47
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend class BloodCakedBlade
	Inherits Strikes.Strike

	public Overrides Function ApplyDamage(T As long,MH as Boolean) As boolean

		If DoMyStrikeHit = false Then
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "BCB fail")
			MissCount = MissCount + 1
			Exit function
		End If
		
		total = total + AvrgNonCrit(T,MH)
		
		If MH Then
			sim.TryOnMHHitProc
		Else
			sim.TryOnOHHitProc
		End If
		
		
		HitCount = HitCount + 1
		if combatlog.LogDetails then combatlog.write(T  & vbtab &  "BCB hit for " & int(AvrgNonCrit(T,MH)))
		return true
	End Function
	public Overrides Function AvrgNonCrit(T as long, MH as Boolean) As Double
		Dim tmp As Double
		If MH Then
			tmp = sim.MainStat.MHBaseDamage
		Else
			tmp = sim.MainStat.OHBaseDamage
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		tmp = tmp * (0.25 + 0.125 * Sim.NumDesease)
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		return tmp
	End Function
	public Overrides Function CritCoef() As Double
		
	End Function
	
	public Overrides Function CritChance() As Double
		return sim.MainStat.crit
	End Function
	public Overrides Function AvrgCrit(T as long,MH as Boolean) As Double
		return AvrgNonCrit(T,MH) * (1 + CritCoef)
	End Function
End class
