using sl;
using System;

namespace VL.Devices.ZED
{
    public static class CameraUtils
    {
        public static SensorsData GetSensorsData(this Camera camera, TIME_REFERENCE referenceTime)
        {
            var sensorsData = default(SensorsData);
            camera.GetSensorsData(ref sensorsData, referenceTime);
            return sensorsData;
        }
    }
}
