using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelArtTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ProfileButton pb)) transform.parent.GetComponent<Dex>().SetPixelArt(pb.fk.fakename);
    }
}
