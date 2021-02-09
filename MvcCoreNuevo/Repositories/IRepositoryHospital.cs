using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Data
{
    public interface IRepositoryHospital
    {
        List<Departamento> GetDepartamentos();
        Departamento BuscarDepartamento(int id);
        VistaDept GetRegistroDepartamento(int posicion);
        int GetNumRegVistaDepartamentos();
        List<VistaDept> GetGrupoDepartamentos(int posicion);
        Departamento GetDepartamentoPosicion(int posicion, ref int salida);
        List<Departamento> GetGrupoDepartamentosSQL(int posicion, ref int numeroregistros);
        List<Empleado> GetGrupoEmpleadosSQL(int posicion, int regpag, ref int numeroregistros);
    }
}
