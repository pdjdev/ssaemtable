Imports System.Runtime.InteropServices

Public Class OptionForm
    Dim mode As Integer = 1
    Dim Loc As Point

    Dim ActiveColor As Color = Color.FromArgb(41, 182, 246)
    Dim inActiveColor As Color = Color.Gainsboro

    Dim def_saveloc As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\ssaemtable\weekdata"

    Dim debugcount = 0

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
            Case WM_WINDOWPOSCHANGING
                SnapToDesktopBorder(Me, m.LParam, 0)
        End Select

        MyBase.WndProc(m)
    End Sub

#End Region

#Region "페이드 효과" 'Load시 Opacity=0 꼭하기

    Private Sub FadeInEffect(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Refresh()
        FadeIn(Me, 1)
    End Sub

    Private Sub FadeOutEffect(sender As Object, e As EventArgs) Handles MyBase.Closing
        FadeOut(Me)
    End Sub
#End Region

    Private Sub FormDrag_MouseDown(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseDown, BottomPanel.MouseDown, Panel2.MouseDown, FormTitle.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Loc = e.Location
        End If
    End Sub

    Private Sub FormDrag_MouseMove(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseMove, BottomPanel.MouseMove, Panel2.MouseMove, FormTitle.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - Loc
        End If
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub OptionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opacity = 0
        Dim showx = TableForm.Location.X + TableForm.Size.Width / 2 - Me.Size.Width / 2
        Dim showy = TableForm.Location.Y + TableForm.Size.Height / 2 - Me.Size.Height / 2
        Me.SetDesktopLocation(showx, showy)

        SettingLoad()
        Modeset(1)
    End Sub

    Sub SettingLoad() 'myset에서 모든 값 불러오기

        'Tab1
        Chk1_1.Checked = checkStartUp()
        Chk1_2.Checked = My.Settings.hidestart
        TB1_1.Text = My.Settings.savelocation

        If My.Settings.savelocation = def_saveloc Then
            Chk1_3.Checked = False
        Else
            Chk1_3.Checked = True
        End If

        TB1_1.Enabled = Chk1_3.Checked
        BT1_1.Enabled = Chk1_3.Checked

        Select Case My.Settings.theme
            Case "white"
                CB1_1.SelectedIndex = 0
            Case "black"
                CB1_1.SelectedIndex = 1
        End Select


        'Tab2
        '표시 설정
        Chk2_1_1.Checked = My.Settings.tablestyle.Contains("1")
        Chk2_1_2.Checked = My.Settings.tablestyle.Contains("2")
        Chk2_1_3.Checked = My.Settings.isitlocked
        Chk2_1_4.Checked = My.Settings.tablestyle.Contains("4")

        '새주갱신시
        Select Case My.Settings.renewtype
            Case 1
                RB2_1_1.Checked = True
            Case 2
                RB2_1_2.Checked = True
            Case 3
                RB2_1_3.Checked = True
        End Select

        '신규생성시
        Select Case My.Settings.createtype
            Case 1
                RB2_2_1.Checked = True
            Case 2
                RB2_2_2.Checked = True
            Case 3
                RB2_2_3.Checked = True
            Case 4
                RB2_2_4.Checked = True
        End Select


        'Tab3
        Chk3_1_1.Checked = My.Settings.aeroeffect
        Chk3_1_2.Checked = My.Settings.fadeeffect


    End Sub

    Sub Applyset()

        'Tab1

        Try
            If Chk1_1.Checked Then
                SetStartup()
            Else
                If checkStartUp() Then
                    RemoveStartup()
                End If
            End If
        Catch ex As Exception
            MsgBox("시작프로그램 설정 중 오류가 발생했습니다." + vbCr + "해당 설정을 제외한 설정은 저장됩니다.", vbCritical)
        End Try

        Dim themestr As String = "white"

        Select Case CB1_1.SelectedIndex
            Case 0
                themestr = "white"
            Case 1
                themestr = "black"
        End Select

        If Not My.Settings.theme = themestr Then

            '일단 테마를 적용
            My.Settings.theme = themestr

            '이제 다 좋은데 문제는 '요일'을 적용해야 한다는 점
            '그렇기 때문에 별도 아래의 과정을 거쳐 요일'만' 초기화하도록 함
            For i = 0 To TableForm.MainTable.ColumnCount '시간표 제거 for문
                '요일 제거 (한번만 실행)
                Dim d As Control = TableForm.DayTable.GetControlFromPosition(i, 0)
                If d IsNot Nothing Then
                    TableForm.DayTable.Controls.Remove(d)
                    d = Nothing
                End If
            Next

            TableForm.DayTable.ColumnCount = TableForm.c_num

            For i = 0 To TableForm.c_num - 1

                TableForm.table_dayready(i)

            Next


            TableForm.ThemeApply()
        End If


        My.Settings.hidestart = Chk1_2.Checked
        If Chk1_3.Checked Then My.Settings.savelocation = TB1_1.Text '이전에 경로 유효성 검사 필수

        'Tab2
        '표시 설정
        Dim tmpValue1 As String = ""
        If Chk2_1_1.Checked Then tmpValue1 += "1"
        If Chk2_1_2.Checked Then tmpValue1 += "2"
        If Chk2_1_4.Checked Then tmpValue1 += "4"
        My.Settings.tablestyle = tmpValue1

        My.Settings.isitlocked = Chk2_1_3.Checked

        '새주갱신시
        If RB2_1_1.Checked Then
            My.Settings.renewtype = "1"
        ElseIf RB2_1_2.Checked Then
            My.Settings.renewtype = "2"
        ElseIf RB2_1_3.Checked Then
            My.Settings.renewtype = "3"
        End If

        '신규생성시
        If RB2_2_1.Checked Then
            My.Settings.createtype = "1"
        ElseIf RB2_2_2.Checked Then
            My.Settings.createtype = "2"
        ElseIf RB2_2_3.Checked Then
            My.Settings.createtype = "3"
        ElseIf RB2_2_4.Checked Then
            My.Settings.createtype = "4"
        End If


        'Tab3
        My.Settings.aeroeffect = Chk3_1_1.Checked
        My.Settings.fadeeffect = Chk3_1_2.Checked


        TableForm.OptionUpdate()
    End Sub

    Sub Modeset(mode As Integer)
        Select Case mode
            Case 1
                Tab1.BackColor = ActiveColor
                Tab2.BackColor = inActiveColor
                Tab3.BackColor = inActiveColor
                TabPanel1.Show()
                TabPanel2.Hide()
                TabPanel3.Hide()

            Case 2
                Tab1.BackColor = inActiveColor
                Tab2.BackColor = ActiveColor
                Tab3.BackColor = inActiveColor
                TabPanel1.Hide()
                TabPanel2.Show()
                TabPanel3.Hide()

            Case 3
                Tab1.BackColor = inActiveColor
                Tab2.BackColor = inActiveColor
                Tab3.BackColor = ActiveColor
                TabPanel1.Hide()
                TabPanel2.Hide()
                TabPanel3.Show()

        End Select
    End Sub

    Private Sub TabLabel1_Click(sender As Object, e As EventArgs) Handles TabLabel1.Click
        Modeset(1)

    End Sub

    Private Sub TabLabel2_Click(sender As Object, e As EventArgs) Handles TabLabel2.Click
        Modeset(2)

    End Sub

    Private Sub TabLabel3_Click(sender As Object, e As EventArgs) Handles TabLabel3.Click
        Modeset(3)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles DebugRunButton.Click
        Select Case DebugCmdBox.Text
            Case "optionform"
                SetupForm.Show()
            Case "showGUID"
                Dim sGUID As String
                sGUID = System.Guid.NewGuid.ToString()
                MessageBox.Show(sGUID)
        End Select
    End Sub

    Private Sub CheckBox9_CheckedChanged(sender As Object, e As EventArgs) Handles Chk1_3.CheckedChanged
        TB1_1.Enabled = Chk1_3.Checked
        BT1_1.Enabled = Chk1_3.Checked
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim closeRequired As Boolean = False

        '저장경로가 기본 설정 경로도 아니면서 존재하지 않는 경로일시
        If (Not TB1_1.Text = def_saveloc) And (Not IO.Directory.Exists(TB1_1.Text)) Then
            MsgBox("유효한 저장 경로를 지정해 주십시오.", vbExclamation)
            GoTo ignoretask
        End If

        If Not (TB1_1.Text = My.Settings.savelocation) Then
            If MsgBox("저장 경로를 변경하실 경우 프로그램이 종료되며 직접 이전 경로에서 시간표를 불러오셔야 합니다. 계속하시겠습니까?", vbQuestion + vbYesNo) = vbYes Then
                My.Settings.recentdataname = Nothing
                My.Settings.Save()
                closeRequired = True
            Else
                MsgBox("위치 변경 설정을 제외한 설정이 저장됩니다.", vbInformation)
                Chk1_3.Checked = False '이를 통해 경로변경 무효화 - 반드시 ApplySet전에하기
            End If
        End If

        If closeRequired Then
            TableForm.Close()
        End If

        Applyset()
        Me.Close()

ignoretask:
    End Sub

    Private Sub BT1_1_Click(sender As Object, e As EventArgs) Handles BT1_1.Click
        FolderBrowserDialog1.ShowDialog()
        If My.Computer.FileSystem.DirectoryExists(FolderBrowserDialog1.SelectedPath) Then
            TB1_1.Text = FolderBrowserDialog1.SelectedPath
        Else
            MsgBox("유효한 경로가 아닙니다", vbCritical)
        End If
    End Sub

    Private Sub Chk3_1_1_CheckedChanged(sender As Object, e As EventArgs) Handles Chk3_1_1.CheckedChanged
        If Not My.Settings.aeroeffect = Chk3_1_1.Checked Then
            MsgBox("Aero 활성화 변경은 다음 창 실행때부터 적용됩니다. (시간표 창의 경우 프로그램를 다시 시작해야 합니다)", vbInformation)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("정말로 초기화 하시겠습니까?", vbQuestion + vbYesNo) = vbYes Then
            My.Settings.Reset()
            My.Settings.Save()

            MsgBox("프로그램이 종료됩니다. 다시 실행해 주세요.", vbInformation)
            TableForm.Close()

        End If
    End Sub

    Private Sub CB1_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB1_1.SelectedIndexChanged
        Dim tempthemestr As String = "white"

        Select Case CB1_1.SelectedIndex
            Case 0
                tempthemestr = "white"
            Case 1
                tempthemestr = "black"
        End Select

        If Not My.Settings.theme = tempthemestr Then
            MsgBox("이미 시간표를 만든 상태에서 테마를 변경할 시 시간표의 글꼴 양식이 그대로 남기 때문에 배경과 글의 색상이 일치하여 내용이 보이지 않을 수 있습니다. 이는 직접 색상을 변경해 주시거나 새로 만드시면 됩니다." + vbCr + vbCr + "팁: 표를 우측 클릭한 후 '전체 설정' > '전체 색상 설정'을 통해 글 색상을 모두 변경하실 수 있습니다.", vbInformation)
        End If
    End Sub

    Private Sub ActiveDebug(sender As Object, e As EventArgs) Handles BT1.Click
        debugcount += 1

        If debugcount = 10 Then
            MsgBox("디버그 옵션 활성화")
            DebugPanel.Visible = True
        End If
    End Sub
End Class