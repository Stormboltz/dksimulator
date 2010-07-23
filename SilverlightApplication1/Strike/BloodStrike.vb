
Friend class BloodStrike
	Inherits Strikes.Strike
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	Function CheckDesolation(T As Long) As Boolean
		dim OtherT as Long
		If sim.proc.Desolation.IsActiveAt(T + 1000) Then Return False
		OtherT = sim.Runes.GetNextBloodCD(T)
		If OtherT = 0 Then Return Not sim.proc.Desolation.IsActiveAt(T)
		Return Not sim.proc.Desolation.IsActiveAt(OtherT+150)
		
	End Function
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		dim dégat as Integer

		If OffHand = False Then
			UseGCD(T)
			If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHBloodStrike.ApplyDamage(T)
			If DoMyStrikeHit() = False Then
				sim.CombatLog.write(T & vbTab & "BS fail")
				MissCount += 1
				Return False
			End If
		Else
            If DoMyToTHit() = False Then Return False
		End If
		If sim.KeepBloodSync Then
			If sim.BloodToSync = True Then
				sim.BloodToSync = False
			Else
				sim.BloodToSync = True
			End If
		End If
		If OffHand = False Then 
			sim.RunicPower.add(10)
		End If
		
		RNG = RngCrit

		If RNG <= CritChance() Then
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			sim.CombatLog.write(T & vbTab & "BS crit for " & dégat)
			TotalCrit += dégat
            sim.proc.tryOnCrit()
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			TotalHit += dégat
			sim.CombatLog.write(T & vbTab & "BS hit for " & dégat)
		End If
		total = total + dégat

		If OffHand = False Then
            sim.proc.TryOnMHHitProc()
			If sim.proc.ReapingBotN.TryMe(T) Then
				sim.Runes.UseBlood(T, True)
			Else
				sim.Runes.UseBlood(T, False)
			End If
		Else
            sim.proc.TryOnOHHitProc()
		End If
        sim.proc.TryOnBloodStrike()
		Return True
End Function

overrides Function AvrgNonCrit(T As Long, target As Targets.Target ) As Double
	Dim tmp As Double
	
	If offhand = false Then
            tmp = sim.MainStat.NormalisedMHDamage * 0.8
	Else
            tmp = sim.MainStat.NormalisedOHDamage * 0.8
	End If
        tmp = tmp + 611.2
	if sim.MainStat.T84PDPS = 1 then
		tmp = tmp * (1 + 0.125 * target.NumDesease * 1.2)
	else
		tmp = tmp * (1 + 0.125 * target.NumDesease)
	End If

        tmp = tmp * (1 + sim.Character.Talents.Talent("BloodoftheNorth").Value * 5 / 100)

        If sim.sigils.DarkRider Then tmp = tmp + 45 + 22.5 * target.NumDesease
        If sim.character.glyph.BloodStrike Then tmp = tmp * (1.2)
        tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
        If offhand Then
            tmp = tmp * 0.5
            tmp = tmp * (1 + sim.Character.Talents.Talent("NervesofColdSteel").Value * 8.3333 / 100)
        End If
        If sim.MainStat.T92PTNK = 1 Then tmp = tmp * 1.05
        AvrgNonCrit = tmp
    End Function

    Public Overrides Function CritCoef() As Double
        CritCoef = 1
        CritCoef = CritCoef * (1 + 0.06 * sim.mainstat.CSD)
    End Function
    Public Overrides Function CritChance() As Double
        CritChance = sim.MainStat.crit
    End Function
public Overrides Function AvrgCrit(T as long,target As Targets.Target) As Double
	AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
End Function


Public Overrides Sub Merge()
	If sim.MainStat.DualW = false Then exit sub
	Total += sim.OHBloodStrike.Total
	TotalHit += sim.OHBloodStrike.TotalHit
	TotalCrit += sim.OHBloodStrike.TotalCrit
	MissCount = (MissCount + sim.OHBloodStrike.MissCount)/2
	HitCount = (HitCount + sim.OHBloodStrike.HitCount)/2
	CritCount = (CritCount + sim.OHBloodStrike.CritCount)/2
	sim.OHBloodStrike.Total = 0
	sim.OHBloodStrike.TotalHit = 0
	sim.OHBloodStrike.TotalCrit = 0
End sub

End Class