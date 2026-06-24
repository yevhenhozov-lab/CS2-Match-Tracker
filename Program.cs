using System;
using System.Linq;
using CS2MatchTracker.Core.Entities;

namespace CS2MatchTracker
{
    /// <summary>
    /// Головний клас програми, що реалізує консольний інтерфейс користувача.
    /// </summary>
    class Program
    {
        private static readonly MatchManager Manager = new MatchManager();

        /// <summary>
        /// Головна точка входу в програму.
        /// </summary>
        static void Main(string[] args)
        {
            // Встановлюємо кодування UTF-8 для коректного відображення української мови в консолі
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("   БАЗА РЕЗУЛЬТАТІВ МАТЧІВ CS2 (Курсова робота)   ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Переглянути всі команди");
                Console.WriteLine("2. Додати нову команду");
                Console.WriteLine("3. Видалити команду (потребує підтвердження)");
                Console.WriteLine("4. Переглянути всі матчі");
                Console.WriteLine("5. Додати результат матчу");
                Console.WriteLine("6. Пошук матчів за назвою команди");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("==================================================");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowTeams(); break;
                    case "2": AddTeam(); break;
                    case "3": RemoveTeam(); break;
                    case "4": ShowMatches(); break;
                    case "5": AddMatch(); break;
                    case "6": SearchMatches(); break;
                    case "0":
                        Console.WriteLine("\nДякуємо за використання програми! Дані збережено.");
                        return;
                    default:
                        Console.WriteLine("\nНекоректний вибір. Натисніть будь-яку клавішу для повтору...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowTeams()
        {
            Console.Clear();
            Console.WriteLine("--- СПИСОК КОМАНД ---");
            if (!Manager.Teams.Any())
            {
                Console.WriteLine("База даних команд порожня.");
            }
            else
            {
                foreach (var team in Manager.Teams)
                {
                    Console.WriteLine(team.ToString());
                }
            }
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
            Console.ReadKey();
        }

        private static void AddTeam()
        {
            Console.Clear();
            Console.WriteLine("--- ДОДАННЯ НОВОЇ КОМАНДИ ---");
            
            Console.Write("Введіть назву команди (наприклад, Natus Vincere): ");
            string name = Console.ReadLine();
            
            Console.Write("Введіть тег команди (наприклад, NAVI): ");
            string tag = Console.ReadLine();
            
            Console.Write("Введіть країну: ");
            string country = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(tag) || string.IsNullOrWhiteSpace(country))
            {
                Console.WriteLine("\nПомилка: всі поля повинні бути заповнені! Команду не додано.");
                Console.ReadKey();
                return;
            }

            Console.Write("Введіть рейтинг команди (очки): ");
            if (!int.TryParse(Console.ReadLine(), out int points) || points < 0)
            {
                Console.WriteLine("\nПомилка: рейтинг має бути додатним цілим числом! Команду не додано.");
                Console.ReadKey();
                return;
            }

            Manager.AddTeam(new Team(name.Trim(), tag.Trim().ToUpper(), country.Trim(), points));
            Console.WriteLine("\nКоманду успішно додано та збережено!");
            Console.ReadKey();
        }

        private static void RemoveTeam()
        {
            Console.Clear();
            Console.WriteLine("--- ВИДАЛЕННЯ КОМАНДИ ---");
            Console.Write("Введіть тег команди для видалення: ");
            string tag = Console.ReadLine()?.Trim().ToUpper();

            var team = Manager.Teams.FirstOrDefault(t => t.Tag.Equals(tag, StringComparison.OrdinalIgnoreCase));
            if (team == null)
            {
                Console.WriteLine("\nКоманду з таким тегом не знайдено.");
                Console.ReadKey();
                return;
            }

            // Запит на підтвердження видалення (вимога безпеки даних у методичці)
            Console.Write($"Ви впевнені, що хочете видалити команду {team.Name} та всі матчі з її участю? (y/n): ");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm == "y" || confirm == "yes")
            {
                Manager.RemoveTeam(tag);
                Console.WriteLine("\nКоманду та пов'язані матчі успішно видалено!");
            }
            else
            {
                Console.WriteLine("\nВидалення скасовано користувачем.");
            }
            Console.ReadKey();
        }

        private static void ShowMatches()
        {
            Console.Clear();
            Console.WriteLine("--- ІСТОРІЯ МАТЧІВ ---");
            if (!Manager.Matches.Any())
            {
                Console.WriteLine("Жодного матчу ще не зареєстровано.");
            }
            else
            {
                foreach (var match in Manager.Matches)
                {
                    Console.WriteLine(match.ToString());
                }
            }
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        private static void AddMatch()
        {
            Console.Clear();
            Console.WriteLine("--- ДОДАННЯ РЕЗУЛЬТАТУ МАТЧУ ---");
            
            if (Manager.Teams.Count < 2)
            {
                Console.WriteLine("Помилка: Для створення матчу в базі має бути щонайменше 2 команди!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введіть назву турніру: ");
            string tournament = Console.ReadLine();

            Console.Write("Введіть формат (Bo1 / Bo3 / Bo5): ");
            string format = Console.ReadLine()?.Trim().ToUpper();

            Console.Write("Введіть тег ПЕРШОЇ команди: ");
            string tag1 = Console.ReadLine()?.Trim().ToUpper();
            var team1 = Manager.Teams.FirstOrDefault(t => t.Tag == tag1);

            Console.Write("Введіть тег ДРУГОЇ команди: ");
            string tag2 = Console.ReadLine()?.Trim().ToUpper();
            var team2 = Manager.Teams.FirstOrDefault(t => t.Tag == tag2);

            if (team1 == null || team2 == null || tag1 == tag2)
            {
                Console.WriteLine("\nПомилка: Некоректні теги команд або обрано однакові команди!");
                Console.ReadKey();
                return;
            }

            Console.Write($"Кількість карт, яку виграли {team1.Tag}: ");
            if (!int.TryParse(Console.ReadLine(), out int score1) || score1 < 0) return;

            Console.Write($"Кількість карт, яку виграли {team2.Tag}: ");
            if (!int.TryParse(Console.ReadLine(), out int score2) || score2 < 0) return;

            Manager.AddMatch(new Match(DateTime.Now, tournament, format, team1, team2, score1, score2));
            Console.WriteLine("\nМатч успішно додано до бази даних!");
            Console.ReadKey();
        }

        private static void SearchMatches()
        {
            Console.Clear();
            Console.WriteLine("--- ПОШУК МАТЧІВ ЗА КОМАНДОЮ ---");
            Console.Write("Введіть назву або тег команди для пошуку: ");
            string query = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(query)) return;

            var results = Manager.Matches.Where(m => 
                m.Team1.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                m.Team1.Tag.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                m.Team2.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                m.Team2.Tag.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            Console.WriteLine("\n--- Результати пошуку ---");
            if (!results.Any())
            {
                Console.WriteLine("Матчів за вашим запитом не знайдено.");
            }
            else
            {
                foreach (var match in results)
                {
                    Console.WriteLine(match.ToString());
                }
            }
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}
