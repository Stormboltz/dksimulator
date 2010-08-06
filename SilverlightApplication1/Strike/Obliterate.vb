'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:29
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Obliterate
	Inherits Strikes.Strike
	
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		
		
		If OffHand = False Then
			UseGCD(T)
			If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHObliterate.ApplyDamage(T)
			If DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab &  "OB fail" & vbtab & RNG)
				MissCount = MissCount + 1
                Return False
			End If
		
			If sim.proc.DRM.TryMe(T) Then
				sim.Runes.UseFU(T, True)
			Else
				sim.Runes.UseFU(T, False)
			End If

		Else
            If DoMyToTHit() = False Then Return False
		End If
		
		If OffHand = False Then 
            sim.RunicPower.add(15 + 5 * sim.Character.Talents.Talent("ChillOfTheGrave").Value + 5 * sim.MainStat.T74PDPS)
        End If

        Dim ccT As Double
        ccT = CritChance

        RNG = RngCrit
        If RNG <= ccT Then
            CritCount = CritCount + 1
            LastDamage = AvrgCrit(T)
            sim.combatlog.write(T & vbtab & "OB crit for " & LastDamage)
            sim.proc.tryOnCrit()
            totalcrit += LastDamage
        Else
            HitCount = HitCount + 1
            LastDamage = AvrgNonCrit(T)
            sim.combatlog.write(T & vbtab & "OB hit for " & LastDamage)
            totalhit += LastDamage
        End If
        total = total + LastDamage
        If OffHand Then
            sim.proc.TryOnOHHitProc()
        Else
            sim.proc.TryOnMHHitProc()
            sim.proc.TryOnFU()
            sim.proc.Rime.TryMe(T)
        End If


        If sim.DRW.IsActive(T) Then
            sim.drw.DRWObliterate()
        End If


        Return True
    End Function


    Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double
        If OffHand Then
            tmp = sim.MainStat.NormalisedOHDamage * 1.6 + 934.4
        Else
            tmp = sim.MainStat.NormalisedMHDamage * 1.6 + 934.4
        End If

        If sim.sigils.Awareness Then tmp = tmp + 336
        If sim.MainStat.T84PDPS = 1 Then
            tmp = tmp * (1 + 0.125 * Target.NumDesease * 1.2)
        Else
            tmp = tmp * (1 + 0.125 * Target.NumDesease)
        End If


        tmp = tmp * (1 + sim.Character.Talents.Talent("Annihilation").Value * 10 / 100)
        If sim.ExecuteRange Then tmp = tmp * (1 + 0.059999999999999998 * sim.Character.Talents.Talent("MercilessCombat").Value)
        tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
        If sim.character.glyph.Obliterate Then tmp = tmp * 1.2
        If OffHand Then
            tmp = tmp * 0.5
            tmp = tmp * (1 + sim.Character.Talents.Talent("NervesofColdSteel").Value * 8.3333 / 100)
        End If
        If sim.MainStat.T102PDPS <> 0 Then
            tmp = tmp * 1.1
        End If
        Return tmp

    End Function

    Public Overrides Function CritChance() As Double
        If sim.DeathChill.IsAvailable(sim.TimeStamp) Then
            sim.Deathchill.use(sim.TimeStamp)
            sim.DeathChill.Active = False
            Return 1
        End If
        Return sim.MainStat.crit + sim.MainStat.T72PDPS * 5 / 100

    End Function
	
	
	
	
	
	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHObliterate.Total
		TotalHit += sim.OHObliterate.TotalHit
		TotalCrit += sim.OHObliterate.TotalCrit
		
		MissCount = (MissCount + sim.OHObliterate.MissCount)/2
		HitCount = (HitCount + sim.OHObliterate.HitCount)/2
		CritCount = (CritCount + sim.OHObliterate.CritCount)/2
		
		sim.OHObliterate.Total = 0
		sim.OHObliterate.TotalHit = 0
		sim.OHObliterate.TotalCrit = 0
	End Sub
	
	
End class
