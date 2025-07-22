using Microsoft.AspNetCore.Components;
using ZiurBlazorApp.Models;
using ZiurBlazorApp.Services;
using System.Text.RegularExpressions;

public class Home : ComponentBase
{
    [Inject]
    protected IDocumentoService DocumentoService { get; set; } = default!;

    protected List<DocumentoFillCombo>? documentos = null;
    protected bool isLoading = true;
    protected string? errorMessage = null;
    private string searchText = string.Empty;

    protected const int PageSize = 10;
    protected int CurrentPage = 1;

    protected List<DocumentoFillCombo> FilteredDocumentos =>
        documentos == null
            ? new List<DocumentoFillCombo>()
            : documentos.Where(d =>
                (string.IsNullOrWhiteSpace(searchText) ||
                 (d.Descripcion != null && d.Descripcion.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                 d.Codigo.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase))
            ).ToList();

    protected int TotalPages =>
        (int)Math.Ceiling((double)FilteredDocumentos.Count / PageSize);

    protected IEnumerable<DocumentoFillCombo> PagedDocumentos =>
        FilteredDocumentos
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize);

    protected override async Task OnInitializedAsync()
    {
        try
        {
            documentos = await DocumentoService.GetDocumentosAsync();
        }
        catch (Exception ex)
        {
            errorMessage = $"Error al cargar documentos: {ex.Message}";
        }
        finally
        {
            isLoading = false;
        }
    }

    protected void PreviousPage()
    {
        if (CurrentPage > 1)
            CurrentPage--;
    }

    protected void NextPage()
    {
        if (CurrentPage < TotalPages)
            CurrentPage++;
    }

    protected void GoToPage(int page)
    {
        if (page >= 1 && page <= TotalPages)
            CurrentPage = page;
    }

    protected string SearchText
    {
        get => searchText;
        set
        {
            if (searchText != value)
            {
                searchText = value;
                CurrentPage = 1;
            }
        }
    }

    protected MarkupString HighlightText(string text, string search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return new MarkupString(text);
        var idx = text.IndexOf(search, StringComparison.OrdinalIgnoreCase);
        if (idx < 0)
            return new MarkupString(text);
        var highlighted = Regex.Replace(
            text,
            Regex.Escape(search),
            match => $"<mark>{match.Value}</mark>",
            RegexOptions.IgnoreCase
        );
        return new MarkupString(highlighted);
    }
} 