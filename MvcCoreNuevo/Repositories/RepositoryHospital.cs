using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

#region procedimiento almacenado paginacion
//alter procedure paginarregistrodepartamento(@posicion int, @registros int out)
//as
//select @registros = count(dept_no) from vistadept
//select dept_no, dnombre, loc from vistadept where posicion = @posicion
//go
#endregion

#region procedimiento almacenado paginacion grupo
//create procedure paginargrupodepartamentos (@posicion int , @registros int out)
//as
//    select @registros = count(dept_no) from vistadept

//    select dept_no, dnombre, loc from vistadept where posicion >= @posicion and posicion < (posicion+2)
//go
#endregion

#region EjercicioTodosEmpleados
// vista todosempleados
//SELECT isnull(empleado_no, 0) AS IdEmpleado, apellido, funcion, salario, nombre
//FROM            plantilla p INNER JOIN
//                         hospital h ON p.HOSPITAL_COD = h.HOSPITAL_COD
//UNION
//SELECT        doctor_no, apellido, especialidad, salario, nombre
//FROM            doctor d INNER JOIN
//                         hospital h ON d .HOSPITAL_COD = h.HOSPITAL_COD
//UNION
//SELECT        emp_no, apellido, oficio, salario, DNOMBRE
//FROM            emp e INNER JOIN
//                         DEPT dept ON e.DEPT_NO = dept.DEPT_NO
//create view todosempleadosposicion as SELECT CAST(isnull(row_number() OVER(ORDER BY IdEmpleado), 0) AS INT) AS posicion, todosempleadosview.*
//FROM todosempleadosview

//select * from todosempleadosposicion;

//create procedure paginargrupoempleados (@posicion int , @registros int out, @regpag int)
//as
//    select @registros = count(IdEmpleado) from todosempleadosview

//    select te.IdEmpleado, te.apellido, te.funcion, te.posicion, te.salario, te.nombre from todosempleadosposicion te where posicion >= @posicion and posicion< @posicion + @regpag
//go
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

        public Departamento GetDepartamentoPosicion(int posicion, ref int salida)
        {
            string sql = "paginarregistrodepartamento @posicion, @registros out";
            SqlParameter pampos = new SqlParameter("@posicion", posicion);
            SqlParameter pamreg = new SqlParameter("@registros", -1);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            Departamento departamento = this.context.Departamentos.FromSqlRaw<Departamento>(sql, pampos,pamreg).AsEnumerable().FirstOrDefault();
            int numeroregistros = Convert.ToInt32(pamreg.Value);
            salida = numeroregistros;
            return departamento;
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public List<VistaDept> GetGrupoDepartamentos(int posicion)
        {
            return this.context.VistaDepartamentos.Where(x => x.Posicion >= posicion && x.Posicion < (posicion + 2)).ToList();
        }

        public List<Departamento> GetGrupoDepartamentosSQL(int posicion, ref int numeroregistros)
        {
            string sql = "paginargrupodepartamentos @posicion, @registros out";
            SqlParameter pamposicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamregistros = new SqlParameter("@registros", -1);
            pamregistros.Direction = System.Data.ParameterDirection.Output;
            List<Departamento> departamentos = this.context.Departamentos.FromSqlRaw(sql, pamposicion, pamregistros).ToList();
            numeroregistros = Convert.ToInt32(pamregistros.Value);
            return departamentos;
        }

        public List<Empleado> GetGrupoEmpleadosSQL(int posicion, int regpag, ref int numeroregistros)
        {
            string sql = "paginargrupoempleados @posicion, @registros out, @regpag";
            SqlParameter pamposicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamregistros = new SqlParameter("@registros", -1);
            SqlParameter pamregpag = new SqlParameter("@regpag", regpag);
            pamregistros.Direction = System.Data.ParameterDirection.Output;
            List<Empleado> empleados = this.context.TodosEmpleadosPosicion.FromSqlRaw(sql, pamposicion, pamregistros, pamregpag).ToList();
            numeroregistros = Convert.ToInt32(pamregistros.Value);
            return empleados;
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
