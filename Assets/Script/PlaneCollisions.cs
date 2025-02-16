using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;

public class PlaneCollisions : MonoBehaviour
{

    [SerializeField] PlaneManager planemanager;
    private float timer = 0f;

    private void Update()
    {
        if (timer > 0f) 
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Coins"))
        {
            planemanager.CoinCollect();
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Blocks") && timer<=0f)
        {
            timer = 3f/planemanager.movingSpeed;
            planemanager.HitBlocks();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bombs"))
        {
            planemanager.Looser();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            planemanager.Finish();
        }
    }
}
