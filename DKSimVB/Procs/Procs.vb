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
	Friend T104PDPS as proc

	Protected Sim as Sim
	Friend T104PDPSFAde As Integer
	

	

	
	
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
			.ProcChance = 0.5
			.ProcValue = 180
			.ProcLenght = 15
			.CD  = 45
		End With

		HauntedDreams = New Proc(s)
		With HauntedDreams
			If s.Sigils.HauntedDreams	Then .Equiped = 1
			.ProcChance = 0.15
			.ProcValue = 173
			.ProcLenght = 10
			.CD  = 45
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
