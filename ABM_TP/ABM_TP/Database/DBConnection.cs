using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace ABM_TP.Database
{
    class DBConnection
    {
        private MySqlConnection connection;
        private string strConn;
        private static DBParam DBParam = new DBParam();

        public DBConnection(string strConn)
        {
            this.strConn = strConn + " SslMode = none; Convert Zero Datetime = True; CharSet = utf8;"; ;
        }


        public void connect()
        {

            this.connection = new MySqlConnection(DBParam.genStrConn());

            try
            {
                this.connection.Open();
            }

            catch (MySqlException ex)
            {
                string msg;
                switch (ex.Number)
                {
                    case 0:
                        msg = "No se pudo conectar al servidor. Usuario/Contraseña invalidos";
                        break;

                    case 1042:
                        msg = "No se pudo conectar al servidor. No se encuentra disponible.";
                        break;
                    default:
                        msg = "Error " + ex.Number + " favor de reportarlo para pronta solucion";
                        break;
                }
                //throw new DbException(msg);
            }

        }



        public bool disconnect()
        {
            connection.Close();
            return true;
        }
        /*
            catch (MySqlException ex)
            {
                string msg = "Error " + ex.Number + " favor de reportarlo para pronta solucion";
                throw new DBException(msg);
            }
            finally
            {
                Log.addMensaje("Conexion cerrada con la base de datos");
            }
            
        } */


        public bool ExecQuery(string sql)
        {
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql, this.connection);
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(sql);
                return false;
            }

        }

        /*
         * hacer selects
        public MySqlDataReader ExecQuerySelect(string sql)
        {
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql, this.connection);
                return myCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(sql);
                return null;
            }
        }
        */

        public string ExecQuerySelectDatoUnico(string sql)
        {
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql, this.connection);
                var reader = myCommand.ExecuteReader();
                var valor = "";

                if (reader.Read())
                    valor = reader.GetString(0);

                reader.Close();
                return valor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(sql);
                return "";
            }
        }

        public void ExecScript(string filename)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(filename, System.Text.Encoding.UTF8);
            var str = file.ReadToEnd();
            ExecQuery(str);
        }



        //Mètodo que trae los productos de la Base de Datos
        public List<Models.ProductoModel> Obtener_Productos()
        {
            connect();
            //string sql = "SELECT * FROM productos";

            List<Models.ProductoModel> retornoList = new List<Models.ProductoModel>();

            MySqlCommand myCommand = new MySqlCommand("SELECT * FROM producto", this.connection);


            MySqlDataReader reader;

            reader = myCommand.ExecuteReader();
            while (reader.Read())
            {

                retornoList.Add(new Models.ProductoModel
                {
                    Id = Convert.ToInt32(reader["IdProducto"]),
                    Nombre = Convert.ToString(reader["nombre"]),
                    Descripcion = Convert.ToString(reader["descripcion"])
                });
            }


            return retornoList;

        }

        //Método para Insertar nuevo producto
        public bool mtd_Insertar_Producto(Models.ProductoModel datosInsertar)
        {
            connect();
            bool resultado = true;
            string sql = "INSERT INTO producto (nombre,descripcion) VALUES (@nombre,@descripcion)";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {

                myCommand.Parameters.Add("@nombre", MySqlDbType.VarChar, 255).Value = datosInsertar.Nombre;
                myCommand.Parameters.Add("@descripcion", MySqlDbType.VarChar, 255).Value = datosInsertar.Descripcion;

                myCommand.ExecuteNonQuery();
            }
            return resultado;
        }

        //Método para Modificar Datos de un producto
        public bool mtd_Update_Producto(Models.ProductoModel datosActualizar)
        {
            connect();
            bool resultado = true;

            string sql = " UPDATE producto SET idProducto = @id , nombre = @nombre, descripcion = @descripcion WHERE idProducto= @id";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {

                myCommand.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = datosActualizar.Id;
                myCommand.Parameters.Add("@nombre", MySqlDbType.VarChar, 255).Value = datosActualizar.Nombre;
                myCommand.Parameters.Add("@descripcion", MySqlDbType.VarChar, 255).Value = datosActualizar.Descripcion;

                myCommand.ExecuteNonQuery();
                
            }

            return resultado;

        }

        //Método para Eliminar un producto
        public bool mtd_Eliminar_Producto(int id)
        {
            connect();
            bool resultado = true;

            string sql = " DELETE FROM producto WHERE idProducto = @id";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {
                myCommand.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = id;

                myCommand.ExecuteNonQuery();

            }
            
    

            return resultado;
        }

        //----------------------------------------------

        //Mètodo que trae las variaciones de la Base de Datos
        public List<Models.VariacionModel> Obtener_Variacion()
        {
            connect();
            string sql = "SELECT * FROM variacion";
            List<Models.VariacionModel> retornoList = new List<Models.VariacionModel>();

            MySqlCommand myCommand = new MySqlCommand(sql, this.connection);


            MySqlDataReader reader;

            reader = myCommand.ExecuteReader();
            while (reader.Read())
            {

                retornoList.Add(new Models.VariacionModel
                {
                    Id = Convert.ToInt32(reader["idVariacion"]),
                    Tamanio = Convert.ToString(reader["tamanio"]),
                    Stock = Convert.ToInt32(reader["stock"]),
                    Precio = Convert.ToInt32(reader["precio"]),
                    Id_color = Convert.ToInt32(reader["idColor"]),
                    Id_producto = Convert.ToInt32(reader["idProducto"])
                   
              
                });
            }


            return retornoList;

        }


        //Método para Insertar nueva variacion
        public bool mtd_Insertar_Variacion(Models.VariacionModel datosInsertar)
        {
            connect();
            bool resultado = true;
            string sql = "INSERT INTO variacion (tamanio,stock,precio,idProducto,idColor) VALUES (@tamanio,@stock,@precio,@idProducto,@idColor)";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {

                myCommand.Parameters.Add("@tamanio", MySqlDbType.VarChar).Value = datosInsertar.Tamanio;
                myCommand.Parameters.Add("@stock", MySqlDbType.Int32).Value = datosInsertar.Stock;
                myCommand.Parameters.Add("@precio", MySqlDbType.Float).Value = datosInsertar.Precio;
                myCommand.Parameters.Add("@idColor", MySqlDbType.Int32).Value = datosInsertar.Id_color;
                myCommand.Parameters.Add("@idProducto", MySqlDbType.Int32).Value = datosInsertar.Id_producto;

                myCommand.ExecuteNonQuery();
            }
            return resultado;
        }


        //Método para Modificar datos de una variacion
        public bool mtd_Update_Variacion(Models.VariacionModel datosActualizar)
        {
            connect();
            bool resultado = true;

            string sql = " UPDATE variacion SET idVariacion = @id , tamanio = @tamanio, stock = @stock, precio = @precio, idColor = @idColor, idProducto = @idProducto WHERE idVariacion= @id";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {

                myCommand.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = datosActualizar.Id;
                myCommand.Parameters.Add("@tamanio", MySqlDbType.VarChar, 255).Value = datosActualizar.Tamanio;
                myCommand.Parameters.Add("@stock", MySqlDbType.Int32, 255).Value = datosActualizar.Stock;
                myCommand.Parameters.Add("@precio", MySqlDbType.VarChar, 255).Value = datosActualizar.Precio;
                myCommand.Parameters.Add("@idColor", MySqlDbType.Int32).Value = datosActualizar.Id_color;
                myCommand.Parameters.Add("@idProducto", MySqlDbType.Int32).Value = datosActualizar.Id_producto;
                myCommand.ExecuteNonQuery();

            }

            return resultado;

        }

        //Método para Eliminar una variacion
        public bool mtd_Eliminar_Variacion(int id)
        {
            connect();
            bool resultado = true;

            string sql = " DELETE FROM variacion WHERE idVariacion = @id";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {
                myCommand.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = id;

                myCommand.ExecuteNonQuery();

            }



            return resultado;
        }

        //----------------------------------------------

        //Mètodo que trae los colores de la Base de Datos
        public List<Models.ColorModel> Obtener_Color()
        {
            connect();

            List<Models.ColorModel> retornoList = new List<Models.ColorModel>();

            MySqlCommand myCommand = new MySqlCommand("SELECT * FROM color", this.connection);


            MySqlDataReader reader;

            reader = myCommand.ExecuteReader();
            while (reader.Read())
            {

                retornoList.Add(new Models.ColorModel
                {
                    Id = Convert.ToInt32(reader["idColor"]),
                    Nombre = Convert.ToString(reader["nombre"]),
                    Hexa = Convert.ToString(reader["hexadecimal"]),
                });
            }


            return retornoList;

        }

        //Método para Insertar nueva color
        public bool mtd_Insertar_Color(Models.ColorModel datosInsertar)
        {
            connect();
            bool resultado = true;
            string sql = "INSERT INTO color (nombre,hexadecimal) VALUES (@nombre,@hexa)";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {

                myCommand.Parameters.Add("@nombre", MySqlDbType.VarChar, 255).Value = datosInsertar.Nombre;
                myCommand.Parameters.Add("@hexa", MySqlDbType.VarChar, 255).Value = datosInsertar.Hexa;

                myCommand.ExecuteNonQuery();
            }
            return resultado;
        }

        //Método para Modificar Datos de un color
        public bool mtd_Update_Color(Models.ColorModel datosActualizar)
        {
            connect();
            bool resultado = true;

            string sql = " UPDATE color SET idColor = @id , nombre = @nombre, hexadecimal = @hexa WHERE idColor = @id";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {

                myCommand.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = datosActualizar.Id;
                myCommand.Parameters.Add("@nombre", MySqlDbType.VarChar, 255).Value = datosActualizar.Nombre;
                myCommand.Parameters.Add("@hexa", MySqlDbType.VarChar, 255).Value = datosActualizar.Hexa;
      

                myCommand.ExecuteNonQuery();

            }

            return resultado;

        }

        //Método para Eliminar un color
        public bool mtd_Eliminar_Color(int id)
        {
            connect();
            bool resultado = true;

            string sql = " DELETE FROM color WHERE idColor = @id";

            using (MySqlCommand myCommand = new MySqlCommand(sql, this.connection))
            {
                myCommand.Parameters.Add("@id", MySqlDbType.Int32, 11).Value = id;

                myCommand.ExecuteNonQuery();

            }



            return resultado;
        }
    }
}