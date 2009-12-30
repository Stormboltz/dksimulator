'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/08/2009
' Heure: 21:30
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Trinkets
	Friend GrimToll As Trinket
	Friend BitterAnguish As Trinket
	Friend Mirror As Trinket
	Friend DCDeath As Trinket
	Friend Victory As Trinket
	Friend Necromantic As Trinket
	Friend Bandit As Trinket
	Friend Pyrite As Trinket
	Friend DarkMatter As Trinket
	Friend OldGod As Trinket
	Friend Comet As Trinket
	Friend DeathChoice As Trinket
	Friend DeathChoiceHeroic As Trinket
	Friend Greatness as Trinket
	Friend MjolRune As Trinket
	Friend DeathbringersWill As Trinket
	Friend DeathbringersWillHeroic As Trinket
	
	Friend MHtemperedViskag As Trinket
	Friend OHtemperedViskag As Trinket
	Friend MHSingedViskag As Trinket
	Friend OHSingedViskag As Trinket
	
	Friend MHEmpoweredDeathbringer As Trinket
	Friend OHEmpoweredDeathbringer As Trinket
	Friend MHRagingDeathbringer As Trinket
	Friend OHRagingDeathbringer As Trinket
	
	Friend HandMountedPyroRocket As Trinket
	Friend TailorEnchant As Trinket
	Friend MHRazorIce As Trinket
	Friend OHRazorIce As Trinket
	
	Friend AshenBand  as Trinket
	
	
	Friend WhisperingFangedSkull As Trinket
	Friend NeedleEncrustedScorpion as Trinket
	
	Friend Bryntroll as New Trinket

	Protected sim as Sim
	Sub New(S As Sim)
		Sim = S
		
		
		AshenBand= New Trinket(s)
		
		With AshenBand
			.Name = "AshenBand"
			.ProcChance = 0.10
		 	.ProcLenght = 10
		 	.ProcValue = 480
		 	.InternalCD = 45
		 	.DamageType = ""
		 	.ProcType = "ap"
		 	.ProcOn = procs.ProcOnType.OnHit
		End With
		
		DeathbringersWill = New Trinket(s)
		With DeathbringersWill 
			.Name = "DeathbringersWill"
			.ProcChance = 0.35
		 	.ProcLenght = 30
		 	.ProcValue = 600
		 	.InternalCD = 105
		 	.DamageType = "DeathbringersWill"
		 	.ProcType = "str"
		 	.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		DeathbringersWillHeroic = New Trinket(s)
		With DeathbringersWillHeroic
			.Name = "DeathbringersWillHeroic"
			.ProcChance = 0.35
		 	.ProcLenght = 30
		 	.ProcValue = 700
		 	.InternalCD = 105
		 	.DamageType = "DeathbringersWillHeroic"
		 	.ProcType = "str"
		 	.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		
		WhisperingFangedSkull = New Trinket(s)
		With WhisperingFangedSkull
			.Name = "WhisperingFangedSkull"
			.ProcChance = 0.35
		 	.ProcLenght = 15
		 	.ProcValue = 1100
		 	.InternalCD = 45
		 	.DamageType = ""
		 	.ProcType = "ap"
		 	.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		NeedleEncrustedScorpion = New Trinket(s)
		With NeedleEncrustedScorpion
			.Name = "NeedleEncrustedScorpion"
			.ProcChance = 0.10
		 	.ProcLenght = 10
		 	.ProcValue = 678
		 	.InternalCD = 45
		 	.DamageType = ""
		 	.ProcType = "arp"
		 	.ProcOn = procs.ProcOnType.OnCrit
		End With
		
		
		DeathChoice = New Trinket(s)
		With DeathChoice 
			.Name = "DeathChoice"
			.ProcChance = 0.35
		 	.ProcLenght = 15
		 	.ProcValue = 450
		 	.InternalCD = 45
		 	.ProcType = "str"
		 	.ProcOn = procs.ProcOnType.OnDamage
		 End With
		
		DeathChoiceHeroic = New Trinket(s)
		With DeathChoiceHeroic 
			.Name = "DeathChoiceHeroic"
			.ProcChance = 0.35
		 	.ProcLenght = 15
		 	.ProcValue = 510
		 	.InternalCD = 45
		 	.ProcType = "str"
		 	.ProcOn = procs.ProcOnType.OnDamage
		End With

		Greatness = New Trinket(s)
		With Greatness
			.Name = "Greatness"
			.ProcChance = 0.35
			.ProcLenght = 15
			.ProcValue = 300
			.InternalCD = 45
			.ProcType = "str"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		MjolRune = New Trinket(s)
		With MjolRune
			.Name = "MjolRune"
			.ProcChance = 0.15
	 		.ProcLenght = 10
	 		.ProcValue = 665
	 		.InternalCD = 45
	 		.ProcType = "arp"
	 		.ProcOn = procs.ProcOnType.OnHit
		End With

		GrimToll = New Trinket(s)
		With GrimToll 
			.Name = "GrimToll"
			.ProcChance = 0.15
		 	.ProcLenght = 10
		 	.ProcValue = 612
		 	.InternalCD = 45
		 	.ProcType = "arp"
		 	.ProcOn = procs.ProcOnType.OnHit
		End With

	 	BitterAnguish = New Trinket(s)
	 	With BitterAnguish
	 		.Name = "BitterAnguish"
			.ProcChance = 0.10
		 	.ProcLenght = 10
		 	.ProcValue = 410
		 	.InternalCD = 45
		 	.ProcType = "haste"
		 	.ProcOn = procs.ProcOnType.OnCrit
		End With
		
		Mirror = New Trinket(s)
		With Mirror 
			.name = "Mirror"
			.ProcChance = 0.10
		 	.ProcLenght = 10
		 	.ProcValue = 1000
		 	.InternalCD = 45
		 	.ProcType = "ap"
		 	.ProcOn = procs.ProcOnType.OnCrit
		End With
		
	 	Pyrite = New Trinket(s)
		With Pyrite 
			.Name = "Pyrite"
			.ProcChance = 0.10
		 	.ProcLenght = 10
		 	.ProcValue = 1234
		 	.InternalCD = 45
		 	.ProcType = "ap"
		 	.ProcOn = procs.ProcOnType.OnCrit
		End With
		
		OldGod = New Trinket(s)
		With OldGod 
			.Name = "OldGod"
			.ProcChance = 0.10
		 	.ProcLenght = 10
		 	.ProcValue = 1284
		 	.InternalCD = 45
		 	.ProcType = "ap"
		 	.ProcOn = procs.ProcOnType.OnCrit
		End With
		
		Victory = New Trinket(s)
		With Victory 
			.Name = "Victory"
			.ProcChance = 0.20
		 	.ProcLenght = 10
		 	.ProcValue = 1008
		 	.InternalCD = 45
		 	.ProcType = "ap"
		 	.ProcOn = procs.ProcOnType.OnHit
		End With
		
		DarkMatter = New Trinket(s)
		With DarkMatter 
			.Name = "DarkMatter"
			.ProcChance = 0.15
		 	.ProcLenght = 10
		 	.ProcValue = 612
		 	.InternalCD = 45
		 	.ProcType = "crit"
		 	.ProcOn = procs.ProcOnType.OnHit
		End With
		
		Comet = New Trinket(s)
		With Comet 
			.ProcChance = 0.15
		 	.ProcLenght = 10
		 	.ProcValue = 726
		 	.InternalCD = 45
		 	.ProcType = "haste"
		 	.Name = "Comet"
		 	.ProcOn = procs.ProcOnType.Onhit
		End With
		
		DCDeath = New Trinket(s)
		With DCDeath 
			.ProcChance = 0.15
		 	.ProcLenght = 0
		 	.ProcValue = 2000
		 	.InternalCD = 45
		 	.DamageType = "shadow"
		 	.Name = "DCDeath"
		 	.ProcOn = procs.ProcOnType.OnDamage		 	
		End With
		
		Necromantic = New Trinket(s)
		With Necromantic 
			.ProcChance = 0.10
		 	.ProcLenght = 0
		 	.ProcValue = 1050
		 	.InternalCD = 15
		 	.DamageType = "shadow"
		 	.Name = "Necromantic"
		 	.ProcOn = procs.ProcOnType.OnDoT
		End With
		
		Bandit = New Trinket(s)
		With Bandit 
			.ProcChance = 0.10
		 	.ProcLenght = 0
		 	.ProcValue = 1880
		 	.InternalCD = 45
		 	.DamageType = "arcane"
		 	.Name = "Bandit"
		 	.ProcOn = procs.ProcOnType.OnHit
		End With
		
		MHtemperedViskag = New Trinket(s)
		With MHtemperedViskag
			.ProcChance = 0.04
		 	.ProcLenght = 0
		 	.ProcValue = 2222
		 	.InternalCD = 0
		 	.DamageType = "physical"
		 	.Name = "MHtemperedViskag"
		 	.ProcOn = procs.ProcOnType.OnMHhit
		End With
		
		OHtemperedViskag = New Trinket(s)
		With OHtemperedViskag
			.ProcChance = 0.04
		 	.ProcLenght = 0
		 	.ProcValue = 2222
		 	.InternalCD = 0
		 	.DamageType = "physical"
		 	.Name = "OHtemperedViskag"
		 	.ProcOn = procs.ProcOnType.OnOHhit
		End With
		
		MHSingedViskag = New Trinket(s)
		With MHSingedViskag
			.ProcChance = 0.04
		 	.ProcLenght = 0
		 	.ProcValue = 2000
		 	.InternalCD = 0
		 	.DamageType = "physical"
		 	.Name = "MHSingedViskag"
		 	.ProcOn = procs.ProcOnType.OnMHhit
		End With
		
		OHSingedViskag = New Trinket(s)
		With OHSingedViskag
			.ProcChance = 0.04
		 	.ProcLenght = 0
		 	.ProcValue = 2000
		 	.InternalCD = 0
		 	.DamageType = "physical"
		 	.Name = "OHSingedViskag"
		 	.ProcOn = procs.ProcOnType.OnOHhit
		End With
		
		MHEmpoweredDeathbringer = New Trinket(S)
		With MHEmpoweredDeathbringer
			.ProcChance = 0.065
		 	.ProcLenght = 0
		 	.ProcValue = 1500
		 	.InternalCD = 0
		 	.DamageType = "shadow"
		 	.Name = "MH Empowered Deathbringer"
		 	.ProcOn = procs.ProcOnType.OnMHhit
		End With
		
		OHEmpoweredDeathbringer = New Trinket(S)
		With OHEmpoweredDeathbringer
			.ProcChance = 0.065
		 	.ProcLenght = 0
		 	.ProcValue = 1500
		 	.InternalCD = 0
		 	.DamageType = "shadow"
		 	.Name = "OH Empowered Deathbringer"
		 	.ProcOn = procs.ProcOnType.OnOHhit
		End With
		
		MHRagingDeathbringer = New Trinket(S)
		With MHRagingDeathbringer
			.ProcChance = 0.065
		 	.ProcLenght = 0
		 	.ProcValue = 1666
		 	.InternalCD = 0
		 	.DamageType = "shadow"
		 	.Name = "MH Raging Deathbringer"
		 	.ProcOn = procs.ProcOnType.OnMHhit
		End With
		
		OHRagingDeathbringer = New Trinket(S)
		With OHRagingDeathbringer
			.ProcChance = 0.065
		 	.ProcLenght = 0
		 	.ProcValue = 1666
		 	.InternalCD = 0
		 	.DamageType = "shadow"
		 	.Name = "OH Raging Deathbringer"
		 	.ProcOn = procs.ProcOnType.OnOHhit
		End With
		
		HandMountedPyroRocket = New Trinket(s)
		With HandMountedPyroRocket
			.ProcChance = 1
		 	.ProcLenght = 0
		 	.ProcValue = 1837
		 	.InternalCD = 45
		 	.DamageType = "arcane"
		 	.Name = "Hand Mounted Pyro Rocket"
		 	.ProcOn = procs.ProcOnType.Ondamage
		End With
		
		TailorEnchant = New Trinket(s)
		With TailorEnchant
			.ProcChance = 0.25
		 	.ProcLenght = 15
		 	.ProcValue = 400
		 	.InternalCD = 60
		 	.ProcType = "ap"
		 	.Name = "Swordguard Embroidery"
		 	.ProcOn = procs.ProcOnType.OnHit
		End With
		
		MHRazorIce = New Trinket(s)
		With MHRazorIce
			.ProcChance = 1
		 	.ProcLenght = 15
		 	.ProcValue = 0
		 	.DamageType = "razorice"
		 	.InternalCD = 0
		 	.Name = "Main Hand RazorIce"
		 	.ProcOn = procs.ProcOnType.OnMHhit
		End With
		
		OHRazorIce = New Trinket(s)
		With OHRazorIce
			.ProcChance = 1
		 	.ProcLenght = 15
		 	.ProcValue = 0
		 	.DamageType = "razorice"
		 	.InternalCD = 0
		 	.Name = "Off Hand RazorIce"
		 	.ProcOn = procs.ProcOnType.OnOHhit
		End With
		
		Bryntroll = New Trinket(s)
		With Bryntroll
			.Name = "Bryntroll"
			.ProcOn = Procs.ProcOnType.OnDamage
			.ProcChance  = 0.1133
			.ProcValue = 2250
			.DamageType = "Bryntroll"
			.ProcLenght = 0
		End With
		
		CollectDamagingTrinket
	End Sub
	
	Sub CollectDamagingTrinket()
		Dim tk As Trinket
		For Each tk In sim.TrinketsCollection
			If tk.DamageType <> "" Then
				sim.DamagingObject.Add(tk)
			End If
		Next
	End Sub
	
	Sub SoftReset()
		Dim tk As Trinket
		
		For Each tk In sim.TrinketsCollection
			tk.CD = 0
		Next
	End Sub
	
	
End Class
