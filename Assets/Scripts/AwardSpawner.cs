using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardSpawner : MonoBehaviour {
    [SerializeField] private GameObject _prefab;

    private GameController _gameController;

    private IEnumerator _spawnCoroutine;

    private void Start() {
        _gameController = Camera.main.GetComponent<GameController>();

        _spawnCoroutine = SpawnNextAward();

        StartCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnNextAward() {
        Debug.Log("spawn next award coroutine");
        
        while (!GameController.gameIsOver) {
            Instantiate(
                _prefab,
                new Vector2(Random.Range(-70.0f, 70.0f), Random.Range(-70.0f, 70.0f)),
                Quaternion.identity,
                transform
            );
            
            yield return new WaitForSeconds(5);
        }
    }
}