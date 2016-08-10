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
        Dim crit As New FormsSearchLibrary.FormDataInfoList.Criteria()
        crit.ProgramName = ProgramName.Text
        crit.FormId = FormID.Text
        crit.Description = Description.Text
        crit.DataSource = New FormsSearchLibrary.DataManager
        myDataGrid.ItemsSource = FormsSearchLibrary.FormDataInfoList.Fetch(crit)
    End Sub
End Class
