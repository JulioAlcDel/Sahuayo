using AutoMapper;
using SahuayoDatos.Entidades;
using SahuayoPrueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SahuayoPrueba.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Persona, PersonaViewModel>();
            CreateMap<PersonaViewModel, Persona>();
        }
    }
}
