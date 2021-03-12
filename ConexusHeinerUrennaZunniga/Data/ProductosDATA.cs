using Microsoft.Data.SqlClient;
using Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConexusHeinerUrennaZunniga.Data
{
    public class ProductosDATA
    {
        string connectingString = "Server=(localhost)\\mssqllocaldb; Database=personas; Trusted_Connection=True; MultipleActiveResultSets=True";
        /// <summary>
        /// Metodo para actualizar un producto
        /// </summary>
        /// <param name="producto">recibe el producto a actualizar</param>
        public void UpdateProducto(Productos producto)
        {
            using(SqlConnection con = new SqlConnection(connectingString)) 
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;//le dice que lo que va a ejecutar es un procedimiento almacenado

                //se agregan todos los campos que se quieran actualizar
                cmd.Parameters.AddWithValue("@COD_PRODUCTO", producto.COD_PRODUCTO);
                cmd.Parameters.AddWithValue("@PRODUCTO", producto.PRODUCTO);
                cmd.Parameters.AddWithValue("@DOSIS", producto.DOSIS);
                cmd.Parameters.AddWithValue("@ENTRATAMIENTO", producto.ENTRATAMIENTO);
                cmd.Parameters.AddWithValue("@MEDICO_PRESCRIPTOR", producto.MEDICO_PRESCRIPTOR);
                cmd.Parameters.AddWithValue("@CODIGO_VEEVA", producto.CODIGO_VEEVA);
                cmd.Parameters.AddWithValue("@ESPECIALIDAD_MEDICO_PRESCRIPTOR", producto.ESPECIALIDAD_MEDICO_PRESCRIPTOR);

                con.Open();//se abre la conexion
                cmd.ExecuteNonQuery();//se ejecuta la conexion
                con.Close();//se cierra la conexion
            }
        }
    }
}
