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

        Function NumDesease() As Integer
            NumDesease = 0
            If BloodPlague.isActive(sim.TimeStamp) Then NumDesease = NumDesease + 1
            If FrostFever.isActive(sim.TimeStamp) Then NumDesease = NumDesease + 1
            If (sim.Character.Talents.Talent("EbonPlaguebringer").Value >= 1) And NumDesease >= 1 Then NumDesease = NumDesease + 1
        End Function


    End Class
End Namespace