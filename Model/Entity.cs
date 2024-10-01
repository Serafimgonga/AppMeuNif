using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppConsultaNif.Model
{
    public class Entity
    {
       
            [JsonPropertyName("sucess")]
            public bool? Sucess { get; set; } = false;

            [JsonPropertyName("message")]
            public string? Message { get; set; } = string.Empty;

            [JsonPropertyName("data")]
            public Data data { get; set; } = new();
    }

        public class Data
        {
            [JsonPropertyName("numero")]
            public string? Numero { get; set; }

            [JsonPropertyName("nome")]
            public string? Nome { get; set; }

            [JsonPropertyName("nif")]
            public string? Nif { get; set; }

            [JsonPropertyName("data_nasc")]
            public string? DataNasc { get; set; }

            [JsonPropertyName("genero")]
            public string? Genero { get; set; }

            [JsonPropertyName("naturalidade")]
            public string? Naturalidade { get; set; }

            [JsonPropertyName("pai_nome_completo")]
            public string? PaiNomeCompleto { get; set; }

            [JsonPropertyName("mae_nome_completo")]
            public string? MaeNomeCompleto { get; set; }

            [JsonPropertyName("estado_civil")]
            public string? EstadoCivil { get; set; }

            [JsonPropertyName("data_emissao")]
            public string? DataEmissao { get; set; }

            [JsonPropertyName("emissao_local")]
            public string? EmissaoLocal { get; set; }
        }
}
