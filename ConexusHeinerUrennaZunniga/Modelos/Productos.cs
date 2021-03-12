using System;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Productos
    {
        [Key]
        public int Id { get; set; }
        public int COD_PRODUCTO { get; set; }
        public String PRODUCTO { get; set; }
        public String DOSIS { get; set; }
        public Boolean ENTRATAMIENTO { get; set; }
        public String MEDICO_PRESCRIPTOR { get; set; }
        public int CODIGO_VEEVA { get; set; }
        public int ESPECIALIDAD_MEDICO_PRESCRIPTOR { get; set; }
        public DateTime FECHA_ALTA_PRODUCTO { get; set; }
    }
}
