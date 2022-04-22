using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    public float minDistanceToSee = 10;

    [Header("Attack")]
    public float distanceToAttack;
    public float delayToAttack;

    [Space()]
    public Animator animator;

    private GameObject player;
    private Vector2 startingPoint;
    private Vector2 target;
    private Vector2 attackPoint;
    private float _delayAttack;
    private bool _isAttacking;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPoint = transform.position;
    }

    private void Update()
    {

        SetTarget();
        Chase();
        Flip();
        Attack();
    }

    public void Attack()
    {
        if(_delayAttack > delayToAttack)
        {
            _delayAttack = 0;
            animator.SetTrigger("isAttacking");

            player.GetComponent<PlayerBehaviour>().Damage();
        }
    }

    private void SetTarget()
    {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (playerDistance < minDistanceToSee)
        {
            if (playerDistance > distanceToAttack)
            {
                target = player.transform.position;
                _delayAttack = 0;
                _isAttacking = false;
                return;
            }

            _delayAttack += Time.deltaTime;
            attackPoint = transform.position;
            target = attackPoint;
            _isAttacking = true;

            return;
        }

        target = startingPoint;
        _delayAttack = 0;
        _isAttacking = false;
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void Flip()
    {
        float targetPosition = _isAttacking ? player.transform.position.x : target.x ;

        if (transform.position.x > targetPosition)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
            collision.gameObject.GetComponent<PlayerBehaviour>().Damage();
    }
}
