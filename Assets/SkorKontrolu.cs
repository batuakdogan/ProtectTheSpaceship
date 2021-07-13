using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkorKontrolu : MonoBehaviour {

    public int skor;

    private Text benimMetnim;

    private void Start()
    {
        benimMetnim = GetComponent<Text>();
        skoruSifirla();
    }
    public void SkoruArttir(int puanlar)
    {
        skor += puanlar;
        benimMetnim.text = skor.ToString();

    }

    public void skoruSifirla()
    {
        skor = 0;
        benimMetnim.text = skor.ToString();
    }
}
