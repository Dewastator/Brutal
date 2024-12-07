using UnityEngine;
using UnityEngine.AI;

public class WanderState : State
{
    private Vector3 _wanderPosition;
    private float _wanderDistance = 10f;

    public WanderState(BrutalBot brutalBot, BotStateMachine botStateMachine, Animator animator, Rigidbody rb, Transform botTransform, NavMeshAgent agent) : base(brutalBot, botStateMachine, animator, rb, botTransform, agent)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Wander();
        FindTarget();
    }

    private void Wander()
    {

        float distance = Vector3.Distance(botTransform.position, _wanderPosition);
        if (distance < _wanderDistance)
        {
            _wanderPosition = GetWanderPosition();
        }

        bool isValidDest = agent.SetDestination(_wanderPosition);

        if (!isValidDest)
        {
            _wanderPosition = GetWanderPosition();
        }
        
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(botTransform.position, 15f);

        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                if (collider.gameObject.GetComponent<Player>() != null)
                {
                    brutalBot.currentTarget = collider.gameObject.transform;
                    botStateMachine.ChangeState(botStateMachine.chaseState);
                }

                if (collider.gameObject.GetComponent<BrutalBot>() != null && collider.gameObject != brutalBot.gameObject)
                {
                    brutalBot.currentTarget = collider.gameObject.transform;
                    botStateMachine.ChangeState(botStateMachine.chaseState);
                }
            }
        }
    }

    private Vector3 GetWanderPosition()
    {
        return botTransform.position + UtilityClass.GetRandomDir() * UnityEngine.Random.Range(10f, 70f);
    }

    
}
