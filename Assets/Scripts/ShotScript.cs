using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotType {
    Fire,
    Ice,
    Laser
}

public class ShotScript : MonoBehaviour
{

    [Header("Shot properties")]
    public ShotType shotType = ShotType.Fire;

    public float lifeSpan = 5;

    [Header("Damage properties")]
    [Tooltip("Projectile damages player or enemies?")]
    public bool isEnemyShot = false;

    [Tooltip("Damage inflicted")]
    public int damage = 1;
    
    void Start()
    {
        // the bullet gets destroyed after x seconds
        Destroy(gameObject, lifeSpan);

        if (shotType == ShotType.Laser) {
            Debug.DrawRay(transform.position, Vector3.right, Color.magenta, 0.1f);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right);

            StartCoroutine(LaserShot(hits));
        }
    }

    IEnumerator LaserShot(RaycastHit2D[] hits) {
        SpecialEffectsHelper.instance.Laser(this.transform.position);

        foreach (RaycastHit2D hit in hits) {
            HealthScript health = hit.transform.gameObject.GetComponent<HealthScript>();

            // Avoid friendly fire
            if (health != null && health.isEnemy != this.isEnemyShot) {

                // Freeze them all for dramatic effect!
                MoveScript moveComponent = hit.transform.gameObject.GetComponent<MoveScript>();
                if (moveComponent != null) {
                    moveComponent.speed = new Vector3(0, 0, 0);
                }
            }
        }

        // Dramatic effect
        yield return new WaitForSeconds(1);


        foreach (RaycastHit2D hit in hits) {
            HealthScript health = hit.transform.gameObject.GetComponent<HealthScript>();

            // Avoid friendly fire
            if (health != null && health.isEnemy != this.isEnemyShot) {

                Debug.Log("Laser hit! (" + health.transform.name + ")");
                health.Damage(health.hp);   // Kill
            }
        }

        yield return null;
    }

}
