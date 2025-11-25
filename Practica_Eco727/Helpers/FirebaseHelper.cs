using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Practica_Eco727.Models;//nombre del proyecto y la carpeta models
using Firebase.Database.Query;


namespace Practica_Eco727.Helpers
{
    public class FirebaseHelper
    {

        private readonly FirebaseClient firebaseClient;

        public FirebaseHelper()
        {
            firebaseClient = new FirebaseClient("https://practicaeco727-default-rtdb.firebaseio.com/");
        }

        public async Task<List<Cancion>> GetAllCanciones()
        {
            var canciones = await firebaseClient
                .Child("Canciones")
                .OnceAsync<Cancion>();

            return canciones.Select(item => new Cancion
            {
                Id = item.Key,
                Titulo = item.Object.Titulo,
                Interprete = item.Object.Interprete,
                Genero = item.Object.Genero,
                Nacionalidad = item.Object.Nacionalidad,
                AnioLanzamiento = item.Object.AnioLanzamiento
            }).ToList();
        }


        public async Task AddCancion(Cancion nuevaCancion)
        {
            await firebaseClient
                .Child("Canciones")
                .PostAsync(nuevaCancion);
        }


        public async Task UpdateCancion(string id, Cancion cancionActualizada)
        {
            var toUpdateCancion = (await firebaseClient
                .Child("Canciones")
                .OnceAsync<Cancion>())
                .FirstOrDefault(c => c.Key == id);
            if (toUpdateCancion != null)
            {
                await firebaseClient
                    .Child("Canciones")
                    .Child(toUpdateCancion.Key)
                    .PutAsync(cancionActualizada);
            }
        }

        public async Task DeleteCancion(string id)
        {
            var toDeleteCancion = (await firebaseClient
                .Child("Canciones")
                .OnceAsync<Cancion>())
                .FirstOrDefault(c => c.Key == id);
            if (toDeleteCancion != null)
            {
                await firebaseClient
                    .Child("Canciones")
                    .Child(toDeleteCancion.Key)
                    .DeleteAsync();
            }
        }



    }
}
