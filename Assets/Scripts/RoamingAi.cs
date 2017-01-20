using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class RoamingAi : MonoBehaviour
{
    public float WanderTime = 3f;
    public float WanderRadius = 5f;
    public GameObject target;

    private float timer;

	// Use this for initialization
	void Start ()
    {
        this.timer = this.WanderTime;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.timer += Time.deltaTime;
        if (this.timer < this.WanderTime)
            return;
        var newPos = this.RandomNavSphere(transform.position, this.WanderRadius, -1);
        target.transform.position = newPos;
        timer = 0;
	}

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        var randDirection = Random.insideUnitSphere * dist + origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
