using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerGenerator : MonoBehaviour
{
    [SerializeField] private int _levelCount;
    [SerializeField] private float _additionalScale;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private StartPlatform _startPlatform;
    [SerializeField] private FinishPlatform _finishPlatform;
    [SerializeField] private Cylinder _cylinderPrefab;

    private float additionalSectionScale = 0.5f;

    private float TowerHeight => (_levelCount + _additionalScale)/2f + additionalSectionScale;

    void Awake()
    {
        BuildTower();
    }

    private void BuildTower()
    {
        var cylinder = Instantiate(_cylinderPrefab, transform);
        cylinder.transform.localScale = new Vector3(1, TowerHeight, 1);
        var cylinderPosition = cylinder.transform.position;
        cylinderPosition.y = -cylinder.transform.localScale.y + _additionalScale;
        cylinder.transform.localPosition = cylinderPosition;
        BuildPlatforms();
    }

    private void BuildPlatforms()
    {
        float startPosition = 0;
        float finishPosition = 0;
        SpawnPlatform(_startPlatform, --startPosition);
        for (float i = 0; i <= _levelCount; i++) {
            var rotation = Vector3.up * Random.Range(0, 360);
            SpawnPlatform(_platformPrefab, i, rotation);
            finishPosition = i;
        }
        SpawnPlatform(_finishPlatform, ++finishPosition);
    }

    private Platform BuildPlatform(Platform platformPrefab, float yOffset, Quaternion quaternion)
    {
        var spawmPosition = transform.position;
        spawmPosition.y -= yOffset;
        var platform = Instantiate(platformPrefab, spawmPosition, quaternion);
        platform.transform.SetParent(transform);
        return platform;
    }

    private Platform SpawnPlatform(Platform platformPrefab, float yOffset)
    {
        return BuildPlatform(platformPrefab, yOffset, Quaternion.identity);
    }

    private Platform SpawnPlatform(Platform platformPrefab, float yOffset, Vector3 rotation)
    {
        return BuildPlatform(platformPrefab, yOffset, Quaternion.Euler(rotation));
    }

}
