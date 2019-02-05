<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Celldesign
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Celldesign))
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.OverwriteCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TablePanel = New System.Windows.Forms.Panel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.TableMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tm_selectedFontSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tm_clear = New System.Windows.Forms.ToolStripMenuItem()
        Me.tm_copy = New System.Windows.Forms.ToolStripMenuItem()
        Me.tm_paste = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ResetButton = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Sat_Chk = New System.Windows.Forms.CheckBox()
        Me.Fri_Chk = New System.Windows.Forms.CheckBox()
        Me.Thu_Chk = New System.Windows.Forms.CheckBox()
        Me.Wed_Chk = New System.Windows.Forms.CheckBox()
        Me.Tue_Chk = New System.Windows.Forms.CheckBox()
        Me.Mon_Chk = New System.Windows.Forms.CheckBox()
        Me.Sun_Chk = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ApplyButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ClassNum = New System.Windows.Forms.NumericUpDown()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.SidePanel = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TipText = New System.Windows.Forms.RichTextBox()
        Me.TipTitle = New System.Windows.Forms.Label()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.FormTitle = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.MainPanel.SuspendLayout()
        Me.TablePanel.SuspendLayout()
        Me.TableMenuStrip.SuspendLayout()
        CType(Me.ClassNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SidePanel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainPanel
        '
        Me.MainPanel.BackColor = System.Drawing.Color.White
        Me.MainPanel.Controls.Add(Me.OverwriteCheckBox)
        Me.MainPanel.Controls.Add(Me.Label9)
        Me.MainPanel.Controls.Add(Me.TablePanel)
        Me.MainPanel.Controls.Add(Me.Label7)
        Me.MainPanel.Controls.Add(Me.TextBox1)
        Me.MainPanel.Controls.Add(Me.Label6)
        Me.MainPanel.Controls.Add(Me.ResetButton)
        Me.MainPanel.Controls.Add(Me.Label5)
        Me.MainPanel.Controls.Add(Me.Label4)
        Me.MainPanel.Controls.Add(Me.Panel4)
        Me.MainPanel.Controls.Add(Me.Label3)
        Me.MainPanel.Controls.Add(Me.Panel3)
        Me.MainPanel.Controls.Add(Me.Sat_Chk)
        Me.MainPanel.Controls.Add(Me.Fri_Chk)
        Me.MainPanel.Controls.Add(Me.Thu_Chk)
        Me.MainPanel.Controls.Add(Me.Wed_Chk)
        Me.MainPanel.Controls.Add(Me.Tue_Chk)
        Me.MainPanel.Controls.Add(Me.Mon_Chk)
        Me.MainPanel.Controls.Add(Me.Sun_Chk)
        Me.MainPanel.Controls.Add(Me.Label2)
        Me.MainPanel.Controls.Add(Me.ApplyButton)
        Me.MainPanel.Controls.Add(Me.Label1)
        Me.MainPanel.Controls.Add(Me.ClassNum)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(1, 35)
        Me.MainPanel.Margin = New System.Windows.Forms.Padding(30)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Padding = New System.Windows.Forms.Padding(25, 20, 25, 20)
        Me.MainPanel.Size = New System.Drawing.Size(473, 372)
        Me.MainPanel.TabIndex = 5
        '
        'OverwriteCheckBox
        '
        Me.OverwriteCheckBox.AutoSize = True
        Me.OverwriteCheckBox.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.OverwriteCheckBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OverwriteCheckBox.Location = New System.Drawing.Point(305, 308)
        Me.OverwriteCheckBox.Name = "OverwriteCheckBox"
        Me.OverwriteCheckBox.Size = New System.Drawing.Size(142, 19)
        Me.OverwriteCheckBox.TabIndex = 45
        Me.OverwriteCheckBox.Text = "현재 표에다 덮어쓰기"
        Me.OverwriteCheckBox.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("맑은 고딕", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(31, 330)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(412, 24)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "※ 너무 많은 표를 만들거나 내용을 삽입할 시 프로그램이 느려질 수 있습니다!"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TablePanel
        '
        Me.TablePanel.BackColor = System.Drawing.Color.DimGray
        Me.TablePanel.Controls.Add(Me.RichTextBox1)
        Me.TablePanel.Location = New System.Drawing.Point(32, 216)
        Me.TablePanel.Name = "TablePanel"
        Me.TablePanel.Padding = New System.Windows.Forms.Padding(1)
        Me.TablePanel.Size = New System.Drawing.Size(225, 111)
        Me.TablePanel.TabIndex = 41
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.ContextMenuStrip = Me.TableMenuStrip
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(1, 1)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(223, 109)
        Me.RichTextBox1.TabIndex = 36
        Me.RichTextBox1.Text = ""
        '
        'TableMenuStrip
        '
        Me.TableMenuStrip.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TableMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tm_selectedFontSet, Me.ToolStripSeparator2, Me.tm_clear, Me.tm_copy, Me.tm_paste})
        Me.TableMenuStrip.Name = "ContextMenuStrip1"
        Me.TableMenuStrip.ShowImageMargin = False
        Me.TableMenuStrip.Size = New System.Drawing.Size(168, 114)
        '
        'tm_selectedFontSet
        '
        Me.tm_selectedFontSet.Name = "tm_selectedFontSet"
        Me.tm_selectedFontSet.Size = New System.Drawing.Size(167, 26)
        Me.tm_selectedFontSet.Text = "글꼴, 색상 설정"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(164, 6)
        '
        'tm_clear
        '
        Me.tm_clear.Name = "tm_clear"
        Me.tm_clear.Size = New System.Drawing.Size(167, 26)
        Me.tm_clear.Text = "비우기"
        '
        'tm_copy
        '
        Me.tm_copy.Name = "tm_copy"
        Me.tm_copy.Size = New System.Drawing.Size(167, 26)
        Me.tm_copy.Text = "복사"
        '
        'tm_paste
        '
        Me.tm_paste.Name = "tm_paste"
        Me.tm_paste.Size = New System.Drawing.Size(167, 26)
        Me.tm_paste.Text = "붙여넣기"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("맑은 고딕", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Gray
        Me.Label7.Location = New System.Drawing.Point(290, 187)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(153, 13)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "예 : OOO 선생님, OO 일정표"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(88, 159)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(355, 25)
        Me.TextBox1.TabIndex = 39
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(28, 162)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 17)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "표 제목"
        '
        'ResetButton
        '
        Me.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ResetButton.Location = New System.Drawing.Point(263, 216)
        Me.ResetButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(64, 85)
        Me.ResetButton.TabIndex = 37
        Me.ResetButton.Text = "초기화"
        Me.ResetButton.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("맑은 고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(28, 196)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(210, 17)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "표 기본 내용 (서식 붙여넣기 가능)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("맑은 고딕", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.Location = New System.Drawing.Point(28, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 20)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "서식 설정"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.DimGray
        Me.Panel4.Location = New System.Drawing.Point(32, 143)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(8)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(411, 1)
        Me.Panel4.TabIndex = 33
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("맑은 고딕", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(28, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 20)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "일정 설정"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.DimGray
        Me.Panel3.Location = New System.Drawing.Point(32, 40)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(8)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(411, 1)
        Me.Panel3.TabIndex = 31
        '
        'Sat_Chk
        '
        Me.Sat_Chk.AutoSize = True
        Me.Sat_Chk.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Sat_Chk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Sat_Chk.Location = New System.Drawing.Point(218, 81)
        Me.Sat_Chk.Name = "Sat_Chk"
        Me.Sat_Chk.Size = New System.Drawing.Size(45, 25)
        Me.Sat_Chk.TabIndex = 30
        Me.Sat_Chk.Text = "토"
        Me.Sat_Chk.UseVisualStyleBackColor = True
        '
        'Fri_Chk
        '
        Me.Fri_Chk.AutoSize = True
        Me.Fri_Chk.Checked = True
        Me.Fri_Chk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Fri_Chk.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Fri_Chk.Location = New System.Drawing.Point(167, 81)
        Me.Fri_Chk.Name = "Fri_Chk"
        Me.Fri_Chk.Size = New System.Drawing.Size(45, 25)
        Me.Fri_Chk.TabIndex = 29
        Me.Fri_Chk.Text = "금"
        Me.Fri_Chk.UseVisualStyleBackColor = True
        '
        'Thu_Chk
        '
        Me.Thu_Chk.AutoSize = True
        Me.Thu_Chk.Checked = True
        Me.Thu_Chk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Thu_Chk.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Thu_Chk.Location = New System.Drawing.Point(116, 81)
        Me.Thu_Chk.Name = "Thu_Chk"
        Me.Thu_Chk.Size = New System.Drawing.Size(45, 25)
        Me.Thu_Chk.TabIndex = 28
        Me.Thu_Chk.Text = "목"
        Me.Thu_Chk.UseVisualStyleBackColor = True
        '
        'Wed_Chk
        '
        Me.Wed_Chk.AutoSize = True
        Me.Wed_Chk.Checked = True
        Me.Wed_Chk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Wed_Chk.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Wed_Chk.Location = New System.Drawing.Point(218, 50)
        Me.Wed_Chk.Name = "Wed_Chk"
        Me.Wed_Chk.Size = New System.Drawing.Size(45, 25)
        Me.Wed_Chk.TabIndex = 27
        Me.Wed_Chk.Text = "수"
        Me.Wed_Chk.UseVisualStyleBackColor = True
        '
        'Tue_Chk
        '
        Me.Tue_Chk.AutoSize = True
        Me.Tue_Chk.Checked = True
        Me.Tue_Chk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Tue_Chk.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Tue_Chk.Location = New System.Drawing.Point(167, 50)
        Me.Tue_Chk.Name = "Tue_Chk"
        Me.Tue_Chk.Size = New System.Drawing.Size(45, 25)
        Me.Tue_Chk.TabIndex = 26
        Me.Tue_Chk.Text = "화"
        Me.Tue_Chk.UseVisualStyleBackColor = True
        '
        'Mon_Chk
        '
        Me.Mon_Chk.AutoSize = True
        Me.Mon_Chk.Checked = True
        Me.Mon_Chk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Mon_Chk.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Mon_Chk.Location = New System.Drawing.Point(116, 50)
        Me.Mon_Chk.Name = "Mon_Chk"
        Me.Mon_Chk.Size = New System.Drawing.Size(45, 25)
        Me.Mon_Chk.TabIndex = 25
        Me.Mon_Chk.Text = "월"
        Me.Mon_Chk.UseVisualStyleBackColor = True
        '
        'Sun_Chk
        '
        Me.Sun_Chk.AutoSize = True
        Me.Sun_Chk.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Sun_Chk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Sun_Chk.Location = New System.Drawing.Point(269, 81)
        Me.Sun_Chk.Name = "Sun_Chk"
        Me.Sun_Chk.Size = New System.Drawing.Size(45, 25)
        Me.Sun_Chk.TabIndex = 24
        Me.Sun_Chk.Text = "일"
        Me.Sun_Chk.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("맑은 고딕", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(306, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 20)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "교시 (행)"
        '
        'ApplyButton
        '
        Me.ApplyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ApplyButton.Location = New System.Drawing.Point(333, 216)
        Me.ApplyButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ApplyButton.Name = "ApplyButton"
        Me.ApplyButton.Size = New System.Drawing.Size(110, 85)
        Me.ApplyButton.TabIndex = 20
        Me.ApplyButton.Text = "적용"
        Me.ApplyButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("맑은 고딕", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 20)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "요일 (열)"
        '
        'ClassNum
        '
        Me.ClassNum.Font = New System.Drawing.Font("맑은 고딕", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ClassNum.Location = New System.Drawing.Point(381, 50)
        Me.ClassNum.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ClassNum.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.ClassNum.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.ClassNum.Name = "ClassNum"
        Me.ClassNum.Size = New System.Drawing.Size(62, 50)
        Me.ClassNum.TabIndex = 21
        Me.ClassNum.Value = New Decimal(New Integer() {7, 0, 0, 0})
        '
        'FontDialog1
        '
        '
        'SidePanel
        '
        Me.SidePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.SidePanel.Controls.Add(Me.PictureBox1)
        Me.SidePanel.Controls.Add(Me.TipText)
        Me.SidePanel.Controls.Add(Me.TipTitle)
        Me.SidePanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.SidePanel.Location = New System.Drawing.Point(474, 35)
        Me.SidePanel.Name = "SidePanel"
        Me.SidePanel.Padding = New System.Windows.Forms.Padding(15)
        Me.SidePanel.Size = New System.Drawing.Size(148, 372)
        Me.SidePanel.TabIndex = 46
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ssaemtable.My.Resources.Resources.icon
        Me.PictureBox1.Location = New System.Drawing.Point(83, 304)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'TipText
        '
        Me.TipText.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TipText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TipText.Dock = System.Windows.Forms.DockStyle.Top
        Me.TipText.ForeColor = System.Drawing.Color.White
        Me.TipText.Location = New System.Drawing.Point(15, 41)
        Me.TipText.Name = "TipText"
        Me.TipText.ReadOnly = True
        Me.TipText.Size = New System.Drawing.Size(118, 257)
        Me.TipText.TabIndex = 4
        Me.TipText.Text = "항목에 커서를 올려 설명을 보실 수 있습니다."
        '
        'TipTitle
        '
        Me.TipTitle.BackColor = System.Drawing.Color.Transparent
        Me.TipTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.TipTitle.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TipTitle.ForeColor = System.Drawing.Color.Silver
        Me.TipTitle.Location = New System.Drawing.Point(15, 15)
        Me.TipTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.TipTitle.Name = "TipTitle"
        Me.TipTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TipTitle.Size = New System.Drawing.Size(118, 26)
        Me.TipTitle.TabIndex = 6
        Me.TipTitle.Text = "새로 만들기"
        Me.TipTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.TopPanel.Controls.Add(Me.FormTitle)
        Me.TopPanel.Controls.Add(Me.Panel2)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(1, 1)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(621, 34)
        Me.TopPanel.TabIndex = 47
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
        Me.FormTitle.Size = New System.Drawing.Size(101, 31)
        Me.FormTitle.TabIndex = 6
        Me.FormTitle.Text = "새로 만들기"
        Me.FormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.ssaemtable.My.Resources.Resources.bt_close2
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(587, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(34, 34)
        Me.Panel2.TabIndex = 3
        '
        'Celldesign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(623, 408)
        Me.Controls.Add(Me.MainPanel)
        Me.Controls.Add(Me.SidePanel)
        Me.Controls.Add(Me.TopPanel)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Celldesign"
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "새로 만들기"
        Me.MainPanel.ResumeLayout(False)
        Me.MainPanel.PerformLayout()
        Me.TablePanel.ResumeLayout(False)
        Me.TableMenuStrip.ResumeLayout(False)
        CType(Me.ClassNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SidePanel.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainPanel As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ResetButton As Button
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Sat_Chk As CheckBox
    Friend WithEvents Fri_Chk As CheckBox
    Friend WithEvents Thu_Chk As CheckBox
    Friend WithEvents Wed_Chk As CheckBox
    Friend WithEvents Tue_Chk As CheckBox
    Friend WithEvents Mon_Chk As CheckBox
    Friend WithEvents Sun_Chk As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ApplyButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ClassNum As NumericUpDown
    Friend WithEvents TablePanel As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents TableMenuStrip As ContextMenuStrip
    Friend WithEvents tm_selectedFontSet As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tm_clear As ToolStripMenuItem
    Friend WithEvents tm_copy As ToolStripMenuItem
    Friend WithEvents tm_paste As ToolStripMenuItem
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents OverwriteCheckBox As CheckBox
    Friend WithEvents SidePanel As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TipText As RichTextBox
    Friend WithEvents TopPanel As Panel
    Friend WithEvents FormTitle As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TipTitle As Label
End Class
