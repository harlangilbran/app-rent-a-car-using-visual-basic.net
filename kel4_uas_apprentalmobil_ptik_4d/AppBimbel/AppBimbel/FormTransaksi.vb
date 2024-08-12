Imports MySql.Data.MySqlClient
Public Class FormTransaksi
    'Fungsi Untuk tampil data
    Sub TampilData()
        Konek()

        da = New MySqlDataAdapter("SELECT * FROM transaksi", Koneksi)
        dt = New DataTable
        da.Fill(dt)
        DGV.Rows.Clear()
        'Looping 
        For i = 0 To dt.Rows.Count - 1
            DGV.Rows.Add(dt.Rows(i).Item(1))
            DGV.Rows(i).Cells(1).Value = dt.Rows(i).Item(2)
            DGV.Rows(i).Cells(2).Value = dt.Rows(i).Item(3)
            DGV.Rows(i).Cells(3).Value = dt.Rows(i).Item(4)
            DGV.Rows(i).Cells(4).Value = dt.Rows(i).Item(5)
        Next
    End Sub
    'Cari data
    Sub CariData()
        Konek()

        da = New MySqlDataAdapter("SELECT * FROM transaksi WHERE nama like '%" & TextCari.Text & "%' or alamat like '%" & TextCari.Text & "%' ", Koneksi)
        dt = New DataTable
        da.Fill(dt)
        DGV.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            DGV.Rows.Add(dt.Rows(i).Item(1))
            DGV.Rows(i).Cells(1).Value = dt.Rows(i).Item(2)
            DGV.Rows(i).Cells(2).Value = dt.Rows(i).Item(3)
        Next
    End Sub

    'Refresh 
    Sub RefreshInput()
        TextNama.Clear()
        Textwaktu.Clear()
        Avatar.Text = " "
        PictureBox1.ImageLocation = ""
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        BukaFile.Filter = "Type Gambar (*.JPG;*.PNG;*.JPEG) | *.jpg;*.png;*.jpeg"
        BukaFile.ShowDialog()
        Avatar.Text = BukaFile.FileName
        PictureBox1.ImageLocation = Avatar.Text
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        'Cek Inputan
        If TextNama.Text = "" Or Textwaktu.Text = "" Or TextAlamat.Text = "" Or TextBiaya.Text = "" Or Avatar.Text = "" Then
            MsgBox("Inputan termasuk Gambar Harus diisi")
        Else
            Try
                Konek()
                cmd = New MySqlCommand("INSERT INTO transaksi (nama, waktu, alamat, biaya, avatar) VALUES(@nama, @waktu, @alamat, @biaya, @avatar)", Koneksi)
                'Tambahkan Parameter Value
                cmd.Parameters.AddWithValue("@nama", TextNama.Text)
                cmd.Parameters.AddWithValue("@waktu", Textwaktu.Text)
                cmd.Parameters.AddWithValue("@alamat", TextAlamat.Text)
                cmd.Parameters.AddWithValue("@biaya", TextBiaya.Text)
                cmd.Parameters.AddWithValue("@avatar", Avatar.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Data Rental Berhasil Disimpan")
                TampilData()
                RefreshInput()


            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub FormTeachers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilData()

    End Sub

    Private Sub DGV_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellClick
        Dim i As Integer
        i = DGV.CurrentRow.Index
        TextNama.Text = DGV.Item(0, i).Value
        Textwaktu.Text = DGV.Item(1, i).Value
        TextAlamat.Text = DGV.Item(2, i).Value
        TextBiaya.Text = DGV.Item(3, i).Value
        Avatar.Text = DGV.Item(4, i).Value
        PictureBox1.ImageLocation = Avatar.Text
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        Dim Result As DialogResult = MessageBox.Show("Apakah Yakin akan Hapus Data ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Result = DialogResult.Yes Then
            If TextNama.Text = "" Then
                MsgBox("Kode Tidak Ditemukan")
            Else
                'Delate
                Try
                    Konek()
                    cmd = New MySqlCommand("DELETE FROM transaksi WHERE nama ='" & TextNama.Text & "'", Koneksi)
                    cmd.ExecuteNonQuery()
                    MsgBox("Data Program Berhasil Dihapus")
                    TampilData()
                    RefreshInput()


                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
            End If
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        RefreshInput()
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If TextNama.Text = "" Then
            MsgBox("Kode Tidak Ditemukan")
        Else
            Try
                Konek()
                cmd = New MySqlCommand("UPDATE transaksi SET waktu =@waktu,avatar=@avatar WHERE kode= @kode", Koneksi)
                cmd.Parameters.AddWithValue("@nama", TextNama.Text)
                cmd.Parameters.AddWithValue("@waktu", Textwaktu.Text)
                cmd.Parameters.AddWithValue("@alamat", TextAlamat.Text)
                cmd.Parameters.AddWithValue("@biaya", TextBiaya.Text)
                cmd.Parameters.AddWithValue("@avatar", Avatar.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Data Guru diperbarui")
                TampilData()
                RefreshInput()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        CariData()
    End Sub

    Private Sub TextCari_TextChanged(sender As Object, e As EventArgs) Handles TextCari.TextChanged
        CariData()
    End Sub
End Class