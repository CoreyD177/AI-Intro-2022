using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform player;

    //An array of game objects
    public Transform[] waypoints;
    public int waypointIndex = 0;
    
    public float speed = 1.5f;
    public float minGoalDistance = 0.01f;
    public float chaseDistance = 3f;
    

    // Update is called once per frame
    void Update()
    {
        //are we within the player chase distance
        if(Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            //move towards player
            AIMoveTowards(player);
        }
        else
        {
            //move towards our waypoints
            WaypointUpdate();
            AIMoveTowards(waypoints[waypointIndex]); //the number is called the index
        }
    }

    private void WaypointUpdate()
    {
        Vector2 AiPosition = transform.position;

        //if we are not near the goal, move towards it.
        if (Vector2.Distance(AiPosition, waypoints[waypointIndex].position) < minGoalDistance)
        {
            waypointIndex++;

            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }

    private void AIMoveTowards(Transform goal)
    {
        Vector2 AiPosition = transform.position;

        
        //if we are not near the goal, move towards it.
        if(Vector2.Distance(AiPosition, goal.position) > minGoalDistance)
        {
            //direction from A to B
            // is B - A
            //Method 3
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }        
        else
        {
            transform.position = goal.position;
        }
    }
}
