using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartNewGame()
    {
        Debug.Log("New Game");
        SceneManager.LoadScene("Church");
        StartCoroutine(WaitForSceneLoad());
    }

    // Update is called once per frame
    public void ContinueGame()
    {
        Debug.Log("Continue Game");
        SceneManager.LoadScene("Church");
        StartCoroutine(WaitForSceneLoad());
    }

    IEnumerator WaitForSceneLoad()
    {
        // 等待场景加载完成
        while (SceneManager.GetActiveScene().name != "ChurchScene")
        {
            yield return null;
        }

        // 场景加载完成后查找player对象
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 houseInteriorPosition = new Vector3(0f, 1.64f, 30f); // 根据房子内部的实际位置调整坐标
            //Quaternion houseInteriorRotation = Quaternion.Euler(0f, 180f, 0f);
            player.transform.position = houseInteriorPosition;
            //player.transform.rotation = houseInteriorRotation;
        }
        else
        {
            Debug.LogError("Player object not found in Church scene.");
        }
    }
}

