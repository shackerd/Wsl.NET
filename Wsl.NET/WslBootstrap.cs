namespace Wsl.NET
{
    public static class WslBootstrap
    {
        public static IWslDriver Setup(bool usePinvoke = false)
        {            
            return usePinvoke ? (IWslDriver)new WslPinvokeDriver() : new WslBinaryWrapDriver();
        }
    }
}