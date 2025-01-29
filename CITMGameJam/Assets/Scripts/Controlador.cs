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

    public int imagen1;
    public int imagen2;
    public int imagen3;

    public bool arranque = false;

    public GameObject winner;

    // Use this for initialization
    void Start()
    {
        palancaArriba.SetActive(true);
        palancaAbajo.SetActive(false);

        // Encuentra los objetos RawImage en la escena
        ImageIz = GameObject.Find("Izquierda").GetComponent<RawImage>();
        ImageCe = GameObject.Find("Centro").GetComponent<RawImage>();
        ImageDe = GameObject.Find("Derecha").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            palancaArriba.SetActive(false);
            palancaAbajo.SetActive(true);
            arranque = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            palancaArriba.SetActive(true);
            palancaAbajo.SetActive(false);
            arranque = false;
        }

        if ((imagen1 == imagen2) && (imagen2 == imagen3))
        {
            winner.transform.gameObject.SetActive(true);
        }

        if (arranque == true)
        {
            // Genera números aleatorios para las imágenes
            imagen1 = Random.Range(1, 6);
            Texture textura1 = Resources.Load<Texture>("Sprites/" + imagen1);
            ImageIz.texture = textura1;

            imagen2 = Random.Range(1, 6);
            Texture textura2 = Resources.Load<Texture>("Sprites/" + imagen2);
            ImageCe.texture = textura2;

            imagen3 = Random.Range(1, 6);
            Texture textura3 = Resources.Load<Texture>("Sprites/" + imagen3);
            ImageDe.texture = textura3;
        }
    }
}
