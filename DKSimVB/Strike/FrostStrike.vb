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
		MyBase.New()
		Sim = S
	End Sub
	
	public Overrides Function isAvailable(T As long) As Boolean
		if sim.glyph.FrostStrike then
			If Sim.RunicPower.Value >= 32 Then isAvailable = True
		Else
			If Sim.RunicPower.Value >= 40 Then isAvailable = True
		end if
	End Function
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		
		Dim RNG As Double
		If sim.MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		
		If sim.glyph.FrostStrike Then
			Sim.RunicPower.Value = Sim.RunicPower.Value - 32
		Else
			Sim.RunicPower.Value = Sim.RunicPower.Value - 40
		End If
		
		Dim MHHit As Boolean
		Dim OHHit As Boolean
		MHHit = True
		OHHit = True
		
		If sim.MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			If DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab & "MH FS fail")
				MissCount = MissCount + 1
				MHHit = False
				OHHit = false
			End If
		Else
			OHHit = false
			If DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab & "FS fail")
				sim.proc.KillingMachine  = False
				MissCount = MissCount + 1
				Exit function
			End If
		End If
		
		Dim ccT As Double
		Dim dégat As Integer
		ccT = CritChance
		If MHHit Or OHHit Then
			If MHHit Then
				RNG = MyRNG
				If RNG < ccT Then
					dégat = AvrgCrit(T,true)
					sim.combatlog.write(T  & vbtab &  "FS crit for " & dégat )
					CritCount = CritCount + 1
					sim.tryOnCrit					
				Else
					dégat = AvrgNonCrit(T,true)
					HitCount = HitCount + 1
					sim.combatlog.write(T  & vbtab &  "FS hit for " & dégat )
				End If

				total = total + dégat
				sim.TryOnMHHitProc
				
			End If
			If OHHit Then
				If RNG < ccT Then
					dégat = AvrgCrit(T,false)
					sim.combatlog.write(T  & vbtab &  "OH FS crit for " & dégat )
					sim.tryOnCrit					
				Else
					dégat = AvrgNonCrit(T,false)
					sim.combatlog.write(T  & vbtab &  "OH FS hit for " & dégat )
				End If

				total = total + dégat
				sim.TryOnOHHitProc
			End If
			sim.proc.KillingMachine  = False
			Return True
		End If
	End Function
	public Overrides Function AvrgNonCrit(T As long, MH as Boolean) As Double
		Dim tmp As Double
		If MH Then
			tmp = sim.mainstaT.NormalisedMHDamage*0.55
		Else
			tmp = sim.mainstaT.NormalisedOHDamage*0.55
		End If
		
		tmp = tmp + 150
		if sim.sigils.VengefulHeart then tmp= tmp + 113
		tmp = tmp * (1+ talentfrost.BloodoftheNorth * 5 /100)
		If sim.NumDesease > 0 Then 	tmp = tmp * (1 + TalentFrost.GlacierRot * 6.6666666 / 100)
		if (T/sim.MaxTime) >= 0.75 then tmp = tmp *(1+ 0.06*talentfrost.MercilessCombat)
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		if sim.runeforge.MHRazorice or (sim.runeforge.OHRazorice and sim.mainstat.DualW)  then tmp = tmp *1.10
		if sim.runeforge.CinderglacierProc > 0 then
			tmp = tmp * 1.2
			sim.runeforge.CinderglacierProc = sim.runeforge.CinderglacierProc -1
		End If
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		AvrgNonCrit = tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef =  1 * (1 + Talentfrost.GuileOfGorefiend * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.Crit + 8/100 * sim.MainStat.T82PDPS
		if sim.proc.KillingMachine  = true then return 1
	End Function
	public Overrides Function AvrgCrit(T As long, MH as Boolean) As Double
		AvrgCrit = AvrgNonCrit(T,MH) * (1 + CritCoef)
	End Function

End Class
