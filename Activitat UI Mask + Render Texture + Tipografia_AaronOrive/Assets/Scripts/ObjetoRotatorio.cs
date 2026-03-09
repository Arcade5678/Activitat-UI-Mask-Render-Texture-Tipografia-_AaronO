using UnityEngine;

public class ObjetoRotatorio : MonoBehaviour
{
    [Header("Configuración")]
    public Vector3 velocidadRotacion = new Vector3(0f, 90f, 30f);

    void Update()
    {
        transform.Rotate(velocidadRotacion * Time.deltaTime, Space.Self);
    }
}
