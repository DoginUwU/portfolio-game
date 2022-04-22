using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(PlayerHudController))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Damage")]
    public int life = 3;
    public float invisibleTime = 1f;

    [HideInInspector]
    public bool isInvinsible;

    private PlayerHudController hudController;
    private PlayerController playerController;
    private PlayerAnimationController playerAnimationController;
    private Rigidbody2D rb;
    private float _invisibleTime;

    private void Start()
    {
        hudController = GetComponent<PlayerHudController>();
        playerController = GetComponent<PlayerController>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        InvisibleUpdate();
    }

    private void InvisibleUpdate()
    {
        if (isInvinsible)
            _invisibleTime += Time.deltaTime;

        if (_invisibleTime > invisibleTime)
        {
            _invisibleTime = 0;
            isInvinsible = false;
        }

    }

    public void Damage()
    {
        if (isInvinsible) return;

        life--;
        hudController.UpdateHearts();
        isInvinsible = true;

        if (life <= 0) Death();
    }

    public void Heal()
    {
        if (life >= 3) return;

        life++;
        hudController.UpdateHearts();
    }

    public void Death()
    {
        LevelManager.Instance.FailLevel();
        rb.simulated = false;
        playerAnimationController.spriteRenderer.enabled = false;
    }

    public void DisablePlayer()
    {
        this.enabled = false;
        playerController.enabled = false;
        playerAnimationController.enabled = false;
    }
}
