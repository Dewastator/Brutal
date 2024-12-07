using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class Player : MonoBehaviour
{
    private PlayerInputController inputController;
    private Rigidbody rb;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float movementSpeed = 2f;

    [SerializeField]
    private float rotationSpeed = 2f;

    [SerializeField]
    private LayerMask enemyMask;

    [SerializeField]
    private Transform rightPunchHitPoint;
    [SerializeField]
    private Transform leftPunchHitPoint;

    bool punchPerformed;
    bool canPunch;

    public static Player Instance;

    [SerializeField]
    private Material originalMaterial;

    [SerializeField]
    private Material hitMaterial;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;

    public Health Health;

    private int CurrentLevelIndex = 0;

    [SerializeField]
    private List<LevelSO> _levels;

    private float _currentDamage;

    private int _currentFoodAmount;
    public int CurrentFoodAmount 
    { 
        get 
        {
            return _currentFoodAmount; 
        }
        set 
        {
            _currentFoodAmount = value;
            CompareFoodAmountForLevelUp();
        }
    }

    private void CompareFoodAmountForLevelUp()
    {
        if(_currentFoodAmount >= _levels[CurrentLevelIndex].foodAmountToLevelUp) 
        {
            CurrentLevelIndex++;
            SetLevel();
            CurrentFoodAmount = 0;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inputController = GetComponent<PlayerInputController>();
        rb = GetComponent<Rigidbody>();
        canPunch = true;
        Health = GetComponent<Health>();
        SetLevel();
    }

    private void SetLevel()
    {
        var level = _levels[CurrentLevelIndex];
        Health.SetupLevel(level.health, level.stunBar);
        movementSpeed = level.speed;
        _currentDamage = level.damage;
        transform.localScale = level.charachterSize;
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    private void Update()
    {
        var inputDir = new Vector3(inputController.movePostion.x, 0f, inputController.movePostion.y).normalized;
        Vector3 faceDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

        //Get the angle between world forward and camera
        float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);
        var moveDir = Quaternion.Euler(0, cameraAngle, 0) * inputDir;
        if (moveDir.magnitude > 0f)
        {
            transform.position += moveDir * Time.deltaTime * movementSpeed;
            Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        if (inputController.movePostion.magnitude > 0)
        {
            animator.SetFloat("MovementSpeed",  movementSpeed / 2);
        }
        else
        {
            animator.SetFloat("MovementSpeed", 0f);
        }
    }

    public void Punch()
    {
        if (!canPunch)
        {
            return;
        }
        if(punchPerformed)
        {
            animator.Play("Right Punch", 1);
            punchPerformed = false;
            canPunch = false;
        }
        else
        {
            animator.Play("Left Punch", 1);
            punchPerformed = true;
            canPunch= false;
        }
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

    public void CanPunch()
    {
        canPunch = true;
    }
}
