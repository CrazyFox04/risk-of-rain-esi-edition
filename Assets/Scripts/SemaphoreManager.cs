using System.Threading;

public static class SemaphoreManager
{
    public static SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
}