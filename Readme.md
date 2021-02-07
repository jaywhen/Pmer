# Pmer

Pmer(**P**assword **m**anag**er**) 是一个 ~~bug比功能多的~~ 简易的、离线的密码管理器，支持从Google chrome 的密码 .csv 文件中导入密码。


<div align="center">
    <img style="border-radius: 0.3125em; box-shadow: 0 2px 4px 0 rgba(34,36,38,.12),0 2px 10px 0 rgba(34,36,38,.08);"
     src="https://cdn.jsdelivr.net/gh/jaywhen/imageBed/imgPmer20.PNG"
     />
    <br />
</div>
<div align="center">
        Register
</div>



<div align="center">
    <img style="border-radius: 0.3125em; 
     box-shadow: 0 2px 4px 0 rgba(34,36,38,.12),0 2px 10px 0 rgba(34,36,38,.08);"
     src="https://cdn.jsdelivr.net/gh/jaywhen/imageBed/imgPmer21PNG.PNG"
         />
    <br />
</div>


<div align="center">
        Login
</div>




<div align="center">
    <img style="border-radius: 0.3125em; 
     box-shadow: 0 2px 4px 0 rgba(34,36,38,.12),0 2px 10px 0 rgba(34,36,38,.08);"
     src="https://cdn.jsdelivr.net/gh/jaywhen/imageBed/imgPmer23.PNG"
         />
</div>


<div align="center">
        Main
</div>
<div align="center">
    <img style="border-radius: 0.3125em; 
     box-shadow: 0 2px 4px 0 rgba(34,36,38,.12),0 2px 10px 0 rgba(34,36,38,.08);"
     src="https://cdn.jsdelivr.net/gh/jaywhen/imageBed/imgPmer25PNG.PNG"
         />
</div>


<div align="center">
        About
</div>


## 加密方式

- 登录密码：将登录密码首尾加盐后做一遍 `SHA512` 存入库中，用户再次登录时，对明文做同样的操作并与库中值比对；
- 用户存储的密码：采用 `AES` 的加密方式，密钥是用户的登录密码明文进行一遍 `MD5` 后的值。

## 感谢

-  [WPF](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/?view=netdesktop-5.0) + [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) {:-< 我没有系统的学习过 `C#` 与 `WPF`，由此可见代码可能比较丑陋}
- [MaterialDesignInXaml](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
- [Sqlite](https://sqlite.org/index.html)
- [EF](https://docs.microsoft.com/zh-cn/ef/core/)
- ...

