using System.Collections;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicsWrap;

namespace WaveFunction;

public class Main : ITickable
{
#pragma warning disable CS8618, CA2211 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static string ProgramPath;
#pragma warning restore CS8618, CA2211 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public const int Width = 640;
    public const int Height = 640;
    public WaveMap? Map { get; private set; }
    public Interfaces.ICollapseMethod? CollapseMethod { get; private set; }
    public GraphicsWrap.Interfaces.Basic.IWindow Window { get; }
    public Scheduler Scheduler { get; }
    private GraphicsWrap.Interfaces.Services.IEventPoller EventPoller { get; }
    public Main(Scheduler scheduler)
    {
        Scheduler = scheduler;
        var WindowFactory = WrapperRegistry.WindowFactory;
        WindowFactory.SetRenderFlags(GraphicsWrap.Interfaces.Services.RendererFlags.RENDERER_ACCELERATED);
        Window = WindowFactory.CreateWindow("WaveFunction", Width, Height);
        Map = new WaveMap(Width / Interfaces.ITile.DefaultWidth, Height / Interfaces.ITile.DefaultHeight,41029874);
        scheduler.AddTickable(this);
        EventPoller = WrapperRegistry.EventPoller;
#pragma warning disable CS8601 // Possible null reference argument.
        //get the path of the program
        ProgramPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
#pragma warning restore CS8601 // Possible null reference argument.
    }
    public void Init()
    {
        TileRegistry.InitAll(Window.Renderer);
        CollapseMethod = new Strategies.SimpleBruteforce();
    }

    public void Update(object? sender, Scheduler.UpdateEventArgs e)
    {
        //Console.WriteLine("Update");
        while (EventPoller.PollEvent(out var ev) > 0)
        {
            if (ev is not null)
            {
                //Console.WriteLine(ev.Type.ToString());
                if (ev.Type == GraphicsWrap.Interfaces.Services.EventType.Quit)
                {
                    Scheduler.Running = false;
                }
            }
        }
        //collapse
        if ((CollapseMethod is not null)&&(Map is not null))
        {
            if (Map.EntropyCells.Count != 0)
            {
                CollapseMethod.Collapse(Map);
            }
        }
    }

    public void Render(object? sender, EventArgs e)
    {
        //clear
        Window.Renderer.SetDrawColor(255, 0, 0, 255);
        Window.Renderer.Clear();
        //TODO: draw stuff
        if (Map is not null)
        {
            for (int x = 0; x < Map.Width; x++)
            {
                for (int y = 0; y < Map.Height; y++)
                {
                    Map.Cells[x, y].Tile.Draw(x * Interfaces.ITile.DefaultWidth, y * Interfaces.ITile.DefaultHeight);
                }
            }
        }
        //present
        Window.Renderer.Present();
    }

    public void Dispose()
    {
        Scheduler.OnRender -= Render;
        Scheduler.OnUpdate -= Update;
    }
}
