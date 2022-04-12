using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Camera cam;

    public AudioSource AmbientAudioSource;

    public float EasingAmount;
    public float lerpAmount = 0;

    public GameObject Background;

    public float ImageResolution;

    public List<GameObject> ParallaxLayers = new List<GameObject>();
    private SpriteRenderer SpriteRef;

    private Vector3 CamStartPos;
    private float CamOrthoStartSize;

    private Vector3 BackgroundStartPos;

    public static GameObject TargetObject;


    private void Start()
    {
        ImageResolution /= 100;

        CamStartPos = transform.position;
        CamOrthoStartSize = cam.orthographicSize;

        BackgroundStartPos = Background.transform.position;
    }
    private void Update()
    {
        CamFollow();
        DoParallax();
        cam.orthographicSize = transform.position.y;
    }

    public void CamFollow()
    {
        if (TargetObject != null)
        {
            if (lerpAmount < 1f)
            {
                lerpAmount += (1 / EasingAmount) * Time.deltaTime;
            }
            transform.position = LerpTransform(transform.position, TargetObject.transform.position, lerpAmount);
        }
        else
        {
            if (lerpAmount < 1f)
            {
                lerpAmount += (1 / EasingAmount) * Time.deltaTime;
            }
            transform.position = LerpTransform(transform.position, CamStartPos, lerpAmount);
        }
        
    }
    private Vector3 LerpTransform(Vector3 StartPos, Vector3 EndPos, float LerpAnalog)
    {
        Vector3 LerpTargetPos = Vector3.Lerp(StartPos, EndPos, lerpAmount);
        return new Vector3(LerpTargetPos.x, Mathf.Clamp(LerpTargetPos.y, CamStartPos.y, 999f), CamStartPos.z);
    }

    public void DoParallax()
    {
        int ParallaxAmount = 2 * (int)(cam.orthographicSize / CamOrthoStartSize) + 2;

        Background.transform.position = new Vector3(0f, BackgroundStartPos.y, 0f);

        for (int x = 0; x < ParallaxLayers.Count; x++)
        {
            SpriteRef = ParallaxLayers[x].GetComponent<SpriteRenderer>();

            Vector3 ParallaxPosition = new Vector3(
                transform.position.x / Mathf.Pow(2, ParallaxLayers.Count - x) + (ImageResolution * (int)((transform.position.x - transform.position.x / Mathf.Pow(2, ParallaxLayers.Count - x)) / ImageResolution)),
                ParallaxLayers[x].transform.position.y,
                ParallaxLayers[x].transform.position.z);

            ParallaxLayers[x].transform.position = ParallaxPosition;

            SpriteRef.size = new Vector2(ParallaxAmount * ImageResolution, ImageResolution);
        }
    }

    public void PlayAmbientSound(AudioClip clip)
    {
        AmbientAudioSource.clip = clip;
        AmbientAudioSource.Play();
    }
}
