'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 00:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Procs
	Friend Bloodlust As Proc

	Friend DRM As Proc
	Friend SuddenDoom As Proc

	Friend AnnihilateDiseases As Proc
	Friend ThreatOfThassarian As Proc
	Friend ReapingBotN As Proc

	Friend T104PDPS As Proc
	Friend IcyTalons As Proc
	Friend Desolation As Proc
	
	Friend KillingMachine As Proc
	Friend Rime As Proc
	Friend ScentOfBlood As ScentOfBlood
	Friend Strife As Proc
	Friend TrollRacial As Proc
	
	Friend MHBloodCakedBlade As Proc
	Friend OHBloodCakedBlade As Proc
	
	
	

	Protected Sim as Sim
	'Friend T104PDPSFAde As Integer
	
	
	
	Friend AllProcs As New Collection
	Friend EquipedProc as New Collection
	Friend OnHitProcs As New Collection
	
	Friend OnMHWhitehitProcs As new Collection
	Friend OnMHhitProcs As new Collection
	Friend OnOHhitProcs As New Collection
	Friend OnFUProcs As new Collection
	Friend OnCritProcs As new Collection
	Friend OnDamageProcs As new Collection
	Friend OnDoTProcs As New Collection
	Friend OnBloodStrikeProcs As New Collection
	Friend OnPlagueStrikeProcs As New Collection
	Private XmlCharacter as Xml.XmlDocument
	

	Public Enum ProcOnType
		OnMisc = 0
		OnHit = 1
		OnMHhit = 2
		OnOHhit = 3
		OnCrit = 4
		OnDamage = 5
		OnDoT = 6
		OnMHWhiteHit=7
		OnFU=8
		OnBloodStrike=9
		OnPlagueStrike = 10
		'OnUse=9
	End Enum
		

	

	
	
	Sub New(S As Sim)
		sim = S
		XmlCharacter = S.XmlCharacter
	End Sub
	
	Function Find(name As String) As Proc
		Dim prc As proc
		For Each prc In AllProcs
			If prc.Name = name Then
				Return prc
			Else
				'debug.Print (prc.Name)
			End If
		Next
		return nothing
	End Function
	
	
	
	Sub SoftReset
		Dim prc As proc
		For Each prc In AllProcs
			prc.CD = 0
			prc.Fade = 0
			prc.Stack = 0
		Next
		Bloodlust.CD = Sim.TimeStamp + 500
	End Sub
	
	Sub Init()
		Dim s As Sim
		s= Me.Sim
		
		s.RuneForge.Init()
		
		MHBloodCakedBlade = New Proc(s)
		With MHBloodCakedBlade
			._Name = "MH Blood-Caked Blade"
			.ProcChance = sim.Character.talentunholy.BloodCakedBlade * 0.1
			If .ProcChance > 0 Then
				.Equip()
			End If
		End With
		
		OHBloodCakedBlade = New Proc(s)
		With OHBloodCakedBlade
			._Name = "OH Blood-Caked Blade"
			.ProcChance = sim.Character.talentunholy.BloodCakedBlade * 0.1
			If .ProcChance > 0 Then
				.Equip()
			End If
		End With
		

		Bloodlust = New Proc(s)
		With Bloodlust
			._Name = "Bloodlust"
			.ProcChance = 1
			.ProcLenght = 40
			.InternalCD = 10 * 60
			.CD = 500
			If Sim.Character.Buff.Bloodlust Then .Equip()
		End With

		DRM = New Proc(s)
		With DRM
			._Name = "DeathRuneMastery"
			.ProcChance = sim.Character.talentblood.DRM * 0.33
			If .ProcChance > 0 Then
				If .ProcChance > 0.85 Then .ProcChance = 1.0
				.Equip()
			End If
		End With

		SuddenDoom = New Proc(s)
		With SuddenDoom
			._Name = "SuddenDoom"
			.ProcChance = sim.Character.talentblood.SuddenDoom * 0.05
			.ProcOn = procs.ProcOnType.OnBloodStrike
			If .ProcChance > 0 Then
				.Equip()
			End If
		End With


		ThreatOfThassarian = New Proc(s)
		With ThreatOfThassarian
			._Name = "ThreatOfThassarian"
			.ProcChance = 0.3 * sim.Character.talentfrost.ThreatOfThassarian
			If .ProcChance > 0 Then
				If .ProcChance > 0.85 Then .ProcChance = 1.0
				If Sim.MainStat.DualW Then .Equip()
			End If
		End With

		AnnihilateDiseases = New Proc(s)
		With AnnihilateDiseases
			._Name = "AnnihilateDiseases"
			.ProcChance = 1 - 0.33 * sim.Character.talentfrost.Annihilation
			If .ProcChance > 0.1 Then
				.Equip()
			End If
		End With


		ReapingBotN = New Proc(s)
		With ReapingBotN
			If sim.Character.talentunholy.Reaping Then
				._Name = "Reaping"
				.ProcChance = sim.Character.talentunholy.Reaping * 0.33
			ElseIf sim.Character.talentfrost.BloodoftheNorth Then
				._Name = "BloodoftheNorth"
				.ProcChance = sim.Character.talentfrost.BloodoftheNorth * 0.3
			End If

			If .ProcChance > 0 Then
				If .ProcChance > 0.85 Then .ProcChance = 1.0
				.Equip()
			End If
		End With


		T104PDPS = New Proc(s)
		With T104PDPS
			._Name = "T104PDPS"
			If Sim.MainStat.T104PDPS Then .Equip()
			.ProcLenght = 15
			.ProcValue = 3
			.ProcChance = 1

		End With

		IcyTalons = New Proc(s)
		With IcyTalons
			._Name = "IcyTalons"
			If sim.Character.talentfrost.IcyTalons > 0 Then .Equip()
			.ProcValue = sim.Character.talentfrost.IcyTalons
			.ProcLenght = 20
			.ProcChance = 1
		End With

		Desolation = New Proc(s)
		With Desolation
			._Name = "Desolation"
			.ProcOn = procs.ProcOnType.OnBloodStrike
			.ProcValue = sim.Character.talentunholy.Desolation
			.ProcLenght = 20
			.ProcChance = 1
			If sim.Character.talentunholy.Desolation > 0 Then .Equip()
		End With


		KillingMachine = New Proc(s)
		With KillingMachine
			._Name = "KillingMachine"
			.ProcOn = Procs.ProcOnType.OnMHWhiteHit
			If sim.Character.talentfrost.KillingMachine > 0 Then .Equip()
			.Equiped = sim.Character.talentfrost.KillingMachine
			.ProcLenght = 30
			.ProcChance = (sim.Character.talentfrost.KillingMachine) * s.MainStat.MHWeaponSpeed / 60
		End With

		Rime = New Proc(s)
		With Rime
			._Name = "Rime"
			If sim.Character.talentfrost.Rime > 0 Then .Equip()
			.Equiped = sim.Character.talentfrost.Rime
			.ProcLenght = 15
			.ProcChance = 5 * sim.Character.talentfrost.Rime / 100
		End With

		ScentOfBlood = New ScentOfBlood(s)
		With ScentOfBlood
			._Name = "ScentOfBlood"
			If s.FrostPresence = 1 Then
				.Equip()
				.Equiped = sim.Character.talentblood.ScentOfBlood
			Else
				.Equiped = 0
			End If
			.ProcLenght = 60
			.ProcChance = 0.15
		End With

		With New Proc(s)
			._Name = "Virulence"
			.ProcLenght = 20
			.ProcChance = 0.85
			.ProcValue = 200
			.ProcType = "str"
			.ProcOn = Procs.ProcOnType.OnFU
			If s.Sigils.Virulence Then .Equip()
		End With

		With New Proc(s)
			._Name = "HangedMan"
			.ProcLenght = 15
			.ProcChance = 1
			.ProcValueStack = 73
			.ProcTypeStack = "str"
			.MaxStack = 3
			.ProcOn = Procs.ProcOnType.OnFU
			If s.Sigils.HangedMan Then .Equip()
		End With


		Strife = New Proc(s)
		With Strife
			._Name = "Strife"
			.ProcChance = 1
			.ProcValue = 144
			.ProcLenght = 10
			.ProcType = "ap"
			If s.Sigils.Strife Then .Equip()
		End With

		With New Proc(s)
			._Name = "T92PDPS"
			.ProcChance = 0.5
			.ProcValue = 180
			.ProcLenght = 15
			.InternalCD = 45
			.ProcType = "str"
			.ProcOn = procs.ProcOnType.OnBloodStrike
			If s.MainStat.T92PDPS = 1 Then .Equip()
		End With

		With New Proc(s)
			._Name = "HauntedDreams"
			.ProcChance = 0.15
			.ProcValue = 173
			.ProcLenght = 10
			.InternalCD = 45
			.ProcType = "crit"
			.ProcOn = procs.ProcOnType.OnBloodStrike
			If s.Sigils.HauntedDreams Then .Equip()
		End With


		With New Proc(s)
			._Name = "OrcRacial"
			.InternalCD = 120
			.ProcOn = Procs.ProcOnType.OnDamage
			.ProcChance = 1
			.ProcLenght = 15
			.ProcValue = 322
			.ProcType = "ap"
			If s.Character.Orc Then .Equip()
		End With

		TrollRacial = New Proc(s)
		With TrollRacial
			._Name = "TrollRacial"
			.InternalCD = 180
			.ProcChance = 1
			.ProcLenght = 15
			.ProcValue = 0.2
			.ProcOn = Procs.ProcOnType.OnDamage
			If s.Character.Troll Then .Equip()
		End With

		With New WeaponProc(s)
			._Name = "BElfRacial"
			.InternalCD = 120
			.ProcChance = 1
			.ProcLenght = 0
			.ProcValue = 15
			.DamageType = "torrent"
			.ProcOn = Procs.ProcOnType.OnDamage
			If s.Character.BloodElf Then .Equip()
		End With

		With New WeaponProc(s)
			._Name = "BloodWorms"
			.InternalCD = 20
			.ProcChance = 3 * s.Character.TalentBlood.BloodWorms / 100
			.DamageType = "BloodWorms"
			.ProcOn = Procs.ProcOnType.OnHit
			If s.Character.TalentBlood.BloodWorms > 0 Then
				.Equip()
			End If
			.isGuardian = True
		End With
		dim Shadowmourne as New WeaponProc(s)
		With Shadowmourne
			.ProcOn = Procs.ProcOnType.OnMHhit
			.ProcChance  = 1
			.ProcValueStack = 30
			.ProcValue = 2000
			.ProcLenght = 60 ' Soul Fragment Duration
			.ProcTypeStack = "str"
			.DamageType = "Shadowmourne"
			.HasteSensible = True
			Try
				If XmlCharacter.SelectSingleNode("//character/WeaponProc/MHShadowmourne").InnerText = 1 Then
					._Name = "Shadowmourne"
					.Equip
					.InternalCD = 10 'Chaos Bane Duration
				End If
			Catch
			End Try
			Try
				If XmlCharacter.SelectSingleNode("//character/WeaponProc/MHShadowmourneCancelCB").InnerText = 1 Then
					._Name = "Shadowmourne (Cancel CB)"
					.Equip
					.InternalCD = 0.1 'Chaos Bane Duration
				End If
			Catch
			
			End Try
		End With
		
		dim Bryntroll as New WeaponProc(s)
		With Bryntroll
			.ProcOn = Procs.ProcOnType.OnMHhit
			.ProcChance  = 0.1133
			.DamageType = "Bryntroll"
			.ProcLenght = 0
			.HasteSensible = True
			Try
				If XmlCharacter.SelectSingleNode("//character/WeaponProc/MHBryntrollHeroic").InnerText = 1 Then
					._Name = "BryntrollHeroic"
					.ProcValue = 2538
					.Equip
				End If
			Catch
			End Try
			Try
				 If XmlCharacter.SelectSingleNode("//character/WeaponProc/MHBryntroll").InnerText = 1 Then
					._Name = "Bryntroll"
					.ProcValue = 2250
					.Equip
				End If
			Catch
			End Try
		End With
		With New Proc(s)
			._Name = "AshenBand"
			.ProcChance = 0.10
			.ProcLenght = 10
			.ProcValue = 480
			.InternalCD = 45
			.ProcType = "ap"
			.ProcOn = procs.ProcOnType.OnHit
			Try
				If XmlCharacter.SelectSingleNode("//character/misc/AshenBand").InnerText= True Then
					.Equip
				End If
			Catch ex As System.Exception
				
			End Try
		End With
			dim	MHtemperedViskag as New WeaponProc(s)
		With MHtemperedViskag
			.ProcChance = 0.04
			.ProcLenght = 0
			.ProcValue = 2222
			.InternalCD = 0
			.DamageType = "physical"
			._Name = "MHtemperedViskag"
			.ProcOn = procs.ProcOnType.OnMHhit
			.HasteSensible = True
			Try
				If sim.XmlCharacter.SelectSingleNode("//character/WeaponProc/MHtemperedViskag").InnerText= 1 Then .Equip
			catch
			End Try
		End With
		
		dim OHtemperedViskag as New WeaponProc(s)
		With OHtemperedViskag
			.ProcChance = 0.04
			.ProcLenght = 0
			.ProcValue = 2222
			.InternalCD = 0
			.DamageType = "physical"
			._Name = "OHtemperedViskag"
			.ProcOn = procs.ProcOnType.OnOHhit
			.HasteSensible = True
			Try
				if sim.XmlCharacter.SelectSingleNode("//character/WeaponProc/OHtemperedViskag").InnerText = 1 Then .Equip
			Catch
			End Try
		End With
		
		dim MHSingedViskag as New WeaponProc(s)
		With MHSingedViskag
			.ProcChance = 0.04
			.ProcLenght = 0
			.ProcValue = 2000
			.InternalCD = 0
			.DamageType = "physical"
			._Name = "MHSingedViskag"
			.ProcOn = procs.ProcOnType.OnMHhit
			.HasteSensible = True
			Try
				If sim.XmlCharacter.SelectSingleNode("//character/WeaponProc/MHSingedViskag").InnerText = 1 Then .Equip
			catch
			End Try
			
			
		End With
		
		dim OHSingedViskag as New WeaponProc(s)
		With OHSingedViskag
			.ProcChance = 0.04
			.ProcLenght = 0
			.ProcValue = 2000
			.InternalCD = 0
			.DamageType = "physical"
			._Name = "OHSingedViskag"
			.ProcOn = procs.ProcOnType.OnOHhit
			.HasteSensible = True
			Try
				If sim.XmlCharacter.SelectSingleNode("//character/WeaponProc/OHSingedViskag").InnerText = 1 Then .Equip
			catch
			End Try
		End With
		
		dim MHEmpoweredDeathbringer as New WeaponProc(S)
		With MHEmpoweredDeathbringer
			.ProcChance = 0.065
			.ProcLenght = 0
			.ProcValue = 1500
			.InternalCD = 0
			.DamageType = "shadow"
			._Name = "MH Empowered Deathbringer"
			.ProcOn = procs.ProcOnType.OnMHhit
			.HasteSensible = True
			Try
				if sim.XmlCharacter.SelectSingleNode("//character/WeaponProc/MHEmpoweredDeathbringer").InnerText = 1 then .Equip
			Catch ex As System.Exception
				
			End Try
		End With
		
		dim OHEmpoweredDeathbringer as New WeaponProc(S)
		With OHEmpoweredDeathbringer
			.ProcChance = 0.065
			.ProcLenght = 0
			.ProcValue = 1500
			.InternalCD = 0
			.DamageType = "shadow"
			._Name = "OH Deathbringer"
			.ProcOn = procs.ProcOnType.OnOHhit
			.HasteSensible = True
			Try
				if sim.XmlCharacter.SelectSingleNode("//character/WeaponProc/OHEmpoweredDeathbringer").InnerText = 1 then .Equip
			Catch ex As System.Exception
			End Try
		End With
		
		dim MHRagingDeathbringer as New WeaponProc(S)
		With MHRagingDeathbringer
			.ProcChance = 0.065
			.ProcLenght = 0
			.ProcValue = 1666
			.InternalCD = 0
			.DamageType = "shadow"
			._Name = "MH Raging Deathbringer"
			.ProcOn = procs.ProcOnType.OnMHhit
			.HasteSensible = True
			Try
				if sim.XmlCharacter.SelectSingleNode("//character/WeaponProc/MHRagingDeathbringer").InnerText = 1 then .Equip
			Catch
			End Try
			
		End With
		
		dim OHRagingDeathbringer as New WeaponProc(S)
		With OHRagingDeathbringer
			.ProcChance = 0.065
			.ProcLenght = 0
			.ProcValue = 1666
			.InternalCD = 0
			.DamageType = "shadow"
			._Name = "OH Raging Deathbringer"
			.ProcOn = procs.ProcOnType.OnOHhit
			.HasteSensible = True
			Try
				if sim.XmlCharacter.SelectSingleNode("//character/WeaponProc/OHRagingDeathbringer").InnerText = 1 then .Equip
			Catch ex As System.Exception
				
			End Try
			
			
		End With
		
		dim HandMountedPyroRocket as New WeaponProc(s)
		With HandMountedPyroRocket
			.ProcChance = 1
			.ProcLenght = 0
			.ProcValue = 1837
			.InternalCD = 45
			.DamageType = "arcane"
			._Name = "Hand Mounted Pyro Rocket"
			.ProcOn = procs.ProcOnType.Ondamage
			Try
				If XmlCharacter.SelectSingleNode("//character/misc/HandMountedPyroRocket").InnerText= True Then
					.Equip
				End If
			Catch ex As System.Exception
				
			End Try
		End With
		
		dim HyperspeedAccelerators as New WeaponProc(s)
		With HyperspeedAccelerators
			.ProcChance = 0.5
			.ProcLenght = 12
			.ProcValue = 340
			.InternalCD = 60
			.ProcType="haste"
			._Name = "Hyperspeed Accelerators"
			.ProcOn = procs.ProcOnType.Onhit
			Try
				If XmlCharacter.SelectSingleNode("//character/misc/HyperspeedAccelerators").InnerText= True Then
					.Equip
				End If
			Catch ex As System.Exception
			End Try
		End With
		
		
		
		
		
		dim TailorEnchant as New Proc(s)
		With TailorEnchant
			.ProcChance = 0.25
			.ProcLenght = 15
			.ProcValue = 400
			.InternalCD = 60
			.ProcType = "ap"
			._Name = "Swordguard Embroidery"
			.ProcOn = procs.ProcOnType.OnHit
			
			Try
				If XmlCharacter.SelectSingleNode("//character/misc/TailorEnchant").InnerText= True Then
					.Equip
				End If
			Catch ex As System.Exception
				
			End Try
		End With
		
		dim SaroniteBomb as New WeaponProc(s)
		With SaroniteBomb
			.ProcChance = 0.5
			.ProcLenght = 0
			.ProcValue = 1325
			.DamageType = "SaroniteBomb"
			.InternalCD = 60
			._Name = "Saronite Bomb"
			.ProcOn = procs.ProcOnType.OnMHhit
			.HasteSensible = False
			Try
				if sim.XmlCharacter.SelectSingleNode("//character/misc/SaroniteBomb").InnerText then .Equip
			Catch ex As System.Exception
			End Try
		End With
		
		dim SapperCharge as new WeaponProc(s)
		With SapperCharge
			.ProcChance = 0.5
			.ProcLenght = 0
			.ProcValue = 2500
			.DamageType = "SapperCharge"
			.InternalCD = 300
			._Name = "Global Thermal Sapper Charge"
			.ProcOn = procs.ProcOnType.OnMHhit
			.HasteSensible = False
			Try
				if sim.XmlCharacter.SelectSingleNode("//character/misc/SapperCharge").InnerText then .Equip
			Catch ex As System.Exception
			End Try
		End With
		
		Dim IndestructiblePotion As New Proc(s)
		With IndestructiblePotion
			.ProcChance = 0.5
			.ProcLenght = 120
			.ProcValue = 2500
			.ProcType = "armor"
			.InternalCD = 6000 '10 minutes
			._Name = "Indestructible Potion"
			.ProcOn = procs.ProcOnType.OnMHhit
			.HasteSensible = False
			Try
				if XmlCharacter.SelectSingleNode("//character/misc/IndestructiblePotion").InnerText then .Equip
			Catch ex As System.Exception
			End Try
		End With
		
		Dim PotionofSpeed As New Proc(s)
		With PotionofSpeed
			.ProcChance = 0.5
			.ProcLenght = 15
			.ProcValue = 500
			.ProcType = "haste"
			.InternalCD = 6000 '10 minutes
			._Name = "Potion of Speed"
			.ProcOn = procs.ProcOnType.OnMHhit
			.HasteSensible = False
			Try
				If sim.XmlCharacter.SelectSingleNode("//character/misc/PotionofSpeed").InnerText Then
					.Equip
				End If
			Catch ex As System.Exception
			End Try
		End With
		
	End Sub
	
	Function GetActiveBonus(stat As String) As Integer
		Dim prc As proc
		dim tmp as Integer
		For Each prc In EquipedProc
			If prc.ProcType = stat Then
				If prc.IsActive Then
					tmp += prc.ProcValue
				End If
			End If
			If prc.ProcTypeStack = stat Then
				if prc.IsActive Then
					tmp += prc.ProcValueStack * prc.Stack
				Else
					prc.Stack = 0
					'I don't think this ever arises in practice
					'but when such buffs fade they should set the stack back to 0
				End if
			End If
		Next
		return tmp
	End Function
	
	Function GetMaxPossibleBonus(stat As String) As Integer
		Dim prc As proc
		dim tmp as Integer
		For Each prc In EquipedProc
			If prc.ProcType = stat Then
				tmp += prc.ProcValue
			End If
		Next
		return tmp
	End Function
	
	
	
	
	Sub tryT104PDPS(T As Long)
		If sim.MainStat.T104PDPS = 0 Then Exit Sub
		
		If sim.Cataclysm Then
			if sim.Runes.BloodRunes.Available Then Exit Sub
			if sim.Runes.FrostRunes.Available Then Exit Sub
			if sim.Runes.UnholyRunes.Available Then Exit Sub
		Else
			If sim.Runes.BloodRune1.AvailableTime < T Then Exit Sub
			If sim.Runes.BloodRune2.AvailableTime < T Then Exit Sub
			If sim.Runes.FrostRune1.AvailableTime < T Then Exit Sub
			If sim.Runes.FrostRune2.AvailableTime < T Then Exit Sub
			If sim.Runes.UnholyRune1.AvailableTime < T Then Exit Sub
			If sim.Runes.UnholyRune2.AvailableTime < T Then Exit Sub
		End If
		T104PDPS.ApplyMe(T)
	End Sub
end Class
