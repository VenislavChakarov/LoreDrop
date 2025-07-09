namespace LoreDrop.Web.ViewModels.Series;

using System.ComponentModel.DataAnnotations;
using static ViewModels.ValidationMessage.Series;
using static LoreDrop.GCommon.ValidationConstants.Series;

public class SeriesFromViewModel
{
    public SeriesFromViewModel()
    {
        this.DateAdded = DateTime.UtcNow.ToString(DateFrmat);
    }

    public string Id { get; set; } = string.Empty;
    
    [Required(ErrorMessage = TitleRequiredMessage)]
    [MinLength(TitleMinLength, ErrorMessage = TitleMinLengthMessage)]
    [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthMessage)]
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = GenreRequiredMessage)]
    public IEnumerable<AddSeriesGenreDropDownMenu> Genre { get; set; } = new HashSet<AddSeriesGenreDropDownMenu>();
    
    [Required(ErrorMessage = DescriptionRequiredMessage)]
    [MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthMessage)]
    [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = AuthorRequiredMessage)]
    [MinLength(AuthorNameMinLength, ErrorMessage = AuthorNameMinLengthMessage)]
    [MaxLength(AuthorNameMaxLength, ErrorMessage = AuthorNameMaxLengthMessage)]
    public string Author { get; set; } = null!;
    
    [Required(ErrorMessage = DateAddedRequiredMessage)]
    public string DateAdded { get; set; } = null!;
    
    public string? ImageUrl { get; set; }
}