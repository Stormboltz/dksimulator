'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 24/09/2009
' Heure: 21:55
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Spells
    Public Class BoneShield
        Inherits Spells.Spell
        Friend Charge As Integer
        Friend previousFade As Long

        Function BuffLength() As Integer
            Return Sim.BoneShieldTTL
        End Function
        Sub New(ByVal MySim As Sim)
            MyBase.New(MySim)
            logLevel = LogLevelEnum.Basic
        End Sub
        Sub UseCharge(ByVal T As Long)
            Charge = Charge - 1
            If Charge = 0 Then
                Me.ActiveUntil = T
                RemoveUptime(T)
                Charge = 0
            End If
        End Sub
        Sub PreBuff()
            If Sim.Character.Talents.Talent("BoneShield").Value = 1 Then
                CD = Sim.TimeStamp + 60 * 100
                ActiveUntil = Sim.TimeStamp + BuffLength() * 100
                AddUptime(Sim.TimeStamp)
                HitCount += 1
            End If
        End Sub
        Public Overloads Overrides Sub Init()
            MyBase.Init()

        End Sub


        Function Use(ByVal T As Long) As Boolean
            If Sim.Character.Talents.Talent("BoneShield").Value = 0 Then Return False
            If Sim.Runes.Unholy() = False Then
                If Sim.BloodTap.IsAvailable(T) Then
                    Sim.BloodTap.Use(T)
                Else
                    Return False
                End If
            End If

            Me.CD = T + 60 * 100
            Me.ActiveUntil = T + BuffLength() * 100
            Sim.Runes.UseDeathBlood(T, True)
            UseGCD(T)
            Sim.RunicPower.add(15)
            Sim.CombatLog.write(T & vbTab & "Bone Shield")
            Charge = 3
            If Sim.Character.Glyph.BoneShield Then Charge += 1
            HitCount += 1
            AddUptime(T)
            Return True
        End Function

        Function IsAvailable(ByVal T As Long) As Boolean
            If Sim.Character.Talents.Talent("BoneShield").Value = 0 Then Return False
            If ActiveUntil > T Then Return False
            If Sim.BloodTap.IsAvailable(T) = False Then Return False
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
            If ActiveUntil > Sim.NextReset Then
                tmp = (Sim.NextReset - T)
            Else
                tmp = ActiveUntil - T
            End If

            If previousFade < T Then
                uptime += tmp
            Else
                uptime += tmp - (previousFade - T)
            End If
            previousFade = T + tmp
        End Sub

        Sub RemoveUptime(ByVal T As Long)
            If previousFade < T Then
            Else
                uptime -= (previousFade - T)
            End If
            previousFade = T
        End Sub



    End Class
End Namespace
