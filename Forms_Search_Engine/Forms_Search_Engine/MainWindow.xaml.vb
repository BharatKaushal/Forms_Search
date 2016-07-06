Imports System.Data.SqlClient
Class MainWindow
    Dim SQL As New DataManager
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        'Dim l As New List(Of Mutant) From {New Mutant("From"), New Mutant("HELLO")}
        'myDataGrid.ItemsSource = l
        myDataGrid.ItemsSource = FormData.FetchExisting("SELECT * FROM FORMS_INFO WHERE ProgramName= '" & ProgramName.Text & "';").Item1
        'MessageBox.Show("Will Find " & ProgramName.Text & " with form ID " & FormID.Text & " when you have the logic in place")
    End Sub
End Class
