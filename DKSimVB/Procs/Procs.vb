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
	
	
	Friend OnHitProcs As Collection
	
	

	

	
	
	Sub New(S As Sim)
		KillingMachine = New Proc(s)
		With KillingMachine
			.Equiped  = Talentfrost.KillingMachine
			.ProcLenght = 60
			.ProcChance = (Talentfrost.KillingMachine)*S.MainStat.MHWeaponSpeed/60
		End With
		
		Rime = New Proc(s)
		With Rime
			.Equiped  = talentfrost.Rime
			.ProcLenght = 60
			.ProcChance = 5 * talentfrost.Rime/100
		End With
		ScentOfBlood = New ScentOfBlood(s)
		With ScentOfBlood
			If s.MainStat.FrostPresence = 1 Then
				.Equiped  = TalentBlood.ScentOfBlood
			Else
				.Equiped = 0
			End If
			.ProcLenght = 60
			.ProcChance = 0.15
		End With
		
		Virulence = New Proc(s)
		With Virulence
			if s.Sigils.Virulence then .equiped = 1
			.ProcLenght = 20
			.ProcChance = 0.85
			.ProcValue = 200
		End With
		
		Strife = New Proc(s)
		With Strife
			if s.Sigils.strife then .equiped = 1
			.ProcChance = 1
			.ProcValue = 144
			.ProcLenght = 10
		End With
		
		T92PDPS = New Proc(s)
		With T92PDPS
			.equiped = s.MainStat.T92PDPS
			.ProcChance = .50
			.ProcValue = 180
			.ProcLenght = 15
			.InternalCD = 45
		End With

		HauntedDreams = New Proc(s)
		With HauntedDreams
			If s.Sigils.HauntedDreams	Then .Equiped = 1
			.ProcChance = 0.15
			.ProcValue = 173
			.ProcLenght = 10
			.InternalCD  = 45
		End With
		s.RuneForge.MHRazorIce = New RazorIce(S)
		With s.RuneForge.MHRazorIce
			.InternalCD = 0
			if s.RuneForge.MHRazoriceRF	then .Equiped=1
			.ProcChance = S.MainStat.MHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 1
		End With
		
		s.RuneForge.OHRazorIce = New RazorIce(S)
		With s.RuneForge.OHRazorIce
			.InternalCD = 0
			if s.RuneForge.OHRazoriceRF	then .Equiped=1
			.ProcChance = S.MainStat.OHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 1
		End With
		
		MHFallenCrusader = new Proc(s)
		With MHFallenCrusader
			.InternalCD = 0
			if s.RuneForge.MHFallenCrusader	then .Equiped=1
			.ProcChance = 2*S.MainStat.MHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 1
		End With
		
		OHFallenCrusader = new Proc(s)
		With OHFallenCrusader
			.InternalCD = 0
			if s.RuneForge.OHFallenCrusader	then .Equiped=1
			.ProcChance = 2*S.MainStat.OHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 1
		End With
		
		
		s.RuneForge.MHCinderglacier = new Proc(s)
		With s.RuneForge.MHCinderglacier
			.InternalCD = 0
			if s.RuneForge.MHCinderglacierRF	then .Equiped=1
			.ProcChance = 1*S.MainStat.MHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 2
			.DamageType = "cinderglacier"
		End With

		s.RuneForge.OHCinderglacier = new Proc(s)
		With s.RuneForge.OHCinderglacier
			.InternalCD = 0
			if s.RuneForge.OHCinderglacierRF	then .Equiped=1
			.ProcChance = 1*S.MainStat.OHWeaponSpeed/60
			.ProcLenght = 20
			.ProcValue = 2
			.DamageType = "cinderglacier"
		End With
		Berserking = New Proc(s)		
		With Berserking
			.InternalCD = 0
			if s.RuneForge.OHBerserking then .Equiped=1
			.ProcChance = 1.2*s.MainStat.OHWeaponSpeed/60
			.ProcLenght = 15
			.ProcValue = 400
		End With
		
		
		OrcRacial = New Proc(s)
		With OrcRacial
			.InternalCD  = 120
			if s.Character.Orc then .Equiped=1
			.ProcChance = 1
			.ProcLenght = 15
			.ProcValue = 322
		End With
		
		TrollRacial = New Proc(s)		
		With TrollRacial
			.InternalCD = 180
			if s.Character.Troll then .Equiped=1
			.ProcChance = 1
			.ProcLenght = 15
			.ProcValue = 0.20
		End With
		
		BElfRacial = New Trinket(s)
		With BElfRacial
			.InternalCD = 120
			if s.Character.BloodElf then .Equiped=1
			.ProcChance = 1
			.ProcLenght = 0
			.ProcValue = 15
			.DamageType = "torrent"
		End With
		
		T104PDPSFAde= 0
		sim = S
	End Sub
	
	
	
	
	
	
	
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
