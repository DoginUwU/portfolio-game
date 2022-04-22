using UnityEngine;

[RequireComponent(typeof(ObjectEnemy))]
public class FireboxEnemy : MonoBehaviour
{
    [Header("Settings")]
    public GameObject fire;

    private ObjectEnemy objectEnemy;

    private void Start()
    {
        objectEnemy = GetComponent<ObjectEnemy>();
        OnDecayEvent();
    }

    public void OnAttackEvent()
    {
        fire.SetActive(true);
        objectEnemy.eventAttackSettings.Attack();
    }

    public void OnDecayEvent()
    {
        fire.SetActive(false);
        objectEnemy.eventAttackSettings.Decay();
    }
}
