global using WaveFunction.Extensions.Collections;

GraphicsWrap.WrapperRegistry.LoadDefaults();
WaveFunction.TileRegistry.RegisterDefaultTiles();
WaveFunction.Scheduler scheduler = new();
WaveFunction.Main core = new(scheduler);
core.Init();
scheduler.TargetFPS = 60;
scheduler.TargetUPS = 30;
scheduler.UnlimitedUPS = true;
scheduler.Start();

//scheduler loop captures the thread, so if we reach this point, the program is exiting
core.Dispose();