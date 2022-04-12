using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D ProjectileBody;
    public AudioSource Voice;
    public AudioSource SFX;

    private bool isLive = false;

    public bool IsLive
    {
        get
        {
            return isLive;
        }
        set
        {
            if(value != isLive)
            {
                if (value)
                {
                    int clipIndex = (int)Random.Range(0, AudioClipCollection.BattleCries.Count);

                    Voice.clip = AudioClipCollection.BattleCries[clipIndex];

                    Voice.Play();
                }
                isLive = value;
            }
        }
    }

    private Vector3 InitPos;

    private Vector3 InitScale;

    float timeOutClock = 1;

    // Start is called before the first frame update
    void Start()
    {
        InitPos = transform.position;

        ProjectileBody.gravityScale = 0f;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        SFX.clip = AudioClipCollection.BounceSound;
        SFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLive == false)
        {
            Vector3 DragPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            float TempMagnitude = (DragPos - InitPos).magnitude;

            Vector3 DragPosDampened = Vector3.Lerp(InitPos, DragPos, 3f/TempMagnitude); //Limit Pull Length

            transform.position = DragPosDampened;
        }

        else
        {
            if (ProjectileBody.velocity.magnitude < 0.1)
            {
                timeOutClock -= Time.deltaTime;

                if(timeOutClock < 0)
                {
                    Camera.main.GetComponent<GameCamera>().lerpAmount = 0f;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                timeOutClock = 1;
            }
        }
    }

    public void Launch()
    {
        IsLive = true;

        GameCamera.TargetObject = this.gameObject;

        ProjectileBody.gravityScale = 1f;

        ProjectileBody.velocity = 3f * ((InitPos - transform.position).magnitude) * new Vector3(InitPos.x - transform.position.x, InitPos.y - transform.position.y, 0f);
    }

    public bool CanLaunch(float SlingRadius)
    {
        if ((InitPos - transform.position).magnitude > (SlingRadius))
        {
            return true;
        }
        return false;
    }
}
