using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public GameObject miniMapPrefab;
    public GameObject miniMap;
    public Texture2D background;
    private bool changingTexture;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMiniMap();
        }
    }

    void ToggleMiniMap()
    {
        if (miniMap == null)
        {
            miniMap = Instantiate(miniMapPrefab, new Vector3(), Quaternion.identity);
            miniMap.transform.GetChild(0).GetComponent<RawImage>().texture = background;
        }
        else
        {
            Destroy(miniMap);
        }
    }
}
