'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 15:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module RuneForge
	
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
	Friend CritCount as Integer
	
	
	Sub init
		HitCount = 0
		MissCount =0
		CritCount = 0
		FallenCrusaderActiveUntil = 0
		CinderglacierProc = 0
		RazoriceProc = 0
		RazoriceTotal = 0
	End Sub
	
	Function applyRazorice() as Boolean
		Dim tmp As Double
		tmp = MainStat.MHWeaponDPS * MainStat.MHWeaponSpeed
		tmp  = tmp * 0.02
'		tmp  = tmp  * mainstat.StandardMagicalDamageMultiplier(sim.TimeStamp)
'		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
'		tmp = tmp * 1.05
		HitCount = HitCount +1
		if combatlog.LogDetails then CombatLog.write(sim.TimeStamp & vbtab & "Razorice hit for " & tmp)
		RazoriceTotal = RazoriceTotal + tmp
	End Function
	
	
	Sub TryMHFallenCrusader()
		Dim RNG As Double
		If MHFallenCrusader Then
			RNG = RandomNumberGenerator.NextDouble()
			If RNG < 2*MainStat.MHWeaponSpeed/60 Then
				FallenCrusaderActiveUntil = sim.TimeStamp + 15 * 100
				if combatlog.LogDetails then CombatLog.write(sim.TimeStamp & vbtab & "Fallen Crusader proc on Main hand")
			End If
		End If
	End Sub
	Sub TryOHFallenCrusader()
		Dim RNG As Double
		If OHFallenCrusader Then
			RNG = RandomNumberGenerator.NextDouble()
			If RNG < 2*MainStat.OHWeaponSpeed/60 Then
				FallenCrusaderActiveUntil = sim.TimeStamp + 15 * 100
				if combatlog.LogDetails then CombatLog.write(sim.TimeStamp & vbtab & "Fallen Crusader proc on Off hand")
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
		
		Dim RNG As Double
		if MHCinderglacier then
			
			RNG = RandomNumberGenerator.NextDouble()
			If RNG < 1*MainStat.MHWeaponSpeed/60 Then
				CinderglacierProc = 2
				if combatlog.LogDetails then CombatLog.write(sim.TimeStamp & vbtab & "Cinderglacier proc on Main hand")
			End If
		end if
	End Sub
	Sub TryOHCinderglacier()
		Dim RNG As Double
		if OHCinderglacier then
			RNG = RandomNumberGenerator.NextDouble()
			If RNG < 1*MainStat.OHWeaponSpeed/60 Then
				CinderglacierProc = 2
				if combatlog.LogDetails then CombatLog.write(sim.TimeStamp & vbtab & "Cinderglacier proc on Off hand")
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
		tmp = tmp & int(100*RazoriceTotal/sim.TotalDamage) & VBtab
		tmp = tmp & int(HitCount+CritCount) & VBtab
		tmp = tmp & int(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(RazoriceTotal/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	
	Function AreStarsAligned(T As Long) As Boolean
		If sim._MainFrm.chkWaitFC.Checked = False Then Return True
		If RuneForge.FallenCrusaderProc = 1 Then Return True
		If RuneForge.MHFallenCrusader or (MainStat.DualW and RuneForge.OHFallenCrusader) Then
			return false
		End If
		return true
	End Function
End Module
