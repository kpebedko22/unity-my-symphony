using System.Collections;
using Models;
using UnityEngine;

public class AwardGo : MonoBehaviour {
    private const float IdleTime = 5.0f;

    public Award Award { get; private set; }

    private IEnumerator _deleteCoroutine;

    private void Start() {
        _deleteCoroutine = DeleteObject(gameObject, IdleTime);
        StartCoroutine(_deleteCoroutine);

        Award = new Award(5);
        Award.GiveOutUpdate += delegate
        {
            StopCoroutine(_deleteCoroutine);
            Destroy(gameObject);
        };
    }

    IEnumerator DeleteObject(GameObject obj, float t) {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }
}