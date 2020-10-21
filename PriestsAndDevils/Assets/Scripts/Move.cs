using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class Move : MonoBehaviour
{
	float speed = 20;

	int status; // 0->not moving, 1->moving to middle, 2->moving to dest
	Vector3 dest;
	Vector3 mid;

	void Update()
	{
		if (status == 1)
		{
			transform.position = Vector3.MoveTowards(transform.position, mid, speed * Time.deltaTime);
			if (transform.position == mid) status = 2;
		}
		if (status == 2)
		{
			transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
			if (transform.position == dest) status = 0;
		}
	}

	public void setDest(Vector3 _dest)
	{
		dest = _dest;
		mid = _dest;
		if (_dest.y == transform.position.y) status = 2; //boat
		else
		{
			if (_dest.y < transform.position.y) mid.y = transform.position.y;   // from coast to boat
			else mid.x = transform.position.x;  // from boat to coast
			status = 1;
		}
	}

	public void reset() { status = 0; }
}
