using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    #region Constantes

    #endregion Constantes

    #region Atributos

    private Text _txtScore;

    private Text txtScore
    {
        get
        {
            if (_txtScore != null)
            {
                return _txtScore;
            }

            _txtScore = this.GetComponentInChildren<Text>();

            return _txtScore;
        }
    }

    private RawImage _imgPlayPause;

    private RawImage imgPlayPause
    {
        get
        {
            if (_imgPlayPause != null)
            {
                return _imgPlayPause;
            }

            _imgPlayPause = this.GetComponentInChildren<RawImage>();

            return _imgPlayPause;
        }
    }

    #endregion Atributos

    #region Construtores

    #endregion Construtores

    #region Métodos

    private void processarUpdate()
    {
        this.txtScore.text = AppCoolerman.i.intScore.ToString();
    }

    #endregion Métodos

    #region Eventos
    private void OnMouseEnter()
    {
        
    }

    private void Update()
    {
        this.processarUpdate();
    }

    #endregion Eventos
}