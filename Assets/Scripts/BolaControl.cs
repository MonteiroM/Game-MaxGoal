using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControl : MonoBehaviour {

    //posição seta
    [SerializeField] private Transform posStart;
    //seta
    public GameObject setaGo;
    //angulo
    public float zRotate;
    public bool liberaRotate = false;
    public bool liberaTiro = false;
    //Força
    private Rigidbody2D bola;
    public float force = 0f;
    public GameObject seta2Img;

    void Awake()
    {
        setaGo = GameObject.Find("Seta");
        seta2Img = setaGo.transform.GetChild(0).gameObject;
        setaGo.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        
        posStart = GameObject.Find("posStart").GetComponent<Transform>();
        PosicionaBola();
        //Força
        bola = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        //Rotação
        PosicionaSeta();
        RotacaoSeta();
        LimitaRotacao();
        InputDeRotacao();
        //Força
        AplicaForca();
        ControlaForca();

    }
    //Rotação
    void PosicionaSeta()
    {
        setaGo.GetComponent<Image>().rectTransform.position = this.transform.position;
    }

    void PosicionaBola()
    {
        this.gameObject.transform.position = posStart.position;
    }

    void RotacaoSeta()
    {
        setaGo.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    void InputDeRotacao()
    {

        if (liberaRotate)
        {

            float moveY = Input.GetAxis("Mouse Y");

            if (zRotate < 90)
            {
                if (moveY > 0)
                {
                    zRotate += 2.5f;
                }
            }

            if (zRotate > 0)
            {
                if (moveY < 0)
                {
                    zRotate -= 2.5f;
                }
            }
        }
    }

    void LimitaRotacao()
    {
        if (zRotate >= 90)
        {
            zRotate = 90;
        }

        if (zRotate <= 0)
        {
            zRotate = 0;
        }
    }

    void OnMouseDown()
    {
        if (GameManager.instance.tiro == 0) {
            liberaRotate = true;
            setaGo.SetActive(true);
        }
    }

    void OnMouseUp()
    {
        liberaRotate = false;
        setaGo.SetActive(false);
        if (GameManager.instance.tiro == 0 && force > 0) {
            liberaTiro = true;
            AudioManager.instance.PlaySonsFX(1);
            GameManager.instance.tiro = 1;
        }
    }
    //Força
    void AplicaForca()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);


        if (liberaTiro)
        {
            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
        }
    }

    void ControlaForca()
    {
        if (liberaRotate)
        {
            float moveX = Input.GetAxis("Mouse X");

            if (moveX < 0)
            {
                seta2Img.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
            }

            if (moveX > 0)
            {
                seta2Img.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
            }
        }
    }

    void BolaDinamica() {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

    }
}
