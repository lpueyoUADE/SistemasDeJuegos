using UnityEngine;

public class EnemyCannonAimSimple : MonoBehaviour
{
    public float range;
    public float angle;
    public float rotatingspeed = 4;
    public GameObject rotatingCannon;
    public Transform projectileOut;
    public Vector3 target;
    public bool exec = false;

    public IWeapon currentWeapon;
    public WeaponType weapon = WeaponType.EnemyBlueRail;


    Vector3 Origin => transform.position;
    Vector3 Forward => transform.forward;

    private void Update()
    {
        float delta = Time.deltaTime;

        if (exec)
        {
            currentWeapon?.Fire(projectileOut);
            Vector3 finalDir = new Vector3(-target.x, 0, -target.z);
            rotatingCannon.transform.rotation = Quaternion.SlerpUnclamped(rotatingCannon.transform.rotation, Quaternion.LookRotation(finalDir), delta * rotatingspeed);
            exec = false;
        }

        currentWeapon?.Recoil(delta);
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 dirToTarget = other.transform.position - Origin;
        float angleToTarget = Vector3.Angle(Forward, dirToTarget);
        if (angleToTarget <= angle / 2)
        {
            target = other.transform.position - rotatingCannon.transform.position;
            exec = true;
        }

        if (currentWeapon == null)
        {
            currentWeapon = FactoryWeapon.CreateWeapon(weapon);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Origin, range);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Origin, Quaternion.Euler(0, angle / 2, 0) * Forward * range);
        Gizmos.DrawRay(Origin, Quaternion.Euler(0, -(angle / 2), 0) * Forward * range);
    }
}
