using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Elevator : MonoBehaviour
{
    public Transform[] floors;
    public float speed = 2f;

    public int currentFloor = 0;

    public TextMeshProUGUI floorDisplay;
    public SpriteRenderer elevatorSprite;

    private Queue<int> requestQueue = new Queue<int>();
    private int targetFloor = -1;

    private bool isWaiting = false;

    void Update()
    {
        
        if (targetFloor == -1 && requestQueue.Count > 0 && !isWaiting)
        {
            targetFloor = requestQueue.Dequeue();
        }

        if (targetFloor != -1)
        {
            Transform target = floors[targetFloor];

            Vector3 targetPos = new Vector3(transform.position.x, target.position.y,transform.position.z);

            transform.position = Vector3.MoveTowards( transform.position, targetPos, speed * Time.deltaTime);

            
            elevatorSprite.color = Color.yellow;

            if (floorDisplay != null)
            {
                floorDisplay.text = "***";
            }

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                currentFloor = targetFloor;
                targetFloor = -1;

                StartCoroutine(FloorStopDelay());
            }
        }
    }

    IEnumerator FloorStopDelay()
    {
        isWaiting = true;

        
        elevatorSprite.color = Color.white;

        if (floorDisplay != null)
        {
            floorDisplay.text = "F" + currentFloor;
        }

        yield return new WaitForSeconds(1f);

        isWaiting = false;
    }

    public void MoveToFloor(int floor)
    {
        if (!requestQueue.Contains(floor) && floor != targetFloor)
        {
            requestQueue.Enqueue(floor);
        }
    }

    public bool IsIdle()
    {
        return requestQueue.Count == 0 && targetFloor == -1 && !isWaiting;
    }
}