using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QRRunner : MonoBehaviour
{
    [Header("Scan")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Slider slider;

    [Header("Generate")]
    [SerializeField] private RawImage qrImage;
    [SerializeField] private TMP_InputField qrInputField;

    [Header("Read")]
    [SerializeField] private Button readButton;
    [SerializeField] private Texture2D texture;

    private void Start()
    {
#if !UNITY_EDITOR
readButton.interactable = false;
#endif
    }

    private void Failed(string result)
    {
        resultText.color = Color.red;

        resultText.text = result;
    }

    private void Success(string result)
    {
        resultText.color = Color.green;
        resultText.text = $"Code => {result}";

        canvas.gameObject.SetActive(true);
    }

    private void FouresStop(string result)
    {
        resultText.color = Color.yellow;
        resultText.text = "Foures Stop";
        canvas.gameObject.SetActive(true);
    }

    public void StartScaning()
    {
        resultText.text = "";
        ShusmoAPI.QR.Scan(Success,Failed, FouresStop);
    }

    public void Generate() => qrImage.texture = ShusmoAPI.QR.Generate(qrInputField.text);

    public void Read() => resultText.text = ShusmoAPI.QR.Read(texture);
}
