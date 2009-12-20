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
		MyBase.New(s)
	End Sub

	'A deadly attack that deals 75% weapon damage plus 222.75
	'and heals the Death Knight for a percent of damage done
	'for each of <his/her> diseases on the target.

	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim MHHit As Boolean
		dim OHHit as Boolean
		
		MHHit = true
		OHHit = true
		Dim RNG As Double
		If sim.MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		If sim.MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			If DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab & "MH/OH DS fail")
				MissCount = MissCount + 1
				MHHit = False
				OHHit = false
			End If
		Else
			OHHit = false
			If DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab & "DS fail")
				MissCount = MissCount + 1
				Exit function
			End If
		End If
		If MHHit Or OHHit Then
			
			dim dégat as Integer
			If MHHit Then
				RNG = MyRNG
				If RNG <= CritChance Then
					CritCount = CritCount + 1
					dégat = AvrgCrit(T,true)
					sim.combatlog.write(T  & vbtab &  "DS crit for " & dégat  )
					sim.tryOnCrit
					
				Else
					HitCount = HitCount + 1
					dégat = AvrgNonCrit(T,true)
					sim.combatlog.write(T  & vbtab &  "DS hit for " & dégat )
				End If

				total = total + dégat
				sim.TryOnMHHitProc
			End If
			If OHHit Then
				
				If RNG <= CritChance Then
					
					dégat = AvrgCrit(T,false)
					if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "OH DS crit for " & dégat  )
					sim.tryOnCrit
				Else
					
					dégat = AvrgNonCrit(T,false)
					if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "OH DS hit for " & dégat )
				End If

				total = total + dégat
				sim.TryOnOHHitProc
			End If
			sim.proc.Virulence.TryMe(t)
			
			If talentblood.DRM = 3 Then
				sim.runes.UseFU(T,True)
			Else
				sim.runes.UseFU(T,False)
			End If
			
			If sim.DRW.IsActive(T) Then
				sim.drw.DeathStrike
			End If
			
			Sim.runicpower.add(15 +  2.5*talentunholy.Dirge )
			Sim.runicpower.add(5*sim.MainStat.T74PDPS)
		End If
		return true
	End Function
	public Overrides Function AvrgNonCrit(T as long, MH as Boolean) As Double
		Dim tmp As Double
		
		If MH Then
			tmp = sim.MainStat.NormalisedMHDamage*0.75
		Else
			tmp = sim.MainStat.NormalisedOHDamage*0.75
		End If
		tmp = tmp + 222.75
		if sim.sigils.Awareness then tmp = tmp + 315
		tmp = tmp * (1 + talentblood.ImprovedDeathStrike * 15/100)
		If sim.glyph.DeathStrike Then
			If Sim.RunicPower.Value >= 25 Then
				tmp = tmp * (1 + 25/100)
			Else
				tmp = tmp * (1 + Sim.RunicPower.Value /100)
			End If
		End If
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		
		return tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + TalentBlood.MightofMograine * 15 / 100)
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.crit + talentblood.ImprovedDeathStrike * 3/100 + sim.MainStat.T72PDPS * 5/100
	End Function
	public Overrides Function AvrgCrit(T As Long, MH As Boolean) As Double
		Return AvrgNonCrit(T,MH) * (1 + CritCoef)
	End Function
	
End Class
