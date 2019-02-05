Imports System.Runtime.InteropServices

Public Class Celldesign
    Dim first As Boolean = True
    Dim Loc As Point


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

    Private Sub FormDrag_MouseDown(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseDown, FormTitle.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Loc = e.Location
        End If
    End Sub

    Private Sub FormDrag_MouseMove(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseMove, FormTitle.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - Loc
        End If
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        TableForm.c_num = ClassNum.Value

        TableForm.tableready()
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles ApplyButton.Click
        'daynum은 체크된 요일ChkBox의 개수를 파악하기 위한 Integer변수,
        'daystr은 체크된 요일ChkBox의 Type를 파악하기 위한 1-7까지가 포함되어지는 String변수

        If ClassNum.Value > 20 Then
            If MsgBox("값이 너무 큽니다! 프로그램의 속도 저하를 초래할 수 있습니다." _
                      + vbCr + vbCr + "그래도 계속하시겠습니까?", vbExclamation + vbYesNo) = vbNo Then GoTo ignoretask
        End If

        Dim daynum As String = 0
        Dim daystr As String = Nothing

        If Mon_Chk.Checked Then
            daynum += 1
            daystr += "1"
        End If

        If Tue_Chk.Checked Then
            daynum += 1
            daystr += "2"
        End If

        If Wed_Chk.Checked Then
            daynum += 1
            daystr += "3"
        End If

        If Thu_Chk.Checked Then
            daynum += 1
            daystr += "4"
        End If

        If Fri_Chk.Checked Then
            daynum += 1
            daystr += "5"
        End If

        If Sat_Chk.Checked Then
            daynum += 1
            daystr += "6"
        End If

        If Sun_Chk.Checked Then
            daynum += 1
            daystr += "7"
        End If


        If daynum = 0 Then
            MsgBox("요일에 적어도 하나는 체크해야 합니다.", vbExclamation)
        ElseIf (TextBox1.Text = Nothing) And OverwriteCheckBox.Checked = False Then
            MsgBox("테이블 제목을 입력해 주십시오.", vbExclamation)
        ElseIf isPropername(TextBox1.Text) = False And OverwriteCheckBox.Checked = False Then
            MsgBox("다음 문자는 시간표 이름으로 지정하실 수 없습니다:" + vbCr + "\ / * ? "" < > |", vbCritical)
        ElseIf (TableForm.MainTable.GetControlFromPosition(0, 0) Is Nothing) And OverwriteCheckBox.Checked Then
            MsgBox("시간표가 지정되지 않은 상태에서 덮어씌울 수 없습니다.", vbCritical)
        Else

            If OverwriteCheckBox.Checked = False And My.Computer.FileSystem.DirectoryExists(My.Settings.savelocation & "/" & TextBox1.Text) Then
                If MsgBox("입력하신 이름이 이미 저장 폴더에 존재합니다. 덮어씌우시겠습니까?", vbQuestion + vbYesNo) = vbNo Then GoTo ignoretask
            End If

            '여기서 first = 기본채우기 rtxtbox를 털끝하나 안건드림 -> 빈 값이라는 말
            If first Then
                RichTextBox1.Rtf = Nothing
            End If

            TableForm.days = daystr
            TableForm.c_num = daynum
            TableForm.r_num = ClassNum.Value
            If OverwriteCheckBox.Checked = False Then TableForm.TableName = TextBox1.Text
            TableForm.defaulttexts = RichTextBox1.Rtf.ToString
            TableForm.tableready()

            '셀디자인 최근설정값 저장
            My.Settings.recent_celldesign = "<days>" + daystr + "</days><class>" + ClassNum.Value.ToString + "</class>"

            Me.Hide()

            If MsgBox("이 시간표를 기본 시간표로 설정하시겠습니까?", vbYesNo + vbQuestion) = vbYes Then
                TableForm.defaultset()
            End If

            Me.Close()
        End If

ignoretask:
    End Sub

    Private Sub Celldesign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opacity = 0

        Dim showx = TableForm.Location.X + TableForm.Size.Width / 2 - Me.Size.Width / 2
        Dim showy = TableForm.Location.Y + TableForm.Size.Height / 2 - Me.Size.Height / 2
        Me.SetDesktopLocation(showx, showy)

        TipText.SelectionAlignment = HorizontalAlignment.Right

        RichTextBox1.BackColor = ThemeCol.cell
        RichTextBox1.ForeColor = ThemeCol.foretext

        Select Case My.Settings.theme
            Case "white"
                RichTextBox1.Rtf = My.Resources.EXMP_RTF
            Case "black"
                RichTextBox1.Rtf = My.Resources.EXMP_RTF_BLACK
        End Select


        Dim tmpdays = midReturn(My.Settings.recent_celldesign, "<days>", "</days>")

        Mon_Chk.Checked = tmpdays.Contains("1")
        Tue_Chk.Checked = tmpdays.Contains("2")
        Wed_Chk.Checked = tmpdays.Contains("3")
        Thu_Chk.Checked = tmpdays.Contains("4")
        Fri_Chk.Checked = tmpdays.Contains("5")
        Sat_Chk.Checked = tmpdays.Contains("6")
        Sun_Chk.Checked = tmpdays.Contains("7")

        ClassNum.Value = Convert.ToInt16(midReturn(My.Settings.recent_celldesign, "<class>", "</class>"))


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        Dim temp1 As Form = New Celldesign
        temp1.Show()
        Me.Close()
    End Sub

    Private Sub FontDialog1_Apply(sender As Object, e As EventArgs) Handles FontDialog1.Apply

    End Sub

    Private Sub tm_selectedFontSet_Click(sender As Object, e As EventArgs) Handles tm_selectedFontSet.Click
        If RichTextBox1.SelectionFont IsNot Nothing Then
            FontDialog1.ShowColor = True

            FontDialog1.Font = RichTextBox1.SelectionFont
            FontDialog1.Color = RichTextBox1.SelectionColor

            If FontDialog1.ShowDialog() = DialogResult.OK Then
                RichTextBox1.SelectionFont = FontDialog1.Font
                RichTextBox1.SelectionColor = FontDialog1.Color
            End If
        End If
    End Sub

    Private Sub RichTextBox1_Enter(sender As Object, e As EventArgs) Handles RichTextBox1.Enter
        If first Then
            RichTextBox1.Rtf = Nothing
            first = False
        End If
    End Sub

    Private Sub TableMenuStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TableMenuStrip.Opening
        tm_copy.Enabled = True
        tm_paste.Enabled = True

        If first Then
            tm_copy.Enabled = False
        End If

        If Clipboard.GetText = Nothing Then
            tm_paste.Enabled = False
        End If
    End Sub

    Private Sub tm_clear_Click(sender As Object, e As EventArgs) Handles tm_clear.Click
        RichTextBox1.Clear()
    End Sub

    Private Sub tm_copy_Click(sender As Object, e As EventArgs) Handles tm_copy.Click
        RichTextBox1.SelectAll()
        RichTextBox1.Copy()
    End Sub

    Private Sub tm_paste_Click(sender As Object, e As EventArgs) Handles tm_paste.Click
        RichTextBox1.Paste()
    End Sub

    Private Sub Dateset_Enter(sender As Object, e As EventArgs) Handles Wed_Chk.MouseEnter, Tue_Chk.MouseEnter, Thu_Chk.MouseEnter, Sun_Chk.MouseEnter, Sat_Chk.MouseEnter, Mon_Chk.MouseEnter, Label2.MouseEnter, Label1.MouseEnter, Fri_Chk.MouseEnter, ClassNum.MouseEnter
        TipTitle.Text = "일정 설정"
        TipText.Text = "만들 시간표의 요일과 교시를 지정합니다. 요일은 일요일부터 표시되며, 교시는 최대 99까지 설정이 가능합니다."
    End Sub

    Private Sub CellTitle_Enter(sender As Object, e As EventArgs) Handles TextBox1.MouseEnter, Label7.MouseEnter, Label6.MouseEnter
        TipTitle.Text = "표 제목"
        TipText.Text = "시간표의 제목을 설정합니다. 향후 시간표의 데이터는 제목으로 구분하여 저장됩니다."
    End Sub

    Private Sub CellDef_Enter(sender As Object, e As EventArgs) Handles RichTextBox1.MouseEnter, Label5.MouseEnter
        TipTitle.Text = "표 기본 내용"
        TipText.Text = "시간표의 각각의 표에 자동으로 채워질 내용입니다. 한글과 같은 워드프로세서에서 양식을 그대로 붙일 수 있습니다. 오른쪽 클릭으로 글꼴을 지정합니다."
    End Sub

    Private Sub ResetButton_Enter(sender As Object, e As EventArgs) Handles ResetButton.MouseEnter
        TipTitle.Text = "초기화"
        TipText.Text = "입력한 값을 모두 초기화합니다."
    End Sub

    Private Sub ApplyButton_Enter(sender As Object, e As EventArgs) Handles ApplyButton.MouseEnter
        TipTitle.Text = "적용"
        TipText.Text = "입력한 값으로 시간표를 생성하고 적용합니다."
    End Sub

    Private Sub OverwriteCheckBox_Enter(sender As Object, e As EventArgs) Handles OverwriteCheckBox.MouseEnter
        TipTitle.Text = "현재 표에다" + vbCr + "덮어쓰기"
        TipText.Text = "입력한 값을 현재 열린 시간표에 덮어 씌웁니다. 체크 시 제목을 지정하실 수 없습니다."
    End Sub

    Private Sub TipText_TextChanged(sender As Object, e As EventArgs) Handles TipText.TextChanged
        TipText.SelectionAlignment = HorizontalAlignment.Right
    End Sub

    Private Sub OverwriteCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles OverwriteCheckBox.CheckedChanged
        TextBox1.Enabled = Not OverwriteCheckBox.Checked
    End Sub

    Private Sub TipTitle_TextChanged(sender As Object, e As EventArgs) Handles TipTitle.TextChanged
        If TipTitle.Text.Contains(vbCr) Then
            TipTitle.Height = dpicalc(Me, 46)
        Else
            TipTitle.Height = dpicalc(Me, 26)
        End If
    End Sub

#Region "도움말 이벤트"

#End Region
End Class