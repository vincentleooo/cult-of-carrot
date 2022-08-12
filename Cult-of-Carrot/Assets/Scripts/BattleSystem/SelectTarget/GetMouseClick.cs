using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseClick : MonoBehaviour
{
    public List<GameObject> currentlySelectedCharacter;
    public EnemyUnit GMCenemyUnit = null;
    // public EnemyUnit[] GMCenemyUnitList;
    public int enemyUnitIndex;
    
    private bool isSelectRingActive = false;
    private int enemyUnitListIndex = 0;
    private BattleSystemManager battleSystemManager;

    void Start()
    {
        currentlySelectedCharacter = new List<GameObject>();
        battleSystemManager = GetComponent<BattleSystemManager>();
    }

    void Update()
    {
        GetMouseInteraction();
    }

    void GetMouseInteraction()
    {
        RaycastHit2D hits = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hits.collider != null)
        {
            SelectTarget targetSelected = hits.transform.GetComponent<SelectTarget>();

            if (Input.GetMouseButtonDown(0))
            {
                SelectTarget(targetSelected);
                currentlySelectedCharacter.Add(targetSelected.gameObject);

                // Deactivate all other SelectRings when you click on a new character
                if (currentlySelectedCharacter.Count > 1)
                {
                    SetSelectRing(currentlySelectedCharacter[0], isSelectRingActive);
                    currentlySelectedCharacter[0].GetComponent<EnemyUnit>().isSelected = false; // Make it unselected
                    currentlySelectedCharacter.RemoveAt(0);
                }

                // Cast on Enemy
                if (targetSelected.gameObject.tag == "Enemy")
                {
                    GMCenemyUnit = targetSelected.GetComponent<EnemyUnit>();
                    GMCenemyUnit.isSelected = true;
                    enemyUnitListIndex = 0; // Reset it each time

                    foreach (EnemyUnit e in battleSystemManager.enemyUnits)
                    {
                        print("index: " + enemyUnitListIndex + "; " + e.isSelected);

                        if (e.isSelected)
                        {
                            enemyUnitIndex = enemyUnitListIndex; // Get the index of the selected enemy
                            print("enemyUnitIndex: " + enemyUnitIndex);
                        }

                        enemyUnitListIndex++;
                    }
                }
            }
        }
    }

    public void SelectTarget(SelectTarget target)
    {
        SetSelectRing(target.gameObject, !isSelectRingActive);
    }

    public void SetSelectRing(GameObject target, bool isActive)
    {
        target.transform.GetChild(4).gameObject.SetActive(isActive);
    }
}
