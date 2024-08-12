Public Class FormUtama
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FormTransaksi.Close()
        With FormPrograms
            .TopLevel = False
            CONTENT.Controls.Add(FormPrograms)
            .BringToFront()
            .Show()

        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FormPrograms.Close()
        With FormTransaksi
            .TopLevel = False
            CONTENT.Controls.Add(FormTransaksi)
            .BringToFront()
            .Show()

        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FormPrograms.Close()
        FormTransaksi.Close()
    End Sub
End Class