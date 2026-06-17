using UnityEngine;

public class Enemy_Health : Entity_Health {

    private Enemy enemy;               // private Enemy enemy => GetComponent<Enemy>();
                                       // Bu þekilde de componenti alýnabilir ama her çaðýrýlan enemy için tekrar oluþturur birkaç kez kullanýma uygundur. 

    private void Start() {
         
        enemy = GetComponent<Enemy>();
    }

    public override bool TakeDamage(float damage, Transform damageDealer) {

        bool wasHit = base.TakeDamage(damage, damageDealer);

        if (wasHit == false) {
            return false;
        }

        if (damageDealer.CompareTag("Player")) {            //if (damageDealer.GetComponent<Player> != null) ayný if koþulunun faklý bir þekilde yazýmý
            enemy.TryEnterBattleState(damageDealer);
        }

        return true;
    }
}
