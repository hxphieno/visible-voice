using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;        // ����ƶ��ٶ�
    public float rotationSpeed = 3f;   // ���������
    public float jumpForce = 5f;       // ��Ծ����
    public Transform cameraTransform;  // �����

    private CharacterController characterController; // ��ɫ���������
    private Vector3 velocity;         // ��ֱ������ٶ�
    private bool isGrounded;          // �Ƿ��ڵ�����
    public float gravity = -9.81f;    // ����ֵ

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //���б�ע�͵�����Ϊ���Ծ��鹦����Ҫ�������
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();      
        RotatePlayer();    
    }

    void MovePlayer()
    {

        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        // ��ȡ������
        float horizontal = Input.GetAxis("Horizontal"); // A �� D ��
        float vertical = Input.GetAxis("Vertical");     // W �� S ��


        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        characterController.Move(move * moveSpeed * Time.deltaTime);

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
