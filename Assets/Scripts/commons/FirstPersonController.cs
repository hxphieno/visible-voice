using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;        // 玩家移动速度
    public float rotationSpeed = 3f;   // 鼠标灵敏度
    public float jumpForce = 5f;       // 跳跃力度
    public Transform cameraTransform;  // 摄像机

    private CharacterController characterController; // 角色控制器组件
    private Vector3 velocity;         // 垂直方向的速度
    private bool isGrounded;          // 是否在地面上
    public float gravity = -9.81f;    // 重力值

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //这行被注释掉是因为测试剧情功能需要鼠标点击。
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

        // 获取输入轴
        float horizontal = Input.GetAxis("Horizontal"); // A 和 D 键
        float vertical = Input.GetAxis("Vertical");     // W 和 S 键


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
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // 旋转玩家水平方向
        transform.Rotate(Vector3.up * mouseX);

        // 控制摄像机的垂直旋转（限制旋转角度避免翻转）
        float verticalRotation = cameraTransform.localEulerAngles.x - mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -0f, 180f);

        cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
    }
}
