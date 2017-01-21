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

    public enum ERoamingType
    {
        Undefined, // Will randomly choose to move or not
        NoRoaming, // Will never move
        RandomRoaming, // Will move
    }

    public float WanderTimeMin = 2f;
    public float WanderTimeMax = 8f;
    
    public float WanderRadius = 3f;
    public Transform Target;
    public ERoamingType RoamingType;
    public float MaxMovingTime = 3f;
    
    // 0.75 is 75% of probability to roam
    public float RoamingProbability = 0.75f;

    private AiState state;
    private float timer;
    private float currentWanderTime;

    /// <summary>
    /// Contains all actions for states
    /// </summary>
    private readonly Dictionary<AiState, Action> stateActions = new Dictionary<AiState, Action>();

	// Use this for initialization
	void Start()
    {
        if (this.RoamingType == ERoamingType.Undefined)
            this.RoamingType = UnityEngine.Random.value >= this.RoamingProbability ? ERoamingType.NoRoaming : ERoamingType.RandomRoaming;
        

        this.state = this.RoamingType == ERoamingType.RandomRoaming ? AiState.Wander : AiState.Idle;
        this.stateActions[AiState.Idle] = this.IdleAction;
        this.stateActions[AiState.Wander] = this.WanderAction;
        this.stateActions[AiState.Listen] = this.ListenAction;
        this.stateActions[AiState.Moving] = this.MovingAction;
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("STATE [" + this.state + "]");
        Debug.Log("ROAMING [" + this.RoamingType + "]");
        if (this.RoamingType != ERoamingType.NoRoaming)
            this.stateActions[this.state]();
	}

    private void MovingAction()
    {
        this.timer += Time.deltaTime;
        if (this.transform.position == this.Target.position || this.timer >= this.MaxMovingTime) // no more move
        {
            this.state = AiState.Idle;
            this.timer = 0;
        }
    }

    private void IdleAction()
    {
        this.timer += Time.deltaTime;
        if (this.timer < this.currentWanderTime) // Still have to wait
            return;
        this.state = AiState.Wander; // wait is over, let's search for a new target
        this.currentWanderTime = UnityEngine.Random.Range(this.WanderTimeMin, this.WanderTimeMax);
    }

    private void WanderAction()
    {
        var newPos = this.RandomNavSphere(transform.position, this.WanderRadius, -1);
        this.Target.position = newPos;
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
        Debug.Log("Looking for a new target");
        var attemps = 0;
        while (attemps < 10)
        {
            if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
                return navHit.position;
            ++attemps;
        }
        Debug.Log("Forgot how to move.");
        return Vector3.zero;
    }
}
