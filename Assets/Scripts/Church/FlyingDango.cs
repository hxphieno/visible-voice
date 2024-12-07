using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlyingDango: MonoBehaviour
{
    public Transform targetObject;
    public float radiusX=1;
    public float radiusY=1;
    public float speedXY=1;
    public float speedZ=1;
    public float second = 3;
    public bool circleDir=false;
    private float distance=5f;

    private Vector3 initPosition;
    private float delay = 0;
    private Rigidbody rb;
    private float rotationSpeed = 1.3f;//球的转速

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    float GetR(Vector3 a, Vector3 b) {
        float r = (float)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));

        return r;
    }
    //默认物体质量为1了，可在rigidbody里面更改
    
    void Update()
    {
        delay += Time.deltaTime;
        Debug.Log(GetR(transform.position, targetObject.position));
        if (GetR(transform.position, targetObject.position) <= distance)
        {
            Vector3 direction = transform.position - targetObject.position;
            float angle = Mathf.Atan2(direction.z, direction.x);  // 获取当前角度（x和z）

            // 因为有的是两个位置，所以用极坐标算了，不想再存一个角度变量了已经够多了……
            if(circleDir) {
                angle += rotationSpeed * Time.deltaTime;  
            }
            else
            {
                angle -= rotationSpeed * Time.deltaTime; 
            }
                float radius = new Vector2(direction.x, direction.z).magnitude;  // 计算在x-z平面的半径
            Vector3 newPosition = new Vector3(
                Mathf.Cos(angle) * radius,
                transform.position.y,  
                Mathf.Sin(angle) * radius
            ) + new Vector3(targetObject.position.x, 0, targetObject.position.z);
            transform.position = newPosition;
        }
        else if (delay > second)
        {
            transform.position = new Vector3(initPosition.x + radiusX * Mathf.Cos((delay-second) * speedXY), initPosition.y + radiusY * Mathf.Sin((delay - second) * speedXY), transform.position.z - speedZ * Time.deltaTime);
        }
    }
}
