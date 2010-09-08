﻿Partial Public Class frmArmory
    Inherits ChildWindow

    Public Sub New()
        InitializeComponent()
        cmbRegion.Items.Clear()
        cmbRegion.Items.Add("US")
        cmbRegion.Items.Add("EU")
        cmbRegion.Items.Add("CN")
        cmbRegion.Items.Add("KR")
        cmbRegion.Items.Add("TW")
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub

    Private Sub ChildWindow_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
       
    End Sub

    Private Sub txtInput_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtCharacter.KeyDown
        If e.Key = Key.Enter Then
            Me.DialogResult = True
        End If
        If e.Key = Key.Escape Then
            Me.DialogResult = False
        End If
    End Sub

End Class
