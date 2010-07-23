'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 24/09/2009
' Heure: 23:21
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'




Public Class Frenzy
	Inherits Spells.Spell
	
	Sub New(s as Sim)
		MyBase.New(s)
	End Sub
	
	
	Function IsFrenzyAvailable(T As Long) As Boolean
        If sim.Character.Talents.Talent("GhoulFrenzy").Value = 0 Then Return False
        If CD < T And sim.Runes.Unholy() Then Return True
        Return False
    End Function

    Function IsAutoFrenzyAvailable(ByVal T As Long) As Boolean
        If sim.Character.Talents.Talent("GhoulFrenzy").Value = 0 Then Return False
        If sim.Rotate = True Then
            If sim.Rotation.MyRotation.Contains("GhoulFrenzy") Then Return False
        Else
            If sim.Priority.prio.Contains("GhoulFrenzy") Then Return False
        End If


        If CD < T Then
            If sim.Runes.Unholy() = False Then
                If sim.Runes.Blood(T) = False Then
                    If sim.BloodTap.IsAvailable(T) Then Return True
                End If
            End If
        End If
        Return False
    End Function

    Function Frenzy(ByVal T As Long) As Boolean
        If sim.Runes.Unholy() = False Then
            If sim.BloodTap.IsAvailable(T) Then
                sim.BloodTap.Use(T)
            Else
                Return False
            End If
        End If
        UseGCD(T)
        sim.runes.UseDeathBlood(T, True)
        Sim.RunicPower.add(10)
        CD = T + 3000
        Me.ActiveUntil = T + 3000
        If sim.combatlog.LogDetails Then sim.combatlog.write(T & vbtab & "Using Ghoul Frenzy")
        HitCount = HitCount + 1
        Return True
    End Function


End Class
