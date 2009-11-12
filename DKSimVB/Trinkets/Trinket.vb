'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/08/2009
' Heure: 21:30
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'

Public Class Trinket
	Friend CD as Integer
	Friend Fade As Integer
	Friend ProcChance As Double
	Friend _RNG As Random
	Friend Equiped As Integer
	Friend ProcLenght As Integer
	Friend ProcValue As Integer
	Friend InternalCD As Integer
	protected Sim as Sim
	public Total as long
	Friend	HitCount As Integer
	Friend	MissCount As Integer
	Friend	CritCount As Integer
	Friend Name as String
	
	Friend DamageType as String

	
	
	Function RNGProc As Double
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(me.ToString)+RNGSeeder)
		End If
		return _RNG.NextDouble
	End Function
	
	
	Overridable Sub TryMe(T As Long)
		dim tmp as Long
		If Equiped = 0 Or CD > T Then Exit Sub
		If RNGProc <= ProcChance Then
			CD = T + InternalCD * 100
			Select Case DamageType
				Case ""
					If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
					Fade = T + ProcLenght * 100
					HitCount += 1
				Case "arcane"
					If RNGProc < (0.17 - sim.MainStat.SpellHit) Then
						MissCount = MissCount + 1
					Exit sub
					End If
					If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.SpellCrit Then
						CritCount = CritCount + 1
						tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
					Else
						tmp = ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						HitCount = HitCount + 1
					End If
				Case "shadow"
					If RNGProc < (0.17 - sim.MainStat.SpellHit) Then
						MissCount = MissCount + 1
					Exit sub
					End If
					If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.SpellCrit Then
						CritCount = CritCount + 1
						
						tmp = ProcValue * 1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
						tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
					Else
						tmp= ProcValue * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
						HitCount = HitCount + 1
					End If
				Case "physical"
					If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.Crit Then
						CritCount = CritCount + 1
						tmp = ProcValue * 2 * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
						
					Else
						tmp= ProcValue * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
						tmp = tmp * (1 + sim.MainStat.BloodPresence*0.15)
						HitCount = HitCount + 1
					End If
					
				Case "razorice"
					HitCount = HitCount + 1
					tmp = procvalue
			End Select
			total += tmp
		End If
	End Sub
	
	
	Sub New()
		_RNG = nothing
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		ProcChance = 0
		Equiped = 0
		ProcLenght = 0
		ProcValue = 0
		InternalCD = 0
	End Sub
	Sub New(S As Sim)
		me.New
		Sim = S
		s.TrinketsCollection.Add(me)
	End Sub
	
	
	Sub ApplyDamage(d As Integer)
		If RNGProc < (0.17 - sim.MainStat.SpellHit) Then
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
	
	
	
	
	
	
	
	Function report as String
		dim tmp as String
		tmp = name & VBtab
		tmp = tmp & total & VBtab
		tmp = tmp & toDecimal(100*Total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(Total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
End Class
