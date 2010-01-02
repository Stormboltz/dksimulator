'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:24
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class FrostStrike
	Inherits Strikes.Strike

	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	public Overrides Function isAvailable(T As long) As Boolean
		If sim.glyph.FrostStrike Then
			If sim.SaveRPForRS Then
				If Sim.RunicPower.Value >= 52 Then isAvailable = True
			Else
				If Sim.RunicPower.Value >= 32 Then isAvailable = True
			End If
			
		Else
			If sim.SaveRPForRS Then
				If Sim.RunicPower.Value >= 60 Then isAvailable = True
			Else
				If Sim.RunicPower.Value >= 40 Then isAvailable = True
			End If
			
		end if
	End Function
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		
		Dim RNG As Double
		
		UseGCD(T)
		
		If OffHand = false Then
			If sim.glyph.FrostStrike Then
				Sim.RunicPower.Value = Sim.RunicPower.Value - 32
			Else
				Sim.RunicPower.Value = Sim.RunicPower.Value - 40
			End If
		end if
		
		
		If sim.MainStat.DualW And sim.TalentFrost.ThreatOfThassarian = 3 Then
			if OffHand = false then sim.OHFrostStrike.ApplyDamage(T)
		End If
		
		
		
		If DoMyStrikeHit = false Then
			sim.combatlog.write(T  & vbtab & "FS fail")
			If offhand = false Then sim.proc.KillingMachine.Use
			MissCount = MissCount + 1
			return false
		End If

		Dim ccT As Double
		Dim dégat As Integer
		ccT = CritChance
		RNG = MyRNG
		If RNG < ccT Then
			dégat = AvrgCrit(T)
			sim.combatlog.write(T  & vbtab &  "FS crit for " & dégat & vbtab & "RP left = " & Sim.RunicPower.Value )
			CritCount = CritCount + 1
			sim.tryOnCrit
			totalcrit += dégat
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			totalhit += dégat
			sim.combatlog.write(T  & vbtab &  "FS hit for " & dégat & vbtab & "RP left = " & Sim.RunicPower.Value )
		End If

		total = total + dégat
		If offhand Then
			sim.TryOnOHHitProc
		Else
			sim.TryOnMHHitProc
			sim.proc.KillingMachine.Use
		End If
		Return True

	End Function
	public Overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		If offhand = false Then
			tmp = sim.mainstaT.NormalisedMHDamage*0.55
		Else
			tmp = sim.mainstaT.NormalisedOHDamage*0.55
		End If
		tmp = tmp + 150
		if sim.sigils.VengefulHeart then tmp= tmp + 113
		tmp = tmp * (1+ sim.TalentFrost.BloodoftheNorth * 5 /100)
		If sim.NumDesease > 0 Then 	tmp = tmp * (1 + sim.TalentFrost.GlacierRot * 6.6666666 / 100)
		if (T/sim.MaxTime) >= 0.75 then tmp = tmp *(1+ 0.06*sim.talentfrost.MercilessCombat)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + sim.TalentFrost.BlackIce * 2 / 100)
		tmp = tmp *(1+sim.RuneForge.RazorIceStack/100) 'TODO: only on main target
		if sim.runeforge.CinderglacierProc > 0 then
			tmp = tmp * 1.2
			If offhand = false Then sim.runeforge.CinderglacierProc = sim.runeforge.CinderglacierProc -1
		End If
		If offhand Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		AvrgNonCrit = tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef =  1 * (1 + sim.TalentFrost.GuileOfGorefiend * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.Crit + 8/100 * sim.MainStat.T82PDPS
		if sim.proc.KillingMachine.IsActive()  = true then return 1
	End Function
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHFrostStrike.Total
		TotalHit += sim.OHFrostStrike.TotalHit
		TotalCrit += sim.OHFrostStrike.TotalCrit

		MissCount = (MissCount + sim.OHFrostStrike.MissCount)/2
		HitCount = (HitCount + sim.OHFrostStrike.HitCount)/2
		CritCount = (CritCount + sim.OHFrostStrike.CritCount)/2
		
		sim.OHFrostStrike.Total = 0
		sim.OHFrostStrike.TotalHit = 0
		sim.OHFrostStrike.TotalCrit = 0
	End Sub
End Class
