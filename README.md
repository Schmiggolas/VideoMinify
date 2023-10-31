# VideoMinify

 [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A small tool to (bulk-)compress video files easily.

## Description
My internet connection ~~is~~ was very bad. But I still want to upload videos for my friends. Heavy compression and conversion is basically the only thing working for me.
Handling FFMPEG by hand every time is tedious and I tried to automate this step for myself. 

Meet VideoMinify!

Automatically convert a video to mp4 and compress the shit out of it!

## Installation

Check the [release tab](https://github.com/TehMightyPotato/VideoMinify/releases) for a built Windows x64 version. I can build for any OS if you hit me up. Or just do it yourself.

You'll also need FFMPEG in your PATH ([Windows tutorial](https://video.stackexchange.com/questions/20495/how-do-i-set-up-and-use-ffmpeg-in-windows)). On Windows I recommend using [chocolatey](https://chocolatey.org/) to install FFMPEG. 

## Usage

Just drag & drop your video files onto the executable and this tool will create a minified copy of that video right next to your original one.

It also works with multiple videos at a time. 

Pro Tip: I save the binary + config in a random folder on my hard drive and use a shortcut on my desktop for easy access. Drag & Drop also works on the shortcut
 

## Configuration

This program will attempt to load a file called `config.json` in the same directory as the executable. There is a sample config provided in this repo. 
If no config is present, defaults (check options below) are used.

Codec is hard-coded to `lib-x264` because I choose to.

### Options

#### `suffix` 
- the suffix the new file is created with. (Input file "awesome_clip.mkv" creates output file "awesome_clip_minified.mp4" etc.)
- Default = `_minified`

#### `use_multithreading` 
- tells FFMPEG to use multithreading for the conversion process. 
- Default = `true`

#### `constant_rate_factor` 
- how aggressive should the compression work. 
- 0 = lossless, 52 = omega potato mode. 
- Sane values are between 0 and 28.
- Default = 28

## Credits

Thanks to the [FFMpegCore Team](https://github.com/rosenbjerg/FFMpegCore) for their C# wrapper of FFMPEG.

And of course to [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) for providing the best JSON library for .NET
