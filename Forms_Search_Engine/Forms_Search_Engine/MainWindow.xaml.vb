﻿Imports System.Data.SqlClient
Class MainWindow
    Dim SQL As New DataManager
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        'Dim l As New List(Of Mutant) From {New Mutant("From"), New Mutant("HELLO")}
        'myDataGrid.ItemsSource = l
        If Mid(ProgramName.Text, 1, 1) > Chr(32) And Mid(FormID.Text, 1, 1) > Chr(32) Then
            myDataGrid.ItemsSource = FormData.FetchExisting("SELECT * FROM FORMS_INFO WHERE ProgramName= '" & ProgramName.Text & "' AND FormId= '" & FormID.Text & "';")
        ElseIf Mid(ProgramName.Text, 1, 1) > Chr(32) Then
            myDataGrid.ItemsSource = FormData.FetchExisting("SELECT * FROM FORMS_INFO WHERE ProgramName= '" & ProgramName.Text & "';")
        ElseIf Mid(FormID.Text, 1, 1) > Chr(32) Then
            myDataGrid.ItemsSource = FormData.FetchExisting("SELECT * FROM FORMS_INFO WHERE FormId= '" & FormID.Text & "';")
        End If
        'MessageBox.Show("Will Find " & ProgramName.Text & " with form ID " & FormID.Text & " when you have the logic in place")
    End Sub
End Class
