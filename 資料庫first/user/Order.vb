Imports System.Data.OleDb
Imports System.Reflection.Emit
Imports System.Windows
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar
Imports FxResources.System

Public Class Order
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Private bitmap As Bitmap
    Dim fo2 As New Order_2
    Dim intValue As Integer
    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function



    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' RoomChange.roomnew.Owner = Me
        Dim rand As Integer
        rand = GetRandom(1, 1000)

        Label8.Text = Label1.Text

        fo2.Owner = Me

        If Label4.Text = "目前沒有符合條件的房間" Then 'filter有符合，編號會蓋過label4
            Label3.Visible = False
            Label4.Visible = False
            Label13.Visible = True

        End If
        If Label6.Text = "目前沒有符合條件的房間" Then
            Label5.Visible = False
            Label6.Visible = False
            Label12.Visible = True

        End If

        ' conn.Open()
        'cmd = conn.CreateCommand()
        'cmd.CommandType = CommandType.Text

        'cmd.CommandText = "INSERT INTO 訂單 (訂單編號,供餐資訊, 總金額, 品名, 價錢,日期) " &
        '         "SELECT '" & rand & "', '" & note.Text & "', '" & tablenum.Text & "', '" & meal.Text & "', 餐點.金額, ? " &
        '        "FROM 餐點 WHERE 餐點.品名 = '" & meal.Text & "'"
        'cmd.Parameters.AddWithValue("@date", DateTimePicker1.Value.ToString("yyyy-MM-dd")) '& DateTimePicker1.Value.ToString("yyyy-MM-dd") & "'"
        'cmd.ExecuteNonQuery()

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("下一步")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button8.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button8.BackColor = Color.Transparent
        Button8.BackgroundImageLayout = ImageLayout.Stretch
        Button8.FlatStyle = FlatStyle.Flat
        Button8.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub roomtype1_Click(sender As Object, e As EventArgs) Handles roomtype1.Click
        Try
            If roomtype1.Text = "單人房" Then
                Dim fr1 As New RoomInfo1
                fr1.Label2.Text = Label1.Text
                fr1.Show()

            ElseIf roomtype1.Text = "雙人房" Then
                Dim fr2 As New roominfo2
                fr2.Label2.Text = Label1.Text
                fr2.Show()
                '  Me.Hide()
            ElseIf roomtype1.Text = "四人房" Then
                Dim fr4 As New roominfo4
                fr4.Label2.Text = Label1.Text
                fr4.Show()
                ' Me.Hide()
            ElseIf roomtype1.Text = "總統套房" Then
                Dim fr6 As New roominfo6666
                fr6.Label2.Text = Label1.Text
                fr6.Show()
                ' Me.Hide()
            End If
        Catch ex As Exception

        End Try
    End Sub




    Private Sub Button7_Click(sender As Object, e As EventArgs)
        Dim f8 As New RoomInfo1
        f8.Show()
        Me.Hide()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim fff As New FilterForm
        fff.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Integer.TryParse(Label9.Text, intValue)

            If RadioButton1.Checked = True And Not String.IsNullOrWhiteSpace(roomtype1.Text) Then
                Label8.Text = Label1.Text
            ElseIf RadioButton2.Checked = True And Not String.IsNullOrWhiteSpace(roomtype2.Text) Then
                Label8.Text = Label4.Text
            ElseIf RadioButton3.Checked = True And Not String.IsNullOrWhiteSpace(roomtype3.Text) Then
                Label8.Text = Label6.Text
            Else
                MessageBox.Show("請選擇有資料的")
                Exit Sub
            End If

            fo2.Label9.Text = Label8.Text
            fo2.day = intValue
            fo2.loginid.Text = loginid.Text
            fo2.startdate = startdate.Text
            fo2.enddate = enddate.Text

            If RadioButton1.Checked = False And RadioButton2.Checked = False And RadioButton3.Checked = False Then
                MessageBox.Show("請選擇房間")
                Exit Sub
            End If
            fo2.Show()
            Me.Hide()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub roomtype2_Click(sender As Object, e As EventArgs) Handles roomtype2.Click
        Try
            If roomtype2.Text = "單人房" Then '傳到房間資訊判斷抓哪個的簡介
                Dim fr1 As New RoomInfo1
                fr1.Label2.Text = Label4.Text
                fr1.Show()

            ElseIf roomtype2.Text = "雙人房" Then
                Dim fr2 As New roominfo2
                fr2.Label2.Text = Label4.Text
                fr2.Show()
                '  Me.Hide()
            ElseIf roomtype2.Text = "四人房" Then
                Dim fr4 As New roominfo4
                fr4.Label2.Text = Label4.Text
                fr4.Show()
                ' Me.Hide()
            ElseIf roomtype2.Text = "總統套房" Then
                Dim fr6 As New roominfo6666
                fr6.Label2.Text = Label4.Text
                fr6.Show()
                ' Me.Hide()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub roomtype3_Click(sender As Object, e As EventArgs) Handles roomtype3.Click
        Try
            If roomtype3.Text = "單人房" Then '傳到房間資訊判斷抓哪個的簡介
                Dim fr1 As New RoomInfo1
                fr1.Label2.Text = Label6.Text
                fr1.Show()

            ElseIf roomtype3.Text = "雙人房" Then
                Dim fr2 As New roominfo2
                fr2.Label2.Text = Label6.Text
                fr2.Show()
                '  Me.Hide()
            ElseIf roomtype3.Text = "四人房" Then
                Dim fr4 As New roominfo4
                fr4.Label2.Text = Label6.Text
                fr4.Show()
                ' Me.Hide()
            ElseIf roomtype3.Text = "總統套房" Then
                Dim fr6 As New roominfo6666
                fr6.Label2.Text = Label6.Text
                fr6.Show()
                ' Me.Hide()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class