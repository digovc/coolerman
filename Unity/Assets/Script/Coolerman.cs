using Assets.Script.O.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coolerman : MonoBehaviour
{
    #region Constantes

    private const string STR_CLIP_DIE = "die";
    private const string STR_CLIP_WALK = "walk";

    #endregion Constantes

    #region Atributos

    private bool _booMorreu;
    private float _fltAlpha;
    private float _fltVelocidade;
    private List<Renderer> _lstObjRenderer;
    private Animator _objAnimator;

    private AudioSource _objAudioSource;

    private bool booMorreu
    {
        get
        {
            return _booMorreu;
        }

        set
        {
            _booMorreu = value;
        }
    }

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

    private List<Renderer> lstObjRenderer
    {
        get
        {
            if (_lstObjRenderer != null)
            {
                return _lstObjRenderer;
            }

            _lstObjRenderer = new List<Renderer>(this.GetComponentsInChildren<Renderer>());

            return _lstObjRenderer;
        }
    }

    private Animator objAnimator
    {
        get
        {
            if (_objAnimator != null)
            {
                return _objAnimator;
            }

            _objAnimator = this.GetComponentInChildren<Animator>();

            return _objAnimator;
        }
    }

    private AudioSource objAudioSource
    {
        get
        {
            if (_objAudioSource != null)
            {
                return _objAudioSource;
            }

            _objAudioSource = this.GetComponentInChildren<AudioSource>();

            return _objAudioSource;
        }
    }

    #endregion Atributos

    #region Construtores

    #endregion Construtores

    #region Métodos

    internal void destruir()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator fadeIn()
    {
        for (float t = 0f; t < 2.5f; t += Time.deltaTime)
        {
            this.lstObjRenderer.ForEach(objRender => this.fadeIn(objRender, t));

            yield return null;
        }

        this.lstObjRenderer.ForEach(objRender => this.fadeInFinalizar(objRender));
    }

    private void fadeIn(Renderer objRender, float t)
    {
        var cor = objRender.material.color;

        cor.a = (t * 2.5f * 1);

        objRender.material.color = cor;
    }

    private void fadeInFinalizar(Renderer objRender)
    {
        var cor = objRender.material.color;

        cor.a = 1;

        objRender.material.color = cor;
    }

    private IEnumerator fadeOut()
    {
        for (float t = 0f; t < 1.5f; t += Time.deltaTime)
        {
            this.lstObjRenderer.ForEach(objRender => this.fadeOut(objRender, t));

            yield return null;
        }

        this.destruir();
    }

    private void fadeOut(Renderer objRender, float t)
    {
        var cor = objRender.material.color;

        cor.a = (1 - t / 1.5f);

        objRender.material.color = cor;
    }

    private void iniciar()
    {
        AppCoolerman.i.lstObjCoolerman.Add(this);

        this.fltVelocidade = AppCoolerman.i.fltCoolermanVelocidade;

        this.iniciarAlpha();

        this.iniciarPosicao();

        this.StartCoroutine(this.fadeIn());
    }

    private void iniciarAlpha()
    {
        this.lstObjRenderer.ForEach(objRender => this.iniciarAlpha(objRender));
    }

    private void iniciarAlpha(Renderer objRender)
    {
        var cor = objRender.material.color;

        cor.a = 0;

        objRender.material.color = cor;
    }

    private void iniciarPosicao()
    {
        var vctCentro = Vector3.zero;

        var vctPosicao = Utils.randomizarCircle(vctCentro, 0, 25);

        var qtnRotacao = Quaternion.FromToRotation(Vector3.forward, vctCentro - vctPosicao);

        this.transform.position = vctPosicao;
        this.transform.rotation = qtnRotacao;
    }

    private void morrer()
    {
        if (this.booMorreu)
        {
            return;
        }

        this.booMorreu = true;
        this.enabled = false;

        AppCoolerman.i.matar(this);

        this.StopAllCoroutines();

        this.objAnimator.Play(Animator.StringToHash(STR_CLIP_DIE));

        this.objAudioSource.Play();

        this.StartCoroutine(this.fadeOut());
    }

    private void mover()
    {
        if (this.booMorreu)
        {
            return;
        }

        if (AppCoolerman.i.booMorreu)
        {
            return;
        }

        if (this.lstObjRenderer[0].material.color.a < 1)
        {
            return;
        }

        this.transform.Translate(Vector3.forward * this.fltVelocidade * Time.deltaTime, Space.Self);

        this.objAnimator.Play(Animator.StringToHash(STR_CLIP_WALK));
    }

    #endregion Métodos

    #region Eventos

    public void OnTriggerEnter(Collider objCollider)
    {
        Debug.Log("Colidiu.");

        AppCoolerman.i.morrer(this);
    }

    private void OnMouseDown()
    {
        this.morrer();
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