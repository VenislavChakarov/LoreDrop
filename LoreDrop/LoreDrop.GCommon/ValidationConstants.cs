namespace LoreDrop.GCommon;

public class ValidationConstants
{
    public static class Series
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 100;

        public const int DescriptionMinLength = 5;
        public const int DescriptionMaxLength = 10000;
        
        public const int AuthorNameMinLength = 3;
        public const int AuthorNameMaxLength = 100;
        
    }

    public static class Comments
    {
        public const int TextMinLength = 1;
        public const int TextMaxLength = 2000;
    }
    
    public static class Genre
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 50;
    }
}