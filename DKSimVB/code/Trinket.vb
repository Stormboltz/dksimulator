'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/08/2009
' Heure: 21:30
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Trinket
	
	Friend MjolRuneFade As Integer
	Friend MjolRuneCd As Integer
	Friend GrimTollFade As Integer
	Friend GrimTollCd As Integer
	Friend BitterAnguishCd As Integer
	Friend BitterAnguishFade As Integer
	
	Friend MjolRune As Integer
	Friend GrimToll As Integer
	Friend BitterAnguish As Integer
	Friend Mirror As Integer
	Friend Greatness As Integer
	Friend DCDeath As Integer
	Friend Victory As Integer
	Friend Necromantic As Integer
	Friend Bandit As Integer
	Friend Pyrite As Integer
	Friend DarkMatter As Integer
	Friend OldGod As Integer
	Friend Comet As Integer
	Friend DeathChoice As Integer
	Friend DeathChoiceHeroic As Integer
	
	Friend Total as long
	Friend HitCount as Integer
	Friend MissCount as Integer
	Friend CritCount as Integer
	
	
	
	Protected sim as Sim
	Sub New(S As Sim)
		Sim = S
		MjolRuneFade = 0
		MjolRuneCd = 0
		GrimTollFade = 0
		GrimTollCd = 0
		MirrorFade = 0
		MirrorCd = 0
		GreatnessFade = 0
		GreatnessCd = 0
		DCDeathFade = 0
		DCDeathCd = 0
		VictoryFade = 0
		VictoryCd = 0
		NecromanticFade = 0
		NecromanticCd = 0
		BanditFade = 0
		BanditCd = 0
		PyriteFade = 0
		PyriteCd = 0
		DarkMatterFade = 0
		DarkMatterCd = 0
		OldGodFade = 0
		OldGodCd = 0
		CometFade = 0
		CometCd = 0
		DeathChoiceFade = 0
		DeathChoiceCd = 0
		DeathChoiceHeroicFade = 0
		DeathChoiceHeroicCd = 0
		BitterAnguishCD = 0
		BitterAnguishfade = 0 
		
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
	End Sub
	
	'MjolRune
	Sub TryMjolRune()
		If MjolRune = 0 Or MjolRuneCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.15 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "MjolRune proc")
			MjolRuneFade = sim.TimeStamp + 10 * 100
			MjolRuneCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'GrimToll
	Sub TryGrimToll()
		If GrimToll = 0 Or GrimTollCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.15 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "GrimToll proc")
			GrimTollFade = sim.TimeStamp + 10 * 100
			GrimTollCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'BitterAnguish
	Sub TryBitterAnguish()
		If BitterAnguish = 0 Or BitterAnguishCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.10 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "BitterAnguish proc")
			BitterAnguishFade = sim.TimeStamp + 10 * 100
			BitterAnguishCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Mirror of Truth
	Friend MirrorFade As Integer
	Friend MirrorCd As Integer
	Sub TryMirror()
		If Mirror = 0 Or MirrorCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.10 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Mirror proc")
			MirrorFade = sim.TimeStamp + 10 * 100
			MirrorCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Pyrite Infuser
	Friend PyriteFade As Integer
	Friend PyriteCd As Integer
	Sub TryPyrite()
		If Pyrite = 0 Or PyriteCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.10 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Pyrite proc")
			PyriteFade = sim.TimeStamp + 10 * 100
			PyriteCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Blood of the Old God
	Friend OldGodFade As Integer
	Friend OldGodCd As Integer
	Sub TryOldGod()
		If  OldGod = 0 Or OldGodCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.10 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "OldGod proc")
			OldGodFade = sim.TimeStamp + 10 * 100
			OldGodCd = sim.TimeStamp + 45 * 100
		End If
	End Sub

	'Darkmoon Card: Greatness
	Friend GreatnessFade As Integer
	Friend GreatnessCd As Integer
	Sub TryGreatness()
		If  Greatness= 0 Or GreatnessCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.35 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Greatness proc")
			GreatnessFade = sim.TimeStamp + 15 * 100
			GreatnessCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Death's Choice
	Friend DeathChoiceFade As Integer
	Friend DeathChoiceCd As Integer
	Sub TryDeathChoice()
		If DeathChoice = 0 Or DeathChoiceCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.35 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DeathChoice proc")
			DeathChoiceFade = sim.TimeStamp + 15 * 100
			DeathChoiceCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Death's Choice Heroic
	Friend DeathChoiceHeroicFade As Integer
	Friend DeathChoiceHeroicCd As Integer
	Sub TryDeathChoiceHeroic()
		If DeathChoiceHeroic = 0 Or DeathChoiceHeroicCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.35 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DeathChoiceHeroic proc")
			DeathChoiceHeroicFade = sim.TimeStamp + 15 * 100
			DeathChoiceHeroicCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Banner of Victory
	Friend VictoryFade As Integer
	Friend VictoryCd As Integer
	Sub TryVictory()
		If Victory = 0 Or VictoryCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.20 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Victory proc")
			VictoryFade = sim.TimeStamp + 10 * 100
			VictoryCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Darkmoon Card: Death
	Friend DCDeathFade As Integer
	Friend DCDeathCd As Integer
	Sub TryDCDeath()
		If DCDeath = 0 Or DCDeathCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.15 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DCDeath proc")
			ApplyDamage(2000)
			DCDeathCd = sim.TimeStamp + 45 * 100
		End If
	End Sub

	'Extract of Necromantic Power
	Friend NecromanticFade As Integer
	Friend NecromanticCd As Integer
	Sub TryNecromantic()
		If  Necromantic= 0 Or NecromanticCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.10 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Necromantic proc")
			ApplyDamage(1050)
			NecromanticCd = sim.TimeStamp + 15 * 100
		End If
	End Sub
	
	'Bandit's Insignia
	Friend BanditFade As Integer
	Friend BanditCd As Integer
	Sub TryBandit()
		If Bandit = 0 Or BanditCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.15 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Bandit proc")
			ApplyDamage(1880)
			BanditCd =  sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Dark Matter
	Friend DarkMatterFade As Integer
	Friend DarkMatterCd As Integer
	Sub TryDarkMatter()
		If  DarkMatter= 0 Or DarkMatterCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.15 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DarkMatter proc")
			DarkMatterFade = sim.TimeStamp + 10 * 100
			DarkMatterCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	'Comet's Trail
	Friend CometFade As Integer
	Friend CometCd As Integer
	Sub TryComet()
		If Comet = 0 Or CometCd > sim.TimeStamp Then Exit Sub
		If sim.RandomNumberGenerator.RNGProc <= 0.15 Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "Comet proc")
			CometFade = sim.TimeStamp + 10 * 100
			CometCd = sim.TimeStamp + 45 * 100
		End If
	End Sub
	
	Function report as String
		dim tmp as String
		tmp = "Trinket" & VBtab
		
		If Total.ToString().Length < 8 Then
			tmp = tmp & sim.RuneForge.RazoriceTotal & "   " & VBtab
		Else
			tmp = tmp & sim.RuneForge.RazoriceTotal & VBtab
		End If
		
		tmp = tmp & toDecimal(100*Total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(Total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	
	Sub ApplyDamage(d As Integer)
		If sim.RandomNumberGenerator.RNGProc < (0.17 - sim.MainStat.SpellHit) Then
			MissCount = MissCount + 1
			Exit sub
		End If
		dim dégat as Integer
		If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.SpellCrit Then
			CritCount = CritCount + 1
			dégat= d*1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
		Else
			dégat= d * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
			HitCount = HitCount + 1
		End If
		
		total = total + dégat
	End Sub
	
End Class
