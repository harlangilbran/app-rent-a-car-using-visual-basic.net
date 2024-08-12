Imports MySql.Data.MySqlClient
Public Class FormPrograms
    'Fungsi Untuk tampil data
    Sub TampilData()
        Konek()

        da = New MySqlDataAdapter("SELECT * FROM programs", Koneksi)
        dt = New DataTable
        da.Fill(dt)
        DGV.Rows.Clear()
        'Looping 
        For i = 0 To dt.Rows.Count - 1
            DGV.Rows.Add(dt.Rows(i).Item(1))
            DGV.Rows(i).Cells(1).Value = dt.Rows(i).Item(1)
            DGV.Rows(i).Cells(2).Value = dt.Rows(i).Item(2)
            DGV.Rows(i).Cells(3).Value = dt.Rows(i).Item(3)
        Next
    End Sub
    'Cari data
    Sub CariData()
        Konek()

        da = New MySqlDataAdapter("SELECT * FROM programs WHERE program like '%" & TextCari.Text & "%' ", Koneksi)
        dt = New DataTable
        da.Fill(dt)
        DGV.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            DGV.Rows.Add(dt.Rows(i).Item(1))
            DGV.Rows(i).Cells(1).Value = dt.Rows(i).Item(2)
        Next
    End Sub
    'Kondisi Awal 
    Sub KondisiAwal()
        TextKode.Clear()
        TextProgram.Clear()
        TextNopol.Clear()
        TextWarna.Clear()
        BtnSimpan.Text = "Simpan"
        BtnHapus.Enabled = False
        TextKode.Enabled = True
    End Sub

    Private Sub FormPrograms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilData()
        KondisiAwal()
        BtnHapus.Enabled = False
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        'Buat Validasi
        If TextKode.Text = "" Or TextProgram.Text = "" Or TextNopol.Text = "" Or TextWarna.Text = "" Then
            MsgBox("Inputan wajib di isi")
        Else
            Try
                Konek()

                'Insert
                If BtnSimpan.Text = "Simpan" Then
                    cmd = New MySqlCommand("INSERT INTO programs (kode,program,nopol,warna) VALUES ('" & TextKode.Text & "' , '" & TextProgram.Text & "', '" & TextProgram.Text & "', '" & TextProgram.Text & "' )", Koneksi)
                    cmd.ExecuteNonQuery()
                    MsgBox("Data Programs Berhasil di Simpan")
                Else
                    'Update
                    cmd = New MySqlCommand("UPDATE programs SET program= '" & TextProgram.Text & "' WHERE kode= '" & TextKode.Text & "' WHERE nopol= '" & TextNopol.Text & "' WHERE warna= '" & TextWarna.Text & "' ", Koneksi)
                    cmd.ExecuteNonQuery()
                    MsgBox("Data Programs Berhasil di Update")
                End If

                TampilData()
                KondisiAwal()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        KondisiAwal()
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        Dim Result As DialogResult = MessageBox.Show("Apakah Yakin akan Hapus Data ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Result = DialogResult.Yes Then
            'Delate
            Try
                Konek()
                cmd = New MySqlCommand("DELETE FROM programs WHERE kode='" & TextKode.Text & "'", Koneksi)
                cmd.ExecuteNonQuery()
                MsgBox("Data Program Berhasil Dihapus")
                TampilData()
                KondisiAwal()

            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub

    Private Sub DGV_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellClick
        Dim i As Integer
        i = DGV.CurrentRow.Index
        TextKode.Text = DGV.Item(0, i).Value
        TextProgram.Text = DGV.Item(1, i).Value
        TextNopol.Text = DGV.Item(2, i).Value
        TextWarna.Text = DGV.Item(3, i).Value
        TextKode.Enabled = False
        BtnHapus.Enabled = True
        BtnSimpan.Text = "Update"

    End Sub

    Private Sub TextCari_TextChanged(sender As Object, e As EventArgs) Handles TextCari.TextChanged
        CariData()
    End Sub

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        CariData()

    End Sub


End Class