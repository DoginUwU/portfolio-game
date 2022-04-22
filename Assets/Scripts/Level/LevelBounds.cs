using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class LevelBounds : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
            collision.gameObject.GetComponent<PlayerBehaviour>().Death();
    }
}
