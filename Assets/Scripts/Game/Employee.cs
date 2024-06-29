using UnityEngine;

[CreateAssetMenu(fileName = "Employee", menuName = "Employee", order = 0)]
public class Employee : ScriptableObject
{
    public int EmployeeID;

    public string Name;
    public Sprite ProfilePicture;
    public Sprite SpriteLD;
    public Sprite SpriteRD;
    public Sprite SpriteLU;
    public Sprite SpriteRU;
    public float BasicDisignSkills;
    public float BasicSpeed;
    public float BasicCommunicationSkills;
    public float Cost;

    public static WorkerData ToWorkerData(Employee employee)
    {
        return new WorkerData() { EmployeeIDRef = employee.EmployeeID, CommunicationSkills = employee.BasicCommunicationSkills, DisignSkills = employee.BasicDisignSkills, Speed = employee.BasicSpeed };
    }
}
