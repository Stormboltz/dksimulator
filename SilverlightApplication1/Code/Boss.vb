Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Namespace Simulator
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
        Sim.proc.tryProcs(WowObjects.Procs.ProcsManager.ProcOnType.OnBossHitOrMiss)
        If RNGHit > Avoidance Then
            'Boss hit
            Sim.BoneShield.UseCharge(T)
        Else
            'Boss miss
            Sim.RuneStrike.trigger = True
        End If
        Return True
    End Function

    Sub LoadTankOptions()
        Try

            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/TankConfig.xml", FileMode.Open, FileAccess.Read, Sim.isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
                Avoidance = doc.Element("config").Element("Stats").Element("txtFBAvoidance").Value / 100
                Speed = doc.Element("config").Element("Stats").Element("txtFPBossSwing").Value * 100
                SpecialArmor = doc.Element("config").Element("Stats").Element("txtFPArmor").Value
            End Using

        Catch
            msgBox("Error retriving Tank parameters")
        End Try

    End Sub
    Sub SoftReset()
        If Sim.BloodPresence <> 0 Then Sim.FutureEventManager.Add(Sim.TimeStamp + 1, "Boss")
    End Sub
End Class
End Namespace
