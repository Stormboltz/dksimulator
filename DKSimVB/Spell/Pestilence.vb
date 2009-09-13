'
' Created by SharpDevelop.
' User: e0030653
' Date: 01/04/2009
' Time: 15:33
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Pestilence
	inherits Spells.Spell

	Friend BPToReapply As Boolean
	Friend FFToReapply As Boolean
	Function use(T As double) As Boolean
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
		If DoMySpellHit = false Then
			combatlog.write(T  & vbtab &  "Pestilence fail")
			MissCount = MissCount +1
			Exit function
		End If
		combatlog.write(T  & vbtab &  "Pestilence")
		HitCount = HitCount +1
		
		If TalentFrost.BloodoftheNorth = 3 Or TalentUnholy.Reaping = 3 Then
			runes.UseBlood(T,True)
		Else
			runes.UseBlood(T,False)
		End If
		RunicPower.add (10)
		
		If glyph.Disease Then
			debug.Print ("PEST! at " & T )
			If sim.BloodPlague.FadeAt > T Then
				sim.BloodPlague.FadeAt = T + 1500 + 300 * talentunholy.Epidemic
				'BloodPlague.nextTick = T + 300
			End If
			If sim.FrostFever.FadeAt > T Then
				sim.FrostFever.FadeAt = T + 1500 + 300 * talentunholy.Epidemic
				'FrostFever.nextTick = T + 300
			End If
		End If
		return true
	End Function
	Function PerfectUsage(T As Double) As Boolean
		Dim tmp1 As Long
		Dim tmp2 As Long
		
		If runes.AnyBlood(T) Then
			tmp1 = math.Min(sim.BloodPlague.FadeAt,sim.FrostFever.FadeAt)
			'debug.Print (RuneState & "time left on disease= " & (tmp1-T)/100 & "s" & " - " & T/100)
			If tmp1 < T Then
				return false
			End If
			
'			debug.Print ("BP = " & BloodPlague.FadeAt)
'			debug.Print ("BP = " & FrostFever.FadeAt)

			If MainStat.AP > math.min(sim.FrostFever.AP, sim.BloodPlague.AP) Then
				BPToReapply = True
				FFToReapply = True
				Return False
			End If

			If sim.BloodPlague.FadeAt <> sim.FrostFever.FadeAt Then
				return true
			End If
			
			If tmp1 - T > 1000 Then Return False
			'debug.Print (RuneState & "time left on disease= " & (tmp1-T)/100 & "s" & " - " & T/100)
			tmp2 = runes.GetNextBloodCD(t)
			'debug.Print (RuneState & "Next blood in " & (tmp2-T)/100 & "s" )
			If tmp2 > tmp1 or tmp2=0 Then
				return true
			End If
		Else
			t=t
		End If
	End Function

End Class

