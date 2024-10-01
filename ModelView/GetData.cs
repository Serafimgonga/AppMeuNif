using AppConsultaNif.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppConsultaNif.ModelView
{
    public class GetData
    {

        private readonly string? _endpoint;

        
        public GetData()
        {
            _endpoint = App.Configuration["ApiSettings:Endpoint"];
        }
        
        public async Task<Entity> Get(string numero)
        {
            // Validação de entrada:
            if (string.IsNullOrWhiteSpace(numero))
            {
                throw new ArgumentException("Número de BI inválido", nameof(numero));
            }

            // HttpClient:
            using var client = new HttpClient();

            // Construção da URL:
            var url = $"{_endpoint+numero}";

            try
            {
                // Fazer a requisição HTTP:
                var response = await client.GetAsync(url);

                // Verificar se a resposta foi bem-sucedida:
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Erro do servidor: {response.StatusCode}");
                }

                // Deserializar o JSON:
                var entity = await response.Content.ReadFromJsonAsync<Entity>();

                // Validar o objeto recebido:
                if (entity == null || entity.data == null)
                {
                    throw new InvalidOperationException("Nenhum registo encontrado!");
                }

                // Retornar o objeto deserializado:
                return entity;
            }
            catch (HttpRequestException ex)
            {
                // Lançar a exceção de rede como uma exceção personalizada:
                throw new Exception("Erro de comunicação com o servidor", ex);
            }
            catch (JsonException ex)
            {
                // Lançar a exceção de deserialização como uma exceção personalizada:
                throw new Exception("Erro ao deserializar dados JSON", ex);
            }
            catch (Exception ex)
            {
                // Lançar a exceção de erro geral:
                throw new Exception($"{ex.Message}\n", ex);
            }
        }
    }
}
