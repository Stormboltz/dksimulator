Partial Public Class EPDisplay
    Inherits ChildWindow

    Private ParentFrame As GearSelectorMainForm

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub

    Friend Sub New(ByVal ParentF As GearSelectorMainForm)
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        ParentFrame = ParentF
        Me.InitializeComponent()


    End Sub

    Sub EPDisplayLoad(ByVal sender As Object, ByVal e As EventArgs)
        Dim GS As GearSelectorMainForm
        GS = ParentFrame
        Dim EPVal As EPValues
        'EPVal = GS.ParentFrame.EPVal
        'TODO REWTRIE THIS
        'gdEPValues.Items.Add("Agility").SubItems.Add(EPVal.Agility)
        'gdEPValues.Items.Add("Armor").SubItems.Add(EPVal.Armor)
        'gdEPValues.Items.Add("ArP").SubItems.Add(EPVal.ArP)
        'gdEPValues.Items.Add("Crit").SubItems.Add(EPVal.Crit)
        'gdEPValues.Items.Add("Exp").SubItems.Add(EPVal.Exp)
        'gdEPValues.Items.Add("Haste").SubItems.Add(EPVal.Haste)
        'gdEPValues.Items.Add("Hit").SubItems.Add(EPVal.Hit)
        'gdEPValues.Items.Add("Weapon DPS").SubItems.Add(EPVal.MHDPS)
        'gdEPValues.Items.Add("Weapon speed").SubItems.Add(EPVal.MHSpeed)
        'gdEPValues.Items.Add("Str").SubItems.Add(EPVal.Str)

    End Sub
End Class
