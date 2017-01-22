using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithPlayer : MonoBehaviour
{

	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag != "Player")
			Physics.IgnoreCollision (other.collider, this.GetComponent<Collider>());
	}
}
