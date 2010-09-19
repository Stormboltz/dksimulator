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
        Dim Talented As Boolean

        Function BuffLength() As Integer
            Return Sim.BoneShieldTTL
        End Function
        Sub New(ByVal MySim As Sim)
            MyBase.New(MySim)
            logLevel = LogLevelEnum.Basic
            Resource = New Resource(sim, ResourcesEnum.BloodTap, 15, False)
            If sim.Character.Talents.Talent("BoneShield").Value = 1 Then Talented = True
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


        Overrides Sub Use()
            If Not Talented Then Return
            Me.CD = sim.TimeStamp + 60 * 100
            Me.ActiveUntil = sim.TimeStamp + BuffLength() * 100
            MyBase.Use()
            UseGCD(sim.TimeStamp)
            sim.CombatLog.write(sim.TimeStamp & vbTab & "Bone Shield")
            Charge = 3
            If sim.Character.Glyph.BoneShield Then Charge += 1
            HitCount += 1
            AddUptime(sim.TimeStamp)
        End Sub

        Overrides Function IsAvailable() As Boolean
            If CD > sim.TimeStamp Then Return False
            If ActiveUntil > sim.TimeStamp Then Return False
            Return MyBase.IsAvailable
        End Function

        Function Value(ByVal T As Long) As Integer
            If ActiveUntil > T Then
                Return 1
            Else
                Return 0
            End If
        End Function

        Sub AddUptime(ByVal T As Long)
            If Not sim.CalculateUPtime Then Return
            Dim tmp As Long
            If ActiveUntil > sim.NextReset Then
                tmp = (sim.NextReset - T)
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
