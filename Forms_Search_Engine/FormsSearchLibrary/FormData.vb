Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient
Imports Csla

<Serializable()>
Public Class FormData
    Inherits BusinessBase(Of FormData)

#Region "  Properties "
    Public Shared FormIdProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(x) x.FormId)
    Public Property FormId As String
        Get
            Return GetProperty(FormIdProperty)
        End Get
        Set(value As String)
            SetProperty(FormIdProperty, value)
        End Set
    End Property
    Public Shared ReadOnly ProgramNameProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.ProgramName)
    Public Property ProgramName() As String
        Get
            Return GetProperty(ProgramNameProperty)
        End Get
        Set(ByVal value As String)
            SetProperty(ProgramNameProperty, value)
        End Set
    End Property
    Public Shared ReadOnly DescriptionProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.Description)
    Public Property Description() As String
        Get
            Return GetProperty(DescriptionProperty)
        End Get
        Set(ByVal value As String)
            SetProperty(DescriptionProperty, value)
        End Set
    End Property
    Public Shared ReadOnly OffsetProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.Offset)
    Public Property Offset() As String
        Get
            Return GetProperty(OffsetProperty)
        End Get
        Set(ByVal value As String)
            SetProperty(OffsetProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PathProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.File_Path)
    Public Property File_Path() As String
        Get
            Return GetProperty(PathProperty)
        End Get
        Set(ByVal value As String)
            SetProperty(PathProperty, value)
        End Set
    End Property

    Public Shared ReadOnly Date_ModifiedProperty As PropertyInfo(Of DateTime) = RegisterProperty(Of DateTime)(Function(c) c.Date_Modified)
    Public Property Date_Modified() As DateTime
        Get
            Return GetProperty(Date_ModifiedProperty)
        End Get
        Set(ByVal value As DateTime)
            SetProperty(Date_ModifiedProperty, value)
        End Set
    End Property
#End Region
#Region "  Data Access "
    Public Sub New()
        MarkAsChild()
    End Sub

    Public Sub Populate(ByVal id As String, name As String, desc As String, offset As String, FIL_PATH As String, modi As DateTime)
        LoadProperty(FormIdProperty, id)
        LoadProperty(ProgramNameProperty, name)
        LoadProperty(DescriptionProperty, desc)
        LoadProperty(OffsetProperty, offset)
        LoadProperty(Date_ModifiedProperty, modi)
        LoadProperty(PathProperty, FIL_PATH)
    End Sub

    Public Shared Function Fetch(ByVal crit As Object) As FormData
        Return DataPortal.Fetch(Of FormData)(crit)
    End Function
    Public Shared Async Function FetchAsync(ByVal crit As Object) As Task(Of FormData)
        Return Await DataPortal.FetchAsync(Of FormData)(crit)
    End Function

    Protected Sub DataPortal_Fetch(ByVal crit As Object)
        Dim sql As String = String.Format("SELECT * FROM FORMS_INFO WHERE FormId = {0}", crit)
        Dim dt = crit.DataProvider.ExecuteQuery(sql)
        Dim dr = dt.Item1.Tables(0).Rows.First
        If dr IsNot Nothing Then
            Dim itm As New FormData()
            itm.Populate(dr("FormId"), dr("ProgramName"), dr("Description"), dr("Offset"), dr("FilePath"), dr("DateModified"))
        End If
    End Sub
    Public Sub Update()
        Dim FileDate1 As Date
        Dim CompareFileDate As Integer
        Dim FileDate2 As Date
        Dim CheckDateModified As String = String.Empty
        Dim updateQuery As String = String.Empty
        updateQuery = "SELECT COUNT(*) FROM Forms_Info WHERE (ProgramName = '" + ProgramName + "')"
        Dim AlreadyExist As Boolean = DataManager.CheckDatabase(updateQuery)
        If AlreadyExist = True Then
            FileDate1 = Date_Modified
            CheckDateModified = "SELECT DateModified FROM FORMS_INFO WHERE ProgramName = '" + ProgramName + "';"
            FileDate2 = DataManager.FormInfoChanged(CheckDateModified)
            CompareFileDate = DateTime.Compare(FileDate1, FileDate2)
            If CompareFileDate > 0 Then
                Dim UpdateFileQuery As New Text.StringBuilder
                UpdateFileQuery.AppendLine("UPDATE Forms_Info ")
                UpdateFileQuery.AppendLine("SET FormID = '" & FormId & "', ProgramName = '" & ProgramName & "', Description = '" & Description & "',Offset = '" & Offset & "', FilePath = '" & File_Path & "', DateModified = '" & Date_Modified & "' ")
                UpdateFileQuery.AppendLine("WHERE ProgramName = '" & ProgramName & "';")
                DataManager.UpdateDatabase(UpdateFileQuery.ToString)
            End If
        Else
            Dim query As New Text.StringBuilder
            query.AppendLine("INSERT INTO Forms_Info(FormId,ProgramName,Description,Offset,FilePath,DateModified)")
            query.AppendLine("VALUES ('" & FormId & "','" & ProgramName & "','" & Description & "','" & Offset & "','" & File_Path & "','" & Date_Modified & "');")
            DataManager.UpdateDatabase(query.ToString)
        End If
        'If Me.BrokenRulesCollection.Count > 0 Then
        '    'add to log file 
        'End If
    End Sub
    Public Sub DeleteSelf()

    End Sub
#End Region
#Region "  Broken Rules"

#End Region

    Public Class Criteria
        Public Property Id As String
        Public Property DataProvider As IDataStore
    End Class
End Class
