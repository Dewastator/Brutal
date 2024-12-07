using UnityEngine;
using UnityEngine.AI;

public class ChaseTarget : State
{
    public ChaseTarget(BrutalBot brutalBot, BotStateMachine botStateMachine, Animator animator, Rigidbody rb, Transform botTransform, NavMeshAgent agent) : base(brutalBot, botStateMachine, animator, rb, botTransform, agent)
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
        agent.SetDestination(brutalBot.currentTarget.position);
        AttackRange();
    }

    private void AttackRange()
    {
        if (brutalBot.currentTarget == null)
        {
            return;
        }

        float targetArea = 1.2f;

        if (Vector3.Distance(botTransform.position, brutalBot.currentTarget.position) < targetArea)
        {
            botStateMachine.ChangeState(botStateMachine.attackState);
        }
    }
}
