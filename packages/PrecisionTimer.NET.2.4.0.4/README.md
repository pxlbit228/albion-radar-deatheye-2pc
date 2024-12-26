[![Nuget](https://i.imgur.com/ZthKn2p.png)](https://www.nuget.org/packages/PrecisionTimer.NET/)

### High Precision/High Resolution Timer for .NET Standard and Greater

`PrecisionTimer.NET` callbacks can run at `1ms` on platforms that allow it.

```cs
using PrecisionTiming;
```

# Basic Usage

```cs
public static PrecisionTimer MyTimer = new();
```
Using Precision Timer is as simple as using any other Windows Timer.

```cs
MyTimer.SetInterval(SomeAction, Interval);
```

Start the Timer
```cs
MyTimer.Start();
```

Check if the Timer is Running
```cs
MyTimer.IsRunning();
```

Stop the Timer
```cs
MyTimer.Stop();
```

Dispose the Timer
```cs
MyTimer.Dispose();
```

# SetInterval / Configure

If you use `SetInterval` to set only `Action` and `Interval` the timer will automatically start with the most common defaults, but it has several optional parameters you can change

![s](https://i.imgur.com/q2SBVh0.png)

For example, If you don't want the timer to start automatically then use the following

```cs
MyTimer.SetInterval(SomeAction, Interval, false);
```

You can use `Configure` instead of `SetInterval` if you prefer

```cs
MyTimer.Configure(SomeAction, Interval, false);
```


# Manual Setup

You can manually configure the `Timer` and then `Start` it yourself instead of using `SetInterval`

![](https://i.imgur.com/luvyJSl.png)


## SetAction

Sets the `Action` that will be triggered by the TimerCallback when the `Period` has elapsed
```cs
MyTimer.SetAction(Action);
```

`Periodic Timers` must stop before setting.


## SetPeriod / SetInterval(int)

Sets the Period (Interval) between `Actions` in `Milliseconds`.
```cs
MyTimer.SetPeriod(int);
```
```cs
MyTimer.GetPeriod();
```

`Both Timer Modes` must stop before setting.


## SetResolution

Set the `Resolution` of the Timer before you start the timer

```cs
MyTimer.Start(int);
```

The resolution is in milliseconds, The default resolution for `SetInterval(Action)` is 0

The `Resolution` increases with smaller values.

A resolution of 0 indicates periodic events should occur with the greatest possible accuracy. 

To reduce system overhead, however, you should use the maximum value appropriate for your application.

The normal Resolution of a .Net Timer is around 12-15ms


## SetEventArgs

Set `EventArgs` of the Timer

```cs
MyTimer.SetEventArgs(EventArgs);
```
```cs
MyTimer.GetEventArgs();
```

`Periodic Timers` must stop before setting.


## SetAutoResetMode

Set the Periodic/OneShot Mode of the <see cref="PrecisionTimer"/>

```cs
MyTimer.SetAutoResetMode(bool);
```
```cs
MyTimer.GetAutoResetMode();
```

`True` if the `PrecisionTimer` should raise the `Ticks` Event each time the interval elapses. (Periodic)

`False` if the `PrecisionTimer` should raise the event only once AFTER the first time the interval elapses. (One-Shot)

`Both Timer Modes` must stop before setting.

# Timing Settings

Global Multimedia Timer settings that affect your application

[Set the Applications Minimum Resolution](https://docs.microsoft.com/en-us/windows/win32/api/timeapi/nf-timeapi-timebeginperiod)
```cs
PrecisionTimerSettings.SetMinimumTimerResolution(0);
```

[Clear the Applications Minimum Resolution](https://docs.microsoft.com/en-us/windows/win32/api/timeapi/nf-timeapi-timeendperiod)
```cs
PrecisionTimerSettings.ClearMinimumTimerResolution(0);
```

# Multimedia Timer

Precision Timer.NET is a Multimedia Timer.

You can read more about Multimedia Timers on [MSDN](https://docs.microsoft.com/en-us/windows/win32/multimedia/multimedia-timer-reference)

![Copyright S Christison ©2023](https://i.imgur.com/DPDYaC7.png)
