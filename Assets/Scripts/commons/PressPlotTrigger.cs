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
    ///TODO：要做一下区域提示和按F对话的窗口可视化，功能倒是实现了

    ///不同于另一个Trigger的是，这个是非强制Trigger，按F触发对话
    ///这个脚本后续有一个需要处理的并发点在于，假如说一个物体同时在多个物体的触发区内有可能重复触发。 <summary>
    /// 解决方法：好好设置每个Object的Collider，或者写一下并发处理。
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
