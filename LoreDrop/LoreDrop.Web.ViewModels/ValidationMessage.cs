namespace LoreDrop.Web.ViewModels;

public class ValidationMessage
{
    public static class Series
    {
        public const string TitleRequiredMessage = "Title is required.";
        public const string TitleMinLengthMessage = "Title must be at least 3 characters.";
        public const string TitleMaxLengthMessage = "Title cannot exceed 100 characters.";

        public const string GenreRequiredMessage = "Genre is required.";

        public const string AuthorRequiredMessage = "Author is required.";
        public const string AuthorNameMinLengthMessage = "Author name must be at least 3 characters.";
        public const string AuthorNameMaxLengthMessage = "Author name cannot exceed 100 characters.";

        public const string DescriptionRequiredMessage = "Description is required.";
        public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
        public const string DescriptionMaxLengthMessage = "Description cannot exceed 10000 characters.";
        
        public const string DateAddedRequiredMessage = "Date is required.";

    }
}