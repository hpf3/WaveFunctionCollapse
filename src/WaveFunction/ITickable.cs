using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction;

public interface ITickable: IDisposable
{
    public void Update(object? sender, Scheduler.UpdateEventArgs e);
    public void Render(object? sender, EventArgs e);
}
