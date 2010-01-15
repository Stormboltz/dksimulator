Imports Microsoft.VisualBasic
Public Class Sim
	
	
	Friend TotalDamageAlternative As Long
	Friend NextFreeGCD As Long
	Friend latency As Long
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
	Friend BloodToSync As Boolean
	Friend KeepBloodSync as Boolean
	
	Friend NumberOfEnemies as Integer
	Private SimStart as Date
	'Friend _MainFrm As MainForm
	
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
	
	
	Friend TalentBlood As TalentBlood
	Friend TalentFrost As TalentFrost
	Friend TalentUnholy as TalentUnholy
	
	
	
	Friend RandomNumberGenerator as RandomNumberGenerator
	
	Friend Runes as runes.runes
	
	Friend RunicPower As RunicPower
	Friend Character as Character
	Friend MainStat as MainStat
	Friend Sigils as Sigils
	Friend boss as Boss
	
	'Strike Creation
	Friend BloodCakedBlade As BloodCakedBlade
	Friend OHBloodCakedBlade As BloodCakedBlade
	
	Friend BloodStrike As BloodStrike
	Friend OHBloodStrike As BloodStrike
	
	Friend Obliterate As Obliterate
	Friend OHObliterate As Obliterate
	
	Friend PlagueStrike As PlagueStrike
	Friend OHPlagueStrike As PlagueStrike
	
	Friend FrostStrike As FrostStrike
	Friend OHFrostStrike As FrostStrike
	
	Friend RuneStrike As RuneStrike
	Friend OHRuneStrike As RuneStrike
	
	Friend DeathStrike As DeathStrike
	Friend OHDeathStrike As DeathStrike
	
	Friend HeartStrike As HeartStrike
	
	
	
	Friend ScourgeStrike As ScourgeStrike
	Friend ScourgeStrikeMagical as ScourgeStrikeMagical
	Friend MainHand as MainHand
	Friend OffHand As OffHand
	
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
	
	Friend MergeReport as Boolean
	Friend WaitForFallenCrusader as Boolean
	
	
	
	Friend ProgressFrame As ProgressFrm
	
	Friend CombatLog as new CombatLog(me)
	Friend BoneShieldUsageStyle As Integer
	Friend BloodPresence As Integer
	Friend UnholyPresence As Integer
	Friend FrostPresence As Integer
	
	
	Friend SaveRPForRS As Boolean
	
	
	Friend ReportName as String
	
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
		Pb = pbar
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
		
		LoadConfig
		
		
		'MergeReport = true
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
			'Debug.Print( "SimTime = " & SimTime * 60 * 60)
			'Debug.Print( "_MainFrm.txtManyFights.text = " & _MainFrm.txtManyFights.text)
			'MaxTime =
			resetTime = _MainFrm.txtManyFights.text * 100
			NextReset = resetTime
			'Debug.Print( "NumberOfFights = " & NumberOfFights)
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
			
			
			If FrostPresence = 1 Then
				If Boss.NextHit <= TimeStamp Then
					Boss.ApplyDamage(TimeStamp)
				End If
			End If
			
			
			application.DoEvents
			
			If AMSTimer < TimeStamp Then
				AMSTimer = TimeStamp + AMSCd
				RunicPower.add(AMSAmount)
			End If
			
			If TalentBlood.Butchery > 0 And Butchery.nextTick <= TimeStamp Then
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
					If TalentUnholy.SummonGargoyle = 1 Then
						If Gargoyle.ActiveUntil >= TimeStamp Then
							If Gargoyle.NextGargoyleStrike <= TimeStamp Then Gargoyle.ApplyDamage(TimeStamp)
						end if
					End If
				end if
				
				If isInGCD(TimeStamp) = False Then rotation.DoIntro(TimeStamp)
				
				If isInGCD(TimeStamp) = False Then
					If BoneShieldUsageStyle = 2 Then
						If runes.BloodRune1.Available(TimeStamp) = False Then
							If runes.BloodRune2.Available(TimeStamp) = False Then
								if BoneShield.IsAvailable(TimeStamp) Then
									BoneShield.Use(TimeStamp)
								End If
								If UnbreakableArmor.IsAvailable(TimeStamp) Then
									UnbreakableArmor.Use(TimeStamp)
								End If
							End If
						End If
					End If
				End If
				
				
				
				
				
				If isInGCD(TimeStamp) = False Then
					
					if Rotate then
						Rotation.DoRoration(TimeStamp)
					else
						Priority.DoNext (TimeStamp)
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
		Dim i As long
		dim obj as Supertype

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
			If UnholyPresence Then
				tGDC = 100+ latency/10 + 50
			Else
				tGDC =  150+ latency/10 + 50
			End If
			
			If math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) < (T +  tGDC) Then
				'debug.Print (RuneState & "time left on disease= " & (math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) -T)/100 & "s" & " - " & T/100)
				return false
			End If
		End If
	End Function
	
	Sub loadtemplate(file As String)
		
		talentblood = New TalentBlood
		talentfrost = New TalentFrost
		talentunholy = new TalentUnholy
		
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(file)
		
		if me._EPStat <> "Butchery" then talentblood.Butchery = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Butchery").InnerText)
		if me._EPStat <> "Subversion" then talentblood.Subversion  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Subversion").InnerText)
		if me._EPStat <> "BladedArmor" then talentblood.BladedArmor  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BladedArmor").InnerText)
		if me._EPStat <> "ScentOfBlood" then TalentBlood.ScentOfBlood = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ScentOfBlood").InnerText)
		if me._EPStat <> "Weapspec" then talentblood.Weapspec  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Weapspec").InnerText)
		if me._EPStat <> "Darkconv" then talentblood.Darkconv  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Darkconv").InnerText)
		if me._EPStat <> "BloodyStrikes" then talentblood.BloodyStrikes  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodyStrikes").InnerText)
		if me._EPStat <> "Vot3W" then talentblood.Vot3W  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Vot3W").InnerText)
		if me._EPStat <> "BloodyVengeance" then talentblood.BloodyVengeance  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodyVengeance").InnerText)
		if me._EPStat <> "AbominationMight" then talentblood.AbominationMight  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/AbominationMight").InnerText)
		if me._EPStat <> "Hysteria" then talentblood.Hysteria  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Hysteria").InnerText)
		if me._EPStat <> "ImprovedDeathStrike" then talentblood.ImprovedDeathStrike  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedDeathStrike").InnerText)
		if me._EPStat <> "SuddenDoom" then talentblood.SuddenDoom  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/SuddenDoom").InnerText)
		if me._EPStat <> "MightofMograine" then talentblood.MightofMograine  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MightofMograine").InnerText)
		if me._EPStat <> "BloodGorged" then talentblood.BloodGorged  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodGorged").InnerText)
		if me._EPStat <> "DRW" then talentblood.DRW  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/DRW").InnerText)
		if me._EPStat <> "DRM" then talentblood.DRM  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/DRM").InnerText)
		
		if me._EPStat <> "RPM" then talentfrost.RPM  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RPM").InnerText)
		if me._EPStat <> "ImprovedIcyTouch" then talentfrost.ImprovedIcyTouch  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedIcyTouch").InnerText)
		if me._EPStat <> "Toughness" then talentfrost.Toughness  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Toughness").InnerText)
		if me._EPStat <> "BlackIce" then talentfrost.BlackIce  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BlackIce").InnerText)
		if me._EPStat <> "NervesofColdSteel" then talentfrost.NervesofColdSteel  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/NervesofColdSteel").InnerText)
		if me._EPStat <> "Annihilation" then talentfrost.Annihilation  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Annihilation").InnerText)
		if me._EPStat <> "KillingMachine" then talentfrost.KillingMachine  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/KillingMachine").InnerText)
		if me._EPStat <> "GlacierRot" then talentfrost.GlacierRot  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GlacierRot").InnerText)
		if me._EPStat <> "Deathchill" then talentfrost.Deathchill  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Deathchill").InnerText)
		if me._EPStat <> "ImprovedIcyTalons" then talentfrost.ImprovedIcyTalons  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedIcyTalons").InnerText)
		if me._EPStat <> "MercilessCombat" then talentfrost.MercilessCombat  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MercilessCombat").InnerText)
		if me._EPStat <> "Rime" then talentfrost.Rime  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Rime").InnerText)
		if me._EPStat <> "BloodoftheNorth" then talentfrost.BloodoftheNorth  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodoftheNorth").InnerText)
		if me._EPStat <> "UnbreakableArmor" then talentfrost.UnbreakableArmor  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/UnbreakableArmor").InnerText)
		if me._EPStat <> "GuileOfGorefiend" then talentfrost.GuileOfGorefiend  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GuileOfGorefiend").InnerText)
		if me._EPStat <> "TundraStalker" then talentfrost.TundraStalker  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/TundraStalker").InnerText)
		if me._EPStat <> "ChillOfTheGrave" then talentfrost.ChillOfTheGrave  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ChillOfTheGrave").InnerText)
		if me._EPStat <> "HowlingBlast" then TalentFrost.HowlingBlast = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/HowlingBlast").InnerText)
		if me._EPStat <> "ThreatOfThassarian" then TalentFrost.ThreatOfThassarian= Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ThreatOfThassarian").InnerText)
		
		if me._EPStat <> "ViciousStrikes" then talentunholy.ViciousStrikes  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ViciousStrikes").InnerText)
		if me._EPStat <> "Virulence" then talentunholy.Virulence  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Virulence").InnerText)
		if me._EPStat <> "Epidemic" then talentunholy.Epidemic  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Epidemic").InnerText)
		if me._EPStat <> "Morbidity" then talentunholy.Morbidity  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Morbidity").InnerText)
		if me._EPStat <> "RavenousDead" then talentunholy.RavenousDead  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RavenousDead").InnerText)
		if me._EPStat <> "MasterOfGhouls" then talentunholy.MasterOfGhouls  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MasterOfGhouls").InnerText)
		if me._EPStat <> "Outbreak" then talentunholy.Outbreak  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Outbreak").InnerText)
		if me._EPStat <> "Necrosis" then talentunholy.Necrosis  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Necrosis").InnerText)
		if me._EPStat <> "BloodCakedBlade" then talentunholy.BloodCakedBlade  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodCakedBlade").InnerText)
		if me._EPStat <> "UnholyBlight" then talentunholy.UnholyBlight  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/UnholyBlight").InnerText)
		if me._EPStat <> "Impurity" then talentunholy.Impurity  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Impurity").InnerText)
		if me._EPStat <> "CryptFever" then talentunholy.CryptFever  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/CryptFever").InnerText)
		if me._EPStat <> "ImprovedUnholyPresence" then talentunholy.ImprovedUnholyPresence = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedUnholyPresence").InnerText)
		if me._EPStat <> "BoneShield" then talentunholy.BoneShield  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BoneShield").InnerText)
		if me._EPStat <> "NightoftheDead" then talentunholy.NightoftheDead  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/NightoftheDead").InnerText)
		if me._EPStat <> "GhoulFrenzy" then TalentUnholy.GhoulFrenzy= Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GhoulFrenzy").InnerText)
		if me._EPStat <> "WanderingPlague" then talentunholy.WanderingPlague  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/WanderingPlague").InnerText)
		if me._EPStat <> "EbonPlaguebringer" then talentunholy.EbonPlaguebringer  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/EbonPlaguebringer").InnerText)
		if me._EPStat <> "RageofRivendare" then talentunholy.RageofRivendare  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RageofRivendare").InnerText)
		if me._EPStat <> "SummonGargoyle" then talentunholy.SummonGargoyle  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/SummonGargoyle").InnerText)
		if me._EPStat <> "Dirge" then talentunholy.Dirge  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Dirge").InnerText)
		if me._EPStat <> "Reaping" then talentunholy.Reaping  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Reaping").InnerText)
		
		if me._EPStat <> "Desecration" then talentunholy.Desecration = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Desecration").InnerText)
		if me._EPStat <> "Desolation" then  talentunholy.Desolation = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Desolation").InnerText)
		
			
		
		
		
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
		Desolation = New Desolation(me)
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
		'DamagingObject.Clear
		PetFriendly = SimConstructor.PetFriendly
			
		'_EpStat = SimConstructor.EpStat
		
		Buff = New Buff(Me)
		'Keep this order for RuneX -> Runse -> Rotation/Prio
		Runes = New Runes.runes(Me)
		

		Rotation = new Rotation(Me)
'		Character = New Character(Me)
'		MainStat = New MainStat(Me)
		
		BloodPlague = new BloodPlague(Me)
		FrostFever = New FrostFever(Me)
		
		UnholyBlight = New UnholyBlight(Me)
		' Buff.UnBuff
		
		
		BloodTap = new BloodTap(Me)
		HowlingBlast = New HowlingBlast(Me)
		
		Ghoul = new Ghoul(Me)
		GhoulStat = New GhoulStat(Me)
		Hysteria = new Hysteria(Me)
		DeathChill = new DeathChill(Me)
		
		UnbreakableArmor = new UnbreakableArmor(Me)
		
		Butchery = new Butchery(Me)
		DRW = new DRW(Me)
		RuneStrike = New RuneStrike(Me)
		
		
		
		
		
		'LoadConfig
		Desolation = New Desolation(me)
		
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
		FrostStrike = New FrostStrike(Me)
		
		OHObliterate = New Obliterate(Me)
		OHObliterate.OffHand = true
		OHPlagueStrike= New PlagueStrike(Me)
		OHPlagueStrike.OffHand = true
		OHBloodStrike = New BloodStrike(Me)
		OHBloodStrike.OffHand = true
		OHFrostStrike = New FrostStrike(Me)
		OHFrostStrike.OffHand = true
		OHRuneStrike = New RuneStrike(Me)
		OHRuneStrike.OffHand = true
		
		
		
		MainHand = New MainHand(Me)
		OffHand = New OffHand(Me)
		DeathCoil = new DeathCoil(Me)
		IcyTouch = new IcyTouch(Me)
		Necrosis = new Necrosis(Me)
		WanderingPlague = new WanderingPlague(Me)
		
		Frenzy = NEw Frenzy(Me)
		BloodCakedBlade = New BloodCakedBlade(Me)
		OHBloodCakedBlade = New BloodCakedBlade(Me)
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
		'on error goto errH
		doc.Load("config.xml")
		loadtemplate (Application.StartupPath & "\Templates\" & doc.SelectSingleNode("//config/template").InnerText)
		IntroPath = Application.StartupPath & "\Intro\"  &  doc.SelectSingleNode("//config/intro").InnerText
		If system.IO.File.Exists(IntroPath)  = False Then
			IntroPath = Application.StartupPath & "\Intro\NoIntro.xml"
		End If
		
		KeepBloodSync = doc.SelectSingleNode("//config/BloodSync").InnerText
		If doc.SelectSingleNode("//config/mode").InnerText <> "priority" Then
			rotate = True
			rotationPath = Application.StartupPath & "\Rotation\"  &  doc.SelectSingleNode("//config/rotation").InnerText
		Else
			rotate = False
			loadPriority (Application.StartupPath & "\Priority\" & doc.SelectSingleNode("//config/priority").InnerText)
		End If
		latency = doc.SelectSingleNode("//config/latency").InnerText
		
		
		
		'		cmbCharacter.SelectedItem = doc.SelectSingleNode("//config/Character").InnerText
		
		Dim Sigil As String
		Sigils = New Sigils(Me)
		
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
				sigils.Virulence = True
				
		end select
		
		Dim Presence As String
		Presence = doc.SelectSingleNode("//config/presence").InnerText
		BloodPresence = 0
		UnholyPresence = 0
		FrostPresence = 0
		Select Case Presence
			Case "Blood"
				BloodPresence = 1
			Case "Unholy"
				UnholyPresence=1
			Case "Frost"
				FrostPresence = 1
		End Select
		RuneForge = New RuneForge(Me)
		
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
		Dim xmlcharacter As New Xml.XmlDocument
		
		RunicPower = New RunicPower(Me)
		proc = New procs(Me)
		Character = New Character(Me)
		MainStat = New MainStat(Me)
		
		
		
		
		xmlcharacter.Load(Application.StartupPath & "\characters\" & doc.SelectSingleNode("//config/Character").InnerText)
		
		 
		
		if xmlcharacter.SelectSingleNode("//character/weapon/count").InnerText = 2 then
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
		
		Me.CombatLog.enable = doc.SelectSingleNode("//config/log").InnerText
		Me.CombatLog.LogDetails = doc.SelectSingleNode("//config/logdetail").InnerText
		
		MergeReport = doc.SelectSingleNode("//config/chkMergeReport").InnerText
		ReportName = doc.SelectSingleNode("//config/txtReportName").InnerText
		WaitForFallenCrusader = doc.SelectSingleNode("//config/WaitFC").InnerText
		
		
		
		
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
		
		
		Exit Sub
		errH:
		msgbox("Error reading config file")
	End Sub
	
	Sub loadPriority(file As String)
		priority = new priority(Me)
		priority.prio.Clear
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(file)
		dim Nod as Xml.XmlNode
		
		If KeepBloodSync Then
			priority.prio.Add("BloodSync")
		End If
		
		
		
		For Each Nod In xmldoc.SelectSingleNode("//Priority").ChildNodes
			If Nod.Name = "SaveRPForRuneStrike" Then
				SaveRPForRS = true
			Else
				priority.prio.Add(Nod.Name)
			End If
			
		Next
		
	End Sub
	
	Sub Report()
		'on error resume next
		Dim Tw As System.IO.TextWriter
		if EPStat <> "" then exit sub
		Tw  =system.IO.File.appendText(ReportPath)
		'Tw  = system.IO.File.Open(reportpath, system.IO.FileMode.Append)     '.OpenWrite(ReportPath)
		Tw.Write (ReportName & "<FONT COLOR='white'>[TABLE]</FONT>"  )
		Tw.Write ("<table border='0' cellspacing='2' style='font-family:Verdana; font-size:10px;'>")
		Tw.Write ("<tr>")
		Tw.Write ("	<th rowspan='2' ><b>Ability</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th colspan='4'><b>Damage done</b><FONT COLOR='white'>||||</FONT></th>")
		Tw.Write ("	<th colspan='3'><b>hits</b><FONT COLOR='white'>|||</FONT></th>")
		Tw.Write ("	<th colspan='3'><b>Crits</b><FONT COLOR='white'>|||</FONT></th>")
		Tw.Write ("	<th colspan='2'><b>Misses</b><FONT COLOR='white'>||</FONT></th>")
		Tw.Write ("	<th><b>Uptime</b><FONT COLOR='white'>|</FONT></th>")
		
		If FrostPresence Then
			Tw.Write ("<th rowspan='2'><b>TPS</b><FONT COLOR='white'>|</FONT></th>")
		End If
		Tw.Write ("</tr>")
		Tw.Write ("<tr>")
		Tw.Write ("	<th><FONT COLOR='white'>|</FONT><b>Total</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>Avg</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>Avg</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<td><b>Avg</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("</tr>")
		Tw.Write ("</tr>")
		
		' Sort report
		dim obj as supertype
		Dim myArray As New ArrayList
		
		If MergeReport Then
			For Each obj In DamagingObject
				If obj.total <> 0 Then
					obj.merge
				End If
			Next
		End If
		
		For Each obj In DamagingObject
			If obj.total <> 0 Then
				myArray.Add(obj.total)
				
			End If
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
					STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
					Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
					'obj.cleanup
				End If
			Next
		Next
		
		If Horn.HitCount <> 0 Then
			STmp = Horn.report
			STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		If Pestilence.HitCount <> 0 Then
			STmp = Pestilence.report
			STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		If BoneShield.HitCount <> 0 Then
			STmp = BoneShield.report
			STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		If BloodTap.HitCount <> 0 Then
			STmp = BloodTap.report
			STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		If Frenzy.HitCount <> 0 Then
			STmp = Frenzy.report
			STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		
		If UnbreakableArmor.HitCount <> 0 Then
			STmp = UnbreakableArmor.report
			STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
			Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
		End If
		'On Error Resume Next
		dim pr as Proc
		For Each pr In proc.EquipedTrinkets
			if pr.Total = 0 then
				STmp = pr.report
				STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
			End If
		Next
		
		sTmp = ""
		if EPStat <> "" then STmp =  "<tr><td COLSPAN=8>EP Stat <b>" &  EPStat & "</b></td></tr>"
		STmp = sTmp &  "<tr><td COLSPAN=8>DPS<FONT COLOR='white'>|</FONT>" & VBtab & "<b>" &  DPS & "</b></td></tr>"
		STmp = sTmp &   "<tr><td COLSPAN=8>Total Damage<FONT COLOR='white'>|</FONT>" & VBtab & Math.Round(TotalDamage/1000000,2) & "m" & VBtab &  "<FONT COLOR='white'>|</FONT> in " & MaxTime / 100 / 60/60 & "h</td></tr>"
		Dim ThreatBeforePresence As Long = Threat
		
		For Each obj In Me.DamagingObject
			Threat += obj.Total * obj.ThreadMultiplicator
		Next
		If FrostPresence = 1 Then
			Threat = Threat * 2.0735
		Else
			Threat = (Threat * 0.80) * (1- TalentBlood.Subversion * 8.333/100 )
		End If
		Dim tps As Integer
		Threat = Threat + ThreatBeforePresence
		tps =  100 * Threat / TimeStamp
		STmp = sTmp &  "<tr><td COLSPAN=8>Threat Per Second<FONT COLOR='white'>|</FONT>" & VBtab & "<b>" &  tps & "</b></td></tr>"
		STmp = sTmp &   "<tr><td COLSPAN=8>Generated in <FONT COLOR='white'>|</FONT>" & DateDiff( DateInterval.Second,SimStart,now())  & "s</td></tr>"
		
		STmp = sTmp &   "<tr><td COLSPAN=8>Template:<FONT COLOR='white'>|</FONT> " & Split(Character.GetTemplateFileName,".")(0) & "</td></tr>"
		If Rotate Then
			STmp = sTmp &   "<tr><td COLSPAN=8>Rotation: <FONT COLOR='white'>|</FONT>" & Split(Character.GetRotationFileName,".")(0) & "</td></tr>"
		Else
			STmp = sTmp &   "<tr><td COLSPAN=8>Priority: <FONT COLOR='white'>|</FONT>" & Split(Character.GetPriorityFileName,".")(0) & "</td></tr>"
		End If
		STmp = sTmp &   "<tr><td COLSPAN=8>Presence: <FONT COLOR='white'>|</FONT>" & Character.GetPresence & vbCrLf & "</td></tr>"
		STmp = sTmp &   "<tr><td COLSPAN=8>Sigil: <FONT COLOR='white'>|</FONT>" & Character.GetSigil & vbCrLf & "</td></tr>"
		
		If MainStat.DualW Then
			STmp = sTmp &   "<tr><td COLSPAN=8>RuneEnchant: <FONT COLOR='white'>|</FONT> " & Character.GetMHEnchant  & " / <FONT COLOR='white'>|</FONT>" &  Character.GetOHEnchant & "</td></tr>"
		Else
			STmp = sTmp &   "<tr><td COLSPAN=8>RuneEnchant: <FONT COLOR='white'>|</FONT>" & Character.GetMHEnchant & "</td></tr>"
		End If
		
		STmp = sTmp &   "<tr><td COLSPAN=8>Pet Calculation: <FONT COLOR='white'>|</FONT>" & Character.GetPetCalculation & "</td></tr>"
		
		stmp = stmp & "</table><FONT COLOR='white'>[/TABLE]</FONT><hr width='80%' align='center' noshade ></hr>"
		Tw.WriteLine(stmp)
		tw.Flush
		tw.Close
		_MainFrm.webBrowser1.Navigate(ReportPath)
		'WriteReport(stmp)
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
		
		If Butchery.nextTick < tmp  And TalentBlood.Butchery > 0 Then tmp = Butchery.nextTick
		
		if DeathandDecay.nextTick > TimeStamp and  DeathandDecay.nextTick < tmp then tmp = DeathandDecay.nextTick
		
		if TalentBlood.DRW = 1 then
			If DRW.IsActive(TimeStamp) Then
				if DRW.NextDRW < tmp  then tmp = DRW.NextDRW
			End If
		End If
		If PetFriendly Then
			If TalentUnholy.SummonGargoyle = 1 Then
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
		
		If FrostPresence = 1 Then
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
	
	
	Sub tryOnDamageProc()
		dim obj as proc
		For Each obj In Me.proc.OnDamageProcs
			obj.TryMe(timestamp)
		Next
	End Sub
	
	Sub tryOnMHWhitehitProc()
		dim obj as proc
		For Each obj In Me.proc.OnMHWhitehitProcs
			obj.TryMe(timestamp)
		Next
		TryOnMHHitProc
	End Sub
	
	
	
	Sub TryOnMHHitProc()
		dim obj as proc
		For Each obj In Me.proc.OnMHhitProcs
			obj.TryMe(timestamp)
		Next
		
		For Each obj In Me.proc.OnHitProcs
			obj.TryMe(timestamp)
		Next
		tryOnDamageProc
	End Sub
	
	Sub TryOnOHHitProc
		dim obj as proc
		For Each obj In Me.proc.OnOHhitProcs
			obj.TryMe(timestamp)
		Next
		For Each obj In Me.proc.OnHitProcs
			obj.TryMe(timestamp)
		Next
		tryOnDamageProc
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