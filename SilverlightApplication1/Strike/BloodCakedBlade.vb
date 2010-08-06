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

        If DoMyStrikeHit = False Then
            If sim.combatlog.LogDetails Then sim.combatlog.write(T & vbtab & "BCB fail")
            MissCount = MissCount + 1
            Return False
        End If
        LastDamage = AvrgNonCrit(T)
        totalhit += LastDamage
        total = total + LastDamage
        HitCount = HitCount + 1
        If sim.combatlog.LogDetails Then sim.combatlog.write(T & vbtab & "BCB hit for " & LastDamage)
		return true
	End Function
	overrides Function AvrgNonCrit(T As Long, target As Targets.Target ) As Double
		Dim tmp As Double
		If offhand = false Then
			tmp = sim.MainStat.MHBaseDamage
		Else
			tmp = sim.MainStat.OHBaseDamage
			tmp = tmp * 0.5
            tmp = tmp * (1 + sim.Character.Talents.Talent("NervesofColdSteel").Value * 8.3333 / 100)
        End If
        tmp = tmp * (0.25 + 0.125 * target.NumDesease)
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		return tmp
	End Function

	public Overrides Function CritChance() As Double
		return sim.MainStat.crit
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
