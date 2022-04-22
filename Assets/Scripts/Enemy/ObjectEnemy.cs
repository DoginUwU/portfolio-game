using UnityEngine;
using MyBox;
using System;
using UnityEngine.Events;

public enum AttackMode
{
    AWAYS_ATTACK,
    TIMER_ATTACK,
    EVENT_ATTACK
}

public class ObjectEnemy : MonoBehaviour
{
    [Header("Settings")]
    public AttackMode attackMode;
    [ConditionalField(nameof(attackMode), false, AttackMode.TIMER_ATTACK)]
    public TimerSettings timerSettings;
    [ConditionalField(nameof(attackMode), false, AttackMode.EVENT_ATTACK)]
    public EventAttackSettings eventAttackSettings;

#nullable enable
    private PlayerBehaviour? player;

    private void Update()
    {
        Attack();
        TimerAttack();
    }

    private void TimerAttack()
    {
        if (attackMode != AttackMode.TIMER_ATTACK) return;

        if (!timerSettings.attacking)
            timerSettings._timerToAttack += Time.deltaTime;
        else
        {
            timerSettings._timerToDecay += Time.deltaTime;
        }

        if (timerSettings._timerToAttack > timerSettings.timerToAttack)
        {
            timerSettings.attacking = true;
            timerSettings._timerToAttack = 0;
            timerSettings.eventOnAttack.Invoke();
        }

        if (timerSettings._timerToDecay > timerSettings.timerToDecay)
        {
            timerSettings.attacking = false;
            timerSettings._timerToAttack = 0;
            timerSettings._timerToDecay = 0;
            timerSettings.eventOnDecay.Invoke();
        }
    }

    private void Attack()
    {
        if (player == null) return;
        if (attackMode == AttackMode.TIMER_ATTACK && !timerSettings.attacking) return;
        if (attackMode == AttackMode.EVENT_ATTACK && !eventAttackSettings.isAttacking) return;

        player.Damage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerBehaviour>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }
}

[Serializable]
public struct TimerSettings
{
    public float timerToAttack;
    public float timerToDecay;

    [Space()]
    public UnityEvent eventOnAttack;
    public UnityEvent eventOnDecay;

    [HideInInspector]
    public float _timerToAttack;
    [HideInInspector]
    public float _timerToDecay;
    [HideInInspector]
    public bool attacking;
}

[Serializable]
public struct EventAttackSettings
{
    [HideInInspector]
    public bool isAttacking;

    public void Attack()
    {
        isAttacking = true;
    }

    public void Decay()
    {
        isAttacking = false;
    }
}