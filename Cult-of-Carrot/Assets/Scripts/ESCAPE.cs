using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("NodeSystem");
        }
        
    }
}
