'Imports System.Data.SqlClient
Class MainWindow
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        Refresh()
    End Sub
    Private Sub SearchButtonClick(sender As Object, e As RoutedEventArgs) Handles SearchButton.Click
        Refresh()
        If myDataGrid.Items.Count = 0 Then
            MessageBox.Show("No Record Found!!")
        End If
    End Sub
    Private Sub ClearButtonClick(sender As Object, e As RoutedEventArgs) Handles ClearButton.Click
        ProgramName.Text = String.Empty
        FormID.Text = String.Empty
        Refresh()
    End Sub
    Public Sub Refresh()
        Dim crit As New FormDataInfoList.Criteria()
        crit.ProgramName = ProgramName.Text
        crit.FormId = FormID.Text
        myDataGrid.ItemsSource = FormDataInfoList.Fetch(crit)
    End Sub

    Private Sub bFileSearchButtonClick(sender As Object, e As RoutedEventArgs) Handles FileSearchButton.Click
        Dim list As List(Of String) = FileSearch.GetFilesRecursive("")
        For Each path In list
            Console.WriteLine(path)
        Next
        Console.WriteLine(list.Count)
    End Sub
End Class
