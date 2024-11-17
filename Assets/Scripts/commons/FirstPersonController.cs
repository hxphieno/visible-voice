using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;        
    public float rotationSpeed = 3f;   
    public float jumpForce = 5f;       
    public Transform cameraTransform;  

    private CharacterController characterController; 
    private Vector3 velocity;         
    private bool isGrounded;        
    public float gravity = -9.81f;    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();      
        RotatePlayer();    
    }

    void MovePlayer()
    {
        isGrounded = characterController.isGrounded;

        float horizontal = Input.GetAxis("Horizontal"); // A �� D ��
        float vertical = Input.GetAxis("Vertical");     // W �� S ��

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // �����ƶ��ٶȣ�������� Shift �����ٶ�Ϊԭ��������
        float currentMoveSpeed = isSprinting ? moveSpeed * 2 : moveSpeed;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        characterController.Move(move * currentMoveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        // ��ȡ�������
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // ��ת���ˮƽ����
        transform.Rotate(Vector3.up * mouseX);

        // ����������Ĵ�ֱ��ת��������ת�Ƕȱ��ⷭת��
        float verticalRotation = cameraTransform.localEulerAngles.x - mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -0f, 180f);

        cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
    }
}
