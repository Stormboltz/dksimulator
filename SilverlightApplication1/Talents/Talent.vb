Namespace Simulator.Character
    Public Class Talent
        Friend Name As String
        Friend Value As Integer
        Friend School As Talents.Schools
        Friend Max As Integer


        Sub New(ByVal Name As String, ByVal School As Talents.Schools, Optional ByVal Value As Integer = 0, Optional ByVal Max As Integer = 0)
            Me.Name = Name
            Me.Value = Value
            Me.School = School
            Me.Max = Max
        End Sub

    End Class
End Namespace
