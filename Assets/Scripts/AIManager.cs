using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : BaseManager
{
    public enum State
    {
        FullHP,
        LowHP,
        Dead,
    }
    public State currentState;
    protected PlayerManager _playerManager;
    [SerializeField] protected Animator _anim;

    protected override void Start()
    {
        base.Start();
        _playerManager = GetComponent<PlayerManager>();
    }
    public override void TakeTurn()
    {
        if (_health <= 0f)
        {
            currentState = State.Dead;
        }
        switch (currentState)
        {
            case State.FullHP:
                if (_health > 0)
                {
                    FullHPState();
                }
                break;
                case State.LowHP:
                if (_health > 0)
                {
                    LowHPState();
                }
                break;
            case State.Dead:
                DeadState();
                break;
        }
        StartCoroutine(EndTurn());
    }
    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(2f);
        _playerManager.TakeTurn();
    }
    void LowHPState()
    {
        int randomAttack = Random.Range(0, 10);
        if (_health > 60)
        {
            currentState = State.FullHP;
            FullHPState();
            return;
        }
        switch (randomAttack)
        {
            case int i when i > 0 && i <= 1:
                SelfDestruct();
                break;
            case int i when i > 1 && i <= 7:
                EatBerries();
                break;
            case int i when i > 7 && i <= 9:
                FlameWheel();
                break;

        }
    }
    void DeadState()
    {
        Debug.Log("The decepticons have won");
    }
    void FullHPState()
    {
        int randomAttack = Random.Range(0, 10);
        if (_health < 40)
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }
        switch (randomAttack)
        {
            case int i when i > 0 && i <=2:
                FlameWheel();
                break;
                case int i when i > 2 && i <= 8:
                VineWhip();
                break;
                case int i when i > 8 && i <= 9:
                SelfDestruct();
                break;

        }
    }

    public void EatBerries()
    {
        Debug.LogWarning("AI Ate Berries");
        Heal(20f);
    }
    public void SelfDestruct()
    {
        Debug.LogWarning("AI Self Destructed");
        DealDamage(_maxHealth);
        _playerManager.DealDamage(80f);
        currentState = State.Dead;
        DeadState();
    }
    public void FlameWheel()
    {
        Debug.LogWarning("AI Used Flame Wheel");
        _playerManager.DealDamage(50f);
    }
    public void VineWhip()
    {
        _anim.SetTrigger("VineWhip");
        Debug.LogWarning("AI Used Vine Whip");
        _playerManager.DealDamage(30f);
    }
}
