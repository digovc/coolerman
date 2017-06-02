using UnityEngine;

namespace Assets.Script.O.Game
{
    internal static class Utils
    {
        public static Vector3 randomizarCircle(Vector3 vctCentro, float fltAltura, float fltRadius)
        {
            float fltAngulo = (Random.value * 360);

            Vector3 vctPosicao = new Vector3(0, fltAltura, 0);

            vctPosicao.x = (vctCentro.x + fltRadius * Mathf.Sin(fltAngulo * Mathf.Deg2Rad));
            vctPosicao.z = (vctCentro.z + fltRadius * Mathf.Cos(fltAngulo * Mathf.Deg2Rad));

            return vctPosicao;
        }
    }
}