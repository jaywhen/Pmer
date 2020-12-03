# Pmer

Pmer(**P**assword **m**anag**er**) 是一个 ~~bug比功能多的~~ 简易的、离线的密码管理器。



<center>
    <img style="border-radius: 0.3125em; box-shadow: 0 2px 4px 0 rgba(34,36,38,.12),0 2px 10px 0 rgba(34,36,38,.08);"
     src="https://cdn.jsdelivr.net/gh/jaywhen/imageBed/imgregi.png"
     />
    <br />
</center>
<div align="center">
        Register
</div>





<center>
    <img style="border-radius: 0.3125em; 
     box-shadow: 0 2px 4px 0 rgba(34,36,38,.12),0 2px 10px 0 rgba(34,36,38,.08);"
     src="https://cdn.jsdelivr.net/gh/jaywhen/imageBed/imgregi.png"
         />
    <br />
</center>

<div align="center">
        Login
</div>





<center>
    <img style="border-radius: 0.3125em; 
     box-shadow: 0 2px 4px 0 rgba(34,36,38,.12),0 2px 10px 0 rgba(34,36,38,.08);"
     src="https://cdn.jsdelivr.net/gh/jaywhen/imageBed/imgmain.png"
         />
</center>

<div align="center">
        Main && About
</div>






## 加密方式

- 登录密码：将登录密码首尾加盐后做一遍 `SHA512` 后存入库中，用户再次登录时，对明文做同样的操作并与库中值比对；
- 用户存储的密码s：采用 `AES` 的加密方式，密钥是用户的登录密码明文进行一遍 `MD5` 后的值。

## Pmer是用这些构建的：

-  [WPF](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/?view=netdesktop-5.0) + [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) {:-< 我没有系统的学习过 `C#` 与 `WPF`，由此可见代码可能比较丑陋}
- [MaterialDesignInXaml](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
- [Sqlite](https://sqlite.org/index.html)
- ...

