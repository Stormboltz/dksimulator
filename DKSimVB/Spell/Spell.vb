﻿'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 13/09/2009
' Heure: 14:25
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
NameSpace Spells
Public Class Spell
	Public Total As  long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit As Long
	Friend CD As Long
	Friend ActiveUntil As Long
	Public ThreadMultiplicator as double
	Protected Sim As Sim
	Protected _RNG as Random
	
	Function MyRng as Double 
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(me.ToString)+RNGSeeder)
		End If
		return _RNG.NextDouble
	End Function
	
	Function DoMySpellHit As Boolean
		Dim RNG As Double
		RNG = MyRNG
		If math.Min(sim.mainstat.SpellHit,0.17) + RNG < 0.17 Then
			Return False
		Else
			return true
		End If
	End Function
	
	Sub New()		
		Init
	End Sub
	
	
	
	
	Overridable Sub Init()
		Total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
		CD = 0
		ActiveUntil = 0
		ThreadMultiplicator = 1
		_RNG = Nothing

	End Sub
	
	Sub New(S As sim)
		me.New
		Sim = S
		sim.DamagingObject.Add(me)
	End Sub
	
	sub UseGCD(T as Long)
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste)) + sim.latency/10
	End sub
	
	
	
	
	Overridable Public Function ApplyDamage(T As Long) As Boolean
	End Function
	Overridable Public Function ApplyDamage(T As Long, SDoom as Boolean) As Boolean
	End Function
		
	Overridable Function AvrgNonCrit(T as long) As Double
	End Function
	
	Overridable Function CritCoef() As Double
	End Function
	
	Overridable Function CritChance() As Double
	End Function
	
	Overridable Function AvrgCrit(T As long) As Double
	End Function
	
	Overridable Function report As String
		dim tmp as String
		tmp = ShortenName(me.Name)  & VBtab
		
		tmp = tmp & total & VBtab
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		
		tmp = tmp & toDecimal(HitCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(totalhit/(HitCount)) & VBtab
		
		tmp = tmp & toDecimal(CritCount) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(totalcrit/(CritCount)) & VBtab
				
		tmp = tmp & toDecimal(MissCount) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		
		If sim.FrostPresence Then
			tmp = tmp & toDecimal((100 * total * ThreadMultiplicator * 2.0735 ) / sim.TimeStamp) & VBtab
		End If
		tmp = tmp & vbCrLf
		tmp = replace(tmp, VBtab & 0, vbtab)
		return tmp
	End Function
	
	Function Name() As String
		return me.ToString
	End Function
	
	Overridable Public Sub Merge()
	End Sub
	
	
	
End Class
end Namespace