using UnityEngine;


public class FloorButton : MonoBehaviour
{
    public int floorNumber;
    public ElevatorManager manager;

    void OnMouseDown()
    {
        manager.RequestElevator(floorNumber);
    }
}
