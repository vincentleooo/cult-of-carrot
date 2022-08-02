using UnityEngine;
using UnityEngine.SceneManagement;


public class Escape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("NodeSystem");
        }
        
    }
}
