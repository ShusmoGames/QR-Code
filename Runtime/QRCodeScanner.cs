using UnityEngine;
using ZXing;
using UnityEngine.UI;
using System.Collections;

namespace ShusmoAPI.QRCode
{
    public class QRCodeScanner : MonoBehaviour
    {
        [SerializeField]
        private RawImage deviceCamera;
        [SerializeField]
        private AspectRatioFitter fitter;
        [SerializeField]
        private RectTransform scanZone;

        private bool _isCamAvaible;
        private WebCamTexture _cameraTexture;
        private bool update = false;
        private bool active = false;

        private EndScanEvent _successCallback;
        private EndScanEvent _failedCallback;
        private EndScanEvent _fouresStopCallback;

        public bool StartScanning(EndScanEvent successCallback, EndScanEvent failedCallback, EndScanEvent fouresStopCallback)
        {
            if (active) return false;
            active = true;

            _successCallback = successCallback;
            _failedCallback = failedCallback;
            _fouresStopCallback = fouresStopCallback;

            StartCoroutine(RequestCameraPermission());

            return true;
        }

        private void OnDisable()
        {
            update = false;
            active = false;

            _successCallback = null;
            _failedCallback = null;
        }

        private IEnumerator RequestCameraPermission()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                SetUp();
                update = true;
                StartCoroutine(UpdateQR());
            }
            else
            {
                _failedCallback?.Invoke("Camera permission denied");
                gameObject.SetActive(false);
            }
        }

        private void SetUp()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;

            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices.Length == 0)
            {
                _isCamAvaible = false;
                return;
            }
            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i].isFrontFacing == false)
                {
                    _cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
                    break;
                }
            }
            _cameraTexture.Play();
            deviceCamera.texture = _cameraTexture;
            _isCamAvaible = true;
        }

        private IEnumerator UpdateQR()
        {
            yield return new WaitForSeconds(1);

            WaitForSeconds wait = new WaitForSeconds(Time.deltaTime);
            while (update)
            {
                UpdateCameraRender();
                Scan();

                yield return wait;
            }
        }

        private void UpdateCameraRender()
        {
            if (_isCamAvaible == false)
            {
                return;
            }
            float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
            fitter.aspectRatio = ratio;

            int orientation = _cameraTexture.videoRotationAngle;
            orientation = orientation * 3;
            deviceCamera.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
        }

        private void Scan()
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);
                if (result != null)
                {
                    _successCallback?.Invoke(result.Text);
                    gameObject.SetActive(false);
                }
                else
                    _failedCallback?.Invoke("Failed to Read QR CODE");
            }
            catch
            {
                _failedCallback?.Invoke("FAILED IN TRY");
            }
        }

        public void StopScaning()
        {
            _fouresStopCallback?.Invoke(null);
            gameObject.SetActive(false);
        }
    }
}