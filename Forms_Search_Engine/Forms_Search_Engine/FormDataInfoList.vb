
<Serializable()>
Public Class FormDataInfoList
    Inherits ReadOnlyBindingListBase(Of FormDataInfoList, FormDataInfoList.listItem)

    'Public Shared Function Fetch(arg1, arg2, arg3, arg4, arg5, arg6) As FormDataInfoList

    'End Function
    'Public Shared Function Fetch(arg1, arg3, arg5, arg6) As FormDataInfoList

    'End Function

    Public Shared Function Fetch(crit As Criteria) As FormDataInfoList
        Dim FormInfoList As New FormDataInfoList
        If crit.IsValid Then
            Dim Query As New Text.StringBuilder
            Dim ProgramName As String = crit.ProgramName
            Dim FormId As String = crit.FormId

            Query.AppendLine("SELECT * FROM FORMS_INFO ")
            Query.AppendLine("WHERE 1=1")
            If Not String.IsNullOrWhiteSpace(ProgramName) AndAlso Not String.IsNullOrWhiteSpace(FormId) Then
                Query.AppendFormat("AND ProgramName = '{0} AND FormId = {1}'", ProgramName, FormId)
            ElseIf Not String.IsNullOrWhiteSpace(ProgramName) Then
                Query.AppendFormat("AND ProgramName = '{0}'", ProgramName)
            ElseIf Not String.IsNullOrWhiteSpace(FormId) Then
                Query.AppendFormat("AND FormId = '{0}'", FormId)
            End If
            If Not String.IsNullOrEmpty(Query.ToString) Then
                Dim dt = crit.DataSource.ExecuteQuery(Query.ToString)
                For Each dr In dt.Item1.Tables(0).Rows 'dt.Item1.Tables.First.Rows
                    'Dim itm As New FormDataInfoList.listItem(dr("FormId"), dr("ProgramName"), dr("Descriprion"), dr("Offset"), dr("Date_Modified"))
                    Dim itm As New FormDataInfoList.listItem
                    With itm
                        .FormId = dr("FormID")
                        .ProgramName = dr("ProgramName")
                        .Description = dr("Descriprion")
                        .Offset = dr("Offset")
                        .Date_Modified = dr("Date_Modified")
                    End With
                    FormInfoList.Add(itm)
                Next
            Else
                MessageBox.Show("Enter Program Name or Form ID to Search the Database!!")
            End If
        End If
        Return FormInfoList
    End Function

    Public Class Criteria
        Public Property ProgramName As String
        Public Property FormId As String
        Public Property Description As String
        'Public Property LastUpdated As DateTime?
        Public Property CreatedDate As Nullable(Of DateTime)
        'Public Property DisplayInactive As Boolean?
        Public Property DataSource As IDataStore

        Public Function IsValid() As Boolean
            Return Not String.IsNullOrWhiteSpace(ProgramName) OrElse Not String.IsNullOrWhiteSpace(FormId) OrElse Not String.IsNullOrWhiteSpace(Description) ' FormId.HasValue OrElse Description.HasValue
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
