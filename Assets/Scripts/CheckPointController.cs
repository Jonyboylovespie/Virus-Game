using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public int currentCheckPoint = 1;

    public void ChangeCheckPoint(int number)
    {
        if (currentCheckPoint < number)
        {
            currentCheckPoint = number;
        }
    }
}
