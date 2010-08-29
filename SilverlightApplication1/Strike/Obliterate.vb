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
        MyBase.New(S)
        BaseDamage = 934.4
        If sim.Sigils.Awareness Then BaseDamage = BaseDamage + 336
        Coeficient = 1.6
        Multiplicator = (1 + sim.Character.Talents.Talent("Annihilation").Value * 10 / 100)
        If sim.Character.Glyph.Obliterate Then Multiplicator = Multiplicator * 1.2
        If sim.MainStat.T102PDPS <> 0 Then Multiplicator = Multiplicator * 1.1

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
            sim.proc.tryProcs(Procs.ProcOnType.OnCrit)
            totalcrit += LastDamage
        Else
            HitCount = HitCount + 1
            LastDamage = AvrgNonCrit(T)
            sim.combatlog.write(T & vbtab & "OB hit for " & LastDamage)
            totalhit += LastDamage
        End If
        total = total + LastDamage
        If OffHand Then
            sim.proc.tryProcs(Procs.ProcOnType.OnOHhit)
        Else
            sim.proc.tryProcs(Procs.ProcOnType.OnMHhit)
            sim.proc.tryProcs(Procs.ProcOnType.OnFU)
            sim.proc.Rime.TryMe(T)
        End If


        If sim.DRW.IsActive(T) Then
            sim.drw.DRWObliterate()
        End If


        Return True
    End Function

    Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double
        tmp = MyBase.AvrgNonCrit(T, target)
        If sim.MainStat.T84PDPS = 1 Then
            tmp = tmp * (1 + 0.125 * target.NumDesease * 1.2)
        Else
            tmp = tmp * (1 + 0.125 * target.NumDesease)
        End If
        If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
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
