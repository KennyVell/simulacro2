using System.Net;
using simulacro2.DTOs.Tratamientos;
using simulacro2.Models;

namespace simulacro2.Services.Tratamientos
{
    public interface ITratamientosRepository
    {
        Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> Add(TratamientoDTO tratamientoDTO);
        Task<(IEnumerable<Tratamiento> tratamientos, string mensaje, HttpStatusCode statusCode)> GetAll();
        Task<(IEnumerable<Tratamiento> tratamientos, string mensaje, HttpStatusCode statusCode)> GetAllDeleted();
        Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> GetById(int id);
        Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> Update(int id, TratamientoDTO tratamientoDTO);
        Task Delete(int id);
        Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> Restore(int id);
    }
}