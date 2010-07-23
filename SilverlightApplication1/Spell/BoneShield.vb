'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 24/09/2009
' Heure: 21:55
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class BoneShield
	Inherits Spells.Spell
	Friend Charge As Integer
	Friend previousFade As Long
	
	Function BuffLength() As Integer
		return sim.BoneShieldTTL
	End Function
	
	
	
	Sub New(MySim as Sim)
		sim = MySim
	End Sub
	
	Sub UseCharge(T as Long)
		Charge = Charge -1
		If Charge = 0 Then
			Me.ActiveUntil = T
			RemoveUptime(T)
			Charge = 0
		End If
	End Sub
	
	Sub PreBuff
        If sim.Character.Talents.Talent("BoneShield").Value = 1 Then
            CD = sim.TimeStamp + 60 * 100
            ActiveUntil = sim.TimeStamp + BuffLength() * 100
            AddUptime(sim.TimeStamp)
            HitCount += 1
        End If
    End Sub
    Public Overloads Overrides Sub Init()
        MyBase.Init()

    End Sub


    Function Use(ByVal T As Long) As Boolean
        If sim.Character.Talents.Talent("BoneShield").Value = 0 Then Return False
        If sim.Runes.Unholy() = False Then
            If sim.BloodTap.IsAvailable(T) Then
                sim.BloodTap.Use(T)
            Else
                Return False
            End If
        End If

        If sim.BoneShieldUsageStyle = 1 Or sim.BoneShieldUsageStyle = 2 Then
            If sim.KeepBloodSync Then
                If sim.BloodToSync = True Then
                    sim.BloodToSync = False
                Else
                    sim.BloodToSync = True
                End If
            End If
        Else
            sim.BloodToSync = False
        End If

        Me.CD = T + 60 * 100
        Me.ActiveUntil = T + BuffLength() * 100
        sim.Runes.UseDeathBlood(T, True)
        UseGCD(T)
        sim.RunicPower.add(10)
        sim.CombatLog.write(T & vbTab & "Bone Shield")
        Charge = 3
        If sim.Character.Glyph.BoneShield Then Charge += 1
        HitCount += 1
        AddUptime(T)
        Return True
    End Function

    Function IsAvailable(ByVal T As Long) As Boolean
        If sim.Character.Talents.Talent("BoneShield").Value = 0 Then Return False
        If ActiveUntil > T Then Return False
        If sim.BloodTap.IsAvailable(T) = False Then Return False
        If CD > T Then Return False
        Return True
    End Function

    Function Value(ByVal T As Long) As Integer
        If ActiveUntil > T Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Sub AddUptime(ByVal T As Long)
        Dim tmp As Long
        If ActiveUntil > sim.NextReset Then
            tmp = (sim.NextReset - T)
        Else
            tmp = ActiveUntil - T
        End If

        If previousfade < T Then
            uptime += tmp
        Else
            uptime += tmp - (previousFade - T)
        End If
        previousFade = T + tmp
    End Sub

    Sub RemoveUptime(ByVal T As Long)
        If previousfade < T Then
        Else
            uptime -= (previousFade - T)
        End If
        previousFade = T
    End Sub



End Class
