using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    private NavMeshAgent navAgent;

	// Use this for initialization
	void Start ()
    {
        this.navAgent = this.GetComponent<NavMeshAgent>();
	}
	
	public void StopNavigation()
    {
        this.navAgent.Stop();
    }

    public void ResumeNavigation()
    {
        this.navAgent.Resume();
    }
}
