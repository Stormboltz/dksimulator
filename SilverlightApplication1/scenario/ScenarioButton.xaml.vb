Imports System.Xml.Linq

Partial Public Class ScenarioButton
    Inherits UserControl
    Dim Mainfrm As ScenarioEditor
    Friend XmlBuid As XElement
    Friend number As Integer

    Friend colControl As New Collections.Generic.List(Of Control)
    Friend myName As String

    Public Sub New(ByVal m As ScenarioEditor)

        InitializeComponent()
        Mainfrm = m
        number = 0
    End Sub

    Sub build(ByVal xElem As XElement)
        Dim N As XElement
        Dim xDoc As New XDocument
        Dim x As Integer = 10
        Dim y As Integer = 15


        Dim chk As CheckBox
        Dim tBox As myTextButton
        Dim lbl As Label



        XmlBuid = xElem
        Me.SetName(xElem.Attribute("caption").Value)
        For Each N In xElem.Elements
            If N.Attribute("type").Value = "checkbox" Then
                chk = New CheckBox
                controlStack.Children.Add(chk)
                'Canvas.SetLeft(chk, x)
                'Canvas.SetTop(chk, y)
                chk.Width = 250
                chk.Height = 20
                Try
                    chk.IsChecked = N.Value
                Catch
                    chk.IsChecked = False
                End Try
                chk.Content = N.Attribute("caption").Value
                chk.Name = N.Name.ToString
                colControl.Add(chk)
            End If

            If N.Attribute("type").Value = "textbox" Then
                tBox = New myTextButton
                controlStack.Children.Add(tBox)
                'Canvas.SetLeft(tBox, x)
                'Canvas.SetTop(tBox, y)
                Try
                    tBox.Text.Text = Int(N.Value)
                Catch
                    tBox.Text.Text = 0
                End Try

                tBox.multi = N.Attribute("multi").Value
                tBox.caption = N.Attribute("caption").Value
                tBox.Name = N.Name.ToString
                lbl = tBox.label
                lbl.Content = N.Attribute("caption").Value
                colControl.Add(tBox)
            End If
            y += 25
        Next
        Me.Height = 20 + controlStack.Children.Count * 20
    End Sub


    Sub buttonAddclick(ByVal sender As Object, ByVal e As EventArgs) Handles buttonAdd.Click
        Mainfrm.AddElement(Me)
    End Sub
    Sub buttonRemoveclick(ByVal sender As Object, ByVal e As EventArgs) Handles buttonRemove.Click
        Mainfrm.RemoveElement(Me)
    End Sub
    Sub buttonUpclick(ByVal sender As Object, ByVal e As EventArgs) Handles buttonUp.Click
        Mainfrm.MoveUp(Me)
    End Sub
    Sub buttonDownclick(ByVal sender As Object, ByVal e As EventArgs) Handles buttonDown.Click
        Mainfrm.MoveDown(Me)
    End Sub
    Sub SetName(ByVal n As String)
        On Error Resume Next
        Me.Text.Content = n
        Me.myName = n
    End Sub

    
    Private Sub ScenarioButton_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
        Me.Border.Height = e.NewSize.Height
        Me.Border.Width = e.NewSize.Width
        Me.controlStack.Width = e.NewSize.Width - 40
    End Sub
End Class
