﻿'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/9/2009
' Heure: 2:12 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Procs
    Public Class Rime
        Inherits Proc
        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub

        Overrides Sub ApplyMe(ByVal T As Long)
            If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & Me.ToString & " proc")
            Fade = T + ProcLenght * 100
            CurrentStack = 1
            HitCount += 1
        End Sub

    End Class
End Namespace
