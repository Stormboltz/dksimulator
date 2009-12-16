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
	Friend Rotate as Boolean
	Friend ReportPath As String
	Friend EpStat As String
	
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
		If EpStat <> "" Then
			Sim.Prepare(pb,Simtime, Mainfrm,EPStat,EPBase)
		Else
			Sim.Prepare(pb,Simtime, Mainfrm)
		End If
		'Sim.Prepare(pb,Simtime, Mainfrm)
		
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
			
			If MainFrm.cmdPresence.SelectedItem = "Frost" Then
				EPStat="ExpertiseRatingAfterCap"
				SimConstructor.Start(pb,SimTime,MainFrm)
			End If
			
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

		Dim T as Threading.Thread
		For Each T In ThreadCollection
			T.Join()
		Next
		
		if doc.SelectSingleNode("//config/Stats/chkEPAfterSpellHitRating").InnerText = "True" then
			EPStat="AfterSpellHitBase"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="AfterSpellHitBaseAP"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="AfterSpellHitRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		For Each T In ThreadCollection
			T.Join()
		Next
		
		
		EPStat = "DryRun"
		BaseDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		EPStat = "AttackPower"
		APDPS = dpss("AttackPower")
		WriteReport ("Average for " & EPStat & " | " & APDPS)
		sReport = sReport +  ("<tr><td>" & EPStat & " | 1 (" & toDDecimal((APDPS-BaseDPS ) / 100) & " DPS/per AP) </td></tr>")
		
		Try
			EPStat="Strength"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="Agility"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="CritRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="HasteRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="ArmorPenetrationRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="ExpertiseRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		Try
			EPStat="ExpertiseRatingAfterCap"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | ExpertiseRating After Dodge Cap | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try
			EPStat="HitRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>BeforeMeleeHitCap<8% | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="SpellHitRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 26
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="WeaponDPS"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 10
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="WeaponSpeed"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 0.1
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try
			EPStat="AfterSpellHitBase"
			BaseDPS = dpss(EPStat)
			EPStat="AfterSpellHitBaseAP"
			APDPS = dpss(EPStat)
			EPStat="AfterSpellHitRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>After spell hit cap | " & toDDecimal (tmp2/tmp1) & "</td></tr>")
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		end try
		
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
		if doc.SelectSingleNode("//config/Sets/chkEP4PT7").InnerText = "True" then
			EPStat="4T7"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT8").InnerText = "True" then
			EPStat="2T8"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT8").InnerText = "True" then
			EPStat="4T8"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT9").InnerText = "True" then
			EPStat="2T9"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT9").InnerText = "True" then
			EPStat="4T9"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT10").InnerText = "True" then
			EPStat="2T10"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT10").InnerText = "True" then
			EPStat="4T10"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		For Each T In ThreadCollection
			T.Join()
		Next

		
		EPStat = "0T7"
		BaseDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		EPStat = "AttackPower0T7"
		APDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & APDPS)
		'sReport = sReport +  ("<tr><td>" & EPStat & " | 1</td></tr>")
		
		Try
			EPStat="2T7"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="4T7"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="2T8"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="4T8"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="2T9"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="4T9"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="2T10"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Catch
		End Try
		Try
			EPStat="4T10"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS)/ 100
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		WriteReport ("")
		
		skipSets:
		
		
		If  doc.SelectSingleNode("//config/Trinket").InnerText.Contains("True")=false Then
			goto skipTrinket
		End If
		
		EPStat="NoTrinket"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		EPStat="AttackPowerNoTrinket"
		SimConstructor.Start(pb,SimTime,MainFrm)
		
		'if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
		'	EPStat="2T7"
		'	SimConstructor.Start(pb,SimTime,MainFrm)
		'End If
		
'		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		Dim trinketsList As Xml.XmlNode
		dim tNode as Xml.XmlNode
		trinketsList = doc.SelectsingleNode("//config/Trinket")
		
		For Each tNode In trinketsList.ChildNodes
			If tNode.InnerText = "True" Then
				EPStat= tNode.Name.Replace("chkEP","")
				SimConstructor.Start(pb,SimTime,MainFrm)
			End If
		Next
		
		For Each T In ThreadCollection
			T.Join()
		Next
		
		EPStat = "NoTrinket"
		BaseDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & BaseDPS)
		
		EPStat = "AttackPowerNoTrinket"
		APDPS = dpss(EPStat)
		WriteReport ("Average for " & EPStat & " | " & APDPS)
		
		
		For Each tNode In trinketsList.ChildNodes
			If tNode.InnerText = "True" Then
				Try
					EPStat= tNode.Name.Replace("chkEP","")
					DPS = dpss(EPStat)
					tmp1 = (APDPS-BaseDPS ) / 100
					tmp2 = (DPS-BaseDPS)/ 100
					sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
					WriteReport ("Average for " & EPStat & " | " & DPS)
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
		'Else
		sReport = sReport &   "<tr><td COLSPAN=8> | RuneEnchant | " & _MainFrm.cmbRuneMH.Text & "</td></tr>"
		'End If
		sReport = sReport &   "<tr><td COLSPAN=8> | Pet Calculation | " & _MainFrm.ckPet.Checked & "</td></tr>"
		sReport = sReport +  ("</table>")
		sReport = sReport +   ("<hr width='80%' align='center' noshade ></hr>")
		
		WriteReport(sReport)
		EPStat = ""
		
	End Sub
	
	
	Sub StartScaling(pb As ProgressBar,SimTime As Double,MainFrm As MainForm)
		'DPSs.Clear
		'FakeResultBuilder
		'createGraph
		'exit sub
		
		
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
		max = 50
		EPBase = 20
		
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
	Sub FakeResultBuilder
		SimConstructor.DPSs.Add(6947,"ScaExp0")
		SimConstructor.DPSs.Add(6977,"ScaExp1")
		SimConstructor.DPSs.Add(6980,"ScaExp2")
		SimConstructor.DPSs.Add(7023,"ScaExp3")
		SimConstructor.DPSs.Add(7004,"ScaExp4")
		SimConstructor.DPSs.Add(7097,"ScaExp5")
		SimConstructor.DPSs.Add(7088,"ScaExp6")
		SimConstructor.DPSs.Add(7068,"ScaExp7")
		SimConstructor.DPSs.Add(7131,"ScaExp8")
		SimConstructor.DPSs.Add(7133,"ScaExp9")
		SimConstructor.DPSs.Add(7186,"ScaExp10")
		SimConstructor.DPSs.Add(7175,"ScaExp11")
		SimConstructor.DPSs.Add(7175,"ScaExp12")
		SimConstructor.DPSs.Add(7175,"ScaExp13")
		SimConstructor.DPSs.Add(7175,"ScaExp14")
		SimConstructor.DPSs.Add(7175,"ScaExp15")
		SimConstructor.DPSs.Add(7175,"ScaExp16")
		SimConstructor.DPSs.Add(7175,"ScaExp17")
		SimConstructor.DPSs.Add(7175,"ScaExp18")
		SimConstructor.DPSs.Add(7175,"ScaExp19")
		SimConstructor.DPSs.Add(7175,"ScaExp20")
		SimConstructor.DPSs.Add(7175,"ScaExp21")
		SimConstructor.DPSs.Add(7175,"ScaExp22")
		SimConstructor.DPSs.Add(7175,"ScaExp23")
		SimConstructor.DPSs.Add(7175,"ScaExp24")
		SimConstructor.DPSs.Add(7175,"ScaExp25")
		SimConstructor.DPSs.Add(7175,"ScaExp26")
		SimConstructor.DPSs.Add(7175,"ScaExp27")
		SimConstructor.DPSs.Add(7175,"ScaExp28")
		SimConstructor.DPSs.Add(7175,"ScaExp29")
		SimConstructor.DPSs.Add(7175,"ScaExp30")
		SimConstructor.DPSs.Add(7175,"ScaExp31")
		SimConstructor.DPSs.Add(7175,"ScaExp32")
		SimConstructor.DPSs.Add(7175,"ScaExp33")
		SimConstructor.DPSs.Add(7175,"ScaExp34")
		SimConstructor.DPSs.Add(7175,"ScaExp35")
		SimConstructor.DPSs.Add(7175,"ScaExp36")
		SimConstructor.DPSs.Add(7175,"ScaExp37")
		SimConstructor.DPSs.Add(7175,"ScaExp38")
		SimConstructor.DPSs.Add(7175,"ScaExp39")
		SimConstructor.DPSs.Add(7175,"ScaExp40")
		SimConstructor.DPSs.Add(7175,"ScaExp41")
		SimConstructor.DPSs.Add(7175,"ScaExp42")
		SimConstructor.DPSs.Add(7175,"ScaExp43")
		SimConstructor.DPSs.Add(7175,"ScaExp44")
		SimConstructor.DPSs.Add(7175,"ScaExp45")
		SimConstructor.DPSs.Add(7175,"ScaExp46")
		SimConstructor.DPSs.Add(7175,"ScaExp47")
		SimConstructor.DPSs.Add(7175,"ScaExp48")
		SimConstructor.DPSs.Add(7175,"ScaExp49")
		SimConstructor.DPSs.Add(7175,"ScaExp50")
		SimConstructor.DPSs.Add(6849,"ScaHit0")
		SimConstructor.DPSs.Add(6911,"ScaHit1")
		SimConstructor.DPSs.Add(6901,"ScaHit2")
		SimConstructor.DPSs.Add(6928,"ScaHit3")
		SimConstructor.DPSs.Add(6964,"ScaHit4")
		SimConstructor.DPSs.Add(6946,"ScaHit5")
		SimConstructor.DPSs.Add(7048,"ScaHit6")
		SimConstructor.DPSs.Add(7068,"ScaHit7")
		SimConstructor.DPSs.Add(7065,"ScaHit8")
		SimConstructor.DPSs.Add(7030,"ScaHit9")
		SimConstructor.DPSs.Add(7120,"ScaHit10")
		SimConstructor.DPSs.Add(7094,"ScaHit11")
		SimConstructor.DPSs.Add(7163,"ScaHit12")
		SimConstructor.DPSs.Add(7212,"ScaHit13")
		SimConstructor.DPSs.Add(7175,"ScaHit14")
		SimConstructor.DPSs.Add(7175,"ScaHit15")
		SimConstructor.DPSs.Add(7175,"ScaHit16")
		SimConstructor.DPSs.Add(7175,"ScaHit17")
		SimConstructor.DPSs.Add(7175,"ScaHit18")
		SimConstructor.DPSs.Add(7175,"ScaHit19")
		SimConstructor.DPSs.Add(7175,"ScaHit20")
		SimConstructor.DPSs.Add(7175,"ScaHit21")
		SimConstructor.DPSs.Add(7175,"ScaHit22")
		SimConstructor.DPSs.Add(7175,"ScaHit23")
		SimConstructor.DPSs.Add(7175,"ScaHit24")
		SimConstructor.DPSs.Add(7175,"ScaHit25")
		SimConstructor.DPSs.Add(7175,"ScaHit26")
		SimConstructor.DPSs.Add(7175,"ScaHit27")
		SimConstructor.DPSs.Add(7175,"ScaHit28")
		SimConstructor.DPSs.Add(7175,"ScaHit29")
		SimConstructor.DPSs.Add(7175,"ScaHit30")
		SimConstructor.DPSs.Add(7175,"ScaHit31")
		SimConstructor.DPSs.Add(7175,"ScaHit32")
		SimConstructor.DPSs.Add(7175,"ScaHit33")
		SimConstructor.DPSs.Add(7175,"ScaHit34")
		SimConstructor.DPSs.Add(7175,"ScaHit35")
		SimConstructor.DPSs.Add(7175,"ScaHit36")
		SimConstructor.DPSs.Add(7175,"ScaHit37")
		SimConstructor.DPSs.Add(7175,"ScaHit38")
		SimConstructor.DPSs.Add(7175,"ScaHit39")
		SimConstructor.DPSs.Add(7175,"ScaHit40")
		SimConstructor.DPSs.Add(7175,"ScaHit41")
		SimConstructor.DPSs.Add(7175,"ScaHit42")
		SimConstructor.DPSs.Add(7175,"ScaHit43")
		SimConstructor.DPSs.Add(7175,"ScaHit44")
		SimConstructor.DPSs.Add(7175,"ScaHit45")
		SimConstructor.DPSs.Add(7175,"ScaHit46")
		SimConstructor.DPSs.Add(7175,"ScaHit47")
		SimConstructor.DPSs.Add(7175,"ScaHit48")
		SimConstructor.DPSs.Add(7175,"ScaHit49")
		SimConstructor.DPSs.Add(7175,"ScaHit50")
		
		SimConstructor.DPSs.Add(6953,"ScaArP0")
		SimConstructor.DPSs.Add(6966,"ScaArP1")
		SimConstructor.DPSs.Add(6980,"ScaArP2")
		SimConstructor.DPSs.Add(6993,"ScaArP3")
		SimConstructor.DPSs.Add(7007,"ScaArP4")
		SimConstructor.DPSs.Add(7021,"ScaArP5")
		SimConstructor.DPSs.Add(7035,"ScaArP6")
		SimConstructor.DPSs.Add(7049,"ScaArP7")
		SimConstructor.DPSs.Add(7063,"ScaArP8")
		SimConstructor.DPSs.Add(7077,"ScaArP9")
		SimConstructor.DPSs.Add(7091,"ScaArP10")
		SimConstructor.DPSs.Add(7106,"ScaArP11")
		SimConstructor.DPSs.Add(7121,"ScaArP12")
		SimConstructor.DPSs.Add(7135,"ScaArP13")
		SimConstructor.DPSs.Add(7151,"ScaArP14")
		SimConstructor.DPSs.Add(7166,"ScaArP15")
		SimConstructor.DPSs.Add(7181,"ScaArP16")
		SimConstructor.DPSs.Add(7196,"ScaArP17")
		SimConstructor.DPSs.Add(7212,"ScaArP18")
		SimConstructor.DPSs.Add(7228,"ScaArP19")
		SimConstructor.DPSs.Add(7244,"ScaArP20")
		SimConstructor.DPSs.Add(7260,"ScaArP21")
		SimConstructor.DPSs.Add(7276,"ScaArP22")
		SimConstructor.DPSs.Add(7293,"ScaArP23")
		SimConstructor.DPSs.Add(7310,"ScaArP24")
		SimConstructor.DPSs.Add(7326,"ScaArP25")
		SimConstructor.DPSs.Add(7344,"ScaArP26")
		SimConstructor.DPSs.Add(7361,"ScaArP27")
		SimConstructor.DPSs.Add(7378,"ScaArP28")
		SimConstructor.DPSs.Add(7396,"ScaArP29")
		SimConstructor.DPSs.Add(7413,"ScaArP30")
		SimConstructor.DPSs.Add(7431,"ScaArP31")
		SimConstructor.DPSs.Add(7450,"ScaArP32")
		SimConstructor.DPSs.Add(7468,"ScaArP33")
		SimConstructor.DPSs.Add(7487,"ScaArP34")
		SimConstructor.DPSs.Add(7505,"ScaArP35")
		SimConstructor.DPSs.Add(7525,"ScaArP36")
		SimConstructor.DPSs.Add(7544,"ScaArP37")
		SimConstructor.DPSs.Add(7563,"ScaArP38")
		SimConstructor.DPSs.Add(7583,"ScaArP39")
		SimConstructor.DPSs.Add(7603,"ScaArP40")
		SimConstructor.DPSs.Add(7623,"ScaArP41")
		SimConstructor.DPSs.Add(7644,"ScaArP42")
		SimConstructor.DPSs.Add(7664,"ScaArP43")
		SimConstructor.DPSs.Add(7685,"ScaArP44")
		SimConstructor.DPSs.Add(7706,"ScaArP45")
		SimConstructor.DPSs.Add(7728,"ScaArP46")
		SimConstructor.DPSs.Add(7749,"ScaArP47")
		SimConstructor.DPSs.Add(7771,"ScaArP48")
		SimConstructor.DPSs.Add(7794,"ScaArP49")
		SimConstructor.DPSs.Add(7816,"ScaArP50")
		
		SimConstructor.DPSs.Add(6598,"ScaHaste0")
		SimConstructor.DPSs.Add(6644,"ScaHaste1")
		SimConstructor.DPSs.Add(6642,"ScaHaste2")
		SimConstructor.DPSs.Add(6661,"ScaHaste3")
		SimConstructor.DPSs.Add(6687,"ScaHaste4")
		SimConstructor.DPSs.Add(6695,"ScaHaste5")
		SimConstructor.DPSs.Add(6710,"ScaHaste6")
		SimConstructor.DPSs.Add(6746,"ScaHaste7")
		SimConstructor.DPSs.Add(6789,"ScaHaste8")
		SimConstructor.DPSs.Add(6834,"ScaHaste9")
		SimConstructor.DPSs.Add(6859,"ScaHaste10")
		SimConstructor.DPSs.Add(6868,"ScaHaste11")
		SimConstructor.DPSs.Add(6878,"ScaHaste12")
		SimConstructor.DPSs.Add(6935,"ScaHaste13")
		SimConstructor.DPSs.Add(6935,"ScaHaste14")
		SimConstructor.DPSs.Add(6945,"ScaHaste15")
		SimConstructor.DPSs.Add(7032,"ScaHaste16")
		SimConstructor.DPSs.Add(6999,"ScaHaste17")
		SimConstructor.DPSs.Add(7037,"ScaHaste18")
		SimConstructor.DPSs.Add(7069,"ScaHaste19")
		SimConstructor.DPSs.Add(7078,"ScaHaste20")
		SimConstructor.DPSs.Add(7142,"ScaHaste21")
		SimConstructor.DPSs.Add(7090,"ScaHaste22")
		SimConstructor.DPSs.Add(7132,"ScaHaste23")
		SimConstructor.DPSs.Add(7175,"ScaHaste24")
		SimConstructor.DPSs.Add(7176,"ScaHaste25")
		SimConstructor.DPSs.Add(7210,"ScaHaste26")
		SimConstructor.DPSs.Add(7216,"ScaHaste27")
		SimConstructor.DPSs.Add(7257,"ScaHaste28")
		SimConstructor.DPSs.Add(7269,"ScaHaste29")
		SimConstructor.DPSs.Add(7291,"ScaHaste30")
		SimConstructor.DPSs.Add(7325,"ScaHaste31")
		SimConstructor.DPSs.Add(7395,"ScaHaste32")
		SimConstructor.DPSs.Add(7373,"ScaHaste33")
		SimConstructor.DPSs.Add(7436,"ScaHaste34")
		SimConstructor.DPSs.Add(7359,"ScaHaste35")
		SimConstructor.DPSs.Add(7479,"ScaHaste36")
		SimConstructor.DPSs.Add(7425,"ScaHaste37")
		SimConstructor.DPSs.Add(7460,"ScaHaste38")
		SimConstructor.DPSs.Add(7443,"ScaHaste39")
		SimConstructor.DPSs.Add(7524,"ScaHaste40")
		SimConstructor.DPSs.Add(7523,"ScaHaste41")
		SimConstructor.DPSs.Add(7562,"ScaHaste42")
		SimConstructor.DPSs.Add(7607,"ScaHaste43")
		SimConstructor.DPSs.Add(7649,"ScaHaste44")
		SimConstructor.DPSs.Add(7621,"ScaHaste45")
		SimConstructor.DPSs.Add(7688,"ScaHaste46")
		SimConstructor.DPSs.Add(7657,"ScaHaste47")
		SimConstructor.DPSs.Add(7735,"ScaHaste48")
		SimConstructor.DPSs.Add(7740,"ScaHaste49")
		SimConstructor.DPSs.Add(7706,"ScaHaste50")
		
		SimConstructor.DPSs.Add(6447,"ScaCrit0")
		SimConstructor.DPSs.Add(6443,"ScaCrit1")
		SimConstructor.DPSs.Add(6465,"ScaCrit2")
		SimConstructor.DPSs.Add(6510,"ScaCrit3")
		SimConstructor.DPSs.Add(6524,"ScaCrit4")
		SimConstructor.DPSs.Add(6571,"ScaCrit5")
		SimConstructor.DPSs.Add(6596,"ScaCrit6")
		SimConstructor.DPSs.Add(6594,"ScaCrit7")
		SimConstructor.DPSs.Add(6644,"ScaCrit8")
		SimConstructor.DPSs.Add(6654,"ScaCrit9")
		SimConstructor.DPSs.Add(6670,"ScaCrit10")
		SimConstructor.DPSs.Add(6708,"ScaCrit11")
		SimConstructor.DPSs.Add(6717,"ScaCrit12")
		SimConstructor.DPSs.Add(6693,"ScaCrit13")
		SimConstructor.DPSs.Add(6727,"ScaCrit14")
		SimConstructor.DPSs.Add(6749,"ScaCrit15")
		SimConstructor.DPSs.Add(6756,"ScaCrit16")
		SimConstructor.DPSs.Add(6824,"ScaCrit17")
		SimConstructor.DPSs.Add(6839,"ScaCrit18")
		SimConstructor.DPSs.Add(6864,"ScaCrit19")
		SimConstructor.DPSs.Add(6886,"ScaCrit20")
		SimConstructor.DPSs.Add(6908,"ScaCrit21")
		SimConstructor.DPSs.Add(6918,"ScaCrit22")
		SimConstructor.DPSs.Add(6963,"ScaCrit23")
		SimConstructor.DPSs.Add(6966,"ScaCrit24")
		SimConstructor.DPSs.Add(6952,"ScaCrit25")
		SimConstructor.DPSs.Add(6994,"ScaCrit26")
		SimConstructor.DPSs.Add(6993,"ScaCrit27")
		SimConstructor.DPSs.Add(7014,"ScaCrit28")
		SimConstructor.DPSs.Add(7039,"ScaCrit29")
		SimConstructor.DPSs.Add(7042,"ScaCrit30")
		SimConstructor.DPSs.Add(7036,"ScaCrit31")
		SimConstructor.DPSs.Add(7090,"ScaCrit32")
		SimConstructor.DPSs.Add(7090,"ScaCrit33")
		SimConstructor.DPSs.Add(7105,"ScaCrit34")
		SimConstructor.DPSs.Add(7139,"ScaCrit35")
		SimConstructor.DPSs.Add(7165,"ScaCrit36")
		SimConstructor.DPSs.Add(7177,"ScaCrit37")
		SimConstructor.DPSs.Add(7197,"ScaCrit38")
		SimConstructor.DPSs.Add(7226,"ScaCrit39")
		SimConstructor.DPSs.Add(7253,"ScaCrit40")
		SimConstructor.DPSs.Add(7247,"ScaCrit41")
		SimConstructor.DPSs.Add(7259,"ScaCrit42")
		SimConstructor.DPSs.Add(7269,"ScaCrit43")
		SimConstructor.DPSs.Add(7300,"ScaCrit44")
		SimConstructor.DPSs.Add(7348,"ScaCrit45")
		SimConstructor.DPSs.Add(7359,"ScaCrit46")
		SimConstructor.DPSs.Add(7379,"ScaCrit47")
		SimConstructor.DPSs.Add(7401,"ScaCrit48")
		SimConstructor.DPSs.Add(7403,"ScaCrit49")
		SimConstructor.DPSs.Add(7459,"ScaCrit50")
		
		SimConstructor.DPSs.Add(7175,"ScaAgility0")
		SimConstructor.DPSs.Add(7179,"ScaAgility1")
		SimConstructor.DPSs.Add(7192,"ScaAgility2")
		SimConstructor.DPSs.Add(7207,"ScaAgility3")
		SimConstructor.DPSs.Add(7223,"ScaAgility4")
		SimConstructor.DPSs.Add(7237,"ScaAgility5")
		SimConstructor.DPSs.Add(7227,"ScaAgility6")
		SimConstructor.DPSs.Add(7230,"ScaAgility7")
		SimConstructor.DPSs.Add(7242,"ScaAgility8")
		SimConstructor.DPSs.Add(7251,"ScaAgility9")
		SimConstructor.DPSs.Add(7259,"ScaAgility10")
		SimConstructor.DPSs.Add(7316,"ScaAgility11")
		SimConstructor.DPSs.Add(7299,"ScaAgility12")
		SimConstructor.DPSs.Add(7317,"ScaAgility13")
		SimConstructor.DPSs.Add(7348,"ScaAgility14")
		SimConstructor.DPSs.Add(7330,"ScaAgility15")
		SimConstructor.DPSs.Add(7352,"ScaAgility16")
		SimConstructor.DPSs.Add(7403,"ScaAgility17")
		SimConstructor.DPSs.Add(7432,"ScaAgility18")
		SimConstructor.DPSs.Add(7444,"ScaAgility19")
		SimConstructor.DPSs.Add(7433,"ScaAgility20")
		SimConstructor.DPSs.Add(7429,"ScaAgility21")
		SimConstructor.DPSs.Add(7435,"ScaAgility22")
		SimConstructor.DPSs.Add(7450,"ScaAgility23")
		SimConstructor.DPSs.Add(7469,"ScaAgility24")
		SimConstructor.DPSs.Add(7473,"ScaAgility25")
		SimConstructor.DPSs.Add(7439,"ScaAgility26")
		SimConstructor.DPSs.Add(7461,"ScaAgility27")
		SimConstructor.DPSs.Add(7521,"ScaAgility28")
		SimConstructor.DPSs.Add(7521,"ScaAgility29")
		SimConstructor.DPSs.Add(7536,"ScaAgility30")
		SimConstructor.DPSs.Add(7555,"ScaAgility31")
		SimConstructor.DPSs.Add(7559,"ScaAgility32")
		SimConstructor.DPSs.Add(7576,"ScaAgility33")
		SimConstructor.DPSs.Add(7596,"ScaAgility34")
		SimConstructor.DPSs.Add(7611,"ScaAgility35")
		SimConstructor.DPSs.Add(7626,"ScaAgility36")
		SimConstructor.DPSs.Add(7642,"ScaAgility37")
		SimConstructor.DPSs.Add(7643,"ScaAgility38")
		SimConstructor.DPSs.Add(7634,"ScaAgility39")
		SimConstructor.DPSs.Add(7654,"ScaAgility40")
		SimConstructor.DPSs.Add(7670,"ScaAgility41")
		SimConstructor.DPSs.Add(7692,"ScaAgility42")
		SimConstructor.DPSs.Add(7698,"ScaAgility43")
		SimConstructor.DPSs.Add(7696,"ScaAgility44")
		SimConstructor.DPSs.Add(7714,"ScaAgility45")
		SimConstructor.DPSs.Add(7686,"ScaAgility46")
		SimConstructor.DPSs.Add(7745,"ScaAgility47")
		SimConstructor.DPSs.Add(7770,"ScaAgility48")
		SimConstructor.DPSs.Add(7764,"ScaAgility49")
		SimConstructor.DPSs.Add(7777,"ScaAgility50")
		
		SimConstructor.DPSs.Add(7175,"ScaStr0")
		SimConstructor.DPSs.Add(7213,"ScaStr1")
		SimConstructor.DPSs.Add(7248,"ScaStr2")
		SimConstructor.DPSs.Add(7287,"ScaStr3")
		SimConstructor.DPSs.Add(7324,"ScaStr4")
		SimConstructor.DPSs.Add(7360,"ScaStr5")
		SimConstructor.DPSs.Add(7398,"ScaStr6")
		SimConstructor.DPSs.Add(7436,"ScaStr7")
		SimConstructor.DPSs.Add(7471,"ScaStr8")
		SimConstructor.DPSs.Add(7509,"ScaStr9")
		SimConstructor.DPSs.Add(7547,"ScaStr10")
		SimConstructor.DPSs.Add(7582,"ScaStr11")
		SimConstructor.DPSs.Add(7620,"ScaStr12")
		SimConstructor.DPSs.Add(7658,"ScaStr13")
		SimConstructor.DPSs.Add(7693,"ScaStr14")
		SimConstructor.DPSs.Add(7732,"ScaStr15")
		SimConstructor.DPSs.Add(7769,"ScaStr16")
		SimConstructor.DPSs.Add(7804,"ScaStr17")
		SimConstructor.DPSs.Add(7843,"ScaStr18")
		SimConstructor.DPSs.Add(7879,"ScaStr19")
		SimConstructor.DPSs.Add(7917,"ScaStr20")
		SimConstructor.DPSs.Add(7953,"ScaStr21")
		SimConstructor.DPSs.Add(7990,"ScaStr22")
		SimConstructor.DPSs.Add(8027,"ScaStr23")
		SimConstructor.DPSs.Add(8064,"ScaStr24")
		SimConstructor.DPSs.Add(8103,"ScaStr25")
		SimConstructor.DPSs.Add(8138,"ScaStr26")
		SimConstructor.DPSs.Add(8176,"ScaStr27")
		SimConstructor.DPSs.Add(8214,"ScaStr28")
		SimConstructor.DPSs.Add(8249,"ScaStr29")
		SimConstructor.DPSs.Add(8287,"ScaStr30")
		SimConstructor.DPSs.Add(8325,"ScaStr31")
		SimConstructor.DPSs.Add(8361,"ScaStr32")
		SimConstructor.DPSs.Add(8398,"ScaStr33")
		SimConstructor.DPSs.Add(8436,"ScaStr34")
		SimConstructor.DPSs.Add(8472,"ScaStr35")
		SimConstructor.DPSs.Add(8509,"ScaStr36")
		SimConstructor.DPSs.Add(8545,"ScaStr37")
		SimConstructor.DPSs.Add(8583,"ScaStr38")
		SimConstructor.DPSs.Add(8620,"ScaStr39")
		SimConstructor.DPSs.Add(8657,"ScaStr40")
		SimConstructor.DPSs.Add(8694,"ScaStr41")
		SimConstructor.DPSs.Add(8731,"ScaStr42")
		SimConstructor.DPSs.Add(8768,"ScaStr43")
		SimConstructor.DPSs.Add(8805,"ScaStr44")
		SimConstructor.DPSs.Add(8843,"ScaStr45")
		SimConstructor.DPSs.Add(8879,"ScaStr46")
		SimConstructor.DPSs.Add(8915,"ScaStr47")
		SimConstructor.DPSs.Add(8954,"ScaStr48")
		SimConstructor.DPSs.Add(8990,"ScaStr49")
		SimConstructor.DPSs.Add(9028,"ScaStr50")
		
		
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
		
		
		
	End sub
	
	
End Module
