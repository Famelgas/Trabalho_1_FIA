using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class CarDetectorScript : MonoBehaviour
{

	public float angle = 360;
	public bool ApplyThresholds, ApplyLimits;
	public float MinX, MaxX, MinY, MaxY;
	private bool useAngle = true;

	public float output;
	public int numObjects;

	void Start()
	{
		output = 0;
		numObjects = 0;

		if (angle > 360)
		{
			useAngle = false;
		}
	}

	void Update()
	{

		GameObject[] cars = null;
		GameObject closestBlock = null;

		output = 0;

		if (useAngle)
		{
			cars = GetVisibleCars();
		}
		else
		{
			cars = GetAllCars();
		}

		foreach (GameObject car in cars)
		{
			//print (1 / (transform.position - light.transform.position).sqrMagnitude);
			output += 1.0f / ((transform.position - car.transform.position).sqrMagnitude);
			//Debug.DrawLine (transform.position, light.transform.position, Color.red);
		}

	}

	public virtual float GetOutput() { throw new NotImplementedException(); }

	// Returns all "Light" tagged objects. The sensor angle is not taken into account.
	GameObject[] GetAllCars()
	{
		return GameObject.FindGameObjectsWithTag("CarToFollow");
	}

	GameObject[] GetVisibleCars()
	{
		ArrayList visibleCars = new ArrayList();
		float halfAngle = angle / 2.0f;

		GameObject[] cars = GameObject.FindGameObjectsWithTag("CarToFollow");

		foreach (GameObject car in cars)
		{
			Vector3 toVector = (car.transform.position - transform.position);
			Vector3 forward = transform.forward;
			toVector.y = 0;
			forward.y = 0;
			float angleToTarget = Vector3.Angle(forward, toVector);

			if (angleToTarget <= halfAngle)
			{
				visibleCars.Add(car);
			}
		}

		return (GameObject[])visibleCars.ToArray(typeof(GameObject));
	}


}