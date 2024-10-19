Public Class StartForm
    ' Dim f2 As New Login()

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fel As New EmployeeLogin
        fel.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fl As New Login
        fl.Show()
        Me.Hide()

        ' f2.Label2.Text = "11111"
        'f2.Show()
        '  Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim fs As New signup
        fs.Show()
        Me.Hide()
    End Sub

    Private Sub StartForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '   f2.Owner = Me

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("登入2")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button2.BackgroundImage = My.Resources.ResourceManager.GetObject("員工切換")
        Button2.BackColor = Color.Transparent
        Button2.BackgroundImageLayout = ImageLayout.Stretch
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0

        Button3.BackgroundImage = My.Resources.ResourceManager.GetObject("註冊1")
        Button3.BackColor = Color.Transparent
        Button3.BackgroundImageLayout = ImageLayout.Stretch
        Button3.FlatStyle = FlatStyle.Flat
        Button3.FlatAppearance.BorderSize = 0
    End Sub


End Class