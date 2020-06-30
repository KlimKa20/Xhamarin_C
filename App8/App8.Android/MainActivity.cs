using System;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Util;
using Android.Views;
using Android.Widget;
using App8.Droid;
using Java.Lang;
using Javax.Crypto;
using Res = Android.Resource;

//[assembly: Dependency(typeof(App8.Droid.MainActivity))]
[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
namespace App8.Droid
{

    [Activity(Label = "Беларусская чыгунка", Icon = "@mipmap/br", Theme = "@style/MainTheme", MainLauncher =/*true*/ false, ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity/*, ILocalize*/
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        static readonly string DIALOG_FRAGMENT_TAG = "fingerprint_auth_fragment";
        static readonly int ERROR_TIMEOUT = 250;
        static int kl = 0;
        static int j= 0;
        // ReSharper restore InconsistentNaming
        bool _canScan;
        [Obsolete]
        FingerprintManagerApiDialogFragment _dialogFrag;
        View _errorPanel, _authenticatedPanel, _initialPanel, _scanInProgressPanel;
        TextView _errorTextView1, _errorTextView2;
        FingerprintManagerCompat _fingerprintManager;
        Android.Support.V4.OS.CancellationSignal cancellationSignal;
        Button _startAuthenticationScanButton, _scanAgainButton, _failedScanAgainButton;
        Bundle bundle;
        //public System.Globalization.CultureInfo GetCurrentCultureInfo()
        //{
        //    var androidLocale = Java.Util.Locale.Default;
        //    var netLanguage = androidLocale.ToString().Replace("_", "-");
        //    return new System.Globalization.CultureInfo(netLanguage);
        //}

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
               TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;
            Task task= new Task(FingerPrintAuthenticationExample);
            //Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            task.Start();

            bundle = savedInstanceState;

            //    SetContentView(Resource.Layout.activity_fingerprintmanager_api);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
            //while(true)
            //{
            //    if (j == 1)
            //        break;
            //}
            //InitializeViewReferences();
            //    _fingerprintManager = FingerprintManagerCompat.From(this);
            //    string canScanMsg = CheckFingerprintEligibility();

            //    _startAuthenticationScanButton.Click += StartFingerprintScan;
            //    _scanAgainButton.Click += ScanAgainButtonOnClick;
            //    _failedScanAgainButton.Click += RecheckEligibility;

            //    if (_canScan)
            //    {
            //        _dialogFrag = FingerprintManagerApiDialogFragment.NewInstance();
            //    }
            //    else
            //    {
            //        ShowError("Can't use this device for the sample.", canScanMsg);
            //    }
            //base.OnDestroy();
            //base.OnCreate(savedInstanceState);
            //dd();
            
        }
        protected void FingerPrintAuthenticationExample()
        {
            const int flags = 0; /* always zero (0) */

            // CryptoObjectHelper is described in the previous section.
            CryptoObjectHelper cryptoHelper = new CryptoObjectHelper();

            // cancellationSignal can be used to manually stop the fingerprint scanner. 
            cancellationSignal = new Android.Support.V4.OS.CancellationSignal();

            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(this);

            // AuthenticationCallback is a base class that will be covered later on in this guide.
            FingerprintManagerCompat.AuthenticationCallback authenticationCallback = new MyAuthCallbackSample();

            // Start the fingerprint scanner.
            fingerprintManager.Authenticate(cryptoHelper.BuildCryptoObject(), flags, cancellationSignal, authenticationCallback, null);
        }
        class MyAuthCallbackSample : FingerprintManagerCompat.AuthenticationCallback
        {
            // Can be any byte array, keep unique to application.
            static readonly byte[] SECRET_BYTES = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // The TAG can be any string, this one is for demonstration.
            static readonly string TAG = "X:" + typeof(MyAuthCallbackSample).Name;

            public MyAuthCallbackSample()
            {
            }

            public override void OnAuthenticationSucceeded(FingerprintManagerCompat.AuthenticationResult result)
            {
                if (result.CryptoObject.Cipher != null)
                {
                    j = 1;
                    try
                    {
                        ;
                        // Calling DoFinal on the Cipher ensures that the encryption worked.
                        byte[] doFinalResult = result.CryptoObject.Cipher.DoFinal(SECRET_BYTES);
                        //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                        //global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

                        //LoadApplication(new App());
                        // No errors occurred, trust the results.              
                    }
                    catch (BadPaddingException bpe)
                    {
                        // Can't really trust the results.
                        Log.Error(TAG, "Failed to encrypt the data with the generated key." + bpe);
                    }
                    catch (IllegalBlockSizeException ibse)
                    {
                        // Can't really trust the results.
                        Log.Error(TAG, "Failed to encrypt the data with the generated key." + ibse);
                    }
                }
                else
                {
                    // No cipher used, assume that everything went well and trust the results.
                }
            }

            public override void OnAuthenticationError(int errMsgId, ICharSequence errString)
            {
               Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }

            public override void OnAuthenticationFailed()
            {
                // Tell the user that the fingerprint was not recognized.
            }

            public override void OnAuthenticationHelp(int helpMsgId, ICharSequence helpString)
            {
                // Notify the user that the scan failed and display the provided hint.
            }
        }
        [Obsolete]
         void RecheckEligibility(object sender, EventArgs eventArgs)
        { 
            string canScanMsg = CheckFingerprintEligibility();
            if (_canScan)
            {
                _dialogFrag = FingerprintManagerApiDialogFragment.NewInstance();
                _initialPanel.Visibility = ViewStates.Visible;
                _authenticatedPanel.Visibility = ViewStates.Gone;
                _errorPanel.Visibility = ViewStates.Gone;
                _scanInProgressPanel.Visibility = ViewStates.Gone;
            }
            else
            {
                Log.Debug(TAG, "This device is still ineligiblity for fingerprint authentication. ");
                _dialogFrag = null;
                ShowError("Can't use this device for the sample.", canScanMsg);
            }
        }


        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug(TAG, "OnResume");
        }

        protected override void OnPause()
        {
            base.OnPause();
            Log.Debug(TAG, "OnPause");
        }

        void StartFingerprintScan(object sender, EventArgs args)
        {
            Permission permissionResult = ContextCompat.CheckSelfPermission(this,
                                                                                   Manifest.Permission.UseFingerprint);
            if (permissionResult == Permission.Granted)
            {
                _initialPanel.Visibility = ViewStates.Gone;
                _authenticatedPanel.Visibility = ViewStates.Gone;
                _errorPanel.Visibility = ViewStates.Gone;
                _scanInProgressPanel.Visibility = ViewStates.Visible;
                _dialogFrag.Init();
                _dialogFrag.Show(FragmentManager, DIALOG_FRAGMENT_TAG);
            }
            else
            {
                Snackbar.Make(FindViewById(Res.Id.Content),
                              Resource.String.missing_fingerprint_permissions,
                              Snackbar.LengthLong)
                        .Show();
            }
        }

        void ScanAgainButtonOnClick(object sender, EventArgs eventArgs)
        {
            _initialPanel.Visibility = ViewStates.Visible;
            _authenticatedPanel.Visibility = ViewStates.Gone;
            _errorPanel.Visibility = ViewStates.Gone;
            _scanInProgressPanel.Visibility = ViewStates.Gone;
        }

        void InitializeViewReferences()
        {
            _scanInProgressPanel = FindViewById(Resource.Id.scan_in_progress);
            _initialPanel = FindViewById(Resource.Id.initial_panel);
            _startAuthenticationScanButton = FindViewById<Button>(Resource.Id.start_authentication_scan_buton);

            _errorPanel = FindViewById(Resource.Id.error_panel);
            _errorTextView1 = FindViewById<TextView>(Resource.Id.error_text1);
            _errorTextView2 = FindViewById<TextView>(Resource.Id.error_text2);
            _failedScanAgainButton = FindViewById<Button>(Resource.Id.failed_scan_again_button);

            _authenticatedPanel = FindViewById(Resource.Id.authenticated_panel);
            _scanAgainButton = FindViewById<Button>(Resource.Id.scan_again_button);
        }

        /// <summary>
        ///     Checks to see if the hardware is available to scan for fingerprints
        ///     and that the user has fingerprints enrolled.
        /// </summary>
        /// <returns></returns>
        string CheckFingerprintEligibility()
        {
            _canScan = true;

            if (!_fingerprintManager.IsHardwareDetected)
            {
                _canScan = false;
                string msg = Resources.GetString(Resource.String.missing_fingerprint_scanner);
                Log.Warn(TAG, msg);
                return msg;
            }

            KeyguardManager keyguardManager = (KeyguardManager) GetSystemService(KeyguardService);
            if (!keyguardManager.IsKeyguardSecure)
            {
                string msg = Resources.GetString(Resource.String.keyguard_disabled);
                _canScan = false;
                Log.Warn(TAG, msg);
                return msg;
            }


            if (!_fingerprintManager.HasEnrolledFingerprints)
            {
                _canScan = false;
                string msg = Resources.GetString(Resource.String.register_fingerprint);
                Log.Warn(TAG, msg);
                return msg;
            }

            return string.Empty;
        }

        /// <summary>
        ///     Display error message feedback to the user.
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        public void ShowError(string text1, string text2 = "")
        {
            Log.Debug(TAG, "ShowError: '{0}' / '{1}'", text1, text2);
            _errorPanel.PostDelayed(() =>
                                    {
                                        _errorTextView1.Text = text1;
                                        _errorTextView2.Text = text2;

                                        _initialPanel.Visibility = ViewStates.Gone;
                                        _authenticatedPanel.Visibility = ViewStates.Gone;
                                        _errorPanel.Visibility = ViewStates.Visible;
                                        _scanInProgressPanel.Visibility = ViewStates.Gone;
                                    }, ERROR_TIMEOUT);
        }

        public void AuthenticationSuccessful()
        {
            _initialPanel.Visibility = ViewStates.Gone;
            _authenticatedPanel.Visibility = ViewStates.Gone;
            _errorPanel.Visibility = ViewStates.Gone;
            _scanInProgressPanel.Visibility = ViewStates.Gone;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        //public enum SensorState
        //{
        //    NOT_SUPPORTED,
        //    NOT_BLOCKED, // если устройство не защищено пином, рисунком или паролем
        //    NO_FINGERPRINTS, // если на устройстве нет отпечатков
        //    READY
        //}
        //public static bool checkFingerprintCompatibility(Context context)
        //{
        //    return FingerprintManagerCompat.From(context).IsHardwareDetected;
        //}
        //public static SensorState checkSensorState(Context context)
        //{
        //    if (checkFingerprintCompatibility(context))
        //    {
        //        KeyguardManager keyguardManager =
        //                (KeyguardManager)context.GetSystemService(KeyguardService);
        //        if (!keyguardManager.IsKeyguardSecure)
        //        {
        //            return SensorState.NOT_BLOCKED;
        //        }
        //        FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(context);
        //        if (!fingerprintManager.HasEnrolledFingerprints)
        //        {
        //            return SensorState.NO_FINGERPRINTS;
        //        }
        //        return SensorState.READY;
        //    }
        //    else
        //    {
        //        return SensorState.NOT_SUPPORTED;
        //    }
        //}
        //private static KeyStore sKeyStore;
        //private static bool getKeyStore()
        //{
        //    try
        //    {
        //        sKeyStore = KeyStore.GetInstance("AndroidKeyStore");
        //        sKeyStore.Load(null);
        //        return true;
        //    }
        //    catch (KeyStoreException /*| IOException | NoSuchAlgorithmException | CertificateException*/ e)
        //    {
        //        e.PrintStackTrace();
        //    }
        //    return false;
        //}

        //private static KeyPairGenerator sKeyPairGenerator;
        //private static bool getKeyPairGenerator()
        //{
        //    try
        //    {
        //        sKeyPairGenerator = KeyPairGenerator.GetInstance(KeyProperties.KeyAlgorithmRsa, "AndroidKeyStore");
        //        return true;
        //    }
        //    catch (NoSuchAlgorithmException /*| NoSuchProviderException */e)
        //    {
        //        e.PrintStackTrace();
        //    }
        //    return false;
        //}
        //private static bool generateNewKey()
        //{
        //    if (getKeyPairGenerator())
        //    {
        //        try
        //        {
        //            sKeyPairGenerator.Initialize(new KeyGenParameterSpec.Builder(Ke KEY_ALIAS,
        //                KeyProperties.PURPOSE_ENCRYPT | KeyProperties.PURPOSE_DECRYPT)
        //                    .setDigests(KeyProperties.DigestSha256, KeyProperties.DigestSha512)
        //                    .setEncryptionPaddings(KeyProperties.EncryptionPaddingRsaOaep )
        //                    .setUserAuthenticationRequired(true)
        //                    .build());
        //            sKeyPairGenerator.GenerateKeyPair();
        //            return true;
        //        }
        //        catch (InvalidAlgorithmParameterException e)
        //        {
        //            e.PrintStackTrace();
        //        }
        //    }
        //    return false;
        //}

    }
}