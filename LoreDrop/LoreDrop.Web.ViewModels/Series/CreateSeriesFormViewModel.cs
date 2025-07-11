namespace LoreDrop.Web.ViewModels.Series;

using System.ComponentModel.DataAnnotations;
using static ViewModels.ValidationMessage.Series;
using static LoreDrop.GCommon.ValidationConstants.Series;

public class CreateSeriesFormViewModel
{
    public CreateSeriesFormViewModel()
    {
        this.CreatedOn = DateTime.UtcNow.ToString(DateFormat);
    }

    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = TitleRequiredMessage)]
    [MinLength(TitleMinLength, ErrorMessage = TitleMinLengthMessage)]
    [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthMessage)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = GenreRequiredMessage)]
    [Display(Name = "Genre")]
    public int GenreId { get; set; }

    public IEnumerable<AddSeriesGenreDropDownMenu> Genres { get; set; } = new HashSet<AddSeriesGenreDropDownMenu>(); // ⭐ The list of genres

    [Required(ErrorMessage = DescriptionRequiredMessage)]
    [MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthMessage)]
    [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = AuthorRequiredMessage)]
    [MinLength(AuthorNameMinLength, ErrorMessage = AuthorNameMinLengthMessage)]
    [MaxLength(AuthorNameMaxLength, ErrorMessage = AuthorNameMaxLengthMessage)]
    public string Author { get; set; } = null!;

    [Required(ErrorMessage = DateAddedRequiredMessage)]
    public string CreatedOn { get; set; } = null!;

    public string? ImageUrl { get; set; }
}
