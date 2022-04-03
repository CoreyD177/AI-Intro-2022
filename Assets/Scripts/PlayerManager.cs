using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    private AIManager _aiManager;
    [SerializeField] protected CanvasGroup _buttonGroup;
    private bool _isHealOverTimeRunning = false;
    protected override void Start()
    {
        base.Start();
        _aiManager = GetComponent<AIManager>();
        if (_aiManager == null)
        {
            Debug.LogError("AI Manager Not Found");
        }
    }
    public override void TakeTurn()
    {
       
        _buttonGroup.interactable = true;
    }
    public void EndTurn()
    {
        _buttonGroup.interactable=false;
        _aiManager.TakeTurn();
    }
    public void EatBerries()
    {
        StartCoroutine(HealOverTime(3, 1f));
        EndTurn();
    }    
    IEnumerator HealOverTime(int times, float waitTime)
    {        
        if (_isHealOverTimeRunning == false)
        {
            _isHealOverTimeRunning = true;
            for (int i = 0; i < times; i++)
            {
                Heal(10f);
                yield return new WaitForSeconds(waitTime);
            }
        }
        _isHealOverTimeRunning = false;
        yield return null;
    }
    public void SelfDestruct()
    {
        DealDamage(_maxHealth);
        _aiManager.DealDamage(80f);
        EndTurn();
    }
    public void FlameWheel()
    {        
        _aiManager.DealDamage(50f);
        EndTurn();
    }
    public void VineWhip()
    {        
        _aiManager.DealDamage(30f);
        EndTurn();
    }
}
