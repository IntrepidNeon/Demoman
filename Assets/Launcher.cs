using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public AudioSource AudioSource;

    public GameObject Projectile;

    private GameObject ProjectileObject;

    public GameObject TargetBuilding;

    private Projectile LiveProjectile;

    private bool SlingZone;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") == 1)
        {
            if (SlingZone == true && LiveProjectile == null)
            {
                if (ProjectileObject == null)
                {
                    ProjectileObject = Instantiate(Projectile, transform);
                    LiveProjectile = ProjectileObject.GetComponent<Projectile>();
                }
            }
            
        }
        else
        {
            if (LiveProjectile != null)
            {
                if (LiveProjectile.CanLaunch(GetComponent<CircleCollider2D>().radius))
                {
                    AudioSource.clip = AudioClipCollection.ShootSound;

                    AudioSource.Play();

                    LiveProjectile.Launch();
                }
                else
                {
                    Destroy(ProjectileObject);
                }
                LiveProjectile = null;
            }
        }
        
    }

    private void OnMouseEnter()
    {
        SlingZone = true;
    }
    private void OnMouseExit()
    {
        SlingZone = false;
    }
}
