using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LinkCutsceneToOtherScenes : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneName;

    void Start()
    {
        videoPlayer.loopPointReached += MoveToOtherScene;
    }
 
    void MoveToOtherScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneName);
    }
}
