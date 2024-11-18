using Unity.Burst.Intrinsics;
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
    private Rig rig;

    bool punchPerformed;
    bool canPunch;

    private void Start()
    {
        inputController = GetComponent<PlayerInputController>();
        rb = GetComponent<Rigidbody>();
        canPunch = true;
    }
    private void Update()
    {
        if(inputController.movePostion.magnitude > 0)
        {
            animator.SetFloat("MovementSpeed",  movementSpeed / 2);
        }
        else
        {
            animator.SetFloat("MovementSpeed", 0f);
        }
    }
    private void FixedUpdate()
    {
        var inputDir = new Vector3(inputController.movePostion.x, 0f, inputController.movePostion.y).normalized;
        Vector3 faceDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

        //Get the angle between world forward and camera
        float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);
        var moveDir = Quaternion.Euler(0, cameraAngle, 0) * inputDir;
        if (moveDir.magnitude > 0f)
        {
            rb.MovePosition(transform.position + moveDir * Time.fixedDeltaTime * movementSpeed);
            Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            rb.MoveRotation(rotation);
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

    public void CanPunch()
    {
        canPunch = true;
    }
}
