Imports System.Runtime.InteropServices

Public Class UpdateMgr
    Dim nowver As String = My.Application.Info.Version.ToString
    Dim newver As String = Nothing
    Dim newinfo As String = Nothing
    Dim updateok As Boolean = True
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

    Private Sub UpdateMgr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opacity = 0
        VerLabel.Text = My.Application.Info.Version.ToString
        Dim showx = TableForm.Location.X + TableForm.Size.Width / 2 - Me.Size.Width / 2
        Dim showy = TableForm.Location.Y + TableForm.Size.Height / 2 - Me.Size.Height / 2
        Me.SetDesktopLocation(showx, showy)

        VerLabel.Text = "현재 버전: " + nowver
        UpdateChk.RunWorkerAsync()
    End Sub

    Private Sub UpdateChk_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles UpdateChk.DoWork
        Try
            Dim info = getHTML("http://status.ssaemtable.kro.kr/")
            newver = midReturn(info, "<version>", "</version>")
            newinfo = midReturn(info, "<note>", "</note>")
        Catch ex As Exception
            updateok = False
        End Try
    End Sub

    Private Sub UpdateChk_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles UpdateChk.RunWorkerCompleted
        RichTextBox1.Text = newinfo
        NewVerLabel.Text = "최신 버전: " + newver


        If updateok Then
            If UpdateAvailable() Then
                UpdButton.Enabled = True
                NewVerLabel.Text += " (업데이트 가능)"
            Else
                VerLabel.Text += " (최신 버전입니다)"
            End If
        Else
            NewVerLabel.Text = "(업데이트 확인 실패)"
        End If

    End Sub

    Function UpdateAvailable()
        If Convert.ToInt32(Replace(nowver, ".", Nothing)) < Convert.ToInt32(Replace(newver, ".", Nothing)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub UpdButton_Click(sender As Object, e As EventArgs) Handles UpdButton.Click
        If MsgBox("업데이트를 설치하시겠습니까?" + vbCr + "(프로그램이 종료될 수 있습니다)" + vbCr + vbCr + "참고: 정상적으로 다운/설치가 진행되지 않을 경우 설정 아이콘 > '도움말...' 로 가 쌤테이블 페이지로 가 직접 받으시기 바랍니다.", vbQuestion + vbYesNo) = vbYes Then
            StartUpdateMgr()
        End If
    End Sub

    Private Sub ForceUpdButton_Click(sender As Object, e As EventArgs) Handles ForceUpdButton.Click
        If MsgBox("정말로 강제로 설치하시겠습니까?" + vbCr + "(프로그램이 종료될 수 있습니다)" + vbCr + vbCr + "참고: 정상적으로 다운/설치가 진행되지 않을 경우 설정 아이콘 > '도움말...' 로 가 쌤테이블 페이지로 가 직접 받으시기 바랍니다.", vbQuestion + vbYesNo) = vbYes Then
            StartUpdateMgr()
        End If
    End Sub

    Sub StartUpdateMgr()
        If IO.File.Exists(My.Application.Info.DirectoryPath + "\st_updatemgr.exe") Then
            My.Settings.Save()
            My.Settings.Reload()

            Dim procStartInfo As New ProcessStartInfo
            Dim procExecuting As New Process

            With procStartInfo
                .UseShellExecute = True
                .FileName = My.Application.Info.DirectoryPath + "\st_updatemgr.exe"
                .WindowStyle = ProcessWindowStyle.Normal
                .Verb = "runas" '권한상승을 위해 필요
            End With

            '실행 요청
            Try
                procExecuting = Process.Start(procStartInfo)
            Catch ex2 As Exception '오류 발생시 (대개 실행거부)
                MsgBox(ex2.Message, vbCritical)
                GoTo donothing
            End Try

            Me.Close()
        Else
            MsgBox("업데이트 프로그램을 찾을 수 없습니다.", vbCritical)
        End If
donothing:
    End Sub

End Class