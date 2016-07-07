
'Public SQLConnect As New SqlConnection With {.ConnectionString = "Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes;"}
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data
Public Class DataManager

    Public Function ExecuteQuery(query As String) As Tuple(Of DataSet, String)
        Dim data As New DataSet
        Dim Result As String = "All good"
        Try
            Using connection As New SqlConnection("Server=DEVELOPMENT01L\SQLEXPRESS;Database=FORMS_DATABASE;User=pbsuser;Pwd=pbs8805")
                connection.Open()
                Dim command As New SqlCommand(query, connection)
                Dim dataAdapter As New SqlDataAdapter(command)
                dataAdapter.Fill(data)
                connection.Close()
            End Using
        Catch ex As Exception
            Result = ex.Message & vbNewLine
            Result = ex.StackTrace.ToString
        End Try
        Return Tuple.Create(data, Result)
    End Function
End Class
