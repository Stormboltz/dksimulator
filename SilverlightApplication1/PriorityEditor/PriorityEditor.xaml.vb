Imports System.IO.IsolatedStorage
Imports System.Xml.Linq
Imports System.IO

Partial Public Class PriorityEditor
    Inherits ChildWindow
    Dim WithEvents UI As New UserInput
    Friend EditType As PossibleEditType
    Friend FilePath As String
    Friend Enum PossibleEditType As Integer
        Rotation = 0
        Priority = 1
        Intro = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub UI_closeEvent() Handles UI.Closing

        If UI.DialogResult Then
            If UI.txtInput.Text <> "" Then
                FilePath = UI.txtInput.Text & ".xml"
                SavePriority()
                Me.DialogResult = True
                'Me.Close()
            End If
        End If
    End Sub
    Sub LoadAvailableElemnt()
        Select Case EditType
            Case PossibleEditType.Intro
                LoadAvailableRota()
            Case PossibleEditType.Priority
                LoadAvailablePrio()
            Case PossibleEditType.Rotation
                LoadAvailableRota()
        End Select
    End Sub

    Sub MoveUp(ByVal s As PrioButton)
        Dim x As Integer
        Dim y As Integer
        Dim p As PrioButton
        For Each p In Me.grpCurrentPrio.Children
            If (p.number = s.number - 1) And s.Equals(p) = False Then
                s.number -= 1
                x = Canvas.GetLeft(s)
                y = Canvas.GetTop(s)

                Canvas.SetTop(s, Canvas.GetTop(p))
                Canvas.SetLeft(s, Canvas.GetLeft(p))


                Canvas.SetTop(p, y)
                Canvas.SetLeft(p, x)

                p.number += 1
                Exit Sub
            End If
        Next
    End Sub
    Sub MoveDown(ByVal s As PrioButton)
        Dim x As Integer
        Dim y As Integer
        Dim p As PrioButton
        For Each p In Me.grpCurrentPrio.Children
            If (p.number = s.number + 1) And s.Equals(p) = False Then
                s.number += 1

                x = Canvas.GetLeft(s)
                y = Canvas.GetTop(s)
                Canvas.SetTop(s, Canvas.GetTop(p))
                Canvas.SetLeft(s, Canvas.GetLeft(p))

                Canvas.SetTop(p, y)
                Canvas.SetLeft(p, x)

                p.number -= 1
                Exit Sub
            End If
        Next
    End Sub
    Sub RemovePrio(ByVal s As PrioButton)
        Dim p As PrioButton
        For Each p In Me.grpCurrentPrio.Children
            If (p.number > s.number) Then
                MoveUp(p)
            End If
        Next
        grpCurrentPrio.Children.Remove(s)
    End Sub
    Sub AddPrio(ByVal s As PrioButton)
        Dim i As Integer
        i = Me.grpCurrentPrio.Children.Count

        Dim btn As New PrioButton(Me)
        Canvas.SetTop(btn, 10 + 45 * i)
        btn.SetName(s.ElementName)
        btn.buttonRemove.Opacity = 1
        btn.buttonUp.Opacity = 1
        btn.buttonDown.Opacity = 1
        btn.buttonAdd.Opacity = 0
        If EditType <> PossibleEditType.Priority Then
            btn.chkRetry.Opacity = 1
        Else
            btn.chkRetry.Opacity = 0
        End If
        Me.grpCurrentPrio.Children.Add(btn)
        btn.number = i
    End Sub

    Sub LoadAvailableRota()
        Dim node As XElement
        Dim btn As PrioButton
        Dim i As Integer
        grpAvailablePrio.Children.Clear()

        
                Dim doc As XDocument = XDocument.Load("config/RotationList.xml")

                For Each node In doc.Element("Rotations").Elements
                    btn = New PrioButton(Me)
                    Me.grpAvailablePrio.Children.Add(btn)
                    Canvas.SetTop(btn, 10 + 45 * i)
                    i += 1
                    btn.SetName(node.Name.ToString)
                    btn.buttonRemove.Opacity = 0
                    btn.buttonUp.Opacity = 0
                    btn.buttonDown.Opacity = 0
                    btn.chkRetry.Opacity = 1
                    btn.buttonAdd.Opacity = 1
                Next
        AutoExtend()
    End Sub

    Sub LoadAvailablePrio()
        
                Dim doc As XDocument = XDocument.Load("config/PrioritiesList.xml")
                grpAvailablePrio.Children.Clear()
                Dim btn As PrioButton
                Dim i As Integer
                Dim node As XElement

                For Each node In doc.Element("Priorities").Elements

                    btn = New PrioButton(Me)
                    Me.grpAvailablePrio.Children.Add(btn)
                    Canvas.SetTop(btn, 10 + 45 * i)

                    btn.SetName(node.Name.ToString)
                    btn.buttonRemove.Opacity = 0
                    btn.buttonUp.Opacity = 0
                    btn.buttonDown.Opacity = 0
                    btn.buttonAdd.Opacity = 1
                    btn.chkRetry.Opacity = 0
                    i += 1
                Next
        AutoExtend()
    End Sub
    Sub AutoExtend()
        grpAvailablePrio.Height = grpAvailablePrio.Children.Count * 50
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        SavePriority()
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
    Private Sub SavePriority()
        Dim Path As String = Me.FilePath
        Dim isoStream As IsolatedStorageFileStream
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim doc As XDocument
            Dim root As XElement
            Select Case EditType
                Case PossibleEditType.Intro
                    isoStream = New IsolatedStorageFileStream("KahoDKSim/Intro/" & Path, FileMode.Create, isoStore)
                    doc = XDocument.Parse("<Intro></Intro>")
                    root = doc.Element("Intro")
                Case PossibleEditType.Priority
                    isoStream = New IsolatedStorageFileStream("KahoDKSim/Priority/" & Path, FileMode.Create, isoStore)
                    doc = XDocument.Parse("<Priority></Priority>")
                    root = doc.Element("Priority")
                Case PossibleEditType.Rotation
                    isoStream = New IsolatedStorageFileStream("KahoDKSim/Rotation/" & Path, FileMode.Create, isoStore)
                    doc = XDocument.Parse("<Rotation><Rotation></Rotation></Rotation>")
                    root = doc.Element("Rotation").Element("Rotation")
                Case Else
                    isoStream = New IsolatedStorageFileStream("KahoDKSim/Rotation/" & Path, FileMode.Create, isoStore)
                    doc = XDocument.Parse("<Rotation><Rotation></Rotation></Rotation>")
                    root = doc.Element("Rotation").Element("Rotation")
            End Select


            For i As Integer = 0 To grpCurrentPrio.Children.Count - 1
                For Each P As PrioButton In grpCurrentPrio.Children
                    If P.number = i Then
                        Dim newElem As XElement
                        If EditType <> PossibleEditType.Priority Then
                            If P.chkRetry.IsChecked Then
                                newElem = XElement.Parse("<" & P.ElementName & " retry='1'></" & P.ElementName & ">")
                            Else
                                newElem = XElement.Parse("<" & P.ElementName & " retry='0'></" & P.ElementName & ">")
                            End If
                        Else
                            newElem = XElement.Parse("<" & P.ElementName & "></" & P.ElementName & ">")
                        End If
                        root.Add(newElem)
                    End If
                Next
            Next
            doc.Save(isoStream)
            isoStream.Close()
        End Using
    End Sub
    Sub OpenIntroForEdit(ByVal path As String)
        Me.grpCurrentPrio.Children.Clear()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim i As Integer
            FilePath = path
            Dim btn As PrioButton

            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Intro/" & path, FileMode.Open, isoStore)
                Dim xmlDoc As XDocument = XDocument.Load(isoStream)
                For Each node As XElement In xmlDoc.Element("Intro").Elements
                    btn = New PrioButton(Me)

                    btn.SetName(node.Name.ToString)
                    btn.buttonRemove.Opacity = 1
                    btn.buttonUp.Opacity = 1
                    btn.buttonDown.Opacity = 1
                    btn.buttonAdd.Opacity = 0
                    btn.chkRetry.Opacity = 1
                    Me.grpCurrentPrio.Children.Add(btn)
                    Canvas.SetTop(btn, 10 + 45 * i)
                    btn.number = i
                    i += 1
                    If node.Attribute("retry").Value = 0 Then
                        btn.chkRetry.IsChecked = False
                    Else
                        btn.chkRetry.IsChecked = True
                    End If
                Next
            End Using
        End Using
    End Sub


    Sub OpenPrioForEdit(ByVal path As String)
        Me.grpCurrentPrio.Children.Clear()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim i As Integer
            FilePath = path
            Dim btn As PrioButton

            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Priority/" & path, FileMode.Open, isoStore)
                Dim xmlDoc As XDocument = XDocument.Load(isoStream)
                For Each node As XElement In xmlDoc.Element("Priority").Elements
                    btn = New PrioButton(Me)

                    btn.SetName(node.Name.ToString)
                    btn.buttonRemove.Opacity = 1
                    btn.buttonUp.Opacity = 1
                    btn.buttonDown.Opacity = 1
                    btn.buttonAdd.Opacity = 0
                    btn.chkRetry.Opacity = 0
                    Me.grpCurrentPrio.Children.Add(btn)
                    Canvas.SetTop(btn, 10 + 45 * i)
                    btn.number = i
                    i += 1
                Next
            End Using
        End Using

    End Sub

    Sub OpenRotaForEdit(ByVal path As String)
        Me.grpCurrentPrio.Children.Clear()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim i As Integer
            FilePath = path
            Dim btn As PrioButton

            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Rotation/" & path, FileMode.Open, isoStore)
                Dim xmlDoc As XDocument = XDocument.Load(isoStream)
                For Each node As XElement In xmlDoc.Element("Rotation").Element("Rotation").Elements
                    btn = New PrioButton(Me)

                    btn.SetName(node.Name.ToString)
                    btn.buttonRemove.Opacity = 1
                    btn.buttonUp.Opacity = 1
                    btn.buttonDown.Opacity = 1
                    btn.buttonAdd.Opacity = 0
                    btn.chkRetry.Opacity = 1
                    Me.grpCurrentPrio.Children.Add(btn)
                    Canvas.SetTop(btn, 10 + 45 * i)
                    btn.number = i
                    i += 1
                    If node.Attribute("retry").Value = 0 Then
                        btn.chkRetry.IsChecked = False
                    Else
                        btn.chkRetry.IsChecked = True
                    End If
                Next
            End Using
        End Using

    End Sub

    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdPreview.Click
        Dim oldPath As String = FilePath
        FilePath = "tempo.xml"
        SavePriority()
        Dim tmpPath As String

         Select EditType
            Case PossibleEditType.Intro
                tmpPath = "KahoDKSim/Intro/tempo.xml"
            Case PossibleEditType.Priority
                tmpPath = ("KahoDKSim/Priority/tempo.xml")

            Case PossibleEditType.Rotation
                tmpPath = ("KahoDKSim/Rotation/tempo.xml")
            Case Else
                FilePath = oldPath
                Exit Sub
        End Select
        Dim txtEditor As New TextEditor
        txtEditor.OpenFileFromISO(tmpPath)
        txtEditor.Show()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            isoStore.DeleteFile(tmpPath)
        End Using

        FilePath = oldPath
    End Sub
End Class
