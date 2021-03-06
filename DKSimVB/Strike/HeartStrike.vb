'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:11
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class HeartStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	public Overrides Function ApplyDamage(T As long) As boolean
		Dim RNG As Double
		
		If sim.Hysteria.IsAvailable(T)  Then sim.Hysteria.use(T)
		
		UseGCD(T)
		
		If DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab &  "HS fail")
			MissCount = MissCount + 1
			Exit function
		End If
		Sim.RunicPower.add (10)
		Dim intCount As Integer = 0
		dim Tar as Targets.Target
		For Each Tar In sim.Targets.AllTargets
			RNG = RngCrit
			Dim d�gat As Integer
			
			If Tar.Equals(sim.Targets.MainTarget) Then
				If RNG <= CritChance Then
					CritCount = CritCount + 1
					d�gat = AvrgCrit(T,tar)
					totalcrit += d�gat
					sim.combatlog.write(T  & vbtab &  "HS crit for " & d�gat)
				Else
					HitCount = HitCount + 1
					d�gat =  AvrgNonCrit(T)
					totalhit += d�gat
					sim.combatlog.write(T  & vbtab &  "HS hit for " & d�gat)
				End If
			ElseIf intCount = 0 Then
				intCount = 1
				If RNG <= CritChance Then
					CritCount = CritCount + 1
					d�gat = AvrgCrit(T,tar)/2
					totalcrit += d�gat
					sim.combatlog.write(T  & vbtab &  "HS crit for " & d�gat)
				Else
					HitCount = HitCount + 1
					d�gat =  AvrgNonCrit(T)/2
					totalhit += d�gat
					sim.combatlog.write(T  & vbtab &  "HS hit for " & d�gat)
				End If
			End If
			total = total + d�gat
			sim.TryOnBloodStrike
			sim.TryOnMHHitProc
		Next
		
		If sim.proc.ReapingBotN.TryMe(T) Then
			sim.Runes.UseBlood(T, True)
		Else
			sim.Runes.UseBlood(T, False)
		End If
		If sim.DRW.IsActive(T) Then
			sim.DRW.DRWHeartStrike
		End If
		
		
		return true
	End Function
	public Overrides Function AvrgNonCrit(T as long,target as Targets.Target) As Double
		Dim tmp As Double
		tmp = sim.MainStat.NormalisedMHDamage * 0.5
		tmp = tmp + 368
		if sim.MainStat.T84PDPS = 1 then
			tmp = tmp * (1 + 0.1 * target.NumDesease * 1.2)
		else
			tmp = tmp * (1 + 0.1 * target.NumDesease)
		end if
		tmp = tmp * (1 + sim.Character.talentblood.BloodyStrikes * 15 / 100)
		tmp = tmp * (1 + sim.Character.talentfrost.BloodoftheNorth * 5 / 100)
		
		if sim.sigils.DarkRider then tmp = tmp + 45 + 22.5 * target.NumDesease
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		
		If sim.MainStat.T102PDPS<>0 Then
			tmp = tmp * 1.07
		End If
		if sim.MainStat.T92PTNK =1 then tmp = tmp *1.05
		AvrgNonCrit = tmp
	End Function
	
	public Overrides Function CritCoef() As Double
		CritCoef = 1* (1 + sim.Character.talentblood.MightofMograine * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.crit + sim.Character.talentblood.Subversion * 3 / 100
	End Function
	public Overrides Function AvrgCrit(T As long,target as Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
End Class
