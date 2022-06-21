using UnityEngine;
using System;

public class SelectableObject : MonoBehaviour
{
    [SerializeField] private bool isSingleClicked;

    public Action OnSelected;

    private void Update()
    {
        if (!isSingleClicked && Input.GetMouseButtonDown(0))
        {
            Select();
        }
        else if(isSingleClicked && Input.GetMouseButton(0))
        {
            Select();
        }
    }

    private void Select()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo))
        {
            var selectableObject = hitInfo.collider.GetComponent<SelectableObject>();

            if (selectableObject == this)
            {
                OnSelected?.Invoke();
            }
        }
    }
}
