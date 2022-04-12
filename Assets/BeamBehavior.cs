using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehavior : MonoBehaviour
{
    public Rigidbody2D BeamBody;

    private float StoredVelocity = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {

        if (StoredVelocity - BeamBody.velocity.sqrMagnitude > 10)
        {
            int clipIndex = (int)Random.Range(0, AudioClipCollection.BreakSounds.Count);

            Camera.main.GetComponent<GameCamera>().PlayAmbientSound(AudioClipCollection.BreakSounds[clipIndex]);

            Destroy(this.gameObject);
        }
        StoredVelocity = BeamBody.velocity.sqrMagnitude;

    }
}
