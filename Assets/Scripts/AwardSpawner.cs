using System.Collections;
using Models;
using UnityEngine;

public class AwardSpawner : MonoBehaviour {
    [SerializeField] private GameObject prefab;

    private IEnumerator _spawnCoroutine;

    private void Start() {
        _spawnCoroutine = SpawnNextAward();

        StartCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnNextAward() {
        while (!GameManager.Instance.IsOver) {
            Instantiate(
                prefab,
                new Vector2(Random.Range(-70.0f, 70.0f), Random.Range(-70.0f, 70.0f)),
                Quaternion.identity,
                transform
            );

            yield return new WaitForSeconds(5);
        }
    }
}