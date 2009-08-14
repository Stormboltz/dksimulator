'
' Created by SharpDevelop.
' User: e0030653
' Date: 01/04/2009
' Time: 15:33
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Pestilence
	Function use(T As double) As Boolean
		Dim RNG As Double
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
		RNG = RandomNumberGenerator.NextDouble()
		If mainstat.SpellHit >= 0.17 Then
			RNG = RNG+0.17
		Else
			RNG = RNG+mainstat.SpellHit
		End If
		If RNG < 0.17 Then
			combatlog.write(T  & vbtab &  "Pestilence fail")
			Exit function
		End If
		combatlog.write(T  & vbtab &  "Pestilence")
		
		
		If TalentFrost.BloodoftheNorth = 3 Or TalentUnholy.Reaping = 3 Then 
			runes.UseBlood(T,True)
		Else
			runes.UseBlood(T,False)
		End If
		RunicPower.add (10)
		
		If glyph.Disease Then
			If BloodPlague.FadeAt > T Then
				BloodPlague.FadeAt = T + 1500 + 300 * talentunholy.Epidemic
				BloodPlague.nextTick = T + 300
			End If
			If FrostFever.FadeAt > T Then
				FrostFever.FadeAt = T + 1500 + 300 * talentunholy.Epidemic
				FrostFever.nextTick = T + 300
			End If
		End If
		return true
	End Function
	Function PerfectUsage(T As Double) as Boolean
		If Rune1.AvailableTime <= T And _
			(BloodPlague.isActive(Rune2.AvailableTime-150)=false or FrostFever.isActive(Rune2.AvailableTime-150) =false)
			Return True
		End If
		
		If Rune2.AvailableTime <= T And _
			(BloodPlague.isActive(Rune1.AvailableTime-150)=false or FrostFever.isActive(Rune1.AvailableTime-150) =false)
			Return True
		End If
	End Function
End Module
