Imports System.Data.OleDb
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar
Imports FxResources.System

Public Class signup
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Private bitmap As Bitmap
    Public Shared ID As String
    Public Shared email As String
    Dim sex As String
    Private Sub signup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("確定")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button4.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button4.BackColor = Color.Transparent
        Button4.BackgroundImageLayout = ImageLayout.Stretch
        Button4.FlatStyle = FlatStyle.Flat
        Button4.FlatAppearance.BorderSize = 0
    End Sub
    Private Function IsInputValidPSWD(inputText As String) As Boolean
        Dim pattern1 As String = "^[0-9]{6}$"
        Dim regex1 As New System.Text.RegularExpressions.Regex(pattern1)
        Return regex1.IsMatch(inputText)
    End Function

    Private Function IsInputValidEmail(inputText As String) As Boolean
        Dim pattern2 As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Dim regex2 As New System.Text.RegularExpressions.Regex(pattern2)
        Return regex2.IsMatch(inputText)
    End Function
    Private Function IsInputValidPhone(inputText As String) As Boolean
        Dim pattern3 As String = "^[0-9]{10}$"
        Dim regex3 As New System.Text.RegularExpressions.Regex(pattern3)
        Return regex3.IsMatch(inputText)
    End Function
    Private Function IsInputValidName(inputText As String) As Boolean
        Dim pattern4 As String = "^[\u4E00-\u9FFF]+$"
        Dim regex4 As New System.Text.RegularExpressions.Regex(pattern4)
        Return regex4.IsMatch(inputText)
    End Function
    Private Function IsInputValidID(inputText As String) As Boolean
        Dim pattern2 As String = "^[A-Z]+[0-9]{9}$"
        Dim regex2 As New System.Text.RegularExpressions.Regex(pattern2)
        Return regex2.IsMatch(inputText)
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim userInputName As String = TextBox1.Text
        Dim a As Integer = 0
        Try
            If RadioButton1.Checked Then
                sex = RadioButton1.Text
            ElseIf RadioButton2.Checked Then
                sex = RadioButton2.Text
            ElseIf RadioButton3.Checked Then
                sex = RadioButton3.Text
            End If
            '------------------------------------------------------
            If IsInputValidName(userInputName) Then

            Else
                MsgBox("請輸入中文姓名")
                a += 1
                Exit Sub
            End If

            Dim userInputEmail As String = TextBox3.Text
            'Dim b As Integer = 0
            If IsInputValidEmail(userInputEmail) Then

            Else
                MsgBox("電子信箱格式不正確")
                a += 1
                Exit Sub
            End If

            Dim userInputPhone As String = TextBox4.Text
            'Dim c As Integer = 0
            If IsInputValidPhone(userInputPhone) Then
                'c = 1
            Else
                MsgBox("電話格式不正確")
                a += 1
                Exit Sub
            End If

            'Dim userInputPSWD As String = TextBox5.Text
            'Dim d As Integer = 0
            'If IsInputValidPSWD(userInputPSWD) Then
            '    d = 1

            'Else
            '    MsgBox("密碼格式不正確")

            'End If

            Dim userInputID As String = TextBox2.Text

            If IsInputValidID(userInputID) Then

            Else
                MsgBox("身分證字號格式不正確")
                a += 1
                Exit Sub
            End If

            If RadioButton1.Checked = False And RadioButton2.Checked = False And RadioButton3.Checked = False Then
                MessageBox.Show("資料請填寫完全")
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then '姓名
                MessageBox.Show("資料請填寫完全")
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(TextBox3.Text) Then '信箱
                MessageBox.Show("資料請填寫完全")
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(TextBox2.Text) Then 'id
                MessageBox.Show("資料請填寫完全")
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(TextBox4.Text) Then '電話
                MessageBox.Show("資料請填寫完全")
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(TextBox5.Text) Then '密碼
                MessageBox.Show("資料請填寫完全")
                Exit Sub
            End If
            '---------------------------------------------------
            If a = 0 Then
                conn.Open()
                cmd = conn.CreateCommand()
                cmd.CommandType = CommandType.Text
                Try
                    cmd.CommandText = "insert into 顧客(身分證字號,姓名,電話,電子信箱,性別,密碼)values('" + TextBox2.Text +
                "', '" + TextBox1.Text + "','" + TextBox4.Text + "','" + TextBox3.Text + "','" + sex + "','" + TextBox5.Text + "')"
                    cmd.ExecuteNonQuery()

                    conn.Close()
                    MsgBox("註冊成功")
                    StartForm.Show()
                    Me.Hide()
                Catch ex As Exception
                    MsgBox("該顧客已存在")
                    conn.Close()
                End Try

            Else
                MsgBox("註冊失敗")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim fst As New StartForm
        fst.Show()
        Me.Hide()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class