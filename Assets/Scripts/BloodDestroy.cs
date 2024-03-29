using System.Collections;
using UnityEngine;

public class BloodDestroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForDone());
    }
    IEnumerator WaitForDone()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
