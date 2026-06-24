using System;

namespace CS2MatchTracker.Core.Entities
{
    /// <summary>
    /// Представляє результат кіберспортивного матчу між двома командами.
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Отримує або задає дату проведення матчу.
        /// </summary>
        public DateTime MatchDate { get; set; }

        /// <summary>
        /// Отримує або задає назву турніру.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Отримує або задає формат матчу (наприклад, Bo1, Bo3, Bo5).
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Отримує або задає першу команду (Team 1).
        /// </summary>
        public Team Team1 { get; set; }

        /// <summary>
        /// Отримує або задає другу команду (Team 2).
        /// </summary>
        public Team Team2 { get; set; }

        /// <summary>
        /// Отримує або задає кількість виграних карт першою командою.
        /// </summary>
        public int ScoreTeam1 { get; set; }

        /// <summary>
        /// Отримує або задає кількість виграних карт другою командою.
        /// </summary>
        public int ScoreTeam2 { get; set; }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Match"/>.
        /// </summary>
        public Match(DateTime matchDate, string tournamentName, string format, Team team1, Team team2, int scoreTeam1, int scoreTeam2)
        {
            MatchDate = matchDate;
            TournamentName = tournamentName ?? throw new ArgumentNullException(nameof(tournamentName));
            Format = format ?? throw new ArgumentNullException(nameof(format));
            Team1 = team1 ?? throw new ArgumentNullException(nameof(team1));
            Team2 = team2 ?? throw new ArgumentNullException(nameof(team2));
            ScoreTeam1 = scoreTeam1;
            ScoreTeam2 = scoreTeam2;
        }

        /// <summary>
        /// Повертає інформацію про матч у зручному для читання форматі.
        /// </summary>
        public override string ToString()
        {
            return $"[{MatchDate:dd.MM.yyyy}] {TournamentName} ({Format}): {Team1.Tag} {ScoreTeam1} : {ScoreTeam2} {Team2.Tag}";
        }
    }
}
