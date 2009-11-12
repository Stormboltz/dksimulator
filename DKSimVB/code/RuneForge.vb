'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 15:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class RuneForge
	
	Friend OHRazorIce As RazorIce
	Friend MHRazorIce As RazorIce
	Friend MHCinderglacier as Proc
	Friend OHCinderglacier as Proc
	
	Friend RazorIceStack as Integer
	
	
	Friend MHCinderglacierRF as Boolean
	Friend MHRazoriceRF as Boolean
	Friend MHFallenCrusader as Boolean
	Friend OHCinderglacierRF as Boolean
	Friend OHRazoriceRF as Boolean
	Friend OHFallenCrusader As Boolean
	
	Friend CinderglacierProc as Integer
	Friend FallenCrusaderActiveUntil As Long

	
	Friend HitCount as Integer
	Friend MissCount as Integer
	Friend CritCount As Integer
	
	
	Friend OHBerserkingActiveUntil As Long
	Friend OHBerserking as Boolean
	Private Sim As Sim
	
	Sub New(S As Sim )
		Sim = S
		HitCount = 0
		MissCount =0
		CritCount = 0
		FallenCrusaderActiveUntil = 0
		CinderglacierProc = 0
		OHBerserkingActiveUntil = 0
		RazorIceStack = 0
	End Sub
	
	
	
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
		
	Function AreStarsAligned(T As Long) As Boolean
		If sim._MainFrm.chkWaitFC.Checked = False Then Return True
		If sim.proc.MHFallenCrusader.IsActive Or sim.proc.OHFallenCrusader.IsActive Then Return True
		If sim.proc.MHFallenCrusader.Equiped + sim.proc.OHFallenCrusader.Equiped > 0 Then Return False
		return true
	End Function
	
	Sub AddRazorIceStack(T As Long)
		RazorIceStack += 1
		If RazorIceStack > 10 Then RazorIceStack=10
		HitCount += 1
	End Sub
	
	
	
	
End Class
