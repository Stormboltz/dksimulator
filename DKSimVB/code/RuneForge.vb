'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 15:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class RuneForge
	
	Private OHRazorIce As WeaponProc
	Private MHRazorIce As WeaponProc
	Private MHCinderglacier as WeaponProc
	Private OHCinderglacier as WeaponProc
	Private MHFallenCrusader As WeaponProc
	Private OHFallenCrusader As WeaponProc
	Private OHBerserking As WeaponProc
	
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
		
	End Sub

	Sub Init()
		dim s as Sim
		s = Sim
		MHRuneForge = s.XmlConfig.SelectSingleNode("//config/mh").InnerText
		If s.MainStat.DualW Then
			OHRuneForge = s.XmlConfig.SelectSingleNode("//config/oh").InnerText
		Else
			OHRuneForge = ""
		
		End If
		if MHRuneForge = "FallenCrusader" or OHRuneForge = "FallenCrusader" then
			FallenCrusaderActiveUntil = 0
		Else
			FallenCrusaderActiveUntil = -100
		End If
		
		MHRazorIce = New WeaponProc(s)
		With MHRazorIce
			.InternalCD = 0
			.ProcOn = Procs.ProcOnType.OnMHhit
			.ProcChance = 1
			.ProcLenght = 20
			.DamageType = "Razorice"
			._Name = "Main Hand RazorIce"
			.HasteSensible = True
			If MHRuneForge = "Razorice" Then
				.ProcValue = Sim.MainStat.MHWeaponSpeed * Sim.MainStat.MHWeaponDPS * 0.02
				.Equip()
			End If
		End With
		
		OHRazorIce = New WeaponProc(s)
		With OHRazorIce
			.InternalCD = 0
			.ProcOn = Procs.ProcOnType.OnOHhit
			.ProcChance = 1
			.ProcLenght = 20
			.DamageType = "Razorice"
			._Name = "Off Hand RazorIce"
			.HasteSensible = True
			If OHRuneForge = "Razorice" Then
				.ProcValue = Sim.MainStat.MHWeaponSpeed * Sim.MainStat.MHWeaponDPS * 0.02
				.Equip()
			End If
		End With

		MHFallenCrusader = New WeaponProc(s)
		With MHFallenCrusader
			._Name = "MHFallenCrusader"
			.InternalCD = 0
			.ProcOn = Procs.ProcOnType.OnMHhit
			.ProcChance = 2 * s.MainStat.MHWeaponSpeed / 60
			.ProcLenght = 15
			.ProcValue = 1
			.DamageType = "FallenCrusader"
			If MHRuneForge = "FallenCrusader" Then .Equip()
		End With

		OHFallenCrusader = New WeaponProc(s)
		With OHFallenCrusader
			._Name = "OHFallenCrusader"
			.InternalCD = 0
			.ProcOn = Procs.ProcOnType.OnOHhit
			.ProcChance = 2 * s.MainStat.OHWeaponSpeed / 60
			.ProcLenght = 15
			.ProcValue = 1
			.DamageType = "FallenCrusader"
			If OHRuneForge = "FallenCrusader" Then .Equip()
		End With


		MHCinderglacier = New WeaponProc(s)
		With MHCinderglacier
			._Name = "MHCinderglacier"
			.InternalCD = 0
			.ProcOn = Procs.ProcOnType.OnMHhit
			.ProcChance = 1.5 * s.MainStat.MHWeaponSpeed / 60
			.ProcLenght = 30
			.ProcValue = 2
			.DamageType = "Cinderglacier"
			If MHRuneForge = "Cinderglacier" Then .Equip()
		End With

		OHCinderglacier = New WeaponProc(s)
		With OHCinderglacier
			._Name = "OHCinderglacier"
			.InternalCD = 0
			.ProcChance = 1.5 * s.MainStat.OHWeaponSpeed / 60
			.ProcLenght = 30
			.ProcValue = 2
			.DamageType = "Cinderglacier"
			.ProcOn = Procs.ProcOnType.OnOHhit
			If OHRuneForge = "Cinderglacier" Then .Equip()
		End With

		OHBerserking = New WeaponProc(s)
		With OHBerserking
			._Name = "Berserking"
			.InternalCD = 0
			.ProcOn = Procs.ProcOnType.OnOHhit
			.ProcChance = 1.2 * s.MainStat.OHWeaponSpeed / 60
			.ProcLenght = 15
			.ProcValue = 400
			.ProcType = "ap"
			If OHRuneForge = "Berserking" Then .Equip()
		End With
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
