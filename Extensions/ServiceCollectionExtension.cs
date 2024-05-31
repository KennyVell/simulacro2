

using System.Reflection;

namespace simulacro2.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            // Obtiene todos los tipos en el ensamblado que terminan con "Repository"
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
                .ToList();

            foreach (var type in types)
            {
                // Busca una interfaz que coincida con el nombre de la clase prefijada con 'I'
                var interfaceType = type.GetInterface($"I{type.Name}");
                if (interfaceType != null)
                {
                    // Registra la implementación en el contenedor de DI
                    services.AddScoped(interfaceType, type);
                }
            }
        }
    }
}