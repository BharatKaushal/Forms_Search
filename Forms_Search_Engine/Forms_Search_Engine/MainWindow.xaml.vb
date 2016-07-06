Imports System.Data.SqlClient
Class MainWindow
    Dim SQL As New SQLControl
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        If SQL.HasConnection = True Then
            SQL.RunQuery("SELECT * FROM FORMS_INFO WHERE ProgramName= '" & ProgramName.Text & "';")
            If SQL.SQLDataSet.Tables.Count > 0 Then
                dataGrid.ItemsSource = SQL.SQLDataSet.DefaultViewManager
            End If
        End If
        'MessageBox.Show("Will Find " & ProgramName.Text & " with form ID " & FormID.Text & " when you have the logic in place")
    End Sub
End Class
