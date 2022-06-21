using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelecter : MonoBehaviour
{
    public Character ActiveCharacter { get; private set; }

    public static CharacterSelecter Instance;

    private void Start()
    {
        Instance = this;
    }

    public void SelectUnit(Character character)
    {
        ActiveCharacter = character;
    }
}
