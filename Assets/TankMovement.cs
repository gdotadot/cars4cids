﻿using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 30f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    public float maxSpeed = 30f;

    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal";

    }


    private void Update()
    {
        // Store the value of both input axes.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }

    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }


    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        if (m_MovementInputValue == 1)
        {
            if (m_Speed < 55)
            {
                m_Speed++;
                Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
                m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            } else
            {
                Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
                m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            }
        } else if (m_MovementInputValue == -1)
        {
            if (m_Speed > maxSpeed)
            {
                m_Speed--;
                Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
                m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            }
            else
            {
                Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
                m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            }
        } else
        {
            Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
      

        // Apply this movement to the rigidbody's position.
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}