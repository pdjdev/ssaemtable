Public Class DateSelectForm
    Dim setWeek As Integer = 1

    Private Sub DateSelectForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim showx = TableForm.Location.X + TableForm.Size.Width / 2 - Me.Size.Width / 2
        Dim showy = TableForm.Location.Y + TableForm.Size.Height / 2 - Me.Size.Height / 2
        Me.SetDesktopLocation(showx, showy)

        DateTimePicker1.Value = DateTime.Today
        WeekLabelUpdate()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        WeekLabelUpdate()
    End Sub

    Sub WeekLabelUpdate()
        setWeek = DateTimePicker1.Value.GetWeekOfMonth

        Select Case setWeek
            Case 1
                Label1.Text = "첫째 주"
            Case 2
                Label1.Text = "둘째 주"
            Case 3
                Label1.Text = "셋째 주"
            Case 4
                Label1.Text = "넷째 주"
            Case 5
                Label1.Text = "다섯째 주"
            Case 6
                Label1.Text = "여섯째 주"
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TableForm.CheckWeekData(TableForm.TableTitle.Text, DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, setWeek, True) = False Then
            If MsgBox("해당 주의 테이블이 없습니다. 새로 만드시겠습니까?", vbQuestion + vbYesNo) = vbYes Then

                If IsDefaultTableExists(TableForm.TableTitle.Text, True) Then '기본 테이블 존재시
                    If MsgBox("기본 테이블로 새 테이블을 채우시겠습니까?" + vbCr + vbCr + "'예' 선택시 기본 테이블로, '아니오' 선택시 현재 테이블로 내용을 채웁니다.", vbQuestion + vbYesNo) = vbYes Then
                        Dim created = TableForm.CreateWeekData(TableForm.TableTitle.Text, DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, setWeek, 1)
                    Else
                        Dim created = TableForm.CreateWeekData(TableForm.TableTitle.Text, DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, setWeek, 2)
                    End If
                Else
                    If MsgBox("현재 테이블로 새 테이블을 채우시겠습니까?" + vbCr + vbCr + "'예' 선택시 현재 테이블로, '아니오' 선택시 빈 테이블로 내용을 채웁니다.", vbQuestion + vbYesNo) = vbYes Then
                        Dim created = TableForm.CreateWeekData(TableForm.TableTitle.Text, DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, setWeek, 1)
                    Else
                        Dim created = TableForm.CreateWeekData(TableForm.TableTitle.Text, DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, setWeek, 3)
                    End If
                End If



                If Not Created Then
                    MsgBox("오류 발생!")
                    GoTo donothing
                End If
            Else
                GoTo donothing
            End If
        End If

        TableForm.CurrentYear = DateTimePicker1.Value.Year
        TableForm.CurrentMonth = DateTimePicker1.Value.Month
        TableForm.CurrentWeek = setWeek

        If CheckBox1.Checked Then
            TableForm.CurrentDay = DateTimePicker1.Value.Day
            TableForm.ManuallySelectedDate = True
        End If

        TableForm.LoadWeekData(TableForm.TableTitle.Text, DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, setWeek)
        TableForm.DateLabelUpdate()

        Me.Close()
donothing:
    End Sub
End Class