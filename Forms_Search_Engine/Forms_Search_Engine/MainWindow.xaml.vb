Imports System.Data.SqlClient
Class MainWindow

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        myDataGrid.ItemsSource = New List(Of FormData)
    End Sub
    Private Sub SearchButtonClick(sender As Object, e As RoutedEventArgs) Handles SearchButton.Click
        Dim Query As String = Nothing
        If Mid(ProgramName.Text, 1, 1) > Chr(32) And Mid(FormID.Text, 1, 1) > Chr(32) Then
            Query = "SELECT * FROM FORMS_INFO WHERE ProgramName= '" & ProgramName.Text & "' AND FormId= '" & FormID.Text & "';"
        ElseIf Mid(ProgramName.Text, 1, 1) > Chr(32) Then
            Query = "SELECT * FROM FORMS_INFO WHERE ProgramName= '" & ProgramName.Text & "';"
        ElseIf Mid(FormID.Text, 1, 1) > Chr(32) Then
            Query = "SELECT * FROM FORMS_INFO WHERE FormId= '" & FormID.Text & "';"
        End If
        If Not String.IsNullOrEmpty(Query) Then
            myDataGrid.ItemsSource = FormData.FetchExisting(Query)
            If myDataGrid.Items.Count = 0 Then
                MessageBox.Show("No Record Found!!")
            End If
        Else
            MessageBox.Show("Enter Program Name or Form ID to Search the Database!!")
        End If
    End Sub
    Private Sub ClearButtonClick(sender As Object, e As RoutedEventArgs) Handles ClearButton.Click

        ProgramName.Text = String.Empty
        FormID.Text = String.Empty
        myDataGrid.ItemsSource = New List(Of FormData)
    End Sub
End Class
