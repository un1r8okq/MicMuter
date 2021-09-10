# Microphone muter

This is a small program that I use to mute and unmute my microphone.

An Arduino Uno is connected to my PC via USB, and a switch is connected to pin 
13 of the Arduino. When the switch is closed, the Ardunio prints a 1 to the 
serial line. When the switch is open, the Arduino prints a 0.

A .NET console app connects to the serial port, and toggles the mute on 
whichever Windows has set as default. It also plays a switch clicking sound as 
the switch is turned on or off.
