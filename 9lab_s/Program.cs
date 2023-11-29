class Program
{
    static void Main(string[] args)
    {
        var films = new List<Movie>();
        var dataSerializer = new DataSerializer<Movie>();

        // Ввод фильмов с клавиатуры
        while (true)
        {
            Console.WriteLine("Введите название фильма (или 'q' для завершения):");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "q")
                break;

            var film = new Movie { Title = userInput, Watched = GetRandomWatchedStatus() };
            films.Add(film);
        }

        // Сериализация в бинарный файл
        dataSerializer.SerializeBinary(films, "movies.dat");

        // Сериализация в JSON файл
        dataSerializer.SerializeJson(films, "movies.json");

        // Оригинальный функционал восстановления из бинарного файла
        var restoredMoviesBinary = dataSerializer.DeserializeBinary("movies.dat");

        // Вывод восстановленных данных из бинарного файла
        Console.WriteLine("\nДанные после восстановления из бинарного файла:");
        PrintMovies(restoredMoviesBinary);

        // Оригинальный функционал восстановления из JSON файла
        var restoredMoviesJson = dataSerializer.DeserializeJson("movies.json");

        // Вывод восстановленных данных из JSON файла
        Console.WriteLine("\nДанные после восстановления из JSON файла:");
        PrintMovies(restoredMoviesJson);

        // Новая функциональность: сортировка и вывод
        dataSerializer.SortAndPrint(films, (movie1, movie2) => movie1.Title.CompareTo(movie2.Title));

        // Новая функциональность: фильтрация и вывод
        var filteredData = dataSerializer.Filter(films, movie => movie.Watched == 1);
        dataSerializer.PrintFilteredData(filteredData);
    }

    static int GetRandomWatchedStatus()
    {
        Random random = new Random();
        return random.Next(0, 2);
    }

    static void PrintMovies(IEnumerable<Movie> movies)
    {
        foreach (var movie in movies)
        {
            Console.WriteLine(movie);
        }
    }
}