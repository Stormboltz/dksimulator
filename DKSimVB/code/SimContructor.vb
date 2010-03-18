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
	friend simCollection as New Collections.ArrayList
	Public _MainFrm As MainForm
	Sub New()
		
	End Sub
	
	Sub Start(SimTime As Double, MainFrm As MainForm, optional StartNow as Boolean = false)
		Dim  sim As Sim
		Dim newthread As System.Threading.Thread
		Sim = New Sim
		_MainFrm = MainFrm
		
		
		If EpStat <> "" Then
			Sim.Prepare(Simtime, Mainfrm,EPStat,EPBase)
		Else
			Sim.Prepare(Simtime, Mainfrm)
		End If
		newthread = New System.Threading.Thread(AddressOf sim.Start)
		newthread.Priority= Threading.ThreadPriority.BelowNormal
		If StartNow Then
			simCollection.Clear
			newthread.Start()
		end if
		ThreadCollection.Add(newthread)
		simCollection.Add(sim)
	End Sub
	
	
	Sub StartEP(SimTime As Double,MainFrm As MainForm)
		DPSs.Clear
		ThreadCollection.Clear
		simCollection.Clear
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
		
		Dim Str As string
		Dim Agility As string
		Dim MHSpeed As string
		Dim Exp As string
		Dim MHDPS As string
		Dim SpHit As string
		Dim Hit As string
		Dim ArP As string
		Dim Haste As string
		Dim Crit As String
		
		Str =	0
		Agility 	=	0
		MHSpeed 	=	0
		Exp 	=	0
		MHDPS 	=	0
		SpHit	=	0
		Hit 	=	0
		ArP 	=	0
		Haste 	=	0
		Crit  	=	0

		
		
		If SimTime = 0 Then SimTime = 1
		'Create EP table
		sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"
		
		If  doc.SelectSingleNode("//config/Stats").InnerText.Contains("True")=false Then
			goto skipStats
		End If
		
		'Dry run
		EPStat="EP DryRun"
		SimConstructor.Start(SimTime,MainFrm)
		Application.DoEvents
		
		EPStat="EP AttackPower"
		SimConstructor.Start(SimTime,MainFrm)
		
		if doc.SelectSingleNode("//config/Stats/chkEPStr").InnerText = "True" then
			EPStat="EP Strength"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPAgility").InnerText = "True" then
			EPStat="EP Agility"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPCrit").InnerText = "True" then
			EPStat="EP CritRating"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPHaste").InnerText = "True" then
			EPStat="EP HasteRating1"
			SimConstructor.Start(SimTime,MainFrm)
			EPStat="EP HasteEstimated"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPArP").InnerText = "True" then
			EPStat="EP ArmorPenetrationRating"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		
		
		if doc.SelectSingleNode("//config/Stats/chkEPExp").InnerText = "True" then
			EPStat="EP ExpertiseRating"
			SimConstructor.Start(SimTime,MainFrm)
			EPStat="EP ExpertiseRatingCap"
			SimConstructor.Start(SimTime,MainFrm)
			EPStat="EP ExpertiseRatingCapAP"
			SimConstructor.Start(SimTime,MainFrm)
			
			EPStat="EP RelativeExpertiseRating"
			SimConstructor.Start(SimTime,MainFrm)
			
			
			If MainFrm.cmdPresence.SelectedItem = "Frost" Then
				EPStat="EP ExpertiseRatingAfterCap"
				SimConstructor.Start(SimTime,MainFrm)
			End If
		End If

		if doc.SelectSingleNode("//config/Stats/chkEPHit").InnerText = "True" then
			EPStat="EP HitRating"
			SimConstructor.Start(SimTime,MainFrm)
			EPStat="EP HitRatingCap"
			SimConstructor.Start(SimTime,MainFrm)
			EPStat="EP HitRatingCapAP"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSpHit").InnerText = "True" then
			EPStat="EP SpellHitRating"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSMHDPS").InnerText = "True" then
			EPStat="EP WeaponDPS"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Stats/chkEPSMHSpeed").InnerText = "True" then
			EPStat="EP WeaponSpeed"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		Dim tmpInt As Integer
		tmpInt = EPBase
		EPBase= 20
		if doc.SelectSingleNode("//config/Stats/chkEPAfterSpellHitRating").InnerText = "True" then
			EPStat="EP AfterSpellHitBase"
			SimConstructor.Start(SimTime,MainFrm)
			EPStat="EP AfterSpellHitBaseAP"
			SimConstructor.Start(SimTime,MainFrm)
			EPStat="EP AfterSpellHitRating"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		EPBase = tmpInt 
		Jointhread

		EPStat = "EP DryRun"
		BaseDPS = dpss(EPStat)
		
		EPStat = "EP AttackPower"
		APDPS = dpss(EPStat)
		sReport = sReport +  ("<tr><td>" & EPStat & " | 1 (" & toDDecimal((APDPS-BaseDPS ) / (2*EPBase)) & " DPS/per AP) </td></tr>")
		
		Try
			EPStat="EP Strength"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) /  (2*EPBase)
			tmp2 = (DPS-BaseDPS) / EPBase
			Str = toDDecimal (tmp2/tmp1)
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		catch
		End Try
		Try
			EPStat="EP Agility"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / EPBase
			Agility = toDDecimal (tmp2/tmp1)
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		catch
		End Try
		Try
			EPStat="EP CritRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / EPBase
			Crit = toDDecimal (tmp2/tmp1)
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try
			EPStat="EP HasteEstimated"
			DPS = DPSs(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / EPBase
			Haste = toDDecimal (tmp2/tmp1)
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		Catch
			
		End Try
		
		Try
			EPStat="EP HasteRating1"
			DPS = DPSs(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		Catch
			
		End Try
		Try
			EPStat="EP ArmorPenetrationRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / EPBase
			ArP = toDDecimal (tmp2/tmp1)
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP ExpertiseRating"
			DPS = dpss(EPStat)
			
			
			tmp1 = (dpss("EP ExpertiseRatingCapAP")-dpss("EP ExpertiseRatingCap") ) / (2*EPBase)
			tmp2 = (DPS-dpss("EP ExpertiseRatingCap")) / EPBase
			Exp = toDDecimal (-tmp2/tmp1)
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			
			EPStat="EP RelativeExpertiseRating"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>Personal Expertise value | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			
			
			
			'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try
			EPStat="EP ExpertiseRatingAfterCap"
			DPS = dpss(EPStat)
			tmp1 = (dpss("EP ExpertiseRatingCapAP")-dpss("EP ExpertiseRatingCap") ) / (2*EPBase)
			tmp2 = (DPS-dpss("EP ExpertiseRatingCap")) / EPBase
			sReport = sReport +  ("<tr><td>ExpertiseRating After Dodge Cap | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		
		Try
			EPStat="EP HitRating"
			DPS = dpss(EPStat)
			tmp1 = (dpss("EP HitRatingCapAP")-dpss("EP HitRatingCap")) / (2*EPBase)
			tmp2 = (DPS-dpss("EP HitRatingCap")) / EPBase
			Hit = toDDecimal (-tmp2/tmp1)
			sReport = sReport +  ("<tr><td>BeforeMeleeHitCap<8% | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		
		Try
			EPStat="EP SpellHitRating"
			DPS = dpss(EPStat)
			tmp1 = (dpss("EP HitRatingCapAP")-dpss("EP HitRatingCap")) / (2*20)
			tmp2 = (DPS-dpss("EP HitRatingCap")) / 20
			SpHit = toDDecimal (tmp2/tmp1)
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
		catch
		End Try
		Try
			EPStat="EP WeaponDPS"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / 10
			MHDPS = toDDecimal (tmp2/tmp1)
			sReport = sReport +  ("<tr><td>" & EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		End Try
		Try
			EPStat="EP WeaponSpeed"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / 0.1
			MHSpeed = toDDecimal (tmp2/tmp1)
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
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS) / EPBase
			sReport = sReport +  ("<tr><td>After spell hit cap | " & toDDecimal (tmp2/tmp1) & "</td></tr>")
			'	WriteReport ("Average for " & EPStat & " | " & DPS)
		catch
		end try
		
		EPStat = ""
		
		skipStats:
		DPSs.Clear
		ThreadCollection.Clear
		simCollection.Clear
		If  doc.SelectSingleNode("//config/Sets").InnerText.Contains("True")=false Then
			goto skipSets
		End If
		
		EPStat="EP 0T7"
		SimConstructor.Start(SimTime,MainFrm)
		
		EPStat="EP AttackPower0T7"
		SimConstructor.Start(SimTime,MainFrm)
		
		if doc.SelectSingleNode("//config/Sets/chkEP2T7").InnerText = "True" then
			EPStat="EP 2T7"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT7").InnerText = "True" then
			EPStat="EP 4T7"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT8").InnerText = "True" then
			EPStat="EP 2T8"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT8").InnerText = "True" then
			EPStat="EP 4T8"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT9").InnerText = "True" then
			EPStat="EP 2T9"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT9").InnerText = "True" then
			EPStat="EP 4T9"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP2PT10").InnerText = "True" then
			EPStat="EP 2T10"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		if doc.SelectSingleNode("//config/Sets/chkEP4PT10").InnerText = "True" then
			EPStat="EP 4T10"
			SimConstructor.Start(SimTime,MainFrm)
		End If
		Jointhread

		
		EPStat = "EP 0T7"
		BaseDPS = dpss(EPStat)
		
		EPStat = "EP AttackPower0T7"
		APDPS = dpss(EPStat)
		
		Try
			EPStat="EP 2T7"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS)/ (2*EPBase)
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"

		catch
		End Try
		Try
			EPStat="EP 4T7"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS)/ (2*EPBase)
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"

		catch
		End Try
		Try
			EPStat="EP 2T8"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS)/ (2*EPBase)
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"

		catch
		End Try
		Try
			EPStat="EP 4T8"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS)/ (2*EPBase)
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"

		catch
		End Try
		Try
			EPStat="EP 2T9"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS)/ (2*EPBase)
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"

		catch
		End Try
		Try
			EPStat="EP 4T9"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS)/ (2*EPBase)
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
		catch
		End Try
		Try
			EPStat="EP 2T10"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS)/ (2*EPBase)
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
		Catch
		End Try
		Try
			EPStat="EP 4T10"
			DPS = dpss(EPStat)
			tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
			tmp2 = (DPS-BaseDPS)/ (2*EPBase)
			sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
		catch
		End Try
		
		WriteReport ("")
		
		skipSets:
		
		
		If  doc.SelectSingleNode("//config/Trinket").InnerText.Contains("True")=false Then
			goto skipTrinket
		End If
		
		EPStat="EP NoTrinket"
		SimConstructor.Start(SimTime,MainFrm)
		
		EPStat="EP AttackPowerNoTrinket"
		SimConstructor.Start(SimTime,MainFrm)
		
		doc.Load("EPconfig.xml")
		Dim trinketsList As Xml.XmlNode
		dim tNode as Xml.XmlNode
		trinketsList = doc.SelectsingleNode("//config/Trinket")
		
		For Each tNode In trinketsList.ChildNodes
			If tNode.InnerText = "True" Then
				EPStat= tNode.Name.Replace("chkEP","EP ")
				SimConstructor.Start(SimTime,MainFrm)
			End If
		Next
		Jointhread

		
		EPStat = "EP NoTrinket"
		BaseDPS = dpss(EPStat)
		
		EPStat = "EP AttackPowerNoTrinket"
		APDPS = dpss(EPStat)
		
		
		For Each tNode In trinketsList.ChildNodes
			If tNode.InnerText = "True" Then
				Try
					EPStat= tNode.Name.Replace("chkEP","EP ")
					DPS = dpss(EPStat)
					tmp1 = (APDPS-BaseDPS ) / (2*EPBase)
					tmp2 = (DPS-BaseDPS)/ (2*EPBase)
					sReport = sReport +  ("<tr><td>"& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
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
		sReport = sReport &   "<tr><td COLSPAN=8> | RuneEnchant | " & _MainFrm.cmbRuneMH.Text  & " / " & _MainFrm.cmbRuneOH.Text  & "</td></tr>"
		sReport = sReport &   "<tr><td COLSPAN=8> | Pet Calculation | " & _MainFrm.ckPet.Checked & "</td></tr>"
		Str 	=	Str.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		Agility =	Agility.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		MHSpeed =	MHSpeed.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		Exp 	=	Exp.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		MHDPS 	=	MHDPS.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		SpHit	=	SpHit.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		Hit 	=	Hit.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		ArP 	=	ArP.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		Haste 	=	Haste.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		Crit  	=	Crit.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,".")
		
		
		
		
		Dim lootlink As String
		lootlink = "<tr><td COLSPAN=8><a href=http://www.guildox.com/wr.asp?Cla=2048&s7=3.2&str=" & Str & "&Arm=" & 0.028 & "&mh=" & Haste & "&dps=" & MHDPS & "&mcr=" & Crit & _
			"&odps=" & 0 & "&Agi=" & Agility & "&mhit=" & Hit & "&map=1" & "&msp=" & MHSpeed & "&arp=" & ArP & "&osp=0" & "&Exp=" & Exp & " target='_blank'>lootlink non hit caped</a></td></tr>"
		sReport = sReport & lootlink
		
		lootlink = "<tr><td COLSPAN=8><a href=http://www.guildox.com/wr.asp?Cla=2048&s7=3.2&str=" & Str & "&Arm=" & 0.028 & "&mh=" & Haste & "&dps=" & MHDPS & "&mcr=" & Crit & _
			"&odps=" & 0 & "&Agi=" & Agility & "&mhit=" & SpHit & "&map=1" & "&msp=" & MHSpeed & "&arp=" & ArP & "&osp=0" & "&Exp=" & Exp & " target='_blank'>lootlink hit caped</a></td></tr>"
		sReport = sReport & lootlink	
		Dim pwan As String
		pwan  = "<tr><td COLSPAN=8>Non hit caped ( Pawn: v1: "+chr(34)+"DK Sim"+chr(34)+": ArmorPenetration="+ArP+", HitRating="+Hit+", CritRating="+crit+", Dps="+MHDPS+", Strength="+Str+", Armor=0.028, Agility="+Agility+", HasteRating="+Haste+", Speed="+MHSpeed+", ExpertiseRating="+Exp+", Ap=1, GemQualityLevel=82 )</td></tr>"		
		sReport = sReport + pwan
		pwan  = "<tr><td COLSPAN=8>hit caped ( Pawn: v1: "+chr(34)+"DK Sim"+chr(34)+": ArmorPenetration="+ArP+", HitRating="+SpHit+", CritRating="+crit+", Dps="+MHDPS+", Strength="+Str+", Armor=0.028, Agility="+Agility+", HasteRating="+Haste+", Speed="+MHSpeed+", ExpertiseRating="+Exp+", Ap=1, GemQualityLevel=82 )</td></tr>"		
		sReport = sReport + pwan
		sReport = sReport +   ("<hr width='80%' align='center' noshade ></hr>")
		
		
		sReport = sReport +  ("</table>")
		
		WriteReport(sReport)
		EPStat = ""
		
	End Sub
	
	
	Sub StartScaling(pb As ProgressBar,SimTime As Double,MainFrm As MainForm)
		
		DPSs.Clear
		ThreadCollection.Clear
		simCollection.Clear
		EPBase = 50
		_MainFrm = MainFrm
		dim sReport as String
		Dim doc As xml.XmlDocument = New xml.XmlDocument

		
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
					SimConstructor.Start(1,MainFrm)
				Next i
				Jointhread
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
		dim sReport as String
		
		'on error resume next
		DPSs.Clear
		ThreadCollection.Clear
		simCollection.Clear
		EPBase = 50
		_MainFrm = MainFrm
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument

		doc.Load(Application.StartupPath & "\Templates\" & MainFrm.cmbTemplate.Text)
		Dim xNodelist As Xml.XmlNode
		xNodelist = doc.SelectSingleNode("//Talents")
		Dim xNode As Xml.XmlNode
		
		EpStat = "OriginalSpec"
		SimConstructor.Start(MainFrm.txtSimtime.Text,MainFrm)
		
		
		
		For Each xNode In xNodelist.ChildNodes
			If (xNode.Name <> "URL" and xNode.Name <> "Glyphs") and xNode.InnerText <> "0" Then
				EpStat = xNode.Name
				SimConstructor.Start(MainFrm.txtSimtime.Text,MainFrm)
			End If
		Next
		Jointhread
		
		dim BaseDPS as Integer
		EpStat = "OriginalSpec"
		BaseDPS = dpss(EPStat)
		
		sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"
		
		sReport += "<tr><td>Average for " & EPStat & " | " & BaseDPS & "</tr></td>"
		
		
		For Each xNode In xNodelist.ChildNodes
			If (xNode.Name <> "URL" and xNode.Name <> "Glyphs") and xNode.InnerText <> "0" Then
				EpStat = xNode.Name
				sReport += "<tr><td>Value for " & EPStat & " | " & toDDecimal((BaseDPS - dpss(EPStat))/xNode.InnerText) & "</tr></td>"
			End If
		Next
		sReport += "</table>"
		sReport += "<hr width='80%' align='center' noshade ></hr>"
		WriteReport(sReport)
		EpStat = ""
	End Sub
	
	Sub RemoveStoppedthread
		Dim j As Integer
		Dim t As Threading.Thread
		For j=0 To ThreadCollection.Count-1
				t = ThreadCollection.Item(j)
				If t.ThreadState = Threading.ThreadState.Stopped Then
						ThreadCollection.Remove(t)
						RemoveStoppedthread
						exit sub
				End If
		Next
	End Sub
	
	
	
	Sub Jointhread()
		Dim t As Threading.Thread
		Dim core As Integer =  Environment.ProcessorCount
		Dim i As Integer
		Do Until ThreadCollection.Count = 0
			RemoveStoppedthread
			i=0
			For Each t In ThreadCollection
				Application.DoEvents
				If i < core Then
					If t.ThreadState = Threading.ThreadState.Unstarted Then t.Start
				End If
				If i = 0  Then
					t.Join(100)
					_MainFrm.UpdateProgressBar
				End If
				i += 1
			Next
		Loop	
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
