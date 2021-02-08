using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("VISTADEPT")]
    public class VistaDept
    {
        [Column("POSICION")]
        //LO HACEMOS LONG SI QUEREMOS QUE NO PETE SIN HACER NADA EN LA VISTA
        public int Posicion { get; set; }
        [Key]
        [Column("DEPT_NO")]
        public int Numero { get; set; }
        [Column("LOC")]
        public string Localidad { get; set; }
        // SI QUISIERAMOS AÑADIR UNA PROPIEDAD CALCULADA POR EJEMPLO
        //[NotMapped]
        //public string OtraPropiedad { get; set; }
    }
}
