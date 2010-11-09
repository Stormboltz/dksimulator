'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/23/2010
' Heure: 11:37 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Imports DKSIMVB.Simulator.WowObjects.Diseases

Namespace Simulator.Targets
    Public Class Target
        Friend Debuff As Debuff
        Protected sim As Sim
        Friend BloodPlague As BloodPlague
        Friend FrostFever As FrostFever
        Friend EbonPlaguebringer As Integer = -1





        Sub New(ByVal s As Sim)
            sim = s
            Debuff = New Debuff(s)
            If sim.Targets Is Nothing Then
                sim.Targets = New Targets.TargetsManager(sim)
            End If
            sim.Targets.AllTargets.Add(Me)
            BloodPlague = New BloodPlague(sim)
            FrostFever = New FrostFever(sim)
        End Sub

        Sub Unbuff()
            Debuff.Unbuff()
        End Sub
        Sub Kill()
            sim.Targets.AllTargets.Remove(Me)
            Me.Finalize()
        End Sub

        Function NumDisease() As Integer
            Dim tmp As Integer
            tmp = 0
            If BloodPlague.isActive(sim.TimeStamp) Then tmp = tmp + 1
            If FrostFever.isActive(sim.TimeStamp) Then tmp = tmp + 1

            If EbonPlaguebringer = -1 Then
                If sim.Character.Talents.Talent("EbonPlaguebringer").Value >= 1 Then
                    EbonPlaguebringer = 1
                Else
                    EbonPlaguebringer = 0
                End If
            End If
            If (EbonPlaguebringer = 1) And tmp >= 1 Then tmp = tmp + 1
            Return tmp
        End Function


    End Class
End Namespace