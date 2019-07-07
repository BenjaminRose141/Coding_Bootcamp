using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is meant to enable free placement of the cubes.

public class PlatformMoving : MonoBehaviour 
{
	Vector3 positionStart;
	Vector3 positionEnd;
    [SerializeField] Transform transformStart;
    [SerializeField] Transform transformEnd;
	[SerializeField] float moveSpeed;
    [SerializeField] GameObject mesh;
	private Vector3 directionStartToEnd;
	bool movingStartToEnd = true;
	private bool endIsRight;
	private bool endIsUp;
	private bool useX;

	void Start()
	{
        positionStart = transformStart.position;
        positionEnd = transformEnd.position;
		mesh.transform.position = positionStart;
		directionStartToEnd = CalculateDirectionVector3(positionStart,positionEnd);
		Debug.Log("Direction Vector Start to End ist: " + directionStartToEnd);
		CreateBounds();
	}

	void Update()
	{
		Move();
	}

	//This Method handles the Movement of the Cubes. It checks where the object is currently moving, then checks the Bounds and then reacts accordingly (by either continuing to move or inverting move direction)
	void Move()
	{
		Vector3 positionCurrent = mesh.transform.position;
		
		if(movingStartToEnd)
		{
			if(CheckBoundsEnd())
			{
				InvertMoveDirection();		
			}
			else
			{
				mesh.transform.position += directionStartToEnd * moveSpeed * Time.deltaTime;
			}
		}
		else
		{
			if(CheckBoundsStart())
			{
				InvertMoveDirection();
			}
			else
			{
				mesh.transform.position -= directionStartToEnd * moveSpeed * Time.deltaTime;
			}
		}
	}


	//Method to invert the current movement Direction
	void InvertMoveDirection()
	{
		movingStartToEnd = !movingStartToEnd;
		//Debug.Log("Inverted Move Direction");
	}

	//Reusable Method to Calculate the Direction Vector3 of two points (from Position One to Position Two)
	Vector3 CalculateDirectionVector3(Vector3 positionOne, Vector3 positionTwo)
	{
		Vector3 direction = positionTwo - positionOne;
		return direction;
	}

	//Creates Bounds for the Cube movement. It checks, if the End Point is left or right of the start point, or above or below it if they have the same x value
	void CreateBounds()
	{
		useX = true;

		if(directionStartToEnd.x > 0)
		{
			endIsRight = true;
		}
		else if(directionStartToEnd.x < 0)
		{
			endIsRight = false;
		}
		else if(directionStartToEnd.x == 0)
		{
			useX = false;

			if(directionStartToEnd.y < 0)
			{
				endIsUp = false;
			}
			else if(directionStartToEnd.y > 0)
			{
				endIsUp = true;
			}
			else
			{
				Debug.Log("Position Start and End are the same! (Excluding Z)");
			}

		}
	}

	//Checks the Bounds for the End Point. returns true if position is out of bounds
	bool CheckBoundsEnd()
	{
		if(useX)
		{
			if(endIsRight && mesh.transform.position.x < positionEnd.x)
			{
				return false;
			}
			else if(endIsRight == false && mesh.transform.position.x > positionEnd.x)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else
		{
			if(endIsUp && mesh.transform.position.y < positionEnd.y)
			{
				return false;
			}
			else if(endIsUp == false && mesh.transform.position.y > positionEnd.y)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}

	//Checks the Bounds for the Start Point. returns true if position is out of bounds
	bool CheckBoundsStart()
	{
		if(useX)
		{
			if(endIsRight && mesh.transform.position.x > positionStart.x)
			{
				return false;
			}
			else if(endIsRight == false && mesh.transform.position.x < positionStart.x)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else
		{
			if(endIsUp && mesh.transform.position.y > positionStart.y)
			{
				return false;
			}
			else if(endIsUp == false && mesh.transform.position.y < positionStart.y)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
