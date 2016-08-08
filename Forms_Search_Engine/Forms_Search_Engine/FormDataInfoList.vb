
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
            Dim Description As String = crit.Description
            Dim connection As SqlConnection = Nothing
            Query.AppendLine("SELECT * FROM FORMS_INFO ")
            Query.AppendLine("WHERE 1=1")
            If Not String.IsNullOrWhiteSpace(ProgramName) AndAlso Not String.IsNullOrWhiteSpace(FormId) AndAlso Not String.IsNullOrWhiteSpace(Description) Then
                Query.AppendFormat("AND ProgramName LIKE '%{0}%' AND FormId LIKE '%{1}%' AND Description LIKE '%{2}%'", ProgramName, FormId, Description)
            ElseIf Not String.IsNullOrWhiteSpace(ProgramName) AndAlso Not String.IsNullOrWhiteSpace(FormId) Then
                Query.AppendFormat("AND ProgramName LIKE '%{0}%' AND FormId LIKE '%{1}%'", ProgramName, FormId)
            ElseIf Not String.IsNullOrWhiteSpace(ProgramName) AndAlso Not String.IsNullOrWhiteSpace(Description) Then
                Query.AppendFormat("AND ProgramName LIKE '%{0}%' AND Description LIKE '%{1}%'", ProgramName, Description)
            ElseIf Not String.IsNullOrWhiteSpace(FormId) AndAlso Not String.IsNullOrWhiteSpace(Description) Then
                Query.AppendFormat("AND FormId LIKE '%{0}%' AND Description LIKE '%{1}%'", FormId, Description)
            ElseIf Not String.IsNullOrWhiteSpace(ProgramName) Then
                Query.AppendFormat("AND ProgramName LIKE '%{0}%'", ProgramName)
            ElseIf Not String.IsNullOrWhiteSpace(FormId) Then
                Query.AppendFormat("AND FormId LIKE '%{0}%'", FormId)
            ElseIf Not String.IsNullOrWhiteSpace(Description) Then
                Query.AppendFormat("AND Description LIKE '%{0}%'", Description)
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
                        .File_Path = dr("FilePath")
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
        Public Property File_Path As String
    End Class
End Class
