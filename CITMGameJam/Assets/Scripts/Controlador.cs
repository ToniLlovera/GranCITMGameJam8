using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    public GameObject palancaArriba;
    public GameObject palancaAbajo;

    public RawImage ImageIz;
    public RawImage ImageCe;
    public RawImage ImageDe;

    public bool winner;
    public int imagen1;
    public int imagen2;
    public int imagen3;

    public bool arranque = false;
    private bool canStart = true; 

    void Start()
    {
        palancaArriba.SetActive(true);
        palancaAbajo.SetActive(false);


        ImageIz = GameObject.Find("Izquierda").GetComponent<RawImage>();
        ImageCe = GameObject.Find("Centro").GetComponent<RawImage>();
        ImageDe = GameObject.Find("Derecha").GetComponent<RawImage>();
    }

    void Update()
    {

        if (Input.mouseScrollDelta.y != 0 && canStart)
        {
            canStart = false; 
            StartCoroutine(ActivateArranque());
        }

        // Comprobación de victoria
        if ((imagen1 == imagen2) && (imagen2 == imagen3))
        {
            winner = true;
        }
    }

    IEnumerator ActivateArranque()
    {
        palancaArriba.SetActive(false);
        palancaAbajo.SetActive(true);
        winner = false;
        arranque = true;

   
        imagen1 = Random.Range(1, 6);
        ImageIz.texture = Resources.Load<Texture>("Sprites/" + imagen1);

        imagen2 = Random.Range(1, 6);
        ImageCe.texture = Resources.Load<Texture>("Sprites/" + imagen2);

        imagen3 = Random.Range(1, 6);
        ImageDe.texture = Resources.Load<Texture>("Sprites/" + imagen3);

        yield return new WaitForSeconds(0.5f); 

        palancaArriba.SetActive(true);
        palancaAbajo.SetActive(false);
        arranque = false;

        yield return new WaitForSeconds(0.5f);
        canStart = true; 
    }
}
