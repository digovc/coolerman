using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    #region Constantes

    #endregion Constantes

    #region Atributos

    public Button btnReiniciar;
    public Button btnSair;
    public GameObject pnlGameOver;
    public GameObject pnlGamePlay;
    public Text txtGameOverScore;
    public Text txtScore;
    private static UiScript _i;

    public static UiScript i
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

    public UiScript()
    {
        _i = this;
    }

    #endregion Construtores

    #region Métodos

    public void btnReiniciar_onClick()
    {
        SceneManager.LoadScene(0);
    }

    public void btnSair_onClick()
    {
        Application.Quit();
    }

    internal void gameOver()
    {
        this.txtGameOverScore.text = this.txtScore.text;

        this.pnlGamePlay.SetActive(false);
        this.pnlGameOver.SetActive(true);

        this.btnReiniciar.onClick.AddListener(delegate
        {
            this.btnReiniciar_onClick();
        });

        this.btnSair.onClick.AddListener(this.btnSair_onClick);
    }

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