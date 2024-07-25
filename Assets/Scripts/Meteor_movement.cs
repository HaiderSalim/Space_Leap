using System;
using System.Diagnostics;
using UnityEngine;

public class Meteor_movement : MonoBehaviour
{
    private enum Direction {RToL, LToR}
    [SerializeField]
    private Direction DirectionType = Direction.RToL;
    [SerializeField, Range(0.01f, 0.5f)]
    private float m_Speed = 0;
    [SerializeField, Range(0.1f, 1f), Header("Direction Adjustment"), Tooltip("The lower the value lesser the effect of the dimentions of x")]
    private float vertical_Adjust = 0.5f;
    [SerializeField, Range(0.1f, 1f), Tooltip("The lower the value lesser the effect of the dimentions of y")]
    private float horizontal_Adjust = 0.1f;
    private float direction_Changer = 1f;

    void Start()
    {
        if (DirectionType == Direction.RToL)
        {
            direction_Changer = -direction_Changer;
        }
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(new Vector3 (direction_Changer * m_Speed * horizontal_Adjust, -m_Speed * vertical_Adjust, 0), Space.Self);
        //transform.Translate(new Vector3(1, 1, 0), Space.Self);
    }
}
