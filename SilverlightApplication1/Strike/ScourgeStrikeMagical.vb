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
	
	public Overrides Function AvrgCrit(T As long,target As Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	
	
	Function ApplyDamage(PhysicalDamage As Integer,T As Long, IsCrit as Boolean) As Boolean
		dim tmp as Integer
		tmpPhysical = PhysicalDamage
		
		
		If IsCrit Then 'RNG <= CritChance Then
			CritCount +=1
			tmp = AvrgNonCrit(T)
			TotalCrit +=  tmp
		Else
			HitCount += 1
			tmp = AvrgNonCrit(T)
			Totalhit += tmp
		End If
		'sim.tryOnDamageProc()
		total += tmp
	End Function
	
	
	Public Overrides Function AvrgNonCrit(T As Long,target As Targets.Target) As Double
		
		
		Dim tmpMagical As Integer
		Dim addtiveDamage As Double
		
		tmpMagical = tmpPhysical
		tmpMagical = tmpMagical * (0.12 * target.NumDesease )
		If sim.MainStat.T84PDPS = 1 Then tmpMagical =tmpMagical  * 1.2
		Dim tmp As Double
		tmp = 1
		addtiveDamage = 1
		addtiveDamage += sim.BloodPresence * 0.15
		addtiveDamage += 0.02 * sim.BoneShield.Value(T)
		If sim.proc.Desolation.IsActiveAt(T) Then addtiveDamage += sim.proc.Desolation.ProcValue / 100
		addtiveDamage +=  sim.Character.talentfrost.BlackIce * 2 / 100
		tmp = tmp * addtiveDamage
		tmp = tmp * (1 + 0.03 *  sim.Character.Buff.PcDamage)
		tmp = tmp * (1 + 0.02 * sim.Character.talentblood.BloodGorged)
		If sim.proc.T104PDPS.IsActiveAt(T) Then tmp = tmp * 1.03
		tmp = tmp * (1 + 0.13 * target.Debuff.SpellDamageTaken)
		tmp = tmp * (1-15/(510+15)) 'Partial Resistance. It's about 0,029% less damage on average.
		tmpMagical = tmpMagical * tmp
		if sim.RuneForge.CheckCinderglacier(True) > 0 then tmpMagical *= 1.2
		return tmpMagical
	End Function
	
	Public Overrides Function CritCoef() As Double
		return sim.ScourgeStrike.CritCoef
	End Function
	Public Overrides Function CritChance() As Double
		return sim.ScourgeStrike.CritChance
	End Function
	
	
	
End Class
