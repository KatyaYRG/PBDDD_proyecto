
Imports System.Data.SqlClient

Public Class Form1



    Dim Conexion As New SqlConnection("NULL")    'Dirección de server'


    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        If MsgBox("¿Desea salir?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Empleado") = MsgBoxResult.Yes Then

            Me.Close()

        End If

    End Sub

    Private Sub btnAltaProveedor_Click(sender As Object, e As EventArgs) Handles btnAltaProveedor.Click

        Conexion.Open()

        Dim CMD As New SqlCommand("altaproveedor", Conexion)
        CMD.CommandType = CommandType.StoredProcedure

        With CMD

            .Parameters.AddWithValue("@nombre", txtNombre.Text)
            .Parameters.AddWithValue("@domicilio", txtDomicilio.Text)
            .Parameters.AddWithValue("@ciudad", txtCiudad.Text)
            .Parameters.AddWithValue("@cp", txtCP.Text)
            .Parameters.AddWithValue("@telefono", txtTelefono.Text)
            .Parameters.AddWithValue("@email", txtEmail.Text)

            .ExecuteNonQuery()

        End With

        MsgBox("Los datos del empleado se guardaron exitoamente", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Empleado")

        Call MostrarProveedores()
        Call limpiar()
        Conexion.Close()

    End Sub

    Sub MostrarProveedores()
        Dim DA As New SqlDataAdapter("mostrarproveedor", Conexion)
        Dim DS As New DataSet
        DA.Fill(DS, "proveedor")
        Me.DataGridView1.DataSource = DS.Tables("proveedor")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call MostrarProveedores()

    End Sub

    Sub limpiar()
        txtcodigo.Clear()
        txtNombre.Clear()
        txtDomicilio.Clear()
        txtCiudad.Clear()
        txtCP.Clear()
        txtTelefono.Clear()
        txtEmail.Clear()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click

        Call limpiar()

    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click

        Dim fila As DataGridViewRow = DataGridView1.CurrentRow

        txtcodigo.Text = fila.Cells(0).Value
        txtNombre.Text = fila.Cells(1).Value
        txtDomicilio.Text = fila.Cells(2).Value
        txtCiudad.Text = fila.Cells(3).Value
        txtCP.Text = fila.Cells(4).Value
        txtTelefono.Text = fila.Cells(5).Value
        txtEmail.Text = fila.Cells(6).Value


    End Sub

    Private Sub btnModificarProveedor_Click(sender As Object, e As EventArgs) Handles btnModificarProveedor.Click

        Conexion.Open()
        If MsgBox("¿Desea modificar los datos del empleado?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Empleado") = MsgBoxResult.Yes Then

            Dim dt As New DataTable
            Dim cmd As New SqlCommand("modificarproveedor", Conexion)
            cmd.CommandType = CommandType.StoredProcedure

            With cmd
                .Parameters.AddWithValue("@nombre", txtNombre.Text)
                .Parameters.AddWithValue("@domicilio", txtDomicilio.Text)
                .Parameters.AddWithValue("@ciudad", txtCiudad.Text)
                .Parameters.AddWithValue("@cp", txtCP.Text)
                .Parameters.AddWithValue("@telefono", txtTelefono.Text)
                .Parameters.AddWithValue("@email", txtEmail.Text)
                .Parameters.AddWithValue("@codigo", txtcodigo.Text)
                .ExecuteNonQuery()
            End With
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Me.DataGridView1.DataSource = dt

            MsgBox("Los datos del proveedor se modificaron correctamente", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Proveedor")

            Call MostrarProveedores()
            Call limpiar()

        End If

        Conexion.Close()

    End Sub

    Private Sub btnMostrar_Click(sender As Object, e As EventArgs) Handles btnMostrar.Click

        Call MostrarProveedores()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Hora.Text = DateTime.Now

    End Sub

End Class
