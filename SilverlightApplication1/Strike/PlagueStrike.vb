Friend Class PlagueStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
	
		If OffHand = False Then
			UseGCD(T)	

			If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHPlagueStrike.ApplyDamage(T)
			If DoMyStrikeHit = false Then
				MissCount = MissCount + 1
				sim.combatlog.write(T  & vbtab &  "PS fail")
                Return False
			End If
		Else
            If DoMyToTHit() = False Then Return False

		End If
		If OffHand = False Then
            sim.RunicPower.add(10 + sim.Character.Talents.Talent("Dirge").Value * 5)
        End If


        RNG = RngCrit
        If RNG <= CritChance Then
            CritCount = CritCount + 1
            LastDamage = AvrgCrit(T)
            sim.combatlog.write(T & vbtab & "PS crit for " & LastDamage)
            sim.proc.tryOnCrit()

            totalcrit += LastDamage
        Else
            HitCount = HitCount + 1
            LastDamage = AvrgNonCrit(T)
            totalhit += LastDamage
            sim.combatlog.write(T & vbtab & "PS hit for " & LastDamage)
        End If
        total = total + LastDamage
        If OffHand = False Then
            sim.proc.TryOnMHHitProc()
            sim.runes.UseUnholy(T, False)

        Else
            sim.proc.TryOnOHHitProc()
        End If
        sim.proc.strife.tryme(t)
        sim.Targets.MainTarget.BloodPlague.Apply(T)

        If sim.DRW.IsActive(T) Then
            sim.drw.DRWPlagueStrike()
        End If
        Return True
    End Function
    Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double

        If OffHand = False Then
            tmp = sim.MainStat.NormalisedMHDamage * 1
        Else
            tmp = sim.MainStat.NormalisedOHDamage * 1
        End If

        tmp = tmp + 378
        tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
        tmp = tmp * (1 + sim.Character.Talents.Talent("CorruptingStrikes").Value * 10 / 100)
        If sim.character.glyph.PlagueStrike Then tmp = tmp * (1.2)
        If OffHand Then
            tmp = tmp * 0.5
            tmp = tmp * (1 + sim.Character.Talents.Talent("NervesofColdSteel").Value * 8.3333 / 100)
        End If
        AvrgNonCrit = tmp
    End Function
    Public Overrides Function CritCoef() As Double
        If _CritCoef <> -1 Then Return _CritCoef
        _CritCoef = (1 + sim.Character.Talents.Talent("ViciousStrikes").Value * 15 / 100)
        _CritCoef = _CritCoef * (1 + 0.06 * sim.MainStat.CSD)
        Return _CritCoef
    End Function
    Public Overrides Function CritChance() As Double
        Dim tmp As Double

        tmp = sim.MainStat.crit + sim.Character.Talents.Talent("ViciousStrikes").Value * 3 / 100 + sim.MainStat.T72PTNK * 0.1

        Return tmp
    End Function
	

	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHPlagueStrike.Total
		TotalHit += sim.OHPlagueStrike.TotalHit
		TotalCrit += sim.OHPlagueStrike.TotalCrit

		MissCount = (MissCount + sim.OHPlagueStrike.MissCount)/2
		HitCount = (HitCount + sim.OHPlagueStrike.HitCount)/2
		CritCount = (CritCount + sim.OHPlagueStrike.CritCount)/2
		
		sim.OHPlagueStrike.Total = 0
		sim.OHPlagueStrike.TotalHit = 0
		sim.OHPlagueStrike.TotalCrit = 0
	End sub
	
	
	
	
end Class