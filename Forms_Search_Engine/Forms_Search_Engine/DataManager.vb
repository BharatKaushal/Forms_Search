Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data
Public Class DataManager
    Implements IDataStore
    Public Shared Function UpdateDatabase(query As String) As Boolean
        Dim data As SqlDataReader = Nothing
        Dim Result As String = "All good"
        Dim connection As New SqlConnection("Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes") 'User=pbsuser;Pwd=pbs8805")
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
            Dim connection As New SqlConnection("Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes") 'User=pbsuser;Pwd=pbs8805")
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            data = command.ExecuteReader()
        Catch ex As Exception
            Result = ex.Message & vbNewLine
            Result &= ex.StackTrace.ToString
        End Try
        Return Tuple.Create(data, Result)
    End Function
    Public Shared Function ExecuteInsertUpdate(query As String, ByVal fields As Dictionary(Of String, Object)) As Object
        Dim IdentityValue As Object = Nothing
        Dim data As SqlDataReader = Nothing
        Using connection As New SqlConnection("Server=MYPC\SQLEXPRESS;Database=FORMS_DATABASE;Trusted_Connection=Yes") 'User=pbsuser;Pwd=pbs8805")
            connection.Open()
            Try
                Dim ds As New DataSet
                Dim da As New SqlDataAdapter(query, connection)
                da.FillSchema(ds, SchemaType.Source)
                da.Fill(ds)
                Dim cb As New SqlCommandBuilder(da)
                Dim dr As DataRow = Nothing
                If ds.Tables(0).Rows.Count = 0 Then
                    dr = ds.Tables(0).NewRow
                Else
                    dr = ds.Tables(0).Rows(0)
                End If
                For Each fieldName As String In fields.Keys
                    dr(fieldName) = fields(fieldName)
                Next
                If dr.RowState = DataRowState.Detached Then
                    ds.Tables(0).Rows.Add(dr)
                End If
                Dim identityColumn As DataColumn = Nothing
                For Each dc As DataColumn In ds.Tables(0).Columns
                    If dc.AutoIncrement Then
                        identityColumn = dc
                        Exit For
                    End If
                Next
                For Each row As DataRow In ds.Tables(0).Rows
                    Dim newRow As Boolean = False
                    If row.RowState = DataRowState.Added Then
                        newRow = True
                    End If
                    If Not row.RowState = DataRowState.Unchanged Then
                        da.Update({row})
                        If identityColumn IsNot Nothing Then
                            IdentityValue = row(identityColumn)
                        End If
                    End If
                    If newRow AndAlso (identityColumn IsNot Nothing) Then
                        Dim result As Object
                        Using comm As New SqlCommand("SELECT @@IDENTITY", connection)
                            result = comm.ExecuteScalar()
                        End Using
                        Try
                            IdentityValue = result
                        Catch ex As Exception
                            IdentityValue = Nothing
                        End Try
                    End If
                Next
            Finally
                connection.Close()
            End Try
        End Using
        Return IdentityValue
    End Function
End Class
