'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 00:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Proc
	Friend KillingMachine As Boolean
	Friend Rime as Boolean
	Friend StrifeFade as double
	Friend HauntedDreamsFade As Double
	Friend ScentOfBloodCD As Long
	Friend ScentOfBloodProc as Integer
	Friend VirulenceFade as Integer
	Friend T92PDPSFAde As Integer
	Friend MjolRuneFade As Integer
	Friend MjolRuneCd As Integer
	Friend GrimTollFade As Integer
	Friend GrimTollCd As Integer
	
	Sub init
		Proc.Rime = False
		KillingMachine = False
		ScentOfBlood = 0
		VirulenceFade = 0
		T92PDPSFAde = 0
		MjolRuneFade = 0
		MjolRuneCd = 0
		GrimTollFade = 0
		GrimTollCd = 0
	End Sub
	
	Sub TryRime()
		If Rnd <= 5 * talentfrost.Rime/100 Then
			Proc.Rime= True
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "Rime proc")
			HowlingBlast.cd = 0
		End If
	End Sub
	
	Sub TryMjolRune()
		If MjolRune = 0 Or MjolRuneCd > sim.TimeStamp Then Exit Sub
		If Rnd <= 0.15 Then
			MjolRuneFade = sim.TimeStamp + 10 * 100
			MjolRuneCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	Sub TryGrimToll()
		If GrimToll = 0 Or GrimTollCd > sim.TimeStamp Then Exit Sub
		If Rnd <= 0.15 Then
			GrimTollFade = sim.TimeStamp + 10 * 100
			GrimTollCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	Sub TryT92PDPS()
		If SetBonus.T92PDPS = 0 Then Exit Sub
		If Rnd <= 0.5 Then
			T92PDPSFAde = sim.TimeStamp + 15 * 100
		End If
	End Sub
	
	
	Sub GetUseScentOfBlood(T as Long)
		ScentOfBloodCD = T
		ScentOfBloodProc = TalentBlood.ScentOfBlood
	End Sub
	
	
	
end Module
