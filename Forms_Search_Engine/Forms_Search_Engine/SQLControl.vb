Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data

Public Class SQLControl
    Public SQLConnect As New SqlConnection With {.ConnectionString = "Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes;"}
    Public SQLCMD As SqlCommand
    Public SQLDA As SqlDataAdapter
    Public SQLDataSet As DataSet

    Public Function HasConnection() As Boolean
        Try
            SQLConnect.Open()
            SQLConnect.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Public Sub RunQuery(Query As String)
        Try
            SQLConnect.Open()
            SQLCMD = New SqlCommand(Query, SQLConnect)
            SQLDA = New SqlDataAdapter(SQLCMD)
            SQLDataSet = New DataSet
            SQLDA.Fill(SQLDataSet)
            'Dim R As SqlDataReader = SQLCMD.ExecuteReader
            'While R.Read
            '    MsgBox(R.GetName(0) & ": " & R(0))
            'End While
            SQLConnect.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLConnect.State = ConnectionState.Open Then
                SQLConnect.Close()
            End If
        End Try
    End Sub
End Class
