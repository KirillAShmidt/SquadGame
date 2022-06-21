using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer), typeof(SelectableObject))]
public class GridSection : MonoBehaviour
{
    private SelectableObject selectable;
    private bool isTaken;

    public Character Character { get; set; }

    public static Action<GridSection> OnSectionSelected;

    private void Start()
    {
        selectable = GetComponent<SelectableObject>();
        selectable.OnSelected += TakeSection;
    }

    private void TakeSection()
    {
        if (!isTaken && CharacterSelecter.Instance.ActiveCharacter != null)
        {
            OnSectionSelected?.Invoke(this);
            gameObject.SetActive(false);
            isTaken = true;
            Character.OnActorDestroyed += RestoreSection;
        }
    }

    private void RestoreSection()
    {
        if (isTaken)
        {
            gameObject.SetActive(true);
            isTaken = false;
        }
    }
}