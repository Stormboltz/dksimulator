Imports Microsoft.VisualBasic
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
	Friend IntroPath as String
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
	Friend ScourgeStrikeMagical as ScourgeStrikeMagical
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
	Friend ERW as EmpowerRuneWeapon
	
	'Disease Creation
	Friend BloodPlague as BloodPlague
	Friend FrostFever as FrostFever
	
	Friend proc As procs
	Friend Buff As buff
	Friend Glyph As Glyph
	
	Friend Priority As Priority
	Friend Rotation as Rotation
	Friend RuneForge As RuneForge
	
	Friend Trinkets As Trinkets
	
	Friend DamagingObject as new Collection
	Friend TrinketsCollection As New Collection
	Friend ProcCollection As New Collection
	
	
	
	
	
	
	Friend ProgressFrame As ProgressFrm
	
	Friend CombatLog as new CombatLog(me)
	Friend BoneShieldUsageStyle As Integer
	
	Friend SaveRPForRS as Boolean
	
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
		Rotation.LoadIntro
		If Rotate Then Rotation.loadRotation
		
		Do Until TimeStamp >= MaxTime
			
			Timestamp = FastFoward(Timestamp)
			If TimeStamp >= MaxTime Then goto finnish
			
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
					If DRW.IsActive(TimeStamp) Then
						if DRW.NextDRW <= TimeStamp then DRW.ApplyDamage(TimeStamp)
					End If
				end if
				
				If PetFriendly Then
					If talentunholy.SummonGargoyle = 1 Then
						If Gargoyle.ActiveUntil >= TimeStamp Then
							If Gargoyle.NextGargoyleStrike <= TimeStamp Then Gargoyle.ApplyDamage(TimeStamp)
						end if
					End If
				end if
				
				If isInGCD(TimeStamp) = False Then rotation.DoIntro(TimeStamp)
				
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
						If runes.BloodRune1.Available(TimeStamp) = False Then
							If runes.BloodRune2.Available(TimeStamp) = False Then
								If runes.FrostRune1.Available(TimeStamp) = False Then
									If runes.FrostRune2.Available(TimeStamp) = False Then
										If runes.UnholyRune1.Available(TimeStamp) = False Then
											If runes.UnholyRune2.Available(TimeStamp) = False Then
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
			
			If ShowDpsTimer <= TimeStamp Then
					ShowDpsTimer = TimeStamp + 0.1 * 60 * 60 * 100
				If TimeStamp <= pb.Maximum Then pb.Value = TimeStamp Else pb.Value = pb.Maximum
					ElseIf ShowDpsTimer <= TimeStamp Then
						ShowDpsTimer = TimeStamp + 0.1 * 60 * 60 * 100
					If TimeStampCounter <= pb.Maximum Then pb.Value = TimeStampCounter Else pb.Value = pb.Maximum
			End If
		Loop
		
		'Finnish Line
		finnish:
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
'		TotalDamage = ScourgeStrike.total + ScourgeStrikeMagical.Total + obliterate.total + PlagueStrike.total + _
'			BloodStrike.total + HeartStrike.total + frostfever.total + _
'			BloodPlague.total + IcyTouch.total + deathcoil.total + _
'			UnholyBlight.total + Necrosis.total + BloodCakedBlade.total + _
'			WanderingPlague.total +FrostStrike.total  +HowlingBlast.total + _
'			BloodBoil.total  + DeathStrike.total + MainHand.total + _
'			OffHand.total  + Ghoul.total + Gargoyle.total + DRW.total + _			
'			Trinkets.MHRazorIce.Total + Trinkets.OHRazorIce.Total + DeathandDecay.total + RuneStrike.total + _
'			Trinkets.Bandit.Total + Trinkets.DCDeath.Total + Trinkets.Necromantic.Total + _
'			Trinkets.MHSingedViskag.Total  + Trinkets.OHSingedViskag.Total + _
'			Trinkets.MHtemperedViskag.Total + Trinkets.OHtemperedViskag.Total + _
'			Trinkets.MHRagingDeathbringer.Total + Trinkets.OHRagingDeathbringer.Total + _
'			Trinkets.MHEmpoweredDeathbringer.Total + Trinkets.OHEmpoweredDeathbringer.Total + _
'			Trinkets.HandMountedPyroRocket.total
			Dim i As long
			dim obj as Object
			
			For Each obj In Me.DamagingObject
				i += obj.Total 
			Next
			return i
			debug.Print(i)
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
		Me.Rotation.IntroStep=0
		Me.Runes.BloodRune1.AvailableTime = 0
		Me.Runes.BloodRune2.AvailableTime = 0
		Me.Runes.FrostRune1.AvailableTime = 0
		Me.Runes.FrostRune2.AvailableTime = 0
		Me.Runes.UnholyRune1.AvailableTime = 0
		Me.Runes.UnholyRune2.AvailableTime = 0
		
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
		proc.SoftReset
		Trinkets.SoftReset
		BoneShield.CD = 0
		BoneShield.PreBuff
		NextFreeGCD = 0
		AMSCd = _MainFrm.txtAMScd.text * 100
		AMSTimer = _MainFrm.txtAMScd.text * 100
		AMSAmount = _MainFrm.txtAMSrp.text
		ERW.CD = 0
		RuneForge.RazorIceStack = 0
	End Sub
	
	Sub Initialisation()
		'RandomNumberGenerator.Init 'done in Start
		DamagingObject.Clear
		PetFriendly = SimConstructor.PetFriendly
		Rotate = SimConstructor.Rotate
		'_EpStat = SimConstructor.EpStat
		
		Buff = New Buff(Me)
		'Keep this order for RuneX -> Runse -> Rotation/Prio
		Runes = New Runes.runes(Me)
		
		RunicPower = New RunicPower(Me)
		proc = New procs(Me)
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
		ScourgeStrike = New ScourgeStrike(Me)
		ScourgeStrikeMagical = New ScourgeStrikeMagical(Me)

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
		
		proc.Init
		BoneShield  = New BoneShield(Me)
		ERW = New EmpowerRuneWeapon(Me)
			
		
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
		loadtemplate (Application.StartupPath & "\Templates\" & doc.SelectSingleNode("//config/template").InnerText)
		IntroPath = Application.StartupPath & "\Intro\"  &  doc.SelectSingleNode("//config/intro").InnerText
		If rotate Then
			rotationPath = Application.StartupPath & "\Rotation\"  &  doc.SelectSingleNode("//config/rotation").InnerText
		Else
			loadPriority (Application.StartupPath & "\Priority\" & doc.SelectSingleNode("//config/priority").InnerText)
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
		
		RuneForge.MHCinderglacierRF = False
		RuneForge.MHFallenCrusader = false
		RuneForge.MHRazoriceRF = false
		Select Case doc.SelectSingleNode("//config/mh").InnerText
			Case "Cinderglacier"
				RuneForge.MHCinderglacierRF = true
			Case "FallenCrusader"
				RuneForge.MHFallenCrusader = True
				
			Case "Razorice"
				RuneForge.MHRazoriceRF = True
				Trinkets.MHRazorIce.Equip
		End Select
		
		RuneForge.OHCinderglacierRF = False
		RuneForge.OHFallenCrusader = false
		RuneForge.OHRazoriceRF = False
		Runeforge.OHBerserking = False
		
		if MainStat.DualW then
			Select Case doc.SelectSingleNode("//config/oh").InnerText
				Case "Cinderglacier"
					RuneForge.OHCinderglacierRF = true
				Case "FallenCrusader"
					RuneForge.OHFallenCrusader = true
				Case "Razorice"
					RuneForge.OHRazoriceRF = True
					Trinkets.OHRazorIce.Equip
				Case "Berserking"
					Runeforge.OHBerserking = True
			End Select
		End If
		
		
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

		errH:
	End Sub
	
	Sub loadPriority(file As String)
		priority = new priority(Me)
		priority.prio.Clear
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(file)
		dim Nod as Xml.XmlNode
		
		For Each Nod In xmldoc.SelectSingleNode("//Priority").ChildNodes
			If Nod.Name = "SaveRPForRuneStrike" Then
				SaveRPForRS = true
			Else
				priority.prio.Add(Nod.Name)
			End If
			
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
		Tw.Write ("	<td><b>Ability</b>|</td>")
		Tw.Write ("	<td><b>Total</b>|</td>")
		Tw.Write ("	<td><b>%</b>|</td>")
		Tw.Write ("	<td><b>Landed</b>|</td>")
		Tw.Write ("	<td><b>Hit%</b>|</td>")
		Tw.Write ("	<td><b>Crit%</b>|</td>")
		Tw.Write ("	<td><b>Miss%</b>|</td>")
		Tw.Write ("	<td><b>Average</b>|</td>")
		
		If me.MainStat.FrostPresence Then
			Tw.Write ("	<td><b>TPS</b>|</td>")
		End If
		
		Tw.Write ("</tr>")
		
		' Sort report
		dim obj as Object
		Dim myArray As New ArrayList
		
		For Each obj In DamagingObject
			if obj.total <> 0 then  myArray.Add(obj.total)
		Next
		myArray.Sort()
		
		
		dim i as Integer
		Dim tot As Long
		dim STmp as string
		For i=0 To myArray.Count -1
			tot = (myArray.Item(myArray.Count-1-i))
			
			For Each obj In DamagingObject
				If obj.total = tot Then
					STmp = obj.report
					STmp = replace(STmp,vbtab,"|</td><td>")
					Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				End If
			Next
		Next
		
		If Horn.HitCount <> 0 Then
			STmp = Horn.report
			STmp = replace(STmp,vbtab,"|</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		If Pestilence.HitCount <> 0 Then
			STmp = Pestilence.report
			STmp = replace(STmp,vbtab,"|</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		If BoneShield.HitCount <> 0 Then
			STmp = BoneShield.report
			STmp = replace(STmp,vbtab,"|</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		If BloodTap.HitCount <> 0 Then
			STmp = BloodTap.report
			STmp = replace(STmp,vbtab,"|</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		If Frenzy.HitCount <> 0 Then
			STmp = Frenzy.report
			STmp = replace(STmp,vbtab,"|</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		If UnbreakableArmor.HitCount <> 0 Then
			STmp = UnbreakableArmor.report
			STmp = replace(STmp,vbtab,"|</td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		sTmp = ""
		if EPStat <> "" then STmp =  "<tr><td COLSPAN=8>EP Stat <b>" &  EPStat & "</b></td></tr>"
		STmp = sTmp &  "<tr><td COLSPAN=8>DPS" & VBtab & "<b>" &  DPS & "</b></td></tr>"
		STmp = sTmp &   "<tr><td COLSPAN=8>Total Damage" & VBtab & Math.Round(TotalDamage/1000000,2) & "m" & VBtab &  " in " & MaxTime / 100 / 60/60 & "h</td></tr>"
		
		Dim ThreatBeforePresence As Long = Threat
		'TODO: Add the collection
		
		For Each obj In Me.DamagingObject
				Threat += obj.Total * obj.ThreadMultiplicator
			Next

	
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
			if runes.BloodRune1.AvailableTime > T and runes.BloodRune1.AvailableTime < tmp then  tmp = runes.BloodRune1.AvailableTime
			if runes.BloodRune2.AvailableTime > T and runes.BloodRune2.AvailableTime < tmp then  tmp = runes.BloodRune2.AvailableTime
			If runes.FrostRune1.AvailableTime > T And runes.FrostRune1.AvailableTime < tmp Then  tmp = runes.FrostRune1.AvailableTime
			If runes.FrostRune2.AvailableTime > T And runes.FrostRune2.AvailableTime < tmp Then  tmp = runes.FrostRune2.AvailableTime
			If runes.UnholyRune1.AvailableTime > T And runes.UnholyRune1.AvailableTime < tmp Then  tmp = runes.UnholyRune1.AvailableTime
			if runes.UnholyRune2.AvailableTime > T and runes.UnholyRune2.AvailableTime < tmp then  tmp = runes.UnholyRune2.AvailableTime
			
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
		dim obj as proc
		For Each obj In Me.proc.OnMHhitProcs
			obj.TryMe(timestamp)
		Next
		For Each obj In Me.proc.OnHitProcs
			obj.TryMe(timestamp)
		Next
		For Each obj In Me.proc.OnDamageProcs
			obj.TryMe(timestamp)
		Next
		
	End Sub
	Sub TryOnOHHitProc
		dim obj as proc
		For Each obj In Me.proc.OnOHhitProcs
			obj.TryMe(timestamp)
		Next
		For Each obj In Me.proc.OnHitProcs
			obj.TryMe(timestamp)
		Next
		For Each obj In Me.proc.OnDamageProcs
			obj.TryMe(timestamp)
		Next
	End Sub
	Sub tryOnCrit()
		dim obj as proc
		For Each obj In Me.proc.OnCritProcs
			obj.TryMe(timestamp)
		Next
	End Sub
	Sub TryOnSpellHit
		dim obj as proc
		For Each obj In Me.proc.OnDamageProcs
			obj.TryMe(timestamp)
		Next
	End Sub
End Class