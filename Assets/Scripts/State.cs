using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected Rigidbody rb;
    protected BrutalBot brutalBot;
    protected Animator animator;
    protected BotStateMachine botStateMachine;
    protected Transform botTransform;
    protected NavMeshAgent agent;

    public State(BrutalBot brutalBot, BotStateMachine botStateMachine ,Animator animator, Rigidbody rb, Transform botTransform, NavMeshAgent agent)
    {
        this.brutalBot = brutalBot;
        this.animator = animator;   
        this.rb = rb;
        this.botStateMachine = botStateMachine;
        this.botTransform = botTransform;
        this.agent = agent;
    }
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnExit() { }
}

