using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction;

/// <summary>
/// provides a scheduler for the game loop
/// </summary>
public class Scheduler: IDisposable
{
    public int TargetFPS { get; set; }
    public int TargetUPS { get; set; }
    public bool Running { get; set; }
    public bool UnlimitedFPS { get; set; }
    public bool UnlimitedUPS { get; set; }
    public Scheduler()
    {
        TargetFPS = 60;
        TargetUPS = 60;
        Running = false;
        UnlimitedFPS = false;
        UnlimitedUPS = false;
    }
    //event args
    public class UpdateEventArgs : System.EventArgs
    {
        public TimeSpan DeltaTime { get; set; }
        public UpdateEventArgs(TimeSpan DeltaTime)
        {
            this.DeltaTime = DeltaTime;
        }
    }

    //event handlers
    public event EventHandler<UpdateEventArgs>? OnUpdate;
    public event EventHandler? OnRender;

    //methods
    /// <summary>
    /// starts the scheduler
    /// </summary>
    public void Start()
    {
        Running = true;
        Run();
    }
    /// <summary>
    /// stops the scheduler
    /// </summary>
    public void Stop()
    {
        Running = false;
    }

    //main loop
    private void Run()
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    var lastUpdateTime = stopwatch.Elapsed;
    var lastRenderTime = stopwatch.Elapsed;
    double updatesPerSecond = TargetUPS;
    double framesPerSecond = TargetFPS;
    var updateInterval = TimeSpan.FromSeconds(1 / updatesPerSecond);
    var renderInterval = TimeSpan.FromSeconds(1 / framesPerSecond);
    int updates = 0;
    int frames = 0;

    while (Running)
    {
        var now = stopwatch.Elapsed;
        if (now - lastUpdateTime >= updateInterval || UnlimitedUPS)
        {
            var deltaUpdate = (now - lastUpdateTime).TotalSeconds;
            OnUpdate?.Invoke(this, new(TimeSpan.FromSeconds(deltaUpdate)));
            updates++;
            lastUpdateTime = now;
        }
        if (now - lastRenderTime >= renderInterval || UnlimitedFPS)
        {
            OnRender?.Invoke(this, EventArgs.Empty);
            frames++;
            lastRenderTime = now;
        }

        if (stopwatch.Elapsed.TotalSeconds >= 1)
        {
            //Console.WriteLine($"UPS: {updates} FPS: {frames}");
            updates = 0;
            frames = 0;
            Thread.Sleep(0);
            stopwatch.Restart();
            lastUpdateTime = stopwatch.Elapsed;
            lastRenderTime = stopwatch.Elapsed;
        }
    }
}


    //disposal
    public void Dispose()
    {
        OnUpdate = null;
        OnRender = null;
    }

    //quick add tickable
    public void AddTickable(ITickable tickable)
    {
        OnUpdate += tickable.Update;
        OnRender += tickable.Render;
    }
}
