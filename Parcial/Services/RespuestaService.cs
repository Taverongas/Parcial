using Parcial.DAO;
using Parcial.Interfaces;
using Parcial.Models;

public class RespuestaService : IRespuestaService
{
    private readonly ApplicationDbContext _context;

    public RespuestaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CrearRespuesta(int preguntaId, string contenido)
    {
        var respuesta = new Respuesta
        {
            PreguntaId = preguntaId,
            Contenido = contenido
        };

        _context.Respuestas.Add(respuesta);
        await _context.SaveChangesAsync();
    }
}