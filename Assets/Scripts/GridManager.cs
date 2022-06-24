using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private int _width;
    [SerializeField] private float _offset;

    [SerializeField] private Transform _gridTransform;
    [SerializeField] private Transform _squadTransform;
    [SerializeField] private GridSection _section;

    public Action<Character> OnCharacterSpawned;

    public static GridManager Instance;

    public List<Character> characterList;

    private void OnEnable() => GridSection.OnSectionSelected += SpawnCharacter;
    private void OnDisable() => GridSection.OnSectionSelected -= SpawnCharacter;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnGrid()
    {
        foreach (var point in NextCoordinates())
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

    private List<Vector3> CalculateGrid()
    {
        var positions = new List<Vector3>();

        for (int i = 0; i <= (int)((_size - 1) / _width); i++)
        {
            for (int j = 0; j < _width; j++)
            {
                positions.Add(new Vector3((transform.position.x + j - Mathf.Ceil(_width / 2)) * _offset, 0, (transform.position.z - i) * _offset - _offset));
            }
        }

        return positions;
    }

    public IEnumerable<Vector3> NextCoordinates()
    {
        foreach (var position in CalculateGrid())
        {
            yield return position;
        }
    }
}
