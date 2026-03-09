using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class TexturaRender3D : MonoBehaviour
{
    [Header("Configuración")]
    public RenderTexture texturaRender;
    public RawImage imagenDestino;

    private Camera camara;

    void Awake()
    {
        camara = GetComponent<Camera>();

        if (texturaRender != null && camara.targetTexture == null)
            camara.targetTexture = texturaRender;

        if (imagenDestino != null && texturaRender != null)
            imagenDestino.texture = texturaRender;
    }
}
