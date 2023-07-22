using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicsWrap.Interfaces.Services;
using GraphicsWrap.structs;
using SDL2;
namespace GraphicsWrap.defaultImplimentation.Services;

public class EventPoller: IEventPoller
{
    public int PollEvent(out IEvent Event)
    {
        SDL.SDL_Event sdlEvent;
        var result = SDL.SDL_PollEvent(out sdlEvent);

           var Type = sdlEvent.type switch
            {
                SDL.SDL_EventType.SDL_QUIT => EventType.Quit,
                SDL.SDL_EventType.SDL_KEYDOWN => EventType.KeyDown,
                SDL.SDL_EventType.SDL_KEYUP => EventType.KeyUp,
                SDL.SDL_EventType.SDL_MOUSEMOTION => EventType.MouseMotion,
                SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN => EventType.MouseButtonDown,
                SDL.SDL_EventType.SDL_MOUSEBUTTONUP => EventType.MouseButtonUp,
                SDL.SDL_EventType.SDL_MOUSEWHEEL => EventType.MouseWheel,
                SDL.SDL_EventType.SDL_WINDOWEVENT => EventType.WindowEvent,
                _ => EventType.Unknown
            };
            DisplayEvent displayEvent = new DisplayEvent()
            {
                type = (EventType)sdlEvent.display.type,
                timestamp = sdlEvent.display.timestamp,
                display = sdlEvent.display.display,
                displayEvent = (DisplayEventID)sdlEvent.display.displayEvent,
                data1 = sdlEvent.display.data1
            };
            WindowEvent windowEvent = new WindowEvent()
            {
                type = (EventType)sdlEvent.window.type,
                timestamp = sdlEvent.window.timestamp,
                windowID = sdlEvent.window.windowID,
                windowEvent = (WindowEventID)sdlEvent.window.windowEvent,
                data1 = sdlEvent.window.data1,
                data2 = sdlEvent.window.data2
            };
            Keysym keysym = new Keysym()
            {
                scancode = (Scancode)sdlEvent.key.keysym.scancode,
                sym = (Keycode)sdlEvent.key.keysym.sym,
                mod = (Keymod)sdlEvent.key.keysym.mod
            };
            KeyboardEvent keyboardEvent = new KeyboardEvent()
            {
                type = (EventType)sdlEvent.key.type,
                timestamp = sdlEvent.key.timestamp,
                windowID = sdlEvent.key.windowID,
                state = sdlEvent.key.state,
                repeat = sdlEvent.key.repeat,
                keysym = keysym
            };
            QuitEvent quitEvent = new QuitEvent()
            {
                type = (EventType)sdlEvent.quit.type,
                timestamp = sdlEvent.quit.timestamp
            };
            var Data = new EventData()
            {
                display = displayEvent,
                window = windowEvent,
                key = keyboardEvent,
                quit = quitEvent
            };
            Event = new Event(Type, Data);
            return result;
    }
}
public class Event: IEvent
{
    public Event()
    {
        Type = EventType.Unknown;
        Data = new EventData();
    }
    public Event(EventType type, EventData data)
    {
        Type = type;
        Data = data;
    }
    public EventType Type { get; }
    public EventData Data { get; }
}
