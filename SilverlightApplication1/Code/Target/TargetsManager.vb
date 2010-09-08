'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/23/2010
' Heure: 12:48 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.Targets
    Public Class TargetsManager

        Friend AllTargets As New Collections.Generic.List(Of Target)
        Friend MainTarget As Target
        Friend CurrentTarget As Target

        Function Count() As Integer
            Return AllTargets.Count
        End Function

        Sub New(ByVal s As Sim)

        End Sub
        Sub KillEveryoneExceptMainTarget()
            Dim Tar As Target
            Do Until AllTargets.Count = 1
                For Each Tar In AllTargets
                    If Tar.Equals(MainTarget) = False Then
                        Tar.Kill()
                        GoTo nextone
                    End If

                Next
nextone:
            Loop
        End Sub

        Function IsFrostFeverOnAll(ByVal T As Long) As Boolean
            Dim Tar As Target
            For Each Tar In AllTargets
                If Tar.FrostFever.isActive(T) = False Then Return False
            Next
            Return True
        End Function
        Function IsBloodPlagueOnAll(ByVal T As Long) As Boolean
            Dim Tar As Target
            For Each Tar In AllTargets
                If Tar.BloodPlague.isActive(T) = False Then Return False
            Next
            Return True
        End Function

    End Class
End Namespace