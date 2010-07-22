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
		If sim.character.glyph.FrostStrike Then
			isAvailable = Sim.RunicPower.CheckRS(32)
		Else
			isAvailable = Sim.RunicPower.CheckRS(40)
		end if
	End Function
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		If OffHand = False Then
			UseGCD(T)
			If sim.character.glyph.FrostStrike Then
				Sim.RunicPower.Use(32)
			Else
				Sim.RunicPower.Use(40)
			End If
			If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHFrostStrike.ApplyDamage(T)
			If DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab & "FS fail")
				sim.proc.KillingMachine.Use
				If sim.character.glyph.FrostStrike Then
					Sim.RunicPower.add(29)
				Else
					Sim.RunicPower.add(36)
				End If
				MissCount = MissCount + 1
				Return False
			End If
		Else
            If DoMyToTHit() = False Then Return False
		End If
		Dim ccT As Double
		Dim dégat As Integer
		ccT = CritChance
		RNG = RngCrit
		If RNG < ccT Then
			dégat = AvrgCrit(T)
			sim.combatlog.write(T  & vbtab &  "FS crit for " & dégat)
			CritCount = CritCount + 1
			sim.tryOnCrit
			totalcrit += dégat
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			totalhit += dégat
			sim.combatlog.write(T  & vbtab &  "FS hit for " & dégat)
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
	Public Function AvrgNonCrit(T As Long,Optional target As Targets.Target = Nothing) As Double
		if target is nothing then target = sim.Targets.MainTarget
		Dim tmp As Double
		If offhand = false Then
            tmp = sim.MainStat.NormalisedMHDamage * 1.1
		Else
            tmp = sim.MainStat.NormalisedOHDamage * 1.1

		End If
        tmp = tmp + 275
		if sim.sigils.VengefulHeart then tmp= tmp + 113
		tmp = tmp * (1+ sim.Character.talentfrost.BloodoftheNorth * 5 /100)
		if target.NumDesease > 0 or (target.Debuff.BloodPlague+target.Debuff.FrostFever>0) Then 	tmp = tmp * (1 + sim.Character.talentfrost.GlacierRot * 6.6666666 / 100)
		if sim.ExecuteRange then tmp = tmp *(1+ 0.06*sim.Character.talentfrost.MercilessCombat)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + sim.Character.talentfrost.BlackIce * 2 / 100)
		tmp *= sim.RuneForge.RazorIceMultiplier(T)
		If sim.RuneForge.CheckCinderglacier(offhand) > 0 then tmp *= 1.2
		If offhand Then
			tmp = tmp * 0.5
				tmp = tmp * (1 + sim.Character.talentfrost.NervesofColdSteel * 8.3333 / 100)
		End If
        Return tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef =  1 * (1 + sim.Character.talentfrost.GuileOfGorefiend * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.Crit + 8/100 * sim.MainStat.T82PDPS
		if sim.proc.KillingMachine.IsActive()  = true then return 1
	End Function
	public Overrides Function AvrgCrit(T As long,target As Targets.Target) As Double
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
