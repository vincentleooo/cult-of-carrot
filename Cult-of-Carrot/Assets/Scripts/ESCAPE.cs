using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCAPE : MonoBehaviour
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
