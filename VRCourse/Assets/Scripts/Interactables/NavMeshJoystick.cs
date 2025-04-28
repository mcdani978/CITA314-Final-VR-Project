using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshJoystick : SimpleHingeInteractable
{
    [SerializeField]
    NavMeshRobot robot;

    [SerializeField]
    Transform trackingObject;

    [SerializeField]
    Transform trackedObject;

    [SerializeField]
    Transform rotationParentObj;

    protected override void ResetHinge()
    {
        if (robot != null)
        {
            robot.StopAgent();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (isSelected)
        {
            MoveRobot();
        }
    }

    private void MoveRobot()
    {
        if (robot != null)
        {
            trackingObject.position = new Vector3(
                trackedObject.position.x,
                trackingObject.position.y,
                trackedObject.position.z
                );

            rotationParentObj.rotation = Quaternion.identity;


            robot.MoveAgent(trackingObject.localPosition);
        }
    }
}
