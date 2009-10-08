Public Class Sim
	
	Friend TotalDamageAlternative As Long
	Friend NextFreeGCD As Long
	Friend Lag As Long
	Friend TimeStamp As Long
	Friend TimeStampCounter As Long
	Friend _EPStat As String
	Friend EPBase as Integer
	Friend DPS As Long
	Friend MaxTime As Long
	Friend NumberOfFights As Long
	Friend RotationStep as Integer
	Friend Rotate as boolean
	Friend rotationPath As String
	Friend PetFriendly As Boolean
	Friend NumberOfEnemies as Integer
	Private SimStart as Date
	Friend _MainFrm As MainForm
	
	Friend Threat as Long
	Private AMSTimer As Long
	Private AMSCd As Integer
	Private AMSAmount As Integer
	Private ShowDpsTimer As Long
	Private InterruptTimer As Long
	Private InterruptAmount As Integer
	Private InterruptCd As Integer
	Friend KeepRNGSeed As Boolean
	Friend KeepDiseaseOnOthersTarget As Boolean
	Friend Viskag As Viskag
	Friend BoneShield33 as Boolean
	
	Friend RandomNumberGenerator as RandomNumberGenerator
	
	Friend Runes as runes.runes
	
	Friend RunicPower As RunicPower
	Friend Character as Character
	Friend MainStat as MainStat
	Friend Sigils as Sigils
	Friend boss as Boss
	
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
	Friend Gargoyle As Gargoyle
	Friend BoneShield As BoneShield
	Friend Frenzy as Frenzy
	
	
	'Disease Creation
	Friend BloodPlague as BloodPlague
	Friend FrostFever as FrostFever
	
	Friend proc As proc
	Friend Buff As buff
	Friend Glyph As Glyph
	
	Friend Priority As Priority
	Friend Rotation as Rotation
	Friend RuneForge As RuneForge
	
	Friend Trinket As Trinket
	Friend ProgressFrame As ProgressFrm
	
	Friend CombatLog as new CombatLog
	Friend BoneShieldUsageStyle as Integer
	
	Sub Init()
	End Sub
	
	Function NumDesease() As Integer
		NumDesease = 0
		If BloodPlague.isActive(TimeStamp) Then NumDesease = NumDesease + 1
		If FrostFever.isActive(TimeStamp) Then NumDesease = NumDesease + 1
		If (TalentUnholy.EbonPlaguebringer + TalentUnholy.CryptFever >= 1) And NumDesease >= 1 Then NumDesease = NumDesease + 1
		
	End Function
	
	Sub StartEP(pb As ProgressBar,EPCalc As Boolean,SimTime As double,MainFrm As MainForm)
		
	End Sub
	Sub Create
		Debug.Print ("Create Sim object")
	End Sub
	
	Function EPStat() as String
		return _EPStat
	End Function
	
	function StartEPTrinket(pb As ProgressBar,EPCalc As Boolean,SimTime As double,MainFrm As MainForm) as string
		Dim BaseDPS As long
		Dim APDPS As Long
		Dim tmp1 As double
		Dim tmp2 As Double
		dim sReport as String
		sReport = ""
		'
		_EPStat="NoTrinket"
		Prepare(pb,SimTime,MainFrm)
		BaseDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		
		'
		_EPStat="AttackPowerNoTrinket"
		Prepare(pb,SimTime,MainFrm)
		APDPS = DPS
		WriteReport ("Average for " & EPStat & " | " & DPS)
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		Dim trinketsList As Xml.XmlNode
		dim tNode as Xml.XmlNode
		trinketsList = doc.SelectsingleNode("//config/Trinket")
		
		For Each tNode In trinketsList.ChildNodes
			If tNode.InnerText = "True" Then
				_EPStat= tNode.Name.Replace("chkEP","")
				Prepare(pb,SimTime,MainFrm)
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
	
	
	'	Sub Start(pb As ProgressBar,SimTime As Double, MainFrm As MainForm, EPstat As String	)
	'		me.EPStat = EPstat
	'		Start(pb ,SimTime , MainFrm )
	'	End Sub
	'
	Private Pb As ProgressBar
	Private SimTime As Double
	Sub Prepare (pbar As ProgressBar,SimTime As Double, MainFrm As MainForm)
		me.SimTime = SimTime
		_MainFrm=MainFrm
	End Sub
	Sub Prepare (pbar As ProgressBar,SimTime As Double, MainFrm As MainForm,EPS As String, EPBse as String )
		Pb = pbar
		me.SimTime = SimTime
		_MainFrm=MainFrm
		_EPStat = EPS
		EPBase = EPBse
	End Sub
	Sub CreateProgressFrame()
		ProgressFrame = New ProgressFrm
		
		ProgressFrame.Show()
		ProgressFrame.TopMost = true
		application.DoEvents
		Pb = ProgressFrame.PBsim
		
		If EPStat <> "" Then
			ProgressFrame.Text = EPStat
		Else
			ProgressFrame.Text = "Simulation"
		End If
	End Sub
	Sub Start()
		CreateProgressFrame
		Rnd(-1) 'Tell VB to initialize using Randomize's parameter
		RandomNumberGenerator = new RandomNumberGenerator 'init here, so that we don't get the same rng numbers for short fights.
		'EPBase = 50
		'combatlog.LogDetails = true
		SimStart = now
		
		MaxTime = SimTime * 60 * 60 * 100
		pb.Maximum = SimTime * 60 * 60 * 100
		
		TotalDamageAlternative = 0
		TimeStampCounter = 1
		dim resetTime as Integer
		Dim NextReset as Integer
		
		If _MainFrm.chkManyFights.Checked Then
			NumberOfFights = Math.Round( ( SimTime * 60 * 60 ) / _MainFrm.txtManyFights.text )
			Debug.Print( "SimTime = " & SimTime * 60 * 60)
			Debug.Print( "_MainFrm.txtManyFights.text = " & _MainFrm.txtManyFights.text)
			'MaxTime = 
			resetTime = _MainFrm.txtManyFights.text * 100
			NextReset = resetTime
			Debug.Print( "NumberOfFights = " & NumberOfFights)
		Else
			NumberOfFights = 1
			resetTime = MaxTime + 1
		End If
		
		Dim intCount As Integer
		
		'Init
		Initialisation
		TimeStamp = 1
		If TalentUnholy.MasterOfGhouls=1 Then Ghoul.Summon(1)
		
		If Rotate Then Rotation.loadRotation
		
		Do Until TimeStamp > MaxTime
			
			Timestamp = FastFoward(Timestamp)
			If NextReset <= Timestamp Then
				SoftReset
				NextReset = NextReset + resetTime
			End If
			
			
			If MainStat.FrostPresence = 1 Then
				If Boss.NextHit <= TimeStamp Then
					Boss.ApplyDamage(TimeStamp)
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
				If Bloodlust.IsAvailable(TimeStamp) And TimeStamp > 500 Then
					Bloodlust.use(TimeStamp)
				End If
				
				if TalentBlood.DRW = 1 then
					If isInGCD(TimeStamp) = False Then
						If DRW.cd < TimeStamp And RunicPower.Value  >= 60 And CanUseGCD(TimeStamp) Then
							DRW.Summon(TimeStamp)
						End If
					End If
					If DRW.IsActive(TimeStamp) Then
						if DRW.NextDRW <= TimeStamp then DRW.ApplyDamage(TimeStamp)
					End If
				end if
				
				If PetFriendly Then
					If talentunholy.SummonGargoyle = 1 Then
						If isInGCD(TimeStamp) = False Then
							If Gargoyle.cd < TimeStamp and RunicPower.Value >= 60 and CanUseGCD(TimeStamp) Then
								Gargoyle.Summon(TimeStamp)
							end if
						End If
						If Gargoyle.ActiveUntil >= TimeStamp Then
							If Gargoyle.NextGargoyleStrike <= TimeStamp Then Gargoyle.ApplyDamage(TimeStamp)
						end if
					End If
				end if
				
				If isInGCD(TimeStamp) = False Then
					if Rotate then
						Rotation.DoRoration(TimeStamp)
					else
						Priority.DoNext (TimeStamp)
					End If
				End If
				
				If isInGCD(TimeStamp) = False Then
					If UnbreakableArmor.IsAvailable(TimeStamp) Then
						UnbreakableArmor.Use(TimeStamp)
					End If
				End If
				
				If isInGCD(TimeStamp) = False Then
					If BoneShieldUsageStyle = 2 Then
						If runes.Rune1.Available(TimeStamp) = False Then
							If runes.Rune2.Available(TimeStamp) = False Then
								If runes.Rune3.Available(TimeStamp) = False Then
									If runes.Rune4.Available(TimeStamp) = False Then
										If runes.Rune5.Available(TimeStamp) = False Then
											If runes.Rune6.Available(TimeStamp) = False Then
												If BoneShield.IsAvailable(TimeStamp) Then
													BoneShield.Use(TimeStamp)
												End If
												If UnbreakableArmor.IsAvailable(TimeStamp) Then
													UnbreakableArmor.Use(TimeStamp)
												End If
											End If
										End If
									End If
								End If
							End If
						End If
					End If
				End If
				
				
				
				If PetFriendly Then
					If isInGCD(TimeStamp) = False Then
						If Ghoul.ActiveUntil < TimeStamp and Ghoul.cd < TimeStamp and CanUseGCD(TimeStamp) Then
							Ghoul.Summon(TimeStamp)
						end if
					End If
					if Ghoul.ActiveUntil >= TimeStamp then
						If Ghoul.NextWhiteMainHit <= TimeStamp Then Ghoul.ApplyDamage(TimeStamp)
						If Ghoul.NextClaw <= TimeStamp Then Ghoul.Claw(TimeStamp)
						If isInGCD(TimeStamp) And Frenzy.IsAutoFrenzyAvailable(Timestamp) Then
							Frenzy.Frenzy(TimeStamp)
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
			
			If isInGCD(TimeStamp) = False Then
				If horn.isAutoAvailable(TimeStamp) and CanUseGCD(TimeStamp) Then
					horn.use(TimeStamp)
				end if
			End If
			
			If DeathandDecay.nextTick = TimeStamp Then
				DeathandDecay.ApplyDamage(TimeStamp)
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
			If TimeStamp <= pb.Maximum Then pb.Value = TimeStamp Else pb.Value = pb.Maximum
			ElseIf ShowDpsTimer <= TimeStamp Then
				ShowDpsTimer = TimeStamp + 0.1 * 60 * 60 * 100
			If TimeStampCounter <= pb.Maximum Then pb.Value = TimeStampCounter Else pb.Value = pb.Maximum
			End If
		Loop
		TotalDamageAlternative = TotalDamageAlternative + TotalDamage
		TimeStampCounter = TimeStampCounter + TimeStamp
		
		
		'TotalDamage = TotalDamageAlternative
		TimeStamp = TimeStampCounter
		
		DPS = 100 * TotalDamage / TimeStamp
		'Problem with MThread
		pb.Value = pb.Maximum
		
'		If NumberOfFights > 1 then
'			WriteReport ("DPS: " & DPS)
'		Else
			Report()
'		End If
		'Problem with MThread
		'_MainFrm.lblDPS.Text = DPS & " DPS"
		Debug.Print( "DPS=" & DPS & " " & EPStat & " hit=" & mainstat.Hit & " sphit=" & mainstat.SpellHit & " exp=" & mainstat.expertise )
		combatlog.finish
		on error resume next
		SimConstructor.DPSs.Add(DPS, me.EPStat)
		
		
	End Sub
	
	Function TotalDamage() as Long
		TotalDamage = ScourgeStrike.total + obliterate.total + PlagueStrike.total + _
			BloodStrike.total + HeartStrike.total + frostfever.total + _
			BloodPlague.total + IcyTouch.total + deathcoil.total + _
			UnholyBlight.total + Necrosis.total + BloodCakedBlade.total + _
			WanderingPlague.total +FrostStrike.total  +HowlingBlast.total + _
			BloodBoil.total  + DeathStrike.total + MainHand.total + _
			OffHand.total  + Ghoul.total + Gargoyle.total + DRW.total + _
			RuneForge.RazoriceTotal + DeathandDecay.total + RuneStrike.total + trinket.Total + Viskag.Total
	End Function
	
	
	
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
				tGDC = 100+ _MainFrm.txtLatency.Text/10 + 50
			Else
				tGDC =  150+ _MainFrm.txtLatency.Text/10 + 50
			End If
			
			If math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) < (T +  tGDC) Then
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
	
	
	
	
	
	Function DoMyWhiteHit As Boolean
		'unused
	End Function
	
	Sub SoftReset()

		Me.RotationStep = 0
		Me.Runes.Rune1.AvailableTime = 0
		Me.Runes.Rune2.AvailableTime = 0
		Me.Runes.Rune3.AvailableTime = 0
		Me.Runes.Rune4.AvailableTime = 0
		Me.Runes.Rune5.AvailableTime = 0
		Me.Runes.Rune6.AvailableTime = 0
		
		RunicPower.Value = 0
		BloodPlague.nextTick = 0
		BloodPlague.FadeAt = 0
		FrostFever.nextTick = 0
		FrostFever.FadeAt = 0
		BloodTap.CD  = 0
		HowlingBlast.CD = 0
		Ghoul.cd = 0
		Hysteria.CD = 0
		DeathChill.Cd = 0
		Desolation = New Desolation
		MainHand.NextWhiteMainHit = 0
		OffHand.NextWhiteOffHit = 0
		Frenzy.CD = 0
		DeathandDecay.CD = 0
		Gargoyle.cd = 0
		Horn.CD = 0
		Bloodlust.Cd = 0
		proc = New proc(Me)
		BoneShield.CD = 0
		NextFreeGCD = 0
		AMSCd = _MainFrm.txtAMScd.text * 100
		AMSTimer = _MainFrm.txtAMScd.text * 100
		AMSAmount = _MainFrm.txtAMSrp.text
		
	End Sub
	
	Sub Initialisation()
		'RandomNumberGenerator.Init 'done in Start
		PetFriendly = SimConstructor.PetFriendly
		Rotate = SimConstructor.Rotate
		'_EpStat = SimConstructor.EpStat
		
		Buff = New Buff(Me)
		'Keep this order for RuneX -> Runse -> Rotation/Prio
		Runes = New Runes.runes(Me)
		
		RunicPower = New RunicPower(Me)
		
		Rotation = new Rotation(Me)
		Priority = New Priority(Me)
		Character = New Character(Me)
		
		
		MainStat = new MainStat(Me)
		' Buff.UnBuff
		BloodPlague = new BloodPlague(Me)
		FrostFever = New FrostFever(Me)
		
		UnholyBlight = New UnholyBlight(Me)
		
		BloodTap = new BloodTap(Me)
		HowlingBlast = New HowlingBlast(Me)
		
		Ghoul = new Ghoul(Me)
		GhoulStat = New GhoulStat(Me)
		Hysteria = new Hysteria(Me)
		DeathChill = new DeathChill(Me)
		
		UnbreakableArmor = new UnbreakableArmor(Me)
		RuneForge = new RuneForge(Me)
		Butchery = new Butchery(Me)
		DRW = new DRW(Me)
		RuneStrike = New RuneStrike(Me)
		
		
		
		Sigils = new Sigils(Me)
		
		LoadConfig
		Desolation = New Desolation
		
		RunicPower.Value = 0
		NextFreeGCD = 0
		Threat = 0
		
		NumberOfEnemies = _MainFrm.txtNumberOfEnemies.text
		KeepDiseaseOnOthersTarget = _MainFrm.chkDisease.Checked
		ScourgeStrike = new ScourgeStrike(Me)
		Obliterate = new Obliterate(Me)
		PlagueStrike= new PlagueStrike(Me)
		BloodStrike = New BloodStrike(Me)
		MainHand = New MainHand(Me)
		OffHand = New OffHand(Me)
		DeathCoil = new DeathCoil(Me)
		IcyTouch = new IcyTouch(Me)
		Necrosis = new Necrosis(Me)
		WanderingPlague = new WanderingPlague(Me)
		FrostStrike = New FrostStrike(Me)
		Frenzy = NEw Frenzy(Me)
		BloodCakedBlade = New BloodCakedBlade(Me)
		DeathStrike = New DeathStrike(Me)
		BloodBoil = new BloodBoil(me)
		HeartStrike = new HeartStrike(Me)
		DeathandDecay = new DeathandDecay(Me)
		Gargoyle = new Gargoyle(Me)
		Horn = new Horn(Me)
		Bloodlust= new Bloodlust(Me)
		Pestilence = new Pestilence(Me)
		proc = New proc(Me)
		BoneShield  = New BoneShield(Me)
		
		
		
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
		
		If rotate Then
			rotationPath = GetFilePath( doc.SelectSingleNode("//config/rotation").InnerText)
		Else
			loadPriority (GetFilePath(doc.SelectSingleNode("//config/priority").InnerText))
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
		MainStat.BloodPresence = 0
		MainStat.UnholyPresence = 0
		Mainstat.FrostPresence = 0
		Select Case Presence
			Case "Blood"
				MainStat.BloodPresence = 1
			Case "Unholy"
				MainStat.UnholyPresence=1
			Case "Frost"
				Mainstat.FrostPresence = 1
		End Select
		
		RuneForge.MHCinderglacier = False
		RuneForge.MHFallenCrusader = false
		RuneForge.MHRazorice = false
		Select Case doc.SelectSingleNode("//config/mh").InnerText
			Case "Cinderglacier"
				RuneForge.MHCinderglacier = true
			Case "FallenCrusader"
				RuneForge.MHFallenCrusader = true
			Case "Razorice"
				RuneForge.MHRazorice = true
		End Select
		
		RuneForge.OHCinderglacier = False
		RuneForge.OHFallenCrusader = false
		RuneForge.OHRazorice = False
		Runeforge.OHBerserking = False
		
		Select Case doc.SelectSingleNode("//config/oh").InnerText
			Case "Cinderglacier"
				RuneForge.OHCinderglacier = true
			Case "FallenCrusader"
				RuneForge.OHFallenCrusader = true
			Case "Razorice"
				RuneForge.OHRazorice = True
			Case "Berserking"
				Runeforge.OHBerserking = True
		end select
		
		'		txtLatency.Text = doc.SelectSingleNode("//config/latency").InnerText
		'
		'		txtSimtime.Text = doc.SelectSingleNode("//config/simtime").InnerText
		Me.CombatLog.enable = doc.SelectSingleNode("//config/log").InnerText
		Me.CombatLog.LogDetails = doc.SelectSingleNode("//config/logdetail").InnerText
		Dim tmp As String
		tmp = doc.SelectSingleNode("//config/BShOption").InnerText
		Select Case tmp
			Case "Instead of Blood Strike"
				Me.BoneShieldUsageStyle =1
			Case "After BS/BB"
				Me.BoneShieldUsageStyle= 2
			Case "Instead of Blood Boil"
				Me.BoneShieldUsageStyle = 3
		End Select
		Boneshield33 = doc.SelectSingleNode("//config/BoneShield3.3").InnerText
		
		
		'		chkCombatLog.Checked = doc.SelectSingleNode("//config/log").InnerText
		'		ckLogRP.Checked = doc.SelectSingleNode("//config/logdetail").InnerText

		'		chkGhoulHaste.Checked = doc.SelectSingleNode("//config/ghoulhaste").InnerText
		'		chkWaitFC.Checked = doc.SelectSingleNode("//config/WaitFC").InnerText
		'		ckPet.Checked = doc.SelectSingleNode("//config/pet").InnerText
		errH:
	End Sub
	
	Sub loadPriority(file As String)
		priority = new priority(Me)
		priority.prio.Clear
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(file)
		dim Nod as Xml.XmlNode
		
		For Each Nod In xmldoc.SelectSingleNode("//Priority").ChildNodes
			priority.prio.Add(Nod.Name)
		Next
		
	End Sub
	
	Sub Report()
		on error resume next
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
		
		If me.MainStat.FrostPresence Then
			Tw.Write ("	<td><b>TPS</b></td>")
		End If
		
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
		If RuneForge.RazoriceTotal <> 0 Then myArray.Add(RuneForge.RazoriceTotal)
		If DeathandDecay.total <> 0 Then myArray.Add(DeathandDecay.total)
		If trinket.Total <> 0 Then myArray.Add(trinket.Total)
		if Viskag.Total <> 0 Then myArray.Add(Viskag.Total)
		
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
			If RuneForge.RazoriceTotal = tot Then
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
			If Viskag.Total = tot Then
				STmp = Viskag.report
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
		
		If BoneShield.HitCount <> 0 Then
			STmp = BoneShield.report
			STmp = replace(STmp,vbtab,"</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		If BloodTap.HitCount <> 0 Then
			STmp = BloodTap.report
			STmp = replace(STmp,vbtab,"</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		If Frenzy.HitCount <> 0 Then
			STmp = Frenzy.report
			STmp = replace(STmp,vbtab,"</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		If UnbreakableArmor.HitCount <> 0 Then
			STmp = UnbreakableArmor.report
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
			OffHand.total  + RuneForge.RazoriceTotal + DeathandDecay.total*1.9 +  RuneStrike.total*1.5
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
		
		STmp = sTmp &   "<tr><td COLSPAN=8>Template: " & Split(Character.GetTemplateFileName,".")(0) & "</td></tr>"
		If Rotate Then
			STmp = sTmp &   "<tr><td COLSPAN=8>Rotation: " & Split(Character.GetRotationFileName,".")(0) & "</td></tr>"
		Else
			STmp = sTmp &   "<tr><td COLSPAN=8>Priority: " & Split(Character.GetPriorityFileName,".")(0) & "</td></tr>"
		End If
		STmp = sTmp &   "<tr><td COLSPAN=8>Presence: " & Character.GetPresence & vbCrLf & "</td></tr>"
		STmp = sTmp &   "<tr><td COLSPAN=8>Sigil: " & Character.GetSigil & vbCrLf & "</td></tr>"
		
		If MainStat.DualW Then
			STmp = sTmp &   "<tr><td COLSPAN=8>RuneEnchant: " & Character.GetMHEnchant  & " / " &  Character.GetOHEnchant & "</td></tr>"
		Else
			STmp = sTmp &   "<tr><td COLSPAN=8>RuneEnchant: " & Character.GetMHEnchant & "</td></tr>"
		End If
		
		STmp = sTmp &   "<tr><td COLSPAN=8>Pet Calculation: " & Character.GetPetCalculation & "</td></tr>"
		
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
			if runes.rune1.AvailableTime > T and runes.rune1.AvailableTime < tmp then  tmp = runes.rune1.AvailableTime
			if runes.rune2.AvailableTime > T and runes.rune2.AvailableTime < tmp then  tmp = runes.rune2.AvailableTime
			If runes.rune3.AvailableTime > T And runes.rune3.AvailableTime < tmp Then  tmp = runes.rune3.AvailableTime
			If runes.rune4.AvailableTime > T And runes.rune4.AvailableTime < tmp Then  tmp = runes.rune4.AvailableTime
			If runes.rune5.AvailableTime > T And runes.rune5.AvailableTime < tmp Then  tmp = runes.rune5.AvailableTime
			if runes.rune6.AvailableTime > T and runes.rune6.AvailableTime < tmp then  tmp = runes.rune6.AvailableTime
			
		End If
		
		If Butchery.nextTick < tmp  And talentblood.Butchery > 0 Then tmp = Butchery.nextTick
		
		if DeathandDecay.nextTick > TimeStamp and  DeathandDecay.nextTick < tmp then tmp = DeathandDecay.nextTick
		
		if TalentBlood.DRW = 1 then
			If DRW.IsActive(TimeStamp) Then
				if DRW.NextDRW < tmp  then tmp = DRW.NextDRW
			End If
		End If
		If PetFriendly Then
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
		
		If MainStat.FrostPresence = 1 Then
			If Boss.NextHit < tmp Then
				tmp = Boss.NextHit
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
		RuneForge.TryMHCinderglacier
		RuneForge.TryMHFallenCrusader
		Trinket.TryMjolRune
		Trinket.TryGrimToll
		Trinket.TryGreatness()
		Trinket.TryDeathChoice()
		trinket.TryDeathChoiceHeroic()
		Trinket.TryDCDeath()
		Trinket.TryVictory()
		Trinket.TryBandit()
		Trinket.TryDarkMatter()
		Trinket.TryComet()
	End Sub
	Sub TryOnOHHitProc
		RuneForge.TryOHCinderglacier
		RuneForge.TryOHFallenCrusader
		RuneForge.TryOHBerserking
		Trinket.TryMjolRune
		Trinket.TryGrimToll
		Trinket.TryGreatness()
		Trinket.TryDeathChoice()
		trinket.TryDeathChoiceHeroic()
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
		trinket.TryDeathChoiceHeroic()
		Trinket.TryDCDeath()
		
	End Sub
	
End Class