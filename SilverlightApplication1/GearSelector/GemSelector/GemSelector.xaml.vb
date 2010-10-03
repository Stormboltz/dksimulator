Imports System.Xml.Linq
Imports System.Linq

Partial Public Class GemSelector
    Inherits ChildWindow

    Friend gemDB As New XDocument
    Friend GemList As New List(Of mGem)
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

    End Sub


    Sub ListView1SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        SelectedItem = sender.SelectedItems.Item(0).subitems.item(1).text
        Me.Close()
    End Sub


    Sub LoadItem(ByVal GemNum As Integer, ByVal slot As Integer, ByVal type As Integer)
        gemDB = MainFrame.GemDB

        If GemList.Count = 0 Then
            Dim g As mGem
            For Each e In gemDB.Element("gems").Elements
                g = New mGem(e)
                g.EPVAlue = getItemEPValue(g)
                GemList.Add(g)
            Next
        End If

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
            statusReport = (From g In GemList
                            Where g.subclass = 6).ToList
        Else
            statusReport = (From g In GemList
                            Where g.reqskill = 0 Or
                            g.reqskill = GetSkillID(Me.MainFrame.ParentFrame.cmbSkill1.SelectedItem) Or
            g.reqskill = GetSkillID(Me.MainFrame.ParentFrame.cmbSkill2.SelectedItem)
                            Order By g.EPVAlue Descending).ToList
        End If
        gGems.AutoGenerateColumns = True
        Try
            gGems.ItemsSource = statusReport
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try

    End Sub

    Private Sub FilterList(ByVal filter As String)
        Dim statusReport As List(Of mGem)
        Dim doc As XDocument = MainFrame.GemDB
        If type = 1 Then
            statusReport = (From g In GemList
                            Where g.subclass = 6 And Contains(g, filter)
                            Order By g.EPVAlue Descending
                            ).ToList
        Else
            statusReport = (From g In GemList
                            Where Contains(g, filter) And (g.reqskill = 0 Or g.reqskill = GetSkillID(Me.MainFrame.ParentFrame.cmbSkill1.SelectedItem) Or g.reqskill = GetSkillID(Me.MainFrame.ParentFrame.cmbSkill2.SelectedItem))
                            Order By g.EPVAlue Descending
                            ).ToList
        End If
        gGems.AutoGenerateColumns = True
        gGems.ItemsSource = statusReport
        Me.type = type
    End Sub
    Sub ColorGrid()

    End Sub
    Private Function Contains(ByVal g As mGem, ByVal filter As String) As Boolean
        Dim tmp As String
        Dim tBool As Boolean = True
        tmp = g.name & " " & _
            g.Str & " " & _
            g.Agi & " " & _
            g.Haste & " " & _
            g.Exp & " " & _
            g.Hit & " " & _
            g.AP & " " & _
            g.Crit & " " & _
            g.ArP & " " & _
            g.Mast & " " & _
            g.Dodge & " " & _
            g.Parry & " " & _
            g.keywords

        For Each s In filter.Split(" ")
            If tmp.ToUpper.Contains(s.ToUpper) = False Then
                tBool = False
            End If
        Next
        Return tBool
    End Function

    Sub GearSelectorLoad(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Sub GearSelectorClose(ByVal sender As Object, ByVal e As EventArgs)
        If SelectedItem <> "" Then Me.DialogResult = True
    End Sub



    Function getItemEPValue(ByVal gem As mGem) As Double
        Dim tmp As Double = 0
        tmp += gem.Str * MainFrame.EPvalues.Str
        tmp += gem.Agi * MainFrame.EPvalues.Agility
        tmp += gem.Exp * MainFrame.EPvalues.Exp
        tmp += gem.Hit * MainFrame.EPvalues.Hit
        tmp += gem.AP
        tmp += gem.Crit * MainFrame.EPvalues.Crit
        tmp += gem.ArP * MainFrame.EPvalues.ArP
        tmp += gem.Haste * MainFrame.EPvalues.Haste
        tmp += gem.Mast * MainFrame.EPvalues.Mastery
        Return tmp
    End Function


 
    
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
        Property Mast As Integer
        Property ArP As Integer
        Property EPVAlue As Integer
        Property Dodge As Integer
        Property Parry As Integer
        Friend ColorId As Integer
        Friend subclass As Integer
        Friend quality As Integer
        Friend reqskill As Integer
        Friend keywords As String
  
        Sub New(ByVal el As XElement)
            Try


                Id = Convert.ToInt32(el.Element("id").Value)
                With Me
                    .name = el.Element("name").Value
                    .Str = el.Element("Strength").Value
                    .Agi = el.Element("Agility").Value
                    .Haste = el.Element("HasteRating").Value
                    .Exp = el.Element("ExpertiseRating").Value
                    .Hit = el.Element("HitRating").Value
                    .AP = el.Element("AttackPower").Value
                    .Crit = el.Element("CritRating").Value
                    .ArP = el.Element("ArmorPenetrationRating").Value
                    .Mast = el.Element("Mastery").Value
                    .ColorId = el.Element("subclass").Value
                    .Dodge = el.Element("Dodge").Value
                    .Parry = el.Element("Parry").Value
                    .subclass = el.Element("subclass").Value
                    .quality = el.Element("quality").Value
                    .reqskill = el.Element("reqskill").Value
                    .keywords = el.Element("keywords").Value
                End With
            Catch ex As Exception

            End Try
        End Sub


    End Class


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

    Private Sub NoneButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles NoneButton.Click
        SelectedItem = 0
        Me.DialogResult = True
    End Sub
End Class
