using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] public int value;

    private void OnTriggerEnter2D(Collider2D other){

        if (other.tag == "Player")
        {

            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddGems(value);
                Destroy(this.gameObject);
            }
        }
    }
}
