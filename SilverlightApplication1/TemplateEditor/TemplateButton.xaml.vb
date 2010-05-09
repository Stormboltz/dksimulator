Imports System.Windows.Media.Imaging

Partial Public Class TemplateButton
    Inherits UserControl
    Protected MF As TemplateEditor

    Friend MaxValue As Integer
    Friend Value As Integer
    Friend School As String

    Sub SetName(ByVal s As String)
        Name = s
        Try
            Dim bmi As BitmapImage = New BitmapImage(New Uri("../images/" & Me.Name & "G.jpg", UriKind.Relative))
            Me.Image.Source = bmi
        Catch ex As Exception

        End Try
    End Sub
    Sub AddPoint()
        SetVal(Value + 1)
    End Sub
    Sub removePoint()
        SetVal(Value - 1)
    End Sub

    Sub SetVal(ByVal x As Integer)
        On Error Resume Next
        Value = x
        If Value > MaxValue Then Value = MaxValue
        If Value <= 0 Then
            Value = 0
            Dim bmi As BitmapImage = New BitmapImage(New Uri("../images/" & Me.Name & "G.jpg", UriKind.Relative))
            Me.Image.Source = bmi

        Else
            Dim bmi As BitmapImage = New BitmapImage(New Uri("../images/" & Me.Name & ".jpg", UriKind.Relative))
            Me.Image.Source = bmi
        End If
        Label.Content = Value & "/" & MaxValue
        MF.SetTalentPointnumber()
    End Sub

    Private Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal MForm As TemplateEditor, ByVal name As String)
        MF = MForm
        InitializeComponent()
        SetName(name)
    End Sub

    

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button.Click
        'System.Diagnostics.Debug.WriteLine("click me")
        AddPoint()
    End Sub

    Private Sub Button_MouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Button.MouseLeftButtonDown
        AddPoint()
    End Sub

    Private Sub Button_MouseRightButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Button.MouseRightButtonDown
        e.Handled = True
        removePoint()
    End Sub
End Class
