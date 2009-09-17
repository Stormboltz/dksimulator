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
	Friend Lissage As Boolean 'How about Smoothing? ;)
	Friend PetFriendly As Boolean
	Friend Rotate as Boolean
	Friend ReportPath As String
	Friend EpStat As String
	Friend _MainFrm As MainForm
	Friend DPSs as new Collection
	Friend ThreadCollection as New Collection
	Friend EPBase as Integer
	
	Sub New()
		
	End Sub
	
	Sub Start(pb As ProgressBar,SimTime As Double, MainFrm As MainForm)
		Dim newthread As System.Threading.Thread
		Sim = New Sim
		_MainFrm = MainFrm
		If EpStat <> "" Then
			Sim.Prepare(pb,Simtime, Mainfrm,EPStat)
		Else
			Sim.Prepare(pb,Simtime, Mainfrm)
		End If
		Sim.Prepare(pb,Simtime, Mainfrm)
		sim.ePBase = 50
		newthread = New System.Threading.Thread(AddressOf sim.Start)
		newthread.Priority= Threading.ThreadPriority.BelowNormal 'Shouldn't be necessary, works fine for me without it.
		'Environment.ProcessorCount 'This gives you the number of cores. Maybe useful.
		newthread.Start()
		ThreadCollection.Add(newthread)
	End Sub
	
	
	Sub StartEP(pb As ProgressBar,SimTime As Double,MainFrm As MainForm)
		DPSs.Clear
		ThreadCollection.Clear
		'newthread = New System.Threading.Thread(AddressOf sim.Start)
		'newthread.Start()
		EPBase = 50
		_MainFrm = MainFrm
		dim sReport as String
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		
		Dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(GetFilePath(_MainFrm.cmbCharacter.Text) )
		
		'int32.Parse(XmlDoc.SelectSingleNode("//character/EP/base").InnerText)
		
		Dim BaseDPS As long
		Dim APDPS As Long
		Dim DPS as Long
		Dim tmp1 As double
		Dim tmp2 As Double
		
		If SimTime = 0 Then SimTime = 1
		'Create EP table
		sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"
		
		If  doc.SelectSingleNode("//config/Stats").InnerText.Contains("True")=false Then
			goto skipStats
		End If
		
		'Dry run
		EPStat="DryRun"
		SimConstructor.Start(pb,SimTime,MainFrm)
		Application.DoEvents
		
		EPStat="AttackPower"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		if doc.SelectSingleNode("//config/Stats/chkEPStr").InnerText = "True" then
			EPStat="Strength"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPAgility").InnerText = "True" then
			EPStat="Agility"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPCrit").InnerText = "True" then
			EPStat="CritRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPHaste").InnerText = "True" then
			EPStat="HasteRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPArP").InnerText = "True" then
			EPStat="ArmorPenetrationRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPExp").InnerText = "True" then
			EPStat="ExpertiseRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPHit").InnerText = "True" then
			EPStat="HitRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSpHit").InnerText = "True" then
			EPStat="SpellHitRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSMHDPS").InnerText = "True" then
			EPStat="WeaponDPS"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSMHSpeed").InnerText = "True" then
			EPStat="WeaponSpeed"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		'This seems to have the same effect as AreMyStrheadFinninshed, without an ugly loop.
		'Dim T as Threading.Thread
		'For Each T In ThreadCollection
		'	T.Join()
		'Next
		Do Until AreMyStrheadFinninshed
			Application.DoEvents
		Loop

		EPStat = "DryRun"
		BaseDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		EPStat = "AttackPower"
		APDPS = dpss("AttackPower")
		WriteReport ("Average for " & EPStat & " | " & APDPS)
		sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | 1</td></tr>")
		
		Try
			EPStat="Strength"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="Agility"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="CritRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="HasteRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="ArmorPenetrationRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="ExpertiseRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="HitRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | BeforeMeleeHitCap<8% | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="SpellHitRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 26
			sReport = sReport +  ("<tr><td>EP:" & sim.EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="WeaponDPS"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / 10
			sReport = sReport +  ("<tr><td>EP:" & "10" & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		Try
			EPStat="WeaponSpeed"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 0.1
			sReport = sReport +  ("<tr><td>EP:" & "0.1" & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & sim.DPS)
		catch
		End Try
		
		EPStat = ""
		
		skipStats:
		DPSs.Clear
		ThreadCollection.Clear
		
		If  doc.SelectSingleNode("//config/Sets").InnerText.Contains("True")=false Then
			goto skipSets
		End If
		
		EPStat="0T7"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		EPStat="AttackPower0T7"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="2T7"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="4T7"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="2T8"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="4T8"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="2T9"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="4T9"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		Do Until AreMyStrheadFinninshed
			Application.DoEvents
		Loop
		
		EPStat = "0T7"
		BaseDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		EPStat = "AttackPower0T7"
		APDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & APDPS)
		'sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | 1</td></tr>")
		
		Try
			EPStat="2T7"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="4T7"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="2T8"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="4T8"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="2T9"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="4T9"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
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
	
	
	Function AreMyStrheadFinninshed() As Boolean
		Dim T as Threading.Thread
		For Each T In ThreadCollection
			if T.IsAlive then return false
		Next
		Return true
	End Function
	
	
End Module
