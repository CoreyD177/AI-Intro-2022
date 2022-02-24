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
    //Variable to determine whether the closest waypoint distance calculation has already been completed
    public bool runCount = false;
    

    // Update is called once per frame
    void Update()
    {
        //are we within the player chase distance
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            //move towards player
            AIMoveTowards(player);
            //Resets waypoint distance calculation status to false after chasing player
            runCount = false;
        }
        else
        {
            //If waypoint distance calculation has not been run already
            if (runCount == false )
            {               
                //if Waypoint 0 is closer than waypoint 1 and waypoint 2
                if (Vector2.Distance(transform.position, waypoints[0].position) < Vector2.Distance(transform.position, waypoints[1].position) && Vector2.Distance(transform.position, waypoints[0].position) < Vector2.Distance(transform.position, waypoints[2].position))
                {
                    waypointIndex = 0; //Set new target destination to waypoint 0
                    runCount = true; //Set status of waypoint distance calculation to true so it won't run again unless another player chase is instigated
                    }
                //If waypoint 1 is the closest
                else if (Vector2.Distance(transform.position, waypoints[1].position) < Vector2.Distance(transform.position, waypoints[0].position) && Vector2.Distance(transform.position, waypoints[1].position) < Vector2.Distance(transform.position, waypoints[2].position))
                {
                    waypointIndex = 1; //set new target destination to waypoint 1
                    runCount = true; //Set status of waypoint distance calculation to true so it won't run again unless another player chase is instigated
                }
                //Otherwise waypoint 2 must be the closest
                else
                {
                    waypointIndex = 2; //Set new target destination to waypoint 2
                    runCount = true; //Set status of waypoint distance calculation to true so it won't run again unless another player chase is instigated
                }                
            }
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
