'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/12/2009
' Heure: 2:01 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class ScourgeStrikeMagical
	Inherits strikes.Strike
	
	friend tmpPhysical as Integer
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	
	
	Function ApplyDamage(PhysicalDamage As Integer,T As Long, IsCrit as Boolean) As Boolean
		Dim RNG As Double
		dim tmp as Integer
		tmpPhysical = PhysicalDamage
		RNG = MyRNG
		
		If IsCrit Then 'RNG <= CritChance Then
			CritCount +=1
			tmp = AvrgNonCrit(T)
			TotalCrit +=  tmp
			sim.tryOnCrit
		Else
			HitCount += 1 
			tmp = AvrgNonCrit(T)
			Totalhit += tmp
		End If
		'sim.tryOnDamageProc()
		total += tmp
	End Function
	
	
	public Overrides Function AvrgNonCrit(T As Long) As Double
		Dim tmpMagical As Integer
		Dim addtiveDamage As Double
		
		tmpMagical = tmpPhysical
		If sim.MainStat.T84PDPS = 1 Then
			tmpMagical = tmpMagical * (0.25 * Sim.NumDesease * 1.2)
		Else
			tmpMagical = tmpMagical * (0.25 * Sim.NumDesease )
		End If
		Dim tmp As Double
		tmp = 1
		addtiveDamage = 1
		addtiveDamage += sim.BloodPresence * 0.15
		addtiveDamage += 0.02 * sim.BoneShield.Value(T)
		If sim.Desolation.isActive(T) Then addtiveDamage += sim.Desolation.Bonus
		addtiveDamage +=  sim.TalentFrost.BlackIce * 2 / 100
		tmp = tmp * addtiveDamage 
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		tmp = tmp * (1 + 0.02 * sim.TalentBlood.BloodGorged)
		if sim.proc.T104PDPSFAde >= T then tmp = tmp * 1.03
		tmp = tmp * (1 + 0.13 *  sim.Buff.SpellDamageTaken)
		tmp = tmp * (1-15/(510+15)) 'Partial Resistance. It's about 0,029% less damage on average.
		tmpMagical = tmpMagical * tmp
		If sim.RuneForge.CinderglacierProc > 0 Then
			tmpMagical = tmpMagical * 1.2
			sim.RuneForge.CinderglacierProc = sim.RuneForge.CinderglacierProc -1
		End If
		Return tmpMagical
	End Function
	
	Public Overrides Function CritCoef() As Double
		return sim.ScourgeStrike.CritCoef
	End Function
	Public Overrides Function CritChance() As Double
		return sim.ScourgeStrike.CritChance
	End Function
	
	
	
End Class
