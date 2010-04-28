'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/08/2009
' Heure: 21:30
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Trinkets
	
	Protected XmlCharacter as Xml.XmlDocument
	
	
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
	Friend HerkumlWarToken as Trinket
	Friend MarkofSupremacy As Trinket
	Friend VengeanceoftheForsaken As Trinket
	Friend VengeanceoftheForsakenHeroic As Trinket
	Friend WhisperingFangedSkull As Trinket
	Friend WhisperingFangedSkullHeroic As Trinket
	Friend NeedleEncrustedScorpion as Trinket
	Friend TinyAbomination as Trinket
	
	
	
	
	
	
	Protected sim as Sim
	Sub New(S As Sim)
		Sim = S
		XmlCharacter = S.XmlCharacter
		

		
		HerkumlWarToken = New Trinket(s)
		With HerkumlWarToken
			._Name = "HerkumlWarToken"
			.ProcOn = Procs.ProcOnType.Onhit
			.ProcChance  = 1
			.InternalCD = 0
			.MaxStack = 20
			.ProcTypeStack = "ap"
			.ProcValueStack = 17
			.ProcLenght = 10
		End With
		
		MarkofSupremacy = New Trinket(s)
		With MarkofSupremacy
			._Name = "MarkofSupremacy"
			.ProcOn = Procs.ProcOnType.Onhit
			.ProcChance  = 0.5
			.InternalCD = 120
			.ProcLenght = "20"
			.ProcType = "ap"
			.ProcValue = "1024"
		End With
		
		VengeanceoftheForsaken = New Trinket(s)
		With VengeanceoftheForsaken
			._Name = "VengeanceoftheForsaken"
			.ProcOn = Procs.ProcOnType.Onhit
			.ProcChance  = 0.5
			.InternalCD = 120
			.ProcLenght = "20"
			.ProcType = "ap"
			.ProcValue = "860"
		End With
		
		VengeanceoftheForsakenHeroic = New Trinket(s)
		With VengeanceoftheForsakenHeroic
			._Name = "VengeanceoftheForsakenHeroic"
			.ProcOn = Procs.ProcOnType.Onhit
			.ProcChance  = 0.5
			.InternalCD = 120
			.ProcLenght = "20"
			.ProcType = "ap"
			.ProcValue = "1000"
		End With
		
		TinyAbomination = New Trinket(s)
		With TinyAbomination
			._Name = "TinyAbomination"
			.ProcOn = Procs.ProcOnType.OnHit
			.ProcChance  = 0.5
			.ProcValue = 0
			.ProcType = ""
			.DamageType = "TinyAbomination"
			.HasteSensible = true
		End With
		
		
		DeathbringersWill = New Trinket(s)
		With DeathbringersWill
			._Name = "DeathbringersWill"
			.ProcChance = 0.35
			.ProcLenght = 30
			.ProcValue = 600
			.InternalCD = 105
			.DamageType = "DeathbringersWill"
			.ProcType = "haste"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		DeathbringersWillHeroic = New Trinket(s)
		With DeathbringersWillHeroic
			._Name = "DeathbringersWillHeroic"
			.ProcChance = 0.35
			.ProcLenght = 30
			.ProcValue = 700
			.InternalCD = 105
			.DamageType = "DeathbringersWillHeroic"
			.ProcType = "haste"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		
		WhisperingFangedSkull = New Trinket(s)
		With WhisperingFangedSkull
			._Name = "WhisperingFangedSkull"
			.ProcChance = 0.35
			.ProcLenght = 15
			.ProcValue = 1100
			.InternalCD = 45
			.DamageType = ""
			.ProcType = "ap"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		WhisperingFangedSkullHeroic = New Trinket(s)
		With WhisperingFangedSkullHeroic
			._Name = "WhisperingFangedSkullHeroic"
			.ProcChance = 0.35
			.ProcLenght = 15
			.ProcValue = 1250
			.InternalCD = 45
			.DamageType = ""
			.ProcType = "ap"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		NeedleEncrustedScorpion = New Trinket(s)
		With NeedleEncrustedScorpion
			._Name = "NeedleEncrustedScorpion"
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
			._Name = "DeathChoice"
			.ProcChance = 0.35
			.ProcLenght = 15
			.ProcValue = 450
			.InternalCD = 45
			.ProcType = "str"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		DeathChoiceHeroic = New Trinket(s)
		With DeathChoiceHeroic
			._Name = "DeathChoiceHeroic"
			.ProcChance = 0.35
			.ProcLenght = 15
			.ProcValue = 510
			.InternalCD = 45
			.ProcType = "str"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		Greatness = New Trinket(s)
		With Greatness
			._Name = "Greatness"
			.ProcChance = 0.35
			.ProcLenght = 15
			.ProcValue = 300
			.InternalCD = 45
			.ProcType = "str"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		MjolRune = New Trinket(s)
		With MjolRune
			._Name = "MjolRune"
			.ProcChance = 0.15
			.ProcLenght = 10
			.ProcValue = 665
			.InternalCD = 45
			.ProcType = "arp"
			.ProcOn = procs.ProcOnType.OnHit
		End With
		
		GrimToll = New Trinket(s)
		With GrimToll
			._Name = "GrimToll"
			.ProcChance = 0.15
			.ProcLenght = 10
			.ProcValue = 612
			.InternalCD = 45
			.ProcType = "arp"
			.ProcOn = procs.ProcOnType.OnHit
		End With
		
		BitterAnguish = New Trinket(s)
		With BitterAnguish
			._Name = "BitterAnguish"
			.ProcChance = 0.10
			.ProcLenght = 10
			.ProcValue = 410
			.InternalCD = 45
			.ProcType = "haste"
			.ProcOn = procs.ProcOnType.OnCrit
		End With
		
		Mirror = New Trinket(s)
		With Mirror
			._name = "Mirror"
			.ProcChance = 0.10
			.ProcLenght = 10
			.ProcValue = 1000
			.InternalCD = 45
			.ProcType = "ap"
			.ProcOn = procs.ProcOnType.OnCrit
		End With
		
		Pyrite = New Trinket(s)
		With Pyrite
			._Name = "Pyrite"
			.ProcChance = 0.10
			.ProcLenght = 10
			.ProcValue = 1234
			.InternalCD = 45
			.ProcType = "ap"
			.ProcOn = procs.ProcOnType.OnCrit
		End With
		
		OldGod = New Trinket(s)
		With OldGod
			._Name = "OldGod"
			.ProcChance = 0.10
			.ProcLenght = 10
			.ProcValue = 1284
			.InternalCD = 45
			.ProcType = "ap"
			.ProcOn = procs.ProcOnType.OnCrit
		End With
		
		Victory = New Trinket(s)
		With Victory
			._Name = "Victory"
			.ProcChance = 0.20
			.ProcLenght = 10
			.ProcValue = 1008
			.InternalCD = 45
			.ProcType = "ap"
			.ProcOn = procs.ProcOnType.OnHit
		End With
		
		DarkMatter = New Trinket(s)
		With DarkMatter
			._Name = "DarkMatter"
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
			._Name = "Comet"
			.ProcOn = procs.ProcOnType.Onhit
		End With
		
		DCDeath = New Trinket(s)
		With DCDeath
			.ProcChance = 0.15
			.ProcLenght = 0
			.ProcValue = 2000
			.InternalCD = 45
			.DamageType = "shadow"
			._Name = "DCDeath"
			.ProcOn = procs.ProcOnType.OnDamage
		End With
		
		Necromantic = New Trinket(s)
		With Necromantic
			.ProcChance = 0.10
			.ProcLenght = 0
			.ProcValue = 1050
			.InternalCD = 15
			.DamageType = "shadow"
			._Name = "Necromantic"
			.ProcOn = procs.ProcOnType.OnDoT
		End With
		
		Bandit = New Trinket(s)
		With Bandit
			.ProcChance = 0.10
			.ProcLenght = 0
			.ProcValue = 1880
			.InternalCD = 45
			.DamageType = "arcane"
			._Name = "Bandit"
			.ProcOn = procs.ProcOnType.OnHit
		End With
		
		
	
		
		
		
		
		'CollectDamagingTrinket
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
