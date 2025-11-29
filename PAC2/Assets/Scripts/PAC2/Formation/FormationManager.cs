using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//[ExecuteInEditMode]
public class FormationManager : MonoBehaviour
{
    public enum FormationMode
    {
        Grid,
        Column,
        Circle,
        Triangle
    }

    [Header("Global Config")]
    [SerializeField]private FormationMode currentFormation = FormationMode.Circle;
    [SerializeField]private Transform leader;
    [SerializeField]private float spread = 2f;
    [SerializeField, UnityEngine.Range(0, 20)] private int numberOfFollowers = 1;
    [SerializeField] private GameObject followerTemplate;
    

    [Header("Specific Config")]
    [SerializeField, UnityEngine.Range(0, 3)] private float columnXSpread = 0;

    [SerializeField, UnityEngine.Range(1, 5)] private int gridColumns = 4;


    private List<FormationFollower> units;
    /// <summary>
    /// Used for column mode, to add some horizontal spread
    /// </summary>
    private List<float> xSpreadValues;
    void Start()
    {
        units = new List<FormationFollower>();
        xSpreadValues = new List<float>();
        foreach(var unit in this.GetComponentsInChildren<FormationFollower>())
            Destroy(unit.gameObject);

        UpdateFollowersList();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFollowersList();
        if(currentFormation != FormationMode.Column && xSpreadValues.Count() > 0)
            xSpreadValues.Clear();
        for (int i = 0; i < units.Count; i++)
        {
            units[i].SetTarget(leader, GetPosition(i));
        }
    }

    private void UpdateFollowersList()
    {
        if(units.Count > numberOfFollowers)
        {
            for(int i = units.Count - 1; i >= numberOfFollowers; i--)
            {
                DestroyImmediate(units[i].gameObject);
                units.RemoveAt(i);
            }    
        }
        else if (units.Count < numberOfFollowers)
        {
            for (int i = units.Count; i < numberOfFollowers; i++)
            {
                GameObject follower = GameObject.Instantiate(followerTemplate, leader.TransformPoint(GetPosition(i)), leader.rotation, this.transform);
                
                units.Add(follower.GetComponent<FormationFollower>());
            }
        }
    }

    #region Positions
    private Vector3 GetPosition(int index)
    {
        Vector3 pos = Vector3.zero;

        switch (currentFormation)
        {
            case FormationMode.Grid:
                pos = GetGridPosition(index);
                break;
            case FormationMode.Column:
                pos = GetColumnPosition(index);
                break;
            case FormationMode.Circle:
                pos = GetCirclePosition(index);
                break;
            case FormationMode.Triangle:
                pos = GetTrianglePosition(index);
                break;
        }
        return pos;
    }

    private Vector3 GetGridPosition(int index)
    {
        int row = index / gridColumns;
        int col = index % gridColumns;

        float xPos = (col - (gridColumns - 1) / 2f) * spread;
        float zPos = -((row + 1) * spread);

        return new Vector3(xPos, 0, zPos);
    }

    private Vector3 GetColumnPosition(int index)
    {
        float zPos = spread + spread * index;
        while (index >= xSpreadValues.Count())
            xSpreadValues.Add(Random.Range(-columnXSpread, columnXSpread));
        return new Vector3(xSpreadValues[index], 0, -zPos);
    }

    /// <summary>
    /// Returns position using x = r * cos(a), z = r * sin(a)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private Vector3 GetCirclePosition(int index)
    {
        float radius = Mathf.Max(spread * units.Count / (2 * Mathf.PI), spread);
        float angle = index * (360f / units.Count) * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
    }

    private Vector3 GetTrianglePosition(int index)
    {
        int currentRow = 0;
        int unitsInRow = 1;
        int currentCount = 0;

        while (currentCount + unitsInRow <= index)
        {
            currentCount += unitsInRow;
            currentRow++;
            unitsInRow++;
        }

        int posInRow = index - currentCount;


        return new Vector3((posInRow - (unitsInRow - 1) / 2f) * spread, 0, -(currentRow + 1) * spread);
    }
    #endregion
}
