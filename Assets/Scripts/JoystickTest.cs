using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JoystickTest : MonoBehaviour
{
    public enum JoystickAxis
    {
        LeftStickHorizontal = 0,
        LeftStickVertical = 1,
        RightStickHorizontal = 3,
        RightStickVertical = 4,
        DPADHorizontal = 5,
        DPADVertical = 6,
        LeftTrigger = 8,
        RightTrigger = 9,
    }

    public enum JoystickButton
    {
        ButtonY = 3,
        ButtonB = 1,
        ButtonA = 0,
        ButtonX = 2,
        LeftBumper = 4,
        RightBumper = 5,
        StartButton = 7,
        BackButton = 6,
        LeftStickClick = 8,
        RightStickClick = 9,
    }

    private void Update()
    {
        foreach (string name in Enum.GetNames(typeof(JoystickAxis)))
        {
            string axisName = name + 1;
            float value = Input.GetAxis(axisName);
            Debug.Log(string.Format("{0}: {1}", axisName, value));
        }

        foreach (string name in Enum.GetNames(typeof(JoystickButton)))
        {
            string axisName = name + 1;
            bool value = Input.GetButton(axisName);
            Debug.Log(string.Format("{0}: {1}", axisName, value ? "pressed" : "released"));
        }
    }
}
