using System.Collections;
using UnityEngine;

public class AwardGo : MonoBehaviour {
    private const float IdleTime = 5.0f;

    public Award Award { get; set; }

    private IEnumerator _deleteCoroutine;

    private void Start() {
        _deleteCoroutine = DeleteObject(gameObject, IdleTime);
        StartCoroutine(_deleteCoroutine);

        Award = new Award(5);
    }

    IEnumerator DeleteObject(GameObject obj, float t) {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }

    public void DestroyMe() {
        StopCoroutine(_deleteCoroutine);
        Destroy(gameObject);
    }
}