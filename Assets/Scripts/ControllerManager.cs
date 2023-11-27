using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
public class ControllerManager : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Action_Boolean showInstructionAction;
    // public SteamVR_Action_Boolean startMenuAction;
    public SteamVR_Action_Boolean shootAction;

    private SteamVR_Input_Sources handType;
    private SteamVR_Behaviour_Pose controllerPose;
    private ControllerGrabScript grabber;
    private ControllerTeleportScript teleporter;
    private ControllerInstructionScript instruction;
    // private ControllerStartMenu startMenu;
    private ControllerShootScript shooter;

    private GameObject objectInHand;

    // Start is called before the first frame update
    void Start()
    {
        controllerPose = GetComponent<SteamVR_Behaviour_Pose>();
        handType = controllerPose.inputSource;
        grabber = GetComponent<ControllerGrabScript>();
        teleporter = GetComponent<ControllerTeleportScript>();
        instruction = GetComponent<ControllerInstructionScript>();
        // startMenu = GetComponent<ControllerStartMenu>();
        shooter = GetComponent<ControllerShootScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabber)
        {
            if (Input.GetMouseButtonDown(0)) // Use left mouse button for grabbing
            {
                if (grabber.IsGrabbing())
                {
                    objectInHand = null;
                    grabber.ReleaseObject(null); // Pass null for non-VR release
                }
                else if (grabber.IsCollidingObject())
                {
                    Debug.Log("Touching: " + grabber.GetCollidingObject().name);
                    if (grabber.IsExtinguisher())
                    {
                        objectInHand = grabber.GrabExtinguisher(transform.rotation);
                    }
                    else
                    {
                        objectInHand = grabber.GrabObject();
                    }
                }
            }
        }

        if (teleporter)
        {
            // Remove VR teleportation logic
            // ...

            // Add logic for mouse shooting
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) // Use right mouse click or spacebar for shooting
            {
                if (shooter && objectInHand && shooter.isExtinguisher(objectInHand))
                {
                    shooter.ShootExtinguisher(objectInHand);
                }
            }
        }

        if (instruction)
        {
            if (Input.GetKeyDown(KeyCode.I)) // Example: Use 'I' key for instruction
            {
                instruction.ToggleInstruction();
            }
        }

        if (shooter)
        {
            // Add logic for mouse shooting
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) // Use right mouse click or spacebar for shooting
            {
                if (objectInHand && shooter.isExtinguisher(objectInHand))
                {
                    shooter.ShootExtinguisher(objectInHand);
                }
            }

            // Add logic for stopping shooting
            if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Space))
            {
                if (objectInHand && shooter.isExtinguisher(objectInHand))
                {
                    shooter.Stop(objectInHand);
                }
            }
        }

        // if (startMenu)
        // {
        //     if (startMenuAction.GetState(handType)){
        //         startMenu.TryLaser(controllerPose);
        //     } else {
        //         startMenu.DisableLaser();
        //     }
        // }
    }
}
