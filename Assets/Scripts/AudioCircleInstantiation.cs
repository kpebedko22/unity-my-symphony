using UnityEngine;

public class AudioCircleInstantiation : MonoBehaviour {

	public GameObject audioCirclePrefab;

	public GameObject enemyContainer;

	void Start () {
		for (int i = 0; i < 8; i++) {
			// создаем объект 
			GameObject instanceAudioCircle = (GameObject)Instantiate(audioCirclePrefab);

			instanceAudioCircle.GetComponent<ParamCircle>().band = i;
			instanceAudioCircle.GetComponent<ParamCircle>().enemyContainer = enemyContainer;

			instanceAudioCircle.GetComponent<CircleMovement>().angle = 0.785f * (i + 1);
			instanceAudioCircle.GetComponent<CircleMovement>().center = this.transform;

			// задаем родителя
			instanceAudioCircle.transform.SetParent(this.transform);
			instanceAudioCircle.name = "audioCircle" + i;
		}
	}
}
