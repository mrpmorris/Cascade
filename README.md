# Cascades

Just an experiment at the moment.

It lets you define sources of data which can be chained together to form calculations etc.

The values are cached to ensure the UI is responsive.

When a source value changes the value is not instantly recalculated (like RX) but
instead marked as Invalidated. Only when its value is next read (if at all) will the
value be recalculated and cached.