using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CapturaPantalla : MonoBehaviour
{
    [Header("Configuración")]
    public float intervaloCaptura = 0.05f;
    public int anchoCaptura = 512;
    public int altoCaptura = 288;

    [Header("UI")]
    public RawImage imagenDestino;

    [Header("Post-proceso")]
    public Material materialPostProceso;

    private Camera camara;
    private RenderTexture rtCaptura;
    private RenderTexture rtSalida;
    private float temporizador = 0f;

    void Awake()
    {
        camara = GetComponent<Camera>();

        rtCaptura = new RenderTexture(anchoCaptura, altoCaptura, 24, RenderTextureFormat.ARGB32);
        rtCaptura.Create();

        rtSalida = new RenderTexture(anchoCaptura, altoCaptura, 0, RenderTextureFormat.ARGB32);
        rtSalida.Create();

        if (imagenDestino != null)
            imagenDestino.texture = rtSalida;
    }

    void Update()
    {
        temporizador += Time.deltaTime;
        if (temporizador >= intervaloCaptura)
        {
            temporizador = 0f;
            CapturarFrame();
        }
    }

    private void CapturarFrame()
    {
        RenderTexture anteriorTarget = camara.targetTexture;
        camara.targetTexture = rtCaptura;
        camara.Render();
        camara.targetTexture = anteriorTarget;

        if (materialPostProceso != null)
            Graphics.Blit(rtCaptura, rtSalida, materialPostProceso);
        else
            Graphics.Blit(rtCaptura, rtSalida);
    }

    void OnDestroy()
    {
        if (rtCaptura != null) rtCaptura.Release();
        if (rtSalida != null) rtSalida.Release();
    }
}
