'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 11:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class DeathStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
		MyBase.New(s)
	End Sub

	'A deadly attack that deals 75% weapon damage plus 222.75
	'and heals the Death Knight for a percent of damage done
	'for each of <his/her> diseases on the target.

	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		UseGCD(T)	
		
		
		
		If OffHand = False Then
			If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHDeathStrike.ApplyDamage(T)
		End If
		If DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab & "DS fail")
			MissCount = MissCount + 1
			return false
		End If
		dim dégat as Integer
		RNG = MyRNG
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			sim.combatlog.write(T  & vbtab &  "DS crit for " & dégat  )
			sim.tryOnCrit
			
			totalcrit += dégat
		Else
			HitCount = HitCount + 1
			dégat = AvrgNonCrit(T)
			totalhit += dégat
			sim.combatlog.write(T  & vbtab &  "DS hit for " & dégat )
		End If
		total = total + dégat
		If OffHand = False Then
			sim.TryOnMHHitProc
			If sim.proc.DRM.TryMe(T) Then
				sim.Runes.UseFU(T, True)
			Else
				sim.Runes.UseFU(T, False)
			End If
			Sim.runicpower.add(15 +  2.5*sim.talentunholy.Dirge )
			Sim.runicpower.add(5*sim.MainStat.T74PDPS)
			sim.TryOnFU
		Else
			sim.TryOnOHHitProc
		End If
		
		If sim.DRW.IsActive(T) Then
			sim.drw.DeathStrike
		End If
		return true
	End Function
	public Overrides Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		
		If offhand = false Then
			tmp = sim.MainStat.NormalisedMHDamage*0.75
		Else
			tmp = sim.MainStat.NormalisedOHDamage*0.75
		End If
		tmp = tmp + 222.75
		if sim.sigils.Awareness then tmp = tmp + 315
		tmp = tmp * (1 + sim.TalentBlood.ImprovedDeathStrike * 15/100)
		If sim.glyph.DeathStrike Then
			If Sim.RunicPower.Value >= 25 Then
				tmp = tmp * (1 + 25/100)
			Else
				tmp = tmp * (1 + Sim.RunicPower.Value /100)
			End If
		End If
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		
		If offhand Then
			tmp = tmp * 0.5
				tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 8.3333 / 100)
		End If
		return tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + sim.TalentBlood.MightofMograine * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.crit + sim.TalentBlood.ImprovedDeathStrike * 3/100 + sim.MainStat.T72PDPS * 5/100
	End Function
	public Overrides Function AvrgCrit(T As Long) As Double
		Return AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHDeathStrike.Total
		TotalHit += sim.OHDeathStrike.TotalHit
		TotalCrit += sim.OHDeathStrike.TotalCrit

		MissCount = (MissCount + sim.OHDeathStrike.MissCount)/2
		HitCount = (HitCount + sim.OHDeathStrike.HitCount)/2
		CritCount = (CritCount + sim.OHDeathStrike.CritCount)/2
		
		sim.OHDeathStrike.Total = 0
		sim.OHDeathStrike.TotalHit = 0
		sim.OHDeathStrike.TotalCrit = 0
	End Sub
	
	
	
End Class
