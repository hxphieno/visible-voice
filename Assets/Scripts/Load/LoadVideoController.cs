using System.Collections; // 确保包含这一行
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool gameLoaded = false;

    void Start()
    {
        // 合并所有初始化逻辑
        Debug.Log("load begin");

        // 确保视频播放器已经设置好
        if (videoPlayer != null)
        {
            videoPlayer.Play(); // 开始播放视频
        }

        // 开始异步加载主游戏场景
        StartCoroutine(LoadMainGameScene());
    }

    IEnumerator LoadMainGameScene()
    {
        yield return new WaitForSeconds(10f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");

        // 等待场景加载完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 场景加载完成后停止视频播放
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        gameLoaded = true;
    }

    void Update()
    {
        // 如果游戏已加载且视频仍在播放，则停止视频播放
        if (gameLoaded && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }
}
