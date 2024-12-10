using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPlotTrigger : MonoBehaviour
{

    public GameObject plotManager;
    public string targetTag = "Player";
    public GameObject objTowatingF;

    private bool isTriggered=false;
    private int times = 0;
    private GameObject waitingFPanel;

    private Color highlightColor = Color.yellow;
    ///TODO��Ҫ��һ��������ʾ�Ͱ�F�Ի��Ĵ��ڿ��ӻ������ܵ���ʵ����

    ///��ͬ����һ��Trigger���ǣ�����Ƿ�ǿ��Trigger����F�����Ի�
    ///����ű�������һ����Ҫ����Ĳ��������ڣ�����˵һ������ͬʱ�ڶ������Ĵ��������п����ظ������� <summary>
    /// ����������ú�����ÿ��Object��Collider������дһ�²�������
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.F))
        {          
            DialogManager dialogManager = plotManager.GetComponent<DialogManager>();
            if (dialogManager != null && times == 0){
                dialogManager.isActive = true;
                times++;
            }           
        }
    }
    void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        
    }
    void OnTriggerExit(Collider other)
    {   
        isTriggered = false;
    }
}
