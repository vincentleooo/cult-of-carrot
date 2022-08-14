using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseClick : MonoBehaviour
{
    public List<GameObject> currentlySelectedCharacter;
    public int enemyUnitIndex;
    
    private bool isSelectRingActive = false;
    private int enemyUnitListIndex = 0;
    private BattleSystemManager battleSystemManager;
    private EnemyUnit GMCenemyUnit = null;
    private PlayerUnit GMCplayerUnit = null;


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
                SetSelectRing(targetSelected.gameObject, !isSelectRingActive);
                currentlySelectedCharacter.Add(targetSelected.gameObject);

                // Deactivate all other SelectRings when you click on a new character
                if (currentlySelectedCharacter.Count > 1)
                {
                    SetSelectRing(currentlySelectedCharacter[0], isSelectRingActive);

                    // Check if it is Enemy
                    if (currentlySelectedCharacter[0].tag == "Enemy")
                    {
                        currentlySelectedCharacter[0].GetComponent<EnemyUnit>().isSelected = false; // Make it unselected
                    }

                    // Check if it is Player
                    else if (currentlySelectedCharacter[0].tag == "Player")
                    {
                        currentlySelectedCharacter[0].GetComponent<PlayerUnit>().isSelected = false; // Make it unselected
                    }

                    currentlySelectedCharacter.RemoveAt(0);
                }

                // Click on Enemy
                if (targetSelected.gameObject.tag == "Enemy")
                {
                    GMCenemyUnit = targetSelected.GetComponent<EnemyUnit>();
                    GMCenemyUnit.isSelected = true;
                    enemyUnitListIndex = 0; // Reset it each time

                    foreach (EnemyUnit e in battleSystemManager.enemyUnits)
                    {
                        // print("index: " + enemyUnitListIndex + "; " + e.isSelected);

                        if (e.isSelected)
                        {
                            enemyUnitIndex = enemyUnitListIndex; // Get the index of the selected enemy
                            print("selected enemy index: " + enemyUnitIndex);
                        }

                        enemyUnitListIndex++;
                    }
                }

                // Click on Player
                else if (targetSelected.gameObject.tag == "Player")
                {
                    GMCplayerUnit = targetSelected.GetComponent<PlayerUnit>();
                    GMCplayerUnit.isSelected = true;

                    // TODO: finish this
                }
            }
        }
    }

    public void SetSelectRing(GameObject target, bool isActive)
    {
        target.transform.GetChild(3).gameObject.SetActive(isActive);
    }
}
