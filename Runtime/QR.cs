using System;
using UnityEngine;
using ZXing.QrCode;
using ZXing;
using ShusmoAPI.QRCode;

namespace ShusmoAPI
{
    public static class QR
    {
        public static QRCodeScanner Scanner = null;

        /// <summary>
        /// Start Scan for QR-Code.
        /// </summary>
        /// <param name="onSuccess">Callback event with the found QR-Code result</param>
        /// <param name="onFailed">Callback event with the error</param>
        /// <param name="onFouresStop">Callback event when the scaner </param>
        /// <returns>Will return if the scaning is started fouresed to stop</returns>
        public static bool Scan(EndScanEvent onSuccess = null, EndScanEvent onFailed = null, EndScanEvent onFouresStop = null)
        {
            if (Scanner is null) Scanner = GameObject.Instantiate(Resources.Load("QR-Code Scanner") as GameObject).GetComponent<QRCodeScanner>();
            else Scanner.gameObject.SetActive(true);

            return Scanner.StartScanning(onSuccess, onFailed, onFouresStop);
        }

        /// <summary>
        /// Foures stop scanning.
        /// </summary>
        public static void FouresStop() 
        {
            if (Scanner is null) return;

            Scanner.StopScaning();
        }

        /// <summary>
        /// Generate a QR-Code.
        /// </summary>
        /// <param name="text">The QR-Code value</param>
        /// <param name="width">Texture width</param>
        /// <param name="height">Texture height</param>
        /// <returns>Will retern the value as Texture2D and can be retern null if the generate failed</returns>
        public static Texture2D Generate(string text, int width = 256, int height = 256)
        {
            try
            {
                var qrWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        Height = height,
                        Width = width
                    }
                };

                var color32 = qrWriter.Write(text);
                Texture2D qrTexture = new Texture2D(width, height);
                qrTexture.SetPixels32(color32);
                qrTexture.Apply();

                return qrTexture;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a QR-Code from a Texture2D.
        /// </summary>
        /// <param name="texture">The Texture2D containing the QR code</param>
        /// <returns>The decoded QR code value as a string, or null if reading failed</returns>
        public static string Read(Texture2D texture)
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                Color32[] pixels = texture.GetPixels32();
                int width = texture.width;
                int height = texture.height;
                Result result = barcodeReader.Decode(pixels, width, height);
                return result?.Text;
            }
            catch (Exception ex)
            {
                Debug.LogError($"QR code reading failed: {ex.Message}");
                return null;
            }
        }
    }
}