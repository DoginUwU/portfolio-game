using UnityEngine;

[RequireComponent(typeof(ItemHandler))]
public class CoffeItem : MonoBehaviour
{
    private ItemHandler itemHandler;
    private PlayerBehaviour playerBehaviour;

    private void Start()
    {
        itemHandler = GetComponent<ItemHandler>();
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        itemHandler.onAction.AddListener(Action);
    }

    private void Action()
    {
        playerBehaviour.Heal();
    }
}
