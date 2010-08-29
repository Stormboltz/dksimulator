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
        MyBase.New(S)
        BaseDamage = 222.75
        If sim.Sigils.Awareness Then BaseDamage = BaseDamage + 445.5
        Coeficient = 1.5
        Multiplicator = 1 + sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 15 / 100
        SpecialCritChance = sim.Character.Talents.Talent("ImprovedDeathStrike").Value * 3 / 100 + sim.MainStat.T72PDPS * 5 / 100
    End Sub

	'A deadly attack that deals 75% weapon damage plus 222.75
	'and heals the Death Knight for a percent of damage done
	'for each of <his/her> diseases on the target.

	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double

		If OffHand = False Then
			UseGCD(T)
			If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHDeathStrike.ApplyDamage(T)
			If DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab & "DS fail")
				MissCount = MissCount + 1
				return false
			End If
		Else
            If DoMyToTHit() = False Then Return False
		End If
		
		If offhand = False Then 
            sim.RunicPower.add(15 + 5 * sim.Character.Talents.Talent("Dirge").Value)
			Sim.runicpower.add(5*sim.MainStat.T74PDPS)
		End If
		

        RNG = RngCrit
        If RNG <= CritChance Then
            CritCount = CritCount + 1
            LastDamage = AvrgCrit(T)
            sim.combatlog.write(T & vbtab & "DS crit for " & LastDamage)
            sim.proc.tryProcs(Procs.ProcOnType.OnCrit)

            totalcrit += LastDamage
        Else
            HitCount = HitCount + 1
            LastDamage = AvrgNonCrit(T)
            totalhit += LastDamage
            sim.combatlog.write(T & vbtab & "DS hit for " & LastDamage)
        End If
        total = total + LastDamage
		If OffHand = False Then
            sim.proc.tryProcs(Procs.ProcOnType.OnMHhit)
			If sim.proc.DRM.TryMe(T) Then
				sim.Runes.UseFU(T, True)
			Else
				sim.Runes.UseFU(T, False)
			End If
			
            sim.proc.tryProcs(Procs.ProcOnType.OnFU)
		Else
            sim.proc.tryProcs(Procs.ProcOnType.OnOHhit)
		End If
		
		If sim.DRW.IsActive(T) Then
			sim.drw.DRWDeathStrike
		End If
		return true
	End Function
	public Overrides Function AvrgNonCrit(T as long,target As Targets.Target) As Double
		Dim tmp As Double
        tmp = MyBase.AvrgNonCrit(T, target)
        If sim.Character.Glyph.DeathStrike Then
            tmp = tmp * (1 + Math.Max(sim.RunicPower.GetValue, 25) / 100)
        End If
        Return tmp
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
