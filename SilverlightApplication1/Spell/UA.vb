'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class PillarOfFrost
    Inherits Spells.Spell
    Friend previousFade As Long
    Friend Talented As Boolean

    Sub New(ByVal S As sim)
        MyBase.New(S)
        logLevel = LogLevelEnum.Basic
        If sim.Character.Talents.Talent("PillarOfFrost").Value <> 0 Then Talented = True


    End Sub



    Function IsAvailable(ByVal T As Long) As Boolean

        If Not Talented Then Return False
        If CD >= T Then Return False
        If sim.BloodTap.IsAvailable(T) And sim.Runes.Frost(T) = False Then
            Return True
        Else
            Return False
        End If

    End Function
    Function Use(ByVal T As Long) As Boolean


        If sim.Runes.Frost(T) = False Then
            If sim.BloodTap.IsAvailable(T) Then
                sim.BloodTap.Use(T)
            Else
                Return False
            End If
        End If
       
        CD = T + 60 * 100
        sim.Runes.UseDeathBlood(T, True)
        ActiveUntil = T + 20 * 100

        sim._UseGCD(T, 1)
        sim.RunicPower.add(15)
        sim.CombatLog.write(T & vbTab & "Pillar of Frost")
        Me.HitCount = Me.HitCount + 1
        AddUptime(T)
        Return True
    End Function
    Function isActive() As Boolean
        If ActiveUntil >= sim.TimeStamp Then
            Return True
        Else
            Return False
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
