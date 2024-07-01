using UnityEngine;

public class MoneyGainer : MonoBehaviour
{
    private int day = 0;
    private float finalMultiplayer;
    public GameManager gameManager;

    public int GainMoney()
    {
        if (day == 10)
            gameManager.EndGainingMoney(this);
        int money;
        if (day < 5)
            money = Mathf.FloorToInt(finalMultiplayer * (day - 5) * (day - 5) * 500);
        else
            money = Mathf.FloorToInt(finalMultiplayer * (day - 4) * (day - 4) * 500);
        day += 1;
        return money;
    }

    public void StartGaining(GameEvent gameEvent)
    {
        finalMultiplayer = gameEvent.FinalMultiplayer;
    }
}