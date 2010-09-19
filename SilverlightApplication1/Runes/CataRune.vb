'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 11/04/2010
' Heure: 17:09
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.Runes
    Public Class CataRune
        Inherits WowObject

        Friend Value As Integer
        Friend reserved As Boolean
        Friend death As Boolean

        Friend Parent As RunePair
        Friend BTuntil As Long
        Friend MaxValue As Integer



        Sub New(ByVal S As Sim)
            MyBase.New(S)
            Me.reserved = False
            Me.death = False

            MaxValue = 100
            Me.Value = MaxValue
            sim = S

        End Sub


        Sub Use(ByVal T As Long, ByVal D As Boolean, ByVal Alf As Boolean)
            'If Alf Then
            '    Me.Value -= 50
            'Else
            '    Me.Value -= 100
            'End If

            'If Me.Value < 0 Then
            '    Diagnostics.Debug.WriteLine("RUNE USE ERROR")
            'End If
            Parent.Use(T, D, Alf)
            death = D
            If BTuntil > T Then death = True
            Sim.proc.tryT104PDPS(T)

        End Sub



        Function Available() As Boolean
            If Value >= 100 Then
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
            If Value >= 100 Then Return Sim.TimeStamp
            Dim d As Double
            d = ((100 - Value) * RunePerSecond() * 100 + Sim.TimeStamp)

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
            Sim.FutureEventManager.Add(Sim.TimeStamp, "Rune")
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
            tmp = (1 / 10) * sim.Character.Haste.Value
            tmp *= sim.Character.RuneRegeneration.Value

            Return tmp
        End Function

    End Class
End Namespace