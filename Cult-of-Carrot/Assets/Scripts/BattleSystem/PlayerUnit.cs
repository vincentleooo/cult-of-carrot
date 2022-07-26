using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public int maxFaith = 100;
    public int currentFaith;
    public FaithBar faithBar;

    // Start is called before the first frame update
    void Start()
    {
        currentFaith = maxFaith;
        faithBar.SetMaxFaith(maxFaith);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentFaith -= damage;
        faithBar.SetFaith(currentFaith);
    }
}
