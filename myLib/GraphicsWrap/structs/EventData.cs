using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicsWrap.structs;
/// <summary>
/// the data portion of SDL_Event
/// </summary>
public struct EventData
{
    public DisplayEvent display;
    public WindowEvent window;
    public KeyboardEvent key;
    public QuitEvent quit;
    /* TODO: add implementation for the following
    public SDL_TextEditingEvent edit;
    public SDL_TextEditingExtEvent editExt;
    public SDL_TextInputEvent text;
    public SDL_MouseMotionEvent motion;
    public SDL_MouseButtonEvent button;
    public SDL_MouseWheelEvent wheel;
    public SDL_JoyAxisEvent jaxis;
    public SDL_JoyBallEvent jball;
    public SDL_JoyHatEvent jhat;
    public SDL_JoyButtonEvent jbutton;
    public SDL_JoyDeviceEvent jdevice;
    public SDL_ControllerAxisEvent caxis;
    public SDL_ControllerButtonEvent cbutton;
    public SDL_ControllerDeviceEvent cdevice;
    public SDL_ControllerTouchpadEvent ctouchpad;
    public SDL_ControllerSensorEvent csensor;
    public SDL_AudioDeviceEvent adevice;
    public SDL_SensorEvent sensor;
    public SDL_UserEvent user;
    public SDL_SysWMEvent syswm;
    public SDL_TouchFingerEvent tfinger;
    public SDL_MultiGestureEvent mgesture;
    public SDL_DollarGestureEvent dgesture;
    public SDL_DropEvent drop;*/
}
//display event struct
public struct DisplayEvent
{
    public Interfaces.Services.EventType type;
			public UInt32 timestamp;
			public UInt32 display;
			public DisplayEventID displayEvent;
			public Int32 data1;
}

//display event id enum
public enum DisplayEventID
{
    None,
    Orientation,
    Connected,
    Disconnected
}
//window event struct
public struct WindowEvent
{
    public Interfaces.Services.EventType type;
    public UInt32 timestamp;
    public UInt32 windowID;
    public WindowEventID windowEvent;
    public Int32 data1;
    public Int32 data2;
}

//window event id enum
public enum WindowEventID
{
    None,
    Shown,
    Hidden,
    Exposed,
    Moved,
    Resized,
    SizeChanged,
    Minimized,
    Maximized,
    Restored,
    Enter,
    Leave,
    FocusGained,
    FocusLost,
    Close,
    TakeFocus,
    HitTest,
    ICCProfileChanged,
    DisplayChanged
}

//keyboard event struct
public struct KeyboardEvent{
    public Interfaces.Services.EventType type;
    public UInt32 timestamp;
    public UInt32 windowID;
    public Byte state;
    public Byte repeat;
    public Keysym keysym;
}

//keysym struct
public struct Keysym{
    public Scancode scancode;
    public Keycode sym;
    public Keymod mod;
}

//quit event struct
public struct QuitEvent{
    public Interfaces.Services.EventType type;
    public UInt32 timestamp;
}