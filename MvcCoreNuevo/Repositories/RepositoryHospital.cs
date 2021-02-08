using MvcCoreNuevo.Data;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region VISTA DEPARTAMENTOS PAGINACION
//alter view VISTADEPT as select CAST(isnull(row_number() over(order by dept_no),0) as INT) as posicion
//, DEPT.* from DEPT
#endregion

namespace MvcCoreNuevo.Repositories
{
    public class RepositoryHospital : IRepositoryHospital
    {
        private HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }
        public Departamento BuscarDepartamento(int id)
        {
            return this.context.Departamentos.Where(x => x.Numero == id).FirstOrDefault();
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public List<VistaDept> GetGrupoDepartamentos(int posicion)
        {
            return this.context.VistaDepartamentos.Where(x => x.Posicion >= posicion && x.Posicion < (posicion + 2)).ToList();
        }

        public int GetNumRegVistaDepartamentos()
        {
            return this.context.VistaDepartamentos.Count();
        }

        public VistaDept GetRegistroDepartamento(int posicion)
        {
            return this.context.VistaDepartamentos.Where(x => x.Posicion == posicion).FirstOrDefault();
        }
    }
}
