using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Renderer rightTriggerObj = null;
    public Renderer leftTriggerObj = null;

    public Renderer leftBumperObj = null;
    public Renderer rightBumperObj = null;

    public Renderer startButtonObj = null;
    public Renderer backButtonObj = null;

    public Renderer aButtonObj = null;
    public Renderer bButtonObj = null;
    public Renderer xButtonObj = null;
    public Renderer yButtonObj = null;

    public Renderer dpadLeftObj = null;
    public Renderer dpadRightObj = null;
    public Renderer dpadUpObj = null;
    public Renderer dpadDownObj = null;

    public Transform joystickLObj = null;
    public Transform joystickRObj = null;

    public float movementSpeed = 1.0f;

    void Update()
    {
        // Controller
        float rightTrigger = Input.GetAxis("Right Trigger");
        float leftTrigger = Input.GetAxis("Left Trigger");

        bool leftBumper = Input.GetButton("Left Bumper");
        bool rightBumper = Input.GetButton("Right Bumper");

        bool startButton = Input.GetButton("Start");
        bool backButton = Input.GetButton("Back");

        bool aButton = Input.GetButton("A Button");
        bool bButton = Input.GetButton("B Button");
        bool xButton = Input.GetButton("X Button");
        bool yButton = Input.GetButton("Y Button");

        float dpadHorizontal = Input.GetAxis("Dpad Horizontal");
        float dpadVertical = Input.GetAxis("Dpad Vertical");

        float joystickLHorizontal = Input.GetAxis("L Horizontal");
        float joystickLVertical = Input.GetAxis("L Vertical");
        float joystickRHorizontal = Input.GetAxis("R Horizontal");
        float joystickRVertical = Input.GetAxis("R Vertical");

        // Colors
        rightTriggerObj.material.color = new Color(rightTrigger, rightTrigger, rightTrigger);
        leftTriggerObj.material.color = new Color(leftTrigger, leftTrigger, leftTrigger);

        if (dpadHorizontal < 0.0f)
        {
            dpadLeftObj.material.color = new Color(1.0f, 1.0f, 1.0f);
            dpadRightObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        }
        else if (dpadHorizontal > 0.0f)
        {
            dpadRightObj.material.color = new Color(1.0f, 1.0f, 1.0f);
            dpadLeftObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        }
        else if (dpadHorizontal == 0.0f)
        {
            dpadRightObj.material.color = new Color(0.0f, 0.0f, 0.0f);
            dpadLeftObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        }

        if (dpadVertical < 0.0f)
        {
            dpadDownObj.material.color = new Color(1.0f, 1.0f, 1.0f);
            dpadUpObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        }
        else if (dpadVertical > 0.0f)
        {
            dpadUpObj.material.color = new Color(1.0f, 1.0f, 1.0f);
            dpadDownObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        }
        else if (dpadVertical == 0.0f)
        {
            dpadDownObj.material.color = new Color(0.0f, 0.0f, 0.0f);
            dpadUpObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        }

        if (leftBumper)
            leftBumperObj.material.color = new Color(1.0f, 1.0f, 1.0f);
        else if (!leftBumper)
            leftBumperObj.material.color = new Color(0.0f, 0.0f, 0.0f);

        if (rightBumper)
            rightBumperObj.material.color = new Color(1.0f, 1.0f, 1.0f);
        else if (!rightBumper)
            rightBumperObj.material.color = new Color(0.0f, 0.0f, 0.0f);

        if (aButton)
            aButtonObj.material.color = new Color(1.0f, 1.0f, 1.0f);
        else if (!aButton)
            aButtonObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        if (bButton)
            bButtonObj.material.color = new Color(1.0f, 1.0f, 1.0f);
        else if (!bButton)
            bButtonObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        if (xButton)
            xButtonObj.material.color = new Color(1.0f, 1.0f, 1.0f);
        else if (!xButton)
            xButtonObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        if (yButton)
            yButtonObj.material.color = new Color(1.0f, 1.0f, 1.0f);
        else if (!yButton)
            yButtonObj.material.color = new Color(0.0f, 0.0f, 0.0f);

        if (startButton)
            startButtonObj.material.color = new Color(1.0f, 1.0f, 1.0f);
        else if (!startButton)
            startButtonObj.material.color = new Color(0.0f, 0.0f, 0.0f);
        if (backButton)
            backButtonObj.material.color = new Color(1.0f, 1.0f, 1.0f);
        else if (!backButton)
            backButtonObj.material.color = new Color(0.0f, 0.0f, 0.0f);

        // Movement
        dpadHorizontal *= movementSpeed * Time.deltaTime;
        dpadVertical *= movementSpeed * Time.deltaTime;

        joystickLHorizontal *= movementSpeed * Time.deltaTime;
        joystickLVertical *= movementSpeed * Time.deltaTime;
        joystickRHorizontal *= movementSpeed * Time.deltaTime;
        joystickRVertical *= movementSpeed * Time.deltaTime;

        joystickLObj.Translate(joystickLHorizontal, 0.0f, joystickLVertical);
        joystickRObj.Translate(joystickRHorizontal, 0.0f, joystickRVertical);
    }
}
