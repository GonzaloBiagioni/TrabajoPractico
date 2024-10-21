using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class TurretAI : MonoBehaviour
{
    public enum TurretType
    {
        Single,
        Dual,
        Catapult
    }

    public GameObject currentTarget;
    public Transform turretHead;
    public float attackDist = 10.0f;
    public float shootCoolDown;
    private float timer;
    public float lookSpeed;

    public TurretType turretType;
    public Transform muzzleMain;
    public Transform muzzleSub;
    public string bulletTagSingle;
    public string bulletTagDual;
    public string bulletTagCatapult;

    void Start()
    {
        InvokeRepeating("CheckForTarget", 0, 0.5f);
    }

    void Update()
    {
        if (currentTarget != null)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTargetDist > attackDist)
            {
                currentTarget = null;
            }
        }

        timer += Time.deltaTime;
        if (timer >= shootCoolDown && currentTarget != null)
        {
            timer = 0;
            Shoot();
        }
    }

    private void CheckForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);

        foreach (var coll in colls)
        {
            if (coll.CompareTag("Player"))
            {
                currentTarget = coll.gameObject;
                return;
            }
        }
    }

    private void FollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - turretHead.position;
        targetDir.y = 0;
        turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, Quaternion.LookRotation(targetDir), lookSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (turretType == TurretType.Catapult)
        {
            GameObject projectile = ObjectPool.Instance.GetPooledObject(bulletTagCatapult);
            if (projectile != null)
            {
                projectile.transform.position = muzzleMain.position;
                projectile.transform.rotation = muzzleMain.rotation;
                projectile.SetActive(true);
            }
        }
        else if (turretType == TurretType.Dual)
        {
            GameObject projectileLeft = ObjectPool.Instance.GetPooledObject(bulletTagDual);
            GameObject projectileRight = ObjectPool.Instance.GetPooledObject(bulletTagSingle);

            if (projectileLeft != null)
            {
                projectileLeft.transform.position = muzzleMain.position;
                projectileLeft.transform.rotation = muzzleMain.rotation;
                projectileLeft.SetActive(true);
            }

            if (projectileRight != null)
            {
                projectileRight.transform.position = muzzleSub.position;
                projectileRight.transform.rotation = muzzleSub.rotation;
                projectileRight.SetActive(true);
            }
        }
        else
        {
            GameObject projectile = ObjectPool.Instance.GetPooledObject(bulletTagSingle);
            if (projectile != null) 
            {
                projectile.transform.position = muzzleMain.position;
                projectile.transform.rotation = muzzleMain.rotation;
                projectile.SetActive(true);
            }
        }
    }
}