using System;
using UnityEngine;

public class CoolermanManger : MonoBehaviour
{
    #region Constantes

    #endregion Constantes

    #region Atributos

    private DateTime _dttUltimo;
    private int _intQuantidadeAtual = 0;
    private int _intQuantidadeLimite = 5;
    private int _intTempoNascimento = 250;
    private GameObject _objCoolerMan;

    private DateTime dttUltimo
    {
        get
        {
            return _dttUltimo;
        }

        set
        {
            _dttUltimo = value;
        }
    }

    private int intQuantidadeAtual
    {
        get
        {
            return _intQuantidadeAtual;
        }

        set
        {
            _intQuantidadeAtual = value;
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

    private int intTempoNascimento
    {
        get
        {
            return _intTempoNascimento;
        }

        set
        {
            _intTempoNascimento = value;
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

    #endregion Construtores

    #region Métodos

    private void gerarCoolerman()
    {
        if (this.intQuantidadeAtual >= this.intQuantidadeLimite)
        {
            return;
        }

        var intDiff = (DateTime.Now - this.dttUltimo).Milliseconds;

        if ((DateTime.Now - this.dttUltimo).Milliseconds < this.intTempoNascimento)
        {
            return;
        }

        this.dttUltimo = DateTime.Now;
        this.intQuantidadeAtual++;

        Instantiate(this.objCoolerMan);
    }

    #endregion Métodos

    #region Eventos

    public void Start()
    {
        this.dttUltimo = DateTime.Now;
    }

    public void Update()
    {
        this.gerarCoolerman();
    }

    #endregion Eventos
}