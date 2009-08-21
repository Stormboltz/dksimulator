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
			debug.Print ("PEST! at " & T )
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
			debug.Print (RuneState & "time left on disease= " & (tmp1-T)/100 & "s" & " - " & T/100)
			If tmp1 < T Then
				return false
			End If
			
'			debug.Print ("BP = " & BloodPlague.FadeAt)
'			debug.Print ("BP = " & FrostFever.FadeAt)
			If BloodPlague.FadeAt <> FrostFever.FadeAt Then
				return true
			End If
			
			If tmp1 - T > 1000 Then Return False
			'debug.Print (RuneState & "time left on disease= " & (tmp1-T)/100 & "s" & " - " & T/100)
			tmp2 = runes.GetNextBloodCD(t)
			debug.Print (RuneState & "Next blood in " & (tmp2-T)/100 & "s" )
			If tmp2 > tmp1 or tmp2=0 Then
				return true
			End If
		Else
			t=t
		End If
	End Function
	Function CanUseGCD(T As Long) As Boolean
		CanUseGCD=true
		If glyph.Disease Then 
			dim tGDC as long
			'return false
			If MainStat.UnholyPresence Then
				tGDC = 100+ sim._MainFrm.txtLatency.Text/10 + 50
			Else
				tGDC =  150+ sim._MainFrm.txtLatency.Text/10 + 50
			End If
			
			If math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) < (T +  tGDC) Then 
				'debug.Print (RuneState & "time left on disease= " & (math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) -T)/100 & "s" & " - " & T/100)
				return false
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
