using UnityEngine;

public class MoneyGainer : MonoBehaviour
{
    private int day = 0;
    public float finalMultiplayer;
    public GameManager gameManager;

    public int GainMoney()
    {
        if (day == 10)
        {
            Invoke(nameof(Stop), 0.01f);
            return 0;
        }
        int money;
        if (day < 5)
            money = Mathf.FloorToInt(finalMultiplayer * (day - 5) * (day - 5) * 5);
        else
            money = Mathf.FloorToInt(finalMultiplayer * (day - 4) * (day - 4) * 5);
        day += 1;
        return money;
    }

    private void Stop()
    {
        gameManager.EndGainingMoney(this);
    }

    public void StartGaining(GameEvent gameEvent)
    {
        Debug.Log(gameEvent.Name);
        Debug.Log(gameEvent.FinalMultiplayer);
        finalMultiplayer = gameEvent.FinalMultiplayer;
    }
}