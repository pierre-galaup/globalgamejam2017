using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingAi : MonoBehaviour
{
    [Flags]
    private enum AiState
    {
        Idle,
        Wander,
        Moving,
        Listen
    }

    public float WanderTime = 3f;
    public float WanderRadius = 5f;
    public Transform Target;

    private AiState state;
    private float timer;

    /// <summary>
    /// Contains all actions for states
    /// </summary>
    private readonly Dictionary<AiState, Action> stateActions = new Dictionary<AiState, Action>();

	// Use this for initialization
	void Start()
    {
        this.state = AiState.Wander;
        this.stateActions[AiState.Idle] = this.IdleAction;
        this.stateActions[AiState.Wander] = this.WanderAction;
        this.stateActions[AiState.Listen] = this.ListenAction;
        this.stateActions[AiState.Moving] = this.MovingAction;
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current state " + this.state);
        this.stateActions[this.state]();
	}

    private void MovingAction()
    {
        if (this.transform.position == this.Target.position) // no more move
        {
            this.state = AiState.Idle;
            this.timer = 0;
        }
    }

    private void IdleAction()
    {
        this.timer += Time.deltaTime;
        if (this.timer < this.WanderTime) // Still have to wait
            return;
        this.state = AiState.Wander; // wait is over, let's search for a new target
    }

    private void WanderAction()
    {
        var newPos = this.RandomNavSphere(transform.position, this.WanderRadius, -1);
        this.Target.position = newPos;
        Debug.Log("my new target " + this.Target.position.ToString());
        this.state = AiState.Moving;
    }

    private void ListenAction()
    {
        this.Target.position = this.transform.position;
    }

    public void StartListening()
    {
        this.state = AiState.Listen;
    }

    public void StopListening()
    {
        this.state = AiState.Idle;
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        var randDirection = UnityEngine.Random.insideUnitSphere * dist + origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
