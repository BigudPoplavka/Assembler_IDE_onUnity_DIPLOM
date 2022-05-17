using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConsoleContentManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _allChildren;
    int i = 0;
    void Start()
    {
       
    }

    public void GetContentObjects()
    {
        _allChildren = new GameObject[transform.childCount];

        foreach (Transform child in transform)
        {
            Debug.Log(child.gameObject.name);
            _allChildren[i] = child.gameObject;
            i += 1;
        }
    }

    public void ClearObjects()
    {
        foreach (GameObject child in _allChildren)
            Destroy(transform.GetChild(0));

        _allChildren = new GameObject[] { };
    }
}
