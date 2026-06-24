using System;

namespace CS2MatchTracker.Core.Entities
{
    public class Team
    {
        public string Name { get; set; }

        public string Tag { get; set; }

        public string Country { get; set; }

        public int RatingPoints { get; set; }

        public Team(string name, string tag, string country, int ratingPoints)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            RatingPoints = ratingPoints;
        }

        public override string ToString()
        {
            return $"[{Tag}] {Name} ({Country}) — Рейтинг: {RatingPoints} у.о.";
        }
    }
}
