using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;        
    public float rotationSpeed = 3f;   
    public float jumpForce = 5f;       
    public Transform cameraTransform;  
    public float smoothRotationSpeed = 5f; // 平滑转动的速度

    // 固定点序列定义路线
    public Vector3[] waypoints;
    public float waypointSpeed = 5f; // 沿路线移动的速度
    public float decelerationDistance = 1f; // 开始减速的距离
    public float decelerationFactor = 0.5f; // 减速因子（0到1之间，越小减速越明显）
    public float accelerationTime = 1f; // 出发时加速的时间

    private int currentWaypointIndex = 0;
    private float currentWaypointSpeed = 0f; // 当前速度
    private float accelerationTimer = 0f; // 加速计时器

    // 视觉焦点
    public Transform focusTarget; // 目标点
    public float focusSpeed = 5f; // 视觉焦点的转向速度

    private CharacterController characterController; 
    private Vector3 velocity;         
    private bool isGrounded;        
    public float gravity = -9.81f;    

    private float targetRotation; // 目标旋转角度（X轴）

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // 初始化目标旋转角度
        targetRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        MovePlayer();      
        RotatePlayer();    
        MoveAlongWaypoints(); // 沿固定路线移动
        SmoothRotateCamera(); // 平滑转动视角
        FocusOnTarget(); // 朝向视觉焦点
    }

    void MovePlayer()
    {
        isGrounded = characterController.isGrounded;

        float horizontal = Input.GetAxis("Horizontal"); // A 和 D 键
        float vertical = Input.GetAxis("Vertical");     // W 和 S 键

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // 调整移动速度，按下 Shift 时速度加倍
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
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

        // 旋转玩家的水平方向
        transform.Rotate(Vector3.up * mouseX);

        // 更新目标旋转角度
        targetRotation = transform.eulerAngles.y;
    }

    void MoveAlongWaypoints()
    {
        if (waypoints.Length == 0) return; // 如果没有定义路线点，直接返回

        Vector3 targetPosition = waypoints[currentWaypointIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 检测与目标点的距离
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // 加速逻辑
        if (accelerationTimer < accelerationTime)
        {
            currentWaypointSpeed = Mathf.Lerp(0f, waypointSpeed, accelerationTimer / accelerationTime);
            accelerationTimer += Time.deltaTime;
        }
        else
        {
            currentWaypointSpeed = waypointSpeed;
        }

        // 减速逻辑
        if (distanceToTarget < decelerationDistance)
        {
            currentWaypointSpeed *= decelerationFactor; // 应用减速因子
        }

        characterController.Move(direction * currentWaypointSpeed * Time.deltaTime);

        // 检查是否到达当前路线点
        if (distanceToTarget < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // 循环回到第一个点
            }

            // 重置加速计时器
            accelerationTimer = 0f;
        }
    }

    void SmoothRotateCamera()
    {
        // 平滑转动视角（仅X轴）
        float mouseX = Input.GetAxis("Mouse X") * smoothRotationSpeed * Time.deltaTime;
        float currentRotation = cameraTransform.localEulerAngles.x;

        // 使用 SmoothDamp 平滑过渡到目标角度
        float targetRotation = currentRotation + mouseX;
        cameraTransform.localEulerAngles = new Vector3(Mathf.SmoothDamp(currentRotation, targetRotation, ref velocity.x, 0.1f), 0, 0);
    }

    void FocusOnTarget()
    {
        if (focusTarget == null) return; // 如果没有目标点，直接返回

        // 计算朝向目标点的方向
        Vector3 direction = (focusTarget.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 平滑旋转到目标方向
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, focusSpeed * Time.deltaTime);
    }
}