using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneButton : MonoBehaviour
{
	// to link to different cutscenes upon button press
    public Button yourButton;
    public GameObject video;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		video.gameObject.SetActive(true);
	}
}
