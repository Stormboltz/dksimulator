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
Sub New(S As sim)
		MyBase.New(s)
		HasteSensible = true
	End Sub
	Public Overrides Function ApplyDamage(T As Long) As Boolean
		dim dégat as Integer
		If DoMyStrikeHit = false Then
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "BCB fail")
			MissCount = MissCount + 1
			Exit function
		End If
		dégat = AvrgNonCrit(T)
		totalhit += dégat
		total = total + dégat
		HitCount = HitCount + 1
		if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "BCB hit for " & dégat )
		return true
	End Function
	public Overrides Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		If offhand = false Then
			tmp = sim.MainStat.MHBaseDamage
		Else
			tmp = sim.MainStat.OHBaseDamage
			tmp = tmp * 0.5
				tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 8.3333 / 100)
		End If
		tmp = tmp * (0.25 + 0.125 * Sim.NumDesease)
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		return tmp
	End Function
	public Overrides Function CritCoef() As Double
	End Function
	
	public Overrides Function CritChance() As Double
		return sim.MainStat.crit
	End Function
	public Overrides Function AvrgCrit(T as long) As Double
		return AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHBloodCakedBlade.Total
		TotalHit += sim.OHBloodCakedBlade.TotalHit
		TotalCrit += sim.OHBloodCakedBlade.TotalCrit

		MissCount = (MissCount + sim.OHBloodCakedBlade.MissCount)/2
		HitCount = (HitCount + sim.OHBloodCakedBlade.HitCount)/2
		CritCount = (CritCount + sim.OHBloodCakedBlade.CritCount)/2
		
		sim.OHBloodCakedBlade.Total = 0
		sim.OHBloodCakedBlade.TotalHit = 0
		sim.OHBloodCakedBlade.TotalCrit = 0
	End sub
End class
