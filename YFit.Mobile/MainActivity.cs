namespace YFit.Mobile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Android.App;
    using Android.Hardware;
    using Android.OS;
    using Android.Widget;

    using RestSharp;

    using YFit.Domain.Entities.Concrete;
    using YFit.Mobile.Services.Abstract;
    using YFit.Mobile.Services.Concrete;

    [Activity(Label = "Accelerometer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ISensorEventListener
    {
        private static readonly object SyncLock = new object();

        private readonly IAccelerometerService accelerometerService;

        private readonly IGyroscopeService gyroscopeService;

        private bool isTrackPoints;

        private Button sendButton;

        private SensorManager sensorManagerA;

        private SensorManager sensorManagerG;

        private List<AccelerometerPoint> APointLists =new List<AccelerometerPoint>();
        private List<GyroscopePoint> GPointLists = new List<GyroscopePoint>();

        private Button trackButton;

        // public MainActivity(IGyroscopeService gyroscopeService, IAccelerometerService accelerometerService)
        // {
        // this.gyroscopeService = gyroscopeService;
        // this.accelerometerService = accelerometerService;
        // }
        public MainActivity()
        {
            gyroscopeService = new GyroscopeService();
            accelerometerService = new AccelerometerService();
            accelerometerService.AccelerometerPoints = new List<AccelerometerPoint>();
            gyroscopeService.GyroscopePoints = new List<GyroscopePoint>();
        }

        public bool IsEqualsData(AccelerometerPoint input, AccelerometerPoint input2)
        {
            var result =
                !(Math.Abs(input.X - input2.X) > 0.10 && Math.Abs(input.Y - input2.Y) > 0.10
                  && Math.Abs(input.Z - input2.Z) > 0.10);

            return result;
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // We don't want to do anything here.
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (isTrackPoints)
            {
                lock (SyncLock)
                {
                    if (e.Sensor.Type == SensorType.Accelerometer)
                    {
                        var x = e.Values[0];
                        var y = e.Values[1];
                        var z = e.Values[2];

                        var point = new AccelerometerPoint(x, y, z);

                        //accelerometerService.AccelerometerPoints.ToList().Add(point);
                        this.APointLists.Add(point);
                    }

                    if (e.Sensor.Type == SensorType.Gyroscope)
                    {
                        var x = e.Values[0];
                        var y = e.Values[1];
                        var z = e.Values[2];

                        var point = new GyroscopePoint(x, y, z);

                        //gyroscopeService.GyroscopePoints.ToList().Add(point);
                        this.GPointLists.Add(point);
                    }
                }
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            sensorManagerA = (SensorManager)GetSystemService(SensorService);
            sensorManagerG = (SensorManager)GetSystemService(SensorService);

            trackButton = FindViewById<Button>(Resource.Id.TrackButton);
            trackButton.Click += delegate { isTrackPoints = !isTrackPoints; };

            sendButton = FindViewById<Button>(Resource.Id.SendButton);
            sendButton.Click += delegate
                {
                    string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    string filename = Path.Combine(path, "myfileA.txt");

                    using (var streamWriter = new StreamWriter(filename, true))
                    {
                        foreach (var VARIABLE in COLLECTION)
                        {
                            
                        }
                        streamWriter.WriteLine();
                    }
                };
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorManagerA.UnregisterListener(this);
            sensorManagerG.UnregisterListener(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            sensorManagerA.RegisterListener(
                this, 
                sensorManagerA.GetDefaultSensor(SensorType.Accelerometer), 
                SensorDelay.Ui);
            sensorManagerG.RegisterListener(this, sensorManagerG.GetDefaultSensor(SensorType.Gyroscope), SensorDelay.Ui);
        }
    }
}