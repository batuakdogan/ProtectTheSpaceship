using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UzayGemimizinKontrolu : MonoBehaviour {

    private float hiz = 10f;
    public float inceAyar = 0.7f;
    public GameObject Mermi;
    public float mermininHizi = 100f;
    public float atesEtmeAraligi = 2f;
    public float can = 400f;
    float xmin ;
    float xmax ;

    public AudioClip AtesSesi;
    public AudioClip Olumsesi;
    
	// Use this for initialization
	void Start () {
        float uzaklik = transform.position.z - Camera.main.transform.position.z;
        Vector3 solUc = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklik));
        Vector3 sagUc = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklik));
        xmin = solUc.x + inceAyar;
        xmax = sagUc.x - inceAyar;


	}
	void AtesEtme()
    {
        GameObject gemimizinMermisi = Instantiate(Mermi, transform.position+ new Vector3(0,1f,0), Quaternion.identity) as GameObject;
        gemimizinMermisi.GetComponent<Rigidbody2D>().velocity = new Vector3(0, mermininHizi, 0);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("AtesEtme", 0.0000001f, atesEtmeAraligi);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("AtesEtme");
        }
        // gemimizin x eksenindeki değeri eğer -8 ile 8 arasındaysa yeniX e ata.
        float yeniX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(yeniX, transform.position.y, transform.position.z);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-hiz*Time.deltaTime, 0, 0);
            // Vector3.left -> (-0.1,0,0)     *   10  *  0,016 -> (-0.16,0,0)
            transform.position += Vector3.left * hiz * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(hiz * Time.deltaTime, 0, 0);
            //Vector3.right -> (1,0,0)
            transform.position += Vector3.right * hiz * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MermiKontrolu carpanMermi = collision.gameObject.GetComponent<MermiKontrolu>();
        if (carpanMermi)
        {
            carpanMermi.CarptigindaYokOl();
            can -= carpanMermi.ZararVerme();
            if (can <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(Olumsesi, transform.position);
            }
        }

    }
}
