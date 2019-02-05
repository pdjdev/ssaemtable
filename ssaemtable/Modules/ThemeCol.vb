Public Module ThemeCol

    Function main() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.FromArgb(250, 250, 250)
            Case "black"
                Return Color.FromArgb(40, 40, 40)
        End Select
    End Function

    Function littledark() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.FromArgb(240, 240, 240)
            Case "black"
                Return Color.FromArgb(50, 50, 50)
        End Select
    End Function

    Function dark() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.DimGray
            Case "black"
                Return Color.FromArgb(100, 100, 100)
        End Select
    End Function

    '테두리
    Function edge() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.DarkGray
            Case "black"
                Return Color.FromArgb(10, 90, 153)
        End Select
    End Function

    '배경 악센트
    Function accent_bg() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.FromArgb(228, 239, 251)
            Case "black"
                Return Color.FromArgb(20, 51, 77)
        End Select
    End Function

    Function accent_foretext() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.DodgerBlue
            Case "black"
                Return Color.DodgerBlue
        End Select
    End Function

    Function cell() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.White
            Case "black"
                Return Color.FromArgb(33, 33, 33)
        End Select
    End Function

    Function cell_locked() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.FromArgb(250, 250, 250)
            Case "black"
                Return Color.FromArgb(40, 40, 40)
        End Select
    End Function

    Function foretext() As Color

        Select Case My.Settings.theme
            Case "white"
                Return Color.Black
            Case "black"
                Return Color.White
        End Select
    End Function

    '토(0) 일(1)
    Function weekend() As Color()

        Select Case My.Settings.theme
            Case "white"
                Return New Color() {Color.DarkBlue, Color.DarkRed}
            Case "black"
                Return New Color() {Color.DodgerBlue, Color.Red}
        End Select
    End Function


End Module
