using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Attack,
        Defence,
        RunAway,
        BerryPicking,
    }

    public State currentState;
    public AIMovement aiMovement;

    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        NextState();
    }

    private void NextState()
    {
        //Runs one of the cases that matches the value (In this example the value is currentState)
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
        }
    }

    //Coroutine is a special method that can be paused and returned to later
    private IEnumerator AttackState()
    {
        Debug.Log(message: "Attack:Enter");
        while (currentState == State.Attack)
        {
            aiMovement.AIMoveTowards(aiMovement.player);
            if (Vector2.Distance(transform.position, aiMovement.player.position) > aiMovement.chaseDistance)
            {
                currentState = State.BerryPicking;
            }
            
            yield return null;
        }
        Debug.Log(message: "Attack:Exit");
        NextState();
    }
    
    private IEnumerator DefenceState()
    {
        Debug.Log(message: "Defence:Enter");
        while (currentState == State.Defence)
        {
            Debug.Log(message: "Defending");
            yield return null;
        }
        Debug.Log(message: "Defence:Exit");
        NextState();
    }
    
    private IEnumerator RunAwayState()
    {
        Debug.Log(message: "Run Away:Enter");
        while (currentState == State.RunAway)
        {
            Debug.Log(message: "Running Away");
            yield return null;
        }
        Debug.Log(message: "Run Away:Exit");
        NextState();
    }

    private IEnumerator BerryPickingState()
    {
        Debug.Log(message: "Berry Picking:Enter");
        aiMovement.LowestDistance();


        while (currentState == State.BerryPicking)
        {
            aiMovement.WaypointUpdate();
            aiMovement.AIMoveTowards(aiMovement.waypoints[aiMovement.waypointIndex]);

            yield return null;
            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.chaseDistance)
            {
                currentState = State.Attack;
            }
        }
        Debug.Log(message: "Berry Picking:Exit");
        NextState();
    }
}
