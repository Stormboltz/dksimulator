'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 15:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class RuneForge
	
	Friend RazorIceStack as Integer
	
	Private CinderglacierProc As Integer
	Private WastedCG as Integer
	
	Friend FallenCrusaderActiveUntil As Long

	Friend MHProc As WeaponProc
	Friend OHProc As WeaponProc
	
	Friend OHRuneForge As String
	Friend MHRuneForge As String
	
	Friend OHBerserkingActiveUntil As Long
	
	Private Sim As Sim
	
	Sub New(S As Sim )
		Sim = S
		CinderglacierProc = 0
		WastedCG = 0
		OHBerserkingActiveUntil = 0
		RazorIceStack = 0
		FallenCrusaderActiveUntil = -100
	End Sub


	Sub ConfigRuneForgeProc(Proc As WeaponProc, RuneForge As String)
		If RuneForge = "" Then Exit Sub
		With Proc
			.DamageType = RuneForge
			Select Case RuneForge

				Case "FallenCrusader"
					.ProcChance *= 2
					.ProcLenght = 15
					.ProcValue = 1
					FallenCrusaderActiveUntil = 0
					
				Case "Razorice"
					.ProcChance = 1
					.ProcLenght = 20
					.HasteSensible = True
					.ProcValue = Sim.MainStat.MHWeaponSpeed * Sim.MainStat.MHWeaponDPS * 0.02

				Case "Cinderglacier"
					.ProcChance *= 1.5
					.ProcLenght = 30
					.ProcValue = 2

				Case "Berzerking"
					.DamageType = ""
					.ProcChance *= 1.2
					.ProcLenght = 15
					.ProcValue = 400
					.ProcType = "ap"
				
				Case Else
					debug.Print ( "Runeforge: " & RuneForge & " not implemented")
					Exit Sub
					
			End Select
			._Name = RuneForge
			.Equip()
			
		End With
	End Sub

	Sub Init()
		dim s as Sim
		s = Sim
		MHRuneForge = s.XmlConfig.SelectSingleNode("//config/mh").InnerText
		MHProc = New WeaponProc(s)
		With MHProc
			.InternalCD = 0
			.ProcOn = Procs.ProcOnType.OnMHhit
			.ProcChance = s.MainStat.MHWeaponSpeed / 60
		End With
		ConfigRuneForgeProc(MHProc, MHRuneForge)

		
		If s.MainStat.DualW Then
			OHProc = New WeaponProc(s)
			OHRuneForge = s.XmlConfig.SelectSingleNode("//config/oh").InnerText
			With OHProc
				.InternalCD = 0
				.ProcOn = Procs.ProcOnType.OnOHhit
				.ProcChance = s.MainStat.OHWeaponSpeed / 60
			End With
			ConfigRuneForgeProc(OHProc, OHRuneForge)
			MHProc._Name = "MH " & MHProc._Name
			OHProc._Name = "OH " & OHProc._Name
			
		Else
			OHRuneForge = ""
		
		End If
End Sub

	Sub ProcFallenCrusader(Fade As Long)
		FallenCrusaderActiveUntil = Fade
	End Sub


	Sub ProcCinderglacier()
		WastedCG += CinderglacierProc
		CinderglacierProc = 2
		
	End Sub
	
	
	
	
	Function CheckFallenCrusader() As Boolean
		return FallenCrusaderActiveUntil >= sim.TimeStamp
		
	End Function
	
	Function HasFallenCrusader() As Boolean
		return FallenCrusaderActiveUntil >= 0
		
	End Function
	
	
	Function CheckCinderglacier(consume As Boolean) As Integer
		Dim rv As Integer
		rv = CinderglacierProc
		If consume Then
			if CinderglacierProc > 0 then CinderglacierProc -=1
		End If
		return rv
	End Function

	Function AreStarsAligned(T As Long) As Boolean
		If sim.WaitForFallenCrusader = False Then Return True
		If checkFallenCrusader() Then Return True
		If HasFallenCrusader() Then Return False
		return true
'		If sim.MainStat.AP >=  sim.MainStat.GetMaxAP * 0.7 Then
'			Return True
'		Else
'			return false
'		End If
	End Function
	
	Sub ProcRazorIce()
		RazorIceStack += 1
			If RazorIceStack > 5 Then RazorIceStack=5
	End Sub
	
	
	
	
End Class
