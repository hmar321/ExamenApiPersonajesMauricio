using ExamenApiPersonajesMauricio.Data;
using ExamenApiPersonajesMauricio.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenApiPersonajesMauricio.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        //personajes
        {
            return await this.context.Personajes.ToListAsync();
        }
        //personajes/{serie}
        public async Task<List<Personaje>> GetPersonajesBySerieAsync(string serie)
        {
            return await this.context.Personajes.Where(x => x.Serie == serie).ToListAsync();
        }
        //personajes/Series
        public async Task<List<string>> GetSeriesAsync()
        {
            return await this.context.Personajes.Select(x => x.Serie).Distinct().ToListAsync(); ;
        }
        //personajes/{id}
        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            return await this.context.Personajes.FirstOrDefaultAsync(x => x.IdPersonaje == id);
        }
        //personajes/insertpersonaje
        public async Task<Personaje> InsertPersonajeAsync(string nombre, string imagen, string serie)
        {
            int nextid = await this.context.Personajes.MaxAsync(x => x.IdPersonaje) + 1;
            Personaje personaje = new Personaje();
            personaje.IdPersonaje = nextid;
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            personaje.Serie = serie;
            this.context.Personajes.Add(personaje);
            int result = await this.context.SaveChangesAsync();
            if (result == 0)
            {
                return null;
            }
            else
            {
                return personaje;
            }
        }
        //personajes/updatepersonaje
        public async Task<int> UpdatePersonajeAsync(int id, string nombre, string imagen, string serie)
        {
            Personaje personaje = await this.FindPersonajeAsync(id);
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            personaje.Serie = serie;
            return await this.context.SaveChangesAsync();
        }
        //personajes/deletepersonaje/{id}
        public async Task<int> DeletePersonajeAsync(int id)
        {
            Personaje personaje = await this.FindPersonajeAsync(id);
            this.context.Personajes.Remove(personaje);
            return await this.context.SaveChangesAsync();
        }
    }
}
