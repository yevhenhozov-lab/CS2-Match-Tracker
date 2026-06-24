using System;

namespace CS2MatchTracker.Core.Entities
{
    /// <summary>
    /// Представляє кіберспортивну команду з дисципліни CS2.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Отримує або задає повну назву команди.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отримує або задає офіційний тег команди (наприклад, NAVI).
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Отримує або задає країну походження команди.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Отримує або задає поточний рейтинг команди в балах.
        /// </summary>
        public int RatingPoints { get; set; }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Team"/>.
        /// </summary>
        public Team(string name, string tag, string country, int ratingPoints)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            RatingPoints = ratingPoints;
        }

        /// <summary>
        /// Повертає текстове представлення команди для виведення в консоль.
        /// </summary>
        public override string ToString()
        {
            return $"[{Tag}] {Name} ({Country}) — Рейтинг: {RatingPoints} у.о.";
        }
    }
}
