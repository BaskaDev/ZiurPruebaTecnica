using System.Collections.Generic;
using System.Threading.Tasks;
using ZiurBlazorApp.Models;

namespace ZiurBlazorApp.Services
{
    public interface IDocumentoService
    {
        Task<List<DocumentoFillCombo>> GetDocumentosAsync();
    }
} 