Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data
Public Class DataManager
    Implements IDataStore
    Public Shared Function FormInfoChanged(query As String) As Date
        Dim Result As String
        Dim DateModi As Date
        'Dim connection As New SqlConnection("Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes")
        Dim connection As New SqlConnection("Server=BHARATS03D\SQLEXPRESS;Database=FORMS_DATABASE;User=pbsuser;Pwd=pbs8805")
        Try
            connection.Open()
            Dim GetDateModified As New SqlCommand(query, connection)
            DateModi = Convert.ToString(GetDateModified.ExecuteScalar)
            Return DateModi
        Catch ex As Exception
            Result = ex.Message & vbNewLine
            Result &= ex.StackTrace.ToString
            Return DateModi
        Finally
            connection.Close()
        End Try
    End Function
    Public Shared Function CheckDatabase(query As String) As Boolean
        Dim Result As String = String.Empty
        Dim AlreadyExist As Boolean
        'Dim connection As New SqlConnection("Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes")
        Dim connection As New SqlConnection("Server=BHARATS03D\SQLEXPRESS;Database=FORMS_DATABASE;User=pbsuser;Pwd=pbs8805")
        Try
            connection.Open()
            Dim CheckUserName As New SqlCommand(query, connection)
            AlreadyExist = Convert.ToBoolean(CheckUserName.ExecuteScalar())
            Return AlreadyExist
        Catch ex As Exception
            Result = ex.Message & vbNewLine
            Result &= ex.StackTrace.ToString
            Return AlreadyExist
        Finally
            connection.Close()
        End Try
    End Function
    Public Shared Function UpdateDatabase(query As String) As Boolean
        Dim data As SqlDataReader = Nothing
        Dim Result As String = String.Empty
        'Dim connection As New SqlConnection("Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes")
        Dim connection As New SqlConnection("Server=BHARATS03D\SQLEXPRESS;Database=FORMS_DATABASE;User=pbsuser;Pwd=pbs8805")
        Try
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            data = command.ExecuteReader()
            Return True
        Catch ex As Exception
            Result = ex.Message & vbNewLine
            Result &= ex.StackTrace.ToString
            Return False
        Finally
            connection.Close()
        End Try
    End Function
    Public Function ExecuteQuery(query As String) As Tuple(Of SqlDataReader, String) Implements IDataStore.ExecuteQuery
        Dim data As SqlDataReader = Nothing
        Dim Result As String = "All good"
        Try
            'Dim connection As New SqlConnection("Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes")
            Dim connection As New SqlConnection("Server=BHARATS03D\SQLEXPRESS;Database=FORMS_DATABASE;User=pbsuser;Pwd=pbs8805")
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            data = command.ExecuteReader()
        Catch ex As Exception
            Result = ex.Message & vbNewLine
            Result &= ex.StackTrace.ToString
        End Try
        Return Tuple.Create(data, Result)
    End Function
End Class
