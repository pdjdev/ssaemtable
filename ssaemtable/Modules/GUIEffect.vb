﻿Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Module GUIEffect

#Region "Aero 그림자 효과 (Vista이상)"
    '윈도우 Vista 이상에서만 작동 가능
    '코드 참조: https://stackoverflow.com/questions/20322637
    '코드 참조2: https://gist.github.com/bae-unidev/94c907b7a5acd98f2c44

    Structure MARGINS
        Public Left, Right, Top, Bottom As Integer
    End Structure

    <DllImport("dwmapi.dll", PreserveSig:=True)>
    Private Function DwmSetWindowAttribute(hwnd As IntPtr, attr As Integer, ByRef attrValue As Integer, attrSize As Integer) As Integer
    End Function

    <DllImport("dwmapi.dll")>
    Private Function DwmExtendFrameIntoClientArea(hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
    End Function

    Public Function CreateDropShadow(form As Form) As Boolean
        If My.Settings.aeroeffect Then
            Try
                Dim val As Integer = 2
                Dim ret1 As Integer = DwmSetWindowAttribute(form.Handle, 2, val, 4)

                If ret1 = 0 Then
                    Dim m As MARGINS = New MARGINS() With {
                            .Left = 1,
                            .Right = 1,
                            .Top = 1,
                            .Bottom = 1
                        }

                    Dim ret2 As Integer = DwmExtendFrameIntoClientArea(form.Handle, m)
                    Return ret2 = 0
                Else
                    Return False
                End If
            Catch ex As Exception
                ' Probably dwmapi.dll not found (incompatible OS)
                Return False
            End Try
        Else
            Return False
        End If
    End Function

#End Region



#Region "창붙기 효과"
    Private Const mSnapOffset As Integer = 35
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

    Public Sub SnapToDesktopBorder(ByVal clientForm As Form, ByVal LParam As IntPtr, ByVal widthAdjustment As Integer)

        If clientForm Is Nothing Then
            ' Satisfies rule: Validate parameters
            Throw New ArgumentNullException("clientForm")
        End If

        ' Snap client to the top, left, bottom or right desktop border
        ' as the form is moved near that border.

        Try
            ' Marshal the LPARAM value which is a WINDOWPOS struct
            Dim NewPosition As New WINDOWPOS
            NewPosition = CType(Runtime.InteropServices.Marshal.PtrToStructure(
                LParam, GetType(WINDOWPOS)), WINDOWPOS)

            If NewPosition.y = 0 OrElse NewPosition.x = 0 Then
                Return ' Nothing to do!
            End If

            ' Adjust the client size for borders and caption bar
            Dim ClientRect As Rectangle =
                clientForm.RectangleToScreen(clientForm.ClientRectangle)
            ClientRect.Width +=
                SystemInformation.FrameBorderSize.Width - widthAdjustment - dpicalc(clientForm, 8)
            ClientRect.Height += (SystemInformation.FrameBorderSize.Height +
                                  SystemInformation.CaptionHeight - dpicalc(clientForm, 31))
            ' 임의로 포인트 변경

            ' Now get the screen working area (without taskbar)
            Dim WorkingRect As Rectangle =
                Screen.GetWorkingArea(clientForm.ClientRectangle)

            ' Left border
            If NewPosition.x >= WorkingRect.X - mSnapOffset AndAlso
                NewPosition.x <= WorkingRect.X + mSnapOffset Then
                NewPosition.x = WorkingRect.X
            End If

            ' Get screen bounds and taskbar height
            ' (when taskbar is horizontal)
            Dim ScreenRect As Rectangle =
                Screen.GetBounds(Screen.PrimaryScreen.Bounds)
            Dim TaskbarHeight As Integer =
                ScreenRect.Height - WorkingRect.Height

            ' Top border (check if taskbar is on top
            ' or bottom via WorkingRect.Y)
            If NewPosition.y >= -mSnapOffset AndAlso
                 (WorkingRect.Y > 0 AndAlso NewPosition.y <=
                 (TaskbarHeight + mSnapOffset)) OrElse
                 (WorkingRect.Y <= 0 AndAlso NewPosition.y <=
                 (mSnapOffset)) Then
                If TaskbarHeight > 0 Then
                    NewPosition.y = WorkingRect.Y ' Horizontal Taskbar
                Else
                    NewPosition.y = 0 ' Vertical Taskbar
                End If
            End If

            ' Right border
            If NewPosition.x + ClientRect.Width <=
                 WorkingRect.Right + mSnapOffset AndAlso
                 NewPosition.x + ClientRect.Width >=
                 WorkingRect.Right - mSnapOffset Then
                NewPosition.x = WorkingRect.Right - (ClientRect.Width +
                                SystemInformation.FrameBorderSize.Width)
            End If

            ' Bottom border
            If NewPosition.y + ClientRect.Height <=
                   WorkingRect.Bottom + mSnapOffset AndAlso
                   NewPosition.y + ClientRect.Height >=
                   WorkingRect.Bottom - mSnapOffset Then
                NewPosition.y = WorkingRect.Bottom - (ClientRect.Height +
                                SystemInformation.FrameBorderSize.Height)
            End If

            ' Marshal it back
            Runtime.InteropServices.Marshal.StructureToPtr(NewPosition,
                                                           LParam, True)
        Catch ex As ArgumentException
        End Try
    End Sub

#End Region

#Region "DPI 계산 (고해상도 디스플레이용)"
    Public Function dpicalc(targetform As Form, size As Integer)
        Dim g As Graphics = targetform.CreateGraphics
        Dim dpi = g.DpiX.ToString()

        Dim current = dpi / 96
        Dim expand = current * size

        Return expand
    End Function
#End Region

#Region "창 페이드 인/아웃 효과"
    Public Sub FadeIn(Form As Form, goalopacity As Double)
        Form.Opacity = 0

        If My.Settings.fadeeffect Then
            Do While Form.Opacity < goalopacity - 0.1
                Form.Opacity += 0.1
                Threading.Thread.Sleep(10)
            Loop
        End If

        Form.Opacity = goalopacity
    End Sub

    Public Sub FadeOut(Form As Form)

        If My.Settings.fadeeffect Then
            Do While Not Form.Opacity = 0
                Form.Opacity = Form.Opacity - 0.1
                Threading.Thread.Sleep(10)
            Loop
        End If

    End Sub
#End Region

End Module
