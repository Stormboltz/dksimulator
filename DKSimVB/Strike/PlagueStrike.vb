Friend Class PlagueStrike
	Inherits Strikes.Strike
		
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim MHHit As Boolean
		Dim OHHit As Boolean
		MHHit = True
		OHHit = True
		
		
		Dim RNG As Double
		If MainStat.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
		Else
			Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
		End If
		
		
		If MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			'MH
			If DoMyStrikeHit = false Then
				MissCount = MissCount + 1
				combatlog.write(T  & vbtab &  "MH PS fail")
				MHHit = False
				OHHit=false
			End If
		Else
			OHHit = false
			If DoMyStrikeHit = false Then
				MissCount = MissCount + 1
				combatlog.write(T  & vbtab &  "PS fail")
				Exit function
			End If
		End If
		
		if sigils.Strife then
			StrifeFade = T+1000
		End If
		
		Dim d�gat As Integer
		
		If MHHit Or OHHit Then
			RNG = RNGStrike
			If MHHit Then
				RNG = RNGStrike
				If RNG <= CritChance Then
					CritCount = CritCount + 1
					d�gat = AvrgCrit(T,true)
					combatlog.write(T  & vbtab &  "PS crit for " & d�gat  )
					TryBitterAnguish()
					TryMirror()
					TryPyrite()
					TryOldGod()
					
				Else
					HitCount = HitCount + 1
					d�gat = AvrgNonCrit(T,true)
					combatlog.write(T  & vbtab &  "PS hit for " & d�gat )
				End If
				if Lissage then d�gat = AvrgCrit(T,true)*CritChance + AvrgNonCrit(T,true)*(1-CritChance )
				total = total + d�gat
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
					d�gat = AvrgCrit(T,false)
					combatlog.write(T  & vbtab &  "OH PS crit for " & d�gat  )
					TryBitterAnguish()
					TryMirror()
					TryPyrite()
					TryOldGod()
					
				Else
					d�gat = AvrgNonCrit(T,false)
					combatlog.write(T  & vbtab &  "OH PS hit for " & d�gat )
				End If
				if Lissage then d�gat = AvrgCrit(T,false)*CritChance + AvrgNonCrit(T,false)*(1-CritChance )
				total = total + d�gat
				TryOHCinderglacier
				TryOHBerserking
				TryOHFallenCrusader
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
			
			
			runes.UseUnholy(T,False)
			sim.BloodPlague.Apply(T)
			
			
			If DRW.IsActive(T) Then
				drw.PlagueStrike
			End If
			RunicPower.add (10 + TalentUnholy.Dirge * 2.5)
			
			
			
			Return True
		End If
	End Function
	public Overrides Function AvrgNonCrit(T As long, MH as Boolean) As Double
		Dim tmp As Double
		
		If MH Then
			tmp = MainStat.NormalisedMHDamage * 0.5
		Else
			tmp = MainStat.NormalisedOHDamage * 0.5
		End If
		
		tmp = tmp + 189
		tmp = tmp * MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentUnholy.Outbreak * 10 / 100)
		If glyph.PlagueStrike Then tmp = tmp * (1.2)
		If MH=false Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		AvrgNonCrit = tmp
	End Function
	public Overrides Function CritCoef() As Double
		CritCoef = (1 + TalentUnholy.ViciousStrikes * 15 / 100)
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	public Overrides Function CritChance() As Double
		Dim tmp As Double
		
		tmp = MainStat.crit + TalentUnholy.ViciousStrikes * 3 / 100 + T72PTNK*0.1
		
		return tmp
	End Function
	public Overrides Function AvrgCrit(T As long, MH as Boolean) As Double
		AvrgCrit = AvrgNonCrit(T, MH) * (1 + CritCoef)
	End Function
	
end Class