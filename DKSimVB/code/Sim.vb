Friend Module Sim
	Friend TotalDamage As Long
	Friend NextFreeGCD As Long
	Friend Lag As Long
	Friend TimeStamp As Long
	Friend EPStat As String
	Friend EPBase as Integer
	Private DPS As Long
	Friend MaxTime As Long
	Friend RotationStep as Integer
	Friend Rotate as boolean
	Friend rotationPath As String
	Friend PetFriendly as Boolean
	Private SimStart as Date
	Friend _MainFrm As MainForm
	Friend ReportPath As String
	Friend Threat as Long
	Friend Lissage As Boolean
	Private AMSTimer As Long
	Private AMSCd As Integer
	Private AMSAmount As Integer
	Private ShowDpsTimer As Long
	Private InterruptTimer As Long
	Private InterruptAmount As Integer
	Private InterruptCd As Integer
	Friend KeepRNGSeed as Boolean
	
	
	
	
	Function NumDesease() As Integer
		NumDesease = 0
		If BloodPlague.isActive(Sim.TimeStamp) Then NumDesease = NumDesease + 1
		If FrostFever.isActive(Sim.TimeStamp) Then NumDesease = NumDesease + 1
		If (TalentUnholy.EbonPlaguebringer + TalentUnholy.CryptFever >= 1) And NumDesease >= 1 Then NumDesease = NumDesease + 1
		
	End Function
	
	Sub StartEP(pb As ProgressBar,EPCalc As Boolean,SimTime As Integer,MainFrm As MainForm)
		'StartEPAlternative(pb,EPCalc,SimTime,MainFrm)
		'exit sub
		
		
		_MainFrm = MainFrm
		dim sReport as String
		If EPCalc = False Then
			Start(pb,SimTime,MainFrm)
			exit sub
		End If
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		
		Dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(_MainFrm.GetFilePath(_MainFrm.cmbCharacter.Text) )
		
		'Fixed EP base value for now
		EPBase = 50
		'int32.Parse(XmlDoc.SelectSingleNode("//character/EP/base").InnerText)
		
		Dim BaseDPS As long
		Dim APDPS As Long
		Dim tmp1 As double
		Dim tmp2 As double
		
		If SimTime = 0 Then SimTime =1
		'Create EP table
		sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"
		
		If  doc.SelectSingleNode("//config/Stats").InnerText.Contains("True")=false Then
			goto skipStats
		End If
		
		'Dry run
		EPStat="DryRun"
		Start(pb,SimTime,MainFrm)
		BaseDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		'AP
		EPStat="AttackPower"
		Start(pb,SimTime,MainFrm)
		APDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & APDPS)
		sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | 100</td></tr>")
		
		'Str
		if doc.SelectSingleNode("//config/Stats/chkEPStr").InnerText = "True" then
			EPStat="Strength"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for Strength | 0")
		End If
		
		'Agi
		if doc.SelectSingleNode("//config/Stats/chkEPAgility").InnerText = "True" then
			EPStat="Agility"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS )
		Else
			WriteReport ("Average for Agility | 0")
		End If
		
		'Crit
		if doc.SelectSingleNode("//config/Stats/chkEPCrit").InnerText = "True" then
			EPStat="CritRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for CritRating | 0")
		End If
		
		'Haste
		if doc.SelectSingleNode("//config/Stats/chkEPHaste").InnerText = "True" then
			EPStat="HasteRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for HasteRating | 0")
		End If
		
		'Arp
		if doc.SelectSingleNode("//config/Stats/chkEPArP").InnerText = "True" then
			EPStat="ArmorPenetrationRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for ArmorPenetrationRating | 0")
		End If
		
		'Exp
		if doc.SelectSingleNode("//config/Stats/chkEPExp").InnerText = "True" then
			EPStat="ExpertiseRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (-100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & (BaseDPS-DPS)+DPS)
		Else
			WriteReport ("Average for ExpertiseRating | 0")
		End If
		
		'Hit
		if doc.SelectSingleNode("//config/Stats/chkEPHit").InnerText = "True" then
			EPStat="HitRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (-100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & (BaseDPS-DPS)+DPS)
		Else
			WriteReport ("Average for HitRating | 0")
		End If
		
		'SpHit
		if doc.SelectSingleNode("//config/Stats/chkEPSpHit").InnerText = "True" then
			EPStat="SpellHitRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for SpellHitRating | 0")
		End If
		
		'DPS
		if doc.SelectSingleNode("//config/Stats/chkEPSMHDPS").InnerText = "True" then
			EPStat="WeaponDPS"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 10
			sReport = sReport +  ("<tr><td>EP:" & "10" & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for WeaponDPS | 0")
		End If
		
		'Speed
		if doc.SelectSingleNode("//config/Stats/chkEPSMHSpeed").InnerText = "True" then
			EPStat="WeaponSpeed"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 0.1
			sReport = sReport +  ("<tr><td>EP:" & "0.1" & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for WeaponSpeed | 0")
		End If
		
		skipStats:
		If  doc.SelectSingleNode("//config/Sets").InnerText.Contains("True")=false Then
			goto skipSets
		End If
		
		'
		EPStat="0T7"
		Start(pb,SimTime,MainFrm)
		BaseDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		
		'
		EPStat="AttackPower0T7"
		Start(pb,SimTime,MainFrm)
		APDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		
		'2T7
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="2T7"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 2T7 | 0")
		End If
		
		'4T7
		if doc.SelectSingleNode("//config/Sets/chkEP4PT7").InnerText = "True" then
			EPStat="4T7"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 4T7 | 0")
		End If
		
		'2T8
		if doc.SelectSingleNode("//config/Sets/chkEP2PT8").InnerText = "True" then
			EPStat="2T8"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 2T8 | 0")
		End If
		
		'4T8
		if doc.SelectSingleNode("//config/Sets/chkEP4PT8").InnerText = "True" then
			EPStat="4T8"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 4T8 | 0")
		End If
		
		'2T9
		if doc.SelectSingleNode("//config/Sets/chkEP2PT9").InnerText = "True" then
			EPStat="2T9"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 2T9 | 0")
		End If
		
		'4T9
		if doc.SelectSingleNode("//config/Sets/chkEP4PT9").InnerText = "True" then
			EPStat="4T9"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 4T9 | 0")
		End If
		
		WriteReport ("")
		skipSets:
		
		
		If  doc.SelectSingleNode("//config/Trinket").InnerText.Contains("True") Then
			sReport = sReport & StartEPTrinket(pb,True, simTime, Mainfrm)
		End If
		
		sReport = sReport &   "<tr><td COLSPAN=8> | Template | " & Split(_MainFrm.cmbTemplate.Text,".")(0) & "</td></tr>"
		If sim.Rotate Then
			sReport = sReport &   "<tr><td COLSPAN=8> | Rotation | " & Split(_MainFrm.cmbRotation.Text,".")(0) & "</td></tr>"
		Else
			sReport = sReport &   "<tr><td COLSPAN=8> | Priority | " & Split(_MainFrm.cmbPrio.Text,".")(0) & "</td></tr>"
		End If
		sReport = sReport &   "<tr><td COLSPAN=8> | Presence | " & _MainFrm.cmdPresence.Text & vbCrLf & "</td></tr>"
		sReport = sReport &   "<tr><td COLSPAN=8> | Sigil | " & _MainFrm.cmbSigils.Text & vbCrLf & "</td></tr>"
		
		If MainStat.DualW Then
			sReport = sReport &   "<tr><td COLSPAN=8> | RuneEnchant | " & _MainFrm.cmbRuneMH.Text  & " / " & _MainFrm.cmbRuneOH.Text  & "</td></tr>"
		Else
			sReport = sReport &   "<tr><td COLSPAN=8> | RuneEnchant | " & _MainFrm.cmbRuneMH.Text & "</td></tr>"
		End If
		sReport = sReport &   "<tr><td COLSPAN=8> | Pet Calculation | " & _MainFrm.ckPet.Checked & "</td></tr>"
		sReport = sReport +  ("</table>")
		sReport = sReport +   ("<hr width='80%' align='center' noshade ></hr>")
		
		WriteReport(sReport)
		EPStat = ""
	End Sub
	
	Sub StartEPAlternative(pb As ProgressBar,EPCalc As Boolean,SimTime As Integer,MainFrm As MainForm)
		_MainFrm = MainFrm
		dim sReport as String
		If EPCalc = False Then
			Start(pb,SimTime,MainFrm)
			exit sub
		End If
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		
		Dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(_MainFrm.GetFilePath(_MainFrm.cmbCharacter.Text) )
		
		EPBase = 50 'Fixed EP base value for now
		'int32.Parse(XmlDoc.SelectSingleNode("//character/EP/base").InnerText)
		
		Dim BaseDPS As long
		Dim APDPS As Long
		
		If SimTime = 0 Then SimTime =1
		'Create EP table
		sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"
		
		If  doc.SelectSingleNode("//config/Stats").InnerText.Contains("True")=false Then
			goto skipStats
		End If
		
		'Dry run
		EPStat="DryRun"
		Start(pb,SimTime,MainFrm)
		BaseDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		
		'AP
		EPStat="AttackPower"
		Start(pb,SimTime,MainFrm)
		APDPS = DPS-BaseDPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | 100</td></tr>")
		
		skipStats:
		
		'Str
		if doc.SelectSingleNode("//config/Stats/chkEPStr").InnerText = "True" then
			EPStat="Strength"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for Strength | 0")
		End If
		
		'Agi
		if doc.SelectSingleNode("//config/Stats/chkEPAgility").InnerText = "True" then
			EPStat="Agility"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS )
		Else
			WriteReport ("Average for Agility | 0")
		End If
		
		'Crit
		if doc.SelectSingleNode("//config/Stats/chkEPCrit").InnerText = "True" then
			EPStat="CritRating"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for CritRating | 0")
		End If
		
		'Haste
		if doc.SelectSingleNode("//config/Stats/chkEPHaste").InnerText = "True" then
			EPStat="HasteRating"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for HasteRating | 0")
		End If
		
		'Arp
		if doc.SelectSingleNode("//config/Stats/chkEPArP").InnerText = "True" then
			EPStat="ArmorPenetrationRating"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for ArmorPenetrationRating | 0")
		End If
		
		'Exp
		if doc.SelectSingleNode("//config/Stats/chkEPExp").InnerText = "True" then
			EPStat="ExpertiseRating"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & (BaseDPS-DPS) / APDPS * 100) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for ExpertiseRating | 0")
		End If
		
		'Hit
		if doc.SelectSingleNode("//config/Stats/chkEPHit").InnerText = "True" then
			EPStat="HitRating"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & (BaseDPS-DPS) / APDPS * 100) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for HitRating | 0")
		End If
		
		'SpHit
		if doc.SelectSingleNode("//config/Stats/chkEPSpHit").InnerText = "True" then
			EPStat="SpellHitRating"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for SpellHitRating | 0")
		End If
		
		'DPS
		if doc.SelectSingleNode("//config/Stats/chkEPSMHDPS").InnerText = "True" then
			EPStat="WeaponDPS"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for WeaponDPS | 0")
		End If
		
		'Speed
		if doc.SelectSingleNode("//config/Stats/chkEPSMHSpeed").InnerText = "True" then
			EPStat="WeaponSpeed"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for WeaponSpeed | 0")
		End If
		
		If  doc.SelectSingleNode("//config/Sets").InnerText.Contains("True")=false Then
			goto skipSets
		End If
		
		'
		EPStat="0T7"
		Start(pb,SimTime,MainFrm)
		BaseDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		
		'
		EPStat="AttackPower0T7"
		Start(pb,SimTime,MainFrm)
		APDPS = DPS-BaseDPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		
		skipSets:
		
		'2T7
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="2T7"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 2T7 | 0")
		End If
		
		'4T7
		if doc.SelectSingleNode("//config/Sets/chkEP4PT7").InnerText = "True" then
			EPStat="4T7"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 4T7 | 0")
		End If
		
		'2T8
		if doc.SelectSingleNode("//config/Sets/chkEP2PT8").InnerText = "True" then
			EPStat="2T8"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 2T8 | 0")
		End If
		
		'4T8
		if doc.SelectSingleNode("//config/Sets/chkEP4PT8").InnerText = "True" then
			EPStat="4T8"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 4T8 | 0")
		End If
		
		'2T9
		if doc.SelectSingleNode("//config/Sets/chkEP2PT9").InnerText = "True" then
			EPStat="2T9"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 2T9 | 0")
		End If
		
		'4T9
		if doc.SelectSingleNode("//config/Sets/chkEP4PT9").InnerText = "True" then
			EPStat="4T9"
			Start(pb,SimTime,MainFrm)
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & Math.Round((DPS-BaseDPS) / APDPS * 100,1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for 4T9 | 0")
		End If
		
		WriteReport ("")
		
		sReport = sReport &   "<tr><td COLSPAN=8> | Template | " & Split(_MainFrm.cmbTemplate.Text,".")(0) & "</td></tr>"
		If sim.Rotate Then
			sReport = sReport &   "<tr><td COLSPAN=8> | Rotation | " & Split(_MainFrm.cmbRotation.Text,".")(0) & "</td></tr>"
		Else
			sReport = sReport &   "<tr><td COLSPAN=8> | Priority | " & Split(_MainFrm.cmbPrio.Text,".")(0) & "</td></tr>"
		End If
		sReport = sReport &   "<tr><td COLSPAN=8> | Presence | " & _MainFrm.cmdPresence.Text & vbCrLf & "</td></tr>"
		sReport = sReport &   "<tr><td COLSPAN=8> | Sigil | " & _MainFrm.cmbSigils.Text & vbCrLf & "</td></tr>"
		
		If MainStat.DualW Then
			sReport = sReport &   "<tr><td COLSPAN=8> | RuneEnchant | " & _MainFrm.cmbRuneMH.Text  & " / " & _MainFrm.cmbRuneOH.Text  & "</td></tr>"
		Else
			sReport = sReport &   "<tr><td COLSPAN=8> | RuneEnchant | " & _MainFrm.cmbRuneMH.Text & "</td></tr>"
		End If
		sReport = sReport &   "<tr><td COLSPAN=8> | Pet Calculation | " & _MainFrm.ckPet.Checked & "</td></tr>"
		sReport = sReport +  ("</table>")
		sReport = sReport +   ("<hr width='80%' align='center' noshade ></hr>")
		
		WriteReport(sReport)
		EPStat = ""
	End Sub
	
	Sub StartEPSimple(pb As ProgressBar,EPCalc As Boolean,SimTime as Integer,MainFrm as MainForm)
		_MainFrm = MainFrm
		dim sReport as String
		If EPCalc = False Then
			Start(pb,SimTime,MainFrm)
			exit sub
		End If
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		
		
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(_MainFrm.GetFilePath(_MainFrm.cmbCharacter.Text) )
		
		'Fixed EP base value for now
		EPBase = 50
		'int32.Parse(XmlDoc.SelectSingleNode("//character/EP/base").InnerText)
		
		dim BaseDPS as long
		Dim APDPS As Long
		Dim EPDPS As Long
		Dim tmp1 As double
		Dim tmp2 As double
		dim aDPS(9) as Integer
		
		
		
		SimTime = SimTime/10
		If SimTime = 0 Then SimTime =1
		'Create EP table
		sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"
		sReport = sReport +   ("<hr width='80%' align='center' noshade ></hr>")
		
		
		If  doc.SelectSingleNode("//config/Stats").InnerText.Contains("True")=false Then
			goto skipStats
		End If
		EPStat="DryRun"
		Start(pb,SimTime,MainFrm)
		BaseDPS = DPS
		WriteReport ("Average for " & EPStat & " = " & BaseDPS)
		
		EPStat="AttackPower"
		Start(pb,SimTime,MainFrm)
		APDPS = DPS
		WriteReport ("Average for " & EPStat & " = " & APDPS)
		sReport = sReport +  ("<tr><td>EP :"& EPStat & " = 100</td></tr>")
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPStr").InnerText = "True" then
			EPStat="Strength"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPAgility").InnerText = "True" then
			EPStat="Agility"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS )
		End If
		
		if doc.SelectSingleNode("//config/Stats/chkEPCrit").InnerText = "True" then
			EPStat="CritRating"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		
		if doc.SelectSingleNode("//config/Stats/chkEPHaste").InnerText = "True" then
			EPStat="HasteRating"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		
		
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPArP").InnerText = "True" then
			EPStat="ArmorPenetrationRating"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		
		
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPExp").InnerText = "True" then
			EPStat="ExpertiseRating"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (-100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPHit").InnerText = "True" then
			EPStat="HitRating"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (-100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPSpHit").InnerText = "True" then
			EPStat="SpellHitRating"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPSMHDPS").InnerText = "True" then
			EPStat="WeaponDPS"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / 10
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPSMHSpeed").InnerText = "True" then
			EPStat="WeaponSpeed"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (EPDPS-BASEDPS) / 0.1
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End If
		skipStats:
		
		If  doc.SelectSingleNode("//config/Sets").InnerText.Contains("True")=false Then
			goto skipSets
		End If
		
		EPStat="0T7"
		Start(pb,SimTime,MainFrm)
		BASEDPS = DPS
		WriteReport ("Average for " & EPStat & " = " & DPS)
		'sReport = sReport +  ("<tr><td>EP :"& EPStat & " = 100</td></tr>")
		
		EPStat="AttackPower0T7"
		Start(pb,SimTime,MainFrm)
		APDPS = DPS
		WriteReport ("Average for " & EPStat & " = " & DPS)
		'sReport = sReport +  ("<tr><td>EP :"& EPStat & " = 100</td></tr>")
		
		
		
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			
			EPStat="2T7"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS)
			tmp2 = (EPDPS-BASEDPS)
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End if
		
		if doc.SelectSingleNode("//config/Sets/chkEP4PT7").InnerText = "True" then
			EPStat="4T7"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS )
			tmp2 = (EPDPS-BASEDPS)
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End if
		
		if doc.SelectSingleNode("//config/Sets/chkEP2PT8").InnerText = "True" then
			EPStat="2T8"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS )
			tmp2 = (EPDPS-BASEDPS)
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
			
		End if
		
		if doc.SelectSingleNode("//config/Sets/chkEP4PT8").InnerText = "True" then
			EPStat="4T8"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS )
			tmp2 = (EPDPS-BASEDPS)
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End if
		
		if doc.SelectSingleNode("//config/Sets/chkEP2PT9").InnerText = "True" then
			
			EPStat="2T9"
			Start(pb,SimTime,MainFrm)
			EPDPS = DPS
			tmp1 = (APDPS-BaseDPS )
			tmp2 = (EPDPS-BASEDPS)
			sReport = sReport +  ("<tr><td>EP :"& EPStat & " = " & int (10000*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " = " & DPS)
		End if
		skipSets:
		
		sReport = sReport +  ("</table>")
		sReport = sReport +   ("<hr width='80%' align='center' noshade ></hr>")
		
		WriteReport(sReport)
		EPStat = ""
	End Sub
	
	function StartEPTrinket(pb As ProgressBar,EPCalc As Boolean,SimTime As Integer,MainFrm As MainForm) as string
		Dim BaseDPS As long
		Dim APDPS As Long
		Dim tmp1 As double
		Dim tmp2 As Double
		dim sReport as String
		
		EPStat="NoTrinket"
		Start(pb,SimTime,MainFrm)
		BaseDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		
		'
		EPStat="AttackPowerNoTrinket"
		Start(pb,SimTime,MainFrm)
		APDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		

		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		Dim trinketsList As Xml.XmlNode
		dim tNode as Xml.XmlNode
		trinketsList = doc.SelectsingleNode("//config/Trinket")

		For Each tNode In trinketsList.ChildNodes
			If tNode.InnerText = "True" Then
				EPStat= tNode.Name.Replace("chkEP","")
				Start(pb,SimTime,MainFrm)
				tmp1 = (APDPS-BaseDPS ) / 100
				tmp2 = (DPS-BaseDPS)/ 100
				sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & int (10000*tmp2/tmp1)) & "</td></tr>"
				WriteReport ("Average for " & EPStat & " | " & DPS)
			End If
		Next
		return sReport
	End Function
	
	
	
	
	
	
	
	Sub initReport
		Dim Tw As System.IO.TextWriter
		
		ReportPath = System.IO.Path.GetTempFileName
		Tw  =system.IO.File.appendText(ReportPath )
		tw.WriteLine("<hmtl style='font-family:Verdana; font-size:10px;'><body>")
		tw.Flush
		tw.Close
		
	End Sub
	Sub WriteReport(txt As String)
		Dim Tw As System.IO.TextWriter
		'On Error Resume Next
		
		Tw  =system.IO.File.appendText(ReportPath )
		tw.WriteLine(txt & "<br>")
		tw.Close
		
		_MainFrm.webBrowser1.Navigate(ReportPath)
		Dim doc As HtmlDocument
		Application.DoEvents
		doc = _MainFrm.webBrowser1.Document
		'doc.Body.ScrollTop = Integer.MaxValue
		doc.Window.ScrollTo(0,32767)
		
		_MainFrm.webBrowser1.Select
	End Sub
	
	Sub Start(pb As ProgressBar,SimTime As Integer, MainFrm As MainForm)
		Rnd(-1) 'Tell VB to initialize using Randomize's parameter
		_MainFrm = MainFrm
		'combatlog.LogDetails = true
		SimStart = now
		MaxTime = SimTime
		MaxTime= MaxTime * 3600 *100
		pb.Maximum = maxtime
		
		'Init
		Initialisation
		proc.init
		trinket.init
		Character.initialisation
		GhoulStat.init
		TimeStamp = 1
		
		If Rotate Then loadRotation
		
		Do Until TimeStamp > MaxTime
			pb.Value = timestamp
			Timestamp = FastFoward(Timestamp)
			
			If MainStat.FrostPresence = 1 Then
				If TalentBlood.ScentOfBlood > 0 Then
					If proc.ScentOfBloodCD < TimeStamp Then
						proc.GetUseScentOfBlood(TimeStamp)
					End If
				End If
			End If
			
			application.DoEvents
			
			If AMSTimer < TimeStamp Then
				AMSTimer = TimeStamp + AMSCd
				RunicPower.add(AMSAmount)
			End If
			
			If talentblood.Butchery > 0 And Butchery.nextTick <= TimeStamp Then
				Butchery.apply(TimeStamp)
			End If
			
			If true then 'InterruptTimer > TimeStamp Or InterruptAmount == 0 Then 'Interrupt fighting every InterruptCd secs
				If Bloodlust.IsAvailable(TimeStamp) And TimeStamp > 1500 Then
					Bloodlust.use(TimeStamp)
				End If
				
				if TalentBlood.DRW = 1 then
					If Sim.isInGCD(TimeStamp) = False Then
						If DRW.cd < TimeStamp and RunicPower.Value  >= 60 Then
							DRW.Summon(TimeStamp)
						End If
					End If
					If DRW.IsActive(TimeStamp) Then
						if DRW.NextDRW <= TimeStamp then DRW.ApplyDamage(TimeStamp)
					End If
				end if
				
				If DeathandDecay.nextTick = TimeStamp Then
					DeathandDecay.ApplyDamage(TimeStamp)
				End If
				
				If sim.PetFriendly Then
					If talentunholy.SummonGargoyle = 1 Then
						If Sim.isInGCD(TimeStamp) = False Then
							If Gargoyle.cd < TimeStamp and RunicPower.Value >= 60 Then
								Gargoyle.Summon(TimeStamp)
							end if
						End If
						If Gargoyle.ActiveUntil >= TimeStamp Then
							If Gargoyle.NextGargoyleStrike <= TimeStamp Then Gargoyle.ApplyDamage(TimeStamp)
						end if
					End If
				end if
				
				If Sim.isInGCD(TimeStamp) = False Then
					if Rotate then
						DoRoration(TimeStamp)
					else
						Sim.DoNext (TimeStamp)
					End If
				End If
				
				If Sim.isInGCD(TimeStamp) = False Then
					If UA.IsAvailable(TimeStamp) Then
						UA.Use(TimeStamp)
					End If
				End If
				
				If sim.PetFriendly Then
					If Sim.isInGCD(TimeStamp) = False Then
						If Ghoul.ActiveUntil < TimeStamp and Ghoul.cd < TimeStamp Then
							Ghoul.Summon(TimeStamp)
						end if
					End If
					if Ghoul.ActiveUntil >= TimeStamp then
						If Ghoul.NextWhiteMainHit <= TimeStamp Then Ghoul.ApplyDamage(TimeStamp)
						If Ghoul.NextClaw <= TimeStamp Then Ghoul.Claw(TimeStamp)
						If Sim.isInGCD(TimeStamp) And Ghoul.IsAutoFrenzyAvailable(Timestamp) Then
							Ghoul.Frenzy(TimeStamp)
						End If
					End If
				End If
				
				If MainHand.NextWhiteMainHit <= TimeStamp Then MainHand.ApplyDamage(TimeStamp)
				If MainStat.DualW Then
					If OffHand.NextWhiteOffHit <= TimeStamp Then OffHand.ApplyDamage(TimeStamp)
				End If
			Else
				'InterruptTimer > TimeStamp Or InterruptAmount
			End If
			
			If Sim.isInGCD(TimeStamp) = False Then
				If horn.isAvailable(TimeStamp) Then
					horn.use(TimeStamp)
				end if
			End If
			
			If BloodPlague.isActive(TimeStamp) Then
				If BloodPlague.nextTick <= TimeStamp Then
					BloodPlague.ApplyDamage(TimeStamp)
				End If
			End If
			If FrostFever.isActive(TimeStamp) Then
				If FrostFever.nextTick <= TimeStamp Then
					FrostFever.ApplyDamage(TimeStamp)
				End If
			End If
			
			If ShowDpsTimer <= TimeStamp Then
				ShowDpsTimer = TimeStamp + 0.5 * 60 * 60 * 100
				TotalDamage = ScourgeStrike.total + obliterate.total + PlagueStrike.total + _
					BloodStrike.total + HeartStrike.total + frostfever.total + _
					BloodPlague.total + IcyTouch.total + deathcoil.total + _
					UnholyBlight.total + Necrosis.total + BloodCakedBlade.total + _
					WanderingPlague.total +FrostStrike.total  +HowlingBlast.total + _
					BloodBoil.total  + DeathStrike.total + MainHand.total + _
					OffHand.total  + Ghoul.total + Gargoyle.total + DRW.total + _
					RazoriceTotal + DeathandDecay.total + RuneStrike.total  + trinket.Total
				_MainFrm.lblDPS.Text = DPS & " DPS"
			End If
		Loop
		
		TotalDamage = ScourgeStrike.total + obliterate.total + PlagueStrike.total + _
			BloodStrike.total + HeartStrike.total + frostfever.total + _
			BloodPlague.total + IcyTouch.total + deathcoil.total + _
			UnholyBlight.total + Necrosis.total + BloodCakedBlade.total + _
			WanderingPlague.total +FrostStrike.total  +HowlingBlast.total + _
			BloodBoil.total  + DeathStrike.total + MainHand.total + _
			OffHand.total  + Ghoul.total + Gargoyle.total + DRW.total + _
			RazoriceTotal + DeathandDecay.total + RuneStrike.total + trinket.Total
		
		DPS = 100 * TotalDamage / TimeStamp
		
		Report()
		'WriteReport ( "--------" & EPStat & " now:" )
		'Report()
		_MainFrm.lblDPS.Text = DPS & " DPS"
		Debug.Print( "DPS=" & DPS & " " & EPStat & " hit=" & mainstat.Hit & " sphit=" & mainstat.SpellHit & " exp=" & mainstat.expertise )
		'Debug.Print( "Done. Total DPS = " & DPS)
		'Debug.Print ("Total Damage = " & TotalDamage & " in " & MaxTime / 1000 / 60 & " m")
		combatlog.finish
	End Sub
	Sub loadRotation()
		rotation.Rotation.Clear
		RotationStep = 0
		XMLRo.Load(rotationPath)
		dim Nod as Xml.XmlNode
		For Each Nod In XMLRo.SelectSingleNode("//Rotation/Rotation").ChildNodes
			rotation.Rotation.Add(Nod.Name)
		Next
		dim i as integer
		i = 0
		
		For Each Nod In XMLRo.SelectSingleNode("//Rotation/Runes").ChildNodes
			i=i+1
			select case i
				case 1
					if Nod.Name ="Death" then rune1.death=true
				case 2
					if Nod.Name ="Death" then rune2.death=true
				case 3
					if Nod.Name ="Death" then rune3.death=true
				case 4
					if Nod.Name ="Death" then rune4.death=true
				case 5
					if Nod.Name ="Death" then rune5.death=true
				case 6
					if Nod.Name ="Death" then rune6.death=true
			end select
		Next
		
		
		Rotate=true
	End Sub
	Function isInGCD(T As long ) As Boolean
		If NextFreeGCD <= T Then
			isInGCD = False
		Else
			isInGCD = True
		End If
	End Function
	Sub DoRoration(TimeStamp As long)
		
		dim ret as Boolean
		ret = DoRoration(TimeStamp,Rotation.Rotation.Item(RotationStep+1),Rotation.XMLRo.SelectSingleNode("//Rotation/Rotation/" & Rotation.Rotation.Item(RotationStep+1) ).Attributes.GetNamedItem("retry").Value )
		If ret = True Then RotationStep = RotationStep + 1
		if Rotation.Rotation.Count <= RotationStep then RotationStep=0
	End Sub
	function DoRoration(TimeStamp As Double,Ability as string,retry as integer ) as Boolean
		Select Case Ability
			Case "BloodTap"
				If BloodTap.IsAvailable(Timestamp) Then
					return BloodTap.Use(Timestamp)
				Else
					If retry = 0 Then Return True
				End If
			Case "GhoulFrenzy"
				if ghoul.IsFrenzyAvailable(Timestamp) Then
					return ghoul.Frenzy(Timestamp)
				Else
					If retry = 0 Then Return True
				End If
			Case "ScourgeStrike"
				If runes.FU(TimeStamp) = True Then
					return ScourgeStrike.ApplyDamage(TimeStamp)
					'debug.Print("SS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "PlagueStrike"
				If runes.Unholy(TimeStamp) Then
					return PlagueStrike.ApplyDamage(TimeStamp)
					'debug.Print("PS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "Obliterate"
				If runes.FU(TimeStamp) = True Then
					
					return Obliterate.ApplyDamage(TimeStamp)
					'debug.Print("OB")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "FrostStrike"
				If FrostStrike.isAvailable(TimeStamp) = True Then
					
					return FrostStrike.ApplyDamage(TimeStamp)
					'debug.Print("FS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "DeathStrike"
				If runes.FU(TimeStamp) Then
					
					return DeathStrike.ApplyDamage(TimeStamp)
					'debug.Print("BS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodStrike"
				If runes.AnyBlood(TimeStamp) Then
					return BloodStrike.ApplyDamage(TimeStamp)
					'debug.Print("BS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "HeartStrike"
				If runes.AnyBlood(TimeStamp) = True Then
					
					return Heartstrike.ApplyDamage(TimeStamp)
					'debug.Print("HS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodPlague"
				If BloodPlague.isActive(TimeStamp + 150) = False And runes.Unholy(TimeStamp) = True Then
					Return PlagueStrike.ApplyDamage(TimeStamp)
					'debug.Print("PS")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "IcyTouch"
				If runes.Frost(TimeStamp) = True Then
					
					Return IcyTouch.ApplyDamage(TimeStamp)
					'debug.Print("IT")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "DeathCoil"
				If deathcoil.isAvailable(TimeStamp) = True Then
					
					Return deathcoil.ApplyDamage(TimeStamp,False)
					'debug.Print("DC")
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "BloodBoil"
				If runes.AnyBlood(TimeStamp) = True Then
					
					Return BloodBoil.ApplyDamage(TimeStamp)
					Exit Function
				Else
					if retry = 0 then return true
				End If
			Case "Pestilance"
			Case "HowlingBlast"
				If HowlingBlast.isAvailable(TimeStamp) Then
					If proc.rime Or runes.FU(TimeStamp) Then
						runes.UnReserveFU(TimeStamp)
						Return HowlingBlast.ApplyDamage(TimeStamp)
						Exit function
					Else
						runes.ReserveFU(TimeStamp)
					End If
				Else
					if retry = 0 then return true
				End If
			Case "DeathandDecay"
				If DeathAndDecay.isAvailable(TimeStamp) Then
					Return DeathAndDecay.Apply(TimeStamp)
				End If
			Case "Pestilence"
				If runes.Blood(TimeStamp) Then
					Return Pestilence.use(TimeStamp)
				Else
					if retry = 0 then return true
				End If
		End Select
	End function
	
	sub DoNext(TimeStamp As long )
		Dim HighestPrio As Integer
		HighestPrio = 1
		
		For Each item as String In priority.prio
			Select Case item
				Case "BloodTap"
					If BloodTap.IsAvailable(Timestamp) and rune1.death = false and rune2.death = false    Then
						BloodTap.Use(Timestamp)
						'debug.Print("BT")
					End If
					
					
				Case "GhoulFrenzy"
					if ghoul.IsFrenzyAvailable(Timestamp) Then
						ghoul.Frenzy(Timestamp)
						exit sub
					end if
					
				Case "ScourgeStrike"
					If runes.FU(TimeStamp) = True Then
						ScourgeStrike.ApplyDamage(TimeStamp)
						'debug.Print("SS")
						exit sub
					End If
				Case "PlagueStrike"
					If runes.Unholy(TimeStamp) Then
						PlagueStrike.ApplyDamage(TimeStamp)
						'debug.Print("PS")
						exit sub
					End If
				Case "DRMObliterate"
					If runes.DRMFU(TimeStamp) = True Then
						Obliterate.ApplyDamage(TimeStamp)
						'debug.Print("OB")
						exit sub
					End If
				Case "Obliterate"
					If runes.FU(TimeStamp) = True Then
						Obliterate.ApplyDamage(TimeStamp)
						'debug.Print("OB")
						exit sub
					End If
					
				Case "KMFrostStrike"
					If FrostStrike.isAvailable(TimeStamp) = True and proc.KillingMachine Then
						FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
				Case "FrostStrike"
					If FrostStrike.isAvailable(TimeStamp) = True Then
						FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
					
				Case "FrostStrikeMaxRp"
					If RunicPower.MaxValue = RunicPower.Value  Then
						FrostStrike.ApplyDamage(TimeStamp)
						'debug.Print("FS")
						exit sub
					End If
					
				Case "DeathStrike"
					If runes.FU(TimeStamp) Then
						DeathStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If
				Case "BloodStrike"
					If runes.Blood(TimeStamp) Then
						BloodStrike.ApplyDamage(TimeStamp)
						'debug.Print("BS")
						exit sub
					End If
				Case "HeartStrike"
					If runes.Blood(TimeStamp) = True Then
						Heartstrike.ApplyDamage(TimeStamp)
						'debug.Print("HS")
						exit sub
					End If
				Case "Rime"
					If proc.rime and HowlingBlast.isAvailable(TimeStamp) Then
						HowlingBlast.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "FrostFever"
					If glyph.Disease Then
						if Pestilence.PerfectUsage(T) then
							Pestilence.use(T)
							exit sub
						End If
					End If
					If FrostFever.isActive(TimeStamp + 150) = False Then
						If talentfrost.HowlingBlast = 1 And glyph.HowlingBlast And HowlingBlast.isAvailable(TimeStamp)  Then
							If proc.rime Or runes.FU(TimeStamp) Then
								HowlingBlast.ApplyDamage(TimeStamp)
								exit sub
							End If
						end if
						if runes.Frost(TimeStamp) = True Then
							IcyTouch.ApplyDamage(TimeStamp)
							Exit Sub
						End If
					End If
				Case "BloodPlague"
					If glyph.Disease Then
						if Pestilence.PerfectUsage(T) then
							Pestilence.use(T)
							exit sub
						End If
					End If
					If BloodPlague.isActive(TimeStamp + 150) = False then
						If runes.Unholy(TimeStamp) = True Then
							PlagueStrike.ApplyDamage(TimeStamp)
							exit sub
						End If
					End If
				Case "IcyTouch"
					If runes.Frost(TimeStamp) = True Then
						IcyTouch.ApplyDamage(TimeStamp)
						exit sub
					End If
					
				Case "DeathCoilMaxRp"
					If RunicPower.MaxValue = RunicPower.Value Then
						deathcoil.ApplyDamage(TimeStamp,False)
						'debug.Print("DC")
						exit sub
					End If
				Case "DeathCoil"
					If deathcoil.isAvailable(TimeStamp) = True Then
						deathcoil.ApplyDamage(TimeStamp,False)
						'debug.Print("DC")
						exit sub
					End If
				Case "BloodBoil"
					If runes.Blood(TimeStamp) = True Then
						BloodBoil.ApplyDamage(TimeStamp)
						exit sub
					End If
				Case "Pestilance"
					
				Case "HowlingBlast"
					If HowlingBlast.isAvailable(TimeStamp) Then
						If proc.rime Or runes.FU(TimeStamp) Then
							HowlingBlast.ApplyDamage(TimeStamp)
							runes.UnReserveFU(TimeStamp)
							Exit Sub
						Else
							runes.ReserveFU(TimeStamp)
						End If
					Else
					End If
				Case "KMHowlingBlast"
					If HowlingBlast.isAvailable(TimeStamp) and proc.KillingMachine Then
						If proc.rime Or runes.FU(TimeStamp) Then
							HowlingBlast.ApplyDamage(TimeStamp)
							runes.UnReserveFU(TimeStamp)
							Exit Sub
						Else
							runes.ReserveFU(TimeStamp)
						End If
					Else
					End If
				Case "KMRime"
					If Proc.Rime and proc.KillingMachine Then
						HowlingBlast.ApplyDamage(TimeStamp)
					Else
					End If
				Case "DeathandDecay"
					If DeathAndDecay.isAvailable(TimeStamp) Then
						DeathAndDecay.Apply(TimeStamp)
						Exit Sub
					End If
			End Select
			doNext:
		Next
	End sub
	Sub loadtemplate(file As String)
		
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(file)
		
		talentblood.Butchery = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Butchery").InnerText)
		talentblood.Subversion  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Subversion").InnerText)
		talentblood.BladedArmor  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BladedArmor").InnerText)
		TalentBlood.ScentOfBlood = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ScentOfBlood").InnerText)
		talentblood.Weapspec  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Weapspec").InnerText)
		talentblood.Darkconv  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Darkconv").InnerText)
		talentblood.BloodyStrikes  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodyStrikes").InnerText)
		talentblood.Vot3W  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Vot3W").InnerText)
		talentblood.BloodyVengeance  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodyVengeance").InnerText)
		talentblood.AbominationMight  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/AbominationMight").InnerText)
		talentblood.Hysteria  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Hysteria").InnerText)
		talentblood.ImprovedDeathStrike  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedDeathStrike").InnerText)
		talentblood.SuddenDoom  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/SuddenDoom").InnerText)
		talentblood.MightofMograine  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MightofMograine").InnerText)
		talentblood.BloodGorged  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodGorged").InnerText)
		talentblood.DRW  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/DRW").InnerText)
		talentblood.DRM  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/DRM").InnerText)
		
		
		
		
		talentfrost.RPM  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RPM").InnerText)
		talentfrost.ImprovedIcyTouch  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedIcyTouch").InnerText)
		talentfrost.Toughness  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Toughness").InnerText)
		talentfrost.BlackIce  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BlackIce").InnerText)
		talentfrost.NervesofColdSteel  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/NervesofColdSteel").InnerText)
		talentfrost.Annihilation  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Annihilation").InnerText)
		talentfrost.KillingMachine  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/KillingMachine").InnerText)
		talentfrost.GlacierRot  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GlacierRot").InnerText)
		talentfrost.Deathchill  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Deathchill").InnerText)
		talentfrost.ImprovedIcyTalons  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedIcyTalons").InnerText)
		talentfrost.MercilessCombat  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MercilessCombat").InnerText)
		talentfrost.Rime  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Rime").InnerText)
		talentfrost.BloodoftheNorth  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodoftheNorth").InnerText)
		talentfrost.UnbreakableArmor  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/UnbreakableArmor").InnerText)
		talentfrost.GuileOfGorefiend  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GuileOfGorefiend").InnerText)
		talentfrost.TundraStalker  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/TundraStalker").InnerText)
		talentfrost.ChillOfTheGrave  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ChillOfTheGrave").InnerText)
		TalentFrost.HowlingBlast = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/HowlingBlast").InnerText)
		TalentFrost.ThreatOfThassarian= Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ThreatOfThassarian").InnerText)
		
		
		
		talentunholy.ViciousStrikes  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ViciousStrikes").InnerText)
		talentunholy.Virulence  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Virulence").InnerText)
		talentunholy.Epidemic  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Epidemic").InnerText)
		talentunholy.Morbidity  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Morbidity").InnerText)
		talentunholy.RavenousDead  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RavenousDead").InnerText)
		talentunholy.MasterOfGhouls  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MasterOfGhouls").InnerText)
		talentunholy.Outbreak  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Outbreak").InnerText)
		talentunholy.Necrosis  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Necrosis").InnerText)
		talentunholy.BloodCakedBlade  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodCakedBlade").InnerText)
		talentunholy.UnholyBlight  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/UnholyBlight").InnerText)
		talentunholy.Impurity  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Impurity").InnerText)
		talentunholy.CryptFever  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/CryptFever").InnerText)
		talentunholy.ImprovedUnholyPresence = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedUnholyPresence").InnerText)
		talentunholy.BoneShield  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BoneShield").InnerText)
		talentunholy.NightoftheDead  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/NightoftheDead").InnerText)
		TalentUnholy.GhoulFrenzy= Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GhoulFrenzy").InnerText)
		talentunholy.WanderingPlague  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/WanderingPlague").InnerText)
		talentunholy.EbonPlaguebringer  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/EbonPlaguebringer").InnerText)
		talentunholy.RageofRivendare  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RageofRivendare").InnerText)
		talentunholy.SummonGargoyle  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/SummonGargoyle").InnerText)
		talentunholy.Dirge  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Dirge").InnerText)
		talentunholy.Reaping  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Reaping").InnerText)
		
		talentunholy.Desecration = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Desecration").InnerText)
		talentunholy.Desolation = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Desolation").InnerText)
		
		Glyph.init(file)
		
		
		
		
		
		
	End Sub
	sub Initialisation()
		CombatLog.Init
		Buff.FullBuff
		'Buff.UnBuff
		
		MainHand.init
		offhand.init
		BloodPlague.init
		
		FrostFever.init
		UnholyBlight.init
		WanderingPlague.init
		BloodTap.init
		HowlingBlast.init
		GhoulStat.init
		Hysteria.init
		DeathChill.init
		UA.init
		RuneForge.init
		Butchery.init
		DRW.init
		RuneStrike.init
		
		RunicPower.Value = 0
		runes.Init
		
		
		MainStat.init
		NextFreeGCD = 0
		TotalDamage = 0
		Threat = 0
		ScourgeStrike.init
		obliterate.init
		PlagueStrike.init
		BloodStrike.init
		frostfever.init
		BloodPlague.init
		IcyTouch.init
		deathcoil.init
		UnholyBlight.init
		MainHand.init
		OffHand.init
		Necrosis.init
		BloodCakedBlade.init
		WanderingPlague.init
		froststrike.init
		deathstrike.init
		HowlingBlast.init
		BloodBoil.init
		HeartStrike.init
		DeathandDecay.init
		Ghoul.init
		Gargoyle.init
		Horn.init
		
		Bloodlust.init
		
		AMSCd = _MainFrm.txtAMScd.text * 100
		AMSTimer = _MainFrm.txtAMScd.text * 100
		AMSAmount = _MainFrm.txtAMSrp.text
		
		InterruptCd = _MainFrm.txtInterruptCd.text * 100
		InterruptTimer = _MainFrm.txtInterruptCd.text * 100
		InterruptAmount = _MainFrm.txtInterruptAmount.text
		
		ShowDpsTimer = 1
		
	End sub
	
	Function toDecimal(d As Double) As Decimal
		try
			Return d.ToString (".#")
		Catch
		End Try
	End Function
	
	Sub loadPriority(file As String)
		
		priority.prio.Clear
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(file)
		dim Nod as Xml.XmlNode
		
		
		For Each Nod In xmldoc.SelectSingleNode("//Priority").ChildNodes
			priority.prio.Add(Nod.Name)
		Next
		
	End Sub
	
	Sub Report()
		Dim Tw As System.IO.TextWriter
		if EPStat <> "" then exit sub
		
		Tw  =system.IO.File.appendText(ReportPath)
		'Tw  = system.IO.File.Open(reportpath, system.IO.FileMode.Append)     '.OpenWrite(ReportPath)
		Tw.Write ("<table border='0' cellspacing='2' style='font-family:Verdana; font-size:10px;'>")
		Tw.Write ("<tr>")
		Tw.Write ("	<td><b>Ability</b></td>")
		Tw.Write ("	<td><b>Total</b></td>")
		Tw.Write ("	<td><b>%</b></td>")
		Tw.Write ("	<td><b>Landed</b></td>")
		Tw.Write ("	<td><b>Hit%</b></td>")
		Tw.Write ("	<td><b>Crit%</b></td>")
		Tw.Write ("	<td><b>Miss%</b></td>")
		Tw.Write ("	<td><b>Average</b></td>")
		Tw.Write ("</tr>")
		
		' Sort report
		
		dim myArray as new ArrayList
		
		
		If MainHand.total <> 0 Then myArray.Add(MainHand.total)
		If OffHand.total <> 0 Then myArray.Add(OffHand.total)
		If ScourgeStrike.total <> 0 Then myArray.Add(ScourgeStrike.total)
		If HeartStrike.total <> 0 Then myArray.Add(HeartStrike.total)
		If obliterate.total <> 0 Then myArray.Add(obliterate.total)
		If DeathStrike.total <> 0 Then myArray.Add(DeathStrike.total)
		If PlagueStrike.total <> 0 Then myArray.Add(PlagueStrike.total)
		If IcyTouch.total <> 0 Then myArray.Add(IcyTouch.total)
		If BloodStrike.total <> 0 Then myArray.Add(BloodStrike.total)
		If FrostStrike.total <> 0 Then myArray.Add(FrostStrike.total)
		If HowlingBlast.total <> 0 Then myArray.Add(HowlingBlast.total)
		If deathcoil.total <> 0 Then myArray.Add(deathcoil.total)
		If UnholyBlight.total <> 0 Then myArray.Add(UnholyBlight.total)
		If frostfever.total <> 0 Then myArray.Add(frostfever.total)
		If BloodPlague.total <> 0 Then myArray.Add(BloodPlague.total)
		If Necrosis.total <> 0 Then myArray.Add(Necrosis.total)
		If BloodCakedBlade.total <> 0 Then myArray.Add(BloodCakedBlade.total)
		If WanderingPlague.total <> 0 Then myArray.Add(WanderingPlague.total)
		If BloodBoil.total <> 0 Then myArray.Add(BloodBoil.total)
		If RuneStrike.total <> 0 Then myArray.Add(RuneStrike.total)
		If Ghoul.total  <> 0 Then myArray.Add(Ghoul.total)
		If Gargoyle.total  <> 0 Then myArray.Add(Gargoyle.total)
		If DRW.total  <> 0 Then myArray.Add(DRW.total)
		If RazoriceTotal <> 0 Then myArray.Add(RazoriceTotal)
		If DeathandDecay.total <> 0 Then myArray.Add(DeathandDecay.total)
		if trinket.Total <> 0 then myArray.Add(trinket.Total)
		
		myArray.Sort()
		
		
		dim i as Integer
		Dim tot As Long
		dim STmp as string
		For i=0 To myArray.Count -1
			tot = (myArray.Item(myArray.Count-1-i))

			If MainHand.total = tot Then
				STmp = MainHand.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
			End If
			If OffHand.total = tot Then
				STmp = OffHand.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If ScourgeStrike.total = tot Then
				STmp = ScourgeStrike.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If HeartStrike.total = tot Then
				STmp = HeartStrike.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If obliterate.total = tot Then
				STmp = obliterate.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If DeathStrike.total = tot Then
				STmp = DeathStrike.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If PlagueStrike.total = tot Then
				STmp = PlagueStrike.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If IcyTouch.total = tot Then
				STmp = IcyTouch.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If BloodStrike.total = tot Then
				STmp = BloodStrike.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If FrostStrike.total = tot Then
				STmp = FrostStrike.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If HowlingBlast.total = tot Then
				STmp = HowlingBlast.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If deathcoil.total = tot Then
				STmp = deathcoil.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If UnholyBlight.total = tot Then
				STmp = UnholyBlight.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If frostfever.total = tot Then
				STmp = frostfever.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If BloodPlague.total = tot Then
				STmp = BloodPlague.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If Necrosis.total = tot Then
				STmp = Necrosis.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If BloodCakedBlade.total = tot Then
				STmp = BloodCakedBlade.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If WanderingPlague.total = tot Then
				STmp = WanderingPlague.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If BloodBoil.total = tot Then
				STmp = BloodBoil.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If RuneStrike.total = tot Then
				STmp = RuneStrike.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If Ghoul.total  = tot Then
				STmp = Ghoul.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If Gargoyle.total  = tot Then
				STmp = Gargoyle.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If DRW.total  = tot Then
				STmp = DRW.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
			End If
			If RazoriceTotal = tot Then
				STmp = RuneForge.Razoricereport
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
			End If
			
			If DeathandDecay.total = tot Then
				STmp = DeathandDecay.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
			End If
			If trinket.Total = tot Then
				STmp = trinket.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
			End If
			
		Next
		If Horn.HitCount <> 0 Then
				STmp = Horn.report
				STmp = replace(STmp,vbtab,"</td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
			End If
		sTmp = ""
		if EPStat <> "" then STmp =  "<tr><td COLSPAN=8>EP Stat <b>" &  EPStat & "</b></td></tr>"
		STmp = sTmp &  "<tr><td COLSPAN=8>DPS" & VBtab & "<b>" &  DPS & "</b></td></tr>"
		STmp = sTmp &   "<tr><td COLSPAN=8>Total Damage" & VBtab & Math.Round(TotalDamage/1000000,2) & "m" & VBtab &  " in " & MaxTime / 100 / 60/60 & "h</td></tr>"
		
		dim ThreatBeforePresence as Long = Threat
		Threat = ScourgeStrike.total + (ScourgeStrike.CritCount+ScourgeStrike.HitCount)*120 + _
			obliterate.total + PlagueStrike.total + _
			BloodStrike.total + HeartStrike.total + frostfever.total + _
			BloodPlague.total + IcyTouch.total + deathcoil.total + _
			UnholyBlight.total + Necrosis.total + BloodCakedBlade.total + _
			WanderingPlague.total +FrostStrike.total  + HowlingBlast.total + _
			BloodBoil.total  + DeathStrike.total + MainHand.total + _
			OffHand.total  + RazoriceTotal + DeathandDecay.total*1.9 +  RuneStrike.total*1.5
		If MainStat.FrostPresence = 1 Then
			Threat = Threat * 2.0735
		Else
			Threat = (Threat * 0.80) * (1- Talentblood.Subversion * 8.333/100 )
		End If
		Dim tps As Integer
		Threat = Threat + ThreatBeforePresence
		tps =  100 * Threat / TimeStamp
		STmp = sTmp &  "<tr><td COLSPAN=8>Threat Per Second" & VBtab & "<b>" &  tps & "</b></td></tr>"
		STmp = sTmp &   "<tr><td COLSPAN=8>Generated in " & DateDiff( DateInterval.Second,SimStart,now())  & "s</td></tr>"
		
		STmp = sTmp &   "<tr><td COLSPAN=8>Template: " & Split(_MainFrm.cmbTemplate.Text,".")(0) & "</td></tr>"
		If sim.Rotate Then
			STmp = sTmp &   "<tr><td COLSPAN=8>Rotation: " & Split(_MainFrm.cmbRotation.Text,".")(0) & "</td></tr>"
		Else
			STmp = sTmp &   "<tr><td COLSPAN=8>Priority: " & Split(_MainFrm.cmbPrio.Text,".")(0) & "</td></tr>"
		End If
		STmp = sTmp &   "<tr><td COLSPAN=8>Presence: " & _MainFrm.cmdPresence.Text & vbCrLf & "</td></tr>"
		STmp = sTmp &   "<tr><td COLSPAN=8>Sigil: " & _MainFrm.cmbSigils.Text & vbCrLf & "</td></tr>"
		
		If MainStat.DualW Then
			STmp = sTmp &   "<tr><td COLSPAN=8>RuneEnchant: " & _MainFrm.cmbRuneMH.Text  & " / " & _MainFrm.cmbRuneOH.Text  & "</td></tr>"
		Else
			STmp = sTmp &   "<tr><td COLSPAN=8>RuneEnchant: " & _MainFrm.cmbRuneMH.Text & "</td></tr>"
		End If
		
		STmp = sTmp &   "<tr><td COLSPAN=8>Pet Calculation: " & _MainFrm.ckPet.Checked & "</td></tr>"
		
		stmp = stmp & "</table><hr width='80%' align='center' noshade ></hr>"
		tw.Flush
		tw.Close
		WriteReport(stmp)
	End Sub
	Function FastFoward(T As Long) As Long
		Dim tmp As Long
		
		tmp = MaxTime+1
		
		If NextFreeGCD > T Then
			tmp = NextFreeGCD
		Else
			if rune1.AvailableTime > T and rune1.AvailableTime < tmp then  tmp = rune1.AvailableTime
			if rune2.AvailableTime > T and rune2.AvailableTime < tmp then  tmp = rune2.AvailableTime
			If rune3.AvailableTime > T And rune3.AvailableTime < tmp Then  tmp = rune3.AvailableTime
			If rune4.AvailableTime > T And rune4.AvailableTime < tmp Then  tmp = rune4.AvailableTime
			If rune5.AvailableTime > T And rune5.AvailableTime < tmp Then  tmp = rune5.AvailableTime
			if rune6.AvailableTime > T and rune6.AvailableTime < tmp then  tmp = rune6.AvailableTime
			
		End If
		
		If Butchery.nextTick < tmp  And talentblood.Butchery > 0 Then tmp = Butchery.nextTick
		
		if DeathandDecay.nextTick > TimeStamp and  DeathandDecay.nextTick < tmp then tmp = DeathandDecay.nextTick
		
		
		if TalentBlood.DRW = 1 then
			If DRW.IsActive(TimeStamp) Then
				if DRW.NextDRW < tmp  then tmp = DRW.NextDRW
			End If
		End If
		If sim.PetFriendly Then
			If talentunholy.SummonGargoyle = 1 Then
				If Gargoyle.ActiveUntil >= TimeStamp Then
					If Gargoyle.NextGargoyleStrike < tmp then tmp = Gargoyle.NextGargoyleStrike
				end if
			End If
			If Ghoul.ActiveUntil >= TimeStamp Then
				If Ghoul.NextWhiteMainHit < tmp Then tmp = Ghoul.NextWhiteMainHit
				If Ghoul.NextClaw < tmp Then tmp = Ghoul.NextClaw
			End If
		End If
		If MainHand.NextWhiteMainHit < tmp Then tmp = MainHand.NextWhiteMainHit
		If MainStat.DualW Then
			If OffHand.NextWhiteOffHit < tmp Then tmp = OffHand.NextWhiteOffHit
		End If
		If BloodPlague.isActive(TimeStamp) Then
			If BloodPlague.nextTick < tmp Then tmp = BloodPlague.nextTick
		End If
		If FrostFever.isActive(TimeStamp) Then
			If FrostFever.nextTick < tmp Then
				tmp = FrostFever.nextTick
			End If
		End If
		
		
		If tmp < T Then
			Return T
		Else
			Return tmp
		End If
		return tmp
	End Function
	
End Module