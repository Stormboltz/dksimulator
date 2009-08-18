'
' Created by SharpDevelop.
' User: e0030653
' Date: 01/04/2009
' Time: 15:33
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Pestilence
	Friend TotalHit As Long
	Friend HitCount As Integer
	Friend MissCount As Integer
	
	
	Sub init()
		TotalHit = 0
		HitCount = 0
		MissCount = 0
	End Sub
	
	Function use(T As double) As Boolean
		Dim RNG As Double
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
		RNG = Rnd
		If mainstat.SpellHit >= 0.17 Then
			RNG = RNG+0.17
		Else
			RNG = RNG+mainstat.SpellHit
		End If
		If RNG < 0.17 Then
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
			If BloodPlague.FadeAt > T Then
				BloodPlague.FadeAt = T + 1500 + 300 * talentunholy.Epidemic
				'BloodPlague.nextTick = T + 300
			End If
			If FrostFever.FadeAt > T Then
				FrostFever.FadeAt = T + 1500 + 300 * talentunholy.Epidemic
				'FrostFever.nextTick = T + 300
			End If
		End If
		return true
	End Function
	Function PerfectUsage(T As Double) As Boolean
		Dim tmp1 As Long
		Dim tmp2 As Long
		
		If runes.AnyBlood(T) Then
			tmp1 = math.Min(BloodPlague.FadeAt,FrostFever.FadeAt)
			if tmp1 < T then return false
			'if tmp1 - T > 1000 then return false
			'debug.Print (RuneState)
			tmp2 = runes.GetNextBloodCD(t)
			If tmp2 > tmp1 or tmp2=0 Then
				return true
			End If
		End If
	End Function
	Function report As String
		Dim tmp As String
		Dim total As String
		Dim CritCount As Integer
		CritCount = 0
		total = "0"
		tmp = "Pestilence" & VBtab
		
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
End Module
