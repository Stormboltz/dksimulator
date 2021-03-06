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
		Sim.RunicPower.add (10)
		sim.combatlog.write(T  & vbtab &  "Pestilence")
		HitCount = HitCount +1
		
		If sim.proc.ReapingBotN.TryMe(T) Then
			sim.Runes.UseBlood(T, True)
		Else
			sim.Runes.UseBlood(T, False)
		End If
		
		dim Tar as Targets.Target
		If sim.Targets.MainTarget.BloodPlague.FadeAt > T Then
			For Each Tar In sim.Targets.AllTargets
				If Tar.Equals(sim.Targets.MainTarget) = False Then
					Tar.BloodPlague.Apply(T,Tar)
				End If
			Next
		End If
		If sim.Targets.MainTarget.FrostFever.FadeAt > T Then
			For Each Tar In sim.Targets.AllTargets
				If Tar.Equals(sim.Targets.MainTarget) = False Then
					Tar.FrostFever.Apply(T,Tar)
				End If
			Next
		End If
			
		If sim.character.glyph.Disease Then
			If sim.Targets.MainTarget.BloodPlague.FadeAt > T Then
				sim.Targets.MainTarget.BloodPlague.Refresh(T)
			End If
			If sim.Targets.MainTarget.FrostFever.FadeAt > T Then
				sim.Targets.MainTarget.FrostFever.Refresh(T)
			End If
		End If
		
		If sim.KeepBloodSync Then
			If sim.BloodToSync = True Then
				sim.BloodToSync = False
			Else
				sim.BloodToSync = True
			End If
		End If
		
		
		
		
		return true
	End Function
	Function PerfectUsage(T As Double) As Boolean
		Dim tmp1 As Long
		Dim tmp2 As Long
		
		Dim blood As Boolean
		
		If sim.Character.talentunholy.Gargoyle = 1 Then
			blood = sim.runes.BloodOnly(T)
		ElseIf sim.Character.talentfrost.GuileOfGorefiend > 1 Then
			blood = sim.runes.BloodOnly(T)
		Else 
			blood = sim.runes.AnyBlood(T)
		End If
		If blood Then
			tmp1 = math.Min(sim.Targets.MainTarget.BloodPlague.FadeAt,sim.Targets.MainTarget.FrostFever.FadeAt)
			If tmp1 < T Then
				return false
			End If
			
			If sim.Targets.MainTarget.BloodPlague.FadeAt <> sim.Targets.MainTarget.FrostFever.FadeAt Then
				return true
			End If
			If tmp1 - T > 800 Then Return False
			tmp2 = sim.runes.GetNextBloodCD(t)
			If tmp2 > tmp1 or tmp2=0 Then
				return true
			End If
		Else
			t=t
		End If
	End Function
	
End Class

