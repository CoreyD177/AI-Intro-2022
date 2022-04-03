using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform player;

    //An array of game objects
    //public Transform[] waypoints;
    public List<Transform> waypoints;
    public int waypointIndex = 0;
    public GameObject waypointPrefab;
    
    public float speed = 1.5f;
    public float minGoalDistance = 0.01f;
    public float chaseDistance = 3f;
    //Variable to determine whether the closest waypoint distance calculation has already been completed
    public bool runCount = false;

    private void Start()
    {
        NewWaypoint();
        NewWaypoint();
        NewWaypoint();
        NewWaypoint();
        NewWaypoint();
        NewWaypoint();
        NewWaypoint();
        NewWaypoint();
    }

    public void NewWaypoint()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);

        GameObject newPoint = Instantiate(waypointPrefab, new Vector2(x,y), Quaternion.identity);

        waypoints.Add(newPoint.transform);
    }

    // Update is called once per frame
    /*void Update()
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
                LowestDistance();
                runCount = true
            }
            //move towards our waypoints
            WaypointUpdate();
            AIMoveTowards(waypoints[waypointIndex]); //the number is called the index
        }
    }*/

    public void LowestDistance()
    {
        float lowestDistance = float.PositiveInfinity;
        int lowestIndex = 0;
        float distance;

        for (int i = 0; i < waypoints.Count; i++)
        {
            distance = Vector2.Distance(player.position, waypoints[i].position);
            if (distance < lowestDistance)
            {
                lowestDistance = distance;
                lowestIndex = i;
            }
        }

        waypointIndex = lowestIndex;
    }

    public void WaypointUpdate()
    {
        Vector2 AiPosition = transform.position;
        //if we are not near the goal, move towards it.
        if (Vector2.Distance(AiPosition, waypoints[waypointIndex].position) < minGoalDistance)
        {
            waypointIndex++;

            if (waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }
        }                
    }

    public void AIMoveTowards(Transform goal)
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
