Imports System.Runtime.InteropServices

Public Class TableForm

#Region "변수"

    Public TableName As String = Nothing

    '행/렬
    Public c_num As Integer = 1
    Public r_num As Integer = 1

    '일수 (max 7)
    Public days As String = Nothing

    '기본 rtf값
    Public defaulttexts As String = Nothing

    '테이블 잠김 여부
    Public istablelocked As Boolean = False

    '슬라이딩 애니메이션 구현을 위한 임시변수
    Dim poscount As Integer = 0
    Dim prevloc As Point

    'GUI관련
    Dim loc As Point '창드래그 위치
    Dim showing As Boolean = True 'FadeAni작동여부 ->끝나고나면 false됨

    '선택한 테이블 RTB을 가리킬때 필요
    Dim selectedtable As RichTextBox = Nothing
    Dim isediting As Boolean = False
    'Dim exitallowed As Boolean = False
    Public SelectedTableColumn As Integer = 0
    Public selectedTableRow As Integer = 0

    '기본 weekdata 저장위치, 차후 사용자 지정 가능하게 바꿀 예정
    'Public WeekDataLoc As String = My.Application.Info.DirectoryPath & "\weekdata" ->MySettings로 사용자지정 구현

    '현재의 테이블 날짜
    Public CurrentYear As Integer = 2001
    Public CurrentMonth As Integer = 1
    Public CurrentWeek As Integer = 1
    Public CurrentDay As Day = Nothing

    '실제 현재의 테이블 날짜 (이거 타이머로 주기적으로 업뎃해야할것같음)
    Dim expdate As New DateTime
    Dim isCurrentToday As Boolean = True

    '직접 선택한 날짜인지 여부 -> 날짜 직접 선택시 일회적으로 표시가 가능케 하도록
    Public ManuallySelectedDate As Boolean = False
    '다 그리고 나면 다시 FALSE로 초기화!!!

#End Region

#Region "Aero 그림자 효과 (Vista이상)"

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        CreateDropShadow(Me)
        MyBase.OnHandleCreated(e)
    End Sub

#End Region

#Region "창붙기 효과"

    Private Const mSnapOffset As Integer = 30
    Private Const WM_WINDOWPOSCHANGING As Integer = &H46

    <StructLayout(LayoutKind.Sequential)>
    Public Structure WINDOWPOS
        Public hwnd As IntPtr
        Public hwndInsertAfter As IntPtr
        Public x As Integer
        Public y As Integer
        Public cx As Integer
        Public cy As Integer
        Public flags As Integer
    End Structure

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' Listen for operating system messages
        Select Case m.Msg
            Case WM_WINDOWPOSCHANGING And hideani.Enabled = False And Not showing
                SnapToDesktopBorder(Me, m.LParam, 0)
        End Select

        MyBase.WndProc(m)
    End Sub

#End Region

    '일반 상태에서는 프로그램이 종료될시 확인 ContextMenu를 띄우는데,
    '시스템 종료/로그오프시에는 종료를 막으면 곤란하므로 종료 상황 판별후 프로그램 종료를 Allow함
    'Public Sub Handler_SessionEnding(ByVal sender As Object, ByVal e As Microsoft.Win32.SessionEndingEventArgs)
    'If e.Reason = Microsoft.Win32.SessionEndReasons.Logoff Or e.Reason = Microsoft.Win32.SessionEndReasons.SystemShutdown Then
    '       exitallowed = True
    'End If
    'End Sub  //적용이 안돼서 그냥 일단 비활성화 해둠


#Region "패널 드래그, 달라붙기"

    Private Sub Panel2_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel2.MouseMove

        If e.Button = Windows.Forms.MouseButtons.Left Then
            SizeLabel.Text = Me.Width.ToString & "×" & Me.Height.ToString
            SizeLabel.Refresh()
            Me.Size = New Size(Me.PointToClient(MousePosition).X, Me.PointToClient(MousePosition).Y)
        End If

    End Sub

    Private Sub FormDrag_MouseDown(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseDown, BottomPanel.MouseDown, Panel4.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            loc = e.Location
        End If
    End Sub

    Private Sub FormDrag_MouseMove(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseMove, BottomPanel.MouseMove, Panel4.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - loc
        End If
    End Sub

    '이거없으면 창크기 저장 안되니 주의!!!!!
    Private Sub FormDrag_MouseUp(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseUp, BottomPanel.MouseUp, Panel4.MouseUp
        My.Settings.recent_formx = Location.X
        My.Settings.recent_formy = Location.Y
        My.Settings.Save()
    End Sub

    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        MainTable.SuspendLayout()
        SizeLabel.Show()
        SizeLabel.Text = "드래그하여 크기를 조절하세요"
    End Sub

    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp
        MainTable.ResumeLayout()
        My.Settings.recent_scalex = Me.Width
        My.Settings.recent_scaley = Me.Height
        My.Settings.Save()
        SizeLabel.Hide()
    End Sub
#End Region


#Region "테이블 관리"

    'CellDesign을 통해 만들시 실행...말고도 이동시에도 실행됨
    Public Sub tableready()

        '호출 횟수를 줄이기 위해 미리 불러놓기
        Dim ThemeBack As Color = ThemeCol.cell
        Dim ThemeBack_Locked As Color = ThemeCol.cell_locked
        Dim ThemeFore As Color = ThemeCol.foretext
        Dim DisableScrollBar As Boolean = My.Settings.tablestyle.Contains("4")

        FirstTipPanel.Visible = False
        MainTable.SuspendLayout()

        Clear()

        MainTable.ColumnStyles.Clear()
        MainTable.RowStyles.Clear()

        DayTable.ColumnStyles.Clear()
        DayTable.RowStyles.Clear()

        'TimeTable.ColumnStyles.Clear()
        TimeTable.RowStyles.Clear()

        MainTable.ColumnCount = c_num '열
        DayTable.ColumnCount = c_num '열(요일)
        MainTable.RowCount = r_num '행
        TimeTable.RowCount = r_num '행(교시)

        For i = 1 To c_num
            Me.MainTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / c_num))
            Me.DayTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / c_num))
        Next

        For i = 1 To r_num
            Me.MainTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / r_num))
            Me.TimeTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / r_num))
        Next

        '요일순서로 교시를 순서대로 추가
        For i = 0 To c_num - 1

            table_dayready(i)

            '매 테이블마다 행해지는 For
            For j = 0 To r_num - 1

                '테이블RichTextBox의 속성
                Dim DataLabel = New RichTextBox With {
                    .Name = i.ToString + " " + j.ToString,
                    .ForeColor = ThemeFore,
                    .Margin = New System.Windows.Forms.Padding(1),
                    .BorderStyle = System.Windows.Forms.BorderStyle.None,
                    .Dock = DockStyle.Fill,
                    .ContextMenuStrip = Me.TableMenuStrip,
                    .ReadOnly = istablelocked
                }

                '테이블RichTextBox의 이벤트
                AddHandler DataLabel.MouseDown, AddressOf TableRightClicked '우클릭시 이벤트 (예정 기능, ContextToolStrip을 대체)
                AddHandler DataLabel.TextChanged, AddressOf AutoSave
                AddHandler DataLabel.DoubleClick, AddressOf TableDoubleClicked '더블클릭시 이벤트

                If istablelocked Then
                    DataLabel.BackColor = ThemeBack_Locked
                Else
                    DataLabel.BackColor = ThemeBack
                End If

                If DisableScrollBar Then
                    DataLabel.ScrollBars = ScrollBars.None
                Else
                    DataLabel.ScrollBars = ScrollBars.Both
                End If

                MainTable.Controls.Add(DataLabel, i, j)
                DataLabel.Rtf = defaulttexts

            Next
        Next

        For j = 0 To r_num - 1
            Dim TimeLabel = New Label With {
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Fill,
                .Font = New System.Drawing.Font("맑은 고딕", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte)),
                .Text = (j + 1).ToString
            }

            TimeTable.Controls.Add(TimeLabel, 0, j)
        Next

        ManuallySelectedDate = False
        TableTitle.Text = TableName
        ErrorLabel.Hide()
        MainTable.ResumeLayout()
    End Sub

    Sub table_dayready(i As Integer)

        Dim DayLabel = New Label With {
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Fill,
                .Font = New System.Drawing.Font("맑은 고딕", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
            }
        Dim nowDay As DayOfWeek = Nothing

        Select Case Mid(days, i + 1, 1)
            Case 1
                DayLabel.Text = "월"
                nowDay = DayOfWeek.Monday
            Case 2
                DayLabel.Text = "화"
                nowDay = DayOfWeek.Tuesday
            Case 3
                DayLabel.Text = "수"
                nowDay = DayOfWeek.Wednesday
            Case 4
                DayLabel.Text = "목"
                nowDay = DayOfWeek.Thursday
            Case 5
                DayLabel.Text = "금"
                nowDay = DayOfWeek.Friday
            Case 6
                DayLabel.Text = "토"
                DayLabel.ForeColor = ThemeCol.weekend(0)
                nowDay = DayOfWeek.Saturday
            Case 7
                DayLabel.Text = "일"
                DayLabel.ForeColor = ThemeCol.weekend(1)
                nowDay = DayOfWeek.Sunday
        End Select


        If Not ManuallySelectedDate Then
            CurrentDay = Now.Day
        End If


        '현재 주 시간표일시
        If (CurrentYear = expdate.Year And CurrentMonth = expdate.Month And CurrentWeek = expdate.GetWeekOfMonth) Or ManuallySelectedDate Then
            '현재 요일을 하이라이트
            Dim dt As New DateTime(CurrentYear, CurrentMonth, CurrentDay)
            If dt.DayOfWeek = nowDay Then
                DayLabel.ForeColor = ThemeCol.accent_foretext
                DayLabel.BackColor = ThemeCol.accent_bg
            End If
        End If


        'MsgBox(Mid(days, i + 1, 1))
        DayTable.Controls.Add(DayLabel, i, 0)

        AddHandler DayLabel.MouseDown, AddressOf FormDrag_MouseDown
        AddHandler DayLabel.MouseMove, AddressOf FormDrag_MouseMove

    End Sub

    Sub Clear()

        For i = 0 To Me.MainTable.ColumnCount '시간표 제거 for문

            '요일 제거 (한번만 실행)
            Dim d As Control = Me.DayTable.GetControlFromPosition(i, 0)
            If d IsNot Nothing Then
                DayTable.Controls.Remove(d)
                d = Nothing
            End If

            For j = 0 To Me.MainTable.RowCount '각각의 행제거

                Dim c As Control = Me.MainTable.GetControlFromPosition(i, j)

                If c IsNot Nothing Then

                    MainTable.Controls.Remove(c)
                    c = Nothing

                End If
            Next
        Next


        For j = 0 To Me.TimeTable.RowCount '교시 제거

            Dim c As Control = Me.TimeTable.GetControlFromPosition(0, j)

            If c IsNot Nothing Then
                TimeTable.Controls.Remove(c)
                c = Nothing
            End If
        Next

    End Sub

    Public Sub LockTable() '시간표 잠그기
        For i = 0 To Me.MainTable.ColumnCount
            For j = 0 To Me.MainTable.RowCount

                Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)

                If c IsNot Nothing Then

                    c.ReadOnly = True
                    c.BackColor = ThemeCol.cell_locked()

                End If
            Next
        Next
        If My.Settings.theme = "black" Then
            BT4.BackgroundImage = My.Resources.b_bt_lock_true
        Else
            BT4.BackgroundImage = My.Resources.bt_lock_true
        End If

        istablelocked = True
    End Sub

    Public Sub UnlockTable() '시간표 잠금풀기
        For i = 0 To Me.MainTable.ColumnCount
            For j = 0 To Me.MainTable.RowCount

                Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)

                If c IsNot Nothing Then

                    c.ReadOnly = False
                    c.BackColor = ThemeCol.cell()

                End If
            Next
        Next
        If My.Settings.theme = "black" Then
            BT4.BackgroundImage = My.Resources.b_bt_lock_false
        Else
            BT4.BackgroundImage = My.Resources.bt_lock_false
        End If
        istablelocked = False
    End Sub

    Public Function GetTableString()

        Dim tb_string As String = vbCr

        For i = 0 To Me.MainTable.ColumnCount
            For j = 0 To Me.MainTable.RowCount
                Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)
                If c IsNot Nothing Then

                    tb_string += "<tb col='" + i.ToString + "' row='" + j.ToString + "'>" + vbCr

                    tb_string += settxt("data", c.Rtf.ToString)
                    tb_string += settxt("bgcolor", "FFFFFF")

                    tb_string += "</tb>" + vbCr

                End If
            Next
        Next

        Dim result = savetask(TableName,
                 MainTable.ColumnCount,
                 MainTable.RowCount,
                 days,
                 1,
                 tb_string,
                 Me.Width,
                 Me.Height,
                 Me.Location.X,
                 Me.Location.Y)

        Return result

    End Function

    Public Function GetTableString_Empty()

        Dim tb_string As String = vbCr

        For i = 0 To Me.MainTable.ColumnCount
            For j = 0 To Me.MainTable.RowCount
                Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)
                If c IsNot Nothing Then

                    tb_string += "<tb col='" + i.ToString + "' row='" + j.ToString + "'>" + vbCr

                    tb_string += settxt("data", Nothing)
                    tb_string += settxt("bgcolor", "FFFFFF")

                    tb_string += "</tb>" + vbCr

                End If
            Next
        Next

        Dim result = savetask(TableName,
                 MainTable.ColumnCount,
                 MainTable.RowCount,
                 days,
                 1,
                 tb_string,
                 Me.Width,
                 Me.Height,
                 Me.Location.X,
                 Me.Location.Y)

        Return result

    End Function

    Sub TableRightClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        selectedtable = sender
        Dim tableloation() = Split(selectedtable.Name, " ")

        ' 우클릭시 이벤트 (차후 ContextMenuStrip말고 따로 Form을 이 트리거를 통해 Show)
        If (e.Button = Windows.Forms.MouseButtons.Right) Then

            SelectedTableColumn = Convert.ToInt32(tableloation(0))
            selectedTableRow = Convert.ToInt32(tableloation(1))

        End If

    End Sub

    Sub TableDoubleClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        selectedtable = sender
        Dim tableloation() = Split(selectedtable.Name, " ")

        If Not istablelocked Then
            SelectedTableColumn = Convert.ToInt32(tableloation(0))
            selectedTableRow = Convert.ToInt32(tableloation(1))
            ShowDetailViewGUI()
        End If

    End Sub

    Private Sub SelectedtextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tm_selectedFontSet.Click
        If istablelocked Then
            MsgBox("잠금 상태에서는 글꼴 변경이 불가능합니다." + vbCr _
                   + "잠금을 해제하거나 편집 창에서 편집해 주세요.", vbExclamation)
        Else
            For i = 0 To Me.MainTable.ColumnCount
                For j = 0 To Me.MainTable.RowCount

                    Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)

                    If c IsNot Nothing Then
                        If c.SelectionFont IsNot Nothing And c.Focused Then
                            FontDialog1.ShowColor = True

                            FontDialog1.Font = c.SelectionFont
                            FontDialog1.Color = c.SelectionColor

                            If FontDialog1.ShowDialog() = DialogResult.OK Then
                                c.SelectionFont = FontDialog1.Font
                                c.SelectionColor = FontDialog1.Color
                            End If
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub bt4m_resetWeekdata_Click(sender As Object, e As EventArgs)
        Process.Start(My.Settings.savelocation + "\" + TableName)
    End Sub

    Private Sub bt4m_movetoCurrent_Click(sender As Object, e As EventArgs) Handles bt4m_movetoCurrent.Click
        setToCurrent()
        DateLabelUpdate()
        LoadWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek)
    End Sub

    Private Sub TableMenuStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TableMenuStrip.Opening
        tm_paste.Enabled = True
        tm_clear.Enabled = True

        If Clipboard.GetText = Nothing Then
            tm_paste.Enabled = False
        End If

        If istablelocked Then
            tm_paste.Enabled = False
            tm_clear.Enabled = False
        End If
    End Sub

    Private Sub tm_clear_Click(sender As Object, e As EventArgs) Handles tm_clear.Click
        Dim tmpdata As RichTextBox = Me.MainTable.GetControlFromPosition(SelectedTableColumn, selectedTableRow)
        tmpdata.Clear()
    End Sub

    Private Sub tm_copy_Click(sender As Object, e As EventArgs) Handles tm_copy.Click
        Dim tmpdata As RichTextBox = Me.MainTable.GetControlFromPosition(SelectedTableColumn, selectedTableRow)
        tmpdata.SelectAll()
        tmpdata.Copy()
    End Sub

    Private Sub tm_paste_Click(sender As Object, e As EventArgs) Handles tm_paste.Click
        Dim tmpdata As RichTextBox = Me.MainTable.GetControlFromPosition(SelectedTableColumn, selectedTableRow)
        tmpdata.Clear()
        tmpdata.Paste()
    End Sub

    Private Sub TmAllFont_Click(sender As Object, e As EventArgs) Handles TmAllFont.Click
        If istablelocked Then
            MsgBox("잠금 상태에서는 글꼴 변경이 불가능합니다." + vbCr _
                   + "잠금을 해제하거나 편집 창에서 편집해 주세요.", vbExclamation)
        Else
            FontDialog1.ShowColor = False

            If FontDialog1.ShowDialog() = DialogResult.OK Then
                For i = 0 To Me.MainTable.ColumnCount
                    For j = 0 To Me.MainTable.RowCount

                        Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)

                        If c IsNot Nothing Then

                            c.Font = FontDialog1.Font

                        End If
                    Next
                Next

            End If
        End If
    End Sub

    Private Sub TmAllColor_Click(sender As Object, e As EventArgs) Handles TmAllColor.Click
        If istablelocked Then
            MsgBox("잠금 상태에서는 색상 변경이 불가능합니다." + vbCr _
                   + "잠금을 해제하거나 편집 창에서 편집해 주세요.", vbExclamation)
        Else

            If ColorDialog1.ShowDialog() = DialogResult.OK Then
                For i = 0 To Me.MainTable.ColumnCount
                    For j = 0 To Me.MainTable.RowCount

                        Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)

                        If c IsNot Nothing Then

                            c.SelectAll()
                            c.SelectionColor = ColorDialog1.Color

                        End If
                    Next
                Next

            End If
        End If
    End Sub

#End Region


#Region "주간 테이블 관리"

    Public Sub LoadWeekData(table As String, year As Integer, month As Integer, week As Integer)
        Dim datapath = My.Settings.savelocation + "\" + table + "\" + year.ToString + "_" + month.ToString + "_" + week.ToString + ".stdata"

        If My.Computer.FileSystem.FileExists(datapath) Then

            Dim fileReader As String
            Try
                fileReader = My.Computer.FileSystem.ReadAllText(datapath, System.Text.Encoding.GetEncoding(949))
                addTables(fileReader, MainTable)
            Catch ex As Exception
                MsgBox("파일을 읽는 도중 문제가 발생했습니다." + vbCr + "이 오류가 지속된다면 새 시간표를 만들어 주세요.", vbCritical)
                FirstTipPanel.Hide()
                ErrorLabel.Show()
                GoTo endtask
            End Try

            '성공했으므로 지우기 (왜 여기냐면 LoadWeekData가 Fail할때 무조건 Tip을 띄울수도 없기때문)
            FirstTipPanel.Visible = False
            ErrorLabel.Visible = False
            TableName = table

        Else
            MsgBox("시간표 불러오기 실패" + vbCr + "이 오류가 지속된다면 새 시간표를 만들어 주세요.", vbCritical)
            FirstTipPanel.Hide()
            ErrorLabel.Show()
        End If
endtask:
        TableTitle.Text = TableName
    End Sub

    Public Function CheckWeekData(table As String, year As Integer, month As Integer, week As Integer, isitlocal As Boolean)
        If isitlocal Then '기존 위치임 -> WeekDataLoc에서 불러오기
            If System.IO.File.Exists(My.Settings.savelocation + "\" + table + "\" + year.ToString + "_" + month.ToString + "_" + week.ToString + ".stdata") Then
                Return True
            Else
                Return False
            End If
        Else '외부 위치임 (불러오기 상태) -> table string을 디렉토리로 생각하고 탐색
            If System.IO.File.Exists(table + "\" + year.ToString + "_" + month.ToString + "_" + week.ToString + ".stdata") Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Function CreateWeekData(table As String, year As Integer, month As Integer, week As Integer, type As Integer)
        Try
            If Not table = Nothing Then
                Dim datapath = My.Settings.savelocation + "\" + table + "\"
                Dim dataname = "\" + year.ToString + "_" + month.ToString + "_" + week.ToString + ".stdata"

                If (Not System.IO.Directory.Exists(datapath)) Then
                    System.IO.Directory.CreateDirectory(datapath)
                End If

                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter(datapath & dataname, False)

                Select Case type
                    Case 1 '기본 시간표로 작성
                        'ForceRenameMainTable(TableTitle.Text)
                        file.Write(GetDefaultTable(table))
                    Case 2 '현재 시간표로 작성
                        file.Write(GetTableString)
                    Case 3 '그냥 빈 시간표
                        file.Write(GetTableString_Empty)
                    Case Else
                        MsgBox("WeekData 생성 도중 올바른 type 값을 받지 못하였습니다.", vbCritical)
                        GoTo error1
                End Select

                file.Close()

                Return True
            Else

                MsgBox("시간표 이름을 읽을 수 없습니다.", vbCritical)
error1:
                Return False
            End If
            '새로 만들기
        Catch ex As Exception
            MsgBox("CreateWeekData 과정에서 문제가 발생하였습니다." + vbCr + ": " + ex.Message, vbCritical)
            Return False

        End Try

    End Function

    Sub MoveWeek(type As Integer)
        AutoSaveWaitTimer.Stop()

        If TableName Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        Else

            Dim prevName = TableName
            Dim prevYear = CurrentYear
            Dim prevMonth = CurrentMonth
            Dim prevWeek = CurrentWeek

            If type = 1 Then gotoPrevWeek()
            If type = 2 Then gotoNextWeek()

            '해당 시간표 존재 안할시
            If CheckWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, True) = False Then

                If MsgBox("해당 주의 시간표가 없습니다. 새로 만드시겠습니까?", vbQuestion + vbYesNo) = vbYes Then

                    Dim created As Boolean = False

                    Select Case My.Settings.createtype
                        Case "1" '기본시간표

                            '기본시간표 설정해놓고 기본시간표가 없을때
                            If IsDefaultTableExists(TableName, True) Then
                                created = CreateWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, 1)

                            Else
                                If MsgBox("새 시간표를 생성하기 위한 기본 시간표가 설정되지 않았습니다. 현재 시간표로 내용을 채우시겠습니까?", vbQuestion + vbYesNo) = vbYes Then
                                    created = CreateWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, 2)

                                End If
                            End If


                        Case "2" '이전시간표

                            created = CreateWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, 2)

                        Case "3" '빈것

                            created = CreateWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, 3)

                        Case "4" '옵션4

                            Dim tableType As Integer = 2 '2 = 자기복제

                            If IsDefaultTableExists(TableName, True) Then
                                If MsgBox("기본 시간표로 새 시간표를 채우시겠습니까?" + vbCr + vbCr + "'예' 선택시 기본 시간표로, '아니오' 선택시 현재 시간표로 내용을 채웁니다.", vbQuestion + vbYesNo) = vbYes Then
                                    tableType = 1
                                End If
                            End If

                            created = CreateWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, tableType)

                    End Select

                    If Not created Then
                        MsgBox("오류 발생!")

                        '오류발생시 롤백
                        CurrentYear = prevYear
                        CurrentMonth = prevMonth
                        CurrentWeek = prevWeek
                        GoTo donothing
                    End If

                Else

                    '작업취소로 롤백
                    CurrentYear = prevYear
                    CurrentMonth = prevMonth
                    CurrentWeek = prevWeek
                    GoTo donothing
                End If

            Else '존재할시

                Dim thisName = GetTableTitle(TableName, CurrentYear, CurrentMonth, CurrentWeek)

                '시간표의 이름이 같은지를 판별하여야 함
                If Not prevName = thisName Then
                    If MsgBox("해당 시간표를 찾았으나 시간표의 제목이 다릅니다." + vbCr _
                           + "해당 시간표의 이름을 " + thisName + "에서 " + prevName + "으로 바꾸시겠습니까?",
                              vbQuestion + vbYesNo) = vbYes Then
                        OverwriteTableTitle(TableName, CurrentYear, CurrentMonth, CurrentWeek)

                    Else
                        If MsgBox("시간표의 제목을 바꾸지 않을 시 시간표의 저장 위치가 바뀌게 되며 이는 충돌을 유발할 수 있습니다." + vbCr _
                               + "그래도 계속하시겠습니까?", vbExclamation + vbYesNo) = vbNo Then
                            CurrentYear = prevYear
                            CurrentMonth = prevMonth
                            CurrentWeek = prevWeek
                            GoTo donothing
                        End If
                    End If

                End If

            End If

            My.Settings.recentdataname = TableName

            DateLabelUpdate()
            CurrentDay = Now.Day
            LoadWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek)
        End If
donothing:
    End Sub

    '이전 주 이동 버튼 클릭 이벤트
    Private Sub PrevWeekBT_Click(sender As Object, e As EventArgs) Handles BT_PrevWeek.Click
        MoveWeek(1)
    End Sub

    '다음 주 이동 버튼 클릭 이벤트
    Private Sub NextWeekBT_Click(sender As Object, e As EventArgs) Handles BT_NextWeek.Click
        MoveWeek(2)
    End Sub

    Private Sub gotoPrevWeek()
        If CurrentMonth = 1 And CurrentWeek = 1 Then
            CurrentYear -= 1
            CurrentMonth = 12
            CurrentWeek = GetWeekCount(New DateTime(CurrentYear, CurrentMonth, 1))
        Else
            If CurrentWeek = 1 Then
                CurrentMonth -= 1
                CurrentWeek = GetWeekCount(New DateTime(CurrentYear, CurrentMonth, 1))
            Else
                CurrentWeek -= 1
            End If
        End If

    End Sub

    Private Sub gotoNextWeek()
        Dim MaxWeek = GetWeekCount(New DateTime(CurrentYear, CurrentMonth, 1))

        If CurrentMonth = 12 And CurrentWeek = MaxWeek Then
            CurrentYear += 1
            CurrentMonth = 1
            CurrentWeek = 1
        Else
            If CurrentWeek = MaxWeek Then
                CurrentMonth += 1
                CurrentWeek = 1
            Else
                CurrentWeek += 1
            End If
        End If
    End Sub

    Public Sub setToCurrent()
        CurrentYear = DateTime.Now.Year
        CurrentMonth = DateTime.Now.Month
        CurrentWeek = DateTime.Now.GetWeekOfMonth
    End Sub

#End Region


#Region "이벤트"

    Private Sub TableForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '버전 업데이트시 이전의 Settings를 업데이트하기위해
        If My.Settings.upgradeRequired Then
            My.Settings.Upgrade()
            My.Settings.upgradeRequired = False
            My.Settings.Save()
            My.Settings.Reload()
        End If

        If My.Settings.savelocation = "<none>" Then '미지정시 (처음일시)
            My.Settings.savelocation = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\ssaemtable\weekdata"

        ElseIf Not (My.Settings.savelocation = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\ssaemtable\weekdata") Then '기본 로케이션이 아닐시
            If Not IO.Directory.Exists(My.Settings.savelocation) Then '그 로케이션이 유효하지 않을시
                MsgBox("시간표 저장 위치가 유효하지 않습니다. 기본 위치로 변경합니다.", vbCritical)
                My.Settings.savelocation = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\ssaemtable\weekdata"
            End If
        End If

        Opacity = 0

        Label1.Text = DateTime.Now.Year.ToString + "-" _
            + DateTime.Now.Month.ToString + "-" _
            + DateTime.Now.Day.ToString + DateTime.Now.ToString(" dddd")

        expdate = Now
        '일단은 현재로 표시일자

        CurrentYear = expdate.Year
        CurrentMonth = expdate.Month
        CurrentWeek = expdate.GetWeekOfMonth
        CurrentDay = expdate.Day

        DateLabelUpdate()
        ThemeApply()

    End Sub

    Private Sub AutoSave()
        AutoSaveLabel.Show()
        AutoSaveWaitTimer.Stop()
        AutoSaveWaitTimer.Start()
    End Sub

    Private Sub AutoSaveWaitTimer_Tick(sender As Object, e As EventArgs) Handles AutoSaveWaitTimer.Tick
        'BackgroundWorker1.RunWorkerAsync(MainTable)
        '백그라운드에서 저장 해보려 했는데 사실 큰 렉도 없고 Deligate 너무 까다로워서 포기
        AutoSaveLabelFade.Start()
        AutoSaveWaitTimer.Stop()
        NowTheme = My.Settings.theme
        My.Settings.recentdataname = TableName

        If Not TableName = Nothing Then '테이블name이 암것도아닐때 > 초기상태이므로 저장하면 안되니까
            CreateWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, 2)
        End If

    End Sub

    Private Sub TableForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        LoadFormStyle()
        OptionUpdate()

        Me.Refresh()

        If My.Settings.hidestart Then
            WindowState = FormWindowState.Minimized
            Opacity = My.Settings.opacity
        Else
            FadeIn(Me, My.Settings.opacity)
        End If


        showing = False

    End Sub

    'My.settings에서 옵션을 적용하 
    Public Sub OptionUpdate()

        If My.Settings.isitlocked Then
            LockTable()
            My.Settings.isitlocked = True
        Else
            UnlockTable()
            My.Settings.isitlocked = False
        End If

        DayPanel.Visible = My.Settings.tablestyle.Contains("1")
        EmptyPanel.Visible = My.Settings.tablestyle.Contains("2") '그 요일패널 모퉁이에 있는 빈 공간->요일만 있을시 버리도록
        TimeTable.Visible = My.Settings.tablestyle.Contains("2")


        Dim DisableScroll = My.Settings.tablestyle.Contains("4")

        For i = 0 To Me.MainTable.ColumnCount
            For j = 0 To Me.MainTable.RowCount

                Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)

                If c IsNot Nothing Then

                    If DisableScroll Then
                        c.ScrollBars = ScrollBars.None
                    Else
                        c.ScrollBars = ScrollBars.Both
                    End If

                End If
            Next
        Next


    End Sub

    '프로그램 종료전 로컬에 시간표 저장
    'My.Settings값은 일괄 저장 처리가 자동으로 되므로 따로 저장할 필요는 없을듯
    Private Sub TableForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If (Not TableName = Nothing) And (Not My.Settings.recentdataname = Nothing) Then
            CreateWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, 2)
        End If
        FadeOut(Me)
    End Sub

#End Region


#Region "GUI"

    Public Sub DateLabelUpdate()

        CurrentDayLabel.Text = CurrentYear & ", " & CurrentMonth.ToString & "월 "

        Dim monthstr As String = Nothing

        Select Case CurrentWeek
            Case 1
                monthstr = "첫째 주"
            Case 2
                monthstr = "둘째 주"
            Case 3
                monthstr = "셋째 주"
            Case 4
                monthstr = "넷째 주"
            Case 5
                monthstr = "다섯째 주"
            Case 6
                monthstr = "여섯째 주"
        End Select

        CurrentDayLabel.Text += monthstr

        If CurrentYear = expdate.Year And CurrentMonth = expdate.Month And CurrentWeek = expdate.GetWeekOfMonth Then
            CurrentDayLabel.ForeColor = ThemeCol.accent_foretext
            isCurrentToday = True
        Else
            CurrentDayLabel.ForeColor = ThemeCol.foretext
            isCurrentToday = False
        End If

    End Sub

    Private Sub LoadFormStyle() '현재로써는 Shown하고 실행되는 일회성 Sub이므로 혼동 금지!!!
        If My.Settings.isitfirst Then '초기설정시 ->그냥 기본 폼값(Settings에 있는걸로) 따르기
            '(단, 자동 DPI변경값을 반영하기 위해 최초실행시 바로 창크기 저장, 이는 서피스 프로와 같은 HDPI 디바이스에게 필수적)
            My.Settings.recent_scalex = Me.Width
            My.Settings.recent_scaley = Me.Height
            My.Settings.recent_formx = Me.Location.X
            My.Settings.recent_formy = Me.Location.Y
            My.Settings.isitfirst = False
        Else
            Me.SetDesktopLocation(My.Settings.recent_formx, My.Settings.recent_formy)
        End If

        Me.Width = My.Settings.recent_scalex
        Me.Height = My.Settings.recent_scaley

        '처음 실행 시에는 무조건 오늘날을 기준으로 하므로

        '참고로 RecentData에서 RecentDataName으로 My.Settings 값 이름을 바꾸면서 용도도 Data 통째로 저장하는 방식에서
        '그냥 Table을 Local에서 지칭하는 역할을 하는 TableTitleText값만 저장하도록 하였음.

        '그리고 자연스럽게 쓰면서 새로운 날이 갱신되면 기본Table로 리셋되도록 함.
        TableName = My.Settings.recentdataname


        If Not TableName = Nothing Then
            '오늘날의data 있을시
            If CheckWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, True) Then
                LoadWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek)
            Else
                '차후 갱신시 어떻게채울지 선택하는 옵션 추가예정

                Select Case My.Settings.renewtype

                    Case "1" '기본테이블로

                        '유효성 검사를 위한 파일 읽기
                        If IO.File.Exists(My.Settings.savelocation + "\" + TableName + "\default.stdata") Then
                            Dim DefaultTxt = IO.File.ReadAllText(My.Settings.savelocation + "\" + TableName + "\default.stdata",
                                                                 System.Text.Encoding.GetEncoding(949))

                            '기본시간표가 유효한경우
                            If DefaultTxt.Contains("<ssaemtable>") Then
                                Load_defTable()
                            Else
                                '기본week가 있긴 한데 유효하지 않은경우 ->저번주일자 찾아보기
                                Load_lastTable()
                            End If
                        Else
                            '기본week가 없는 경우
                            Load_lastTable()
                        End If

                    Case "2" '이전주로

                        Load_lastTable()


                    Case "3" '그냥 빈것
                        Load_emptyTable()
                End Select

            End If
        Else '만약 최근 기록이 아무것도 없다면 (최초실행이라면)
            FirstTipPanel.Visible = True

            If Not checkStartUp() And My.Settings.startupwarn Then
                StartupTip.Show()
            End If
            '처음팁패널 표시
        End If
    End Sub

    '테이블 불러오기 3타입
    Sub Load_defTable()
        CreateWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, 1)
        LoadWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek)
    End Sub

    Sub Load_lastTable()
        Dim prevYear = CurrentYear
        Dim prevMonth = CurrentMonth
        Dim prevWeek = CurrentWeek

        gotoPrevWeek()

        If CheckWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek, True) Then
            IO.File.Copy(My.Settings.savelocation + "\" + TableName.ToString + "\" + CurrentYear.ToString + "_" + CurrentMonth.ToString + "_" + CurrentWeek.ToString + ".stdata",
             My.Settings.savelocation + "\" + TableName + "\" + prevYear.ToString + "_" + prevMonth.ToString + "_" + prevWeek.ToString + ".stdata", True)
        Else
            Load_emptyTable()
        End If

        CurrentYear = prevYear
        CurrentWeek = prevWeek
        CurrentMonth = prevMonth

        LoadWeekData(TableName, prevYear, prevMonth, prevWeek)
    End Sub

    Sub Load_emptyTable()
        Clear()
        FirstTipPanel.Show()
    End Sub


    Private Sub BT1_Click(sender As Object, e As EventArgs) Handles BT1.Click
        BT1_menu.Show(Cursor.Position)
    End Sub

    Private Sub bt1m_new_Click(sender As Object, e As EventArgs) Handles bt1m_new.Click
        Celldesign.Show()
    End Sub

    Private Sub bt1m_clear_Click(sender As Object, e As EventArgs) Handles bt1m_reset.Click
        If MsgBox("기존 시간표은 저장하지 않을 시 지워집니다. 계속하시겠습니까?", vbQuestion + vbYesNo) = vbYes Then
            DetailView.Close()
            TableTitle.Text = "시간표를 설정하세요"
            TableName = Nothing
            RenamePanel.Visible = False
            TableTitle.Visible = True
            FirstTipPanel.Visible = True
            BT_titleedit.BackgroundImage = My.Resources.bt_titleedit
            My.Settings.recentdataname = Nothing

            isediting = False
            Clear()
        End If
    End Sub

    Private Sub BT4_Click(sender As Object, e As EventArgs) Handles BT4.Click
        If Me.MainTable.GetControlFromPosition(0, 0) Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        Else
            If istablelocked Then
                UnlockTable()
                My.Settings.isitlocked = False
            Else
                LockTable()
                My.Settings.isitlocked = True
            End If
        End If
    End Sub

    Private Sub BT_titleedit_Click(sender As Object, e As EventArgs) Handles BT_titleedit.Click

        'errortask : 작업 도중 오류가 일어난 상태로, 롤백을 함
        'endtask : 롤백을 건너뛰고 작업을 중단

        If Me.MainTable.GetControlFromPosition(0, 0) Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        ElseIf istablelocked Then
            MsgBox("잠금 상태에서는 제목 변경이 불가능합니다.", vbExclamation)
        Else
            If isediting Then
                If Not RenameTitleTextBox.Text = Nothing Then

                    If isPropername(RenameTitleTextBox.Text) Then '금지 문자열 포함 여부 체크

                        '이름이 다를!! 시
                        If Not TableName = RenameTitleTextBox.Text Then

                            '이미 존재하는 디렉토리 체크
                            If My.Computer.FileSystem.DirectoryExists(My.Settings.savelocation & "/" & RenameTitleTextBox.Text) Then
                                MsgBox("지정하신 이름과 동일한 시간표가 이미 저장 위치에 있습니다." + vbCr _
                                   + vbCr + "해당 시간표 폴더를 지우시거나 다른 이름으로 시간표를 지정해 주시고, 기존 시간표에 새 내용 추가를 원하실 경우 해당 시간표 폴더를 연 후 직접 새 시간표를 생성해 주시기 바랍니다.", vbExclamation)
                                GoTo errortask
                            End If

                            If MsgBox("나머지 주간 시간표의 이름도 모두 바꾸시겠습니까?" + vbCr + vbCr + "(변경하지 않을 시 이름이 변경된 현재 시간표를 제외한 다른 주의 시간표은 모두 무시되고 새로 생성해야 합니다.)", vbQuestion + vbYesNo) = vbYes Then
                                '예 -> 나머지 이름 모두 교체
                                Try
                                    RenameAllTable(TableName, RenameTitleTextBox.Text)
                                    TableName = RenameTitleTextBox.Text
                                Catch ex As Exception
                                    MsgBox("처리 도중 문제가 발생했습니다." + vbCr + ": " + ex.Message + vbCr + vbCr + "시간표의 위치가 유효한지, 또는 다른 프로그램에 의해 사용 중인지 확인해 주시고 다시 한번 시도해 주세요.", vbCritical)
                                    TableName = TableTitle.Text
                                    GoTo errortask
                                End Try
                            Else
                                '아니오 -> 현재만교체
                                TableName = RenameTitleTextBox.Text
                            End If
                        End If

                        TableTitle.Text = TableName
                        RenamePanel.Visible = False
                        TableTitle.Visible = True
                        BT_titleedit.BackgroundImage = My.Resources.bt_titleedit
                        isediting = False
                        AutoSave()
                    Else

                        MsgBox("다음 문자는 시간표 이름으로 지정하실 수 없습니다:" + vbCr + "\ / * ? "" < > |", vbCritical)
                        GoTo errortask
                    End If

                Else
                    MsgBox("시간표의 이름을 입력해 주세요.", vbCritical)
                    GoTo errortask
                End If
            Else
                RenameTitleTextBox.Text = TableTitle.Text
                RenamePanel.Width = TableTitle.Width
                If RenamePanel.Width < dpicalc(Me, 50) Then RenamePanel.Width = dpicalc(Me, 50)
                RenamePanel.Visible = True
                TableTitle.Visible = False
                BT_titleedit.BackgroundImage = My.Resources.bt_titleapply
                isediting = True
            End If
        End If

        GoTo stoptask
errortask:
        RenamePanel.Visible = False
        TableTitle.Visible = True
        BT_titleedit.BackgroundImage = My.Resources.bt_titleedit
        isediting = False
stoptask:
    End Sub

    Private Sub 클립보드에복사ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 클립보드에복사ToolStripMenuItem.Click
        If Me.MainTable.GetControlFromPosition(0, 0) Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        Else
            Clipboard.SetText(GetTableString)
            MsgBox("클립보드에 복사되었습니다.", vbInformation)
        End If
    End Sub

    Private Sub 파일로저장stdataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 파일로저장stdataToolStripMenuItem.Click
        If Me.MainTable.GetControlFromPosition(0, 0) Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        Else
            SaveFileDialog1.FileName = TableName
            SaveFileDialog1.ShowDialog()
        End If
    End Sub

    Dim o_color As Color = Color.DodgerBlue
    Dim Perc As Double = 1
    Dim NowTheme As String = My.Settings.theme


    Private Sub AutoSaveLabelFade_Tick(sender As Object, e As EventArgs) Handles AutoSaveLabelFade.Tick

        If Perc < 1.5 - 0.1 Then
            Perc += 0.1

            If NowTheme = "white" Then
                AutoSaveLabel.ForeColor = ControlPaint.Light(o_color, Perc)
            ElseIf NowTheme = "black" Then
                AutoSaveLabel.ForeColor = ControlPaint.Dark(o_color, Perc - 1)
            End If

        Else
            AutoSaveLabelFade.Stop()
            AutoSaveLabel.Hide()
            AutoSaveLabel.ForeColor = o_color
            Perc = 1
        End If

    End Sub

    Private Sub AutoSaveLabel_Click(sender As Object, e As EventArgs) Handles AutoSaveLabel.Click
        AutoSaveLabel.ForeColor = ControlPaint.Light(AutoSaveLabel.ForeColor, 2)
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Select Case ToolStripComboBox1.SelectedIndex
            Case 0 '100
                Opacity = 1
            Case 1 '90
                Opacity = 0.9
            Case 2 '80
                Opacity = 0.8
            Case 3 '70
                Opacity = 0.7
            Case 4 '60
                Opacity = 0.6
            Case 5 '50
                Opacity = 0.5
            Case 6 '40
                Opacity = 0.4
            Case 7 '30
                Opacity = 0.3
            Case 8 '20
                Opacity = 0.2
        End Select

        My.Settings.opacity = Opacity
    End Sub


    Private Sub BT3_menu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles BT3_menu.Opening
        Select Case My.Settings.opacity
            Case 1
                ToolStripComboBox1.SelectedIndex = 0
            Case 0.9
                ToolStripComboBox1.SelectedIndex = 1
            Case 0.8
                ToolStripComboBox1.SelectedIndex = 2
            Case 0.7
                ToolStripComboBox1.SelectedIndex = 3
            Case 0.6
                ToolStripComboBox1.SelectedIndex = 4
            Case 0.5
                ToolStripComboBox1.SelectedIndex = 5
            Case 0.4
                ToolStripComboBox1.SelectedIndex = 6
            Case 0.3
                ToolStripComboBox1.SelectedIndex = 7
            Case 0.2
                ToolStripComboBox1.SelectedIndex = 8
        End Select
    End Sub

    Private Sub BT2_Click(sender As Object, e As EventArgs) Handles BT2.Click
        BT2_menu.Show(Cursor.Position)
    End Sub

    Private Sub BT3_Click(sender As Object, e As EventArgs) Handles BT3.Click
        BT3_menu.Show(Cursor.Position)
    End Sub

    Private Sub ExitBT_Click(sender As Object, e As EventArgs) Handles ExitBT.Click
        ExitBT_menu.Show(Cursor.Position)
    End Sub

    Private Sub ItemYes_Click(sender As Object, e As EventArgs) Handles ItemYes.Click
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim log As String = Nothing
        log += My.Settings.recent_formx.ToString + vbCr
        log += My.Settings.recent_formy.ToString + vbCr
        log += My.Settings.recent_scalex.ToString + vbCr
        log += My.Settings.recent_scaley.ToString + vbCr

        MsgBox(log)
    End Sub

    Private Sub bt2m_update_Click(sender As Object, e As EventArgs) Handles bt2m_update.Click
        UpdateMgr.Show()
    End Sub

    Private Sub DetailviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tm_openEditor.Click
        If Me.MainTable.GetControlFromPosition(0, 0) Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        Else
            ShowDetailViewGUI()
        End If
    End Sub

    Sub ShowDetailViewGUI()
        DetailView.Text = Me.DayTable.GetControlFromPosition(SelectedTableColumn, 0).Text.ToString _
            + "요일, " + (selectedTableRow + 1).ToString + "번째"
        DetailView.DetailViewTitle.Text = Me.DayTable.GetControlFromPosition(SelectedTableColumn, 0).Text.ToString _
            + "요일, " + (selectedTableRow + 1).ToString + "번째"
        Dim tmpdata As RichTextBox = Me.MainTable.GetControlFromPosition(SelectedTableColumn, selectedTableRow)
        DetailView.RichTextBox1.Rtf = tmpdata.Rtf
        DetailView.Show()
    End Sub

    Private Sub 정보ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles bt2m_info.Click
        InfoForm.Show()
        'MsgBox(My.Resources.VERTEXT, vbInformation, "프로그램 정보")
    End Sub

    Private Sub TableTitle_TextChanged(sender As Object, e As EventArgs) Handles TableTitle.TextChanged
        Me.Text = TableTitle.Text
    End Sub

    Private Sub bt_hide_Click(sender As Object, e As EventArgs) Handles bt_hide.Click
        prevloc = Me.Location
        hideani.Start()
    End Sub

    Private Sub hideani_Tick(sender As Object, e As EventArgs) Handles hideani.Tick
        If poscount >= 15 Then
            Me.Opacity = 0
            poscount = 0
            hideani.Stop()
            Me.SetDesktopLocation(prevloc.X, prevloc.Y)
            WindowState = FormWindowState.Minimized
            Me.Opacity = My.Settings.opacity
        Else
            Me.SetDesktopLocation(Location.X, Location.Y + dpicalc(Me, poscount))
            poscount += 1
            Opacity -= My.Settings.opacity / 15
        End If
    End Sub

    Private Sub SizeLabel_Resize(sender As Object, e As EventArgs) Handles SizeLabel.Resize

    End Sub

    Private Sub bt2m_option_Click(sender As Object, e As EventArgs) Handles bt2m_option.Click
        OptionForm.Show()
    End Sub

    Private Sub CurrentDayLabel_Click_1(sender As Object, e As EventArgs) Handles CurrentDayLabel.Click
        If Me.MainTable.GetControlFromPosition(0, 0) Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        Else
            BT4_menu.Show(Cursor.Position)
        End If
    End Sub

    Private Sub BT4_menu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles BT4_menu.Opening
        bt4m_movetoCurrent.Enabled = Not isCurrentToday
    End Sub

    Private Sub bt4m_selectDate_Click(sender As Object, e As EventArgs) Handles bt4m_selectDate.Click
        DateSelectForm.Show()
    End Sub

    '시간표 편집 버튼(BT1)의 마우스 이벤트 반응 Sub
    Private Sub BT1_MouseEnter(sender As Object, e As EventArgs) Handles BT1.MouseEnter
        BT1.BackColor = ThemeCol.main
    End Sub

    Private Sub BT1_MouseLeave(sender As Object, e As EventArgs) Handles BT1.MouseLeave
        BT1.BackColor = ThemeCol.littledark
    End Sub

    Private Sub BT1_MouseDown(sender As Object, e As MouseEventArgs) Handles BT1.MouseDown
        BT1.BackColor = ThemeCol.dark
    End Sub

    '시작프로그램 팁 관련
    Private Sub ignoreTipChk_CheckedChanged(sender As Object, e As EventArgs) Handles ignoreTipChk.CheckedChanged
        My.Settings.startupwarn = Not ignoreTipChk.Checked
    End Sub

    Private Sub SetStartupLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles SetStartupLink.LinkClicked
        If MsgBox("쌤테이블을 시작프로그램으로 설정하시겠습니까?" + vbCr + "(이후 설정에서 변경이 가능합니다)", vbQuestion + vbYesNo) = vbYes Then

            Try
                SetStartup()
            Catch ex As Exception
                MsgBox("문제가 발생했습니다. 다시 시도해 주세요.", vbCritical)
                GoTo donothing
            End Try

            StartupTip.Visible = False

donothing:
        End If
    End Sub

    '테마 적용
    Sub ThemeApply()

        BackColor = ThemeCol.edge
        ForeColor = ThemeCol.foretext
        BodyPanel.BackColor = ThemeCol.main
        MainTable.BackColor = ThemeCol.main
        BT1.BackColor = ThemeCol.littledark
        DateLabelUpdate()

        Select Case My.Settings.theme
            Case "black"
                BT1.BackgroundImage = My.Resources.b_bt1_table
                BT2.BackgroundImage = My.Resources.b_bt2_opt
                BT3.BackgroundImage = My.Resources.b_bt3_opacity
                ExitBT.BackgroundImage = My.Resources.b_bt_close
                Panel2.BackgroundImage = My.Resources.b_edge
                BT_titleedit.BackgroundImage = My.Resources.b_bt_titleedit
                bt_hide.BackgroundImage = My.Resources.b_bt_hide
                BT_PrevWeek.BackgroundImage = My.Resources.b_bt_prev
                BT_NextWeek.BackgroundImage = My.Resources.b_bt_next
            Case "white"
                BT1.BackgroundImage = My.Resources.bt1_table
                BT2.BackgroundImage = My.Resources.bt2_opt
                BT3.BackgroundImage = My.Resources.bt3_opacity
                ExitBT.BackgroundImage = My.Resources.bt_close
                Panel2.BackgroundImage = My.Resources.edge
                BT_titleedit.BackgroundImage = My.Resources.bt_titleedit
                bt_hide.BackgroundImage = My.Resources.bt_hide
                BT_PrevWeek.BackgroundImage = My.Resources.bt_prev
                BT_NextWeek.BackgroundImage = My.Resources.bt_next
        End Select

    End Sub

    '도움말 페이지 실행
    Private Sub bt2m_help_Click(sender As Object, e As EventArgs) Handles bt2m_help.Click
        Process.Start("http://ssaemtable.kro.kr/help")
    End Sub

#End Region


#Region "파일 I/O"

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim fileReader As String
        Dim changetitle As Boolean = True

        Try
            fileReader = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName, System.Text.Encoding.GetEncoding(949))
        Catch ex As Exception
            MsgBox("파일을 읽는 도중 문제가 발생했습니다.", vbCritical)
            GoTo ignoretask
        End Try


        Dim filetitle As String = Nothing

        '파일에 title을 포함하고 있을 시에
        If fileReader.Contains("<title>") Then
            filetitle = midReturn(fileReader, "<title>", "</title>")

            If Not TableName = Nothing Then '현재 테이블 이름이 공백이 아닌 경우

                '시간표 이름 변경 묻기 대화상자
                If (Not filetitle = TableName) And (Not filetitle = Nothing) Then
                    If MsgBox("현재 시간표 이름을 해당 파일 시간표 이름으로(" + filetitle + ") 변경하시겠습니까?" _
                               + vbCr + vbCr + "경고: 변경시 기존 시간표(" + TableName + ")는 보존된 채 저장 경로가 변경되며, 변경하지 않을 시 기존 시간표에 해당 시간표가 병합됩니다.", vbQuestion + vbYesNo) = vbNo Then
                        changetitle = False
                    End If
                End If

            End If
            Clear()
        Else
            MsgBox("올바른 시간표 파일이 아닙니다.", vbCritical)
            GoTo ignoretask
        End If


        If changetitle Then
            addTables(fileReader, MainTable)

            'title 포함은 했는데 공백일시에 그냥 적용 안하는걸로
            If Not filetitle = Nothing Then
                TableName = filetitle
                TableTitle.Text = TableName
            End If
        Else
            addTables_notitle(fileReader, MainTable)
        End If


ignoretask:
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(SaveFileDialog1.FileName, False)
            file.Write(GetTableString)
            file.Close()
        Catch ex As Exception
            MsgBox("저장 도중 문제가 발생했습니다.", vbCritical)
        End Try
    End Sub

    '시간표 비우기 (표 형태 유지)

    Private Sub bt1m_clear_Click_1(sender As Object, e As EventArgs) Handles bt1m_clear.Click
        If MsgBox("시간표의 모든 내용을 지웁니다. 계속하시겠습니까?", vbQuestion + vbYesNo) = vbYes Then
            For i = 0 To Me.MainTable.ColumnCount
                For j = 0 To Me.MainTable.RowCount

                    Dim c As RichTextBox = Me.MainTable.GetControlFromPosition(i, j)

                    If c IsNot Nothing Then

                        c.Clear()

                    End If
                Next
            Next
        End If
    End Sub

    Private Sub bt1m_read_file_Click(sender As Object, e As EventArgs) Handles bt1m_read_file.Click
        If Me.MainTable.GetControlFromPosition(0, 0) IsNot Nothing Then
            If MsgBox("기존 시간표은 저장하지 않을 시 지워집니다. 계속하시겠습니까?" + vbCr + vbCr + "팁: 동일 이름의 특정 주 시간표를 탐색하고 싶다면 '" + CurrentDayLabel.Text + "'를 클릭한 뒤 '직접 날짜 선택'을, 다른 시간표의 특정 시간표를 탐색하고 싶다면 '주간 시간표 불러오기'를 통해 불러온 뒤 직접 탐색해 주세요.", vbQuestion + vbYesNo) = vbNo Then
                GoTo ignoretask
            End If
        End If

        If Clipboard.GetText.Contains("<ssaemtable>") Then
            If MsgBox("클립보드에서 서식 파일을 발견했습니다. 클립보드에서 불러오시겠습니까?", vbQuestion + vbYesNo) = vbYes Then

                Dim prevTable As String = GetTableString()

                Clear()
                Try
                    addTables(Clipboard.GetText, MainTable)
                Catch ex As Exception
                    MsgBox("오류가 발생했습니다. 유효한 시간표가 아니거나 내부 오류가 발생하였을 수 있습니다.", vbCritical)
                    addTables(prevTable, MainTable)
                End Try

                GoTo ignoretask
            End If
        End If

        OpenFileDialog1.ShowDialog()

ignoretask:
    End Sub

    Private Sub bt1m_read_folder_Click(sender As Object, e As EventArgs) Handles bt1m_read_folder.Click
        If Me.MainTable.GetControlFromPosition(0, 0) IsNot Nothing Then
            If MsgBox("기존 시간표은 저장하지 않을 시 지워집니다. 계속하시겠습니까?", vbQuestion + vbYesNo) = vbNo Then
                GoTo donothing
            End If
        End If

        FolderBrowserDialog1.SelectedPath = My.Settings.savelocation + "\"
        FolderBrowserDialog1.Description = "주간 테이블이 모여 있는 폴더를 선택해 주세요."
        Dim BrowseFolder = FolderBrowserDialog1.ShowDialog()

        If BrowseFolder = DialogResult.OK Then

            '롤백용 값저장 임시변수
            Dim prevYear = CurrentYear
            Dim prevMonth = CurrentMonth
            Dim prevweek = CurrentWeek


            setToCurrent()

            '해당 폴더의 이름 추출
            Dim foldername As String = GetFoldersTitle(FolderBrowserDialog1.SelectedPath)
            '> 원래는 그냥 folder명을 타이틀이름으로 수집하려 했지만 그러면 파일내 실제 테이블타이틀과 불일치시 꼬여버리는 문제가 발생하여 가장처음 stdata에서 읽는 방식으로 교체

            '해당 폴더 안의 모든 .stdata 파일 이름 추출
            Dim lastname As String = Nothing
            Dim files As String() = IO.Directory.GetFiles(FolderBrowserDialog1.SelectedPath, "*.stdata")

            '일단 애초에 테이블파일이 있는지부터 Check -> 제목추출Function을 이용
            If foldername = Nothing Then
                MsgBox("폴더 내에 시간표 파일이 없거나 손상된 것 같습니다.", vbCritical)
                CurrentYear = prevYear
                CurrentMonth = prevMonth
                CurrentWeek = prevweek
                GoTo donothing
            End If

            '이번주 테이블 존재여부확인
            If CheckWeekData(FolderBrowserDialog1.SelectedPath, CurrentYear, CurrentMonth, CurrentWeek, False) Then

                '이번주가 존재 -> 묻지도 따지지도 않고 로컬에 복사후 현재날짜설정
                CopyFolder(FolderBrowserDialog1.SelectedPath, foldername)
                setToCurrent() '이번주파일존재 ->현재주설정

            Else
                If MsgBox("이번 주의 시간표가 없습니다. " _
                          + foldername + "의 기본 시간표로 오늘 시간표를 채우시겠습니까?" _
                          + vbCr + vbCr + "'아니오'를 누를 시 폴더 내의 가장 최근 시간표를 불러옵니다.",
                          vbQuestion + vbYesNo) = vbYes Then

                    If IsDefaultTableExists(FolderBrowserDialog1.SelectedPath, False) Then
                        '이제 기본 시간표를 불러와야하므로 
                        CopyFolder(FolderBrowserDialog1.SelectedPath, foldername)

                        If Not CreateWeekData(foldername, CurrentYear, CurrentMonth, CurrentWeek, 1) Then
                            '기본 시간표로 만들기 실패로 롤백
                            CurrentYear = prevYear
                            CurrentMonth = prevMonth
                            CurrentWeek = prevweek
                            GoTo donothing
                        End If

                        setToCurrent()
                    Else
                        MsgBox("기본 시간표가 없습니다. 가장 최근 시간표를 불러옵니다.", vbInformation)
                        GoTo getRecentTable
                    End If
                Else
getRecentTable:
                    '가장최초의 파일을 읽어오기
                    For Each f As String In files
                        If Not f.Substring(f.LastIndexOf("\") + 1).Contains("default") Then '디폴트 폴더는 빼내야함
                            lastname = f.Substring(f.LastIndexOf("\") + 1)
                        End If
                    Next

                    If lastname = Nothing Then '찾고보니 없을시
                        '작업취소로 롤백
                        CurrentYear = prevYear
                        CurrentMonth = prevMonth
                        CurrentWeek = prevweek
                        GoTo donothing
                    Else
                        lastname = Mid(lastname, 1, lastname.Length - 7) '*.stdata 를 빼기 위한 목적!
                        Dim recentTable() = lastname.Split("_")

                        '가장나중 날짜 파일 이름 구하기
                        CurrentYear = recentTable(0)
                        CurrentMonth = recentTable(1)
                        CurrentWeek = recentTable(2)

                        If CheckWeekData(FolderBrowserDialog1.SelectedPath, CurrentYear, CurrentMonth, CurrentWeek, False) = False Then
                            MsgBox("올바른 시간표 파일을 찾을 수 없습니다.", vbCritical)
                            '작업취소로 롤백
                            CurrentYear = prevYear
                            CurrentMonth = prevMonth
                            CurrentWeek = prevweek
                            GoTo donothing
                        Else
                            CopyFolder(FolderBrowserDialog1.SelectedPath, foldername)
                            'CreateWeekData(foldername, CurrentYear, CurrentMonth, CurrentWeek, 1)
                        End If
                    End If
                End If
            End If

            My.Settings.recentdataname = foldername
            DateLabelUpdate()
            TableName = foldername


            LoadWeekData(TableName, CurrentYear, CurrentMonth, CurrentWeek)
        End If
donothing:
    End Sub

    Sub CopyFolder(orgloc As String, name As String)
        If Not orgloc = (My.Settings.savelocation + "\" + name) Then
            My.Computer.FileSystem.CopyDirectory(orgloc, My.Settings.savelocation + "\" + name, True)
        End If
    End Sub

    Private Sub 시간표폴더열기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 시간표폴더열기ToolStripMenuItem.Click
        Process.Start(My.Settings.savelocation + "\" + TableName)
    End Sub

    Private Sub 폴더로내보내기전체주ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 폴더로내보내기전체주ToolStripMenuItem.Click
        FolderBrowserDialog1.Description = "주간 테이블 폴더를 저장할 경로를 선택해 주세요."
        Dim BrowseFolder = FolderBrowserDialog1.ShowDialog()

        Try
            If BrowseFolder = DialogResult.OK Then

                If My.Computer.FileSystem.DirectoryExists(FolderBrowserDialog1.SelectedPath + "\" + TableName) Then
                    If MsgBox("이미 같은 이름의 폴더(" + TableName + ")가 존재합니다. 계속 하시겠습니까?", vbQuestion + vbYesNo) = vbNo Then
                        GoTo donothing
                    End If
                End If

                My.Computer.FileSystem.CopyDirectory(My.Settings.savelocation + "\" + TableName, FolderBrowserDialog1.SelectedPath + "\" + TableName, True)
            Else
                GoTo donothing
            End If
        Catch ex As Exception
            MsgBox("내보내기에 실패하였습니다." + vbCr + ": " + ex.Message, vbExclamation)
            GoTo donothing
        End Try


        If MsgBox("성공적으로 내보내었습니다. 폴더를 열어보시겠습니까?", vbQuestion + vbYesNo) = vbYes Then Process.Start("explorer.exe", "/select," & FolderBrowserDialog1.SelectedPath + "\" + TableName + "\")


donothing:

    End Sub

    Private Sub 엑셀로내보내기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 엑셀로내보내기ToolStripMenuItem.Click
        If Me.MainTable.GetControlFromPosition(0, 0) Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        Else
            If MsgBox("시간표의 글꼴 양식은 보존되지 않습니다." + vbCr + "계속하시겠습니까?", vbQuestion + vbYesNo) = vbYes Then
                Try
                    ExcelExport(MainTable, DayTable)
                Catch ex As Exception
                    MsgBox("오류가 발생했습니다." + vbCr + "Microsoft Excel이 설치되어 있는지, 시간표의 데이터가 유효한지 확인해 주시기 바랍니다.", vbCritical)
                End Try
            End If
        End If

    End Sub

#End Region


#Region "기본 시간표 설정"

    Sub defaultset()
        Dim DefaultLoc As String = My.Settings.savelocation + "\" + TableName + "\default.stdata"

        If Not IO.Directory.Exists(My.Settings.savelocation + "\" + TableName) Then
            IO.Directory.CreateDirectory(My.Settings.savelocation + "\" + TableName)
        End If

        If Not My.Computer.FileSystem.FileExists(DefaultLoc) Then
            'My.Settings.defalutTable = GetTableString()
            My.Computer.FileSystem.WriteAllText(DefaultLoc, GetTableString, False, System.Text.Encoding.GetEncoding(949))
        Else
            If MsgBox("기존에 설정된 기본 시간표은 지위집니다. 계속하시겠습니까?", vbYesNo + vbQuestion) = vbYes Then
                My.Computer.FileSystem.WriteAllText(DefaultLoc, GetTableString, False, System.Text.Encoding.GetEncoding(949))
            End If
        End If
    End Sub

    Private Sub bt1m2_defaultset_Click(sender As Object, e As EventArgs) Handles bt1m2_defaultset.Click
        If Me.MainTable.GetControlFromPosition(0, 0) Is Nothing Then
            MsgBox("시간표를 만들어 주세요.", vbInformation)
        Else
            defaultset()
        End If
    End Sub

    Private Sub bt1m2_settodefault_Click(sender As Object, e As EventArgs) Handles bt1m2_settodefault.Click
        Dim DefaultLoc As String = My.Settings.savelocation + "\" + TableName + "\default.stdata"

        If My.Computer.FileSystem.FileExists(DefaultLoc) Then
            If Me.MainTable.GetControlFromPosition(0, 0) IsNot Nothing Then
                If MsgBox("기존 시간표은 저장하지 않을 시 지워집니다. 계속하시겠습니까?", vbQuestion + vbYesNo) = vbNo Then GoTo ignoretask
            End If

            Dim DefaultData As String = My.Computer.FileSystem.ReadAllText(DefaultLoc, System.Text.Encoding.GetEncoding(949))
            addTables(DefaultData, MainTable)

        Else
            MsgBox("기본 시간표가 설정되지 않았습니다.", vbExclamation)
        End If
ignoretask:
    End Sub
#End Region

    '프로젝트 시작: 2018 2 7
    '본 프로그램은 고해상도 DPI 디스플레이 환경에서 작동을 고려하여 제작되었음
    '박동준 PBJSoftware
    '예정기능: 환경설정 구현, 검은 테마 추가

End Class
