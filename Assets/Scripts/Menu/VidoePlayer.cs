using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Canvas buttonsCanvas;

    void Start()
    {
        Debug.Log("begin");
        // 确保Canvas起始时隐藏
        buttonsCanvas.gameObject.SetActive(false);

        // 监听视频播放完毕事件
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        Debug.Log("video done");
        // 视频结束后显示Canvas
        buttonsCanvas.gameObject.SetActive(true);
    }
}
