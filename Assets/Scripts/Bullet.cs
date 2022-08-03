using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    Vector3 startPos;
    Rigidbody bulletRB;
    public ParticleSystem partical;

    private void Start()
    {
        startPos = transform.position;
           bulletRB = GetComponent<Rigidbody>();
        bulletRB.AddForce(transform.forward * 20, ForceMode.Impulse);
    }

    private void Update()
    {
        if(startPos.z +20 <= transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
            partical.Play();
        if (collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.GetComponent<Obstacle>().Hit();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        else
        {
            partical.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
          
        }
            StartCoroutine(destroyBullet());

    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
