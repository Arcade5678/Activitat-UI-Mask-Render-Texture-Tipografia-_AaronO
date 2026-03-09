using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonConfirmacion : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Header("Configuración")]
    public float duracionPulsacion = 2f;
    public Image imagenProgreso;
    public GameObject objetivoToggle;

    private bool estaPulsado = false;
    private float temporizador = 0f;

    void Awake()
    {
        if (imagenProgreso != null)
            imagenProgreso.fillAmount = 0f;
    }

    void Update()
    {
        if (!estaPulsado) return;

        temporizador += Time.deltaTime;
        float progreso = Mathf.Clamp01(temporizador / duracionPulsacion);

        if (imagenProgreso != null)
            imagenProgreso.fillAmount = progreso;

        if (temporizador >= duracionPulsacion)
        {
            Confirmar();
            Reiniciar();
        }
    }

    private void Confirmar()
    {
        if (objetivoToggle == null) return;
        objetivoToggle.SetActive(!objetivoToggle.activeSelf);
    }

    private void Reiniciar()
    {
        estaPulsado = false;
        temporizador = 0f;
        if (imagenProgreso != null)
            imagenProgreso.fillAmount = 0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        estaPulsado = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reiniciar();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Reiniciar();
    }
}
