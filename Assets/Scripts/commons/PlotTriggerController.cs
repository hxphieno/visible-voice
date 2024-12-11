using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotTriggerController : MonoBehaviour
{
    public string targetTag = "Player";
    public GameObject plotManager;

    private int times = 0;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
        if (other.CompareTag(targetTag))  
        {
            DialogManager dialogManager = plotManager.GetComponent<DialogManager>();
            if (dialogManager != null&&times==0)
            {
                dialogManager.isActive=true;
                times++;
            }
        }
    }
}
