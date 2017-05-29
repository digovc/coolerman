using UnityEngine;

public class Coolerman : MonoBehaviour
{
    #region Constantes

    #endregion Constantes

    #region Atributos

    #endregion Atributos

    #region Construtores

    #endregion Construtores

    #region Métodos

    private void iniciar()
    {
        this.iniciarPosicao();
    }

    private void iniciarPosicao()
    {
        Vector3 vctCentro = new Vector3(0, .5f, 0);

        Vector3 vctPosicao = this.randomizarCircle(vctCentro, 5.0f);

        Quaternion qtnRotacao = Quaternion.FromToRotation(Vector3.forward, vctCentro - vctPosicao);

        this.transform.position = vctPosicao;
        this.transform.rotation = qtnRotacao;
    }

    private Vector3 randomizarCircle(Vector3 vctCentro, float fltRadius)
    {
        float fltAngulo = (Random.value * 360);

        Vector3 vctPosicao = new Vector3(0, .5f, 0);

        vctPosicao.x = (vctCentro.x + fltRadius * Mathf.Sin(fltAngulo * Mathf.Deg2Rad));
        vctPosicao.z = (vctCentro.z + fltRadius * Mathf.Cos(fltAngulo * Mathf.Deg2Rad));

        return vctPosicao;
    }

    #endregion Métodos

    #region Eventos

    public void Start()
    {
        this.iniciar();
    }

    public void Update()
    {
    }

    #endregion Eventos
}