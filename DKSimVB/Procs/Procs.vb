'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 00:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Procs
	Friend KillingMachine As Proc
	Friend Rime As Proc
	Friend ScentOfBlood As ScentOfBlood
	Friend Virulence As Proc
	Friend Strife As Proc
	Friend T92PDPS as Proc
	Friend HauntedDreams As Proc
	Friend T104PDPS As proc
	Friend MHFallenCrusader As Proc
	Friend OHFallenCrusader As Proc
	Friend Berserking As Proc
	Friend OrcRacial As Proc
	Friend TrollRacial As Proc
	Friend BElfRacial As Trinket

	Protected Sim as Sim
	Friend T104PDPSFAde As Integer
	
	Friend AllProcs As New Collection
	Friend EquipedTrinkets as New Collection
	Friend OnHitProcs As new Collection
	Friend OnMHhitProcs As new Collection
	Friend OnOHhitProcs As new Collection
	Friend OnCritProcs As new Collection
	Friend OnDamageProcs As new Collection
	Friend OnDoTProcs As new Collection

	Public Enum ProcOnType
		OnMisc = 0
		OnHit = 1
		OnMHhit = 2
		OnOHhit = 3
		OnCrit = 4
		OnDamage = 5
		OnDoT = 6
	End Enum
		

	

	
	
	Sub New(S As Sim)
		T104PDPSFAde= 0
		sim = S
	End Sub
	Sub SoftReset
		Dim prc As proc
		For Each prc In AllProcs
			prc.CD = 0
			prc.Fade = 0			
		Next
	End Sub
	
	Sub Init()
		Dim s As Sim
		s= me.Sim
		KillingMachine = New Proc(s)
		With KillingMachine
			.Name = "KillingMachine"
			.ProcOn = procs.ProcOnType.OnMHhit
			if Talentfrost.KillingMachine > 0 then .Equip
			.Equiped  = Talentfrost.KillingMachine
			.ProcLenght = 60
			.ProcChance = (Talentfrost.KillingMachine)*S.MainStat.MHWeaponSpeed/60
		End With
		
		Rime = New Proc(s)
		With Rime
			.Name = "Rime"
			if talentfrost.Rime >  0 then .equip 
			.Equiped  = talentfrost.Rime
			.ProcLenght = 60
			.ProcChance = 5 * talentfrost.Rime/100
		End With
		
		ScentOfBlood = New ScentOfBlood(s)
		With ScentOfBlood
			.Name = "ScentOfBlood"
			If s.MainStat.FrostPresence = 1 Then
				.equip
				.Equiped  = TalentBlood.ScentOfBlood
			Else
				.Equiped = 0
			End If
			.ProcLenght = 60
			.ProcChance = 0.15
		End With
		
		Virulence = New Proc(s)
		With Virulence
			.Name = "Virulence"
			if s.Sigils.Virulence then .equip
			.ProcLenght = 20
			.ProcChance = 0.85
			.ProcValue = 200
			.ProcType = "str"
		End With
		
		Strife = New Proc(s)
		With Strife
			.Name = "Strife"
			.ProcChance = 1
			.ProcValue = 144
			.ProcLenght = 10
			.ProcType = "ap"
			if s.Sigils.strife then .equip
		End With
		
		T92PDPS = New Proc(s)
		With T92PDPS
			.Name = "T92PDPS"
			.ProcChance = .50
			.ProcValue = 180
			.ProcLenght = 15
			.InternalCD = 45
			.ProcType ="str"
			if s.MainStat.T92PDPS = 1 then .equip
		End With

		HauntedDreams = New Proc(s)
		With HauntedDreams
			.Name = "HauntedDreams"
			.ProcChance = 0.15
			.ProcValue = 173
			.ProcLenght = 10
			.InternalCD  = 45
			.ProcType = "crit"
			'.ProcOn = procs.ProcOnType.OnMHhit
			If s.Sigils.HauntedDreams	Then .Equip
		End With
		s.RuneForge.MHRazorIce = New RazorIce(S)
		With s.RuneForge.MHRazorIce
			.Name = "MHRazorIce"
			.InternalCD = 0
			.ProcOn = procs.ProcOnType.OnMHhit
			.ProcChance = S.MainStat.MHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 1
			if s.RuneForge.MHRazoriceRF	then .Equip
		End With
		
		s.RuneForge.OHRazorIce = New RazorIce(S)
		With s.RuneForge.OHRazorIce
			.Name = "OHRazorIce"
			.InternalCD = 0
			.ProcOn = procs.ProcOnType.OnOHhit
			.ProcChance = S.MainStat.OHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 1
			if s.RuneForge.OHRazoriceRF	then .Equip
		End With
		
		MHFallenCrusader = new Proc(s)
		With MHFallenCrusader
			.Name = "MHFallenCrusader"
			.InternalCD = 0
			.ProcOn = procs.ProcOnType.OnMHhit
			.ProcChance = 2*S.MainStat.MHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 1
			if s.RuneForge.MHFallenCrusader	then .Equip
		End With
		
		OHFallenCrusader = new Proc(s)
		With OHFallenCrusader
			.Name = "OHFallenCrusader"
			.InternalCD = 0
			.ProcOn = procs.ProcOnType.OnMHhit
			.ProcChance = 2*S.MainStat.OHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 1
			if s.RuneForge.OHFallenCrusader	then .Equip
		End With
		
		
		s.RuneForge.MHCinderglacier = new Proc(s)
		With s.RuneForge.MHCinderglacier
			.Name = "MHCinderglacier"
			.InternalCD = 0
			.ProcOn = procs.ProcOnType.OnMHhit
			.ProcChance = 1*S.MainStat.MHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 2
			.DamageType = "cinderglacier"
			if s.RuneForge.MHCinderglacierRF then .Equip
		End With

		s.RuneForge.OHCinderglacier = new Proc(s)
		With s.RuneForge.OHCinderglacier
			.Name = "OHCinderglacier"
			.InternalCD = 0
			.ProcChance = 1*S.MainStat.OHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 2
			.DamageType = "cinderglacier"
			.ProcOn = procs.ProcOnType.OnOHhit
			if s.RuneForge.OHCinderglacierRF	then .Equip
		End With
		Berserking = New Proc(s)		
		With Berserking
			.Name = "Berserking"
			.InternalCD = 0
			.ProcOn = procs.ProcOnType.OnOHhit
			.ProcChance = 1.2*s.MainStat.OHWeaponSpeed/60
			.ProcLenght = 15
			.ProcValue = 400
			.ProcType = "ap"
			if s.RuneForge.OHBerserking then .Equip
		End With
		
		
		OrcRacial = New Proc(s)
		With OrcRacial
			.Name = "OrcRacial"
			.InternalCD  = 120
			.ProcOn = procs.ProcOnType.OnDamage
			.ProcChance = 1
			.ProcLenght = 15
			.ProcValue = 322
			.ProcType = "ap"
			If s.Character.Orc Then .Equip
		End With
		
		TrollRacial = New Proc(s)		
		With TrollRacial
			.Name = "TrollRacial"
			.InternalCD = 180
			.ProcChance = 1
			.ProcLenght = 15
			.ProcValue = 0.20
			.ProcOn = procs.ProcOnType.OnDamage
			if s.Character.Troll then .Equip
		End With
		
		BElfRacial = New Trinket(s)
		With BElfRacial
			.Name = "BElfRacial"
			.InternalCD = 120
			.ProcChance = 1
			.ProcLenght = 0
			.ProcValue = 15
			.DamageType = "torrent"
			.ProcOn = procs.ProcOnType.OnDamage
			if s.Character.BloodElf then .Equip
		End With
		
		
	End Sub
	
	Function GetActiveBonus(stat As String) As Integer
		Dim prc As proc
		dim tmp as Integer
		For Each prc In EquipedTrinkets
			If prc.ProcType = stat Then
				If prc.IsActive Then 
					tmp += prc.ProcValue
				End If
			End If
		Next
		return tmp
	End Function
	
	
	
	
	
	Sub tryT104PDPS(T As Long)
		If sim.MainStat.T104PDPS = 0 Then Exit Sub
		'Debug.Print(T & vbtab & sim.Runes.Rune1.AvailableTime & vbtab & sim.Runes.Rune2.AvailableTime & vbtab & sim.Runes.Rune3.AvailableTime & vbtab & sim.Runes.Rune4.AvailableTime & vbtab & sim.Runes.Rune5.AvailableTime & vbtab  & sim.Runes.Rune6.AvailableTime)
		If sim.Runes.BloodRune1.AvailableTime < T Then Exit Sub
		If sim.Runes.BloodRune2.AvailableTime < T Then Exit Sub
		If sim.Runes.FrostRune1.AvailableTime < T Then Exit Sub
		If sim.Runes.FrostRune2.AvailableTime < T Then Exit Sub
		If sim.Runes.UnholyRune1.AvailableTime < T Then Exit Sub
		If sim.Runes.UnholyRune2.AvailableTime < T Then Exit Sub
		T104PDPSFAde = T +15 *100
	End Sub
end Class
