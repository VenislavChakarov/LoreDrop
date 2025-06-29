namespace LoreDrop.GCommon;

public class ValidationConstants
{
    public static class Content
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 100;

        public const int DescriptionMinLength = 5;
        public const int DescriptionMaxLength = 10000;
        
    }

    public static class Comment
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