'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 15/09/2009
' Heure: 16:15
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module SimConstructor
	Friend sim As Sim
	Friend Lissage As Boolean
	Friend PetFriendly As Boolean
	Friend Rotate as Boolean
	Friend ReportPath As String
	Friend EpStat As String
	Friend _MainFrm As MainForm
	
	
	Sub New()
		
	End Sub
	
	
	Sub Start(pb As ProgressBar,SimTime As Double, MainFrm As MainForm)
		
		Dim newthread As System.Threading.Thread
		
'		
		
		Sim = New Sim
		newthread = New System.Threading.Thread(AddressOf sim.Init)
		newthread.Start()
		
		
		_MainFrm = MainFrm
		Sim.Start(pb,Simtime, Mainfrm)
		
	End Sub
	
	Sub StartEP(pb As ProgressBar,EPCalc As Boolean,SimTime As Double,MainFrm As MainForm)
		Dim newthread As System.Threading.Thread
		Sim = New Sim
		newthread = New System.Threading.Thread(AddressOf sim.Init)
		newthread.Start()
		
		
		_MainFrm = MainFrm
		dim sReport as String
		If EPCalc = False Then
			Start(pb,SimTime,MainFrm)
			exit sub
		End If
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		
		Dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(GetFilePath(_MainFrm.cmbCharacter.Text) )
		
		'Fixed EP base value for now
		sim.ePBase = 50
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
		sim.Start(pb,SimTime,MainFrm)
		BaseDPS = sim.DPS
		WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		Dim newthread1 As System.Threading.Thread
		newthread1 = New System.Threading.Thread(AddressOf sim.Init)
		newthread1.Start()
		Sim = New Sim
		
		
		
		
		'AP
		EPStat="AttackPower"
		sim.Start(pb,SimTime,MainFrm)
		APDPS = sim.DPS
		WriteReport ("Average for " & EPStat & " | " & APDPS)
		sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | 1</td></tr>")
		
		'Str
		if doc.SelectSingleNode("//config/Stats/chkEPStr").InnerText = "True" then
			EPStat="Strength"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for Strength | 0")
		End If
		
		'Agi
		if doc.SelectSingleNode("//config/Stats/chkEPAgility").InnerText = "True" then
			EPStat="Agility"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS )
		Else
			WriteReport ("Average for Agility | 0")
		End If
		
		'Crit
		if doc.SelectSingleNode("//config/Stats/chkEPCrit").InnerText = "True" then
			EPStat="CritRating"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& sim.EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for CritRating | 0")
		End If
		
		'Haste
		if doc.SelectSingleNode("//config/Stats/chkEPHaste").InnerText = "True" then
			EPStat="HasteRating"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for HasteRating | 0")
		End If
		
		'Arp
		if doc.SelectSingleNode("//config/Stats/chkEPArP").InnerText = "True" then
			EPStat="ArmorPenetrationRating"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& sim.EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & sim.EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for ArmorPenetrationRating | 0")
		End If
		
		'Exp
		if doc.SelectSingleNode("//config/Stats/chkEPExp").InnerText = "True" then
			EPStat="ExpertiseRating"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for ExpertiseRating | 0")
		End If
		
		'Hit
		if doc.SelectSingleNode("//config/Stats/chkEPHit").InnerText = "True" then
			EPStat="HitRating"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / sim.EPBase
			'sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (-100*tmp2/tmp1)) & "</td></tr>"
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | BeforeMeleeHitCap<8% | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & sim.EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for HitRating | 0")
		End If
		
		'SpHit
		dim SPHitDps as Integer
		if doc.SelectSingleNode("//config/Stats/chkEPSpHit").InnerText = "True" then
			EPStat="SpellHitRating"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / 26
			SPHitDps = sim.DPS
			'sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			sReport = sReport +  ("<tr><td>EP:" & 26 & " | AfterMeleeHitCap | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for SpellHitRating | 0")
		End If
		
		if doc.SelectSingleNode("//config/Stats/chkEPAfterSpellHitRating").InnerText = "True" then
			EPStat="AfterSpellHitRating"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-SPHitDps) / 50
			'sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | AfterSpellHitCap | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for AfterSpellHitRating | 0")
		End If
		
		
		
		
		'WeapDPS
		if doc.SelectSingleNode("//config/Stats/chkEPSMHDPS").InnerText = "True" then
			EPStat="WeaponDPS"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / 10
			sReport = sReport +  ("<tr><td>EP:" & "10" & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for WeaponDPS | 0")
		End If
		
		'Speed
		if doc.SelectSingleNode("//config/Stats/chkEPSMHSpeed").InnerText = "True" then
			EPStat="WeaponSpeed"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / 0.1
			sReport = sReport +  ("<tr><td>EP:" & "0.1" & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for WeaponSpeed | 0")
		End If
		
		skipStats:
		If  doc.SelectSingleNode("//config/Sets").InnerText.Contains("True")=false Then
			goto skipSets
		End If
		
		'
		EPStat="0T7"
		sim.Start(pb,SimTime,MainFrm)
		BaseDPS = sim.DPS
		WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		
		'
		EPStat="AttackPower0T7"
		sim.Start(pb,SimTime,MainFrm)
		APDPS = sim.DPS
		WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		
		'2T7
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="2T7"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for 2T7 | 0")
		End If
		
		'4T7
		if doc.SelectSingleNode("//config/Sets/chkEP4PT7").InnerText = "True" then
			EPStat="4T7"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for 4T7 | 0")
		End If
		
		'2T8
		if doc.SelectSingleNode("//config/Sets/chkEP2PT8").InnerText = "True" then
			EPStat="2T8"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for 2T8 | 0")
		End If
		
		'4T8
		if doc.SelectSingleNode("//config/Sets/chkEP4PT8").InnerText = "True" then
			EPStat="4T8"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for 4T8 | 0")
		End If
		
		'2T9
		if doc.SelectSingleNode("//config/Sets/chkEP2PT9").InnerText = "True" then
			EPStat="2T9"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for 2T9 | 0")
		End If
		
		'4T9
		if doc.SelectSingleNode("//config/Sets/chkEP4PT9").InnerText = "True" then
			EPStat="4T9"
			sim.Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		Else
			WriteReport ("Average for 4T9 | 0")
		End If
		
		WriteReport ("")
		skipSets:
		
		If  doc.SelectSingleNode("//config/Trinket").InnerText.Contains("True") Then
			sReport = sReport & sim.StartEPTrinket(pb,True, simTime, Mainfrm)
		End If
		
		sReport = sReport &   "<tr><td COLSPAN=8> | Template | " & Split(_MainFrm.cmbTemplate.Text,".")(0) & "</td></tr>"
		If Rotate Then
			sReport = sReport &   "<tr><td COLSPAN=8> | Rotation | " & Split(_MainFrm.cmbRotation.Text,".")(0) & "</td></tr>"
		Else
			sReport = sReport &   "<tr><td COLSPAN=8> | Priority | " & Split(_MainFrm.cmbPrio.Text,".")(0) & "</td></tr>"
		End If
		sReport = sReport &   "<tr><td COLSPAN=8> | Presence | " & _MainFrm.cmdPresence.Text & vbCrLf & "</td></tr>"
		sReport = sReport &   "<tr><td COLSPAN=8> | Sigil | " & _MainFrm.cmbSigils.Text & vbCrLf & "</td></tr>"
		
		If sim.MainStat.DualW Then
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
	
	
	
	
	
End Module
