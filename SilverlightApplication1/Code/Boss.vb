Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

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
        Return True
    End Function

    Sub LoadTankOptions()
        Try
            Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/TankConfig.xml", FileMode.Open, isoStore)
                    Dim doc As XDocument = XDocument.Load(isoStream)
                    Avoidance = doc.Element("config").Element("Stats").Element("txtFBAvoidance").Value / 100
                    Speed = doc.Element("config").Element("Stats").Element("txtFPBossSwing").Value * 100
                    SpecialArmor = doc.Element("config").Element("Stats").Element("txtFPArmor").Value
                End Using
            End Using
        Catch
            msgBox("Error retriving Tank parameters")
        End Try

    End Sub

End Class
