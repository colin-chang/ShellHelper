# ShellHelper
It's a utility that can help you to execute commands or command files(like bat file on windows or shell file on linux) on OS.

**Nuget**
```sh
# Package Manager
Install-Package ColinChang.ShellHelper

# .NET CLI
dotnet add package ColinChang.ShellHelper
```

> Tips

* You can not control the real time or orders when commands will be executed eventually because it will new processes to run different commands or command files.
Plus, different commands can not share a context.By the way, you can't know what time will it be finished after staring a new processes to execute commands. 

* When one command file being executed in one process, all scrips will be executed orderly, and it will share the context.

* Some unexpected exceptions perhaps will be thrown when executing commands directly.So only simple commands should be executed directory.For complex commands, 
**We strongly recommend that put all of your commands in one command file and execute it.** 

* Argument transfer is also supported.

* Windows/Linux/OS X are all supported.

* Be sure the property of command files is executable and the right permission are granted.  

About how to use this, please check the [Sample project](https://github.com/colin-chang/ShellHelper/tree/master/ColinChang.ShellHelper.Sample).
