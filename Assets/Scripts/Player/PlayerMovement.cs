using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Id")]
    public int m_PlayerNumber = 1;       

    [Header("Properties")]
    public float m_Speed = 10f;
    public float m_SpeedToAim = 5.0f;
    public float m_TurnSpeed = 30f;           
         
    private Rigidbody m_Rigidbody;         
   
    private Vector3 m_Movement;
    private Vector3 m_Turning;
    private bool m_ToAim;

    private float m_LeftVerticalAnalog;
    private float m_LeftHorizontalAnalog;
    private float m_RightVerticalAnalog;
    private float m_RightHorizontalAnalog;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_LeftVerticalAnalog = 0.0f;
        m_LeftHorizontalAnalog = 0.0f;
        m_RightVerticalAnalog = 0.0f;
        m_RightHorizontalAnalog = 0.0f;
        m_ToAim = false;
        m_Movement = Vector3.zero;
        m_Turning = Vector3.zero;
    }

    private void Update()
    {
        m_LeftVerticalAnalog = Input.GetAxis("LeftStickVertical" + m_PlayerNumber);
        m_LeftHorizontalAnalog = Input.GetAxis("LeftStickHorizontal" + m_PlayerNumber);

        m_RightVerticalAnalog = Input.GetAxis("RightStickVertical" + m_PlayerNumber);
        m_RightHorizontalAnalog = Input.GetAxis("RightStickHorizontal" + m_PlayerNumber);

        m_ToAim = m_RightHorizontalAnalog != 0.0f || m_RightVerticalAnalog != 0.0f;
    }

    private void FixedUpdate()
    {
        Move(m_LeftHorizontalAnalog, m_LeftVerticalAnalog, m_ToAim ? m_SpeedToAim : m_Speed);
        
        if (m_ToAim)
            Turn(m_RightHorizontalAnalog, m_RightVerticalAnalog); 
        else
            Turn(m_LeftHorizontalAnalog, m_LeftVerticalAnalog);
            
    }

    private void Move(float horizontal, float vertical, float speed)
    {
        m_Movement.Set(horizontal, 0.0f, vertical);
        m_Movement = m_Movement.normalized * speed * Time.deltaTime;

        m_Movement = Camera.main.transform.TransformDirection(m_Movement);
        m_Movement.y = 0.0f;

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement);
    }

    private void Turn(float horizontal, float vertical)
    {
        m_Turning.Set(horizontal, 0.0f, vertical);
        m_Turning = Camera.main.transform.TransformDirection(m_Turning);
        m_Turning.y = 0.0f;

        if (m_Turning == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(m_Turning);
        Quaternion newRotation = Quaternion.Lerp(m_Rigidbody.rotation, targetRotation, m_TurnSpeed * Time.deltaTime);
        m_Rigidbody.MoveRotation(newRotation);
    }
}
