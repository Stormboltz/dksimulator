Friend Module Sim
	Friend TotalDamage As Long
	Friend TotalDamageAlternative As Long
	Friend NextFreeGCD As Long
	Friend Lag As Long
	Friend TimeStamp As Long
	Friend TimeStampCounter As Long
	Friend EPStat As String
	Friend EPBase as Integer
	Private DPS As Long
	Friend MaxTime As Long
	Friend NumberOfFights As Long
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
	Friend KeepRNGSeed As Boolean
	
	Friend RandomNumberGenerator as RandomNumberGenerator
	
	Friend Runes as runes
	Friend Rune1 As Rune1
	Friend Rune2 As Rune2
	Friend Rune3 As Rune3
	Friend Rune4 As Rune4
	Friend Rune5 As Rune5
	Friend Rune6 as Rune6
	
	Friend RunicPower As RunicPower
	Friend Character as Character
	Friend MainStat as MainStat
	Friend Sigils as Sigils
	
	
	'Strike Creation
	Friend BloodCakedBlade As BloodCakedBlade
	Friend BloodStrike As BloodStrike
	Friend DeathStrike As DeathStrike
	Friend FrostStrike As FrostStrike
	Friend HeartStrike As HeartStrike
	Friend Obliterate As Obliterate
	Friend PlagueStrike As PlagueStrike
	Friend ScourgeStrike As ScourgeStrike
	Friend MainHand as MainHand
	Friend OffHand As OffHand
	Friend RuneStrike As RuneStrike
	Friend GhoulStat  As GhoulStat
	Friend Ghoul as Ghoul
	
	'Spell Creation
	Friend BloodBoil As BloodBoil
	Friend BloodTap As BloodTap
	Friend Butchery As Butchery
	Friend DeathandDecay as DeathandDecay
	Friend DeathChill As DeathChill
	Friend Desolation as Desolation
	Friend Horn As Horn
	Friend HowlingBlast As HowlingBlast
	Friend Hysteria  as Hysteria
	Friend IcyTouch As IcyTouch
	Friend Necrosis As Necrosis
	Friend DeathCoil as DeathCoil
	Friend Pestilence as Pestilence
	Friend UnbreakableArmor as UnbreakableArmor
	Friend UnholyBlight as UnholyBlight
	Friend Bloodlust as Bloodlust
	Friend DRW As DRW
	Friend WanderingPlague as WanderingPlague
	Friend Gargoyle as Gargoyle
	
	
	'Disease Creation
	Friend BloodPlague as BloodPlague
	Friend FrostFever as FrostFever
	
	Friend proc As proc
	Friend Buff As buff
	Friend Glyph As Glyph
	
	Friend Priority As Priority
	Friend Rotation as Rotation
	Friend RuneForge As RuneForge
	
	Friend Trinket as Trinket
	
	Function NumDesease() As Integer
		NumDesease = 0
		If BloodPlague.isActive(Sim.TimeStamp) Then NumDesease = NumDesease + 1
		If FrostFever.isActive(Sim.TimeStamp) Then NumDesease = NumDesease + 1
		If (TalentUnholy.EbonPlaguebringer + TalentUnholy.CryptFever >= 1) And NumDesease >= 1 Then NumDesease = NumDesease + 1
		
	End Function
	
	Sub StartEP(pb As ProgressBar,EPCalc As Boolean,SimTime As double,MainFrm As MainForm)
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
		XmlDoc.Load(GetFilePath(_MainFrm.cmbCharacter.Text) )
		
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
		sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | 1</td></tr>")
		
		'Str
		if doc.SelectSingleNode("//config/Stats/chkEPStr").InnerText = "True" then
			EPStat="Strength"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for ExpertiseRating | 0")
		End If
		
		'Hit
		if doc.SelectSingleNode("//config/Stats/chkEPHit").InnerText = "True" then
			EPStat="HitRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / sim.EPBase
			'sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (-100*tmp2/tmp1)) & "</td></tr>"
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | BeforeMeleeHitCap<8% | " & toDDecimal (-tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for HitRating | 0")
		End If
		
		'SpHit
		dim SPHitDps as Integer
		if doc.SelectSingleNode("//config/Stats/chkEPSpHit").InnerText = "True" then
			EPStat="SpellHitRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 26
			SPHitDps = DPS
			'sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			sReport = sReport +  ("<tr><td>EP:" & 26 & " | AfterMeleeHitCap | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for SpellHitRating | 0")
		End If
		
		if doc.SelectSingleNode("//config/Stats/chkEPAfterSpellHitRating").InnerText = "True" then
			EPStat="AfterSpellHitRating"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-SPHitDps) / 50
			'sReport = sReport +  ("<tr><td>EP:" & EPBase & " | "& EPStat & " | " & int (100*tmp2/tmp1)) & "</td></tr>"
			sReport = sReport +  ("<tr><td>EP:" & EPBase & " | AfterSpellHitCap | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
			WriteReport ("Average for " & EPStat & " | " & DPS)
		Else
			WriteReport ("Average for AfterSpellHitRating | 0")
		End If
		
		
		
		
		'WeapDPS
		if doc.SelectSingleNode("//config/Stats/chkEPSMHDPS").InnerText = "True" then
			EPStat="WeaponDPS"
			Start(pb,SimTime,MainFrm)
			tmp1 = (APDPS-BaseDPS ) / 100
			tmp2 = (DPS-BaseDPS) / 10
			sReport = sReport +  ("<tr><td>EP:" & "10" & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & "0.1" & " | "& EPStat & " | " & toDDecimal (tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
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
			sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal (100*tmp2/tmp1)) & "</td></tr>"
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
	
	
	function StartEPTrinket(pb As ProgressBar,EPCalc As Boolean,SimTime As double,MainFrm As MainForm) as string
		Dim BaseDPS As long
		Dim APDPS As Long
		Dim tmp1 As double
		Dim tmp2 As Double
		dim sReport as String
		sReport = ""
		'
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
				sReport = sReport +  ("<tr><td>EP:" & " | "& EPStat & " | " & toDDecimal(100*tmp2/tmp1)) & "</td></tr>"
				WriteReport ("Average for " & EPStat & " | " & DPS)
			Else
				'WriteReport ("Average for " & EPStat & " | 0")
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
	
	Sub Start(pb As ProgressBar,SimTime As double, MainFrm As MainForm)
		Rnd(-1) 'Tell VB to initialize using Randomize's parameter
		RandomNumberGenerator = new RandomNumberGenerator 'init here, so that we don't get the same rng numbers for short fights.
		
		_MainFrm = MainFrm
		'combatlog.LogDetails = true
		SimStart = now
		
		MaxTime = SimTime * 60 * 60 * 100
		pb.Maximum = SimTime * 60 * 60 * 100
		
		TotalDamageAlternative = 0
		TimeStampCounter = 1
		If _MainFrm.chkManyFights.Checked Then
			NumberOfFights = Math.Round( ( SimTime * 60 * 60 ) / _MainFrm.txtManyFights.text )
			Debug.Print( "SimTime = " & SimTime * 60 * 60)
			Debug.Print( "_MainFrm.txtManyFights.text = " & _MainFrm.txtManyFights.text)
			MaxTime = _MainFrm.txtManyFights.text * 100
			Debug.Print( "NumberOfFights = " & NumberOfFights)
		Else
			NumberOfFights = 1
		End If
		
		Dim intCount As Integer
		For intCount = 1 To NumberOfFights
			'Init
			Initialisation
			TimeStamp = 1
			
			If Rotate Then Rotation.loadRotation
			
			Do Until TimeStamp > MaxTime
				Timestamp = FastFoward(Timestamp)
				
				If MainStat.FrostPresence = 1 Then
					If TalentBlood.ScentOfBlood > 0 Then
						If sim.proc.ScentOfBloodCD < TimeStamp Then
							proc.GetUseScentOfBlood(TimeStamp)
						End If
					End If
				End If
				
				application.DoEvents
				
				If AMSTimer < TimeStamp Then
					AMSTimer = TimeStamp + AMSCd
					RunicPower.add(AMSAmount)
				End If
				
				If talentblood.Butchery > 0 And sim.Butchery.nextTick <= TimeStamp Then
					Butchery.apply(TimeStamp)
				End If
				
				If true then 'InterruptTimer > TimeStamp Or InterruptAmount == 0 Then 'Interrupt fighting every InterruptCd secs
					If Bloodlust.IsAvailable(TimeStamp) And TimeStamp > 500 Then
						Bloodlust.use(TimeStamp)
					End If
					
					if TalentBlood.DRW = 1 then
						If Sim.isInGCD(TimeStamp) = False Then
							If DRW.cd < TimeStamp And RunicPower.Value  >= 60 And CanUseGCD(TimeStamp) Then
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
								If Gargoyle.cd < TimeStamp and RunicPower.Value >= 60 and CanUseGCD(TimeStamp) Then
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
							Rotation.DoRoration(TimeStamp)
						else
							sim.Priority.DoNext (TimeStamp)
						End If
					End If
					
					If Sim.isInGCD(TimeStamp) = False Then
						If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
							sim.UnbreakableArmor.Use(TimeStamp)
						End If
					End If
					
					If sim.PetFriendly Then
						If Sim.isInGCD(TimeStamp) = False Then
							If Ghoul.ActiveUntil < TimeStamp and Ghoul.cd < TimeStamp and CanUseGCD(TimeStamp) Then
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
					If horn.isAvailable(TimeStamp) and CanUseGCD(TimeStamp) Then
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
				
				If NumberOfFights = 1 and ShowDpsTimer <= TimeStamp Then
					ShowDpsTimer = TimeStamp + 0.1 * 60 * 60 * 100
					TotalDamage = ScourgeStrike.total + obliterate.total + PlagueStrike.total + _
						BloodStrike.total + HeartStrike.total + frostfever.total + _
						BloodPlague.total + IcyTouch.total + deathcoil.total + _
						UnholyBlight.total + Necrosis.total + BloodCakedBlade.total + _
						WanderingPlague.total +FrostStrike.total  +HowlingBlast.total + _
						BloodBoil.total  + DeathStrike.total + MainHand.total + _
						OffHand.total  + Ghoul.total + Gargoyle.total + DRW.total + _
						sim.RuneForge.RazoriceTotal + DeathandDecay.total + RuneStrike.total  + trinket.Total
					_MainFrm.lblDPS.Text = todecimal(100 * TotalDamage /TimeStamp) & " DPS"
				If TimeStamp <= pb.Maximum Then pb.Value = TimeStamp Else pb.Value = pb.Maximum
				ElseIf ShowDpsTimer <= TimeStamp Then
					ShowDpsTimer = TimeStamp + 0.1 * 60 * 60 * 100
					_MainFrm.lblDPS.Text = "n/a"
				If TimeStampCounter <= pb.Maximum Then pb.Value = TimeStampCounter Else pb.Value = pb.Maximum
				End If
			Loop
			
			TotalDamage = ScourgeStrike.total + obliterate.total + PlagueStrike.total + _
				BloodStrike.total + HeartStrike.total + frostfever.total + _
				BloodPlague.total + IcyTouch.total + deathcoil.total + _
				UnholyBlight.total + Necrosis.total + BloodCakedBlade.total + _
				WanderingPlague.total +FrostStrike.total  +HowlingBlast.total + _
				BloodBoil.total  + DeathStrike.total + MainHand.total + _
				OffHand.total  + Ghoul.total + Gargoyle.total + DRW.total + _
				sim.RuneForge.RazoriceTotal + DeathandDecay.total + RuneStrike.total + trinket.Total
			
			TotalDamageAlternative = TotalDamageAlternative + TotalDamage
			TimeStampCounter = TimeStampCounter + TimeStamp
		Next intCount
		
		TotalDamage = TotalDamageAlternative
		TimeStamp = TimeStampCounter
		
		DPS = 100 * TotalDamage / TimeStamp
		pb.Value = pb.Maximum
		
		If NumberOfFights > 1 then
			WriteReport ("DPS: " & DPS)
		Else
			Report()
		End If
		_MainFrm.lblDPS.Text = DPS & " DPS"
		Debug.Print( "DPS=" & DPS & " " & EPStat & " hit=" & mainstat.Hit & " sphit=" & mainstat.SpellHit & " exp=" & mainstat.expertise )
		combatlog.finish
	End Sub
	
	Function isInGCD(T As long ) As Boolean
		If NextFreeGCD <= T Then
			isInGCD = False
		Else
			isInGCD = True
		End If
	End Function
	
	Function CanUseGCD(T As Long) As Boolean
		CanUseGCD=true
		If glyph.Disease Then
			dim tGDC as long
			'return false
			If MainStat.UnholyPresence Then
				tGDC = 100+ sim._MainFrm.txtLatency.Text/10 + 50
			Else
				tGDC =  150+ sim._MainFrm.txtLatency.Text/10 + 50
			End If
			
			If math.Min(sim.BloodPlague.FadeAt,sim.FrostFever.FadeAt) < (T +  tGDC) Then
				'debug.Print (RuneState & "time left on disease= " & (math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) -T)/100 & "s" & " - " & T/100)
				return false
			End If
		End If
	End Function
	
	
	
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
		
		Glyph = new glyph(file)
	End Sub
	
	Function DoMyStrikeHit As Boolean
		Dim RNG As Double
		RNG = RandomNumberGenerator.RNGStrike
		If MainStat.FrostPresence = 1 Then
			If math.Min(mainstat.Expertise,0.065)+ math.Min(mainstat.Expertise,0.14) + math.Min (mainstat.Hit,0.08) + RNG < 0.285 Then
				Return False
			Else
				return true
			End If
		Else
			If math.Min(mainstat.Expertise,0.065) + math.Min (mainstat.Hit,0.08) + RNG < 0.145 Then
				Return False
			Else
				return true
			End If
		End If
		
	End Function
	
	Function DoMySpellHit As Boolean
		Dim RNG As Double
		RNG = RandomNumberGenerator.RNGStrike
		If math.Min(mainstat.SpellHit,0.17) + RNG < 0.17 Then
			Return False
		Else
			return true
		End If
	End Function
	
	Function DoMyWhiteHit As Boolean
		'unused
	End Function
	
	Sub Initialisation()
		'RandomNumberGenerator.Init 'done in Start

		Buff = New Buff
		'Keep this order for RuneX -> Runse -> Rotation/Prio
		Rune1 = New Rune1
		Rune2 = New Rune2
		Rune3 = New Rune3
		Rune4 = New Rune4
		Rune5 = New Rune5
		Rune6 = new Rune6
		Runes = New runes
		
		RunicPower = New RunicPower

		Rotation = new Rotation
		Priority = New Priority
		Character = new Character
		MainStat = new MainStat
		' sim.Buff.UnBuff
		BloodPlague = new BloodPlague
		FrostFever = New FrostFever
		
		UnholyBlight = New UnholyBlight
	
		BloodTap = new BloodTap
		HowlingBlast = New HowlingBlast
		Ghoul = new Ghoul
		GhoulStat = New GhoulStat
		Hysteria = new Hysteria
		DeathChill = new DeathChill
		Desolation = new Desolation
		UnbreakableArmor = new UnbreakableArmor
		RuneForge = new RuneForge
		Butchery = new Butchery
		DRW = new DRW
		RuneStrike = New RuneStrike
		
		
		Sigils = new Sigils
		
		LoadConfig
		
		
		
		
		
		RunicPower.Value = 0
		
	
		NextFreeGCD = 0
		TotalDamage = 0
		Threat = 0
		ScourgeStrike = new ScourgeStrike
		Obliterate = new Obliterate
		PlagueStrike= new PlagueStrike
		BloodStrike = New BloodStrike
		MainHand = New MainHand
		OffHand = New OffHand
		DeathCoil = new DeathCoil
		IcyTouch = new IcyTouch
		Necrosis = new Necrosis
		WanderingPlague = new WanderingPlague
		FrostStrike = New FrostStrike
		BloodCakedBlade = New BloodCakedBlade
		DeathStrike = New DeathStrike
		BloodBoil = new BloodBoil
		HeartStrike = new HeartStrike
		DeathandDecay = new DeathandDecay
		Gargoyle = new Gargoyle
		Horn = new Horn
		Bloodlust= new Bloodlust
		Pestilence = new Pestilence
		proc = New proc
		
		
		
		AMSCd = _MainFrm.txtAMScd.text * 100
		AMSTimer = _MainFrm.txtAMScd.text * 100
		AMSAmount = _MainFrm.txtAMSrp.text
		
		InterruptCd = _MainFrm.txtInterruptCd.text * 100
		InterruptTimer = _MainFrm.txtInterruptCd.text * 100
		InterruptAmount = _MainFrm.txtInterruptAmount.text
		
		
		
		
		
		
		
		ShowDpsTimer = 1
	End Sub
	
	Sub LoadConfig
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		on error goto errH
		doc.Load("config.xml")
		loadtemplate (GetFilePath(doc.SelectSingleNode("//config/template").InnerText))

		If sim.rotate Then
			sim.rotationPath = GetFilePath( doc.SelectSingleNode("//config/rotation").InnerText)
		Else
			sim.loadPriority (GetFilePath(doc.SelectSingleNode("//config/priority").InnerText))
		End If
'		cmbCharacter.SelectedItem = doc.SelectSingleNode("//config/Character").InnerText

		Dim Sigil As String
		Sigil = doc.SelectSingleNode("//config/sigil").InnerText
		Sigils.WildBuck = false
		Sigils.FrozenConscience = false
		Sigils.DarkRider = false
		Sigils.ArthriticBinding = false
		Sigils.Awareness = false
		Sigils.Strife = false
		Sigils.HauntedDreams = false
		sigils.VengefulHeart = False
		sigils.Virulence = false
		select case Sigil
			case "WildBuck"
				Sigils.WildBuck = true
			case "FrozenConscience"
				Sigils.FrozenConscience =true
			case "DarkRider"
				Sigils.DarkRider = true
			case "ArthriticBinding"
				Sigils.ArthriticBinding = true
			case "Awareness"
				Sigils.Awareness = true
			case "Strife"
				Sigils.Strife = true
			case "HauntedDreams"
				Sigils.HauntedDreams = True
			Case "VengefulHeart"
				sigils.VengefulHeart = True
			Case "Virulence"
				sigils.Virulence = true
		end select




		Dim Presence As String
		Presence = doc.SelectSingleNode("//config/presence").InnerText
		sim.MainStat.BloodPresence = 0
		sim.MainStat.UnholyPresence = 0
		sim.Mainstat.FrostPresence = 0
		Select Case Presence
			Case "Blood"
				sim.MainStat.BloodPresence = 1
			Case "Unholy"
				sim.MainStat.UnholyPresence=1
			Case "Frost"
				sim.Mainstat.FrostPresence = 1
		End Select

		sim.RuneForge.MHCinderglacier = False
		sim.RuneForge.MHFallenCrusader = false
		sim.RuneForge.MHRazorice = false
		Select Case doc.SelectSingleNode("//config/mh").InnerText
			Case "Cinderglacier"
				sim.RuneForge.MHCinderglacier = true
			Case "FallenCrusader"
				sim.RuneForge.MHFallenCrusader = true
			Case "Razorice"
				sim.RuneForge.MHRazorice = true
		End Select



		sim.RuneForge.OHCinderglacier = False
		sim.RuneForge.OHFallenCrusader = false
		sim.RuneForge.OHRazorice = False
		sim.Runeforge.OHBerserking = False
		
		Select Case doc.SelectSingleNode("//config/oh").InnerText
			Case "Cinderglacier"
				sim.RuneForge.OHCinderglacier = true
			Case "FallenCrusader"
				sim.RuneForge.OHFallenCrusader = true
			Case "Razorice"
				sim.RuneForge.OHRazorice = True
			Case "Berserking"
				sim.Runeforge.OHBerserking = True
			end select

'		txtLatency.Text = doc.SelectSingleNode("//config/latency").InnerText
'
'		txtSimtime.Text = doc.SelectSingleNode("//config/simtime").InnerText
'		chkCombatLog.Checked = doc.SelectSingleNode("//config/log").InnerText
'		ckLogRP.Checked = doc.SelectSingleNode("//config/logdetail").InnerText
'		chkLissage.Checked = doc.SelectSingleNode("//config/smooth").InnerText
'		chkGhoulHaste.Checked = doc.SelectSingleNode("//config/ghoulhaste").InnerText
'		chkWaitFC.Checked = doc.SelectSingleNode("//config/WaitFC").InnerText
'		ckPet.Checked = doc.SelectSingleNode("//config/pet").InnerText
		errH:
	End Sub
	
	
	Function toDecimal(d As Double) As Decimal
		try
			Return d.ToString (".#")
		Catch
		End Try
	End Function
	
	Function toDDecimal(d As Double) As Decimal
		try
			Return d.ToString (".##")
		Catch
		End Try
	End Function
	
	Sub loadPriority(file As String)
		priority = new priority
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
		If sim.RuneForge.RazoriceTotal <> 0 Then myArray.Add(sim.RuneForge.RazoriceTotal)
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
			If sim.RuneForge.RazoriceTotal = tot Then
				STmp = Sim.RuneForge.Razoricereport
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
		If Pestilence.HitCount <> 0 Then
			STmp = Pestilence.report
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
			OffHand.total  + sim.RuneForge.RazoriceTotal + DeathandDecay.total*1.9 +  RuneStrike.total*1.5
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
	
	Sub TryOnMHHitProc()
		sim.RuneForge.TryMHCinderglacier
		sim.RuneForge.TryMHFallenCrusader
		Trinket.TryMjolRune
		Trinket.TryGrimToll
		Trinket.TryGreatness()
		Trinket.TryDeathChoice()
		Trinket.TryDCDeath()
		Trinket.TryVictory()
		Trinket.TryBandit()
		Trinket.TryDarkMatter()
		Trinket.TryComet()
	End Sub
	Sub TryOnOHHitProc
		sim.RuneForge.TryOHCinderglacier
		sim.RuneForge.TryOHFallenCrusader
		sim.RuneForge.TryOHBerserking
		Trinket.TryMjolRune
		Trinket.TryGrimToll
		Trinket.TryGreatness()
		Trinket.TryDeathChoice()
		Trinket.TryDCDeath()
		Trinket.TryVictory()
		Trinket.TryBandit()
		Trinket.TryDarkMatter()
		Trinket.TryComet()
	End Sub
	Sub tryOnCrit()
		Trinket.TryBitterAnguish()
		Trinket.TryMirror()
		Trinket.TryPyrite()
		Trinket.TryOldGod()
	End Sub
	Sub TryOnSpellHit
		Trinket.TryGreatness()
		Trinket.TryDeathChoice()
		Trinket.TryDCDeath()
		
	End Sub
	
End Module