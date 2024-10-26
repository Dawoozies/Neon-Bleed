using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityHFSM;
public class PathMove : PhysMovement, IVelocityComponent
{
    public int pathIndex;
    public float startDistanceThreshold;
    public float moveToStartSpeed;
    public float maxDistance;
    public AnimationCurve moveToStartSpeedCurve;
    public float edgeTraversalSpeed;
    public AnimationCurve edgeTraversalCurve;
    [Tooltip("How long to wait at each vertex before moving on")]
    public float vertexWaitTime;
    float vertexWaitTimer;
    public int edgeIndex;
    public float progress;
    public UnityEvent whileNotAtStarting;
    public UnityEvent whileTraversingEdge;
    public UnityEvent whileAtVertex;
    Tuple<Transform, Transform>[] edges;
    public enum Status
    {
        NotAtStarting,
        Traversing,
        WaitingAtVertex,
        PathCompleted
    }
    public Status pathStatus;
    StateMachine<Status> moveStateMachine;
    public enum PathCompleteBehaviour
    {
        Loop,
        PingPong,
    }
    public PathCompleteBehaviour pathCompleteBehaviour;
    bool pingPonging;
    public Vector2 v => movement;
    Vector2 movement;

    protected override void Awake()
    {
        base.Awake();
        onCastCollision.AddListener(OnCastCollisionHandler);
        edges = Paths.ins.GetPath(pathIndex);

        moveStateMachine = new StateMachine<Status>();

        moveStateMachine.AddState(Status.NotAtStarting, onLogic: state => NotAtStarting());
        moveStateMachine.AddState(Status.Traversing, onLogic: state => Traversing());
        moveStateMachine.AddState(Status.WaitingAtVertex, onLogic: state => WaitingAtVertex());
        moveStateMachine.AddState(Status.PathCompleted, onLogic: state => PathCompleted());

        moveStateMachine.AddTransition(Status.NotAtStarting, Status.Traversing, transition => NotAtStarting_To_Traversing());

        moveStateMachine.Init();
    }
    protected virtual void OnCastCollisionHandler(Vector3 hitVelocity, RaycastHit2D[] results)
    {
        int resultIndex = 0;
        if (results[0].rigidbody == rb)
        {
            resultIndex = 1;

            if (results.Length == 1)
                return;
        }
        rb.position = results[resultIndex].centroid;
    }
    public override void OnRent()
    {
        base.OnRent();
        pathStatus = Status.NotAtStarting;
        vertexWaitTimer = vertexWaitTime;

        edgeIndex = 0;
        progress = 0;
    }
    protected override void Move()
    {
        moveStateMachine.OnLogic();
    }
    protected virtual void NotAtStarting()
    {
        whileNotAtStarting?.Invoke();

        Vector2 startingPoint = edges[0].Item1.position;
        Vector2 dir = (startingPoint - (Vector2)transform.position).normalized;
        float distanceMagnitude = Vector2.Distance(transform.position, startingPoint);
        float lerpValue = Mathf.Clamp(distanceMagnitude / maxDistance, 0f, 1f);
        movement = dir * moveToStartSpeed * moveToStartSpeedCurve.Evaluate(lerpValue);
    }
    protected virtual bool NotAtStarting_To_Traversing()
    {
        Vector2 startingPoint = edges[0].Item1.position;
        if(Vector2.Distance(transform.position, startingPoint) <= startDistanceThreshold)
        {
            transform.position = startingPoint;
            movement = Vector2.zero;
            return true;
        }
        return false;
    }
    protected virtual void Traversing()
    {
        whileTraversingEdge?.Invoke();
        float nextProgress = progress + Time.fixedDeltaTime*edgeTraversalSpeed;
        Vector2 nextPosition = Vector2.Lerp(edges[edgeIndex].Item1.position, edges[edgeIndex].Item2.position, edgeTraversalCurve.Evaluate(nextProgress));
        if (pingPonging)
        {
            nextPosition = Vector2.Lerp(edges[edgeIndex].Item2.position, edges[edgeIndex].Item1.position, edgeTraversalCurve.Evaluate(nextProgress));
        }
        if (nextProgress >= 1f)
        {
            moveStateMachine.RequestStateChange(Status.WaitingAtVertex);
        }
        rb.MovePosition(nextPosition);
        progress = nextProgress;
    }
    protected virtual void WaitingAtVertex()
    {
        whileAtVertex?.Invoke();
        if(vertexWaitTimer > 0f)
        {
            vertexWaitTimer -= Time.deltaTime;
        }
        else
        {
            //go back to traversing
            vertexWaitTimer = vertexWaitTime;
            if(pingPonging)
            {
                edgeIndex--;
                if(edgeIndex < 0)
                {
                    moveStateMachine.RequestStateChange(Status.PathCompleted);
                }
                else
                {
                    moveStateMachine.RequestStateChange(Status.Traversing);
                }
            }
            else
            {
                edgeIndex++;
                if(edgeIndex >= edges.Length)
                {
                    moveStateMachine.RequestStateChange(Status.PathCompleted);
                }
                else
                {
                    moveStateMachine.RequestStateChange(Status.Traversing);
                }
            }
            progress = 0f;
        }
    }
    protected virtual void PathCompleted()
    {
        switch (pathCompleteBehaviour)
        {
            case PathCompleteBehaviour.Loop:
                edgeIndex = 0;
                moveStateMachine.RequestStateChange(Status.NotAtStarting);
                break;
            case PathCompleteBehaviour.PingPong:
                moveStateMachine.RequestStateChange(Status.Traversing);
                pingPonging = !pingPonging;
                if(pingPonging)
                {
                    edgeIndex--;
                }
                else
                {
                    edgeIndex++;
                }
                break;
        }
    }
}
