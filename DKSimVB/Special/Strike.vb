'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 13/09/2009
' Heure: 14:25
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Strikes

Public Class Strike
	Friend Total As  long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit As Long
	Protected Sim as Sim
	
	
    
    Public Sub New()
		init()
    End Sub
    
	Overridable Protected Sub init()
		Total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End sub
	
	
	Overridable Public Function isAvailable(T As Long) As Boolean
	End Function
	
	Overridable Public Function ApplyDamage(T As Long) As Boolean
	End Function
	
	Overridable Public Function ApplyDamage(T As Long,MH as Boolean) As Boolean
	End Function
	
	Overridable Function AvrgNonCrit(T as long, MH as Boolean) As Double
	End Function
	Overridable Function AvrgNonCrit(T as long) As Double
	End Function
	
	Overridable Function CritCoef() As Double
	End Function
	
	Overridable Function CritChance() As Double
	End Function
	
	Overridable Function AvrgCrit(T As long, MH as Boolean) As Double
	End Function
	Overridable Function AvrgCrit(T As long) As Double
	End Function
	
	Overridable Function report As String
		dim tmp as String
		tmp = ShortenName(me.ToString)  & VBtab
		
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	
End Class
end Namespace