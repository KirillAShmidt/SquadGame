using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridData _data;
    [SerializeField] private Transform _gridTransform;
    [SerializeField] private Transform _squadTransform;
    [SerializeField] private GridSection _section;

    private GridGenerator _generator;

    public Action<Character> OnCharacterSpawned;

    public static GridManager Instance;

    public List<Character> characterList;

    private void OnEnable() => GridSection.OnSectionSelected += SpawnCharacter;
    private void OnDisable() => GridSection.OnSectionSelected -= SpawnCharacter;

    private void Awake()
    {
        Instance = this;
        _generator = new GridGenerator(_data, transform);
        SpawnGrid();
    }

    public void SpawnGrid()
    {
        foreach (var point in _generator.NextCoordinates())
        {
            var clone = Instantiate(_section);
            clone.transform.parent = _gridTransform;
            clone.transform.position = point;
        }
    }

    public void SpawnCharacter(GridSection section)
    {
        var character = Instantiate(CharacterSelecter.Instance.ActiveCharacter, section.transform.position, Quaternion.identity, _squadTransform);

        character.GridPosition = section.transform;
        section.Character = character;

        characterList.Add(character);

        OnCharacterSpawned?.Invoke(character);
    }
}
