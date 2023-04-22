using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PreviewLevel : MonoBehaviour
{
    [Header("Level Settings")]
    public int levelIndex;
    public bool previewMode;
    private int currentLevelIndex;
    private GameObject currentLevelInstance;

    private void Start()
    {
        if (Application.isPlaying)
        {
            Destroy(gameObject);
        }
        else
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (previewMode == false)
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        else if(previewMode == true)
        {
            SpawnLevelFromIndex(levelIndex);
        }
    }

    private void SpawnLevelFromIndex(int levelIndex)
    {
        if (levelIndex > LevelManager.singleton.levelsPrefab.Length - 1)
        {
            throw new ("No Level Found!");
        }
        if (levelIndex == currentLevelIndex) return;
        DestroyImmediate(currentLevelInstance);
        currentLevelInstance = PrefabUtility.InstantiatePrefab(LevelManager.singleton.levelsPrefab[levelIndex].gameObject) as GameObject;
        currentLevelInstance.transform.parent = transform;
        currentLevelIndex = levelIndex;
    }
}
