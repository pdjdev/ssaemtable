<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateMgr
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UpdateMgr))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.NewVerLabel = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.ForceUpdButton = New System.Windows.Forms.Button()
        Me.UpdButton = New System.Windows.Forms.Button()
        Me.VerLabel = New System.Windows.Forms.Label()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.FormTitle = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UpdateChk = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TopPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.NewVerLabel)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.VerLabel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 35)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(15)
        Me.Panel1.Size = New System.Drawing.Size(384, 236)
        Me.Panel1.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ssaemtable.My.Resources.Resources.icon
        Me.PictureBox1.Location = New System.Drawing.Point(321, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(45, 45)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 43
        Me.PictureBox1.TabStop = False
        '
        'NewVerLabel
        '
        Me.NewVerLabel.AutoSize = True
        Me.NewVerLabel.Font = New System.Drawing.Font("맑은 고딕", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.NewVerLabel.Location = New System.Drawing.Point(19, 37)
        Me.NewVerLabel.Name = "NewVerLabel"
        Me.NewVerLabel.Size = New System.Drawing.Size(172, 20)
        Me.NewVerLabel.TabIndex = 42
        Me.NewVerLabel.Text = "최신 버전: (불러오는 중)"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.DimGray
        Me.Panel4.Location = New System.Drawing.Point(18, 66)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(8)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(350, 1)
        Me.Panel4.TabIndex = 41
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.RichTextBox1)
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Location = New System.Drawing.Point(18, 72)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(348, 146)
        Me.Panel3.TabIndex = 41
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.Color.White
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(348, 104)
        Me.RichTextBox1.TabIndex = 37
        Me.RichTextBox1.Text = ""
        '
        'Panel6
        '
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 104)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(348, 5)
        Me.Panel6.TabIndex = 40
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.ForceUpdButton)
        Me.Panel5.Controls.Add(Me.UpdButton)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 109)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(348, 37)
        Me.Panel5.TabIndex = 39
        '
        'ForceUpdButton
        '
        Me.ForceUpdButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ForceUpdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ForceUpdButton.Font = New System.Drawing.Font("맑은 고딕", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ForceUpdButton.Location = New System.Drawing.Point(0, 0)
        Me.ForceUpdButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ForceUpdButton.Name = "ForceUpdButton"
        Me.ForceUpdButton.Size = New System.Drawing.Size(98, 37)
        Me.ForceUpdButton.TabIndex = 38
        Me.ForceUpdButton.Text = "강제로 설치하기"
        Me.ForceUpdButton.UseVisualStyleBackColor = True
        '
        'UpdButton
        '
        Me.UpdButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.UpdButton.Enabled = False
        Me.UpdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.UpdButton.Location = New System.Drawing.Point(170, 0)
        Me.UpdButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.UpdButton.Name = "UpdButton"
        Me.UpdButton.Size = New System.Drawing.Size(178, 37)
        Me.UpdButton.TabIndex = 21
        Me.UpdButton.Text = "업데이트 다운로드, 설치"
        Me.UpdButton.UseVisualStyleBackColor = True
        '
        'VerLabel
        '
        Me.VerLabel.AutoSize = True
        Me.VerLabel.Font = New System.Drawing.Font("맑은 고딕", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.VerLabel.Location = New System.Drawing.Point(19, 12)
        Me.VerLabel.Name = "VerLabel"
        Me.VerLabel.Size = New System.Drawing.Size(77, 20)
        Me.VerLabel.TabIndex = 39
        Me.VerLabel.Text = "현재 버전:"
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.TopPanel.Controls.Add(Me.FormTitle)
        Me.TopPanel.Controls.Add(Me.Panel2)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(1, 1)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(384, 34)
        Me.TopPanel.TabIndex = 3
        '
        'FormTitle
        '
        Me.FormTitle.AutoSize = True
        Me.FormTitle.BackColor = System.Drawing.Color.Transparent
        Me.FormTitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.FormTitle.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.FormTitle.Location = New System.Drawing.Point(0, 0)
        Me.FormTitle.Margin = New System.Windows.Forms.Padding(3)
        Me.FormTitle.Name = "FormTitle"
        Me.FormTitle.Padding = New System.Windows.Forms.Padding(5, 5, 0, 5)
        Me.FormTitle.Size = New System.Drawing.Size(159, 31)
        Me.FormTitle.TabIndex = 5
        Me.FormTitle.Text = "업데이트 확인, 설치"
        Me.FormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.ssaemtable.My.Resources.Resources.bt_close2
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(350, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(34, 34)
        Me.Panel2.TabIndex = 3
        '
        'UpdateChk
        '
        '
        'UpdateMgr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(386, 272)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TopPanel)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UpdateMgr"
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "업데이트 확인, 설치"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents VerLabel As Label
    Friend WithEvents TopPanel As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents UpdButton As Button
    Friend WithEvents ForceUpdButton As Button
    Friend WithEvents NewVerLabel As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents UpdateChk As System.ComponentModel.BackgroundWorker
    Friend WithEvents FormTitle As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
