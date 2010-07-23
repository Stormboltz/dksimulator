'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 11/04/2010
' Heure: 17:09
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class CataRune
    Inherits Supertype

    Friend Value As Integer
    Friend reserved As Boolean
    Friend death As Boolean

    Friend Parent As RunePair
    Friend BTuntil As Long



    Sub New(ByVal S As Sim)
        Me.reserved = False
        Me.death = False
        Me.Value = 100
        Sim = S
    End Sub


    Sub Use(ByVal T As Long, ByVal D As Boolean)
        Parent.Use(T, D)
        death = D
        If BTuntil > T Then death = True
        sim.proc.tryT104PDPS(T)

    End Sub


    Function Available() As Boolean
        If Value = 100 Then
            Return True
        Else
            Return False
        End If
    End Function

    Function Available(ByVal T As Long) As Boolean
        If T >= AvailableTime() Then
            Return True
        Else
            Return False
        End If
    End Function

    'Function NextAvailableTime() As Long
    '    'Dim tmp As Long
    '    If Value = 100 Then Return Sim.TimeStamp
    '    Return ((100 - Value) * Parent.RunePerSecond + Sim.TimeStamp)
    'End Function

    Function AvailableTime() As Long
        'Dim tmp As Long
        If Value >= 100 Then Return sim.TimeStamp
        Dim d As Double
        d = ((100 - Value) * RunePerSecond() * 100 + sim.TimeStamp)

        Return Convert.ToInt64(d + 1)
    End Function

    Sub Activate()
        Value = 100
    End Sub

    Sub Reset()
        death = False
        reserved = False
        Value = 100
    End Sub
    Sub AddEventInManager()
        sim.FutureEventManager.Add(sim.TimeStamp, "Rune")
    End Sub


    Function Death_Report() As String
        If death Then
            Return "D"
        Else
            Return ""
        End If
    End Function
    Function RunePerSecond() As Double 'Rune fraction per second
        'withoutHaste 10s per rune 
        Dim tmp As Double
        'tmp = (1 / 10)
        tmp = (1 / 10) * sim.MainStat.Haste
        If Sim.UnholyPresence = 1 Then
            tmp = tmp * (1.1 + sim.Character.Talents.Talent("ImprovedUnholyPresence").Value * 2.5 / 100)
        End If

        Return tmp
    End Function

End Class
