using Microsoft.EntityFrameworkCore;
using Parcial.DAO;
using Parcial.Interfaces;
using Parcial.Models;

namespace Parcial.Services
{
    public class PreguntaService : IPreguntaService
    {
        private readonly ApplicationDbContext _context;

        public PreguntaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pregunta> CrearPregunta(Pregunta pregunta)
        {
            _context.Preguntas.Add(pregunta);
            await _context.SaveChangesAsync();
            return pregunta;
        }

        public async Task<List<Pregunta>> ObtenerPorEstado(string estado)
        {
            return await _context.Preguntas
                .Where(p => p.Estado.ToLower() == estado.ToLower())
                .ToListAsync();
        }

        public async Task<Respuesta> CrearRespuesta(Respuesta respuesta)
        {
            _context.Respuestas.Add(respuesta);

            var pregunta = await _context.Preguntas.FindAsync(respuesta.PreguntaId);
            if (pregunta != null)
            {
                CambiarEstadoPregunta(pregunta);
            }

            await _context.SaveChangesAsync();
            return respuesta;
        }
        private void CambiarEstadoPregunta(Pregunta pregunta)
        {
            pregunta.Estado = "Resuelta";
        }
    }
}
