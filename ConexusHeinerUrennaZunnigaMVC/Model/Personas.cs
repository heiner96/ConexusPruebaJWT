using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Personas
    {
        [Key]
        public int Id { get; set; }
        public String ID_PACIENTE_TITULAR { get; set; }
        public int ID_VENDOR { get; set; }
        public int ID_PERSONAL_PACIENTE { get; set; }
        public int NUMERO_CARNET { get; set; }
        public String NOMBRE { get; set; }
        public String PRIMER_APELLIDO { get; set; }
        public String SEGUNDO_APELLIDO { get; set; }
        public String PAIS { get; set; }
        public String GENERO { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime FECHA_NACIMIENTO { get; set; }
        public String TIPO_IDENTIFICACION { get; set; }
        public String FUENTE_INSCRIPCION { get; set; }
        public String TELEFONO { get; set; }
        public String TELEFONO2 { get; set; }
        public String MOVIL_CELULAR { get; set; }
        public String EMAIL { get; set; }
        public String DIRECCIONDETALLADA { get; set; }
        public String PARENTESCO_CON_TITULAR_CUENTA { get; set; }
        public String PROVINCIA { get; set; }
        public String TIPO_PACIENTE { get; set; }
        public DateTime FECHA_CREACION { get; set; }
        public DateTime FECHA_INSCRIPCION { get; set; }
        public DateTime FECHA_ULTIMA_MODIFICACION { get; set; }
        public String COMO_SE_ENTERO_PROGRAMA { get; set; }
        public Boolean ACEPTA_SER_CONTACTADO { get; set; }
        public Boolean ACEPTA_CONTACTO_TELEFONO { get; set; }
        public Boolean ACEPTA_CONTACTO_CELULAR { get; set; }
        public Boolean ACEPTA_CONTACTO_CORREO { get; set; }
        public int ESTADO { get; set; }
        public int authorizedAccount { get; set; }
        public Productos Productos { get; set; }
    }
}
