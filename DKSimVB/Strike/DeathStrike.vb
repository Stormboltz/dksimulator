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
	

	'A deadly attack that deals 75% weapon damage plus 222.75
	'and heals the Death Knight for a percent of damage done
	'for each of <his/her> diseases on the target.

	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim MHHit As Boolean
		dim OHHit as Boolean
		
		MHHit = true
		OHHit = true
		Dim RNG As Double
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		If MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			If DoMyStrikeHit = false Then
				combatlog.write(T  & vbtab & "MH/OH DS fail")
				MissCount = MissCount + 1
				MHHit = False
				OHHit = false
			End If
		Else
			OHHit = false
			If DoMyStrikeHit = false Then
				combatlog.write(T  & vbtab & "DS fail")
				MissCount = MissCount + 1
				Exit function
			End If
		End If
		If MHHit Or OHHit Then
			
			dim dégat as Integer
			If MHHit Then
				RNG = RNGStrike
				If RNG <= CritChance Then
					CritCount = CritCount + 1
					dégat = AvrgCrit(T,true)
					combatlog.write(T  & vbtab &  "DS crit for " & dégat  )
					TryBitterAnguish()
					TryMirror()
					TryPyrite()
					TryOldGod()
					
				Else
					HitCount = HitCount + 1
					dégat = AvrgNonCrit(T,true)
					combatlog.write(T  & vbtab &  "DS hit for " & dégat )
				End If
				If Lissage Then dégat = AvrgCrit(T,true)*CritChance + AvrgNonCrit(T,true)*(1-CritChance )
				total = total + dégat
				TryMHCinderglacier
				TryMHFallenCrusader
				TryMjolRune
				TryGrimToll
				TryGreatness()
				TryDeathChoice()
				TryDCDeath()
				TryVictory()
				TryBandit()
				TryDarkMatter()
				TryComet()
			End If
			If OHHit Then
				
				If RNG <= CritChance Then
					
					dégat = AvrgCrit(T,false)
					combatlog.write(T  & vbtab &  "OH DS crit for " & dégat  )
					TryBitterAnguish()
					TryMirror()
					TryPyrite()
					TryOldGod()
				Else
					
					dégat = AvrgNonCrit(T,false)
					combatlog.write(T  & vbtab &  "OH DS hit for " & dégat )
				End If
				If Lissage Then dégat = AvrgCrit(T,false)*CritChance + AvrgNonCrit(T,false)*(1-CritChance )
				total = total + dégat
				TryOHCinderglacier
				TryOHFallenCrusader
				TryOHBerserking
				TryMjolRune
				TryGrimToll
				TryGreatness()
				TryDeathChoice()
				TryDCDeath()
				TryVictory()
				TryBandit()
				TryDarkMatter()
				TryComet()
			End If
			proc.VirulenceFade = T + 2000
			
			If talentblood.DRM = 3 Then
				runes.UseFU(T,True)
			Else
				runes.UseFU(T,False)
			End If
			
			If DRW.IsActive(T) Then
				drw.DeathStrike
			End If
			
			runicpower.add(15 +  2.5*talentunholy.Dirge )
			runicpower.add(5*SetBonus.T74PDPS)
		End If
		return true
	End Function
	public Overrides Function AvrgNonCrit(T as long, MH as Boolean) As Double
		Dim tmp As Double
		
		If MH Then
			tmp = MainStat.NormalisedMHDamage*0.75
		Else
			tmp = MainStat.NormalisedOHDamage*0.75
		End If
		tmp = tmp + 222.75
		if sigils.Awareness then tmp = tmp + 315
		tmp = tmp * (1 + talentblood.ImprovedDeathStrike * 15/100)
		If glyph.DeathStrike Then
			If RunicPower.Value >= 25 Then
				tmp = tmp * (1 + 25/100)
			Else
				tmp = tmp * (1 + RunicPower.Value /100)
			End If
		End If
		tmp = tmp *MainStat.StandardPhysicalDamageMultiplier(T)
		
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		return tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = 1 * (1 + TalentBlood.MightofMograine * 15 / 100)
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		CritChance = MainStat.crit + talentblood.ImprovedDeathStrike * 3/100 + SetBonus.T72PDPS * 5/100
	End Function
	public Overrides Function AvrgCrit(T As Long, MH As Boolean) As Double
		Return AvrgNonCrit(T,MH) * (1 + CritCoef)
	End Function
	
End Class
