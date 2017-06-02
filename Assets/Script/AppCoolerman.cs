using Boo.Lang;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppCoolerman : MonoBehaviour
{
    #region Constantes

    private const float FLT_TEMPO_NASCIMENTO_MINIMO = 150;
    private const int FLT_VELOCIDADE_MAXIMA = 25;
    private const int INT_QUANTIDADE_MAXIMA = 150;

    #endregion Constantes

    #region Atributos

    private static AppCoolerman _i;
    private bool _booMorreu;
    private float _fltCoolermanVelocidade = 5;
    private float _fltTempoNascimento = 1.5f;
    private int _intQuantidadeLimite = 5;
    private int _intScore;
    private List<Coolerman> _lstObjCoolerman;
    private GameObject _objCoolerMan;

    public static AppCoolerman i
    {
        get
        {
            return _i;
        }

        private set
        {
            _i = value;
        }
    }

    public bool booMorreu
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

    public int intScore
    {
        get
        {
            return _intScore;
        }

        private set
        {
            _intScore = value;
        }
    }

    public List<Coolerman> lstObjCoolerman
    {
        get
        {
            if (_lstObjCoolerman != null)
            {
                return _lstObjCoolerman;
            }

            _lstObjCoolerman = new List<Coolerman>();

            return _lstObjCoolerman;
        }
    }

    internal float fltCoolermanVelocidade
    {
        get
        {
            return _fltCoolermanVelocidade;
        }

        set
        {
            _fltCoolermanVelocidade = value;
        }
    }

    private float fltTempoNascimento
    {
        get
        {
            return _fltTempoNascimento;
        }

        set
        {
            _fltTempoNascimento = value;
        }
    }

    private int intQuantidadeLimite
    {
        get
        {
            return _intQuantidadeLimite;
        }

        set
        {
            _intQuantidadeLimite = value;
        }
    }

    private GameObject objCoolerMan
    {
        get
        {
            if (_objCoolerMan != null)
            {
                return _objCoolerMan;
            }

            _objCoolerMan = Resources.Load<GameObject>("Prefab/Coolerman");

            return _objCoolerMan;
        }
    }

    #endregion Atributos

    #region Construtores

    public AppCoolerman()
    {
        _i = this;
    }

    #endregion Construtores

    #region Métodos

    internal void matar(Coolerman objCoolerman)
    {
        this.intScore++;

        this.removerObjCoolerman(objCoolerman);

        if (this.intQuantidadeLimite < INT_QUANTIDADE_MAXIMA)
        {
            this.intQuantidadeLimite = (int)(this.intScore * .075 + 5);
        }

        if (this.fltTempoNascimento < FLT_TEMPO_NASCIMENTO_MINIMO)
        {
            this.fltTempoNascimento -= (this.fltTempoNascimento * .05f);
        }

        if (this.fltCoolermanVelocidade < FLT_VELOCIDADE_MAXIMA)
        {
            this.fltCoolermanVelocidade += (this.fltCoolermanVelocidade * .0125f);
        }
    }

    internal void morrer(Coolerman objCoolerman)
    {
        this.booMorreu = true;

        this.StopAllCoroutines();

        foreach (var objCoolerman2 in this.lstObjCoolerman)
        {
            if (objCoolerman2.Equals(objCoolerman))
            {
                continue;
            }

            objCoolerman2.sumir();
        }

        SceneManager.LoadScene(0);
    }

    private void gerarCoolerman()
    {
        if (this.lstObjCoolerman.Count > this.intQuantidadeLimite)
        {
            return;
        }

        Instantiate(this.objCoolerMan);
    }

    private IEnumerator gerarCoolermanLoop()
    {
        while (true)
        {
            this.gerarCoolerman();

            yield return new WaitForSeconds(this.fltTempoNascimento);
        }
    }

    private void removerObjCoolerman(Coolerman objCoolerman)
    {
        if (!this.lstObjCoolerman.Contains(objCoolerman))
        {
            return;
        }

        this.lstObjCoolerman.Remove(objCoolerman);
    }

    #endregion Métodos

    #region Eventos

    private void Start()
    {
        this.StartCoroutine(this.gerarCoolermanLoop());
    }

    #endregion Eventos
}