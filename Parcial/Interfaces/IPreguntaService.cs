using Parcial.Models;

namespace Parcial.Interfaces
{
    public interface IPreguntaService
    {
        Task<Pregunta> CrearPregunta(Pregunta pregunta);
        Task<List<Pregunta>> ObtenerPorEstado(string estado);
        Task<Respuesta> CrearRespuesta(Respuesta respuesta);
    }
}
