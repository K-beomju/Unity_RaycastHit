using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
      float rightMax = 4.0f;
    float leftMax = -4.0f;
    float currentPosition;
    float currentPositions;
    float direction = 50.0f;



    void Start()
    {
        currentPosition = transform.position.y;
          currentPositions = transform.position.x;
    }

    void Update()
    {
           transform.position = Vector3.Lerp(transform.position, transform.position, Time.deltaTime * direction);




        currentPosition += Time.deltaTime * direction;
         currentPositions += Time.deltaTime * direction;
        if (currentPosition >= rightMax)
        {
            direction *= -1;
            currentPosition = rightMax;
            currentPositions = rightMax;
        }
        else if (currentPosition <= leftMax)
        {
            direction *= -1;
            currentPosition = leftMax;
               currentPositions = leftMax;
        }
        transform.position = new Vector3(currentPositions, currentPosition, 3);
    }




}




