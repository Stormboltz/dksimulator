Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

'
' Created by SharpDevelop.
' User: Fabien
' Date: 16/03/2009
' Time: 22:08
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Rotation
    Friend XMLRo As New XDocument
    Friend XMLIntro As New XDocument
    Friend MyRotation As New Collection
    Friend MyIntro As New Collection
    Friend IntroDone As Boolean
    Private Runes As Runes.runes
    Friend IntroStep As Integer
    Private sim As Sim
    Sub New(ByVal S As Sim)
        sim = S
        Runes = sim.Runes
    End Sub
    Sub loadRotation()
        MyRotation.Clear()

        sim.RotationStep = 0

        Dim XMLRo As XDocument
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & sim.rotationPath, FileMode.Open, isoStore)
                XMLRo = XDocument.Load(isoStream)
                For Each Nod In XMLRo.Element("Rotation/Rotation").Elements
                    Try
                        MyRotation.Add(Nod.Name, Nod.Name.ToString)
                    Catch
                        MyRotation.Add(Nod.Name)
                    End Try
                Next
                Dim i As Integer
                i = 0
            End Using
        End Using
    End Sub

    Sub DoRoration(ByVal TimeStamp As Long)
        Dim ret As Boolean
        If MyIntro.Count > 0 And IntroStep < MyIntro.Count Then Exit Sub
        ret = DoRoration(TimeStamp, MyRotation.Item(sim.RotationStep + 1), XMLRo.Element("Rotation/Rotation/" & MyRotation.Item(sim.RotationStep + 1)).Attribute("retry").Value)
        If ret = True Then sim.RotationStep = sim.RotationStep + 1
        If MyRotation.Count <= sim.RotationStep Then sim.RotationStep = 0
    End Sub
    Function DoRoration(ByVal TimeStamp As Double, ByVal Ability As String, ByVal retry As Integer) As Boolean
        Select Case Ability
            Case "BloodTap"
                If sim.BloodTap.IsAvailable(TimeStamp) Then
                    Return sim.BloodTap.Use(TimeStamp)
                Else
                    If retry = 0 Then Return True
                End If
            Case "BoneShield"
                If sim.BoneShield.IsAvailable(TimeStamp) Then
                    Return sim.BoneShield.Use(TimeStamp)
                Else
                    If retry = 0 Then Return True
                End If
            Case "GhoulFrenzy"
                If sim.Frenzy.IsFrenzyAvailable(TimeStamp) Then
                    Return sim.Frenzy.Frenzy(TimeStamp)
                Else
                    If retry = 0 Then Return True
                End If
            Case "ScourgeStrike"
                If Runes.FU(TimeStamp) = True Then
                    Return sim.ScourgeStrike.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("SS")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "PlagueStrike"
                If Runes.Unholy(TimeStamp) Then
                    Return sim.PlagueStrike.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("PS")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "Obliterate"
                If Runes.FU(TimeStamp) = True Then
                    Return sim.Obliterate.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("OB")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "EmpowerRuneWeapon"
                If sim.ERW.CD <= TimeStamp Then
                    Return sim.ERW.Use(TimeStamp)
                End If
            Case "FrostStrike"
                If sim.FrostStrike.isAvailable(TimeStamp) = True Then

                    Return sim.FrostStrike.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("FS")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "DeathStrike"
                If Runes.FU(TimeStamp) Then

                    Return sim.DeathStrike.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("BS")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "BloodStrike"
                If Runes.AnyBlood(TimeStamp) Then
                    If sim.BoneShieldUsageStyle = 1 Then
                        If sim.BoneShield.IsAvailable(TimeStamp) Then
                            sim.BoneShield.Use(TimeStamp)
                            Return True
                        End If
                        If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
                            sim.UnbreakableArmor.Use(TimeStamp)
                            Return True
                        End If
                    End If
                    Return sim.BloodStrike.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("BS")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "HeartStrike"
                If Runes.AnyBlood(TimeStamp) = True Then

                    Return sim.HeartStrike.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("HS")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "BloodPlague"

                If sim.Targets.MainTarget.BloodPlague.isActive(TimeStamp + 150) = False And Runes.Unholy(TimeStamp) = True Then
                    Return sim.PlagueStrike.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("PS")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "IcyTouch"
                If Runes.Frost(TimeStamp) = True Then

                    Return sim.IcyTouch.ApplyDamage(TimeStamp)
                    'Diagnostics.Debug.WriteLine("IT")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "DeathCoil"
                If sim.DeathCoil.isAvailable(TimeStamp) = True Then

                    Return sim.DeathCoil.ApplyDamage(TimeStamp, False)
                    'Diagnostics.Debug.WriteLine("DC")
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "BloodBoil"
                If Runes.AnyBlood(TimeStamp) = True Then
                    If sim.BoneShieldUsageStyle = 3 Then
                        If sim.BoneShield.IsAvailable(TimeStamp) Then
                            sim.BoneShield.Use(TimeStamp)
                            Return True
                        End If
                        If sim.UnbreakableArmor.IsAvailable(TimeStamp) Then
                            sim.UnbreakableArmor.Use(TimeStamp)
                            Return True
                        End If
                    End If

                    Return sim.BloodBoil.ApplyDamage(TimeStamp)
                    Exit Function
                Else
                    If retry = 0 Then Return True
                End If
            Case "HowlingBlast"
                If sim.HowlingBlast.isAvailable(TimeStamp) Then
                    If sim.proc.Rime.IsActive Or Runes.FU(TimeStamp) Then
                        Runes.UnReserveFU(TimeStamp)
                        Return sim.HowlingBlast.ApplyDamage(TimeStamp)
                        Exit Function
                    Else
                        Runes.ReserveFU(TimeStamp)
                    End If
                Else
                    If retry = 0 Then Return True
                End If
            Case "DeathandDecay"
                If sim.DeathandDecay.isAvailable(TimeStamp) Then
                    Return sim.DeathandDecay.Apply(TimeStamp)
                End If
            Case "Pestilence"
                If Runes.AnyBlood(TimeStamp) Then
                    Return sim.Pestilence.use(TimeStamp)
                Else
                    If retry = 0 Then Return True
                End If
            Case "Horn"
                If sim.Horn.isAvailable(TimeStamp) Then
                    Return sim.Horn.use(TimeStamp)
                Else
                    If retry = 0 Then Return True
                End If
            Case "BloodPresence"
                If sim.BloodPresenceSwitch.IsAvailable(TimeStamp) Then
                    Return sim.BloodPresenceSwitch.Use(TimeStamp)
                Else
                    If retry = 0 Then Return True
                End If
            Case "FrostPresence"
                If sim.FrostPresenceSwitch.IsAvailable(TimeStamp) Then
                    Return sim.FrostPresenceSwitch.Use(TimeStamp)
                Else
                    If retry = 0 Then Return True
                End If
            Case "UnholyPresence"
                If sim.UnholyPresenceSwitch.IsAvailable(TimeStamp) Then
                    Return sim.UnholyPresenceSwitch.Use(TimeStamp)
                Else
                    If retry = 0 Then Return True
                End If
        End Select
        Return False
    End Function
    Sub DoIntro(ByVal TimeStamp As Long)
        Dim ret As Boolean
        If MyIntro.Count > 0 And IntroStep < MyIntro.Count Then
            ret = DoRoration(TimeStamp, MyIntro.Item(IntroStep + 1), XMLIntro.Element("//Intro/" & MyIntro.Item(IntroStep + 1)).Attribute("retry").Value)
            If ret = True Then IntroStep = IntroStep + 1
            Exit Sub
        Else
            IntroDone = True
        End If

    End Sub
    Sub LoadIntro()
        Dim nod As XElement
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & sim.IntroPath, FileMode.Open, isoStore)
                Dim XMLIntro As XDocument = XDocument.Load(isoStream)
                For Each nod In XMLIntro.Element("Intro").Elements
                    Try
                        MyIntro.Add(nod.Name, nod.Name.ToString)
                    Catch
                        MyIntro.Add(nod.Name)
                    End Try
                Next
                If MyIntro.Count = 0 Then IntroDone = True
            End Using
        End Using
    End Sub
End Class
