﻿using MvcApiPersonajesAWS.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajesAWS.Services
{
    public class ServicePersonajes
    {
        private MediaTypeWithQualityHeaderValue header;
        private string UrlApi;
        public ServicePersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                    if (response.IsSuccessStatusCode)
                    {
                        List<Personaje> personajes = await response.Content.ReadAsAsync<List<Personaje>>();
                        return personajes;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task CreatePersonajeAsync(string nombre , string imagen)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    Personaje personaje = new Personaje
                    {
                        IdPersonaje = 0,
                        Nombre = nombre,
                        Imagen = imagen
                    };
                    string json = JsonConvert.SerializeObject(personaje);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(this.UrlApi + request, content);
                }
            }            
        }

        public async Task UpdatePersonajeAsync(int idpersonaje , string nombre , string imagen)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    Personaje personaje = new Personaje
                    {
                        IdPersonaje = idpersonaje,
                        Nombre = nombre,
                        Imagen = imagen
                    };
                    string json = JsonConvert.SerializeObject(personaje);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(this.UrlApi + request, content);
                }
            }
        }

        public async Task<Personaje> FindPersonajeAsync(int idpersonaje)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicies) =>
                {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    string request = "api/personajes/"+idpersonaje;
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);
                    HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                    if (response.IsSuccessStatusCode)
                    {
                        Personaje personaje = await response.Content.ReadAsAsync<Personaje>();
                        return personaje;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
