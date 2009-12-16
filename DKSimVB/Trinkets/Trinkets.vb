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

	Protected sim as Sim
	Sub New(S As Sim)
		Sim = S
		
		DeathbringersWill = New Trinket(s)
		With DeathbringersWill 
			.ProcChance = 0.35
		 	.Equiped = 0
		 	.ProcLenght = 30
		 	.ProcValue = 600
		 	.InternalCD = 105
		 	.DamageType = "DeathbringersWill"
		 	.ProcType = "str"
		End With
		
		DeathbringersWillHeroic = New Trinket(s)
		With DeathbringersWillHeroic 
			.ProcChance = 0.35
		 	.Equiped = 0
		 	.ProcLenght = 30
		 	.ProcValue = 700
		 	.InternalCD = 105
		 	.DamageType = "DeathbringersWillHeroic"
		 	.ProcType = "str"
		End With
		
		DeathChoice = New Trinket(s)
		With DeathChoice 
			.ProcChance = 0.35
		 	.Equiped = 0
		 	.ProcLenght = 15
		 	.ProcValue = 450
		 	.InternalCD = 45
		End With
		
		DeathChoiceHeroic = New Trinket(s)
		With DeathChoiceHeroic 
			.ProcChance = 0.35
		 	.Equiped = 0
		 	.ProcLenght = 15
		 	.ProcValue = 510
		 	.InternalCD = 45
		End With

		Greatness = New Trinket(s)
		With Greatness
			.ProcChance = 0.35
			.Equiped = 0
			.ProcLenght = 15
			.ProcValue = 300
			.InternalCD = 45			
		End With
		
		MjolRune = New Trinket(s)
		With MjolRune
			.ProcChance = 0.15
	 		.Equiped = 0
	 		.ProcLenght = 10
	 		.ProcValue = 665
	 		.InternalCD = 45
		End With

		GrimToll = New Trinket(s)
		With GrimToll 
			.ProcChance = 0.15
		 	.Equiped = 0
		 	.ProcLenght = 10
		 	.ProcValue = 612
		 	.InternalCD = 45
		End With

	 	BitterAnguish = New Trinket(s)
	 	With BitterAnguish 
			.ProcChance = 0.10
		 	.Equiped = 0
		 	.ProcLenght = 10
		 	.ProcValue = 410
		 	.InternalCD = 45
		End With
		
		Mirror = New Trinket(s)
		With Mirror 
			.ProcChance = 0.10
		 	.Equiped = 0
		 	.ProcLenght = 10
		 	.ProcValue = 1000
		 	.InternalCD = 45
		End With
		
	 	Pyrite = New Trinket(s)
		With Pyrite 
			.ProcChance = 0.10
		 	.Equiped = 0
		 	.ProcLenght = 10
		 	.ProcValue = 1234
		 	.InternalCD = 45
		End With
		
		OldGod = New Trinket(s)
		With OldGod 
			.ProcChance = 0.10
		 	.Equiped = 0
		 	.ProcLenght = 10
		 	.ProcValue = 1284
		 	.InternalCD = 45
		End With
		
		Victory = New Trinket(s)
		With Victory 
			.ProcChance = 0.20
		 	.Equiped = 0
		 	.ProcLenght = 10
		 	.ProcValue = 1008
		 	.InternalCD = 45
		End With
		
		DarkMatter = New Trinket(s)
		With DarkMatter 
			.ProcChance = 0.15
		 	.Equiped = 0
		 	.ProcLenght = 10
		 	.ProcValue = 612
		 	.InternalCD = 45
		End With
		
		Comet = New Trinket(s)
		With Comet 
			.ProcChance = 0.15
		 	.Equiped = 0
		 	.ProcLenght = 10
		 	.ProcValue = 726
		 	.InternalCD = 45
		End With
		
		DCDeath = New Trinket(s)
		With DCDeath 
			.ProcChance = 0.15
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 2000
		 	.InternalCD = 45
		 	.DamageType = "shadow"
		 	.Name = "DCDeath"
		End With
		
		Necromantic = New Trinket(s)
		With Necromantic 
			.ProcChance = 0.10
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 1050
		 	.InternalCD = 15
		 	.DamageType = "shadow"
		 	.Name = "Necromantic"
		End With
		
		Bandit = New Trinket(s)
		With Bandit 
			.ProcChance = 0.10
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 1880
		 	.InternalCD = 45
		 	.DamageType = "arcane"
		 	.Name = "Bandit"
		End With
		
		MHtemperedViskag = New Trinket(s)
		With MHtemperedViskag
			.ProcChance = 0.04
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 2222
		 	.InternalCD = 0
		 	.DamageType = "physical"
		 	.Name = "MHtemperedViskag"
		End With
		
		OHtemperedViskag = New Trinket(s)
		With OHtemperedViskag
			.ProcChance = 0.04
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 2222
		 	.InternalCD = 0
		 	.DamageType = "physical"
		 	.Name = "OHtemperedViskag"
		End With
		
		MHSingedViskag = New Trinket(s)
		With MHSingedViskag
			.ProcChance = 0.04
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 2000
		 	.InternalCD = 0
		 	.DamageType = "physical"
		 	.Name = "MHSingedViskag"
		End With
		
		OHSingedViskag = New Trinket(s)
		With OHSingedViskag
			.ProcChance = 0.04
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 2000
		 	.InternalCD = 0
		 	.DamageType = "physical"
		 	.Name = "OHSingedViskag"
		End With
		
		MHEmpoweredDeathbringer = New Trinket(S)
		With MHEmpoweredDeathbringer
			.ProcChance = 0.065
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 1500
		 	.InternalCD = 0
		 	.DamageType = "shadow"
		 	.Name = "MH Empowered Deathbringer"
		End With
		
		OHEmpoweredDeathbringer = New Trinket(S)
		With OHEmpoweredDeathbringer
			.ProcChance = 0.065
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 1500
		 	.InternalCD = 0
		 	.DamageType = "shadow"
		 	.Name = "OH Empowered Deathbringer"
		End With
		
		MHRagingDeathbringer = New Trinket(S)
		With MHRagingDeathbringer
			.ProcChance = 0.065
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 1666
		 	.InternalCD = 0
		 	.DamageType = "shadow"
		 	.Name = "MH Raging Deathbringer"
		End With
		
		OHRagingDeathbringer = New Trinket(S)
		With OHRagingDeathbringer
			.ProcChance = 0.065
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 1666
		 	.InternalCD = 0
		 	.DamageType = "shadow"
		 	.Name = "OH Raging Deathbringer"
		End With
		
		HandMountedPyroRocket = New Trinket(s)
		With HandMountedPyroRocket
			.ProcChance = 1
		 	.Equiped = 0
		 	.ProcLenght = 0
		 	.ProcValue = 1837
		 	.InternalCD = 45
		 	.DamageType = "arcane"
		 	.Name = "Hand Mounted Pyro Rocket"
		End With
		
		TailorEnchant = New Trinket(s)
		With TailorEnchant
			.ProcChance = 0.25
		 	.Equiped = 0
		 	.ProcLenght = 15
		 	.ProcValue = 400
		 	.InternalCD = 60
		 	.Name = "Swordguard Embroidery"
		End With
		
		MHRazorIce = New Trinket(s)
		With MHRazorIce
			.ProcChance = 1
		 	.Equiped=0
		 	.ProcLenght = 15
		 	.ProcValue = 0
		 	.DamageType = "razorice"
		 	.InternalCD = 0
		 	.Name = "Main Hand RazorIce"
		End With
		
		OHRazorIce = New Trinket(s)
		With OHRazorIce
			.ProcChance = 1
		 	.Equiped=0
		 	.ProcLenght = 15
		 	.ProcValue = 0
		 	.DamageType = "razorice"
		 	.InternalCD = 0
		 	.Name = "Off Hand RazorIce"
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
