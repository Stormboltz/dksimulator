Imports Microsoft.VisualBasic
Public Class Sim
	Friend Cataclysm as Boolean = false
	Friend UselessCheck As Long
	Friend UselessCheckColl as New Collection
	
	
	'Friend Patch as Boolean
	Friend TotalDamageAlternative As Long
	Friend NextFreeGCD As Long
	Friend GCDUsage As Spells.Spell
	
	Friend latency As Long
	Friend TimeStamp As Long
	Friend TimeStampCounter As Long
	Friend _EPStat As String
	Friend EPBase as Integer
	Friend DPS As Long
	Friend TPS As Long
	Friend MaxTime As Long
	Friend NumberOfFights As Long
	Friend RotationStep as Integer
	Friend Rotate as boolean
	Friend rotationPath As String
	Friend IntroPath as String
	Friend PetFriendly As Boolean
	Friend BloodToSync As Boolean
	Friend KeepBloodSync as Boolean
	Friend ShowProc As Boolean
	Friend MultipleDamage as new ArrayList
	Friend NextReset As Integer
	Friend LastReset as Integer
	
	Private SimStart As Date
	Friend ICCDamageBuff As Integer
	Friend BoneShieldTTL as Integer
	Friend FutureEventManager as New FutureEventManager(me)
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
	Friend Ghoul As Ghoul
	Friend AotD as AotD
	
	'Spell Creation
	Friend BloodBoil As BloodBoil
	Friend BloodTap As BloodTap
	Friend Butchery As Butchery
	Friend DeathandDecay as DeathandDecay
	Friend DeathChill As DeathChill
	
	'Friend Desolation As Desolation
	Friend Horn As Horn
	Friend HowlingBlast As HowlingBlast
	Friend Hysteria  as Hysteria
	Friend IcyTouch As IcyTouch
	Friend Necrosis As Necrosis
	Friend OHNecrosis As Necrosis
	Friend DeathCoil as DeathCoil
	Friend Pestilence as Pestilence
	Friend UnbreakableArmor as UnbreakableArmor
	Friend UnholyBlight as UnholyBlight
	Friend DRW As DRW
	Friend WanderingPlague as WanderingPlague
	Friend Gargoyle As Gargoyle
	Friend BoneShield As BoneShield
	Friend Frenzy as Frenzy
	Friend ERW as EmpowerRuneWeapon
	
	
	
	
	Friend proc As procs
	Friend Targets as Targets.TargetsManager
	
	
	
	
	Friend Priority As Priority
	Friend Rotation as Rotation
	Friend RuneForge As RuneForge
	
	Friend Trinkets As Trinkets
	
	Friend DamagingObject as new Collections.ArrayList
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
	
	Friend BloodPresenceSwitch As BloodPresence
	Friend UnholyPresenceSwitch As UnholyPresence
	Friend FrostPresenceSwitch As FrostPresence
	
	
	Friend SaveRPForRS As Boolean
	
	
	Friend ReportName As String
	
	Friend XmlCharacter As New xml.XmlDocument
	Friend XmlConfig As New Xml.XmlDocument
	
	
	Friend Scenario as Scenarios.Scenario
	
	
	
	
	
	Sub Init()
	End Sub
	
	
	
	
	
	Function EPStat() as String
		return _EPStat
	End Function
	
	
	Function ExecuteRange As Boolean
		If (TimeStamp-LastReset) / (NextReset - LastReset) > 0.75 Then
			Return True
		Else
			Return False
		End If
	End Function
	
	
	Private SimTime As Double
	Sub Prepare (SimTime As Double, MainFrm As MainForm)
		me.SimTime = SimTime
		_MainFrm=MainFrm
	End Sub
	Sub Prepare (SimTime As Double, MainFrm As MainForm,EPS As String, EPBse as String )
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
		
		If EPStat <> "" Then
			ProgressFrame.Text = EPStat
		Else
			ProgressFrame.Text = "Simulation"
		End If
	End Sub
	Sub PrePull(T as Long)
		AotD.PrePull(T)
		'Pot Usage
		Try
			If Me.Character.XmlDoc.SelectSingleNode("/character/misc/PrePullIndestructiblePotion").InnerText Then
				Dim p As Proc
				p = proc.Find("Indestructible Potion")
				If p.Equiped = 0 Then
					p.Equip
					p.ProcChance = 0
				End If
				p.ApplyMe(T-1000)
				p.CD = T+120*100
			End If
		Catch ex As System.Exception
			
		End Try
		Try
			If Me.Character.XmlDoc.SelectSingleNode("/character/misc/PrePullPotionofSpeed").InnerText Then
				Dim p As Proc
				p = proc.Find("Potion of Speed")
				If p.Equiped = 0 Then
					p.Equip
					p.ProcChance = 0
				End If
				p.ApplyMe(T-1000)
				p.CD = T+60*100
			End If
		Catch ex As System.Exception
			
		End Try
	End Sub
	
	Function ProcessEvent(FE As FutureEvent) As Boolean
		'this routine returns whether an explicit action was taken
		proc.Bloodlust.TryMe(TimeStamp)
		Select Case FE.Ev
			Case "Boss"
				If FrostPresence = 1 Then
					If Boss.NextHit <= TimeStamp Then
						Boss.ApplyDamage(TimeStamp)
					End If
				End If
			Case "GCD", "Rune"
				If isInGCD(TimeStamp) Then Return True
				If me.Rotation.IntroDone = false Then
					rotation.DoIntro(TimeStamp)
					if isInGCD(TimeStamp) then Return True
				End If
				If BoneShieldUsageStyle = 2 Then 'after BS
					If runes.BloodRune1.Available(TimeStamp) = False and runes.BloodRune1.death = true  Then
						If runes.BloodRune2.Available(TimeStamp) = False and runes.BloodRune2.death = true Then
							if BoneShield.IsAvailable(TimeStamp) Then
								if BoneShield.Use(TimeStamp) then Return True
							End If
							If UnbreakableArmor.IsAvailable(TimeStamp) Then
								if UnbreakableArmor.Use(TimeStamp) then Return True
							End If
						End If
					End If
				End If
				If me.Rotation.IntroDone = true Then
					if Rotate then
						Rotation.DoRoration(TimeStamp)
					else
						Priority.DoNext (TimeStamp)
					End If
				End If
				If isInGCD(TimeStamp) Then Return True
				If BoneShieldUsageStyle = 4 Then 'after Death rune OB/SS with cancel aura
					If runes.BloodRune1.Available(TimeStamp) = False and runes.BloodRune1.death = false Then
						If runes.BloodRune2.Available(TimeStamp) = False and runes.BloodRune2.death = false Then
							if BoneShield.IsAvailable(TimeStamp) Then
								If BoneShield.Use(TimeStamp) Then
									BloodTap.CancelAura
									Return True
								End If
							End If
							If UnbreakableArmor.IsAvailable(TimeStamp) Then
								If UnbreakableArmor.Use(TimeStamp) Then
									BloodTap.CancelAura
									Return True
								End If
							End If
						End If
					End If
				End If
				if PetFriendly then
					If Ghoul.ActiveUntil < TimeStamp and Ghoul.cd < TimeStamp and CanUseGCD(TimeStamp) Then
						Ghoul.Summon(TimeStamp)
						If isInGCD(TimeStamp) Then Return True
					end if
					If AoTD.cd < TimeStamp and CanUseGCD(TimeStamp) Then
						AoTD.Summon(TimeStamp)
						If isInGCD(TimeStamp) Then Return True
					end if
				End If
				If me.Rotation.IntroDone Then
					If horn.isAutoAvailable(TimeStamp) and CanUseGCD(TimeStamp) Then
						horn.use(TimeStamp)
						If isInGCD(TimeStamp) Then Return True
					end if
				End If
				
			Case "AotD"
				if AoTD.ActiveUntil >= TimeStamp then
					If AoTD.NextWhiteMainHit <= TimeStamp Then AoTD.ApplyDamage(TimeStamp)
					If AoTD.NextClaw <= TimeStamp Then AoTD.Claw(TimeStamp)
				End If
				
			Case "Disease"
				Dim t As Targets.Target
				For Each t In Targets.AllTargets
					If t.BloodPlague.isActive(TimeStamp) Then
						If t.BloodPlague.nextTick = TimeStamp Then
							t.BloodPlague.ApplyDamage(TimeStamp)
						End If
					End If
					If t.FrostFever.isActive(TimeStamp) Then
						If t.FrostFever.nextTick = TimeStamp Then
							t.FrostFever.ApplyDamage(TimeStamp)
						End If
					End If
				Next
			Case "DRW"
				If DRW.IsActive(TimeStamp) Then
					if DRW.NextDRW <= TimeStamp then DRW.ApplyDamage(TimeStamp)
				End If
			Case "Gargoyle"
				If Gargoyle.ActiveUntil >= TimeStamp Then
					If Gargoyle.NextGargoyleStrike <= TimeStamp Then Gargoyle.ApplyDamage(TimeStamp)
				End If
				
			Case "Ghoul"
				if Ghoul.ActiveUntil >= TimeStamp then
					Ghoul.TryActions(TimeStamp)
				End If
				
			Case "Butchery"
				Butchery.apply(TimeStamp)
				
			Case "MainHand"
				MainHand.ApplyDamage(TimeStamp)
				
			Case "OffHand"
				OffHand.ApplyDamage(TimeStamp)
				
			Case "D&D"
				DeathandDecay.ApplyDamage(TimeStamp)
			Case "AMS"
				AMSTimer = TimeStamp + AMSCd
				RunicPower.add(AMSAmount)
				FutureEventManager.Add(AMSTimer + AMSCd,"AMS")
			Case "RuneFill"
				Me.Runes.FillRunes
			Case "Scenario"
				Dim e As Scenarios.Element
				
				For Each e In Me.Scenario.Elements
					If e.Start <= TimeStamp And e.Ending >= TimeStamp Then
						If e.CanTakeDiseaseDamage = False Then
							FutureEventManager.reschedule("Disease",e.Ending)
						End If
						If e.CanTakePetDamage = False Then
							FutureEventManager.reschedule("AotD",e.Ending)
							FutureEventManager.reschedule("Ghoul",e.Ending)
							FutureEventManager.reschedule("Gargoyle",e.Ending)
							FutureEventManager.reschedule("DRW",e.Ending)
						End If
						If e.CanTakePlayerStrike = False Then
							FutureEventManager.reschedule("GCD",e.Ending)
							FutureEventManager.reschedule("Rune",e.Ending)
							FutureEventManager.reschedule("MainHand",e.Ending)
							FutureEventManager.reschedule("OffHand",e.Ending)
						End If
						
					End If
				Next
			Case "AddPop"
				Dim e As Scenarios.Element
				For Each e In Me.Scenario.Elements
					If e.Start = Timestamp And e.AddPop <> 0 Then
						me.KeepDiseaseOnOthersTarget = e.SpreadDisease
						Dim i as Integer = 0
						Do Until i = e.AddPop
							Dim Add As Targets.target
							add = new Targets.target(me)
							i+= 1
						Loop
					End If
				Next
				
			Case "AddDepop"
				Dim e As Scenarios.Element
				For Each e In Me.Scenario.Elements
					If e.Ending = Timestamp and e.AddPop <> 0 Then
						Dim i as Integer = 0
						targets.KillEveryoneExceptMainTarget
					End If
				Next
			Case "SuperBuff"
				Dim e As Scenarios.Element
				Dim prc As New Proc(Me)
				
				For Each e In Me.Scenario.Elements
					If e.start = Timestamp And e.DamageBonus <> 0 Then
						With prc
							.ProcChance = 1
							.ProcLenght = e.length/100
							.ProcValue = e.DamageBonus
							.ProcType = "percent"
							._Name = "SuperBuff"
							.ProcOn = procs.ProcOnType.OnMisc
							.Equip
							.TryMe(Timestamp)
							.ProcChance = 0
						End With
					End If
				Next
				
				
				
			Case Else
				Debug.Print ("WTF is this event ?")
		End Select
		return False
	End Function
	
	Sub Start()
		SimStart = now
		LoadConfig
		
		
		Rnd(-1) 'Tell VB to initialize using Randomize's parameter
		RandomNumberGenerator = new RandomNumberGenerator 'init here, so that we don't get the same rng numbers for short fights.
		
		MaxTime = SimTime * 60 * 60 * 100
		TotalDamageAlternative = 0
		TimeStampCounter = 1
		Dim resetTime As Integer
		If _MainFrm.chkManyFights.Checked Then
			NumberOfFights = Math.Round( ( SimTime * 60 * 60 ) / _MainFrm.txtManyFights.text )
			resetTime = _MainFrm.txtManyFights.text * 100
			NextReset = resetTime
			if NextReset > MaxTime then NextReset = MaxTime
		Else
			NumberOfFights = 1
			resetTime = MaxTime + 1
			NextReset = MaxTime
		End If
		Dim intCount As Integer
		'Init
		Initialisation
		TimeStamp = 1
		If Character.TalentUnholy.MasterOfGhouls=1 Then Ghoul.Summon(1)
		Rotation.LoadIntro
		If Rotate Then Rotation.loadRotation
		' Pre Pull Activities
		
		PrePull(TimeStamp)
		SoftReset
		Dim FE As FutureEvent
		Do Until False
			'TimeStamp = FastFoward(TimeStamp)
			
			FE = Me.FutureEventManager.GetFirst
			TimeStamp = FE.T
			
			If TimeStamp >= NextReset Then
				If MaxTime = NextReset Then Exit Do
				StoreMyDamage(TotalDamage)
				LastReset = NextReset
				NextReset += resetTime
				If NextReset > MaxTime Then NextReset = MaxTime
				SoftReset
			End If
			ProcessEvent(FE)
			application.DoEvents
		Loop
		
		TotalDamageAlternative = TotalDamageAlternative + TotalDamage
		TimeStampCounter = TimeStampCounter + TimeStamp
		'TotalDamage = TotalDamageAlternative
		TimeStamp = TimeStampCounter
		
		
		
		dim obj as supertype
		If ICCDamageBuff > 0 Then
			For Each obj In DamagingObject
				If obj.isGuardian = False Then
					obj.total *= (1+ ICCDamageBuff/100)
					obj.TotalCrit*= (1+ ICCDamageBuff/100)
					obj.TotalHit*= (1+ ICCDamageBuff/100)
					obj.TotalGlance*= (1+ ICCDamageBuff/100)
				End If
			Next
		End If
		
		
		DPS = 100 * TotalDamage / TimeStamp
		Report()
		Debug.Print( "DPS=" & DPS & " " & "TPS=" & TPS & " " & EPStat & " hit=" & mainstat.Hit & " sphit=" & mainstat.SpellHit & " exp=" & mainstat.expertise )
		'Debug.Print( "UselessCheck = " & UselessCheck)
		Debug.Print("Max events in queue " & me.FutureEventManager.Max )
		combatlog.finish
		On Error Resume Next
		If Me.FrostPresence = 1 Then
			SimConstructor.DPSs.Add(TPS, Me.EPStat)
		Else
			SimConstructor.DPSs.Add(DPS, Me.EPStat)
		End If
		'SimConstructor.simCollection.Remove(me)
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
	
	Sub _UseGCD(T As Long, Length As Long)
		dim tmp as Long
		GCDUsage.HitCount += 1
		
		
		If Length + T > NextReset Then
			tmp = (NextReset - T)
		Else
			tmp = Length
		End If
		If NextFreeGCD > T Then
			'should never happen currently is called when use BS etc is after a special
			tmp += T - NextFreeGCD
		End If
		GCDUsage.uptime += tmp
		T += Length
		If T > NextFreeGCD Then
			NextFreeGCD = T
			FutureEventManager.Add(NextFreeGCD,"GCD")
		End If
	End Sub
	
	Sub UseGCD(T As Long, Spell As Boolean)
		Dim tmp As Long
		If UnholyPresence Then
			tmp = 100
		ElseIf Spell Then
			tmp = Math.Max(150.0 / MainStat.SpellHaste, 100)
		Else
			tmp = 150
		End If
		tmp += latency / 10
		_UseGCD(T, tmp)
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
		If character.glyph.Disease Then
			dim tGDC as long
			'return false
			If UnholyPresence Then
				tGDC = 100+ latency/10 + 50
			Else
				tGDC =  150+ latency/10 + 50
			End If
			
			If math.Min(targets.MainTarget.BloodPlague.FadeAt,targets.MainTarget.FrostFever.FadeAt) < (T +  tGDC) Then
				'debug.Print (RuneState & "time left on disease= " & (math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) -T)/100 & "s" & " - " & T/100)
				return false
			End If
		End If
	End Function
	
	
	
	
	
	
	
	Function DoMyWhiteHit As Boolean
		'unused
	End Function
	
	Sub SoftReset()
		FutureEventManager.Clear
		Me.RotationStep = 0
		Me.Rotation.IntroStep=0
		Runes.SoftReset()
		FutureEventManager.Add(TimeStamp,"Rune")
		
		
		
		RunicPower.Reset()
		targets.KillEveryoneExceptMainTarget
		dim T as Targets.target
		For Each t In targets.AllTargets
			t.BloodPlague.nextTick = 0
			t.BloodPlague.FadeAt = 0
			t.FrostFever.nextTick = 0
			t.FrostFever.FadeAt = 0
		Next
		
		
		BloodTap.CD  = 0
		HowlingBlast.CD = 0
		Ghoul.cd = 0
		AoTD.cd = 0
		Hysteria.CD = TimeStamp
		DeathChill.Cd = 0
		'Desolation = New Desolation(me)
		MainHand.NextWhiteMainHit = TimeStamp
		FutureEventManager.Add(TimeStamp,"MainHand")
		
		if MainStat.DualW then
			OffHand.NextWhiteOffHit = TimeStamp
			FutureEventManager.Add(TimeStamp,"OffHand")
		End If
		
		Gargoyle.NextGargoyleStrike = 0
		
		Ghoul.NextClaw = TimeStamp
		Ghoul.NextWhiteMainHit = TimeStamp
		FutureEventManager.Add(TimeStamp,"Ghoul")
		Frenzy.CD = 0
		DeathandDecay.CD = 0
		Gargoyle.cd = 0
		Horn.CD = 0
		'Bloodlust.Cd = 0
		proc.SoftReset
		Trinkets.SoftReset
		BoneShield.CD = 0
		BoneShield.PreBuff
		Scenario.SoftReset
		NextFreeGCD = 0
		AMSCd = _MainFrm.txtAMScd.text * 100
		AMSTimer = TimeStamp + AMSCd
		AMSAmount = _MainFrm.txtAMSrp.text
		If AMSAmount <> 0 and AMSCd <> 0  Then
			FutureEventManager.Add(AMSTimer + AMSCd,"AMS")
		End If
		
		ERW.CD = 0
		RuneForge.SoftReset
		If Character.TalentBlood.Butchery > 0 Then
			Butchery.nextTick = TimeStamp + 500
			FutureEventManager.Add(Butchery.nextTick,"Butchery")
		End If
		me.Rotation.IntroDone = false
		PrePull(TimeStamp)
		
	End Sub
	
	
	Sub Initialisation()
		'RandomNumberGenerator.Init 'done in Start
		'DamagingObject.Clear
		PetFriendly = SimConstructor.PetFriendly
		
		'_EpStat = SimConstructor.EpStat
		
		'Buff = New Buff(Me)
		
		Dim targ As New Targets.Target(Me)
		Targets.CurrentTarget = targ
		Targets.MainTarget = targ
		
		'Keep this order for RuneX -> Runse -> Rotation/Prio
		Runes = New Runes.runes(Me)
		Rotation = new Rotation(Me)
		
		UnholyBlight = New UnholyBlight(Me)
		
		BloodTap = new BloodTap(Me)
		HowlingBlast = New HowlingBlast(Me)
		
		Ghoul = New Ghoul(Me)
		AotD = New AotD(Me)
		GhoulStat = New GhoulStat(Me)
		Hysteria = new Hysteria(Me)
		DeathChill = new DeathChill(Me)
		UnbreakableArmor = new UnbreakableArmor(Me)
		Butchery = new Butchery(Me)
		DRW = New DRW(Me)
		RuneStrike = New RuneStrike(Me)
		'LoadConfig
		'Desolation = New Desolation(me)
		RunicPower.Reset()
		NextFreeGCD = 0
		Threat = 0
		
	
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
		
		BloodPresenceSwitch = new BloodPresence(me)
		UnholyPresenceSwitch = new UnholyPresence(me)
		FrostPresenceSwitch = new FrostPresence(me)
		
		
		MainHand = New MainHand(Me)
		OffHand = New OffHand(Me)
		DeathCoil = new DeathCoil(Me)
		IcyTouch = new IcyTouch(Me)
		Necrosis = new Necrosis(Me)
		OHNecrosis = New Necrosis(Me)
		OHNecrosis.Offhand = True
		WanderingPlague = new WanderingPlague(Me)
		
		Frenzy = NEw Frenzy(Me)
		BloodCakedBlade = New BloodCakedBlade(Me)
		OHBloodCakedBlade = New BloodCakedBlade(Me)
		OHBloodCakedBlade.OffHand = true
		
		DeathStrike = New DeathStrike(Me)
		OHDeathStrike = New DeathStrike(Me)
		BloodBoil = new BloodBoil(me)
		HeartStrike = new HeartStrike(Me)
		DeathandDecay = new DeathandDecay(Me)
		Gargoyle = New Gargoyle(Me)
		
		
		Horn = new Horn(Me)
		'Bloodlust= new Bloodlust(Me)
		Pestilence = new Pestilence(Me)
		
		proc.Init
		BoneShield  = New BoneShield(Me)
		ERW = New EmpowerRuneWeapon(Me)
		
		Scenario = New Scenarios.Scenario(Me)
		
		
		AMSCd = _MainFrm.txtAMScd.text * 100
		AMSTimer = _MainFrm.txtAMScd.text * 100
		AMSAmount = _MainFrm.txtAMSrp.text
		
		InterruptCd = _MainFrm.txtInterruptCd.text * 100
		InterruptTimer = _MainFrm.txtInterruptCd.text * 100
		InterruptAmount = _MainFrm.txtInterruptAmount.text
		
		ShowDpsTimer = 1
		
		GCDUsage = New Spells.Spell(Me)
		GCDUsage._Name = "GCD Usage"
		
		
		
		
		
	End Sub
	
	Sub LoadConfig
		XmlConfig.Load("config.xml")
		
		If XmlConfig.SelectSingleNode("//config/UseCharacter").InnerText = True Then
			XmlCharacter.Load(Application.StartupPath & "\characters\" & XmlConfig.SelectSingleNode("//config/Character").InnerText)
		Else
			xmlcharacter.Load(Application.StartupPath & "\CharactersWithGear\" & XmlConfig.SelectSingleNode("//config/CharacterWithGear").InnerText)
		End If
		
		
		IntroPath = Application.StartupPath & "\Intro\"  &  XmlConfig.SelectSingleNode("//config/intro").InnerText
		
		If system.IO.File.Exists(IntroPath)  = False Then
			IntroPath = Application.StartupPath & "\Intro\NoIntro.xml"
		End If
		
		KeepBloodSync = XmlConfig.SelectSingleNode("//config/BloodSync").InnerText
		If XmlConfig.SelectSingleNode("//config/mode").InnerText <> "priority" Then
			rotate = True
			rotationPath = Application.StartupPath & "\Rotation\"  &  XmlConfig.SelectSingleNode("//config/rotation").InnerText
		Else
			rotate = False
			loadPriority (Application.StartupPath & "\Priority\" & XmlConfig.SelectSingleNode("//config/priority").InnerText)
		End If
		latency = XmlConfig.SelectSingleNode("//config/latency").InnerText
		ShowProc = XmlConfig.SelectSingleNode("//config/ShowProc").InnerText
		
		Dim Sigil As String
		Sigils = New Sigils(Me)
		
		Sigil = XmlConfig.SelectSingleNode("//config/sigil").InnerText
		Sigils.WildBuck = false
		Sigils.FrozenConscience = false
		Sigils.DarkRider = false
		Sigils.ArthriticBinding = false
		Sigils.Awareness = false
		Sigils.Strife = false
		Sigils.HauntedDreams = false
		sigils.VengefulHeart = False
		sigils.Virulence = False
		sigils.HangedMan = False
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
			Case "HangedMan"
				sigils.HangedMan = True
		end select
		
		Dim Presence As String
		Presence = XmlConfig.SelectSingleNode("//config/presence").InnerText
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
		RunicPower = New RunicPower(Me)
		proc = New procs(Me)
		Character = New Character(Me)
		MainStat = New MainStat(Me)
		
		Me.CombatLog.enable = XmlConfig.SelectSingleNode("//config/log").InnerText
		Me.CombatLog.LogDetails = XmlConfig.SelectSingleNode("//config/logdetail").InnerText
		
		MergeReport = XmlConfig.SelectSingleNode("//config/chkMergeReport").InnerText
		ReportName = XmlConfig.SelectSingleNode("//config/txtReportName").InnerText
		WaitForFallenCrusader = XmlConfig.SelectSingleNode("//config/WaitFC").InnerText
		'Patch = doc.SelectSingleNode("//config/Patch").InnerText
		Dim tmp As String
		tmp = XmlConfig.SelectSingleNode("//config/BShOption").InnerText
		Select Case tmp
			Case "Instead of Blood Strike"
				Me.BoneShieldUsageStyle =1
			Case "After BS/BB"
				Me.BoneShieldUsageStyle= 2
			Case "Instead of Blood Boil"
				Me.BoneShieldUsageStyle = 3
			Case "After Death rune OB/SS with cancel aura"
				Me.BoneShieldUsageStyle = 4
			Case Else
				Me.BoneShieldUsageStyle= 2
		End Select
		
		
		Try
			ICCDamageBuff = XmlConfig.SelectSingleNode("//config/ICCBuff").InnerText
		Catch
			
			ICCDamageBuff  = 0
		End Try
		
		Try
			BoneShieldTTL = XmlConfig.SelectSingleNode("//config/BSTTL").InnerText
		Catch
			BoneShieldTTL = 300
		End Try
		
		
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
			priority.prio.Add("BloodSync","BloodSync")
		End If
		
		
		
		For Each Nod In xmldoc.SelectSingleNode("//Priority").ChildNodes
			If Nod.Name = "SaveRPForRuneStrike" Then
				SaveRPForRS = true
			Else
				priority.prio.Add(Nod.Name,Nod.Name)
			End If
			
		Next
		
	End Sub
	
	Sub Report()
		'on error resume next
		Dim Tw As System.IO.TextWriter
		
		
		' Sort report
		
		Dim myArray As New ArrayList
		
		dim obj as supertype
		
		'MergeDisease
		
		For Each obj In DamagingObject
			If TypeOf obj is Diseases.BloodPlague Then
				If obj.total <> 0 Then
					obj.merge
				End If
			End If
		Next
		
		For Each obj In DamagingObject
			If TypeOf obj is Diseases.FrostFever Then
				If obj.total <> 0 Then
					obj.merge
				End If
			End If
		Next
		
		
		
		
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
		
		Dim ThreatBeforePresence As Long = Threat
		For Each obj In Me.DamagingObject
			Threat += obj.Total * obj.ThreadMultiplicator
		Next
		If FrostPresence = 1 Then
			Threat = Threat * 2.0735
		Else
			Threat = (Threat * 0.80) * (1- Character.TalentBlood.Subversion * 8.333/100 )
		End If
		
		Threat = Threat + ThreatBeforePresence
		tps =  100 * Threat / TimeStamp
		
		
		
		
		
		If EPStat <> "" Then Exit Sub
		
		
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
		Tw.Write ("	<th colspan='3'><b>Glances</b><FONT COLOR='white'>|||</FONT></th>")
		Tw.Write ("	<th><b>Uptime</b><FONT COLOR='white'>|</FONT></th>")
		
		If FrostPresence Then
			Tw.Write ("<th rowspan='2'><b>TPS</b><FONT COLOR='white'>|</FONT></th>")
		End If
		Tw.Write ("</tr>")
		Tw.Write ("<tr>")
		Tw.Write ("	<th><FONT COLOR='white'>|</FONT><b>Total</b><FONT COLOR='white'>|</FONT></th>")
		'Total
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>Avg</b><FONT COLOR='white'>|</FONT></th>")
		
		'Hit
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>Avg</b><FONT COLOR='white'>|</FONT></th>")
		'Crit
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<td><b>Avg</b><FONT COLOR='white'>|</FONT></th>")
		
		'Misses
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>Avg</b><FONT COLOR='white'>|</FONT></th>")
		
		'Glance
		Tw.Write ("	<th><b>#</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("	<td><b>Avg</b><FONT COLOR='white'>|</FONT></th>")
		
		'Uptime
		Tw.Write ("	<th><b>%</b><FONT COLOR='white'>|</FONT></th>")
		Tw.Write ("</tr>")
		Tw.Write ("</tr>")
		
		
		
		
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
		If ShowProc Then
			If True Then
				STmp = GCDUsage.report
				STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
				STmp = Runes.BloodRune1.report
				STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				STmp = Runes.BloodRune2.report
				STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
				STmp = Runes.FrostRune1.report
				STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
				STmp = Runes.FrostRune2.report
				STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
				STmp = Runes.UnholyRune1.report
				STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				STmp = Runes.UnholyRune2.report
				STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
				Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				
				
			End If
			
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
			Dim pr As Proc
			For Each pr In proc.EquipedProc
				if pr.Total = 0 then
					STmp = pr.report
					STmp = replace(STmp,vbtab,"<FONT COLOR='white'>|</FONT></td><td>")
					Tw.WriteLine("<tr><td>" & sTmp & "</tr>")
				End If
			Next
		End If
		
		sTmp = ""
		if EPStat <> "" then STmp =  "<tr><td COLSPAN=8>EP Stat <b>" &  EPStat & "</b></td></tr>"
		'STmp = sTmp &  "<tr><td COLSPAN=8>DPS<FONT COLOR='white'>|</FONT>" & VBtab & "<b>" &  DPS & "</b></td></tr>"
		
		Dim minDPS As Integer
		dim maxDPS as Integer
		Dim MinMAx As Integer
		Dim range As Double
		
		If 	MultipleDamage.Count > 1 Then
			MultipleDamage.Sort
			minDPS = MultipleDamage.Item(1)/(_MainFrm.txtManyFights.text)
			maxDPS = MultipleDamage.Item(MultipleDamage.Count-1)/(_MainFrm.txtManyFights.text)
			MinMAx = math.Max(DPS-minDPS,maxDPS-DPS)
			range = (maxDPS-minDPS)/(2*DPS)
			STmp = sTmp &  "<tr><td COLSPAN=8>DPS<FONT COLOR='white'>|</FONT>" & VBtab & "<b>" &  DPS & "(+/- " & MinMAx & ")</b></td></tr>"
		Else
			STmp = sTmp &  "<tr><td COLSPAN=8>DPS<FONT COLOR='white'>|</FONT>" & VBtab & "<b>" &  DPS & "</b></td></tr>"
		End If
		STmp = sTmp &   "<tr><td COLSPAN=8>Total Damage<FONT COLOR='white'>|</FONT>" & VBtab & Math.Round(TotalDamage/1000000,2) & "m" & VBtab &  "<FONT COLOR='white'>|</FONT> in " & MaxTime / 100 / 60/60 & "h</td></tr>"
		STmp = sTmp & "<tr><td COLSPAN=8>" & RunicPower.report() & "</td></tr>"
		
		
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
	End Sub
	
	
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
	
	Sub StoreMyDamage(damage As Long)
		Dim tmp As Long
		Dim i As Integer
		on error resume next
		tmp = damage
		For i=0 To MultipleDamage.Count
			tmp = tmp - integer.Parse(MultipleDamage.Item(i).ToString)
		Next
		MultipleDamage.Add(tmp)
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
	
	Sub TryOnFU()
		dim obj as proc
		For Each obj In Me.proc.OnFUProcs
			obj.TryMe(timestamp)
		Next
	End Sub
	
	Sub tryOnCrit()
		dim obj as proc
		For Each obj In Me.proc.OnCritProcs
			obj.TryMe(timestamp)
		Next
	End Sub
	
	Sub tryOnDoT()
		dim obj as proc
		For Each obj In Me.proc.OnDoTProcs
			obj.TryMe(timestamp)
		Next
		tryOnDamageProc
	End Sub
	
	Sub TryOnSpellHit
		dim obj as proc
		For Each obj In Me.proc.OnDamageProcs
			obj.TryMe(timestamp)
		Next
		tryOnDamageProc
	End Sub
	
	Sub TryOnBloodStrike
		dim obj as proc
		For Each obj In Me.proc.OnBloodStrikeProcs
			obj.TryMe(timestamp)
		Next
	End Sub
	
	
	
	
	
End Class