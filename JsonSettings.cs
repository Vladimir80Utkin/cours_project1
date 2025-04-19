using System.Text.Encodings.Web;
using System.Text.Json;

namespace cours_project
{
    class JsonSettings{
        public static JsonSerializerOptions LinesOptions { get; } = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = false
        };
    }
}