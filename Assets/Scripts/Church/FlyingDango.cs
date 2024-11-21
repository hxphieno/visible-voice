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
    //Ĭ����������Ϊ1�ˣ�����rigidbody�������
    
    void Update()
    {
        delay += Time.deltaTime;
        if (GetR(transform.position, targetObject.position)<distance) {
            ///TODO:�����ƶ���ĳ��λ�ú�ʼ��Բ���˶�����õĽ��������AddForce���ǲ���д����ʽ
            ///�˶�����ο������Ž������λ�ú��ٶȶ��ƶ���һ��ָ��λ���ٿ�ʼ�ƶ���������Ҫ���õı����ͻ�϶ࡣ

        }
        else if (delay > second)
        {
            transform.position = new Vector3(initPosition.x + radiusX * Mathf.Cos((delay-second) * speedXY), initPosition.y + radiusY * Mathf.Sin((delay - second) * speedXY), transform.position.z - speedZ * Time.deltaTime);
        }
    }
}
