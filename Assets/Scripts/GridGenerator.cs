using System.Collections.Generic;
using UnityEngine;

public class GridGenerator
{
    public GridData data;
    public Transform transform;

    private List<Vector3> _coordinates;

    public GridGenerator(GridData data, Transform transform)
    {
        this.data = data;
        this.transform = transform;

        _coordinates = CalculateGrid();
    }

    private List<Vector3> CalculateGrid()
    {
        var positions = new List<Vector3>();

        for (int i = 0; i <= (int)((data.size - 1) / data.width); i++)
        {
            for (int j = 0; j < data.width; j++)
            {
                 positions.Add(new Vector3((transform.position.x + j - Mathf.Ceil(data.width / 2)) * data.offset, 0, (transform.position.z - i) * data.offset - data.offset));
            }
        }

        return positions;
    }

    public IEnumerable<Vector3> NextCoordinates()
    {
        foreach (var position in _coordinates)
        {
            yield return position;
        }
    }
}