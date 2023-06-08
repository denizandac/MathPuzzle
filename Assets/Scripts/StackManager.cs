using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _spawnPoint;
    private List<NumberCube> _numberCubes = new List<NumberCube>();
    private List<int> expectedResults = new List<int>();
    private void Start()
    {
        expectedResults = new List<int>(){1,2,3,4,5,6,7};
        CreateRandomCubes();
    }
    private void CreateRandomCubes()
    {
        Vector3 spawnPosition = _spawnPoint.position;
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
            go.transform.SetParent(_spawnPoint);
            NumberCube cube = go.GetComponent<NumberCube>();
            cube.SetCubeValue(expectedResults[i]);
            _numberCubes.Add(cube);
            spawnPosition += new Vector3(0,1.5f,0);
        }
    }

    public void SearchInCubes(List<int> comboList)
    {
        int DropIndex = -1;
        for (int i = 0; i < _numberCubes.Count; i++)
        {
            if (_numberCubes[i].GetCubeValue() == comboList[0])
            {
                _numberCubes[i].gameObject.SetActive(false);
                DropIndex = i;
                _numberCubes.RemoveAt(i);
                break;
                //broken cube animation 
            }
        }
        // List<Transform> animationList = new List<Transform>();

        // if(DropIndex != -1)
        // {
        //     for (int i = DropIndex; i < _numberCubes.Count; i++)
        //     {
        //         animationList.Add(_numberCubes[i].transform);
        //     }
        // }
        // if (DropIndex == _numberCubes.Count)
        // {
        //     _animationManager.CharacterJumpAnim();
        // }
        // if (animationList.Count != 0)
        // {
        //     _animationManager.CubeDrop(animationList);
        // }
        // if (_numberCubes.Count == 0)
        // {
        //     _animationManager.CharacterDieAnim();
        // }
    }
}