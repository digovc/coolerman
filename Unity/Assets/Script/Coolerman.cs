using Assets.Script.O.Game;
using System.Collections;
using UnityEngine;
using System;

public class Coolerman : MonoBehaviour
{
    #region Constantes

    #endregion Constantes

    #region Atributos

    private float _fltAlpha;
    private float _fltVelocidade = 5f;
    private Renderer _objRenderer;

    private float fltAlpha
    {
        get
        {
            return _fltAlpha;
        }

        set
        {
            _fltAlpha = value;
        }
    }

    private float fltVelocidade
    {
        get
        {
            return _fltVelocidade;
        }

        set
        {
            _fltVelocidade = value;
        }
    }

    private Renderer objRenderer
    {
        get
        {
            if (_objRenderer != null)
            {
                return _objRenderer;
            }

            _objRenderer = this.GetComponent<Renderer>();

            return _objRenderer;
        }
    }

    #endregion Atributos

    #region Construtores

    #endregion Construtores

    #region Métodos

    internal void sumir()
    {
        this.StopAllCoroutines();

        this.StartCoroutine(this.fadeOut());
    }

    private IEnumerator fadeIn()
    {
        Color cor = this.objRenderer.material.color;

        for (float f = 0f; f < 2.5f; f += Time.deltaTime)
        {
            cor.a = (f / 2.5f);

            this.objRenderer.material.color = cor;

            yield return null;
        }

        cor.a = 1;

        this.objRenderer.material.color = cor;
    }

    private IEnumerator fadeOut()
    {
        Color cor = this.objRenderer.material.color;

        for (float f = 0f; f < .35f; f += Time.deltaTime)
        {
            cor.a = (1 - f / .35f);

            this.objRenderer.material.color = cor;

            yield return null;
        }

        this.destruir();
    }

    private void destruir()
    {
        Destroy(this.gameObject);
    }

    private void iniciar()
    {
        AppCoolerman.i.lstObjCoolerman.Add(this);

        this.iniciarAlpha();

        this.iniciarPosicao();

        this.StartCoroutine(this.fadeIn());
    }

    private void iniciarAlpha()
    {
        var cor = this.objRenderer.material.color;

        cor.a = 0;

        this.objRenderer.material.color = cor;
    }

    private void iniciarPosicao()
    {
        Vector3 vctCentro = new Vector3(0, .5f, 0);

        Vector3 vctPosicao = Utils.randomizarCircle(vctCentro, .5f, 25);

        Quaternion qtnRotacao = Quaternion.FromToRotation(Vector3.forward, vctCentro - vctPosicao);

        this.transform.position = vctPosicao;
        this.transform.rotation = qtnRotacao;
    }

    private void mover()
    {
        if (AppCoolerman.i.booMorreu)
        {
            return;
        }

        if (this.objRenderer.material.color.a < 1)
        {
            return;
        }

        this.transform.Translate(Vector3.forward * this.fltVelocidade * Time.deltaTime, Space.Self);
    }

    #endregion Métodos

    #region Eventos

    public void OnTriggerEnter(Collider objCollider)
    {
        Debug.Log("Colidiu.");

        AppCoolerman.i.morrer(this);
    }

    private void Start()
    {
        this.iniciar();
    }

    private void Update()
    {
        this.mover();
    }

    #endregion Eventos
}