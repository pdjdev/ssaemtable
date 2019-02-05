<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DetailView
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DetailView))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectedFontOptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BottomPanel = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ResizePanel = New System.Windows.Forms.Panel()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.BT_zoom1 = New System.Windows.Forms.Panel()
        Me.ZoomFactorLabel = New System.Windows.Forms.Label()
        Me.BT_zoom2 = New System.Windows.Forms.Panel()
        Me.DividePanel = New System.Windows.Forms.Panel()
        Me.DetailViewTitle = New System.Windows.Forms.Label()
        Me.CloseBT = New System.Windows.Forms.Panel()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.BottomPanel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TopPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.BottomPanel)
        Me.Panel1.Controls.Add(Me.TopPanel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(492, 370)
        Me.Panel1.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.RichTextBox1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 34)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(492, 296)
        Me.Panel5.TabIndex = 2
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.Color.White
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(492, 296)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedFontOptToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.ShowImageMargin = False
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(126, 30)
        '
        'SelectedFontOptToolStripMenuItem
        '
        Me.SelectedFontOptToolStripMenuItem.Name = "SelectedFontOptToolStripMenuItem"
        Me.SelectedFontOptToolStripMenuItem.Size = New System.Drawing.Size(125, 26)
        Me.SelectedFontOptToolStripMenuItem.Text = "글꼴 설정"
        '
        'BottomPanel
        '
        Me.BottomPanel.Controls.Add(Me.Panel3)
        Me.BottomPanel.Controls.Add(Me.Button1)
        Me.BottomPanel.Controls.Add(Me.ResizePanel)
        Me.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BottomPanel.Location = New System.Drawing.Point(0, 330)
        Me.BottomPanel.Name = "BottomPanel"
        Me.BottomPanel.Size = New System.Drawing.Size(492, 40)
        Me.BottomPanel.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(202, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(250, 40)
        Me.Panel3.TabIndex = 6
        '
        'Button3
        '
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("맑은 고딕", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button3.Location = New System.Drawing.Point(109, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(135, 33)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "클립보드에서 가져오기"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("맑은 고딕", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Button2.Location = New System.Drawing.Point(3, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 33)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "클립보드로 복사"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(5, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(147, 33)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "적용하기 (Ctrl + S)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ResizePanel
        '
        Me.ResizePanel.BackColor = System.Drawing.Color.Transparent
        Me.ResizePanel.BackgroundImage = Global.ssaemtable.My.Resources.Resources.edge
        Me.ResizePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ResizePanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.ResizePanel.Location = New System.Drawing.Point(452, 0)
        Me.ResizePanel.Name = "ResizePanel"
        Me.ResizePanel.Size = New System.Drawing.Size(40, 40)
        Me.ResizePanel.TabIndex = 2
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.BT_zoom1)
        Me.TopPanel.Controls.Add(Me.ZoomFactorLabel)
        Me.TopPanel.Controls.Add(Me.BT_zoom2)
        Me.TopPanel.Controls.Add(Me.DividePanel)
        Me.TopPanel.Controls.Add(Me.DetailViewTitle)
        Me.TopPanel.Controls.Add(Me.CloseBT)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(492, 34)
        Me.TopPanel.TabIndex = 0
        '
        'BT_zoom1
        '
        Me.BT_zoom1.BackColor = System.Drawing.Color.Transparent
        Me.BT_zoom1.BackgroundImage = Global.ssaemtable.My.Resources.Resources.bt_zoom1
        Me.BT_zoom1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BT_zoom1.Dock = System.Windows.Forms.DockStyle.Right
        Me.BT_zoom1.Location = New System.Drawing.Point(318, 0)
        Me.BT_zoom1.Name = "BT_zoom1"
        Me.BT_zoom1.Size = New System.Drawing.Size(34, 34)
        Me.BT_zoom1.TabIndex = 6
        '
        'ZoomFactorLabel
        '
        Me.ZoomFactorLabel.Dock = System.Windows.Forms.DockStyle.Right
        Me.ZoomFactorLabel.Font = New System.Drawing.Font("맑은 고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ZoomFactorLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ZoomFactorLabel.Location = New System.Drawing.Point(352, 0)
        Me.ZoomFactorLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.ZoomFactorLabel.Name = "ZoomFactorLabel"
        Me.ZoomFactorLabel.Size = New System.Drawing.Size(52, 34)
        Me.ZoomFactorLabel.TabIndex = 8
        Me.ZoomFactorLabel.Text = "100%"
        Me.ZoomFactorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BT_zoom2
        '
        Me.BT_zoom2.BackColor = System.Drawing.Color.Transparent
        Me.BT_zoom2.BackgroundImage = Global.ssaemtable.My.Resources.Resources.bt_zoom2
        Me.BT_zoom2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BT_zoom2.Dock = System.Windows.Forms.DockStyle.Right
        Me.BT_zoom2.Location = New System.Drawing.Point(404, 0)
        Me.BT_zoom2.Name = "BT_zoom2"
        Me.BT_zoom2.Size = New System.Drawing.Size(34, 34)
        Me.BT_zoom2.TabIndex = 5
        '
        'DividePanel
        '
        Me.DividePanel.BackColor = System.Drawing.Color.Transparent
        Me.DividePanel.BackgroundImage = Global.ssaemtable.My.Resources.Resources.divide
        Me.DividePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DividePanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.DividePanel.Location = New System.Drawing.Point(438, 0)
        Me.DividePanel.Name = "DividePanel"
        Me.DividePanel.Size = New System.Drawing.Size(20, 34)
        Me.DividePanel.TabIndex = 7
        '
        'DetailViewTitle
        '
        Me.DetailViewTitle.AutoSize = True
        Me.DetailViewTitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.DetailViewTitle.Font = New System.Drawing.Font("맑은 고딕", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.DetailViewTitle.Location = New System.Drawing.Point(0, 0)
        Me.DetailViewTitle.Margin = New System.Windows.Forms.Padding(3)
        Me.DetailViewTitle.Name = "DetailViewTitle"
        Me.DetailViewTitle.Padding = New System.Windows.Forms.Padding(5, 5, 0, 5)
        Me.DetailViewTitle.Size = New System.Drawing.Size(109, 35)
        Me.DetailViewTitle.TabIndex = 4
        Me.DetailViewTitle.Text = "DetailView"
        Me.DetailViewTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CloseBT
        '
        Me.CloseBT.BackColor = System.Drawing.Color.Transparent
        Me.CloseBT.BackgroundImage = Global.ssaemtable.My.Resources.Resources.bt_close2
        Me.CloseBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CloseBT.Dock = System.Windows.Forms.DockStyle.Right
        Me.CloseBT.Location = New System.Drawing.Point(458, 0)
        Me.CloseBT.Name = "CloseBT"
        Me.CloseBT.Size = New System.Drawing.Size(34, 34)
        Me.CloseBT.TabIndex = 3
        '
        'DetailView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(494, 372)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(448, 166)
        Me.Name = "DetailView"
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "DetailView"
        Me.Panel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.BottomPanel.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents BottomPanel As Panel
    Friend WithEvents TopPanel As Panel
    Friend WithEvents ResizePanel As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents CloseBT As Panel
    Friend WithEvents DetailViewTitle As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SelectedFontOptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents BT_zoom1 As Panel
    Friend WithEvents BT_zoom2 As Panel
    Friend WithEvents DividePanel As Panel
    Friend WithEvents ZoomFactorLabel As Label
End Class
