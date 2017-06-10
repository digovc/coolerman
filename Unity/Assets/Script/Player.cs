using UnityEngine;

public class Player : MonoBehaviour
{
    #region Constantes

    #endregion Constantes

    #region Atributos

    private static Player _i;

    public static Player i
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

    #endregion Atributos

    #region Construtores

    private Player()
    {
        _i = this;
    }

    #endregion Construtores

    #region Métodos

    internal void morrer()
    {
        Destroy(this.gameObject);
    }

    #endregion Métodos

    #region Eventos

    private void Start()
    {
    }

    private void Update()
    {
    }

    #endregion Eventos
}