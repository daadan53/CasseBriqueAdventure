using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique
{
    public static class ServiceLocator
    {
        // On crée un dico qui contiendra les méthodes et autres qu'on aura recup
        private static readonly Dictionary<Type, object> listServices = new Dictionary<Type, object>();

        // Une méthode qui va récup et add au dico ce qu'on a ajouter avec son type
        public static void RegisterService<T>(T service)
        {
            listServices[typeof(T)] = service; // On lui passe avec le type de notre paramètre
        }

        // On balance ce que on nous a demandé avec son type = T
        public static T GetService<T>()
        {
            return (T)listServices[typeof(T)]; // Reroune le service de Type T à l'indice T
        }
    }
}
