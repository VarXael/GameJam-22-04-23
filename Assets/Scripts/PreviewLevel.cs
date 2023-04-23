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
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (previewMode == false)
        {
            if(currentLevelInstance == null)
            {
                foreach (Transform child in transform)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
            else
            {
                currentLevelInstance.SetActive(false);
            }
        }
        else if(previewMode == true && currentLevelInstance == null)
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
            SpawnLevelFromIndex(levelIndex);
        }
        else if(previewMode == true && currentLevelInstance != null)
        {
            currentLevelInstance.SetActive(true);
        }
        if (levelIndex != currentLevelIndex)
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
