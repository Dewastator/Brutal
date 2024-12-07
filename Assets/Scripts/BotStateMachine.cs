using UnityEngine;
using UnityEngine.AI;

public class BotStateMachine : MonoBehaviour
{
    #region States
    public State CurrentBotState { get; set; }

    public WanderState wanderState { get; private set; }
    public AttackState attackState { get; private set; }
    public FleeState fleeState { get; private set; }
    public ChaseTarget chaseState { get; private set; }

    #endregion


    public void Initialize(BrutalBot brutalBot, Animator animator, Rigidbody rb, Transform botTransform, NavMeshAgent agent)
    {
        wanderState = new WanderState(brutalBot, this, animator, rb, botTransform, agent);
        attackState = new AttackState(brutalBot, this, animator, rb, botTransform, agent);
        fleeState = new FleeState(brutalBot, this, animator, rb, botTransform, agent);
        chaseState = new ChaseTarget(brutalBot, this, animator, rb, botTransform, agent);
        

        CurrentBotState = wanderState;
        CurrentBotState.OnEnter();
    }

    public void ChangeState(State newState)
    {
        if (newState != CurrentBotState)
        {
            CurrentBotState.OnExit();
            CurrentBotState = newState;
            CurrentBotState.OnEnter();
        }
    }
}
