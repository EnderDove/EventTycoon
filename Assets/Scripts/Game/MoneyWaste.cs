using UnityEngine;

public class MoneyWaste : MonoBehaviour
{
    public int Cost;
    public bool IsOn { get; private set; }
    public float Multiplaer = 1f;

    public void Toogle()
    {
        IsOn = !IsOn;
    }
}
