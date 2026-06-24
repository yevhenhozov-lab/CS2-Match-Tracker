using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CS2MatchTracker.Core.Entities
{
    /// <summary>
    /// Керує колекціями кіберспортивних команд та матчів, а також забезпечує збереження даних.
    /// </summary>
    public class MatchManager
    {
        private readonly string _teamsFilePath = "teams.json";
        private readonly string _matchesFilePath = "matches.json";

        /// <summary>
        /// Списокіснуючих команд.
        /// </summary>
        public List<Team> Teams { get; private set; } = new List<Team>();

        /// <summary>
        /// Список проведених матчів.
        /// </summary>
        public List<Match> Matches { get; private set; } = new List<Match>();

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="MatchManager"/> та завантажує дані.
        /// </summary>
        public MatchManager()
        {
            LoadData();
        }

        /// <summary>
        /// Додає нову команду до бази даних.
        /// </summary>
        public void AddTeam(Team team)
        {
            if (team == null) return;
            Teams.Add(team);
            SaveData();
        }

        /// <summary>
        /// Видаляє команду з бази даних за її тегом.
        /// </summary>
        public bool RemoveTeam(string tag)
        {
            var team = Teams.FirstOrDefault(t => t.Tag.Equals(tag, StringComparison.OrdinalIgnoreCase));
            if (team != null)
            {
                Teams.Remove(team);
                // Також видаляємо матчі з цією командою, щоб зберегти цілісність даних
                Matches.RemoveAll(m => m.Team1.Tag.Equals(tag, StringComparison.OrdinalIgnoreCase) || 
                                       m.Team2.Tag.Equals(tag, StringComparison.OrdinalIgnoreCase));
                SaveData();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Додає новий матч до історії.
        /// </summary>
        public void AddMatch(Match match)
        {
            if (match == null) return;
            Matches.Add(match);
            SaveData();
        }

        /// <summary>
        /// Зберігає всі колекції у файли JSON.
        /// </summary>
        public void SaveData()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(_teamsFilePath, JsonSerializer.Serialize(Teams, options));
                File.WriteAllText(_matchesFilePath, JsonSerializer.Serialize(Matches, options));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Помилка збереження даних]: {ex.Message}");
            }
        }

        /// <summary>
        /// Автоматично завантажує дані з файлів JSON при старті.
        /// </summary>
        public void LoadData()
        {
            try
            {
                if (File.Exists(_teamsFilePath))
                {
                    string json = File.ReadAllText(_teamsFilePath);
                    Teams = JsonSerializer.Deserialize<List<Team>>(json) ?? new List<Team>();
                }

                if (File.Exists(_matchesFilePath))
                {
                    string json = File.ReadAllText(_matchesFilePath);
                    Matches = JsonSerializer.Deserialize<List<Match>>(json) ?? new List<Match>();
                }
            }
            catch
            {
                // Якщо файли пошкоджені, створюємо нові порожні списки для стабільності програми
                Teams = new List<Team>();
                Matches = new List<Match>();
            }
        }
    }
}
