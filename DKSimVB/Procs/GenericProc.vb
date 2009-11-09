'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/9/2009
' Heure: 2:04 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Proc
	Friend CD as Integer
	Friend Fade As Integer
	Friend ProcChance As Double
	Friend _RNG As Random
	Friend Equiped As Integer
	Friend ProcLenght As Integer
	Friend ProcValue As Integer
	Friend InternalCD As Integer
	protected Sim as Sim
	Friend Total as Integer
	Friend HitCount As Integer
	Friend MissCount As Integer
	Friend CritCount As Integer
	Friend DamageType As String
	Friend Count as Integer

	
	
	Function RNGProc As Double
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(me.ToString)+RNGSeeder)
		End If
		return _RNG.NextDouble
	End Function
	
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
		count = 0
	End Sub
	Sub New(S As Sim)
		me.New
		Sim = S
	End Sub
	
	Overridable Function IsActive() As Boolean
		if Fade >= sim.TimeStamp then return true
	End Function
	
	Overridable Function Use() As Boolean
		Fade = 0
		count = 0
	End Function
	
	
	
	
	Overridable Sub TryMe(T As Long)
		If Equiped = 0 Or CD > T Then Exit Sub
		If RNGProc <= ProcChance Then
			CD = T + InternalCD * 100
			If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
			Fade = T + ProcLenght * 100
			Count += 1
			HitCount += 1
		end if
	End Sub
	
End Class
