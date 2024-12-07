using DG.Tweening;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public class BrutalBot : MonoBehaviour 
{
    private BotStateMachine _botStateMachine;

    private NavMeshAgent _agent;

    [SerializeField]
    private Animator _animator;

    private Rigidbody _rb;

    public bool canPunch;
    public bool punchPerformed;

    [SerializeField]
    private Transform rightPunchHitPoint;
    [SerializeField]
    private Transform leftPunchHitPoint;

    [SerializeField]
    private Material originalMaterial;

    [SerializeField]
    private Material hitMaterial;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;

    public Transform currentTarget;

    public Health Health;

    public Vector3 currentRunPos;
    private float _currentDamage;

    private int CurrentLevelIndex = 0;

    [SerializeField]
    private List<LevelSO> _levels;

    [SerializeField]
    private float _rotationSpeed;

    private void Start()
    {
        _botStateMachine = new BotStateMachine();
        Initialize();
        
        canPunch = true;
        SetLevel();
    }

    private void SetLevel()
    {
        var level = _levels[CurrentLevelIndex];
        Health.SetupLevel(level.health, level.stunBar);
        _agent.speed = level.speed;
        _currentDamage = level.damage;
    }

    private void Initialize()
    {
        _agent = transform.GetComponent<NavMeshAgent>();
        Health = transform.GetComponent<Health>();
        _rb = transform.GetComponent<Rigidbody>();
        _botStateMachine.Initialize(this, _animator, _rb, transform, _agent);
    }

    private void Update()
    {
        _botStateMachine.CurrentBotState.OnUpdate();
        var posToLookAt = new Vector3(_agent.destination.x, transform.position.y, _agent.destination.z);
        transform.LookAt(posToLookAt);
        _animator.SetFloat("MovementSpeed", _agent.speed / 2);
    }

    public void CanPunch()
    {
        canPunch = true;
    }
    public void TakeDamage(string attackType)
    {
        var hitPointPosition = Vector3.zero;

        switch (attackType)
        {
            case "RightPunch":
                hitPointPosition = rightPunchHitPoint.position;
                break;
            case "LeftPunch":
                hitPointPosition = leftPunchHitPoint.position;
                break;
        }

        Collider[] colliders = Physics.OverlapSphere(hitPointPosition, 0.05f);

        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                if (collider.gameObject.GetComponent<Player>() != null && collider.gameObject != gameObject)
                {
                    collider.gameObject.GetComponent<Player>().GetHit(_currentDamage);
                }

                if (collider.gameObject.GetComponent<BrutalBot>() != null && collider.gameObject != gameObject)
                {
                    collider.gameObject.GetComponent<BrutalBot>().GetHit(_currentDamage);
                }
            }
        }
    }
    private void EvaluateSurroundings()
    {
        //Bot should prioritize food, if he spots a target that is of same level as he is, or he has bigger health than him.
        //Something like (enemy.health - bot.damage == kill for bot) otherwise try to run.
    }


    public void GetHit(float damage)
    {
        skinnedMeshRenderer.material = hitMaterial;
        Invoke("EnableOrigMaterial", 0.1f);
        Health.SetHealth(damage);
    }

    private void EnableOrigMaterial()
    {
        skinnedMeshRenderer.material = originalMaterial;
    }
}
