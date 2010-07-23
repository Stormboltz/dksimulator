Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class TemplateEditor
    Inherits ChildWindow
    Friend FilePath As String
    Dim WithEvents UI As New UserInput


    Private Sub UI_closeEvent() Handles UI.Closing

        If UI.DialogResult Then
            If UI.txtInput.Text <> "" Then
                FilePath = UI.txtInput.Text & ".xml"
                SaveTemplate(FilePath)
                Me.DialogResult = True
                'Me.Close()
            End If
        End If
    End Sub
    Friend btList As New Collection
    Public Sub New()
        InitializeComponent()
        CreateTreeTemplate()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        SaveTemplate("")
        Me.DialogResult = True
        'UI.Close()
        'Me.Close()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
        'UI.Close()
        'Me.Close()
    End Sub
    Private Sub cmdSaveAs_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdSaveAs.Click
        UI.Show()
    End Sub
    Sub CreateTreeTemplate()

        Dim XmlDoc As XDocument = XDocument.Load("config/template.xml")
        Dim xNode As XElement
        Dim myBT As TemplateButton
        btList.Clear()
        For Each xNode In XmlDoc.Element("Talents").Element("blood").Elements
            If xNode.Name.ToString = "include" Then GoTo NextBlood
            myBT = New TemplateButton(Me, xNode.Name.ToString)
            myBT.School = "blood"
            Me.tbTpl.Children.Add(myBT)
            Canvas.SetLeft(myBT, -10 + (xNode.Attribute("col")).Value * 35)
            Canvas.SetTop(myBT, -35 + xNode.Attribute("row").Value * 50)
            myBT.MaxValue = xNode.Value
            btList.Add(myBT, myBT.Name)
            ToolTipService.SetToolTip(myBT, myBT.Name)
NextBlood:

        Next



        For Each xNode In XmlDoc.Element("Talents").Element("frost").Elements
            If xNode.Name.ToString = "include" Then GoTo NextFrost
            myBT = New TemplateButton(Me, xNode.Name.ToString)
            myBT.School = "frost"
            Me.tbTpl.Children.Add(myBT)
            Canvas.SetLeft(myBT, 140 + (xNode.Attribute("col")).Value * 35)
            Canvas.SetTop(myBT, -35 + xNode.Attribute("row").Value * 50)
            myBT.MaxValue = xNode.Value
            btList.Add(myBT, myBT.Name)
            ToolTipService.SetToolTip(myBT, myBT.Name)
NextFrost:
        Next

        For Each xNode In XmlDoc.Element("Talents").Element("unholy").Elements
            If xNode.Name.ToString = "include" Then GoTo NextUnholy
            myBT = New TemplateButton(Me, xNode.Name.ToString)
            myBT.School = "unholy"
            Me.tbTpl.Children.Add(myBT)
            Canvas.SetLeft(myBT, 300 + (xNode.Attribute("col")).Value * 35)
            Canvas.SetTop(myBT, -35 + xNode.Attribute("row").Value * 50)
            myBT.MaxValue = xNode.Value
            btList.Add(myBT, myBT.Name)
            ToolTipService.SetToolTip(myBT, myBT.Name)
NextUnholy:
        Next


        For Each xNode In XmlDoc.Element("Talents").Element("Glyphs").Elements
            cmbGlyph1.Items.Add(xNode.Name)
            cmbGlyph2.Items.Add(xNode.Name)
            cmbGlyph3.Items.Add(xNode.Name)
        Next


    End Sub
    Sub SetTalentPointnumber()
        Dim BT As TemplateButton
        Dim b As Integer
        Dim f As Integer
        Dim u As Integer


        For Each BT In btList
            Select Case BT.School
                Case "blood"
                    b = b + BT.Value
                Case "frost"
                    f = f + BT.Value
                Case "unholy"
                    u = u + BT.Value
            End Select
        Next
        lblBlood.Content = b
        lblFrost.Content = f
        lblUnholy.Content = u
    End Sub

    Sub DisplayTemplateInEditor(ByVal path As String)
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim i As Integer
            FilePath = path
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/templates/" & path, FileMode.Open, isoStore)
                Dim xmlDoc As XDocument = XDocument.Load(isoStream)

                For Each BT As TemplateButton In btList
                    Try
                        BT.SetVal(xmlDoc.Element("Talents").Element(BT.Name).Value)
                    Catch
                    End Try

                Next
                For Each XNode As XElement In xmlDoc.Element("Talents").Element("Glyphs").Elements
                    If XNode.Value = 1 Then
                        Select Case i
                            Case 0
                                cmbGlyph1.SelectedItem = XNode.Name
                            Case 1
                                cmbGlyph2.SelectedItem = XNode.Name
                            Case 2
                                cmbGlyph3.SelectedItem = XNode.Name
                        End Select
                        i = i + 1
                    End If
                Next
            End Using
        End Using





    End Sub
    Sub SaveTemplate(ByVal path As String)
        If path = "" Then path = Me.FilePath
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/templates/" & path, FileMode.Create, isoStore)
                Dim doc As XDocument = XDocument.Parse("<Talents><Glyphs/></Talents>")

                For Each ctr As Control In Me.tbTpl.Children
                    If TypeOf ctr Is TemplateButton Then
                        Dim tb As TemplateButton = ctr
                        Dim xEl As XElement = XElement.Parse("<" & tb.Name & "/>")
                        xEl.SetValue(tb.Value)
                        doc.Element("Talents").Add(xEl)
                    End If
                Next
                Dim xGlyph As XDocument = XDocument.Load("config/template.xml")
                For Each x As XElement In xGlyph.Element("Talents").Element("Glyphs").Elements
                    Dim xEl As XElement = XElement.Parse("<" & x.Name.ToString & "/>")
                    If x.Name = cmbGlyph1.SelectedValue Or x.Name = cmbGlyph2.SelectedValue Or x.Name = cmbGlyph3.SelectedValue Then
                        xEl.SetValue(1)
                    Else
                        xEl.SetValue(0)
                    End If
                    doc.Element("Talents").Element("Glyphs").Add(xEl)
                Next
                doc.Save(isoStream)
            End Using
        End Using
    End Sub

    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdPreview.Click
        Dim oldPath As String = FilePath
        SaveTemplate("tempo.xml")
        Dim tmpPath As String
        tmpPath = ("KahoDKSim/templates/tempo.xml")
        Dim txtEditor As New TextEditor
        txtEditor.OpenFileFromISO(tmpPath)
        txtEditor.Show()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            isoStore.DeleteFile(tmpPath)
        End Using
        FilePath = oldPath

    End Sub
End Class
