using LoreDrop.Web.ViewModels.Admin.SeriesAdmin;

namespace LoreDrop.Services.Core.Contracts;

public interface IGenreService
{
    Task<IEnumerable<AddSeriesGenreDropDownMenu>> GetAllGenresAsync();
}