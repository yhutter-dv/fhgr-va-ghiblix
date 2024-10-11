# Ghiblix
Visual Analytics Tool for exploring the Studio Ghibli movies. 

## IDE
I recommend the `Rider IDE` which can be downloaded from [here](https://www.jetbrains.com/rider/download/).

## Prerequisites
Please make sure that you have `.NET 8.0` installed on your System. You can download it [here](https://dotnet.microsoft.com/en-us/download).
In order to verify that you have installed the `dotnet` SDK correctly please run the following command in your terminal

```bash
dotnet --version
```

The output of this command should be something like `8.x`.

> :warning: Please note that the the following commands assume that you are inside the  `src` folder.

## Building the Application
Building the Application should be relatively straightforward now that the `dotnet` SDK is installed.

### Windows 
dotnet publish -c Release -r win-x64

### MacOS (Intel)
dotnet publish -c Release -r osx-x64

### MacOS (ARM)
dotnet publish -c Release -r osx-arm64

## Running the Application
If you just want to run the Application without building it, you can simply execute the command `dotnet run`.

## Acknowledgments
Without the awesome people behind these GitHub Projects this would not have been possible.

- [Raylib-CS Bindings](https://github.com/MrScautHD/Raylib-CSharp)
- [Flexoki Color Scheme](https://github.com/kepano/flexoki)
