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
        logLevel = LogLevelEnum.detailed
	End Sub
	Function IsAvailable(T As Long) As Boolean
        If sim.Character.Talents.Talent("Hysteria").Value = 0 Then Return False
        If sim.Character.Talents.Talent("DRW").Value = 1 And sim.DRW.cd > T Then Return False
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
