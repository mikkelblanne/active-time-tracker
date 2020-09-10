# active-time-tracker
A simple Windows application for tracking unlocked screen time on a daily basis. Useful for time keeping when working from home. Only saves the sum total per day, not the individual active periods within a day.

There's only one screen with two functions:
- Display total active time today
- Allow datepicker selection of a day for which to show the active time

"Connected Standby" on modern hardware can cause problems - Windows in some cases doesn't send the usual PowerModeChanged events. The result is that time in standby/suspend mode counts as active. To be sure that it works correctly on your machine, please validate that time in standby/suspend mode is correctly excluded, before relying on the results for something important.
