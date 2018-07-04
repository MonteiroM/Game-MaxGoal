using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControl : MonoBehaviour {

    //posição seta
    
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
    //paredes
    //             ParedeDireita  ParedeEsquerda
    private Transform paredeLD, paredeLE;
    //MorteBola anim
    [SerializeField]
    private GameObject morteBolaAnim; 

    void Awake()
    {
        setaGo = GameObject.Find("Seta");
        seta2Img = setaGo.transform.GetChild(0).gameObject;

        //garantir que as setas começarão desativadas
        setaGo.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
        
        paredeLD = GameObject.Find("ParedeDireita").GetComponent<Transform>();
        paredeLE = GameObject.Find("ParedeEsquerda").GetComponent<Transform>();

    }

    // Use this for initialization
    void Start () {
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

        Paredes();
        
    }
    //Rotação
    void PosicionaSeta()
    {
        setaGo.GetComponent<Image>().rectTransform.position = this.transform.position;
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

            //para ativar a imagem das setas quando tocar na bola
            setaGo.GetComponent<Image>().enabled = true;
            seta2Img.GetComponent<Image>().enabled = true;
        }
    }

    void OnMouseUp()
    {
        liberaRotate = false;
        //desativar as setas quando a bola for arremessada
        setaGo.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;

        if (GameManager.instance.tiro == 0 && force > 0) {
            liberaTiro = true;
            seta2Img.GetComponent<Image>().fillAmount = 0;

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
    //Morte Bola
    //verificar se a bola passou do espaço determinado,se passou ela será destruida
    void Paredes() {
        if ((this.transform.position.x > paredeLD.position.x) || (this.transform.position.x < paredeLE.position.x)) {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Morte")) {
            Instantiate(morteBolaAnim, transform.position, Quaternion.identity);//instanciar a aniamação da morte da bola 
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if (collision.gameObject.CompareTag("Win")) {
            GameManager.instance.win = true;
            int temp = OndeEstou.instance.fases + 1;
            temp++;
            PlayerPrefs.SetInt("Level" + temp, 1);
        }
    }

}
