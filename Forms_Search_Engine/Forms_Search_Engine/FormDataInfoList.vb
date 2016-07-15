
Imports System.Data.SqlClient

<Serializable()>
Public Class FormDataInfoList
    Inherits ReadOnlyBindingListBase(Of FormDataInfoList, FormDataInfoList.listItem)

    Public Shared Function Fetch(crit As Criteria) As FormDataInfoList
        Dim FormInfoList As New FormDataInfoList
        If crit.IsValid Then
            Dim Query As New Text.StringBuilder
            Dim ProgramName As String = crit.ProgramName
            Dim FormId As String = crit.FormId
            Dim connection As SqlConnection = Nothing
            Query.AppendLine("SELECT * FROM FORMS_INFO ")
            Query.AppendLine("WHERE 1=1")
            If Not String.IsNullOrWhiteSpace(ProgramName) AndAlso Not String.IsNullOrWhiteSpace(FormId) Then
                Query.AppendFormat("AND ProgramName = '{0} AND FormId = {1}'", ProgramName, FormId)
            ElseIf Not String.IsNullOrWhiteSpace(ProgramName) Then
                Query.AppendFormat("AND ProgramName = '{0}'", ProgramName)
            ElseIf Not String.IsNullOrWhiteSpace(FormId) Then
                Query.AppendFormat("AND FormId = '{0}'", FormId)
            End If
            FormInfoList.IsReadOnly = False
            Dim result = crit.DataSource.ExecuteQuery(Query.ToString)
            Using dr As SqlDataReader = result.Item1
                Do While dr.Read()
                    Dim itm As New FormDataInfoList.listItem
                    With itm
                        .FormId = dr("FormID")
                        .ProgramName = dr("ProgramName")
                        .Description = dr("Description")
                        .Offset = dr("Offset")
                        .Date_Modified = dr("DateModified")
                    End With
                    FormInfoList.Add(itm)
                Loop
            End Using
            FormInfoList.IsReadOnly = True
        End If
        Return FormInfoList
    End Function

    Public Class Criteria
        Public Property ProgramName As String
        Public Property FormId As String
        Public Property Description As String
        Public Property CreatedDate As Nullable(Of DateTime)
        Public Property DataSource As IDataStore

        Public Function IsValid() As Boolean
            Return Not String.IsNullOrWhiteSpace(ProgramName) OrElse Not String.IsNullOrWhiteSpace(FormId) OrElse Not String.IsNullOrWhiteSpace(Description)
        End Function
    End Class

    Public Class listItem
        Public Property FormId As String
        Public Property ProgramName As String
        Public Property Description As String
        Public Property Offset As String
        Public Property Date_Modified As DateTime
    End Class
End Class
