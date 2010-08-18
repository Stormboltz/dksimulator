Imports System.Xml.Linq
Imports System.Linq

Partial Public Class GemSelector
    Inherits ChildWindow

    Friend gemDB As New XDocument
    Friend Slot As String
    Friend type As Integer
    Friend SelectedItem As String
    Friend GemNum As String
    Friend MainFrame As FrmGearSelector
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub
    Public Sub New(ByVal M As FrmGearSelector)

        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()

        MainFrame = M
        gemDB = M.GemDB

        '
        ' TODO : Add constructor code after InitializeComponents
        '


    End Sub


    Sub ListView1SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        SelectedItem = sender.SelectedItems.Item(0).subitems.item(1).text
        Me.Close()
    End Sub
    

    Sub LoadItem(ByVal GemNum As Integer, ByVal slot As Integer, ByVal type As Integer)
        Me.GemNum = GemNum
        Me.type = type
        Me.Slot = slot


        If txtFilter.Text.Trim <> "" Then
            FilterList(txtFilter.Text)
            Return
        End If



        Dim statusReport As List(Of mGem)
        Dim doc As XDocument = MainFrame.GemDB
        If type = 1 Then
            statusReport = (From el In doc.Element("gems").Elements Where el.Element("subclass").Value = 6 Select aGem(el)).ToList()
        Else
            statusReport = (From el In doc.Element("gems").Elements
                            Where (el.Element("quality").Value = 4 And (el.Element("reqskill").Value = 0 Or el.Element("reqskill").Value = GetSkillID(Me.MainFrame.ParentFrame.cmbSkill1.SelectedItem) Or el.Element("reqskill").Value = GetSkillID(Me.MainFrame.ParentFrame.cmbSkill2.SelectedItem)))
                            Order By getItemEPValue(el) Descending
                            Select aGem(el)).ToList()
        End If
        gGems.AutoGenerateColumns = True
        Try
            gGems.ItemsSource = statusReport
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try

    End Sub

    Sub TextBox1TextChanged(ByVal sender As TextBox, ByVal e As EventArgs)
        
    End Sub

    Private Sub FilterList(ByVal filter As String)
        Dim statusReport As List(Of mGem)
        Dim doc As XDocument = MainFrame.GemDB
        If type = 1 Then
            statusReport = (From el In doc.Element("gems").Elements
                            Where el.Element("subclass").Value = 6 And Contains(el, filter)
                            Order By getItemEPValue(el) Descending
                            Select aGem(el)).ToList()
        Else
            statusReport = (From el In doc.Element("gems").Elements
                            Where (el.Element("quality").Value = 4 And Contains(el, filter) And (el.Element("reqskill").Value = 0 Or el.Element("reqskill").Value = GetSkillID(Me.MainFrame.ParentFrame.cmbSkill1.SelectedItem) Or el.Element("reqskill").Value = GetSkillID(Me.MainFrame.ParentFrame.cmbSkill2.SelectedItem)))
                            Order By getItemEPValue(el) Descending
                            Select aGem(el)).ToList()
        End If
        gGems.AutoGenerateColumns = True
        gGems.ItemsSource = statusReport
        Me.type = type
    End Sub
    Sub ColorGrid()
        
    End Sub
    Private Function Contains(ByVal el As XElement, ByVal filter As String) As Boolean
        Dim tmp As String
        Dim tBool As Boolean = True
        tmp = el.Element("name").Value & " " & _
            el.Element("Strength").Value & " " & _
            el.Element("Agility").Value & " " & _
            el.Element("HasteRating").Value & " " & _
            el.Element("ExpertiseRating").Value & " " & _
            el.Element("HitRating").Value & " " & _
            el.Element("AttackPower").Value & " " & _
            el.Element("CritRating").Value & " " & _
            el.Element("ArmorPenetrationRating").Value & " " & _
            el.Element("keywords").Value
        For Each s In filter.Split(" ")
            If tmp.ToUpper.Contains(s.ToUpper) = False Then
                tBool = False
            End If
        Next
        Return tBool
    End Function
    Function aGem(ByVal el As XElement) As mGem
        Dim myGem As New mGem
        myGem.Id = Convert.ToInt32(el.Element("id").Value)
        With myGem
            .name = el.Element("name").Value
            .Str = el.Element("Strength").Value
            .Agi = el.Element("Agility").Value
            .Haste = el.Element("HasteRating").Value
            .Exp = el.Element("ExpertiseRating").Value
            .Hit = el.Element("HitRating").Value
            .AP = el.Element("AttackPower").Value
            .Crit = el.Element("CritRating").Value
            .ArP = el.Element("ArmorPenetrationRating").Value
            .ColorId = el.Element("subclass").Value
            .EPVAlue = getItemEPValue(el)
        End With
        Return myGem
    End Function


    Sub GearSelectorLoad(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Sub GearSelectorClose(ByVal sender As Object, ByVal e As EventArgs)
        If SelectedItem <> "" Then Me.DialogResult = True
    End Sub

    Function getItemEPValue(ByVal txDoc As XDocument) As Double
        Dim tmp As Double = 0
        tmp += txDoc.Element("/item/Strength").Value * MainFrame.EPvalues.Str
        tmp += txDoc.Element("/item/Agility").Value * MainFrame.EPvalues.Agility
        tmp += txDoc.Element("/item/ExpertiseRating").Value * MainFrame.EPvalues.Exp
        tmp += txDoc.Element("/item/HitRating").Value * MainFrame.EPvalues.Hit
        tmp += txDoc.Element("/item/AttackPower").Value * 1
        tmp += txDoc.Element("/item/CritRating").Value * MainFrame.EPvalues.Crit
        tmp += txDoc.Element("/item/ArmorPenetrationRating").Value * MainFrame.EPvalues.ArP
        tmp += txDoc.Element("/item/HasteRating").Value * MainFrame.EPvalues.Haste



        Return tmp
    End Function
    Function getItemEPValue(ByVal el As XElement) As Integer
        Dim tmp As Double = 0

        tmp += el.Element("Strength").Value * MainFrame.EPvalues.Str
        tmp += el.Element("Agility").Value * MainFrame.EPvalues.Agility
        tmp += el.Element("ExpertiseRating").Value * MainFrame.EPvalues.Exp
        tmp += el.Element("HitRating").Value * MainFrame.EPvalues.Hit
        tmp += el.Element("AttackPower").Value * 1
        tmp += el.Element("CritRating").Value * MainFrame.EPvalues.Crit
        tmp += el.Element("ArmorPenetrationRating").Value * MainFrame.EPvalues.ArP
        tmp += el.Element("HasteRating").Value * MainFrame.EPvalues.Haste
        Return Convert.ToInt32(tmp)


    End Function
    Sub CmdClearClick(ByVal sender As Object, ByVal e As EventArgs)
        SelectedItem = 0
        Me.Close()
    End Sub
    Public Class mGem
        Friend Id As Integer
        Property name As String
        Property Str As Integer
        Property Agi As Integer
        Property Haste As Integer
        Property Exp As Integer
        Property Hit As Integer
        Property AP As Integer
        Property Crit As Integer
        Property ArP As Integer
        Property EPVAlue As Integer
        Friend ColorId As Integer
    End Class

    Private Sub gGems_AutoGeneratingColumn(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs) Handles gGems.AutoGeneratingColumn

    End Sub

    Private Sub gGems_BeginningEdit(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridBeginningEditEventArgs) Handles gGems.BeginningEdit
        Dim a As mGem
        If IsNothing(sender.selecteditem) Then Exit Sub
        a = sender.selecteditem

        Try

            SelectedItem = a.Id
            Me.DialogResult = True
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try

    End Sub

    Private Sub txtFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles txtFilter.TextChanged
        If sender.Text.Trim <> "" Then
            FilterList(sender.Text)
        Else
            LoadItem(GemNum, Me.Slot, Me.type)
        End If
    End Sub

    Private Sub gGems_LoadingRow(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridRowEventArgs) Handles gGems.LoadingRow
        Dim r As DataGridRow = e.Row
        Dim g As mGem = r.DataContext
        r.Background = New SolidColorBrush(GemColor(g.ColorId))
    End Sub
End Class
