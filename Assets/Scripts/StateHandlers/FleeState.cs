using UnityEngine;
using UnityEngine.AI;

public class FleeState : State
{
    private float runTimer = 20;
    public FleeState(BrutalBot brutalBot, BotStateMachine botStateMachine, Animator animator, Rigidbody rb, Transform botTransform, NavMeshAgent agent) : base(brutalBot, botStateMachine, animator, rb, botTransform, agent)
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
        Flee();
    }

    private void Flee()
    {
        var runDir = botTransform.position + brutalBot.currentRunPos * UnityEngine.Random.Range(10f, 70f);
        bool isValid = agent.SetDestination(runDir);
        if(!isValid )
        {
            brutalBot.currentRunPos = UtilityClass.GetRandomDir();
        }
        runTimer -= Time.deltaTime;

        if (Time.time >= Time.time + runTimer)
        {
            runTimer = 20;
            botStateMachine.ChangeState(botStateMachine.wanderState);
        }
    }
}
