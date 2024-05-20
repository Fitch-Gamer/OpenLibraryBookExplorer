# OpenLibraryBookExplorer

## Build

The Repo contains a build under /Built and the actual .exe is /Built/OpenLibraryBookExplorer.exe

In order to make another build, OpenLibraryBookExplorer/OpenLibraryBookExplorer.sln must be opene in Visual studio 2022 or later and then published through there. Running the raw .exe contained within the Bin file will not work.

## Hosting

The HTTP version of the web app is hosted on http://localhost:5000 by default, this can be changed by editing the LaunchOptions.json and rebuilding.

### Notes

The OpenLibrary api is subject to change and as such the api may become deprecated over time.

The JS query code uses the fetch API however there is a commented version of an AJAX query that cna be used instead and can be rebuilded.

The build can also be made to use HTTPS if a certificate is attached and then rebuilt. 
 
