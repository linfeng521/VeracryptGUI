# VeracryptGUI

## 项目介绍
利用command调用Veracrypt执行挂载创建磁盘等操作
### 目录介绍
1 VeraRelease:Veracypt程序目录
2 根目录为C# 使用WPF创建的GUI界面
3 PythonCode: 使用python argparse构建的命令行程序
### 运行截图
![C# WPF 运行界面](Images\WPF主窗口运行界面.png "C# WPF 运行界面")
![C#创建磁盘 运行界面](Images\WPF子窗口创建磁盘界面.png "WPF子窗口创建磁盘界面")
### TODO
1 继续完善状态栏显示
2 优化界面显示效果
3 完成主体功能
## 使用示例：
[veracrypt官网](https://veracrypt.fr/en/Home.html "veracrypt官网")
![veracrypt命令行帮助](Images\Veracrypt命令行帮助.png "Veracrypt命令行帮助")
1 挂在文件到O盘符下：
```VeraCrypt.exe /q /l o /v e:\veracryptnfs.disk```

2  卸载加载到O盘的卷
```VeraCrypt.exe /Q /l o /d```

3 显示帮助：
```Veracrypt.exe /?```

## Code snippet

创建加密卷：
`VeraCrypt Format.exe" /create "C:\temp\vctest.vc" /size "200M" /password MySuperSecurePassword1! /encryption AES /hash sha-512 /filesystem exfat /pim 0 /silent`

挂载卷：

`\VeraCrypt.exe" /volume "C:\temp\vctest.vc" /letter x /password MySuperSecurePassword1! /quit /silent`

卸载卷：
`"C:\Program Files\VeraCrypt\VeraCrypt.exe" /dismount X /quit /silent /force`

参考文档
[VeraCrypt On The Command Line for Windows – Arcane Code](https://arcanecode.com/2021/06/14/veracrypt-on-the-command-line-for-windows/ "VeraCrypt On The Command Line for Windows – Arcane Code")
