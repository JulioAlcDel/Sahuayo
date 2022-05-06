using SahuayoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahuayoDatos.Interfaces
{
    public  interface IRepositorioGenerico<T> where T : class
    {
        Task<bool> Grabar(Persona entity);
        Task<bool> Eliminar(int id);

    }
}
