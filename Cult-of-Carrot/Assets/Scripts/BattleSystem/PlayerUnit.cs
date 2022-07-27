using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public int maxFaith = 100;
    public int currentFaith;
    private FaithBar faithBar;

    // Start is called before the first frame update
    void Start()
    {
        faithBar = (GetComponentInChildren(typeof(FaithBar))) as FaithBar;
        currentFaith = maxFaith;
        faithBar.SetMaxFaith(maxFaith);
    }

    // unique to each enemy type
    public void TakeDamage(int damage)
    {
        currentFaith -= damage;
        faithBar.SetFaith(currentFaith);
    }

}
