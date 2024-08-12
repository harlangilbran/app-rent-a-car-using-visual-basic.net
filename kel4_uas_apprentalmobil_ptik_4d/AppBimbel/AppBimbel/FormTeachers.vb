Imports MySql.Data.MySqlClient
Public Class FormTeachers
    'Fungsi Untuk tampil data
    Sub TampilData()
        Konek()

        da = New MySqlDataAdapter("SELECT * FROM teachers", Koneksi)
        dt = New DataTable
        da.Fill(dt)
        DGV.Rows.Clear()
        'Looping 
        For i = 0 To dt.Rows.Count - 1
            DGV.Rows.Add(dt.Rows(i).Item(1))
            DGV.Rows(i).Cells(1).Value = dt.Rows(i).Item(2)
            DGV.Rows(i).Cells(2).Value = dt.Rows(i).Item(3)
        Next
    End Sub
    'Cari data
    Sub CariData()
        Konek()

        da = New MySqlDataAdapter("SELECT * FROM teachers WHERE nama_guru like '%" & TextCari.Text & "%' or kode like '%" & TextCari.Text & "%' ", Koneksi)
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
        TextKode.Clear()
        TextNama.Clear()
        Avatar.Text = " "
        PictureBox1.ImageLocation = ""
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        BukaFile.Filter = "Jenis File (*.JPG;*.PNG;*.JPEG) | *.jpg;*.png;*.jpeg"
        BukaFile.ShowDialog()
        Avatar.Text = BukaFile.FileName
        PictureBox1.ImageLocation = Avatar.Text
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        'Cek Inputan
        If TextKode.Text = "" Or TextNama.Text = "" Or Avatar.Text = "" Then
            MsgBox("Inputan termasuk Gambar Harus diisi")
        Else
            Try
                Konek()
                cmd = New MySqlCommand("INSERT INTO teachers (kode,nama_guru,avatar) VALUES(@kode,@nama_guru,@avatar)", Koneksi)
                'Tambahkan Parameter Value
                cmd.Parameters.AddWithValue("@kode", TextKode.Text)
                cmd.Parameters.AddWithValue("@nama_guru", TextNama.Text)
                cmd.Parameters.AddWithValue("@avatar", Avatar.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Data Guru Berhasil Disimpan")
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
        TextKode.Text = DGV.Item(0, i).Value
        TextNama.Text = DGV.Item(1, i).Value
        Avatar.Text = DGV.Item(2, i).Value
        PictureBox1.ImageLocation = Avatar.Text
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        Dim Result As DialogResult = MessageBox.Show("Apakah Yakin akan Hapus Data ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Result = DialogResult.Yes Then
            If TextKode.Text = "" Then
                MsgBox("Kode Tidak Ditemukan")
            Else
                'Delate
                Try
                    Konek()
                    cmd = New MySqlCommand("DELETE FROM teachers WHERE kode='" & TextKode.Text & "'", Koneksi)
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
        If TextKode.Text = "" Then
            MsgBox("Kode Tidak Ditemukan")
        Else
            Try
                Konek()
                cmd = New MySqlCommand("UPDATE teachers SET nama_guru=@nama_guru,avatar=@avatar WHERE kode= @kode", Koneksi)
                cmd.Parameters.AddWithValue("@kode", TextKode.Text)
                cmd.Parameters.AddWithValue("@nama_guru", TextNama.Text)
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