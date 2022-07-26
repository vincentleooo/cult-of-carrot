using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
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

    public void TakeDamage(int damage)
    {
        currentFaith -= damage;
        faithBar.SetFaith(currentFaith);
    }

    public int Attack() {
        return 5;
    }
}
