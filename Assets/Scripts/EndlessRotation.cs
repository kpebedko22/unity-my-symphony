using UnityEngine;

public class EndlessRotation : MonoBehaviour {
	/*
	 * Скрипт бесконечного вращения объекта вокруг собственного центра
	 */

	// угол на который выполняется поворот объекта
	[SerializeField]
	float angle = 3f;
	
	void Update () {
		// поворачиваем объект на угол angle по оси Z
		transform.Rotate(new Vector3(0, 0, angle));
	}
}
