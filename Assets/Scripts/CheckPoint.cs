using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            CheckPointController parentScript = gameObject.GetComponentInParent<CheckPointController>();
            int checkPointNumber = int.Parse(gameObject.name.Substring(10));
            parentScript.ChangeCheckPoint(checkPointNumber);
        }
    }
}
