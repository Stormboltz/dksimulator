'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 11:12
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Hysteria
	inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	Function IsAvailable(T As Long) As Boolean
		If sim.Character.talentblood.Hysteria =  0 Then Return False 
		If sim.Character.talentblood.DRW = 1 and sim.DRW.cd > T then return false
        If CD <= T Then
            Return True
        Else
            Return False
        End If
	End Function

	Function IsActive(T as Long) as Boolean
        If T <= ActiveUntil Then
            Return True
        Else
            Return False
        End If
        

    End Function
	
	Sub use(T As Long)
		CD = T + 3*60*100
		ActiveUntil = T + 3000
	End Sub
	
	
End Class
