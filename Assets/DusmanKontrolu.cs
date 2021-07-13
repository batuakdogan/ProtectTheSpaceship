using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanKontrolu : MonoBehaviour {
    public GameObject mermi;
    public float mermiHizi = 8f;
    public float can = 100f;
    public float saniyeBasinaMermiAtma = 0.6f;
    public int skorDegeri = 200;
    private SkorKontrolu skorKontrolu;

    public AudioClip AtesSesi;
    public AudioClip OlumSesi;

    private void Start()
    {
        skorKontrolu = GameObject.Find("Skor").GetComponent<SkorKontrolu>();
    }
    //OLASILIK  0,1   0-> false 1->true   (0,1) - > olasılık   0.99 - > %99

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
                AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
                skorKontrolu.SkoruArttir(skorDegeri);
            }
        }
        
    }
    
	
	// Update is called once per frame
	void Update () {
        //0,016*0.6 = 0,0096
        float atmaOlasiligi = Time.deltaTime * saniyeBasinaMermiAtma;
        if(Random.value < atmaOlasiligi)
        {
            AtesEt();

        }
       
	}

    void AtesEt()
    {
        Vector3 baslangicPozisyonu = transform.position + new Vector3(0, -0.8f, 0);
        GameObject dusmaninMermisi = Instantiate(mermi, baslangicPozisyonu, Quaternion.identity) as GameObject;
        dusmaninMermisi.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -mermiHizi);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);
    }
}
