# ShellHelper
It's an utility that can help you to execute OS script or script file like bat file on windows or shell file on linux.

**Nuget**
```sh
# Package Manager
Install-Package ColinChang.ShellHelper

# .NET CLI
dotnet add package ColinChang.ShellHelper
```

> Tips

* You can not control the real time or orders when the scripts will be executed eventually because it will new processes to run different script or script files.
Plus,different scripts can not share a context.By the way,you can't know what time will it be finished after staring a new processes to execute scripts. 

* When one script file being executed in one process,all scrips will be executed orderly,and it will share the context.

* Some unexpected exceptions perhaps will be thrown when executing scripts directly.So only simple scripts should be executed directory.For complex scripts, 
**We strongly recommend that put all of your scripts in one script file and execute it.** 

* Argument transfer is also supported.

* Windows/Linux/OS X are all supported.

* Be sure the property of script files is executable and the right permission are granted.  

About how to use this,please check the [Sample project](https://github.com/colin-chang/ShellHelper/tree/master/ColinChang.ShellHelper.Sample).
