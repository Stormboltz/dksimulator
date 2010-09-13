'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 11/04/2010
' Heure: 12:11
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class FutureEvent
	Friend T As Long
	Friend Ev As String
    Friend WowObj As Simulator.WowObjects.WowObject
    Sub New(ByVal Timestamp As Long, ByVal EventName As String, ByVal ReferenceObject As Simulator.WowObjects.WowObject)
        Me.T = Timestamp
        Me.Ev = EventName
        If Not (ReferenceObject Is Nothing) Then
            Me.WowObj = ReferenceObject
        End If
    End Sub
	
	
	
End Class
