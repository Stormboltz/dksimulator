'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/08/2009
' Heure: 21:30
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Procs
    Public Class Trinket
        Inherits WeaponProc

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            Sim = S
            S.TrinketsCollection.Add(Me)
        End Sub
    End Class
End Namespace
