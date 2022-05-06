using SahuayoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahuayoDatos.Interfaces
{
    public interface IPersonaRepositorio: IRepositorioGenerico<Persona>
    {
        Task<Persona> GetPersonaId(int id);
        Task<List<Persona>> GetListaPersona();
    }
}
