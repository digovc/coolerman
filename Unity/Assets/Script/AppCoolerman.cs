using Boo.Lang;
using System.Collections;
using UnityEngine;

public class AppCoolerman : MonoBehaviour
{
    #region Constantes

    #endregion Constantes

    #region Atributos

    private static AppCoolerman _i;
    private bool _booMorreu;
    private float _fltTempoNascimento = 2.5f;
    private int _intQuantidadeLimite = 5;
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

    #endregion Métodos

    #region Eventos

    private void Start()
    {
        this.StartCoroutine(this.gerarCoolermanLoop());
    }

    #endregion Eventos
}