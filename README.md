# GUI-MEU and what it is
GUI-MEU (pronounced `Gooey Mew`), standing for **G**raphical **U**ser **I**nterface **M**icrosoft **E**dge **U**ninstaller, is a simple application which allows for uninstalling Microsoft Edge. Over time, Microsoft have been making it harder and harder to uninstall Edge and this tool aims to make it easy and painless.

## Prebuilt Binary (Recommended)
This prebuilt binary is the latest release of GUI-MEU packaged in a neat and handy installer and uninstaller built with NSIS. It's recommended that you use this as it creates a service file which runs GUI-MEU on login, this allows it to check for if Microsoft Edge is installed, if it is, it'll ask you if you would like to uninstall it.  
  
[GUI-MEU Installer.exe](https://cdn.discordapp.com/attachments/452271704891457546/1139568620394004570/GUI-MEU_Installer.exe)

## Building
- Dependencies:
    - Visual Studio 2022
        - .Net desktop development module

1. Clone the repository to a destination of your choosing.
2. Open the solution file inside (`GUI-MEU.sln`)
3. At the top of VS2022, click the dropdown which is set to `Debug` and select `Release`
4. Click `Build`
5. Click `Build Solution`
6. Navigate to the path of the generated binary and move it to wherever you want to permenantly store it.

## Setting Up
The installer takes care of this all by itself, but if built via VS2022 a step is skipped which is responsible for making the executable automatically run on login.
1. Open `Task Scheduler`
2. In the rightmost panel click `Crate Task...`
3. Give the task a name
4. Set to `Run only when user is logged on`
5. Tick `Run with highest priveleges`
6. Navigate to the `Triggers` tab
7. Click `New...`
8. Select the `At log on` option for the `Begin the task:` dropdown
9. Click `OK`
10. Navigate to the `Actions` tab
11. Click `New...`
12. Click `Browse...`
13. Navigate to where you stored your compiled executable and select it
14. Click `OK`
15. Click `OK` again
16. Done. When you login, the process will automatically execute and check for Edge and uninstall it if you want it to.