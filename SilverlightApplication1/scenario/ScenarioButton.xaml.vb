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
                Me.LayoutRoot.Children.Add(chk)

                '.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
                Canvas.SetLeft(chk, x)
                Canvas.SetTop(chk, y)
                chk.Width = 50
                chk.Height = 20
                Try
                    chk.IsChecked = N.Value
                Catch
                    chk.IsChecked = False
                End Try

                '.AutoSize = True
                chk.Content = N.Attribute("caption").Value
                chk.Name = N.Name.ToString

                colControl.Add(chk)
            End If

            If N.Attribute("type").Value = "textbox" Then
                tBox = New myTextButton
                Me.LayoutRoot.Children.Add(tBox)

                Canvas.SetLeft(tBox, x)
                Canvas.SetTop(tBox, y)
                tBox.Width = 50
                tBox.Height = 20
                Try
                    tBox.Text = Int(N.Value)
                Catch
                    tBox.Text = 0
                End Try

                tBox.multi = N.Attribute("multi").Value
                tBox.caption = N.Attribute("caption").Value
                tBox.Name = N.Name.ToString


                lbl = New Label
                Me.LayoutRoot.Children.Add(lbl)

                Canvas.SetLeft(lbl, Canvas.GetLeft(tBox) + tBox.Width + x)
                Canvas.SetTop(lbl, y)
                lbl.Width = 50
                lbl.Height = 20
                '.AutoSize = True
                lbl.Content = N.Attribute("caption").Value
                lbl.Name = "lbl" & N.Name.ToString
                colControl.Add(tBox)
            End If
            y += 25
        Next
        Me.Height = y
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

    Friend Class myTextButton
        Inherits TextBox
        Friend multi As Integer
        Friend caption As String
    End Class
End Class
