using Models;
using UnityEngine;

/// <summary>
/// Скрипт для проверки столкновения двух объектов
/// </summary>
public class HeroGo : MonoBehaviour {
    public Hero Hero { get; set; }

    private void Start() {
        Hero = new Hero();
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
            var awardGo = other.GetComponent<AwardGo>();
            var award = awardGo.Award;

            if (award.IsGiven) {
                Debug.Log("Award is already taken!");
            }
            else {
                Debug.Log("Award touched!");
                Hero.TakeAward(award);
                awardGo.DestroyMe();
            }
        }
    }
}