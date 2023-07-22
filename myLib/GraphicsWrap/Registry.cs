using System;
using SDL2;
namespace GraphicsWrap;
/// <summary>
/// this class provides a method for getting classes without having to know the implementation
/// </summary>
public static class WrapperRegistry
{
    private static readonly HashSet<Type> _types = new();
    //class variables
    public static Interfaces.Factories.IWindowFactory WindowFactory{get{return Get<Interfaces.Factories.IWindowFactory>();}}
    public static Interfaces.Services.IEventPoller EventPoller{get{return Get<Interfaces.Services.IEventPoller>();}}
    //methods
    private static bool _initialized = false;
    private static T Get<T>() where T : class
{
    foreach (var type in _types)
    {
        if (typeof(T).IsAssignableFrom(type))
        {
            #pragma warning disable CS8600,CS8603 // Possible null reference argument.
            return (T)Activator.CreateInstance(type);
            #pragma warning restore CS8600,CS8603 // Possible null reference argument.
        }
    }

    throw new Exception("Type not registered: " + typeof(T).Name);
}
    private static void Initialize(){
        if(_initialized) return;
        _initialized = true;
        //initialize SDL
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
         {
             Console.WriteLine("SDL could not initialize! SDL_Error: {0}", SDL.SDL_GetError());
         }
        //initialize SDL_image
        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) <0)
        {
            Console.WriteLine("SDL_image could not initialize! SDL_image Error: {0}", SDL.SDL_GetError());
        }
    }
    public static void LoadDefaults()
    {
        Initialize();
        _types.Add(typeof(defaultImplimentation.Basic.Window));
        _types.Add(typeof(defaultImplimentation.Services.EventPoller));
        _types.Add(typeof(defaultImplimentation.Factories.WindowFactory));
    }
}
