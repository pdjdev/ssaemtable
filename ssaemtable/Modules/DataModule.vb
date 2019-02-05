Imports System.Globalization
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.Office.Interop
Imports System.Security.Principal

Module DataModule
    Public Function settxt(ByVal tag As String, ByVal val As String) As String
        Return "<" & tag & ">" & val & "</" & tag & ">" + Environment.NewLine
    End Function

    Public Function chktask(ByVal day As String, ByVal stime As Integer, ByVal etime As Integer) As Boolean
        Return True
    End Function

    Public Function savetask(title As String, '제목
                             column As Integer, '열
                             row As Integer, '행
                             days As String, '요일
                             theme As Integer, '테마
                             tables As String, '테이블
                             formsize_x As Integer, 'x사이즈
                             formsize_y As Integer, 'y사이즈
                             winloc_x As Integer, 'x위치
                             winloc_y As Integer) As String 'y위치

        Dim settingtext As String = vbCrLf
        settingtext += settxt("title", title)
        settingtext += settxt("column", column)
        settingtext += settxt("row", row)
        settingtext += settxt("days", days)
        settingtext += settxt("theme", theme)
        settingtext += settxt("tables", tables)
        settingtext += settxt("formsize_x", formsize_x)
        settingtext += settxt("formsize_y", formsize_y)
        settingtext += settxt("winloc_x", winloc_x)
        settingtext += settxt("winloc_y", winloc_y)

        settingtext = settxt("ssaemtable", settingtext)
        Return settingtext
    End Function

    Public Function midReturn(ByRef total As String, ByVal first As String, ByVal last As String)
        If total.Contains(first) Then
            Dim FirstStart As Long = total.IndexOf(first) + first.Length + 1
            Return Trim(Mid$(total, FirstStart, total.Substring(FirstStart).IndexOf(last) + 1))
        Else
            Return ErrorToString("{ERROR}")
        End If
    End Function

    Public Function multiplemidReturn(ByRef total As String, ByVal first As String, ByVal last As String)
        If total.Contains(first) Then
            Dim tmptotal = total
            Dim res As String = Nothing
            Dim count = 0

ret:
            If tmptotal.Contains(first) = True Then
                Dim FirstStart As Long = tmptotal.IndexOf(first) + first.Length + 1
                res += (Trim(Mid$(tmptotal, FirstStart, tmptotal.Substring(FirstStart).IndexOf(last) + 1))) + vbCr
                tmptotal = Mid(tmptotal, FirstStart, tmptotal.Length)
                GoTo ret
            End If

            Return res
        Else
            Return ErrorToString("{ERROR}")
        End If
    End Function

    Public Sub addTables(tb_data As String, targettable As TableLayoutPanel)
        '※ 테이블이 완전히 초기화 되었다는 전제 하에 수행!!
        TableForm.TableTitle.Text = midReturn(tb_data, "<title>", "</title>") '타이틀
        TableForm.c_num = Convert.ToInt32(midReturn(tb_data, "<column>", "</column>")) '열
        TableForm.r_num = Convert.ToInt32(midReturn(tb_data, "<row>", "</row>")) '행
        TableForm.days = midReturn(tb_data, "<days>", "</days>") '요일

        TableForm.tableready()

        If tb_data.Contains("<tb col=") Then '유효성 검사

            For i = 0 To targettable.ColumnCount
                For j = 0 To targettable.RowCount
                    Dim c As RichTextBox = targettable.GetControlFromPosition(i, j)
                    If c IsNot Nothing Then

                        Dim tmp1 = midReturn(tb_data, "<tb col='" + i.ToString + "' row='" + j.ToString + "'>", "</tb>")
                        c.Rtf = midReturn(tmp1, "<data>", "</data>")


                    End If
                Next
            Next
        End If
    End Sub

    '기본테이블 불러오기 전용-타이틀을 건드리지 않음
    Public Sub addTables_notitle(tb_data As String, targettable As TableLayoutPanel)
        '※ 테이블이 완전히 초기화 되었다는 전제 하에 수행!!
        Try
            'TableForm.TableTitle.Text = midReturn(tb_data, "<title>", "</title>") '타이틀
            TableForm.c_num = Convert.ToInt32(midReturn(tb_data, "<column>", "</column>")) '열
            TableForm.r_num = Convert.ToInt32(midReturn(tb_data, "<row>", "</row>")) '행
            TableForm.days = midReturn(tb_data, "<days>", "</days>") '요일

            TableForm.tableready()

            If tb_data.Contains("<tb col=") Then '유효성 검사

                For i = 0 To targettable.ColumnCount
                    For j = 0 To targettable.RowCount
                        Dim c As RichTextBox = targettable.GetControlFromPosition(i, j)
                        If c IsNot Nothing Then

                            Dim tmp1 = midReturn(tb_data, "<tb col='" + i.ToString + "' row='" + j.ToString + "'>", "</tb>")
                            c.Rtf = midReturn(tmp1, "<data>", "</data>")


                        End If
                    Next
                Next

            End If
        Catch ex As Exception
            MsgBox("오류가 발생했습니다. 파일의 유효성을 확인해 주십시오.", vbCritical)
        End Try

    End Sub

    Sub ExcelExport(maintable As TableLayoutPanel, daytable As TableLayoutPanel)
        Dim appXL As Excel.Application
        Dim wbXl As Excel.Workbook
        Dim shXL As Excel.Worksheet

        appXL = CreateObject("Excel.Application")
        appXL.Visible = True
        wbXl = appXL.Workbooks.Add
        shXL = wbXl.ActiveSheet


        For i = 0 To maintable.ColumnCount

            '요일 추가
            Dim c As Label = daytable.GetControlFromPosition(i, 0)
            If c IsNot Nothing Then
                shXL.Cells(1, i + 1).Value = c.Text
            End If

            For j = 0 To maintable.RowCount
                Dim d As RichTextBox = maintable.GetControlFromPosition(i, j)
                If d IsNot Nothing Then
                    shXL.Cells(j + 2, i + 1).Value = d.Text
                End If
            Next
        Next
    End Sub


    '특정날짜의 주 카운트를 리턴
    Dim _gc As GregorianCalendar = New GregorianCalendar()

    <Extension()>
    Function GetWeekOfMonth(ByVal time As DateTime) As Integer
        Dim first As DateTime = New DateTime(time.Year, time.Month, 1)
        Return time.GetWeekOfYear() - first.GetWeekOfYear() + 1
    End Function

    <Extension()>
    Private Function GetWeekOfYear(ByVal time As DateTime) As Integer
        Return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
        '혼동 방지를 위해 주의 시작을 월요일로 셋팅함.
    End Function

    '특정 날짜의 달의 토탈 주 카운트를 리턴
    Function GetWeekCount(time As DateTime) As Integer

        Dim daysInMonth = DateTime.DaysInMonth(time.Year, time.Month)

        Dim firstOfMonth As DateTime = New DateTime(time.Year, time.Month, 1)

        Dim firstDayOfMonth = firstOfMonth.DayOfWeek
        Dim weeksInMonth = Convert.ToInt32(Math.Ceiling((firstDayOfMonth + daysInMonth) / 7.0))

        Return weeksInMonth
    End Function

    Public Sub RenameAllTable(name As String, newname As String)

        Dim targetLoc As String = My.Settings.savelocation & "/" & name
        Dim oldTitle As String = "<title>" + name + "</title>"
        Dim newTitle As String = "<title>" + newname + "</title>"

        'FileText 읽을때 인코딩 꼭 주의!!!!

        Dim files As String() = IO.Directory.GetFiles(targetLoc, "*.stdata").Where(Function(x) File.ReadAllText(x, System.Text.Encoding.GetEncoding(949)).Contains(oldTitle)).ToArray
        For Each f As String In files
            Dim contents As String = File.ReadAllText(f, System.Text.Encoding.GetEncoding(949))
            File.WriteAllText(f, contents.Replace(oldTitle, newTitle), System.Text.Encoding.GetEncoding(949))
        Next

        My.Computer.FileSystem.RenameDirectory(targetLoc, newname)

    End Sub

    Public Function GetFoldersTitle(path As String) As String

        Dim files As String() = IO.Directory.GetFiles(path, "*.stdata").Where(Function(x) File.ReadAllText(x, System.Text.Encoding.GetEncoding(949)).Contains("<title>")).ToArray

        For Each f As String In files
            Dim contents As String = File.ReadAllText(f, System.Text.Encoding.GetEncoding(949))

            If contents.Contains("</title>") Then
                Return midReturn(contents, "<title>", "</title>")
                GoTo endtask
            End If
        Next

        Return Nothing
endtask:
    End Function

    Public Sub OverwriteTableTitle(tablename As String, year As Integer, month As Integer, week As Integer)
        Dim path = My.Settings.savelocation + "/" + tablename + "/" + year.ToString + "_" + month.ToString + "_" + week.ToString + ".stdata"
        Dim contents As String = File.ReadAllText(path, System.Text.Encoding.GetEncoding(949))


        If contents.Contains("<title>") And contents.Contains("</title>") Then
            Dim oldTitle = "<title>" + midReturn(contents, "<title>", "</title>") + "<title>"
            Dim newTitle As String = "<title>" + tablename + "</title>"
            File.WriteAllText(path, contents.Replace(oldTitle, newTitle), System.Text.Encoding.GetEncoding(949))
        Else
            MsgBox("시간표의 이름을 가져오는 과정에서 오류가 발생하였습니다.", vbCritical)
        End If

    End Sub

    Public Function GetTableTitle(tablename As String, year As Integer, month As Integer, week As Integer)

        Dim path = My.Settings.savelocation + "/" + tablename + "/" + year.ToString + "_" + month.ToString + "_" + week.ToString + ".stdata"
        Dim contents As String = File.ReadAllText(path, System.Text.Encoding.GetEncoding(949))

        If contents.Contains("<title>") And contents.Contains("</title>") Then
            Return midReturn(contents, "<title>", "</title>")
        Else
            MsgBox("시간표의 이름을 가져오는 과정에서 오류가 발생하였습니다.", vbCritical)
            Return Nothing
        End If

    End Function

    '디폴트테이블을 불렀을시 충돌방지를 위해 ->이제 디렉토리의 이름에서 Title을 불러오는 이상 문제는 없을것으로 생각
    'Public Sub ForceRenameMainTable(name As String)
    'Dim temp As String = My.Settings.defalutTable
    'Dim dtTitle As String = midReturn(temp, "<title>", "</title>")
    '
    'If Not dtTitle = name Then
    '        My.Settings.defalutTable = My.Settings.defalutTable.Replace("<title>" + dtTitle + "</title>", "<title>" + name + "</title>")
    'End If
    '
    'End Sub


#Region "기본 테이블"

    '기본 테이블의 텍스트를 가져옵니다.
    Public Function GetDefaultTable(name As String)
        Dim DefaultLoc As String = My.Settings.savelocation + "\" + name + "\default.stdata"

        If My.Computer.FileSystem.FileExists(DefaultLoc) Then
            'My.Settings.defalutTable = OptionSave()
            Return My.Computer.FileSystem.ReadAllText(DefaultLoc, System.Text.Encoding.GetEncoding(949))
        Else
            MsgBox("기본 시간표 데이터 찾기 실패", vbCritical)
            Return Nothing
        End If
    End Function

    '기본 테이블의 텍스트를 씁니다.
    Public Sub WriteDefaultTable(name As String)
        Dim DefaultLoc As String = My.Settings.savelocation + "\" + TableForm.TableTitle.Text + "\default.stdata"
        My.Computer.FileSystem.WriteAllText(DefaultLoc, TableForm.GetTableString, False, System.Text.Encoding.GetEncoding(949))
    End Sub

    '기본 테이블의 존재 여부를 확인합니다.
    Public Function IsDefaultTableExists(name As String, isitlocal As Boolean) As Boolean
        Dim DefaultLoc As String

        '로컬일시 WeekDataLoc, 지정 위치일시 name을 디렉토리로
        If isitlocal Then
            DefaultLoc = My.Settings.savelocation + "\" + name + "\default.stdata"
        Else
            DefaultLoc = name + "\default.stdata"
        End If

        If My.Computer.FileSystem.FileExists(DefaultLoc) Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region

#Region "파일 I/O"

    Public Function isPropername(name As String) As String
        Dim invalidChars() As Char = {"\", "/", ":", "*", "?", """", "<", ">", "|"}

        For Each invalidChar As String In invalidChars
            If name.Contains(invalidChar) Then
                Return False
            End If
        Next

        Return True
    End Function
#End Region

#Region "웹 접속"

    Public Function getHTML(url As String) '페이지에서 소스 불러오기
        Try
            Dim clie As New System.Net.WebClient()
            clie.Encoding = System.Text.Encoding.UTF8
            Dim sourceString As String = clie.DownloadString(url)
            Return sourceString

        Catch
            Return "{ERROR}"
        End Try
    End Function
#End Region

#Region "시작프로그램설정"

    Public Function checkStartUp() As Boolean
        Dim destlnk As String = Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "\ssaemtable.lnk"

        If IO.File.Exists(destlnk) Then
            If GetTargetPath(destlnk) = Application.ExecutablePath Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Sub SetStartup()
        Dim Path As String
        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)

        Path = Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "\ssaemtable.lnk"

        Dim wsh As Object = CreateObject("WScript.Shell")

        Dim MyShortcut
        MyShortcut = wsh.CreateShortcut(Path)
        MyShortcut.TargetPath = wsh.ExpandEnvironmentStrings(My.Application.Info.DirectoryPath & "\Ssaemtable.exe")
        MyShortcut.WindowStyle = 4
        MyShortcut.Save()
    End Sub

    Sub RemoveStartup()
        My.Computer.FileSystem.DeleteFile(Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "\ssaemtable.lnk")
    End Sub

    '바로가기 파일의 목적지경로를 리턴 -> Win7 포함한 일부 환경에 문제 있어 사용안함!!!
    'Public Function GetLnkTarget(lnkPath As String) As String
    ' Dim shl = New Shell32.Shell()
    '     lnkPath = System.IO.Path.GetFullPath(lnkPath)
    ' Dim dir = shl.[NameSpace](System.IO.Path.GetDirectoryName(lnkPath))
    ' Dim itm = dir.Items().Item(System.IO.Path.GetFileName(lnkPath))
    ' Dim lnk = DirectCast(itm.GetLink, Shell32.ShellLinkObject)
    ' Return lnk.Target.Path
    'End Function

    '바로가기 목적지경로 리턴 2
    Function GetTargetPath(ByVal FileName As String)
        Dim Obj As Object
        Obj = CreateObject("WScript.Shell")
        Dim Shortcut As Object
        Shortcut = Obj.CreateShortcut(FileName)
        GetTargetPath = Shortcut.TargetPath
    End Function
#End Region

End Module
