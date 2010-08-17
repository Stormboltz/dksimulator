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
        HasteSensible = True
        BaseDamage = 0
        Coeficient = 0.25
        Multiplicator = 1
    End Sub
	
	overrides Function AvrgNonCrit(T As Long, target As Targets.Target ) As Double
        Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
        tmp *= (1 + 4 * 0.125 * target.NumDesease)
        If sim.EPStat = "EP HasteEstimated" Then
            tmp *= sim.MainStat.EstimatedHasteBonus
        End If
		return tmp
	End Function

	public Overrides Function CritChance() As Double
        Return 0
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
