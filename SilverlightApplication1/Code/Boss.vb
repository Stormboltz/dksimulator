Imports System.Xml.Linq

'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 29/09/2009
' Heure: 09:57
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Boss
    Friend NextHit As Long
    Private Avoidance As Double
    Private Sim As Sim
    Private Speed As Integer
    Friend SpecialArmor As Integer
    Friend _RNG As Random


    Function MyRng() As Double
        If _RNG Is Nothing Then
            _RNG = New Random(ConvertToInt("Boss") + RNGSeeder)
        End If
        Return _RNG.NextDouble
    End Function


    Sub New(ByVal S As Sim)
        NextHit = 0
        Sim = S
        LoadTankOptions()
    End Sub

    Function ApplyDamage(ByVal T As Long) As Boolean
        Dim RNGHit As Double
        RNGHit = MyRng()
        NextHit = T + Speed
        Sim.FutureEventManager.Add(NextHit, "Boss")
        If RNGHit > Avoidance Then
            'Boss hit
            Sim.BoneShield.UseCharge(T)
        Else
            'Boss miss
            Sim.RuneStrike.trigger = True
        End If
        Sim.proc.ScentOfBlood.TryMe(T)
    End Function

    Sub LoadTankOptions()
        Dim doc As XDocument = New XDocument
        Try
            doc.Load("TankConfig.xml")
            Avoidance = doc.Element("//config/Stats/txtFBAvoidance").Value / 100
            Speed = doc.Element("//config/Stats/txtFPBossSwing").Value * 100
            SpecialArmor = doc.Element("//config/Stats/txtFPArmor").Value
        Catch
            msgBox("Error retriving Tank parameters")
        End Try

    End Sub

End Class
