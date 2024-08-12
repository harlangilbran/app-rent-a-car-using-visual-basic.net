Imports MySql.Data.MySqlClient

Module KoneksiDB
    Dim MySQLKonek = "Server=localhost;Database=kel4_uas_apprentalmobil_ptik_4d;User=root;Password=;SslMode=none"
    Public Koneksi As New MySqlConnection(MySQLKonek)

    Public da As MySqlDataAdapter = Nothing
    Public cmd As MySqlCommand = Nothing
    Public dt As New DataTable
    Public dr As MySqlDataReader

    Public Status As Boolean = False

    Public Sub Konek()
        Try
            If Not Koneksi Is Nothing Then Koneksi.Close()
            Koneksi.Open()
            'MsgBox("koneksi sukses")
        Catch ex As Exception
            MsgBox(ex.Message
                   )

        End Try
    End Sub
    Function Diskonek()
        Koneksi.Close()
        Return Koneksi



    End Function
End Module
