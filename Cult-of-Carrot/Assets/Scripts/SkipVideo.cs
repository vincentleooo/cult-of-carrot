using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SkipVideo : MonoBehaviour
{
    public Button yourButton;
    public VideoPlayer vp;
    public float skipTime;
    

    // Start is called before the first frame update
    void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		vp.time += skipTime;
        // yourButton.gameObject.SetActive(false);
	}
}
