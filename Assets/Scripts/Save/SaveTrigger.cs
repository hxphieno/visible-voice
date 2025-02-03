using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    public SaveManager saveManager;  // 引用存档管理器
    public GameObject player;        // 引用玩家游戏对象

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
       
            SaveData data = new SaveData();
            data.scene = 1;
            saveManager.SaveGame(data);
            Debug.Log("Game Saved at Trigger 1");
        
    }
}
