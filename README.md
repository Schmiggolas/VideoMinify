# VideoMinify

## Description
My internet connection is very bad. But I still want to upload videos for my friends. Heavy compression and conversion is basically the only thing working for me.
Handling FFMPEG by hand every time is tedious and I tried to automate this step for myself. 

Meet VideoMinify!

Automatically convert a video to mp4 and compress the shit out of it!

## Installation

Check the release tab for a built Windows x64 version. I can build for any OS if you hit me up. Or just do it yourself.

You'll also need FFMPEG in your PATH. On Windows I recommend using [chocolatey](https://chocolatey.org/) to install FFMPEG. 

## Usage

Just drag & drop your video files onto the executable and this tool will create a minified copy of that video right next to your original one.

It also works with multiple videos at a time. 
 

## Configuration

This program will attempt to load a file called `config.json` in the same directory as the executable. There is a sample config provided in this repo. 
If no config is present defaults (check options below) are used.

Codec is hard-coded to `lib-x264` because I couldn't be bothered to also include that.

### Options

#### `suffix` 
- the suffix the new file is created with. 
- Default = `_minified`

#### `use_multithreading` 
- use multithreading for the conversion process. 
- Default = `true`

#### `constant_rate_factor` 
- how aggressive should the compression work. 
- 0 = lossless, 52 = omega potato mode. 
- Sane values are between 0 and 28.
- Default = 28

## Credits

Thanks to the [FFMpegCore Team](https://github.com/rosenbjerg/FFMpegCore) for their C# wrapper of FFMPEG.

## License
MIT. Do whatever you want.
