using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicsWrap.Interfaces.Services;
/// <summary>
/// wraps SDL_EventPoller
/// </summary>
public interface IEventPoller
{
    //basic event polling
    public int PollEvent(out IEvent Event);
}
/// <summary>
/// wraps SDL_Event
/// </summary>
public interface IEvent
{
    //event type
    public EventType Type { get; }
    //event data
    public structs.EventData Data { get; }
}

//event types
public enum EventType
{
    Quit,
    KeyDown,
    KeyUp,
    MouseMotion,
    MouseButtonDown,
    MouseButtonUp,
    MouseWheel,
    WindowEvent,
    Unknown
}
