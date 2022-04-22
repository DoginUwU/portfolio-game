using UnityEngine;
using UnityEngine.UI;

public class PlayerHudController : MonoBehaviour
{
    [Header("Life")]
    public GameObject heartImage;
    public GameObject heartPanel;

    private PlayerBehaviour playerBehaviour;

    private void Start()
    {
        playerBehaviour = GetComponent<PlayerBehaviour>();
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        if (!playerBehaviour) return;

        for (int i = 0; i < heartPanel.transform.childCount; i++)
        {
            Destroy(heartPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < playerBehaviour.life; i++)
        {
            Instantiate(heartImage, heartPanel.transform);
        }
    }
}
