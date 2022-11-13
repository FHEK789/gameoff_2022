using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    Mover mover;
    [SerializeField] float waypointDistanceError = 0.5f;
    [SerializeField] Transform path;
    int actualWaypointIndex = 0;
    private void Awake() {
        mover = GetComponent<Mover>();        
            
    }
    private void Start() {
        GoToWaypoint();
    }
    
    void Update()
    {
        if(IsAtWaypoint())
        GoToWaypoint();
    }

    private bool IsAtWaypoint()
    {
        if(Vector2.Distance(transform.position, path.GetChild(actualWaypointIndex).position) < waypointDistanceError){
            actualWaypointIndex = GetNextWaypointIndex(actualWaypointIndex);
            return true;
        }
        return false;
    }

    private void GoToWaypoint()
    {
        mover.Move(((path.GetChild(actualWaypointIndex).position)-transform.position).normalized);
    }

    private void OnDrawGizmosSelected() {

        //lines in order
        for (int i = 0; i < path.childCount; i++)
        {
            Gizmos.DrawLine(path.GetChild(i).position, path.GetChild(GetNextWaypointIndex(i)).position);
        }
        
        
    }

    private int GetNextWaypointIndex(int index)
    {
        //add 1 and check, if it is in range
        int newIndex = index+1;
        if(newIndex == path.childCount){
            return 0;
        }
        else{
            return newIndex;
        }
    }
}
