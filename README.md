ModpackCreator allows you to create your own Modpacks with ease! With a simplistic and easy to understand UI and doing all the ID finding heavy lifting for you.

- [Setup](#how-do-you-use-modpackcreator)
- [Creating a Modpack](#creating-your-modpack)
    - [Configuring the Manifest](#configuring-the-manifest)
    - [Adding Mods](#adding-mods)
    - [Adding Overrides](#adding-overrides)
- [Exporting Your Modpack](#exporting-your-modpack)
- [Installing Your Modpack](#installing-your-modpack)

# How do you use ModpackCreator?
First unzip the programme from the files attached and run `ModpackCreator.exe`. This is not malware, the only files it touches are the ones you upload to it and the ones in creates in `%temp%/ModpackCreator`.

![image](https://user-images.githubusercontent.com/64277238/145723048-58c32e3b-daac-46dd-a2b8-63cf6704f039.png)

A window should pop up that looks like this:

![image](https://user-images.githubusercontent.com/64277238/145723074-aea81eeb-971b-4aae-b44e-89a90e969a32.png)

# Creating your Modpack
## Configuring the Manifest
A good first step when making a Modpack is setting your manifest properties. Click on `Edit Manifest`, and another window should pop up that looks like this:

![image](https://user-images.githubusercontent.com/64277238/145723146-38870ab3-1eae-419f-b217-e9660e715781.png)

The settings are fairly self explanatory: `Minecraft Settings` are parts that are relevant to the game requirements such as **game version** and **modloader**

Enter your targeted Minecraft version, the mod loader you wish to use and the latest version of the modloader for the Minecraft version you are using.

![image](https://user-images.githubusercontent.com/64277238/145723260-a34c8431-d9ad-4e41-a809-5b944f2e164d.png)

Next scroll down to the `Modpack Settings`. These are settings relative to your modpack, including the name of your modpack, the version of your modpack and the modpack author (that's you!)

The name can be whatever you want, for the version: I highly recommend that you stick to the [Semantic Versioning](https://semver.org/) system. And the author(s) section is you and anyone else that planned the modpack with you. If it's just you, just put your name in that section or if its multiple people, separate each name with a comma (`,`).

![image](https://user-images.githubusercontent.com/64277238/145723432-0c4353f5-cbe5-4f07-a975-e26beebe8030.png)

Once you are done click `Save Manifest` at the bottom of the page. The manifest window should close

## Adding Mods

To upload mods to the Modpack. Download them from [CurseForge](https://www.curseforge.com/minecraft/mc-mods). All mods uploaded MUST be on CurseForge and unaltered. This is due to the fact that in order to fetch relevant info about the mod, it needs to be unaltered to calculate the hash or the file, the hash then gets searched for on CurseForge's API.

Click on `Upload Mod JARs`, this will open an Open File Dialogue, locate the mods you downloaded, select them and press `Open`

![image](https://user-images.githubusercontent.com/64277238/145723757-4e951f03-6e58-4811-8169-552e3b48852b.png)

After this the mods should appear in the grey rectangle below the buttons at the top. You can remove any of the mods at any time by clicking the red `Remove` button.

![image](https://user-images.githubusercontent.com/64277238/145723804-b962c775-9b3f-48f9-9b00-6db02eba106f.png)

## Adding Overrides

Overrides are files that get copied into the base Minecraft instance directory. They are usually used to hold Resource Packs and config files for the Modpack, so they don't have to be installed later. For this example we will add a few config files to it.

Click `Open Overrides`, a File Explorer window should open in the location of the overrides directory.

![image](https://user-images.githubusercontent.com/64277238/145723986-ba240cb0-3f59-44bf-8462-6735ef65b1fd.png)

In this example I will copy the `config` folder from the Minecraft instance I want to use into the `overrides` folder.

![image](https://user-images.githubusercontent.com/64277238/145724073-99555afd-c842-4bc4-a26c-06bf857d9815.png)

As I said, you can add resource packs and other files here as well, just make sure they are in their appropriate directory respective to the instance root.

# Exporting your Modpack

This is the easy part: Press `Export Modpack` at the bottom of the main window. A Save File Dialogue will pop up, save the Modpack anywhere  that is easily accessible. This will write the manifest, the modist and the overrides into a Zip Archive, which is then saved to your chosen location.

![image](https://user-images.githubusercontent.com/64277238/145724177-4b7bdd42-df1e-43f3-b347-f5e18f50302b.png)

# Installing your Modpack

There are many ways to install modpacks. We will go over the 3 easiest options: **ModpackInstaller**, **MultiMC** and **CurseForge**

### ModpackExtractor
Use guide: https://github.com/MillzyDev/ModpackExtractor/blob/main/README.md

### MultiMC
Download and Install MultiMC from [here](https://multimc.org/#Download).

Once you have setup and logged into MultiMC, click on `Create Instance` in the top left corner

![image](https://user-images.githubusercontent.com/64277238/145724429-71d3cacc-73b9-448c-a499-053c9e8974d0.png)

A window will pop up. In the sidebar select `Import From Zip`

![image](https://user-images.githubusercontent.com/64277238/145724467-4b6bde30-8e61-4d3b-96ed-e7c77f5d5d86.png)

Click on `Browse`. An Open File Dialog will open, locate your Modpack and `Open` it. 

![image](https://user-images.githubusercontent.com/64277238/145724548-c7ef8c85-f6fb-45e2-bdc3-2b4a1ca9ac0e.png)

Click `Ok` in the window. MultiMC will now handle installing the correct Minecraft and Fabric versions, download and install the mods and copy `overrides` contents to the instance root folder.

Double click the new created instance, or select it and press `Launch` in the right sidebar.

![image](https://user-images.githubusercontent.com/64277238/145724657-cf7028a1-3722-47b0-aaf8-038776d83b05.png)

Wait a few seconds and you will now have a version of Minecraft running your custom modpack!

### CurseForge

Due to the CurseForge app being large and very diverse, I will link a tutorial [here](https://support.overwolf.com/en/support/solutions/articles/9000196984-installing-modpacks).
