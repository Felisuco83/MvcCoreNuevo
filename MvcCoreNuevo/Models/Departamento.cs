﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("DEPT")]
    public class Departamento
    {
        [Key]
        [Column("DEPT_NO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Numero { get; set; }
        [Column("DNOMBRE")]
        public string Nombre { get; set; }
        [Column("LOC")]
        public string Localidad { get; set; }
    }
}
