Partial Public Class EPDisplay
    Inherits ChildWindow

    Private ParentFrame As FrmGearSelector

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub

    Friend Sub New(ByVal ParentF As FrmGearSelector)
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        ParentFrame = ParentF
        Me.InitializeComponent()


    End Sub

    Sub EPDisplayLoad(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Loaded
        Dim GS As FrmGearSelector
        GS = ParentFrame
        Dim EPVal As EPValues
        EPVal = GS.ParentFrame.EPVal
        Dim tbl As New List(Of EPValue)
        tbl.Add(New EPValue("Agility", EPVal.Agility))
        tbl.Add(New EPValue("Armor", EPVal.Armor))
        tbl.Add(New EPValue("ArP", EPVal.ArP))

        tbl.Add(New EPValue("Crit", EPVal.Exp))
        tbl.Add(New EPValue("Exp", EPVal.Exp))
        tbl.Add(New EPValue("Haste", EPVal.Haste))
        tbl.Add(New EPValue("Hit", EPVal.Hit))
        tbl.Add(New EPValue("Weapon DPS", EPVal.MHDPS))
        tbl.Add(New EPValue("Weapon speed", EPVal.MHSpeed))
        tbl.Add(New EPValue("Str", EPVal.Str))
        gdEPValues.ItemsSource = tbl

    End Sub

    Class EPValue
        Property Name As String
        Property Value As String

        Sub New(ByVal sName As String, ByVal sValue As String)
            Name = sName
            Value = sValue
        End Sub

    End Class

End Class
