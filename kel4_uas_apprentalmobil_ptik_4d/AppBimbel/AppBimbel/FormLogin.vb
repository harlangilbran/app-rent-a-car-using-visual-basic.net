Imports MySql.Data.MySqlClient

Public Class FormLogin
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        Dim Email, Password As String

        Email = TextEmail.Text
        Password = TextPassword.Text
        'validasi
        If Email = "" Or Password = "" Then
            MsgBox("Email or Password wajib diisi")
        Else
            Try
                Konek()
                cmd = New MySqlCommand("SELECT email, password FROM users WHERE email ='" & Email & "' AND password ='" & Password & "' ", Koneksi)
                dr = cmd.ExecuteReader()
                dr.Read()

                If dr.HasRows Then
                    MsgBox("Login Berhasil")
                    Me.Hide()
                    Me.Invoke(New Action(Sub()
                                             Dim BukaFormUtama As New FormUtama
                                             BukaFormUtama.Show()
                                         End Sub
                                         ))
                Else
                    MsgBox("Login Gagal")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
End Class