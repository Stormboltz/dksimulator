'
' Created by SharpDevelop.
' User: e0030653
' Date: 01/04/2009
' Time: 15:33
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Pestilence
	Inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub

	Function use(T As double) As Boolean
		UseGCD(T)
		If DoMySpellHit = false Then
			sim.combatlog.write(T  & vbtab &  "Pestilence fail")
			MissCount = MissCount +1
			Exit function
		End If
		sim.combatlog.write(T  & vbtab &  "Pestilence")
		HitCount = HitCount +1
		
		If sim.proc.ReapingBotN.TryMe(T) Then
			sim.Runes.UseBlood(T, True)
		Else
			sim.Runes.UseBlood(T, False)
		End If
		Sim.RunicPower.add (10)
		
		If sim.BloodPlague.FadeAt > T Then
				sim.BloodPlague.OtherTargetsFade  = T + sim.BloodPlague.Lenght
				'BloodPlague.nextTick = T + 300
			End If
		If sim.FrostFever.FadeAt > T Then
			sim.FrostFever.OtherTargetsFade = T + sim.frostfever.Lenght
			'FrostFever.nextTick = T + 300
		End If
	
		
		
		If sim.glyph.Disease Then
			If sim.BloodPlague.FadeAt > T Then
				sim.BloodPlague.Refresh(T)
			End If
			If sim.FrostFever.FadeAt > T Then
				sim.FrostFever.Refresh(T)
			End If
		End If
		return true
	End Function
	Function PerfectUsage(T As Double) As Boolean
		Dim tmp1 As Long
		Dim tmp2 As Long
		
		If sim.runes.AnyBlood(T) Then
			tmp1 = math.Min(sim.BloodPlague.FadeAt,sim.FrostFever.FadeAt)
			If tmp1 < T Then
				return false
			End If
			
			'if sim.BloodPlague.nextTick <> sim.FrostFever.nextTick then return true
			'If tmp1 - T > 1000 Then Return False
			tmp2 = sim.runes.GetNextBloodCD(t)
			If tmp2 > tmp1 or tmp2=0 Then
				return true
			End If
		Else
			t=t
		End If
	End Function

End Class

