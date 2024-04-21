using Models;
using UnityEngine;

/// <summary>
/// Скрипт для проверки столкновения двух объектов
/// </summary>
public class HeroGo : MonoBehaviour {
    private Hero Hero { get; set; }

    private void Start() {
        Hero = new Hero("Main Hero");

        GameManager.Instance.AddHero(Hero);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (GameManager.Instance.IsOver) {
            return;
        }

        // Если Герой столкнулся с противником
        if (other.CompareTag("Enemy")) {
            GameManager.Instance.EnemyWasBumped();
        }

        // Если Герой столкнулся с Наградой
        if (other.CompareTag("Award")) {
            var award = other.GetComponent<AwardGo>().Award;

            Hero.TakeAward(award);
        }
    }
}