Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class ScenarioEditor
    Inherits ChildWindow
    Protected Friend EditorFilePAth As String
    Dim WithEvents UI As New UserInput
    Protected MainForm As MainForm


    Public Sub New(ByVal m As MainForm)
        MainForm = m
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub
    Sub BtOkClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.DialogResult = True
    End Sub


    Sub LoadAvailableScenario()
        Dim btn As ScenarioButton
        Dim xDoc As XDocument = XDocument.Load("config/Scenarios.xml")
        For Each xNode In xDoc.Element("Scenarios").Elements("Element")
            btn = New ScenarioButton(Me)
            grpAvailableScenario.Children.Add(btn)
            btn.build(xNode)
            btn.buttonAdd.Opacity = 1
            btn.buttonRemove.Opacity = 0
            btn.buttonUp.Opacity = 0
            btn.buttonDown.Opacity = 0
        Next
    End Sub

    Sub OpenForEdit(ByVal FilePath As String)
        EditorFilePAth = FilePath

        Dim doc As XDocument
        LoadAvailableScenario()
        'Exit Sub
        grpCurrentScenario.Children.Clear()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Scenario/" & FilePath, FileMode.Open, isoStore)
                EditorFilePAth = FilePath
                doc = XDocument.Load(isoStream)
                Dim Node As XElement
                Dim btn As ScenarioButton
                Dim i As Integer

                For Each Node In doc.Element("Scenario").Elements
                    btn = New ScenarioButton(Me)
                    Try
                        grpCurrentScenario.Children.Add(btn)
                    Catch ex As Exception

                    End Try
                    btn.build(Node)
                    btn.buttonAdd.Opacity = 0
                    btn.buttonRemove.Opacity = 1
                    btn.buttonUp.Opacity = 1
                    btn.buttonDown.Opacity = 1
                    btn.number = i
                    i += 1
                Next
            End Using
        End Using
    End Sub

    Sub MoveUp(ByVal s As ScenarioButton)
        Dim p As ScenarioButton
        Try
            p = grpCurrentScenario.Children.Item(s.number - 1)
            grpCurrentScenario.Children.Remove(s)
            grpCurrentScenario.Children.Insert(s.number - 1, s)
            ReNumberCurrentScenario
        Catch ex As Exception

        End Try
    End Sub

    Sub MoveDown(ByVal s As ScenarioButton)
        
        Dim p As ScenarioButton
        Try
            p = grpCurrentScenario.Children.Item(s.number + 1)
            grpCurrentScenario.Children.Remove(s)
            grpCurrentScenario.Children.Insert(s.number + 1, s)
           ReNumberCurrentScenario
        Catch ex As Exception

        End Try

    End Sub

    Sub RemoveElement(ByVal s As ScenarioButton)
        grpCurrentScenario.Children.Remove(s)
        ReNumberCurrentScenario()
    End Sub

    Sub ReNumberCurrentScenario()

        For i As Integer = 0 To grpCurrentScenario.Children.Count - 1
            CType(grpCurrentScenario.Children.Item(i), ScenarioButton).number = i
        Next



      
    End Sub


    Sub AddElement(ByVal s As ScenarioButton)
        Dim i As Integer
        i = Me.grpCurrentScenario.Children.Count
        Dim btn As New ScenarioButton(Me)
        grpCurrentScenario.Children.Add(btn)
        btn.build(s.XmlBuid)
        btn.SetName(s.myName)
        btn.buttonAdd.Opacity = 0
        btn.buttonRemove.Opacity = 1
        btn.buttonUp.Opacity = 1
        btn.buttonDown.Opacity = 1
        btn.number = i
    End Sub


    Sub SaveScenario()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Scenario/" & EditorFilePAth, FileMode.Create, isoStore)
                Dim xmlDoc As XDocument
                Dim xelem As XElement


                xmlDoc = XDocument.Parse("<Scenario></Scenario>")
                Dim i As Integer
                Dim p As ScenarioButton
                Dim tmp As String

                For Each p In grpCurrentScenario.Children
                    tmp = "<Element id='" & i & "' caption='" & p.myName & "'>"
                    Dim ctr As Control
                    Dim chk As CheckBox
                    Dim txt As myTextButton
                    For Each ctr In p.colControl
                        If TypeOf ctr Is CheckBox Then
                            chk = ctr
                            tmp &= "<" & chk.Name & " type='checkbox' caption='" & chk.Content.ToString & "'>"
                            tmp &= chk.IsChecked
                            tmp &= "</" & chk.Name & ">"
                        End If

                        If TypeOf ctr Is myTextButton Then
                            txt = ctr
                            tmp &= "<" & txt.Name & " type='textbox' caption='" & txt.caption.ToString & "' multi='" & txt.multi & " '>"
                            tmp &= txt.Text.Text
                            tmp &= "</" & txt.Name & ">"
                        End If
                    Next
                    tmp &= "</Element>"
                    xelem = XElement.Parse(tmp)
                    xmlDoc.Element("Scenario").Add(xelem)
                    i += 1
                Next
                xmlDoc.Save(isoStream)
            End Using
        End Using
    End Sub

    Sub Userinput_close() Handles UI.Closing
        If UI.DialogResult Then
            EditorFilePAth = UI.txtInput.Text & ".xml"
            SaveScenario()
            Me.DialogResult = True
        End If
    End Sub


    Private Sub cmdSaveAs_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdSaveAs.Click
        UI.Show()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdSave.Click
        SaveScenario()
        Me.DialogResult = True
    End Sub

    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdPreview.Click
        Dim oldPath As String = EditorFilePAth
        EditorFilePAth = "tempo.xml"
        SaveScenario()
        Dim tmpPath As String
        tmpPath = ("KahoDKSim/Scenario/tempo.xml")
        Dim txtEditor As New myTextReader
        txtEditor.OpenFileFromISO(tmpPath)
        txtEditor.Show()
        Dim isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        isoStore.DeleteFile(tmpPath)
        isoStore.Dispose()
        EditorFilePAth = oldPath
    End Sub


End Class


