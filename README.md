# Free DEATHEYE Radar for Albion Online (2 device version)

## Discord

- Link: [https://discord.gg/Jhr5Y7qrCY](https://discord.gg/Jhr5Y7qrCY)

## Warning

This software is **DETECTED** by **BattlEye** and not intended to be used on a single PC with game client.

## Features
- Players (**current position**, items, detection sound, name, guild, alliance, distance, colors, etc.)
- Resources and resource mobs
- World mobs
- Dungeons (any type, **chest type and rarity** for current floor)
- Customizable map (size, position, colors, etc.)

### Hot it can be used

You need two devices:

1. Windows PC (or virtual machine - VMware, Virtual Box, etc.)
    - for _radar_ and _traffic decryption tool (Cryptonite)_
2. Any other device, where you can run Albion Online (any platform)

### How to run

Next guide is for pair of Windows PC (or VM with Windows) and another PC.

PCs should be in the same network, and if you are going to use VM, make sure that it uses NAT or Bridged network adapter

#### Install some required components (only first time) on 1st PC (or VM)

### 1st PC (or VM) for radar

1. Install npcap: https://npcap.com/dist/npcap-1.80.exe
2. Install .NET Framework 4.8 Runtime: https://dotnet.microsoft.com/download/dotnet-framework/net48
3. Download the latest build from [Releases](https://github.com/pxlbit228/albion-radar-deatheye-2pc/releases) and extract it to any directory

   ⚠️ Download and install [font from current repository](https://github.com/pxlbit228/albion-radar-deatheye-2pc/raw/refs/heads/master/Design/Font/DEATHEYE.ttf) to show icons on the overlay correctly.
4. Download items images from https://drive.google.com/file/d/1Egji6ceOt3eBh6yE9-zxB4OPtoWMYSby/view?usp=sharing and
   extract directory ITEMS (it contains only images) to root directory of the radar.
   Replace all previous files if asked.
5. Download ao-bin-dumps
   from [https://github.com/ao-data/ao-bin-dumps](https://github.com/ao-data/ao-bin-dumps/archive/refs/heads/master.zip)
   and content of directory ao-bin-dumps-master into ao-bin-dumps directory in root directory of the radar.
   Replace all previous files if asked.
6. Download the latest version of traffic decryption tool Cryptonite from our Discord server (channel `#cryptonite`) and
   extract all files to any directory.
7. Find local IP address of this PC (or VM) ([Video Tutorial](https://youtu.be/goTdaCFog3U))
   
   ⚠️ Will be used in the next steps as `my_local_ip`. This is the IP address of **the 1st PC (or VM)**.
8. You should see the following files:
    - `Cryptonite.CLI.exe` - main executable, don't run it directly because it need options, see eu.bat for example.
    - `eu.bat` - for Europe
    - `am.bat` - for America
    - `as.bat` - for Asia
    
   ⚠️ Only one can be used at a time.
9. Choose file for your server and open it in Notepad, replace `my_local_ip` with your local IP address and save the
   file.

### 2nd PC for Albion Online client

We just redirect the game client traffic to the 1st PC.
Your PCs should be in the same network.

#### Windows

1. Run Notepad as Administrator.
2. Open the file `C:\Windows\System32\drivers\etc\hosts`.
3. Add the following lines at the end of the file (replace `my_local_ip` with your local IP address of the 1st PC):
   ```
   my_local_ip live03-loginserver.ams.albion.zone # for Europe
   my_local_ip live02-loginserver.sg.albion.zone # for Asia
   my_local_ip loginserver.live.albion.zone # for America
   ```
4. Save the file.
5. [Optional] Sometimes you need to [flush DNS cache](https://serverfault.com/questions/452268/hosts-file-ignored-how-to-troubleshoot) to make changes work:
   - Open Command Prompt as Administrator
   - Run the command `ipconfig /flushdns`

#### Linux

Linux users should add the lines to `/etc/hosts` file (ex: use terminal and `sudo nano /etc/hosts`).

#### macOS

Mac users should add the lines to `/private/etc/hosts` file (ex: use terminal and `sudo nano /private/etc/hosts`).

📌 If you don't want to use Cryptonite anymore, just remove the lines from the `hosts` file (or comment them
typing `#` at the beginning of the line).

That's all, next time you can just run Cryptonite and radar on the 1st PC and Albion Online on the 2nd PC.

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