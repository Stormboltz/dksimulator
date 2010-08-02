Public Class RunePair

    Dim Rune1 As CataRune
    Dim Rune2 As CataRune

    Sub New(ByVal r1 As CataRune, ByVal r2 As CataRune)
        Rune1 = r1
        Rune2 = r2
        Rune1.Parent = Me
        Rune2.Parent = Me


    End Sub

    Sub Use(ByVal T As Long, ByVal D As Boolean)
        If Rune2.Value > 0 Then
            Dim i As Integer
            i = Rune2.Value
            Rune2.Value = 0
            Rune1.Value = Rune1.Value - (100 - i)
        ElseIf Rune1.Value = 100 Then
            Rune1.Value = 0
        Else
            Diagnostics.Debug.WriteLine("Negative Rune")
        End If
        If Rune1.Value + Rune2.Value < 0 Then Diagnostics.Debug.WriteLine("Negative Rune")

    End Sub

    Function Available() As Boolean
        If Rune1.Value >= 100 Or Rune2.Value >= 100 Then
            Return True
        Else
            Return False
        End If
    End Function

    Function AvailableTwice() As Boolean
        If Rune1.Value >= 100 And Rune2.Value >= 100 Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub Refill(ByVal second As Double)
        Dim tmp As Double
        tmp = Rune1.RunePerSecond() * second * 100
        Rune1.Value += tmp
        If Rune1.Value > Rune1.MaxValue Then
            Dim d As Double
            d = Rune1.Value - Rune1.MaxValue
            Rune1.Value = Rune1.MaxValue
            Rune2.Value = Math.Min(Rune2.Value + d, Rune2.MaxValue)
        End If
        If Rune1.Value = Rune1.MaxValue Or Rune2.Value = Rune2.MaxValue Then Rune1.AddEventInManager()
    End Sub
End Class
