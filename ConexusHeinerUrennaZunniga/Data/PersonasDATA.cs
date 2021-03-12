using Microsoft.Data.SqlClient;
using Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConexusHeinerUrennaZunniga.Data
{
    public class PersonasDATA
    {
        string connectingString = "Server=(localhost)\\mssqllocaldb; Database=personas; Trusted_Connection=True; MultipleActiveResultSets=True";
        /// <summary>
        /// metodo para actualizar una persona
        /// </summary>
        /// <param name="persona">objeto de tipo persona a actualizar</param>
        public void UpdatePersona(Personas persona)
        {
            using (SqlConnection con = new SqlConnection(connectingString))// lo usa y lo mata al finalizar la conexion
            {
                SqlCommand cmd = new SqlCommand("SP_UpdatePersona", con);
                cmd.CommandType = CommandType.StoredProcedure;//le dice que lo que va a ejecutar es un procedimiento almacenado

                //se agregan todos los campos que se quieran actualizar
                cmd.Parameters.AddWithValue("@ID_PACIENTE_TITULAR", persona.ID_PACIENTE_TITULAR);
                cmd.Parameters.AddWithValue("@ID_VENDOR", persona.ID_VENDOR);
                cmd.Parameters.AddWithValue("@ID_PERSONAL_PACIENTE", persona.ID_PERSONAL_PACIENTE);
                cmd.Parameters.AddWithValue("@NUMERO_CARNET", persona.NUMERO_CARNET);
                cmd.Parameters.AddWithValue("@NOMBRE", persona.NOMBRE);
                cmd.Parameters.AddWithValue("@PRIMER_APELLIDO", persona.PRIMER_APELLIDO);
                cmd.Parameters.AddWithValue("@SEGUNDO_APELLIDO", persona.SEGUNDO_APELLIDO);
                cmd.Parameters.AddWithValue("@PAIS", persona.PAIS);                

                con.Open();//se abre la conexion
                cmd.ExecuteNonQuery();//se ejecuta la conexion
                con.Close();//se cierra la conexion
            }
        }
    }
}
