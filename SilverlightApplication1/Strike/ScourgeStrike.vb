Friend Class ScourgeStrike
	Inherits strikes.Strike
	
	Private tmpPhysical As integer
	Private tmpMagical As integer
	Private MagicHit As long
	Private MagicCrit As long
	Friend MagicTotal As Long
	
	
	Sub New(S As sim)
		MyBase.New(s)
		MagicCrit = 0
		MagicHit = 0
		MagicTotal = 0
	End Sub
	
	public Overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		'scourgestrike glyph
		UseGCD(T)
		
		If DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab &  "SS fail")
			MissCount = MissCount + 1
            Return False
		End If
		
        sim.RunicPower.add(15 + sim.Character.Talents.Talent("Dirge").Value * 2.5 + 5 * sim.MainStat.T74PDPS)
        Dim dégat As Integer
        tmpPhysical = 0
        tmpMagical = 0
        'Physical part
        RNG = RngCrit
        If RNG <= CritChance Then
            CritCount = CritCount + 1
            dégat = AvrgNonCrit(T) * (1 + CritCoef)
            sim.combatlog.write(T & vbtab & "SS Physical crit for " & dégat)
            totalcrit += dégat
            sim.proc.tryOnCrit()
            sim.ScourgeStrikeMagical.ApplyDamage(dégat, T, True)
        Else
            HitCount = HitCount + 1
            dégat = AvrgNonCrit(T)
            totalhit += dégat
            sim.combatlog.write(T & vbtab & "SS Physical hit for " & dégat)
            sim.ScourgeStrikeMagical.ApplyDamage(dégat, T, False)
        End If

        total = total + dégat


        If sim.character.glyph.ScourgeStrike Then
            If sim.Targets.MainTarget.BloodPlague.ScourgeStrikeGlyphCounter < 3 Then
                sim.Targets.MainTarget.BloodPlague.FadeAt = sim.Targets.MainTarget.BloodPlague.FadeAt + 3 * 100
                sim.Targets.MainTarget.BloodPlague.ScourgeStrikeGlyphCounter = sim.Targets.MainTarget.BloodPlague.ScourgeStrikeGlyphCounter + 1
            End If
            If sim.Targets.MainTarget.FrostFever.ScourgeStrikeGlyphCounter < 3 Then
                sim.Targets.MainTarget.FrostFever.FadeAt = sim.Targets.MainTarget.FrostFever.FadeAt + 3 * 100
                sim.Targets.MainTarget.FrostFever.ScourgeStrikeGlyphCounter = sim.Targets.MainTarget.FrostFever.ScourgeStrikeGlyphCounter + 1
            End If
        End If
        sim.Runes.UseUnholy(T, False)
        sim.proc.TryOnFU()
        sim.proc.TryOnMHHitProc()
        Return True
    End Function

    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        tmpPhysical = sim.MainStat.NormalisedMHDamage
        tmpPhysical = tmpPhysical * 1
        tmpPhysical = tmpPhysical + 561
        If sim.sigils.Awareness Then tmpPhysical = tmpPhysical + 189
        If sim.sigils.ArthriticBinding Then tmpPhysical = tmpPhysical + 91.35
        tmpPhysical = tmpPhysical * sim.MainStat.StandardPhysicalDamageMultiplier(T)
        tmpPhysical = tmpPhysical * (1 + 6.6666666 * sim.Character.Talents.Talent("Outbreak").Value / 100)
        If sim.MainStat.T102PDPS <> 0 Then tmpPhysical = tmpPhysical * 1.1
        Return tmpPhysical
    End Function

    Public Overrides Function CritCoef() As Double
        CritCoef = 1 + sim.Character.Talents.Talent("ViciousStrikes").Value * 15 / 100
        CritCoef = CritCoef * (1 + 0.06 * sim.mainstat.CSD)
    End Function



    Public Overrides Function CritChance() As Double
        Dim tmp As Double
        tmp = sim.MainStat.crit + sim.Character.Talents.Talent("ViciousStrikes").Value * 3 / 100 + sim.MainStat.T72PDPS * 5 / 100
        Return tmp
    End Function
	
	public Overrides Function AvrgCrit(T As long,target As Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	

	
	Public Overrides sub Merge()
		Total += sim.ScourgeStrikeMagical.Total
		TotalHit += sim.ScourgeStrikeMagical.TotalHit
		TotalCrit += sim.ScourgeStrikeMagical.TotalCrit

		MissCount = (MissCount + sim.ScourgeStrikeMagical.MissCount)/2
		HitCount = (HitCount + sim.ScourgeStrikeMagical.HitCount)/2
		CritCount = (CritCount + sim.ScourgeStrikeMagical.CritCount)/2
		
		sim.ScourgeStrikeMagical.Total = 0
		sim.ScourgeStrikeMagical.TotalHit = 0
		sim.ScourgeStrikeMagical.TotalCrit = 0
	End sub
	
	
	
	
End Class
