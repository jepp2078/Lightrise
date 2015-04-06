using UnityEngine;
using System.Collections;

public class RPG_Controller : MonoBehaviour
{
    public CharacterController characterController;
    public float walkSpeed = 7f;
    public float turnSpeed = 0f;
    public float jumpHeight = 10f;
    public float gravity = 35f;
    public float fallingThreshold = -6f; // -6f gets the character being almost always grounded

    private Vector3 playerDir;
    private Vector3 playerDirWorld;
    private Vector3 rotation = Vector3.zero;
    private int serverTickIn;
    private int serverTickCurrent = 0;
    private int serverTickCurrentAudio = 0;
    public Player playerInstance;
    public Camera cameraInstance;

    public AudioSource[] audio = new AudioSource[4];
    private int audioState = 0;
    private bool didPlay = false;
    public bool autorun = false;
    private int lastPlayed = 0;
    public float walkSpeedModifier = 0;
    public float baseCrouchSpeed = 4f, baseWalkSpeed = 7f, baseSprintSpeed = 10f, basePaddlePenalty = 1f;

    void Awake()
    {
        characterController = GetComponent("CharacterController") as CharacterController;

        //RPG_Camera.CameraSetup();
    }


    void Update()
    {
        if (cameraInstance == null)
            return;

        if (characterController == null)
        {
            Debug.Log("Error: No Character Controller component found! Please add one to the GameObject which has this script attached.");
            return;
        }

        GetInput();

        StartMotor();
    }


    void GetInput()
    {
        float horizontalStrafe = 0f;
        float vertical = 0f;
        if (walkSpeed < 7)
        {
            audioState = 12;
        }
        else if (walkSpeed <= 10)
        {
            audioState = 6;
        }
        else
        {
            audioState = 3;
        }
        if (Input.GetButton("Horizontal Strafe"))
        {
            if (((Castable)playerInstance.player.getSkill(1)).getState())
            {
                ((Castable)playerInstance.player.getSkill(1)).stopEffect();
            }
            horizontalStrafe = Input.GetAxis("Horizontal Strafe") < 0 ? -1f : Input.GetAxis("Horizontal Strafe") > 0 ? 1f : 0f;
            if (horizontalStrafe != 0)
            {
                if(walkSpeedModifier == 3)
                    walkSpeed = baseSprintSpeed - 1;
                else if (walkSpeedModifier >0)
                    walkSpeed = baseWalkSpeed - 1 + walkSpeedModifier;
                else
                    walkSpeed = baseCrouchSpeed - 1  + playerInstance.player.getSkillEffect(3);
            }
            else
            {
                if (walkSpeedModifier == 3)
                    walkSpeed = baseSprintSpeed + walkSpeedModifier;
                else if (walkSpeedModifier > 0)
                    walkSpeed = baseWalkSpeed + walkSpeedModifier;
                else
                    walkSpeed = baseCrouchSpeed + playerInstance.player.getSkillEffect(3);
            }

            serverTickIn = playerInstance.serverTicks;
            serverTickCurrentAudio = serverTickIn;
            if (serverTickCurrentAudio % audioState == 0 && characterController.isGrounded)
            {
                if (serverTickCurrent != serverTickIn)
                {
                    didPlay = false;
                }
                if (!didPlay)
                {
                    int current;
                    do
                    {
                    current = Random.Range(0, 4);
                    }
                    while(current == lastPlayed);
                    lastPlayed = current;
                    //audio[current].Play();
                    didPlay = true;
                }
            }
            if (serverTickCurrent != serverTickIn)
            {
                if (!Input.GetButton("Crouch") && !Input.GetButton("Sprint"))
                {
                    playerInstance.gainSkill(0.0833f / 60f, 0);
                }
                serverTickCurrent = serverTickIn;
            }
        }

        if (Input.GetButton("Vertical") || autorun)
        {
            if (((Castable)playerInstance.player.getSkill(1)).getState())
            {
                ((Castable)playerInstance.player.getSkill(1)).stopEffect();
            }
            vertical = Input.GetAxis("Vertical") < 0 ? -1f : Input.GetAxis("Vertical") > 0 ? 1f : 0f;
            if (autorun)
                vertical = 1f;
            if (vertical < 0)
            {
                if (walkSpeedModifier == 3)
                    if (horizontalStrafe == 0)
                        walkSpeed = baseWalkSpeed - 1;
                    else
                        walkSpeed = baseWalkSpeed - 2;
                else if (walkSpeedModifier > 0)
                    if (horizontalStrafe == 0)
                        walkSpeed = baseWalkSpeed - 1 + playerInstance.player.getSkillEffect(0);
                    else
                        walkSpeed = baseWalkSpeed - 2 + playerInstance.player.getSkillEffect(0);
                else
                    if (horizontalStrafe == 0)
                        walkSpeed = baseCrouchSpeed - 1 + playerInstance.player.getSkillEffect(3);
                    else
                        walkSpeed = baseCrouchSpeed - 2 + playerInstance.player.getSkillEffect(3);
            }
            else
            {
                if (walkSpeedModifier == 3)
                    if (horizontalStrafe == 0)
                        walkSpeed = baseSprintSpeed;
                    else
                        walkSpeed = baseSprintSpeed - 1;
                else if (walkSpeedModifier > 0)
                    if (horizontalStrafe == 0)
                        walkSpeed = baseWalkSpeed + playerInstance.player.getSkillEffect(0);
                    else
                        walkSpeed = baseWalkSpeed - 1 + playerInstance.player.getSkillEffect(0);
                else
                    if (horizontalStrafe == 0)
                        walkSpeed = baseCrouchSpeed + playerInstance.player.getSkillEffect(3);
                    else
                        walkSpeed = baseCrouchSpeed - 1 + playerInstance.player.getSkillEffect(3);
            }

            serverTickIn = playerInstance.serverTicks;
            serverTickCurrentAudio = serverTickIn;
            if (serverTickCurrentAudio % audioState == 0 && characterController.isGrounded)
            {
                if (serverTickCurrent != serverTickIn)
                {
                    didPlay = false;
                }
                if (!didPlay)
                {
                    int current;
                    do
                    {
                        current = Random.Range(0, 4);
                    }
                    while (current == lastPlayed);
                    lastPlayed = current;
                    //audio[current].Play();
                    didPlay = true;
                }
            }
            if (serverTickCurrent != serverTickIn)
            {
                if (!Input.GetButton("Crouch") && !Input.GetButton("Sprint"))
                {
                    playerInstance.gainSkill(0.0833f / 60f, 0);
                }
                serverTickCurrent = serverTickIn;
            }
        }

        playerDir = horizontalStrafe * Vector3.right + vertical * Vector3.forward;
        //if (RPG_Animation.instance != null)
        //    RPG_Animation.instance.SetCurrentMoveDir(playerDir);

        if (characterController.isGrounded)
        {
            playerDirWorld = transform.TransformDirection(playerDir);

            if (Mathf.Abs(playerDir.x) + Mathf.Abs(playerDir.z) > 1)
                playerDirWorld.Normalize();

            playerDirWorld *= walkSpeed;
            playerDirWorld.y = fallingThreshold;

            if (Input.GetButtonDown("Jump"))
            {
                if (((Castable)playerInstance.player.getSkill(1)).getState())
                {
                    ((Castable)playerInstance.player.getSkill(1)).stopEffect();
                }
                playerDirWorld.y = jumpHeight;
                //if (RPG_Animation.instance != null)
                //    RPG_Animation.instance.Jump(); // the pattern for calling animations is always the same: just add some lines under line 77 and write an if statement which
            }                                      // checks for an arbitrary key if it is pressed and, if true, calls "RPG_Animation.instance.YourAnimation()". After that you add
            gameObject.GetComponent<PhotonTransformView>().SetSynchronizedValues(playerDirWorld, 0f);
        }
        else 
        {
            gameObject.GetComponent<PhotonTransformView>().SetSynchronizedValues(playerDirWorld, 0f);
        }
            // this method to the other animation clip methods in "RPG_Animation" (do not forget to make it public) 

        rotation.y = Input.GetAxis("Horizontal") * turnSpeed;
    }


    void StartMotor()
    {
        playerDirWorld.y -= gravity * Time.deltaTime;
        characterController.Move(playerDirWorld * Time.deltaTime);

        transform.Rotate(rotation);
    }
}