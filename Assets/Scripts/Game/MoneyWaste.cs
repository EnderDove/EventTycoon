using UnityEngine;

public class MoneyWaste : MonoBehaviour
{
    public string Name = "";
    public int Cost;
    public bool IsOn { get; private set; }

    public void Toogle()
    {
        IsOn = !IsOn;
    }
}
