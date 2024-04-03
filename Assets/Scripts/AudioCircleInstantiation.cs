using UnityEngine;

public class AudioCircleInstantiation : MonoBehaviour {
    public GameObject audioCirclePrefab;

    public GameObject enemyContainer;

    private void Start() {
        for (var i = 0; i < 8; i++) {
            GameObject instanceAudioCircle = Instantiate(audioCirclePrefab, transform);

            instanceAudioCircle.GetComponent<ParamCircle>().band = i;
            instanceAudioCircle.GetComponent<ParamCircle>().enemyContainer = enemyContainer;

            instanceAudioCircle.GetComponent<CircleMovement>().angle = 0.785f * (i + 1);
            instanceAudioCircle.GetComponent<CircleMovement>().center = transform;

            instanceAudioCircle.name = "audioCircle" + i;
        }
    }
}