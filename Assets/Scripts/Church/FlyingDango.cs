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
    
    public float distance=10;

    private Vector3 initPosition;
    private float delay = 0;
    private Rigidbody rb;
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
        if (GetR(transform.position, targetObject.position)<distance) {
            ///TODO:将球移动到某个位置后开始做圆周运动，最好的解决方案是AddForce但是不想写物理公式
            ///退而求其次可以试着将物体的位置和速度都移动到一个指定位置再开始移动，但是需要设置的变量就会较多。

        }
        else if (delay > second)
        {
            transform.position = new Vector3(initPosition.x + radiusX * Mathf.Cos((delay-second) * speedXY), initPosition.y + radiusY * Mathf.Sin((delay - second) * speedXY), transform.position.z - speedZ * Time.deltaTime);
        }
    }
}
