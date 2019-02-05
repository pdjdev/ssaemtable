Imports System.Runtime.InteropServices

Public Class DetailView
    Dim loc As Point
    Dim istextchanged As Boolean = False
    Dim changecount As Integer = 0

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

    Private Sub DetailView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opacity = 0
        RichTextBox1.ZoomFactor = My.Settings.detailview_zoom
        Dim showx = TableForm.Location.X + TableForm.Size.Width / 2 - Me.Size.Width / 2
        Dim showy = TableForm.Location.Y + TableForm.Size.Height / 2 - Me.Size.Height / 2
        Me.SetDesktopLocation(showx, showy)
        UpdateZoomFactor()
        ThemeApply()
    End Sub

#End Region

#Region "페이드 효과" 'Load시 Opacity=0 꼭하기

    Private Sub FadeInEffect(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Refresh()
        FadeIn(Me, 1)
    End Sub

    'FadeOut은 핸들러 중복되므로 X
#End Region

    Private Sub ResizePanel_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ResizePanel.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Size = New Size(Me.PointToClient(MousePosition).X, Me.PointToClient(MousePosition).Y)
        End If
    End Sub

    Private Sub FormDrag_MouseDown(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseDown, BottomPanel.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            loc = e.Location
        End If
    End Sub

    Private Sub FormDrag_MouseMove(sender As Object, e As MouseEventArgs) Handles TopPanel.MouseMove, BottomPanel.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += e.Location - loc
        End If
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles CloseBT.Click
        Me.Close()
    End Sub

    Private Sub DetailView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If istextchanged Then
            Dim msg1 = MsgBox("변경 사항을 저장하시겠습니까?", vbQuestion + vbYesNoCancel)
            If msg1 = MsgBoxResult.Yes Then
                ApplyData()
            ElseIf msg1 = MsgBoxResult.No Then
            Else
                e.Cancel = True
            End If
        End If

        If Not e.Cancel Then FadeOut(Me)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        If changecount = 3 Then
            istextchanged = True
        Else
            changecount += 1
        End If
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.S AndAlso e.Control = True Then
            istextchanged = False
            ApplyData()
            Me.Close()
        End If
    End Sub

    Private Sub ApplyData()
        Dim c As RichTextBox = TableForm.MainTable.GetControlFromPosition(TableForm.SelectedTableColumn, TableForm.selectedTableRow)
        c.Rtf = RichTextBox1.Rtf
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RichTextBox1.SelectAll()
        RichTextBox1.Copy()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        RichTextBox1.Clear()
        RichTextBox1.Paste()
    End Sub

    Private Sub SelectedFontOptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectedFontOptToolStripMenuItem.Click
        FontDialog1.ShowColor = True

        FontDialog1.Font = RichTextBox1.SelectionFont
        FontDialog1.Color = RichTextBox1.SelectionColor

        If FontDialog1.ShowDialog() = DialogResult.OK Then
            RichTextBox1.SelectionFont = FontDialog1.Font
            RichTextBox1.SelectionColor = FontDialog1.Color
        End If
    End Sub

    Sub ThemeApply()

        BackColor = ThemeCol.edge
        ForeColor = ThemeCol.foretext
        Panel1.BackColor = ThemeCol.main
        RichTextBox1.BackColor = ThemeCol.cell
        RichTextBox1.ForeColor = ThemeCol.foretext

        Select Case My.Settings.theme
            Case "black"
                CloseBT.BackgroundImage = My.Resources.b_bt_close2
                BT_zoom1.BackgroundImage = My.Resources.b_bt_zoom1
                BT_zoom2.BackgroundImage = My.Resources.b_bt_zoom2
                ResizePanel.BackgroundImage = My.Resources.b_edge
            Case "white"
                CloseBT.BackgroundImage = My.Resources.bt_close2
                BT_zoom1.BackgroundImage = My.Resources.bt_zoom1
                BT_zoom2.BackgroundImage = My.Resources.bt_zoom2
                ResizePanel.BackgroundImage = My.Resources.edge
        End Select
    End Sub

    Private Sub Panel6_Click(sender As Object, e As EventArgs) Handles BT_zoom1.Click
        RichTextBox1.ZoomFactor += 0.2
        UpdateZoomFactor()
    End Sub

    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles BT_zoom2.Click
        If RichTextBox1.ZoomFactor > 0.015625 + 0.2 Then
            RichTextBox1.ZoomFactor -= 0.2
            UpdateZoomFactor()
        End If
    End Sub

    Private Sub UpdateZoomFactor()
        If RichTextBox1.ZoomFactor = 1 Then
            ZoomFactorLabel.ForeColor = Color.Gray
        Else
            ZoomFactorLabel.ForeColor = Color.DodgerBlue
        End If
        ZoomFactorLabel.Text = Convert.ToInt32((RichTextBox1.ZoomFactor * 100)).ToString + "%"
        My.Settings.detailview_zoom = RichTextBox1.ZoomFactor
    End Sub

    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown, Me.KeyDown
        UpdateZoomFactor()
    End Sub

    Private Sub RichTextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyUp, Me.KeyUp
        UpdateZoomFactor()
    End Sub

    Private Sub ZoomFactorLabel_Click(sender As Object, e As EventArgs) Handles ZoomFactorLabel.Click
        RichTextBox1.ZoomFactor = 1
        UpdateZoomFactor()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        istextchanged = False
        ApplyData()
        Me.Close()
    End Sub
End Class