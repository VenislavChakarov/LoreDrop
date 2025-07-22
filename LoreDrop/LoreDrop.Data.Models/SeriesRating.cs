using System;

namespace LoreDrop.Data.Models
{
    public class SeriesRating
    {
        public Guid Id { get; set; }
        public Guid SeriesId { get; set; }
        public Series Series { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public double Rating { get; set; }
    }
}

