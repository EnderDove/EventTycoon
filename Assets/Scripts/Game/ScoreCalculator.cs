using System.Collections.Generic;

public class ScoreCalculator
{
    public static float[,] TypeThemeTable =
    {
        {1.2f, 1.5f, 1f,   0.8f},
        {1f,   1.2f, 1f,   0.5f},
        {0.8f, 1f,   1.5f, 1.2f},
        {1.2f, 1.5f, 0.9f, 0.5f},
        {1f,   1.2f, 0.8f, 1.5f},
        {1.2f, 1f,   1.5f, 1.2f},
        {1.2f, 1.5f, 0.8f, 1f},
        {1.2f, 1f,   1.5f, 0.9f},
        {0.5f, 1f,   1.5f, 0.5f},
    };

    public readonly static Dictionary<string, int> Themes = new()
    {
        {"Ведроид", 0},
        {"Ипхон", 1},
        {"Аниме", 2},
        {"Робомозг", 3},
        {"Геймфикация", 4},
        {"Другая реальность", 5},
        {"Деньги или творчество", 6},
        {"Инновации", 7},
        {"Карьра", 8},
    };

    public readonly static Dictionary<string, int> Types = new()
    {
        {"Митап", 0},
        {"Конференция", 1},
        {"Фестиваль", 2},
        {"Геймджем", 3},
    };

    public static Dictionary<string, float> LocationTable = new()
    {
        {"Конференц-центр", 2f},
        {"Учебный центр", 1.65f},
        {"Библиотека", 1.5f},
        {"Коворкинг", 1.25f},
        {"Открытые площадки", 1.75f},
    };

    public static float[] MoneyTable = { 0.25f, 0.15f, 0.4f, 0.2f };

    private static float CalculateMoney(bool[] moneySpendOn)
    {
        var sum = 1f;
        for (int i = 0; i < moneySpendOn.Length; i++)
            if (moneySpendOn[i])
                sum += MoneyTable[i];
        return sum;
    }

    public static void Calculate(GameEvent gameEvent)
    {
        var coefficient = 1f;
        coefficient *= TypeThemeTable[Types[gameEvent.Type], Themes[gameEvent.Threme]];
        coefficient *= LocationTable[gameEvent.Location];
        coefficient *= CalculateMoney(gameEvent.MoneySpendedOn);
        coefficient *= UnityEngine.Random.Range(1f, 2f);
        coefficient *= (10 - gameEvent.CurrentMistakes) / 10f;
        gameEvent.FinalMultiplayer = coefficient;
    }
}