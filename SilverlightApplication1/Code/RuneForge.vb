'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 15:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class RuneForge
	
	Private RazorIceStack as Integer
	
	Private CinderglacierProc As Integer
	Private WastedCG as Integer
	Friend CGMultiplier As Double
	Friend MHProc As WeaponProc
	Friend OHProc As WeaponProc
	
	Friend FCProc As WeaponProc
	Friend RIProc As WeaponProc
	Friend CGProc As WeaponProc
	
	Friend OHRuneForge As String
	Friend MHRuneForge As String
	
	Friend OHBerserkingActiveUntil As Long
	
	Private Sim As Sim
	
	Sub New(S As Sim )
		Sim = S
		CinderglacierProc = 0
		WastedCG = 0
		OHBerserkingActiveUntil = 0
		CGMultiplier = 1.2
	End Sub

	Sub SoftReset()
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
					FCProc = Proc
					
				Case "Razorice"
					.ProcChance = 1
					RIProc = Proc
					

				Case "Cinderglacier"
					.ProcChance *= 1.5
					CGProc = Proc

				Case "Berzerking"
					.DamageType = ""
					.ProcChance *= 1.2
					.ProcLenght = 15
					.ProcValue = 400
					.ProcType = "ap"
				
				Case Else
                    Diagnostics.Debug.WriteLine("Runeforge: " & RuneForge & " not implemented")
					.ProcChance = 0.0
					Exit Sub
					
			End Select
			._Name &= RuneForge
			
		End With
	End Sub

	Sub Init()
		dim s as Sim
		s = Sim
        MHRuneForge = s.XmlConfig.Element("config").Element("mh").Value
        MHProc = New WeaponProc(s)
        With MHProc
            If s.MainStat.DualW Then ._Name = "MH "
            .InternalCD = 0
            .ProcOn = Procs.ProcOnType.OnMHhit
            .ProcChance = s.MainStat.MHWeaponSpeed / 60
        End With
        ConfigRuneForgeProc(MHProc, MHRuneForge)
        If MHProc.ProcChance > 0.0 Then MHProc.Equip()

        If s.MainStat.DualW Then
            OHProc = New WeaponProc(s)
            OHRuneForge = s.XmlConfig.Element("config").Element("oh").Value
            With OHProc
                ._Name = "OH "
                .InternalCD = 0
                .ProcOn = Procs.ProcOnType.OnOHhit
                .ProcChance = s.MainStat.OHWeaponSpeed / 60
            End With
            ConfigRuneForgeProc(OHProc, OHRuneForge)
            If OHProc.ProcChance > 0.0 Then
                OHProc.Equip()

                If MHRuneForge = OHRuneForge Then
                    Dim Proc As New WeaponProc(s)
                    ConfigRuneForgeProc(Proc, MHRuneForge)
                    Proc._Name &= " (Combined)"
                    Proc.Equip()
                End If
            End If
        Else
            OHRuneForge = ""



        End If
		If RIProc IsNot Nothing Then
			With RIProc
				.ProcLenght = 20
				.ProcValue = Sim.MainStat.MHWeaponSpeed * Sim.MainStat.MHWeaponDPS * 0.02
				.MaxStack = 5
				.ProcValueStack = 2
			End With
		End If
		
		If CGProc IsNot Nothing Then
			With CGProc
				.ProcLenght = 30
				.ProcValue = 20
			End With
		End If

		
End Sub

	Sub ProcFallenCrusader(Proc As WeaponProc, T as Long)
		Proc.BaseApplyMe(T)
		if FCProc IsNot Proc then FCProc.ApplyFade(T)
	End Sub


	Sub ProcCinderglacier(Proc As WeaponProc, T As Long)
		Proc.BaseApplyMe(T)
		If Proc IsNot CGProc Then CGProc.ApplyFade(T)
		CGProc.Count = 2
	End Sub
	
	
	Function CheckFallenCrusader() As Boolean
		If FCProc Is Nothing Then Return False
		return FCProc.IsActive()
		
	End Function
	
	Function HasFallenCrusader() As Boolean
		return FCProc IsNot Nothing
		
	End Function
	
	
	Function CheckCinderglacier(consume As Boolean) As Integer
		If CGProc Is Nothing Then Return 0
		dim rv as Integer
		rv = CGProc.Count
		if consume Then CGProc.Use()
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
	
	Function RazorIceMultiplier(T As Long) as Double
		If RIProc Is Nothing Then Return 1.0
		Return 1.0 + 0.02 * RIProc.Stack
	End Function
	
	Sub ProcRazorIce(Proc As WeaponProc, T As Long)
		Dim tmp As Double
		tmp = RIProc.procvalue
		'Hastebonus should only be applied to procs from hasteable attacks
		'If sim.EPStat = "EP HasteEstimated" Then
		'	tmp *= sim.MainStat.EstimatedHasteBonus
		'End If
		RIProc.ApplyFade(T)
		
		if RIProc.Stack < RIProc.MaxStack then RIProc.Stack += 1

		With Proc
			If Proc IsNot RIProc Then .HitCount += 1
			If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & .ToString & " proc")
			.total += tmp
		End With
	End Sub
	
	
	
	
End Class
