using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public BushFactory bushFactory;
    public RockFactory rockFactory;
    public TreeFactory treeFactory;
    public Button bushButton;
    public Button rockButton;
    public Button treeButton;

    private void Start()
    {
        bushButton.onClick.AddListener(SpawnBush);
        rockButton.onClick.AddListener(SpawnRock);
        treeButton.onClick.AddListener(SpawnTree);
    }

    private void SpawnBush()
    {
        bushFactory.CreateObject(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
    }

    private void SpawnRock()
    {
        rockFactory.CreateObject(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
    }

    private void SpawnTree()
    {
        treeFactory.CreateObject(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
    }
}