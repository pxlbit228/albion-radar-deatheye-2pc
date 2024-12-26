# DEATHEYE for Albion Online (2 device version)

## Discord

- Link: [https://discord.gg/Jhr5Y7qrCY](https://discord.gg/Jhr5Y7qrCY)

## Warning

This software is **DETECTED** by **BattlEye** and not intended to be used on a single PC with game client.

### Hot it can be used

You need two devices:

1. Windows PC (or virtual machine - VMware, Virtual Box, etc.)
    - for _radar_ and _traffic decryption tool (Cryptonite)_
2. Any other device, where you can run Albion Online (any platform)

### How to run

Next guide is for pair of two Windows PCs (or VM + PC).

#### Install some required components (only first time) on 1st PC (or VM)

1. Install npcap: https://npcap.com/dist/npcap-1.80.exe
2. Install .NET Framework 4.8 Runtime: https://dotnet.microsoft.com/download/dotnet-framework/net48
3. Download the latest build from Releases and extract it to any directory
4. Download items images from https://drive.google.com/file/d/1Egji6ceOt3eBh6yE9-zxB4OPtoWMYSby/view?usp=sharing and
   extract directory ITEMS (it contains only images) to root directory of the radar.
   Replace all previous files if asked.
5. Download ao-bin-dumps
   from [https://github.com/ao-data/ao-bin-dumps](https://github.com/ao-data/ao-bin-dumps/archive/refs/heads/master.zip)
   and content of directory ao-bin-dumps-master into ao-bin-dumps directory in root directory of the radar.
   Replace all previous files if asked.
6. Download the latest version of traffic decryption tool Cryptonite from our Discord server and set it up.
    - Guide how to set it up is in the Discord server, check the channel `#cryptonite`
    - Your PCs must be in the same network

#### Run

1. `1st PC:` Run Cryptonite (use .bat depending on your game server)
2. `1st PC:` Run radar X975.exe
3. `2nd PC:` Run Albion Online

### How to build

1. Install npcap
2. Install .NET Framework 4.8 SDK
3. Clone the repository
4. Items images and ao-bin-dumps are not included in the repository,
   you need to download them from the links above and extract to source directory 
   (or output, but you should delete last AfterBuild tasks in the project file)
5. Open the project in your favorite IDE and build it

If you can't build this project following the steps above, you should learn the basics.

But anyway if you have any questions, try to search for the answer in our Discord server or feel free to ask.

## Source

Original repository can be found [here](https://github.com/W4RPWISH/AlbionRadar-DEATHEYE_2pc), it is not possible to
build the project from it.