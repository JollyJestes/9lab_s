[Serializable]
public class Movie
{
    public string Title { get; set; }
    public int Watched { get; set; }

    public override string ToString()
    {
        return $"{Title} - Смотрел: {(Watched == 1 ? "Да" : "Нет")}";
    }
}