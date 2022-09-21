    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public int dano;
    public float tempoDeVida;
    public float distancia;
    public int tempoTiro;
    public LayerMask layerInimigo;


    // Start is called before the first frame update
    void Start() {
        //Destroe o tiro
        Invoke("DestruirProjetil", tempoDeVida/4);

    }

    // Update is called once per frame
    void Update()
    {
        //Verifica colisao
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, distancia, layerInimigo);
        if(hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Inimigo")) {
                hitInfo.collider.GetComponent<enemyAI>().TakeDamage(dano);
            }
            if (hitInfo.collider.CompareTag("InimigoFlying")) {
                hitInfo.collider.GetComponent<enemyGraphics>().TakeDamage(dano);
            }            
            DestruirProjetil();
        }
    }

    void DestruirProjetil() {
        Destroy(gameObject);
    }
}
