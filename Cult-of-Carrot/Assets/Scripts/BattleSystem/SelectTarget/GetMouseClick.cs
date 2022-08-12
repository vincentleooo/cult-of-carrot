using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseClick : MonoBehaviour
{
    private bool isSelectRingActive = false;
    private List<GameObject> currentlySelectedCharacter;

    void Start()
    {
        currentlySelectedCharacter = new List<GameObject>();
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
                GetComponent<GetMouseClick>().SelectTarget(targetSelected);
                currentlySelectedCharacter.Add(targetSelected.gameObject);

                // Deactivate all other SelectRings when you click on a new character
                if (currentlySelectedCharacter.Count > 1)
                {
                    SetSelectRing(currentlySelectedCharacter[0], isSelectRingActive);
                    currentlySelectedCharacter.RemoveAt(0);
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
