using UnityEngine;
using System.Collections.Generic;

public class ElevatorManager : MonoBehaviour
{
    public Elevator[] elevators;

    private Queue<int> pendingRequests = new Queue<int>();

    public void RequestElevator(int floor)
    {
        pendingRequests.Enqueue(floor);
    }

    void Update()
    {
        if (pendingRequests.Count == 0)
            return;

        foreach (Elevator e in elevators)
        {
            if (e.IsIdle())
            {
                int floor = pendingRequests.Dequeue();
                e.MoveToFloor(floor);
                return;
            }
        }
    }
}