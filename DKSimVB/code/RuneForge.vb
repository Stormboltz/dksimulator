'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 15:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class RuneForge
	
	Friend MHCinderglacier as Boolean
	Friend MHRazorice as Boolean
	Friend MHFallenCrusader as Boolean
	Friend OHCinderglacier as Boolean
	Friend OHRazorice as Boolean
	Friend OHFallenCrusader As Boolean
	
	Friend CinderglacierProc as Integer
	Friend RazoriceProc  as Integer
	Friend FallenCrusaderActiveUntil As Long
	Friend RazoriceTotal As Long
	
	Friend HitCount as Integer
	Friend MissCount as Integer
	Friend CritCount As Integer
	
	
	Friend OHBerserkingActiveUntil As Long
	Friend OHBerserking as Boolean
	Private Sim as Sim
	Sub New(S As Sim )
		Sim = S
		HitCount = 0
		MissCount =0
		CritCount = 0
		FallenCrusaderActiveUntil = 0
		CinderglacierProc = 0
		RazoriceProc = 0
		RazoriceTotal = 0
		OHBerserkingActiveUntil = 0
	End Sub
	
	Function applyRazorice() as Boolean
		Dim tmp As Double
		tmp = sim.MainStat.MHWeaponDPS * sim.MainStat.MHWeaponSpeed
		tmp  = tmp * 0.02
'		tmp  = tmp  * mainstat.StandardMagicalDamageMultiplier(sim.TimeStamp)
'		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
'		tmp = tmp * 1.05
		HitCount = HitCount +1
		if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp & vbtab & "Razorice hit for " & tmp)
		RazoriceTotal = RazoriceTotal + tmp
	End Function
	
	Sub TryMHFallenCrusader()
		If MHFallenCrusader Then
			If sim.RandomNumberGenerator.RNGProc < 2*sim.MainStat.MHWeaponSpeed/60 Then
				FallenCrusaderActiveUntil = sim.TimeStamp + 15 * 100
				if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp & vbtab & "Fallen Crusader sim.proc on Main hand")
			End If
		End If
	End Sub
	Sub TryOHFallenCrusader()
		If OHFallenCrusader Then
			If sim.RandomNumberGenerator.RNGProc < 2*sim.MainStat.OHWeaponSpeed/60 Then
				FallenCrusaderActiveUntil = sim.TimeStamp + 15 * 100
				if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp & vbtab & "Fallen Crusader sim.proc on Off hand")
			End If
		End If
	End Sub
	Sub TryOHBerserking()
		If OHBerserking Then
			If sim.RandomNumberGenerator.RNGProc < 1.2*sim.MainStat.OHWeaponSpeed/60 Then
				OHBerserkingActiveUntil = sim.TimeStamp + 15 * 100
				if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp & vbtab & "Berserking sim.proc on Off hand")
			End If
		End If
	End Sub
	Function FallenCrusaderProc As Integer
		If FallenCrusaderActiveUntil >= sim.TimeStamp Then
			Return 1
		Else
			Return 0
		end if
	End Function
	Sub TryMHCinderglacier()
		if MHCinderglacier then
			
			If sim.RandomNumberGenerator.RNGProc < 1*sim.MainStat.MHWeaponSpeed/60 Then
				CinderglacierProc = 2
				if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp & vbtab & "Cinderglacier sim.proc on Main hand")
			End If
		end if
	End Sub
	Sub TryOHCinderglacier()
		if OHCinderglacier then
			If sim.RandomNumberGenerator.RNGProc < 1*sim.MainStat.OHWeaponSpeed/60 Then
				CinderglacierProc = 2
				if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp & vbtab & "Cinderglacier sim.proc on Off hand")
			End If
		end if
	End Sub
	Function Razoricereport As String
		dim tmp as String
		tmp = "Raz" & VBtab
		
		If RazoriceTotal.ToString().Length < 8 Then
			tmp = tmp & RazoriceTotal & "   " & VBtab
		Else
			tmp = tmp & RazoriceTotal & VBtab
		End If
		
		tmp = tmp & toDecimal(100*RazoriceTotal/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(RazoriceTotal/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	
	Function AreStarsAligned(T As Long) As Boolean
		If sim._MainFrm.chkWaitFC.Checked = False Then Return True
		If Sim.RuneForge.FallenCrusaderProc = 1 Then Return True
		If Sim.RuneForge.MHFallenCrusader or (sim.MainStat.DualW and Sim.RuneForge.OHFallenCrusader) Then
			return false
		End If
		return true
	End Function
End Class
