# Cascade

![](./Images/cascade-logo.png)

Lets you define sources of data which can be chained together to form calculations.

The values are cached to ensure the UI is responsive.

When a source value changes the value is not instantly recalculated (like RX) but
instead marked as Invalidated. Only when its value is next read (if at all) will the
value be recalculated and cached.

## Features
- ValueSource&lt;T&gt;: A mutable source that can be subscribed to.
- CalculatedSource&lt;T&gt;: Derives new values based on a calculation, lazily recalculates on demand.
- CombinedSource&lt;T&gt;: Merges multiple sources’ values into one computed result.

## Usage
```csharp
using Morris.Cascade;

// Create a source
var x = new ValueSource<int>();
x.Subscribe(() => Console.WriteLine($"x changed: {x.Value}"));
x.Value = 42; // Subscribers notified when value changes

// Create a calculated source
var doubled = new CalculatedSource<int>(x, v => v * 2);
doubled.Subscribe(() => Console.WriteLine($"doubled: {doubled.Value}"));

// Create a combined source
var y = new ValueSource<int> { Value = 10 };
var sum = new CombinedSource<int>(new[] { x, y }, vals => vals.Sum());
sum.Subscribe(() => Console.WriteLine($"sum: {sum.Value}"));
y.Value = 20; // sum recalculated on access