using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoButton : MonoBehaviour
{
    // for different cutscene options
    
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public GameObject Button1;
    public GameObject Button2;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadPanel;
    }

    public void LoadPanel(VideoPlayer vp)
    {
        Button1.gameObject.SetActive(true);
        Button2.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
