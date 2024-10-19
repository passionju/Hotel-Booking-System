Imports System.Data.OleDb
Imports System.Runtime.InteropServices.ComTypes
Imports System.Security.Policy
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class FilterForm
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Dim dr As OleDbDataReader
    Dim f2 As New Order()


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim startDate As DateTime = DateTimePicker1.Value
        Dim endDate As DateTime = DateTimePicker2.Value
        Dim days As Integer = CInt((endDate - startDate).TotalDays)

        f2.Label9.Text = days
        f2.loginid.Text = loginid.Text
        f2.startdate.Text = startDate.ToString("yyyy-MM-dd")
        f2.enddate.Text = endDate.ToString("yyyy-MM-dd")


        Try
            conn.Open()

            ' cmd = conn.CreateCommand()
            'cmd.CommandType = CommandType.Text
            'cmd.CommandText = "SELECT 房間編號 FROM 房間 WHERE 房間編號 NOT IN(SELECT 房間編號 FROM 訂單 WHERE 入住日期 between'" +
            ' DateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "')"
            Dim mycommand As New OleDbCommand("SELECT * FROM 房間 WHERE (房間編號 NOT IN(SELECT 房間編號 FROM 訂單 WHERE 
(入住日期 <='" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AND 退房日期>='" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "') or(入住日期 <='" +'偏左
        DateTimePicker1.Value.ToString("yyyy-MM-dd") + "'AND 退房日期>= '" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "')))  
        and 房型='" + ComboBox1.Text + "'and (價格 in (select 價格 from 房間 where 價格 between'" + ComboBox3.Text + "'and'" + Label5.Text + "'))", conn)
            dr = mycommand.ExecuteReader()
            If Not dr.HasRows Then
                MessageBox.Show("目前選擇的選項尚無空房", "搜尋錯誤", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            While dr.Read()

                If String.IsNullOrWhiteSpace(f2.roomtype1.Text) Then
                    f2.roomtype1.Text = dr.GetString(1)
                    f2.Label1.Text = dr.GetString(0)

                    If dr.GetString(1) = "單人房" Then
                        f2.PictureBox1.Image = My.Resources.ResourceManager.GetObject("單人房")
                    ElseIf dr.GetString(1) = "雙人房" Then
                        f2.PictureBox1.Image = My.Resources.ResourceManager.GetObject("雙人房")
                    ElseIf dr.GetString(1) = "四人房" Then
                        f2.PictureBox1.Image = My.Resources.ResourceManager.GetObject("四人房")
                    ElseIf dr.GetString(1) = "總統套房" Then
                        f2.PictureBox1.Image = My.Resources.ResourceManager.GetObject("總統套房")
                    End If
                    'f2.Label7.Text = "11111111111111"

                    If String.IsNullOrWhiteSpace(f2.roomtype2.Text) Then
                        If dr.Read() Then
                            f2.roomtype2.Text = dr.GetString(1)
                            f2.Label4.Text = dr.GetString(0)
                            ' f2.Label8.Text = "11111111111111"

                            If dr.GetString(1) = "單人房" Then
                                f2.PictureBox2.Image = My.Resources.ResourceManager.GetObject("單人房")
                            ElseIf dr.GetString(1) = "雙人房" Then
                                f2.PictureBox2.Image = My.Resources.ResourceManager.GetObject("雙人房")
                            ElseIf dr.GetString(1) = "四人房" Then
                                f2.PictureBox2.Image = My.Resources.ResourceManager.GetObject("四人房")
                            ElseIf dr.GetString(1) = "總統套房" Then
                                f2.PictureBox2.Image = My.Resources.ResourceManager.GetObject("總統套房")
                            End If
                        End If
                    End If
                    If String.IsNullOrWhiteSpace(f2.roomtype3.Text) Then
                        If dr.Read() Then
                            f2.roomtype3.Text = dr.GetString(1)
                            f2.Label6.Text = dr.GetString(0)

                            If dr.GetString(1) = "單人房" Then
                                f2.PictureBox3.Image = My.Resources.ResourceManager.GetObject("單人房")
                            ElseIf dr.GetString(1) = "雙人房" Then
                                f2.PictureBox3.Image = My.Resources.ResourceManager.GetObject("雙人房")
                            ElseIf dr.GetString(1) = "四人房" Then
                                f2.PictureBox3.Image = My.Resources.ResourceManager.GetObject("四人房")
                            ElseIf dr.GetString(1) = "總統套房" Then
                                f2.PictureBox3.Image = My.Resources.ResourceManager.GetObject("總統套房")
                            End If
                        End If
                    End If
                End If
                f2.Show()

                Me.Hide()
                conn.Close()
            End While


            ' Dim fodr As New Order
            ' fodr.Show()
            ' Me.Hide()
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FilterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
        DateTimePicker1.MinDate = DateTime.Today
        DateTimePicker2.MinDate = DateTime.Today

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("搜尋")
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

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'Dim fst As New UserFunction
        UserFunction.Show()
        Me.Hide()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Dim query2 As String = "SELECT 房間編號 FROM 房間 WHERE 房間編號 NOT IN(SELECT 房間編號 FROM 訂單 WHERE 入住日期 between'" +
        DateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "')"
            Using connection As New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb")
                Using command As New OleDbCommand(query2, connection)
                    connection.Open()

                    Dim reader As OleDbDataReader = command.ExecuteReader()

                    While reader.Read()
                        Dim roomID As String = reader("房間編號").ToString()
                        ' 在此處處理每個符合條件的房間編號
                    End While

                    reader.Close()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "1000" Then
            Label5.Text = "3000"
        ElseIf ComboBox3.Text = "3000" Then
            Label5.Text = "5000"
        ElseIf ComboBox3.Text = "5000" Then
            Label5.Text = "10000"
        ElseIf ComboBox3.Text = "10000" Then
            Label5.Text = "50000"
        End If
    End Sub


    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Dim startDate As DateTime = DateTimePicker1.Value
        Dim endDate As DateTime = DateTimePicker2.Value
        Dim days As Integer = CInt((endDate - startDate).TotalDays)

        If days < 0 Then
            DateTimePicker2.Value = DateTimePicker1.Value

        End If

    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        Dim startDate As DateTime = DateTimePicker1.Value
        Dim endDate As DateTime = DateTimePicker2.Value
        Dim days As Integer = CInt((endDate - startDate).TotalDays)

        If days < 0 Then
            DateTimePicker2.Value = DateTimePicker1.Value

        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class