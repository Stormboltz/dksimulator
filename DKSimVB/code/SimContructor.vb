'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 15/09/2009
' Heure: 16:15
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module SimConstructor
	
	
	Friend PetFriendly As Boolean
	
	Friend ReportPath As String
	Friend EpStat As String
	friend Rotate as Boolean
	Friend DPSs as new Collection
	Friend sThreadCollection as new Collection
	Friend EPBase as Integer
	Friend ThreadCollection As New Collections.ArrayList
	Public _MainFrm As MainForm
	Sub New()
		
	End Sub
	
	Sub Start(pb As ProgressBar,SimTime As Double, MainFrm As MainForm)
		Dim  sim As Sim
		Dim newthread As System.Threading.Thread
		Sim = New Sim
		_MainFrm = MainFrm
		
'		dim prb as New ProgressBar
'		prb.Left = 10
'		prb.height = 40
'		prb.Width = 300
'		prb.Top = ThreadCollection.Count * prb.height + 10
'		MainFrm.TabPrio2.Controls.Add(prb)
		
		If EpStat <> "" Then
			Sim.Prepare(pb,Simtime, Mainfrm,EPStat,EPBase)
		Else
			Sim.Prepare(pb,Simtime, Mainfrm)
		End If
		
		
		newthread = New System.Threading.Thread(AddressOf sim.Start)
		newthread.Priority= Threading.ThreadPriority.BelowNormal 'Shouldn't be necessary, works fine for me without it.
		'Environment.ProcessorCount 'This gives you the number of cores. Maybe useful.
		newthread.Start()
		ThreadCollection.Add(newthread)
		
	End Sub
	
	
	Sub StartEP(pb As ProgressBar,SimTime As Double,MainFrm As MainForm)
		DPSs.Clear
		ThreadCollection.Clear
		EPBase = MainFrm.txtEPBase.Text
		_MainFrm = MainFrm
		dim sReport as String
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		
		Dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(Application.StartupPath & "\Characters\"  & _MainFrm.cmbCharacter.Text)
		
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
		EPStat="EP DryRun"
		SimConstructor.Start(pb,SimTime,MainFrm)
		Application.DoEvents
		
		EPStat="EP AttackPower"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		if doc.SelectSingleNode("//config/Stats/chkEPStr").InnerText = "True" then
			EPStat="EP Strength"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPAgility").InnerText = "True" then
			EPStat="EP Agility"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPCrit").InnerText = "True" then
			EPStat="EP CritRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPHaste").InnerText = "True" then
			EPStat="EP HasteRating1"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="EP HasteEstimated"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPArP").InnerText = "True" then
			EPStat="EP ArmorPenetrationRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPExp").InnerText = "True" then
			EPStat="EP ExpertiseRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="EP ExpertiseRatingCap"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="EP ExpertiseRatingCapAP"
			SimConstructor.Start(pb,SimTime,MainFrm)
			If MainFrm.cmdPresence.SelectedItem = "Frost" Then
				EPStat="EP ExpertiseRatingAfterCap"
				SimConstructor.Start(pb,SimTime,MainFrm)
			End If
		End If
		
		
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPHit").InnerText = "True" then
			EPStat="EP HitRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="EP HitRatingCap"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="EP HitRatingCapAP"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSpHit").InnerText = "True" then
			EPStat="EP SpellHitRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSMHDPS").InnerText = "True" then
			EPStat="EP WeaponDPS"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSMHSpeed").InnerText = "True" then
			EPStat="EP WeaponSpeed"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If

		Dim T as Threading.Thread
		For Each T In ThreadCollection
			T.Join()
		Next
		
		if doc.SelectSingleNode("//config/Stats/chkEPAfterSpellHitRating").InnerText = "True" then
			EPStat="EP AfterSpellHitBase"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="EP AfterSpellHitBaseAP"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="EP AfterSpellHitRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		For Each T In ThreadCollection
			T.Join()
		Next
		
		
		EPStat = "EP DryRun"
		BaseDPS = dpss(EPStat)
		'WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		EPStat = "EP AttackPower"
		APDPS = dpss(EPStat)
		'WriteReport ("Average for " & EPStat & " | " & APDPS)
		sReport = sReport +  ("<tr><td>" & EPStat & " | 1 (" & toDDecimal((APDPS-BaseDPS ) / 100) & " DPS/per AP) </td></tr>")
		
		Try
			EPStat="EP Strength"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP Agility"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP CritRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try
			EPStat="EP HasteEstimated"
			DPS = DPSs(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		Catch
			
		End Try
		
		Try
			EPStat="EP HasteRating1"
			DPS = DPSs(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		Catch
			
		End Try
		
		
		
'		Try
'			EPStat="EP HasteRating"
''			DPS = dpss(EPStat)
''			EPStat="EP HasteRating1"
''			EPStat="EP HasteRating2"
''			EPStat="EP HasteRating3"
''			EPStat="EP HasteRating4"
''			EPStat="EP HasteRating5"
''			EPStat="EP HasteRating6"
'			tmp1 = (APDPS-BaseDPS ) / 100
'			'tmp2 = (DPS-BaseDPS) / EPBase
'			tmp2 = 0
'			Dim i As Integer
'			For i=1 To 6
'				tmp2 += ((dpss("EP HasteRating" & i) - BaseDPS)/(i*EPBase))
'			Next
'			tmp2 = tmp2/6
'			
'			
'			
'			
'			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
'		'	WriteReport ("Average for " & EPStat & " | " & DPS)
'		catch
'		End Try
		Try
			EPStat="EP ArmorPenetrationRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP ExpertiseRating"
			DPS = dpss(EPStat)
			
			
			tmp1 = (dpss("EP ExpertiseRatingCapAP")-dpss("EP ExpertiseRatingCap") ) / 100
			tmp2 = (DPS-dpss("EP ExpertiseRatingCap")) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try
			EPStat="EP ExpertiseRatingAfterCap"
			DPS = dpss(EPStat)
			tmp1 = (dpss("EP ExpertiseRatingCapAP")-dpss("EP ExpertiseRatingCap") ) / 100
			tmp2 = (DPS-dpss("EP ExpertiseRatingCap")) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | ExpertiseRating After Dodge Cap | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try

			EPStat="EP HitRating"
			DPS = dpss(EPStat)
			tmp1 = (dpss("EP HitRatingCapAP")-dpss("EP HitRatingCap")) / 100
			tmp2 = (DPS-dpss("EP HitRatingCap")) / EPBase
			sReport = sReport +  ("<tr><td>BeforeMeleeHitCap<8% | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP SpellHitRating"
			DPS = dpss(EPStat)
			tmp1 = (dpss("EP HitRatingCapAP")-dpss("EP HitRatingCap")) / 100
			tmp2 = (DPS-dpss("EP HitRatingCap")) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP WeaponDPS"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 10
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP WeaponSpeed"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 0.1
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try
			EPStat="EP AfterSpellHitBase"
			BaseDPS = dpss(EPStat)
			EPStat="EP AfterSpellHitBaseAP"
			APDPS = dpss(EPStat)
			EPStat="EP AfterSpellHitRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>After spell hit cap | " & toDDecimal (tmp2/tmp1) & "</td></tr>")
		'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		end try
		
		EPStat = ""
		
		skipStats:
		DPSs.Clear
		ThreadCollection.Clear
		
		If  doc.SelectSingleNode("//config/Sets").InnerText.Contains("True")=false Then
			goto skipSets
		End If
		
		EPStat="EP 0T7"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		EPStat="EP AttackPower0T7"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="EP 2T7"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT7").InnerText = "True" then
			EPStat="EP 4T7"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT8").InnerText = "True" then
			EPStat="EP 2T8"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT8").InnerText = "True" then
			EPStat="EP 4T8"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT9").InnerText = "True" then
			EPStat="EP 2T9"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT9").InnerText = "True" then
			EPStat="EP 4T9"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT10").InnerText = "True" then
			EPStat="EP 2T10"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT10").InnerText = "True" then
			EPStat="EP 4T10"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		For Each T In ThreadCollection
			T.Join()
		Next

		
		EPStat = "EP 0T7"
		BaseDPS = dpss(EPStat)
		'WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		EPStat = "EP AttackPower0T7"
		APDPS = dpss(EPStat)
		'WriteReport ("Average for " & EPStat & " | " & APDPS)
		'sReport = sReport +  ("<tr><td>" & EPStat & " | 1</td></tr>")
		
		Try
			EPStat="EP 2T7"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			'WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP 4T7"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			'WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP 2T8"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			'WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP 4T8"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			'WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP 2T9"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			'WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP 4T9"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			'WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP 2T10"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			'WriteReport ("Average for " & EPStat & " | " & DPS)
		Catch
		End Try
		Try
			EPStat="EP 4T10"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			'WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		WriteReport ("")
		
		skipSets:
		
		
		If  doc.SelectSingleNode("//config/Trinket").InnerText.Contains("True")=false Then
			goto skipTrinket
		End If
		
		EPStat="EP NoTrinket"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		EPStat="EP AttackPowerNoTrinket"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		doc.Load("EPconfig.xml")
		Dim trinketsList As Xml.XmlNode
		dim tNode as Xml.XmlNode
		trinketsList = doc.SelectsingleNode("//config/Trinket")
		
		For Each tNode In trinketsList.ChildNodes
			If tNode.InnerText = "True" Then
				EPStat= tNode.Name.Replace("chkEP","EP ")
				SimConstructor.Start(pb,SimTime,MainFrm)
			End If
		Next
		
		For Each T In ThreadCollection
			T.Join()
		Next
		
		EPStat = "EP NoTrinket"
		BaseDPS = dpss(EPStat)
		'WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		EPStat = "EP AttackPowerNoTrinket"
		APDPS = dpss(EPStat)
		'WriteReport ("Average for " & EPStat & " | " & APDPS)
		
		
		For Each tNode In trinketsList.ChildNodes
			If tNode.InnerText = "True" Then
				Try
					EPStat= tNode.Name.Replace("chkEP","EP ")
					DPS = dpss(EPStat)
					tmp1 = (APDPS-BaseDPS ) / 100
					tmp2 = (DPS-BaseDPS)/ 100
					sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
					'WriteReport ("Average for " & EPStat & " | " & DPS)
				Catch
					
				End Try
			End If
		Next
		skipTrinket:
		sReport = sReport &   "<tr><td COLSPAN=8> | Template | " & Split(_MainFrm.cmbTemplate.Text,".")(0) & "</td></tr>"
		If Rotate Then
			sReport = sReport &   "<tr><td COLSPAN=8> | Rotation | " & Split(_MainFrm.cmbRotation.Text,".")(0) & "</td></tr>"
		Else
			sReport = sReport &   "<tr><td COLSPAN=8> | Priority | " & Split(_MainFrm.cmbPrio.Text,".")(0) & "</td></tr>"
		End If
		sReport = sReport &   "<tr><td COLSPAN=8> | Presence | " & _MainFrm.cmdPresence.Text & vbCrLf & "</td></tr>"
		sReport = sReport &   "<tr><td COLSPAN=8> | Sigil | " & _MainFrm.cmbSigils.Text & vbCrLf & "</td></tr>"
		
		'If sim.MainStat.DualW Then
		sReport = sReport &   "<tr><td COLSPAN=8> | RuneEnchant | " & _MainFrm.cmbRuneMH.Text  & " / " & _MainFrm.cmbRuneOH.Text  & "</td></tr>"
		
		
		sReport = sReport &   "<tr><td COLSPAN=8> | Pet Calculation | " & _MainFrm.ckPet.Checked & "</td></tr>"
		sReport = sReport +  ("</table>")
		sReport = sReport +   ("<hr width='80%' align='center' noshade ></hr>")
		
		WriteReport(sReport)
		EPStat = ""
		
	End Sub
	
	
	Sub StartScaling(pb As ProgressBar,SimTime As Double,MainFrm As MainForm)
		
		DPSs.Clear
		ThreadCollection.Clear
		EPBase = 50
		_MainFrm = MainFrm
		dim sReport as String
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		Dim T as Threading.Thread
		
		doc.Load("ScalingConfig.xml")
		Dim xNodelist As Xml.XmlNode
		xNodelist = doc.SelectSingleNode("//config/Stats")
		Dim xNode As Xml.XmlNode
		Dim i As Integer
		sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"
		Dim max As Integer
		max = 200
		EPBase = 5
		
		sReport = sReport +  ("<tr><td>Stat</td>")
		For i=0 To max
			sReport = sReport & "<td>" & EPBase*i & "</td>"
		Next
		sReport = sReport +  ("</tr>")
		
		dim INSRTCOLOR as String
		For Each xNode In xNodelist.ChildNodes
			
			If xNode.InnerText = "True" Then
				For i=0 To max
					EpStat=Replace(xNode.Name,"chk","") & i
					SimConstructor.Start(pb,1,MainFrm)
				Next i
				For Each T In ThreadCollection
					T.Join()
				Next
				EpStat= Replace(xNode.Name,"chk","")
				
				INSRTCOLOR = ""
				Select Case EpStat
					Case "ScaExp"
						INSRTCOLOR = "Violet"
					Case "ScaHit"
						INSRTCOLOR = "Yellow"
					Case "ScaArP"
						INSRTCOLOR = "Maroon"
					Case "ScaHaste"
						INSRTCOLOR = "Pink"
					Case "ScaCrit"
						INSRTCOLOR = "Orange"
					Case "ScaAgility"
						INSRTCOLOR = "Purple"
					Case "ScaStr"
						INSRTCOLOR = "Red"
				End Select
				INSRTCOLOR = "Black"
				sReport = sReport +  ("<tr><td><font color=" & INSRTCOLOR & ">" & EpStat & "</td>")
				For i=0 To max
					sReport = sReport +  ("<td>" & DPSs(EpStat & i) & "</td>")
				Next i
				sReport = sReport +  ("</tr>")
			End If
			
		Next
		sReport = sReport & "</table>"
		
		WriteReport(sReport)
		'createGraph
		EpStat = ""
		
	End Sub
	Sub StartSpecDpsValue(pb As ProgressBar,SimTime As Double,MainFrm As MainForm)
		'on error resume next
		DPSs.Clear
		ThreadCollection.Clear
		EPBase = 50
		_MainFrm = MainFrm

		Dim doc As xml.XmlDocument = New xml.XmlDocument
		Dim T As Threading.Thread
		doc.Load(Application.StartupPath & "\Templates\" & MainFrm.cmbTemplate.Text)
		Dim xNodelist As Xml.XmlNode
		xNodelist = doc.SelectSingleNode("//Talents")
		Dim xNode As Xml.XmlNode
		
		EpStat = "OriginalSpec"
		SimConstructor.Start(pb,MainFrm.txtSimtime.Text,MainFrm)
		
		For Each xNode In xNodelist.ChildNodes
			If (xNode.Name <> "URL" and xNode.Name <> "Glyphs") and xNode.InnerText <> "0" Then
				EpStat = xNode.Name
				SimConstructor.Start(pb,MainFrm.txtSimtime.Text,MainFrm)
			End If
		Next
		
		For Each T In ThreadCollection
			T.Join()
		Next

		dim BaseDPS as Integer
		EpStat = "OriginalSpec"
		BaseDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & BaseDPS)
				
		
		For Each xNode In xNodelist.ChildNodes
			If (xNode.Name <> "URL" and xNode.Name <> "Glyphs") and xNode.InnerText <> "0" Then
				EpStat = xNode.Name
				WriteReport ("Value for " & EPStat & " | " & toDDecimal((BaseDPS - dpss(EPStat))/xNode.InnerText))
			End If
		Next
		WriteReport("<hr width='80%' align='center' noshade ></hr>")
		
		
		EpStat = ""
	End Sub
	
	Sub createGraph()
		
		Dim maxDPS As Integer
		Dim minDPS As Integer
		maxDPS= GetHigherValueofThisCollection(DPSs)
		minDPS= GetLowerValueofThisCollection(DPSs)
		dim w as Integer
		w = maxDPS/ 10
		Dim pg As Bitmap = New Bitmap((500),(w))
		Dim gr As Graphics = Graphics.FromImage(pg)
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		'pg.MakeTransparent(color.Black)
		
		doc.Load("ScalingConfig.xml")
		Dim xNodelist As Xml.XmlNode
		xNodelist = doc.SelectSingleNode("//config/Stats")
		Dim xNode As Xml.XmlNode
		Dim i As Integer
		Dim max As Integer
		max = 50
		EPBase = 20
		Dim x1 As Integer
		Dim y1 As Integer
		Dim x2 As Integer
		Dim y2 As Integer
		
		Dim pen As New Drawing.Pen(color.Blue)
		'pen.Width = 3
		
		
		'gr.DrawLine(pen,10,10,50,50)
		For Each xNode In xNodelist.ChildNodes
			If xNode.InnerText = "True" Then
				EpStat= Replace(xNode.Name,"chk","")
				Select Case EpStat
					Case "ScaExp"
						pen.Color  = color.Violet
					Case "ScaHit"
						pen.Color  = color.Yellow
					Case "ScaArP"
						pen.Color  = color.Maroon
					Case "ScaHaste"
						pen.Color  = color.Pink
					Case "ScaCrit"
						pen.Color  = color.Orange
					Case "ScaAgility"
						pen.Color  = color.Purple
					Case "ScaStr"
						pen.Color  = color.Red
				End Select
				
				
				For i=0 To max-1
					x1 = i*10
					y1 = pg.Height -  DPSs(EpStat & i)/10
					x2 = (i+1)*10
					y2 = pg.Height - DPSs(EpStat & i+1)/10
					gr.DrawLine(pen,x1,y1,x2,y2)
				Next i
			End If
		Next
		MakeGrid(pg,100,100)
		pg.Save("myScaling.png",imaging.ImageFormat.Png)
		
		
		
		
		pg = CropBitmap(pg,0,0, pg.Width, (maxDPS-minDPS)/10)
		Dim pathPng As String
		pathPng = System.IO.Path.GetTempFileName()
		
		pg.Save(pathPng,imaging.ImageFormat.Png)
		GlobalFunction.WriteReport("<img src='" & pathPng & "' width='100%'></img>")
		
		
	End sub
	
	
	Private Function CropBitmap(ByRef bmp As Bitmap, ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer, ByVal cropHeight As Integer) As Bitmap
		Dim rect As New Rectangle(cropX, cropY, cropWidth, cropHeight)
		Dim cropped As Bitmap = bmp.Clone(rect, bmp.PixelFormat)
		Return cropped
	End Function
	
	sub MakeGrid(ByVal bmp As Bitmap, XSpace As Integer,YSpace As Integer)
		Dim gr As Graphics = Graphics.FromImage(bmp)
		Dim i As Integer
		Dim pen As New Pen(color.Black)
		
		
		i=0
		do until i >= bmp.Width
			i = i + XSpace
			gr.DrawLine(pen,i,0,i,bmp.height)
		loop
		i=0
		do until i >= bmp.Height
			i = i + YSpace
			gr.DrawLine(pen,0,bmp.Height-i,bmp.Width,bmp.Height-i)
			gr.DrawString((XSpace*i/10), new Font("Arial", 8,FontStyle.Regular ), SystemBrushes.WindowText, new Point( 10,bmp.Height-i ))
		loop
	End Sub
	
	
	
	
	
	
	
End Module
