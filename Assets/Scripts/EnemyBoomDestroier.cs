using UnityEngine;
using UnityEngine.UI;

public class EnemyBoomDestroier : MonoBehaviour {
	/*
	 * Скрипт для уничтожения объекта Взрыв
	 */
	
	void Update () {
		// для анимации Взрыва проверяем
		// если альфа-канал равен нулю, то объект прозрачный и его можно удалить со сцены
		if (transform.GetComponent<Image>().color.a == 0) 
			Destroy(transform.gameObject);
	}
}
