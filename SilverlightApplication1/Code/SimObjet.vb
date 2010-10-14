Namespace Simulator
    Public Class SimObjet
        Friend sim As Simulator.Sim
        Friend TimeWasted As TimeWastedAnaliser.TimeWasted
        Friend _Name As String
        Sub New(ByVal s As Simulator.Sim)
            sim = s
            TimeWasted = New TimeWastedAnaliser.TimeWasted(Me)

        End Sub
        Public Overridable Function Name() As String

            If _Name <> "" Then Return _Name
            Dim t As String = ShortenName(Me.ToString)

            Return t
        End Function
        Overridable Sub SoftReset()

        End Sub
    End Class
End Namespace
