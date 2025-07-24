using LoreDrop.Web.ViewModels.Series;

namespace LoreDrop.Services.Core.Contracts;

public interface IGenreService
{
    Task<IEnumerable<AddSeriesGenreDropDownMenu>> GetAllGenresAsync();
}