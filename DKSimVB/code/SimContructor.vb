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

	Friend PetFriendly As Boolean
	Friend Rotate as Boolean
	Friend ReportPath As String
	Friend EpStat As String
	Friend _MainFrm As MainForm
	Friend DPSs as new Collection
	Friend sThreadCollection as new Collection
	Friend EPBase as Integer
	Friend ThreadCollection as new Collections.ArrayList
	Sub New()
		
	End Sub
	
	Sub Start(pb As ProgressBar,SimTime As Double, MainFrm As MainForm)
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
		
		if doc.SelectSingleNode("//config/Stats/chkEPAfterSpellHitRating").InnerText = "True" then
			EPStat="AfterSpellHitBase"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="AfterSpellHitBaseAP"
			SimConstructor.Start(pb,SimTime,MainFrm)
			EPStat="AfterSpellHitRating"
			SimConstructor.Start(pb,SimTime,MainFrm)
		End If
		
		
		
		'This seems to have the same effect as AreMyStrheadFinninshed, without an ugly loop.
		Dim T as Threading.Thread
		For Each T In ThreadCollection
			T.Join()
		Next
'		Do Until AreMyStrheadFinninshed
'			Application.DoEvents
'		Loop

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
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="Agility"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="CritRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="HasteRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="ArmorPenetrationRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="ExpertiseRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | BeforeMeleeHitCap<8% | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="SpellHitRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 26
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="WeaponDPS"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (sim.DPS-BaseDPS) / 10
			sReport = sReport +  ("<tr><td>EP:" & "10" & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="WeaponSpeed"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 0.1
			sReport = sReport +  ("<tr><td>EP:" & "0.1" & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | After spell hit cap | " & toDDecimal (tmp2/tmp1) & "</td></tr>")
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
		
		For Each T In ThreadCollection
			T.Join()
		Next
'		Do Until AreMyStrheadFinninshed
'			Application.DoEvents
'		Loop
		
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
		max = 50
		EPBase = 20
		
		sReport = sReport +  ("<tr><td>Stat</td>")
		For i=0 To max
			sReport = sReport & "<td>" & EPBase*i & "</td>"
		Next
		sReport = sReport +  ("</tr>")
		
		
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
				sReport = sReport +  ("<tr><td>" & EpStat & "</td>")
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
		Sub FakeResultBulder
		'	EpStat= Replace(xNode.Name,"chk","")
			
'			Stat 0 20 40 60 80 100 120 140 160 180 200 220 240 260 280 300 320 340 360 380 400 420 440 460 480 500 520 540 560 580 600 620 640 660 680 700 720 740 760 780 800 820 840 860 880 900 920 940 960 980 1000 
'			ScaExp 6947 6977 6980 7023 7004 7097 7088 7068 7131 7133 7186 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 
'			ScaHit 6849 6911 6901 6928 6964 6946 7048 7068 7065 7030 7120 7094 7163 7212 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 7175 
'			ScaArP 6953 6966 6980 6993 7007 7021 7035 7049 7063 7077 7091 7106 7121 7135 7151 7166 7181 7196 7212 7228 7244 7260 7276 7293 7310 7326 7344 7361 7378 7396 7413 7431 7450 7468 7487 7505 7525 7544 7563 7583 7603 7623 7644 7664 7685 7706 7728 7749 7771 7794 7816 
'			ScaHaste 6598 6644 6642 6661 6687 6695 6710 6746 6789 6834 6859 6868 6878 6935 6935 6945 7032 6999 7037 7069 7078 7142 7090 7132 7175 7176 7210 7216 7257 7269 7291 7325 7395 7373 7436 7359 7479 7425 7460 7443 7524 7523 7562 7607 7649 7621 7688 7657 7735 7740 7706 
'			ScaCrit 6447 6443 6465 6510 6524 6571 6596 6594 6644 6654 6670 6708 6717 6693 6727 6749 6756 6824 6839 6864 6886 6908 6918 6963 6966 6952 6994 6993 7014 7039 7042 7036 7090 7090 7105 7139 7165 7177 7197 7226 7253 7247 7259 7269 7300 7348 7359 7379 7401 7403 7459 
'			ScaAgility 7175 7179 7192 7207 7223 7237 7227 7230 7242 7251 7259 7316 7299 7317 7348 7330 7352 7403 7432 7444 7433 7429 7435 7450 7469 7473 7439 7461 7521 7521 7536 7555 7559 7576 7596 7611 7626 7642 7643 7634 7654 7670 7692 7698 7696 7714 7686 7745 7770 7764 7777 
'			ScaStr 7175 7213 7248 7287 7324 7360 7398 7436 7471 7509 7547 7582 7620 7658 7693 7732 7769 7804 7843 7879 7917 7953 7990 8027 8064 8103 8138 8176 8214 8249 8287 8325 8361 8398 8436 8472 8509 8545 8583 8620 8657 8694 8731 8768 8805 8843 8879 8915 8954 8990 9028 

		End Sub
		sub createGraph()
		Dim pg As Bitmap = New Bitmap((500),(900))
		Dim gr As Graphics = Graphics.FromImage(pg)

		Dim doc As xml.XmlDocument = New xml.XmlDocument

		
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
					y1 = DPSs(EpStat & i)/10
					x2 = (i+1)*10
					y2 = DPSs(EpStat & i+1)/10
					gr.DrawLine(pen,x1,y1,x2,y2)
				Next i
			End If
		Next
		pg.Save("myScaling.jpeg",imaging.ImageFormat.Jpeg)
	End sub
	
End Module
