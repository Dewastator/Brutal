using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    public AttackState(BrutalBot brutalBot, BotStateMachine botStateMachine, Animator animator, Rigidbody rb, Transform botTransform, NavMeshAgent agent) : base(brutalBot, botStateMachine, animator, rb, botTransform, agent)
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
        Punch();
        AttackRange();
    }

    public void Punch()
    {
        botTransform.LookAt(brutalBot.currentTarget.position, Vector3.up);
        //if (brutalBot.Health.CurrentHealth <= 20)
        //{
        //    botStateMachine.ChangeState(botStateMachine.fleeState);
        //    brutalBot.currentRunPos = (botTransform.position - brutalBot.currentTarget.position).normalized;
        //    return;
        //}
        if (!brutalBot.canPunch)
        {
            return;
        }

        //PUNCHING
        if (brutalBot.punchPerformed)
        {
            animator.Play("Right Punch", 1);
            brutalBot.punchPerformed = false;
            brutalBot.canPunch = false;
        }
        else
        {
            animator.Play("Left Punch", 1);
            brutalBot.punchPerformed = true;
            brutalBot.canPunch = false;
        }
    }

    private void AttackRange()
    {
        if (brutalBot.currentTarget == null)
        {
            return;
        }

        float targetArea = 1.2f;

        if (Vector3.Distance(botTransform.position, brutalBot.currentTarget.position) > targetArea)
        {
            botStateMachine.ChangeState(botStateMachine.chaseState);
        }
    }
}
