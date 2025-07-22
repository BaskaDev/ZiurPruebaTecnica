using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ZiurBlazorApp.Models;

namespace ZiurBlazorApp.Services
{
    public class DocumentoService : IDocumentoService
    {
        private readonly HttpClient _httpClient;
        private string? _bearerToken;
        private string? _apiUrl;
        private bool _configLoaded = false;

        public DocumentoService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private async Task LoadConfigAsync()
        {
            if (_configLoaded) return;
            var response = await _httpClient.GetAsync("appsettings.json");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var config = JsonSerializer.Deserialize<ApiConfig>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            _apiUrl = config?.ApiUrl ?? throw new InvalidOperationException("ApiUrl no configurada");
            _bearerToken = config?.BearerToken ?? throw new InvalidOperationException("BearerToken no configurado");
            _configLoaded = true;
        }

        public async Task<List<DocumentoFillCombo>> GetDocumentosAsync()
        {
            await LoadConfigAsync();
            if (string.IsNullOrWhiteSpace(_apiUrl) || string.IsNullOrWhiteSpace(_bearerToken))
                throw new InvalidOperationException("Configuración de API incompleta");

            using var request = new HttpRequestMessage(HttpMethod.Get, _apiUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            using var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al obtener documentos: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<DocumentoFillCombo>();
            }

            var documentos = JsonSerializer.Deserialize<List<DocumentoFillCombo>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return documentos ?? new List<DocumentoFillCombo>();
        }

        private class ApiConfig
        {
            public string? ApiUrl { get; set; }
            public string? BearerToken { get; set; }
        }
    }
} 