using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private Transform debugHitPointTransform;
    [SerializeField] private Transform hookshotTransform;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float hookshotLength;

    private CharacterController characterController;
    private float cameraVerticalAngle;
    private float characterVelocityY;
    private Vector3 characterVelocityMomentum;
    private Camera playerCamera;
    private CameraFov cameraFov;
    //private ParticleSystem speedLinesParticleSystem;
    private State state;
    private Vector3 hookshotPosition;
    private float hookshotSize;

    public bool can;

    private bool dashing;
    private float dashTime;
    private float dashPower;

    private float draftTime;
    private float updraftPower;

    Vector3 dashDirection;

    Manta man;

    ManuManu mani;

    float draftCD;
    float dashCD;

    float fordashtext;
    float fordrafttext;

    public TextMeshProUGUI udCdtext;
    public TextMeshProUGUI dashcdtext;



    private enum State
    {
        Normal,
        HookshotThrown,
        HookshotFlyingPlayer,
    }

    private void Awake()
    {
        draftCD = 15;
        dashCD = 10;

        man = GameObject.Find("Manta").GetComponent<Manta>();
        mani = GameObject.Find("Manta").GetComponent<ManuManu>();

        SetDashVariables();
        characterController = GetComponent<CharacterController>();
        playerCamera = transform.Find("Camera").GetComponent<Camera>();
        cameraFov = playerCamera.GetComponent<CameraFov>();
        Cursor.lockState = CursorLockMode.Locked;
        state = State.Normal;
        hookshotTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleLast();

        dashCD += Time.deltaTime;
        draftCD += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (!man.gameOver)
        {
            if (state == State.Normal) can = true;
            else can = false;

            switch (state)
            {
                default:
                case State.Normal:
                    HandleCharacterLook();
                    HandleCharacterMovement();
                    HandleHookshotStart();
                    break;
                case State.HookshotThrown:
                    HandleHookshotThrow();
                    HandleCharacterLook();
                    HandleCharacterMovement();
                    break;
                case State.HookshotFlyingPlayer:
                    HandleCharacterLook();
                    HandleHookshotMovement();
                    break;
            }
        }
    }

    private void HandleCharacterLook()
    {
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(new Vector3(0f, lookX * mouseSensitivity, 0f), Space.Self);

        cameraVerticalAngle -= lookY * mouseSensitivity;

        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }

    private void HandleCharacterMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        float moveSpeed = 20f;
        

        if (Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded) moveSpeed = 1.5f * moveSpeed;

        Vector3 characterVelocity = (transform.right * moveX + transform.forward * moveZ).normalized * moveSpeed;


        if (characterController.isGrounded)
        {
            characterVelocityY = 0f;
            if (TestInputJump())
            {
                float jumpSpeed = 30f;
                characterVelocityY = jumpSpeed;
            }
        }


        if (draftCD >= 5 && Input.GetKeyDown(KeyCode.Q) && mani.canUp)
        {
            draftCD = 0;
            characterVelocityY = updraftPower;
            StartCoroutine("SetUpDraft");
        }



        float gravityDownForce = -60f;
        characterVelocityY += gravityDownForce * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Space) && mani.canUp)
        {
            characterVelocityY = Mathf.Clamp(characterVelocityY, -floatSpeed, characterVelocityY);
        }
        
        characterVelocity.y = characterVelocityY;

        characterVelocity += characterVelocityMomentum;

        if (dashCD >= 6 && Input.GetKeyDown(KeyCode.F) && mani.canDash)
        {
            dashCD = 0;
            StartDash();
            characterVelocity.y = 0;
            if (characterVelocity.normalized == Vector3.zero) dashDirection = transform.forward * dashPower * moveSpeed;
            else
                dashDirection = characterVelocity * dashPower;
        }

        if (dashing)
        {
            characterVelocity = dashDirection;
        }

        Physics.SyncTransforms();
        characterController.Move(characterVelocity * Time.deltaTime);

        if(characterVelocityMomentum.magnitude > 0f)
        {
            float momentumDrag = 3f;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if(characterVelocityMomentum.magnitude < .0f)
            {
                characterVelocityMomentum = Vector3.zero;
            }
        }
    }

    private void ResetGravityEffect()
    {
        characterVelocityY = 0f;
    }

    private void HandleHookshotStart()
    {
        if (TestInputDownHookshot())
        {
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit, hookshotLength))
            {
                //if(true/*raycastHit.transform.parent && raycastHit.transform.gameObject.CompareTag("Platforms")*/)
                {
                    debugHitPointTransform.position = raycastHit.point;
                    if(raycastHit.transform.gameObject.CompareTag("Platforms"))
                        debugHitPointTransform.transform.parent = raycastHit.transform;
                    hookshotPosition = debugHitPointTransform.position;
                    hookshotSize = 0f;
                    hookshotTransform.gameObject.SetActive(true);
                    hookshotTransform.localScale = Vector3.zero;
                    state = State.HookshotThrown;
                }
            }
        }
    }

    private void HandleHookshotThrow()
    {
        hookshotTransform.LookAt(hookshotPosition);

        float hookshotThrowSpeed = 500f;
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;

        if(hookshotSize >= Vector3.Distance(transform.position, hookshotPosition))
        {
            hookshotSize = Vector3.Distance(transform.position, hookshotPosition);
            hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);
            state = State.HookshotFlyingPlayer;
        }
        else
        {
            hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);
        }

        hookshotPosition = debugHitPointTransform.position;
    }

    private void HandleHookshotMovement()
    {
        hookshotTransform.LookAt(hookshotPosition);

        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        float hookshotSpeedMin = 2f;
        float hookshotSpeedMax = 10f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotSpeedMultiplier = 5f;

        characterController.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);

        float reachedHookshotPositionDistance = 5f;
        if(Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            StopHookshot();
        }

        if (TestInputDownHookshot())
        {
            StopHookshot();
        }

        if (TestInputJump())
        {
            float momentumExtraSpeed = 7f;
            characterVelocityMomentum = hookshotDir * hookshotSpeed * momentumExtraSpeed;
            float jumpSpeed = 40f;
            characterVelocityMomentum += Vector3.up * jumpSpeed;
            StopHookshot();
        }

        hookshotSize = Vector3.Distance(transform.position, hookshotPosition);
        hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);

        hookshotPosition = debugHitPointTransform.position;
    }

    private void StopHookshot()
    {
        state = State.Normal;
        ResetGravityEffect();
        hookshotTransform.gameObject.SetActive(false);
        debugHitPointTransform.parent = null;
    }

    private bool TestInputDownHookshot()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    private bool TestInputJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void StartDash()
    {
        dashing = true;
        StartCoroutine("SetDash");
    }

    IEnumerator SetDash()
    {
        yield return new WaitForSeconds(dashTime);
        dashing = false;
    }

    void SetDashVariables()
    {
        JettDash jD;
        jD = GetComponent<JettDash>();
        dashTime = jD.dashTime;
        dashPower = jD.dashPower;
        updraftPower = jD.draftPower;
        draftTime = jD.draftTime;
    }

    IEnumerator SetUpDraft()
    {
        yield return new WaitForSeconds(draftTime);
    }

    private void HandleLast()
    {
        fordrafttext = 5 - draftCD;
        udCdtext.text = ((int)fordrafttext + 1) + "s";
        if ((int)fordrafttext <= 0) udCdtext.text = "";

        fordashtext = 6 - dashCD;
        dashcdtext.text = ((int)fordashtext + 1) + "s";
        if ((int)fordashtext <= 0) dashcdtext.text = "";
    }
}
