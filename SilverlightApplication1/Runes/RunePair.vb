Namespace Simulator.WowObjects.Runes
    Public Class RunePair

        Dim Rune1 As CataRune
        Dim Rune2 As CataRune

        Dim RuneToRefill As CataRune
        

        Sub New(ByVal r1 As CataRune, ByVal r2 As CataRune)
            Rune1 = r1
            Rune2 = r2
            Rune1.Parent = Me
            Rune2.Parent = Me
            RuneToRefill = Rune1
        End Sub

        Sub Use(ByVal T As Long, ByVal ToDeath As Boolean)
            Dim cost As Integer
            cost = 100
            If Rune1.death Then
                If Rune2.Value = 100 Then
                    Rune2.Value = 0
                    Rune2.death = ToDeath
                    RuneToRefill = Rune1
                ElseIf Rune1.Value = 100 Then
                    Rune1.Value = 0
                    Rune1.death = ToDeath
                    RuneToRefill = Rune2
                Else
                    RuneToRefill = OppositeRune(RuneToRefill)
                    Diagnostics.Debug.WriteLine("Negative Rune")
                End If
            ElseIf Rune2.death Then
                If Rune1.Value = 100 Then
                    Rune1.Value = 0
                    Rune1.death = ToDeath
                    RuneToRefill = Rune2
                ElseIf Rune2.Value = 100 Then
                    Rune2.Value = 0
                    Rune2.death = ToDeath
                    RuneToRefill = Rune1
                Else
                    RuneToRefill = OppositeRune(RuneToRefill)
                    Diagnostics.Debug.WriteLine("Negative Rune")
                End If
            Else
                If Rune1.Value = 100 Then
                    Rune1.Value = 0
                    Rune1.death = ToDeath
                    RuneToRefill = Rune2
                ElseIf Rune2.Value = 100 Then
                    Rune2.Value = 0
                    Rune2.death = ToDeath
                    RuneToRefill = Rune1
                Else
                    RuneToRefill = OppositeRune(RuneToRefill)
                    Diagnostics.Debug.WriteLine("Negative Rune")
                End If

            End If
          
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
            Dim r As CataRune = RuneToRefill

            tmp = Rune1.RunePerSecond() * second * 100
            r.Value += tmp
            If r.Value > 100 Then
                Dim d As Double
                d = r.Value - 100
                r.Value = 100
                RuneToRefill = OppositeRune(r)
                RuneToRefill.Value = Math.Min(RuneToRefill.Value + d, 100)
                RuneToRefill = RuneToRefill
            End If
            If Rune1.Value = 100 Or Rune2.Value = 100 Then Rune1.AddEventInManager()
        End Sub
        Function RuneToRefill_deprecated() As CataRune
            If Rune1.Value >= 100 Then
                Return Rune2
            ElseIf Rune2.Value >= 100 Then
                Return Rune1
            ElseIf Rune1.Value >= Rune2.Value Then
                Return Rune1
            Else
                Return Rune2
            End If
        End Function
        Function OppositeRune(ByVal rune As CataRune)
            If rune.Equals(Rune1) Then
                Return Rune2
            Else
                Return Rune1
            End If
        End Function
    End Class
End Namespace